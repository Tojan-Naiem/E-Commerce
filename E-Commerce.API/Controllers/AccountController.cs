using E_Commerce.BLL.Service.Interfaces;
using E_Commerce.DAL.DTO.Request;
using E_Commerce.DAL.DTO.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAuthService _authenticationService;
        public AccountController(
            IAuthService authenticationService
            )
        {
            _authenticationService = authenticationService;
        }
        [HttpPost("register")]
        public async Task<ActionResult<UserResponse>> Register([FromBody] RegisterRequest request)
        {
            var result =await _authenticationService.RegisterAsync(request);
            return Ok(result);
        }
        [HttpPost("login")]
        public async Task<ActionResult<UserResponse>> Login([FromBody] LoginRequest request)
        {
            var result = await _authenticationService.LoginAsync(request);
            return Ok(result);
        }
        [HttpGet("ConfirmedEmail")]
        public async Task<ActionResult<string>> ConfirmEmail([FromQuery]string token, [FromQuery]string userId)
        {
            var result = await _authenticationService.ConfirmEmail(token, userId);
            return Ok(result);
        }
        [HttpPost("ForgotPassword")]
        public async Task<ActionResult<string>> ForgotPassword([FromBody] ForgotPasswordRequest request)
        {
            var result = await _authenticationService.ForgotPassword(request);
            return Ok(result);

        }
        [HttpPatch("ResetPassword")]
        public async Task<ActionResult<string>> ResetPassword([FromBody] ResetPasswordRequestDTO request)
        {
            var result = await _authenticationService.ResetPassword (request);
            return Ok(result);

        }


    }
}
