using EFCoreAPI.Repositories.Interfaces;
using EFCoreAPI.Repositories.Models;
using Microsoft.EntityFrameworkCore;

namespace EFCoreAPI.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly AppDbContext appDbContext;
        public ProjectRepository(AppDbContext _dbContext) 
        {
            appDbContext = _dbContext;
        }

        public async Task Add(Project model)
        {
            appDbContext.Projects.Add(model);
            await appDbContext.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var projectModel = await appDbContext.Projects.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (projectModel != null)
            {
                projectModel.IsActive = false;
                appDbContext.Projects.Update(projectModel);
                await appDbContext.SaveChangesAsync();
            }
        }

        public async Task<List<Project>> GetAll(bool includeRelations = false)
        {
            return await appDbContext.Projects.ToListAsync();
        }

        public async Task<Project> GetById(int id, bool includeRelations = false)
        {
            return await appDbContext.Projects.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task Update(Project model)
        {
            appDbContext.Projects.Update(model);
            await appDbContext.SaveChangesAsync();
        }
    }
}
