using EFCoreAPI.Repositories.Interfaces;
using EFCoreAPI.Repositories.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EFCoreAPI.Repositories
{
    public class PersonRepository: IPersonRepository
    {
        private readonly AppDbContext dbContext;
        public PersonRepository(AppDbContext _dbContext)
        {
            dbContext = _dbContext; 
        }

        public async Task Add(Person model)
        {
            dbContext.Persons.Add(model);
            await dbContext.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var person = await dbContext.Persons.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (person != null)
            {
                person.IsActive = false;
                dbContext.Persons.Update(person);
                await dbContext.SaveChangesAsync();
            }
        }

        public async Task<List<Person>> GetAll(bool includeRelations = false)
        {
            return await dbContext.Persons.ToListAsync();
        }

        public async Task<Person> GetById(int id, bool includeRelations)
        {
            return await dbContext.Persons.Where(p => p.Id == id).FirstOrDefaultAsync();
        }

        public async Task Update(Person model)
        {
            dbContext.Update(model);
            await dbContext.SaveChangesAsync();
        }
    }
}
