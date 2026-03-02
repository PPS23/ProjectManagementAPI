using EFCoreAPI.Repositories.Models;

namespace EFCoreAPI.Repositories.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        Task Add(T model);
        Task Update(T model);
        Task Delete(int id);
        Task<List<T>> GetAll(bool includeRelations);
        Task<T> GetById(int id, bool includeRelations);
    }
}
