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

    public class CheckOutController : ControllerBase
    {
        private readonly ICheckOutService _checkOutService;
        public CheckOutController(ICheckOutService checkOutService)
        {
            _checkOutService = checkOutService;
        }
        [HttpPost("payment")]
        public async Task<IActionResult> Payment([FromBody] CheckOutRequest request)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var response = await _checkOutService.ProcessPaymentAsync(request, userId, Request);
            return Ok(response);
        }
        [HttpGet("success")]
        [AllowAnonymous]
        public ActionResult Success()
        {
            return Ok("Success");
        }
        [HttpGet("cancel")]
        [AllowAnonymous]
        public ActionResult Cancel()
        {
            return Ok("Cancel");
        }
    }
}
