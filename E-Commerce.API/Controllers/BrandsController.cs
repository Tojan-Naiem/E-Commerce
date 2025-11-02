using E_Commerce.BLL.Repository;
using E_Commerce.BLL.Service.Classes;
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

        public CategoriesController(
          CategoryService categoryService
          )
        {
            _brandService = categoryService;


        }
        [HttpGet]
        public async Task<ActionResult<List<CategoryResponseDTO>>> GetAll([FromQuery] string lang = "en")
        {
            var categoryDTOs = _brandService.GetAll(lang);
            return Ok(new { categoryDTOs });

        }
        [HttpGet("{id}")]
        public IActionResult GetCategory([FromRoute] long id)
        {
            CategoryResponseDTO categoryResponseDTO = _brandService.GetCategory(id);
            if (categoryResponseDTO is null) return NotFound();
            return Ok(categoryResponseDTO);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CategoryRequestDTO categoryDTO)
        {
            await _brandService.Create(categoryDTO);
            return StatusCode(StatusCodes.Status201Created);
        }
        [HttpPatch("{id}")]
        public async Task<IActionResult> Update([FromRoute] long id, [FromBody] CategoryRequestDTO categoryRequestDTO)
        {
            bool isExist = await _brandService.Update(id, categoryRequestDTO);
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
