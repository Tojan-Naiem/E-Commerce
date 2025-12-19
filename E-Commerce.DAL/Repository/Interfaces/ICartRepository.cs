using E_Commerce.DAL.DTO.Request;
using E_Commerce.DAL.DTO.Response;
using E_Commerce.DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.DAL.Repository.Interfaces
{
    public interface  ICartRepository
    {
        public Task<bool> AddAsync(Cart Cart);
        public Task<List<Cart>> GetAsync(string UserId);
        public void DeleteAsync(Cart Cart);
        public Task SaveChangesInDatabaseAsync();

    }
}
