using E_Commerce.BLL.Service.Interfaces;
using E_Commerce.DAL.DTO.Response;
using E_Commerce.DAL.Model;
using E_Commerce.DAL.Repository.Interfaces;
using Mapster;
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
        public async Task<List<UserDTO>> GetAllAsync()
        {
            var users = await _userRepository.GetAllAsync();
            return users.Adapt<List<UserDTO>>();
        }

        public async Task<UserDTO> GetByIdAsync(string UserId)
        {
            var users = await _userRepository.GetByIdAsync(UserId);
            return users.Adapt<UserDTO>();
        }
        public async Task<bool> BlockUserAsync(string UserId,int days)
        {
            var result = await _userRepository.BlockUserAsync(UserId, days);
            return result;
        }
    }
}
