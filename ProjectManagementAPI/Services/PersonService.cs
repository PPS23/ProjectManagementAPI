using EFCoreAPI.Entities.DTOs.Comments;
using EFCoreAPI.Entities.DTOs.Persons;
using EFCoreAPI.Repositories;
using EFCoreAPI.Repositories.Interfaces;
using EFCoreAPI.Repositories.Models;
using EFCoreAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using ProjectManagementAPI.Entities;
using System.Reflection.Metadata.Ecma335;

namespace EFCoreAPI.Services
{
    public class PersonService: IPersonService
    {
        private readonly IPersonRepository personRepository;
        public PersonService(IPersonRepository _personRepository)
        {
            personRepository = _personRepository;
        }

        public async Task Add(PersonRequestDto model)
        {
            var personModel = new Person() 
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                IsActive = true
            };

            await personRepository.Add(personModel);
        }

        public async Task Delete(int id)
        {
            var personModel = await personRepository.GetById(id, false);
            if (personModel != null)
            {
                personModel.IsActive = false;
                await personRepository.Update(personModel);
            }
            else
            {
                throw new Exception("Person not found.");
            }
        }

        public async Task<List<PersonResponseDto>> GetAll(bool includeRelations)
        {
            var personsModelList = await personRepository.GetAll(includeRelations);
            if (personsModelList != null)
            {
                return personsModelList.Select(x => new PersonResponseDto
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    IsActive = x.IsActive
                }).ToList();
            }
            else 
            {
                throw new Exception("There are not persons.");
            }
        }

        public async Task<PersonResponseDto> GetById(int id, bool includeRelations)
        {
            var personModel = await personRepository.GetById(id, includeRelations);
            if (personModel != null)
            {
                return new PersonResponseDto 
                {
                    Id = personModel.Id,
                    FirstName = personModel.FirstName,
                    LastName = personModel.LastName,
                    IsActive = personModel.IsActive
                };
            }
            else 
            {
                throw new Exception("Person not found.");
            }
        }

        public async Task<PagedResult<PersonResponseDto>> GetPaged(bool includeRelations, int page = 1, int pageSize = 10)
        {
            var persons = await personRepository.GetAll(includeRelations);
            if (persons != null)
            {
                int totalCount = persons.Count;
                var items = persons.OrderBy(x => x.Id)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToList().Select(x => new PersonResponseDto()
                    {
                        Id = x.Id,
                        FirstName = x.FirstName,
                        LastName = x.LastName,
                        IsActive = x.IsActive
                    });
                int totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

                return new PagedResult<PersonResponseDto>
                {
                    Items = items,
                    TotalCount = totalCount,
                    Page = page,
                    PageSize = pageSize,
                    TotalPages = totalPages
                };
            }
            else
            {
                throw new Exception("There are not persons.");
            }
        }

        public async Task Update(int id, PersonRequestDto model)
        {
            var personModel = await personRepository.GetById(id, false);
            if (personModel != null)
            {
                personModel.FirstName = model.FirstName;
                personModel.LastName = model.LastName;
                personModel.IsActive = model.IsActive;
                await personRepository.Update(personModel);
            }
            else
            {
                throw new Exception("Person not found.");
            }
        }
    }
}
