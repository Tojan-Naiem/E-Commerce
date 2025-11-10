using E_Commerce.BLL.Service.Interfaces;
using E_Commerce.DAL.DTO.Request;
using E_Commerce.DAL.DTO.Response;
using E_Commerce.DAL.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.BLL.Service.Classes
{
    public class AuthenticationService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IEmailSender _emailSender;
        public AuthenticationService(
             UserManager<ApplicationUser> userManager,
             IConfiguration configuration,
             IEmailSender emailSender
            )
        {
            _configuration = configuration;
            _userManager = userManager;
            _emailSender = emailSender;
        }
        public async Task<UserResponse> LoginAsync(LoginRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (!await _userManager.IsEmailConfirmedAsync(user))
            {
                throw new Exception("Not confirmed email");
            }
            if(user is null)
            {
                return new UserResponse()
                {
                    Token = "Not found"
                };
            }
            var isCorrect = await _userManager.CheckPasswordAsync(user, request.Password);
            if(!isCorrect)
            {
                throw new Exception("Invalid Password");
            }

            return new UserResponse()
            {
                // create token
                Token = await CreateTokenAsync(user)
            };
        }
        public async Task<string>ConfirmEmail(string token,string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if(user is not null)
            {

            }
            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                return "Email is confirmed successfully";
            }
            return "Email confirmaion failed";
        }

        public async Task<UserResponse> RegisterAsync(RegisterRequest request)
        {
            var user = new ApplicationUser()
            {
                FullName = request.FullName,
                UserName = request.UserName,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber
            };
           var result= await _userManager.CreateAsync(user, request.Password);

            if (result.Succeeded)
            {
                var token =await  _userManager.GenerateEmailConfirmationTokenAsync(user);
                var escapeToken = Uri.EscapeDataString(token); 
                var emailUrl = $"https://localhost:7039/api/Account/ConfirmedEmail?token={escapeToken}&userId={user.Id}";
                await _emailSender.SendEmailAsync(user.Email, $"Welcome to the new user for my lovely ecommerce >3", $"Welcome , we are totaly happy that u register to our ecommerce ," +
                    $" we are too lucky cuz we have a new member ! lolololololoishhhhhhhhhhhhhhhhhhhhhhhhhhhhh <a href={token}></a> ");
                return new UserResponse()
                {
                    Token = request.Email
                };
            }
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));

            throw new Exception(errors);
        }
        private async Task<string> CreateTokenAsync(ApplicationUser user)
        {
            Console.WriteLine("full name :  "+user.FullName);
            Console.WriteLine(user.Email);
            Console.WriteLine(user.Id);

            var Claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name,user.FullName),
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(ClaimTypes.NameIdentifier,user.Id)
            };
            var Roles = await _userManager.GetRolesAsync(user);
            foreach(var role in Roles)
            {

                Claims.Add(new Claim(ClaimTypes.Role, role));


            }
            var securityKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration.GetSection("jwtOptions")["SecretKey"])
            );
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                claims: Claims,
                expires:DateTime.Now.AddDays(15),
                signingCredentials:credentials
                );
            // convert token from object to string 
            return new JwtSecurityTokenHandler().WriteToken(token);

        }
    }
}
