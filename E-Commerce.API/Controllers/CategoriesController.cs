 using E_Commerce.Data;
using E_Commerce.DTO.Request;
using E_Commerce.DTO.Response;
using E_Commerce.Model;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System.Globalization;
using System.Threading.Tasks;
using System.IO;
using Microsoft.EntityFrameworkCore;
using E_Commerce.BLL.Repository;
namespace E_Commerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly CategoryService _categoryService;

        public CategoriesController(
          CategoryService categoryService
          )
        {
            _categoryService = categoryService;


        }
        [HttpGet]
        public async Task<ActionResult<List<CategoryResponseDTO>>> GetAll([FromQuery]string lang="en")
        {
            var categoryDTOs = _categoryService.GetAll(lang);
            return Ok(new {categoryDTOs} );     

        }
        [HttpGet("{id}")]
        public IActionResult GetCategory([FromRoute] long id)
        {
            CategoryResponseDTO categoryResponseDTO= _categoryService.GetCategory(id);
            if (categoryResponseDTO is null) return NotFound();
            return Ok(categoryResponseDTO);
        }
        [HttpPost]
        public async Task<IActionResult>  Create([FromBody]CategoryRequestDTO categoryDTO)
        {
            await _categoryService.Create(categoryDTO);
            return StatusCode(StatusCodes.Status201Created);
        }
        [HttpPatch("{id}")]
        public async Task<IActionResult> Update([FromRoute]long id, [FromBody] CategoryRequestDTO categoryRequestDTO)
        {
            bool isExist = await _categoryService.Update(id, categoryRequestDTO);
            if (isExist is false) return NotFound(new { message = "Not found" });
            return Ok(new{message="Updated" });
        }
        [HttpPatch("{id}/toggle-status")]
        public async Task<IActionResult> ToggleStatus([FromRoute] long id)
        {
            bool isExist = await _categoryService.ToggleStatus(id);
            if (isExist is false) return NotFound(new { message = "Not found" });
            return Ok(new { message = "Updated" });
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] long id)
        {
            bool isExist = await _categoryService.Delete(id);
            if (isExist is false) return NotFound(new { message = "Not found" });
            return Ok(new { message = "Deleted" });
        }
    }
}
