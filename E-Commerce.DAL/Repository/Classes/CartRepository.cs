using E_Commerce.DAL.DTO.Request;
using E_Commerce.DAL.DTO.Response;
using E_Commerce.DAL.Model;
using E_Commerce.DAL.Repository.Interfaces;
using E_Commerce.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.DAL.Repository.Classes
{
    public class CartRepository : ICartRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public CartRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<bool> AddAsync(Cart Cart)
        {
            _dbContext.Cart.Add(Cart);
            await _dbContext.SaveChangesAsync();
            return true;

        }

        public async Task<List<Cart>> GetAsync(string UserId)
        {
            return await _dbContext.Cart.Include(
                c => c.Product
                ).Where(c => c.UserId == UserId).ToListAsync();
            
        }
        public void DeleteAsync(Cart Cart)
        {
            _dbContext.Cart.Remove(Cart);
        }
        public async Task SaveChangesInDatabaseAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
