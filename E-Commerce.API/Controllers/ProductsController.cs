using E_Commerce.BLL.Service.Classes;
using E_Commerce.BLL.Service.Interfaces;
using E_Commerce.DAL.DTO.Request;
using E_Commerce.DAL.DTO.Response;
using E_Commerce.DTO.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]

    public class ProductsController : ControllerBase
    {
   
            private readonly IProductService _productService;

            public ProductsController(
              IProductService productService
              )
            {
            _productService = productService;


        }
            [HttpGet]
            [AllowAnonymous]
            public async Task<ActionResult<List<ProductResponse>>> GetAll([FromQuery] string lang = "en")
            {
                var productsDto = _productService.GetAll(lang);
                return Ok(new { productsDto });

            }
            [HttpGet("{id}")]
            [AllowAnonymous]

            public IActionResult Get([FromRoute] long id)
            {
            ProductResponse productResponse = _productService.GetById(id);
                if (productResponse is null) return NotFound();
                return Ok(productResponse);
            }
            [HttpPost]
            public async Task<IActionResult> Create([FromForm] ProductRequest productRequest)
        {
            await _productService.CreateFile(productRequest);
            return StatusCode(StatusCodes.Status201Created);
            }
            [HttpPatch("{id}")]
            public async Task<IActionResult> Update([FromRoute] long id, [FromBody] ProductRequest productRequest)
            {
                bool isExist = await _productService.Update(id, productRequest);
                if (isExist is false) return NotFound(new { message = "Not found" });
                return Ok(new { message = "Updated" });
            }
            [HttpPatch("{id}/toggle-status")]
            public async Task<IActionResult> ToggleStatus([FromRoute] long id)
            {
                bool isExist = await _productService.ToggleStatus(id);
                if (isExist is false) return NotFound(new { message = "Not found" });
                return Ok(new { message = "Updated" });
            }
            [HttpDelete("{id}")]
            public async Task<IActionResult> Delete([FromRoute] long id)
            {
                bool isExist = await _productService.Delete(id);
                if (isExist is false) return NotFound(new { message = "Not found" });
                return Ok(new { message = "Deleted" });
            }
        }
}
