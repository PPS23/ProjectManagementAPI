using EFCoreAPI.Repositories.Interfaces;
using EFCoreAPI.Repositories.Models;
using Microsoft.EntityFrameworkCore;

namespace EFCoreAPI.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly AppDbContext dbContext;
        public CommentRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task Add(Comment model)
        {
            dbContext.Comments.Add(model);
            await dbContext.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var commentModel = await dbContext.Comments.Where(c => c.Id == id).FirstOrDefaultAsync();
            if (commentModel != null)
            {
                commentModel.IsActive = false;
                dbContext.Comments.Update(commentModel);
                await dbContext.SaveChangesAsync();
            }
        }

        public async Task Update(Comment model)
        {
            dbContext.Comments.Update(model);
            await dbContext.SaveChangesAsync();
        }

        public async Task<List<Comment>> GetAll(bool includeRelations)
        {
            if (includeRelations)
            {
                return await dbContext.Comments
                    .Include(c => c.User)
                    .ToListAsync();
            }
            else
            {
                return await dbContext.Comments.ToListAsync();
            }
        }

        public async Task<Comment> GetById(int id, bool includeRelations)
        {
            if (includeRelations)
            {
                return await dbContext.Comments.Where(c => c.Id == id)
                    .Include(c=>c.User)
                    .FirstOrDefaultAsync();
            }
            else
            {
                return await dbContext.Comments.Where(c => c.Id == id).FirstOrDefaultAsync();
            }
            
        }


    }
}
