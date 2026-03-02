using EFCoreAPI.Repositories.Models;
using Microsoft.AspNetCore.Mvc;

namespace EFCoreAPI.Repositories.Interfaces
{
    public interface IPersonRepository: IBaseRepository<Person>
    {
    }
}
