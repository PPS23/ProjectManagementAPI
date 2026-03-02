using EFCoreAPI.Repositories.Interfaces;
using EFCoreAPI.Repositories.Models;
using Microsoft.EntityFrameworkCore;

namespace EFCoreAPI.Repositories
{
    public class TaskUserRelationRepository : ITaskUserRelationRepository
    {
        private readonly AppDbContext dbContext;
        public TaskUserRelationRepository(AppDbContext _dbContext) 
        {
            dbContext = _dbContext;
        }

        public async Task Add(TaskUserRelation model)
        {
            dbContext.TasksUsersRelations.Add(model);
            await dbContext.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var relation = await dbContext.TasksUsersRelations.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (relation != null)
            {
                dbContext.TasksUsersRelations.Remove(relation);
                await dbContext.SaveChangesAsync();
            }
        }

        public async Task<List<TaskUserRelation>> GetAll(bool includeRelations)
        {
            return await dbContext.TasksUsersRelations.ToListAsync();
        }

        public async Task<TaskUserRelation> GetById(int id, bool includeRelations)
        {
            return await dbContext.TasksUsersRelations.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task Update(TaskUserRelation model)
        {
            dbContext.Update(model);
            await dbContext.SaveChangesAsync();
        }
    }
}
