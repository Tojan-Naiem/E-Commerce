using E_Commerce.BLL.Service.Interfaces;
using E_Commerce.DAL.DTO.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace E_Commerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Customer")]
    public class CartsController : ControllerBase
    {
        private readonly ICartService _cartService;
        public CartsController(ICartService cartService)
        {
            _cartService = cartService;
        }
        [HttpPost("")]
        public async Task<IActionResult> Add([FromBody]CartRequest cartRequest)
        {
            var UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result=await _cartService.addToCart(cartRequest, UserId);
            return result?Ok("Done"):BadRequest();
        }
    }
}
