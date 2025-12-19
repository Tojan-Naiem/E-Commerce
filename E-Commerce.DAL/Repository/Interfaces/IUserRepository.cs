using E_Commerce.DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.DAL.Repository.Interfaces
{
    public interface IUserRepository
    {
        public Task<List<ApplicationUser>> GetAllAsync();
        public Task<ApplicationUser> GetByIdAsync(string UserId);
        public Task<bool> BlockUserAsync(string UserId, int days);
        public Task<bool> UnBlockUserAsync(string UserId);

    }
}
