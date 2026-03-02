namespace EFCoreAPI.Services.Interfaces
{
    public interface IServiceBase<TResponse, TRequest> 
        where TResponse : class
        where TRequest : class
    {
        Task Add(TRequest model);
        Task Update(int id, TRequest model);
        Task Delete(int id);
        Task<List<TResponse>> GetAll(bool includeRelations);
        Task<TResponse> GetById(int id, bool includeRelations);
    }
}
