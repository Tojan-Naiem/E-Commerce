using E_Commerce.DAL.Repository.Interfaces;
using E_Commerce.Data;
using E_Commerce.Model;
using E_Commerce.Model.Category;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.DAL.Repository.Classes
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseModel
    {
        private readonly ApplicationDbContext _dbContext;
        public GenericRepository(
            ApplicationDbContext dbContext
       )
        {
            _dbContext = dbContext;
        }

        public async Task<List<T>> GetAll(string lang = "en")=>await  _dbContext.Set<T>()
                .OrderByDescending(c => c.CreatedAt)
                .ToListAsync();

        public T? GetById(long id)=> _dbContext.Set<T>().Find(id);



        public async Task Remove(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }
         
        public async Task Save(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task SaveChangesInDatabase()
        {
            await _dbContext.SaveChangesAsync();

        }
    }
}
