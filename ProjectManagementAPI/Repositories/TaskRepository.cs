using EFCoreAPI.Repositories.Interfaces;
using EFCoreAPI.Repositories.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace EFCoreAPI.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly AppDbContext dbContext;
        public TaskRepository(AppDbContext _dbContext)
        {
            dbContext = _dbContext;
        }

        public async Task Add(TaskModel task)
        {
            dbContext.Tasks.Add(task);
            await dbContext.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var taskModel = await dbContext.Tasks.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (taskModel != null)
            {
                taskModel.IsActive = false;
                dbContext.Tasks.Update(taskModel);
                await dbContext.SaveChangesAsync();
            }
        }

        public async Task<List<TaskModel>> GetAll(bool includeRelations)
        {
            if (includeRelations)
            {
                return await dbContext.Tasks
                    .Include(t => t.Project)
                    .Include(t => t.Status)
                    .Include(t => t.TaskUsers)
                        .ThenInclude(t => t.User)
                    .ToListAsync();
            }
            else
            {
                return await dbContext.Tasks
                    .Include(t => t.Project)
                    .Include(t => t.Status)
                    .ToListAsync();
            }
        }

        public async Task<TaskModel> GetById(int id, bool includeRelations)
        {
            if (includeRelations)
            {
                return await dbContext.Tasks.Where(t => t.Id == id)
                    .Include(t => t.Project)
                    .Include(t => t.Status)
                    .Include(t => t.Comments)
                    .Include(t => t.TaskUsers)
                        .ThenInclude(t => t.User)
                    .FirstOrDefaultAsync();
            }
            else
            {
                return await dbContext.Tasks
                    .Include(t => t.Project)
                    .Include(t => t.Status)
                    .Where(t => t.Id == id)
                    .FirstOrDefaultAsync();
            }
        }

        public async Task Update(TaskModel model)
        {
            dbContext.Update(model);
            await dbContext.SaveChangesAsync();
        }

    }
}
