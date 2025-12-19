using E_Commerce.DAL.DTO.Response;
using E_Commerce.DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.BLL.Service.Interfaces
{
    public interface IUserService
    {
        public Task<List<UserDTO>> GetAllAsync();
        public Task<UserDTO> GetByIdAsync(string UserId);
        public Task<bool> BlockUserAsync(string UserId, int days);
        public Task<bool> UnBlockUserAsync(string UserId);


    }
}
