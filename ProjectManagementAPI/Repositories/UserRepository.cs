using EFCoreAPI.Entities.DTOs;
using EFCoreAPI.Repositories.Interfaces;
using EFCoreAPI.Repositories.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System;

namespace EFCoreAPI.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext dbContext;
        public UserRepository(AppDbContext _dbContext)
        {
            dbContext = _dbContext;
        }

        public async Task Add(User model)
        {
            dbContext.Users.Add(model);
            await dbContext.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var userModel = await dbContext.Users.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (userModel != null)
            {
                userModel.IsActive = false;
                dbContext.Users.Update(userModel);
                await dbContext.SaveChangesAsync();
            }
        }

        public async Task<List<User>> GetAll(bool includeRelations = false)
        {
            return await dbContext.Users
                    .Include(p => p.Person)
                    .ToListAsync();
        }

        public async Task<User> GetById(int id, bool includeRelations = false)
        {
            return await dbContext.Users.Where(x => x.Id == id).Include(p => p.Person).FirstOrDefaultAsync();
        }

        public async Task<User> GetUserByDomainName(string domainName)
        {
            return await dbContext.Users.Where(u => u.DomainName == domainName).FirstOrDefaultAsync();
        }

        public async Task Update(User model)
        {
            dbContext.Users.Update(model);
            await dbContext.SaveChangesAsync();
        }
    }
}
