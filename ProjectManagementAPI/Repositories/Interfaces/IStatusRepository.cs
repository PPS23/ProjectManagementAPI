using EFCoreAPI.Repositories.Models;

namespace EFCoreAPI.Repositories.Interfaces
{
    public interface IStatusRepository: IBaseRepository<Status>
    {
        Task<Status> GetByDescription(string description);
    }
}
