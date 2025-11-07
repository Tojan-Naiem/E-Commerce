using E_Commerce.DAL.DTO.Request;
using E_Commerce.DAL.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.BLL.Service.Interfaces
{
    public interface IAuthService
    {
        public Task<UserResponse> LoginAsync(LoginRequest request);
        public Task<UserResponse> RegisterAsync(RegisterRequest request);

    }
}
