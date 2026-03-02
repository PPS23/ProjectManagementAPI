using EFCoreAPI.Repositories.Models;
using Microsoft.AspNetCore.Mvc;

namespace EFCoreAPI.Repositories.Interfaces
{
    public interface IUserRepository: IBaseRepository<User>
    {
        Task<User> GetUserByDomainName(string domainName);
    }
}
