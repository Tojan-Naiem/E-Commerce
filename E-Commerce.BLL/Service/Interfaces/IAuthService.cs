using E_Commerce.DAL.DTO.Request;
using E_Commerce.DAL.DTO.Response;
using Microsoft.AspNetCore.Http;
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
        public Task<UserResponse> RegisterAsync(RegisterRequest request, HttpRequest httpRequest);
        public Task<string> ConfirmEmail(string token, string userId);
        public  Task<bool> ForgotPassword(ForgotPasswordRequest request);
        public  Task<bool> ResetPassword(ResetPasswordRequestDTO request);


    }
}
