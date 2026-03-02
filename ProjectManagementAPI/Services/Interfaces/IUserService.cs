using EFCoreAPI.Entities.DTOs.Users;
using EFCoreAPI.Repositories.Models;

namespace EFCoreAPI.Services.Interfaces
{
    public interface IUserService: IServiceBase<UserResponseDto, UserRequestDto>
    {
        Task<CurrentUserDto> GetLoginByDomainNameAndPassword(LoginDto loginDto);
    }
}
