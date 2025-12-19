using E_Commerce.BLL.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpGet("")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users=await _userService.GetAllAsync();
            return Ok(users);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById([FromRoute] string id)
        {
            var user = await _userService.GetByIdAsync(id);
            return Ok(user);
        }
        [HttpPatch("unblock/{id}")]
        public async Task<IActionResult> UnBlockUserAsync([FromRoute] string id)
        {
            var user = await _userService.UnBlockUserAsync(id);
            if (user is false)
                return NotFound();
            return Ok(user);
        }
        [HttpPatch("block/{id}/{days}")]
        public async Task<IActionResult> BlockUserAsync([FromRoute] string id, [FromRoute] int days)
        {
            if (days <= 0)
                return BadRequest("Days must be more than 1 day");
            var user = await _userService.BlockUserAsync(id,days);
            if (user is false)
                return NotFound();
            return Ok(user);
        }
        [HttpGet("IsBlocked/{id}")]
        public async Task<IActionResult> IsBlocked([FromRoute] string id)
        {
            var user = await _userService.IsBlocked(id);
            if (user is false)
                return NotFound();
            return Ok(user);
        }
    }
}
