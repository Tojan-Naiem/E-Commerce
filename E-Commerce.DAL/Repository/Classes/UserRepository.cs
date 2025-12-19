using E_Commerce.DAL.Model;
using E_Commerce.DAL.Repository.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.DAL.Repository.Classes
{
    public class UserRepository:IUserRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public UserRepository(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<List<ApplicationUser>> GetAllAsync()
        {
            return await _userManager.Users.ToListAsync();
        }
        public async Task<ApplicationUser> GetByIdAsync(string UserId)
        {
            return await _userManager.FindByIdAsync(UserId);
        }
        public async Task<bool>BlockUserAsync(string UserId,int days)
        {
            var user = await _userManager.FindByIdAsync(UserId);
            if (user is null)
                return false;
            user.LockoutEnd = DateTime.UtcNow.AddDays(days);
            var result = await _userManager.UpdateAsync(user);
            return result.Succeeded;
        }
        public async Task<bool> UnBlockUserAsync(string UserId)
        {
            var user = await _userManager.FindByIdAsync(UserId);
            if (user is null)
                return false;
            user.LockoutEnd = null;
            var result = await _userManager.UpdateAsync(user);
            return result.Succeeded;
        }
        public async Task<bool> IsBlocked(string UserId)
        {
            var user = await _userManager.FindByIdAsync(UserId);
            if (user is null)
                throw new Exception("User id not found");
        
            return user.LockoutEnd.HasValue&&user.LockoutEnd>DateTime.UtcNow;
        }
    }
}
