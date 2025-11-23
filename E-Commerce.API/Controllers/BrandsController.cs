using E_Commerce.BLL.Repository;
using E_Commerce.BLL.Service.Classes;
using E_Commerce.BLL.Service.Interfaces;
using E_Commerce.DAL.DTO.Request;
using E_Commerce.DAL.DTO.Response;
using E_Commerce.DTO.Request;
using E_Commerce.DTO.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class BrandsController : Controller
    {
        private readonly IBrandService _brandService;

        public BrandsController(
          IBrandService brandService
          )
        {
            _brandService = brandService;


        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<List<BrandResponseDTO>>> GetAll([FromQuery] string lang = "en")
        {
            var brandResponseDTOs = _brandService.GetAll(lang);
            return Ok(new { brandResponseDTOs });

        }
        [HttpGet("{id}")]
        [AllowAnonymous]

        public IActionResult Get([FromRoute] long id)
        {
            BrandResponseDTO brandResponseDTO = _brandService.GetById(id);
            if (brandResponseDTO is null) return NotFound();
            return Ok(brandResponseDTO);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] BrandRequestDTO brandRequestDTO)
        {
            await _brandService.CreateFile(brandRequestDTO);
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
            bool isExist = await _brandService.DeleteFile(id);
            if (isExist is false) return NotFound(new { message = "Not found" });
            return Ok(new { message = "Deleted" });
        }
    }
}
