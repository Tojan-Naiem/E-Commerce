using E_Commerce.BLL.Service.Interfaces;
using E_Commerce.DAL.DTO.Request;
using E_Commerce.DAL.DTO.Response;
using E_Commerce.DAL.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Runtime.Intrinsics.X86;
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
        private readonly SignInManager<ApplicationUser> _signInManager;
        public AuthenticationService(
             UserManager<ApplicationUser> userManager,
             IConfiguration configuration,
             IEmailSender emailSender,
             SignInManager<ApplicationUser> signInManager
            )
        {
            _configuration = configuration;
            _userManager = userManager;
            _emailSender = emailSender;
            _signInManager = signInManager;


        }
        public async Task<bool>ForgotPassword(ForgotPasswordRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if(user is null)
            {
                throw new Exception("User not found");
            }
            var random = new Random();
            var code = random.Next(1000, 9000).ToString();
            user.CodeResetPassword = code;
            user.PasswordResetCodeExpiry = DateTime.Now.AddMinutes(15);
            await _userManager.UpdateAsync(user);
            await _emailSender.SendEmailAsync(
                request.Email,
                "Reset ur Password !!",
                $"<h1>Reset ur password and don't forgot it ! , the code is {code}</h1>"
                );
            return true;

        }
        public async Task<bool>ResetPassword(ResetPasswordRequestDTO request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user is null) throw new Exception("User not found");
            if (user.CodeResetPassword != request.Code) throw new Exception("Code is not equal!");
            if (user.PasswordResetCodeExpiry < DateTime.Now) throw new Exception("Code expiry end!");
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user, token, request.NewPassword);

            if (result.Succeeded)
            {
                await _emailSender.SendEmailAsync(
                   request.Email,
                   "Successfully change ur password!",
                   "Done saving ur new password ! in the next time save it plz"
                    );
            }
            return true;
        }
        public async Task<UserResponse> LoginAsync(LoginRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user is null)
            {
                return new UserResponse()
                {
                    Token = "Not found"
                };
            }
            var result= await _signInManager.CheckPasswordSignInAsync(user, request.Password, true);
            if(result.Succeeded)
            {
                return new UserResponse()
                {
                    // create token
                    Token = await CreateTokenAsync(user)
                };
            }
            else if (result.IsLockedOut)
            {
                throw new Exception("Your account is locked");

            }
            else if (result.IsNotAllowed)
            {
                throw new Exception("Not confirmed email");

            }
            else
            {
                throw new Exception("Plz confirm ur email ! ");

            }
         
        }
        public async Task<string>ConfirmEmail(string token,string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if(user is  null)
            {
                throw new Exception("User not found");
            }
            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                return "Email is confirmed successfully";
            }
            return "Email confirmation failed";
        }

        public async Task<UserResponse> RegisterAsync(RegisterRequest request, HttpRequest httpRequest)
        {
            var existUser = await _userManager.FindByEmailAsync(request.Email);
            if(existUser is not null)
            {
                throw new Exception("The user already in");
            }
            var user = new ApplicationUser()
            {
                FullName = request.FullName,
                UserName = request.UserName,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber
            };
            await _userManager.AddToRoleAsync(user, "Customer");

            var result = await _userManager.CreateAsync(user, request.Password);

            if (result.Succeeded)
            {
                var token =await  _userManager.GenerateEmailConfirmationTokenAsync(user);
                var escapeToken = Uri.EscapeDataString(token); 
                var emailUrl = $"{httpRequest.Scheme}://{httpRequest.Host}/api/Account/ConfirmedEmail?token={escapeToken}&userId={user.Id}";
                await _emailSender.SendEmailAsync(user.Email, $"Welcome to the new user for my lovely ecommerce >3", $"Welcome , we are totaly happy that u register to our ecommerce ," +
                    $" we are too lucky cuz we have a new member ! lolololololoishhhhhhhhhhhhhhhhhhhhhhhhhhhhh <a href='{emailUrl}'>confirm email</a> ");
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
