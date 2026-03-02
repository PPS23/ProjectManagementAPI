using EFCoreAPI.Entities.DTOs.Persons;
using EFCoreAPI.Entities.DTOs.Users;
using EFCoreAPI.Extensions;
using EFCoreAPI.Repositories.Interfaces;
using EFCoreAPI.Repositories.Models;
using EFCoreAPI.Services.Helpers;
using EFCoreAPI.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EFCoreAPI.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;
        private readonly IPersonRepository personRepository;
        public UserService(IUserRepository _userRepository, IPersonRepository _personRepository) 
        {
            userRepository = _userRepository;
            personRepository = _personRepository;
        }

        public async Task Add(UserRequestDto user)
        {
            if (user == null) 
            {
                throw new ArgumentNullException("User is required");
            }

            User userModel = new User();
            var person = await personRepository.GetById(user.PersonId, false);

            
            var salt = Guid.NewGuid().ToString();
            userModel.DomainName = user.DomainName.Trim();
            userModel.Email = user.Email.Trim();
            userModel.Person = person;
            userModel.LastLogin = user.LastLogin;
            userModel.Salt = salt.Trim();
            userModel.IsActive = true;
            userModel.Password = PasswordHasher.HashPassword(user.Password.Trim(), salt.Trim());

            await userRepository.Add(userModel);

        }

        public async Task Delete(int id)
        {
            var projectModel = await userRepository.GetById(id, false);
            if (projectModel != null)
            {
                projectModel.IsActive = false;
                await userRepository.Update(projectModel);
            }
        }

        public async Task Update(int id, UserRequestDto model)
        {
            if (id > 0)
            {
                if (!string.IsNullOrEmpty(model.DomainName))
                {
                    if (!string.IsNullOrEmpty(model.Email))
                    {
                        if (model.PersonId > 0)
                        {
                            var userModel = await userRepository.GetById(id, false);
                            if (userModel != null)
                            {
                                var person = await personRepository.GetById(model.PersonId, false);
                                if (person != null)
                                {
                                    userModel.DomainName = model.DomainName;
                                    userModel.Email = model.Email;
                                    userModel.PersonId = person.Id;
                                    userModel.Person = person;
                                    await userRepository.Update(userModel);
                                }
                                else
                                {
                                    throw new Exception("Person doesn't exists.");
                                }
                            }
                            else
                            {
                                throw new Exception("This project can't be updated, because was not found.");
                            }
                        }
                        else
                        {
                            throw new Exception("Person is required.");
                        }
                    }
                    else
                    {
                        throw new Exception("Email is required.");
                    }
                }
                else
                {
                    throw new Exception("Domain name is required.");
                }
            }
            else
            {
                throw new Exception("This project can't be updated, because Project Id is required.");
            }
        }

        public async Task<CurrentUserDto> GetLoginByDomainNameAndPassword(LoginDto loginDto)
        {
            if (loginDto == null) throw new Exception("The DTO of login is required.");
            if (string.IsNullOrEmpty(loginDto.DomainName)) throw new Exception("Domain name is required.");
            if (string.IsNullOrEmpty(loginDto.Password)) throw new Exception("Password is required.");

            //var user = get the user by DB
            var userModel = await userRepository.GetUserByDomainName(loginDto.DomainName.Trim());
            if (userModel != null)
            {
                string password = PasswordHasher.HashPassword(loginDto.Password.Trim(), userModel.Salt);
                if (!string.IsNullOrEmpty(password))
                {
                    if (loginDto.DomainName.Equals(userModel.DomainName, StringComparison.OrdinalIgnoreCase) && PasswordHasher.VerifyPassword(loginDto.Password, userModel.Salt, userModel.Password))
                    {
                        var claims = new[]
{
                        new Claim(ClaimTypes.Name, loginDto.DomainName),
                            //new Claim(ClaimTypes.Role, "Admin")
                        };

                        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AppSettingsGlobal.JWT.Key));
                        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                        var token = new JwtSecurityToken(
                            issuer: AppSettingsGlobal.JWT.Issuer,
                            audience: AppSettingsGlobal.JWT.Audience,
                            claims: claims,
                            expires: DateTime.Now.AddMinutes(AppSettingsGlobal.JWT.ExpiresInMinutes),
                            signingCredentials: creds);

                        return new CurrentUserDto
                        {
                            Id = 4,
                            DomainName = loginDto.DomainName,
                            Token = new JwtSecurityTokenHandler().WriteToken(token)
                        };
                    }
                    else
                    {
                        throw new Exception("User or password incorrect.");
                    }
                }
                else
                {
                    throw new Exception("Password incorrect.");
                }
            }
            else
            {
                throw new Exception("User doesn't exists.");
            }
        }

        public async Task<List<UserResponseDto>> GetAll(bool includeRelations)
        {
            try
            {
                var users = await userRepository.GetAll(false);
                var usersDto = users.Select(u => new UserResponseDto
                {
                    Id = u.Id,
                    Email = u.Email,
                    DomainName = u.DomainName,
                    IsActive = u.IsActive,
                    Person = new PersonResponseDto()
                    {
                        Id = u.Person.Id,
                        FirstName = u.Person.FirstName,
                        LastName = u.Person.LastName,
                        IsActive = u.IsActive
                    }

                }).ToList();

                return usersDto;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<UserResponseDto> GetById(int id, bool includeRelations)
        {
            if (id > 0)
            {
                var userModel = await userRepository.GetById(id, includeRelations);
                if (userModel != null)
                {
                    return new UserResponseDto
                    {
                        Id = userModel.Id,
                        DomainName = userModel.DomainName,
                        Person = new PersonResponseDto()
                        {
                            Id = userModel.Person.Id,
                            FirstName = userModel.Person.FirstName,
                            LastName = userModel.Person.LastName,
                            IsActive = userModel.IsActive
                        },
                        IsActive = userModel.IsActive
                    };
                }
                else
                {
                    throw new Exception("Project not found.");
                }
            }
            else
            {
                throw new Exception("Project Id is required.");
            }
        }
    }
}
