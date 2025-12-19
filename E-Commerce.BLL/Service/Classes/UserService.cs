using E_Commerce.BLL.Service.Interfaces;
using E_Commerce.DAL.Model;
using E_Commerce.DAL.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.BLL.Service.Classes
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<List<ApplicationUser>> GetAllAsync()
        {
            return await _userRepository.GetAllAsync();
        }

        public async Task<ApplicationUser> GetByIdAsync(string UserId)
        {
            return await _userRepository.GetByIdAsync(UserId);
        }
    }
}
