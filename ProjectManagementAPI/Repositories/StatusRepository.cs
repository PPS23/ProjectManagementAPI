using EFCoreAPI.Repositories.Interfaces;
using EFCoreAPI.Repositories.Models;
using Microsoft.EntityFrameworkCore;

namespace EFCoreAPI.Repositories
{
    public class StatusRepository : IStatusRepository
    {
        private readonly AppDbContext dbContext;
        public StatusRepository(AppDbContext _dbContext) 
        {
            dbContext = _dbContext;
        }

        public async Task Add(Status model)
        {
            dbContext.Statuses.Add(model);
            await dbContext.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var status = await dbContext.Statuses.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (status != null)
            {
                status.IsActive = false;
                dbContext.Statuses.Update(status);
                await dbContext.SaveChangesAsync();
            }
        }

        public async Task<List<Status>> GetAll(bool includeRelations = false)
        {
            return await dbContext.Statuses.ToListAsync();
        }

        public async Task<Status> GetByDescription(string description)
        {
            return await dbContext.Statuses.Where(x => x.Description.Equals(description)).FirstOrDefaultAsync();
        }

        public async Task<Status> GetById(int id, bool includeRelations = false)
        {
            return await dbContext.Statuses.FindAsync(id);
        }

        public async Task Update(Status model)
        {
            dbContext.Update(model);
            await dbContext.SaveChangesAsync();
        }
    }
}
