using E_Commerce.BLL.Repository;
using E_Commerce.BLL.Service.Classes;
using E_Commerce.DAL.DTO.Request;
using E_Commerce.DAL.DTO.Response;
using E_Commerce.DTO.Request;
using E_Commerce.DTO.Response;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandsController : Controller
    {
        private readonly BrandService _brandService;

        public BrandsController(
          BrandService brandService
          )
        {
            _brandService = brandService;


        }
        [HttpGet]
        public async Task<ActionResult<List<CategoryResponseDTO>>> GetAll([FromQuery] string lang = "en")
        {
            var categoryDTOs = _brandService.GetAll(lang);
            return Ok(new { categoryDTOs });

        }
        [HttpGet("{id}")]
        public IActionResult Get([FromRoute] long id)
        {
            BrandResponseDTO brandResponseDTO = _brandService.GetById(id);
            if (brandResponseDTO is null) return NotFound();
            return Ok(brandResponseDTO);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] BrandRequestDTO brandRequestDTO)
        {
            await _brandService.Create(brandRequestDTO);
            return StatusCode(StatusCodes.Status201Created);
        }
        [HttpPatch("{id}")]
        public async Task<IActionResult> Update([FromRoute] long id, [FromBody] BrandRequestDTO brandRequestDTO)
        {
            bool isExist = await _brandService.Update(id, brandRequestDTO);
            if (isExist is false) return NotFound(new { message = "Not found" });
            return Ok(new { message = "Updated" });
        }
        [HttpPatch("{id}/toggle-status")]
        public async Task<IActionResult> ToggleStatus([FromRoute] long id)
        {
            bool isExist = await _brandService.ToggleStatus(id);
            if (isExist is false) return NotFound(new { message = "Not found" });
            return Ok(new { message = "Updated" });
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] long id)
        {
            bool isExist = await _brandService.Delete(id);
            if (isExist is false) return NotFound(new { message = "Not found" });
            return Ok(new { message = "Deleted" });
        }
    }
}
