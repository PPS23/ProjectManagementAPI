using EFCoreAPI.Entities.DTOs.Persons;
using EFCoreAPI.Repositories.Models;
using Microsoft.AspNetCore.Mvc;

namespace EFCoreAPI.Services.Interfaces
{
    public interface IPersonService: IServiceBase<PersonResponseDto, PersonRequestDto>
    {
    }
}
