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
using E_Commerce.Model.Category;
using Microsoft.EntityFrameworkCore;
using E_Commerce.BLL.Repository;
namespace E_Commerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly CategoryRepository _categoryRepository;
        private readonly IStringLocalizer<SharedResources> _localizer;

        public CategoriesController(
          CategoryRepository categoryRepository,
           IStringLocalizer<SharedResources> localizer)
        {
            _categoryRepository = categoryRepository;
            _localizer = localizer;


        }
        [HttpGet]
        public async Task<ActionResult<List<CategoryResponseDTO>>> GetAll([FromQuery]string lang="en")
        {
            var categoryDTOs = _categoryRepository.GetAll(lang);
            return Ok(new {message= _localizer["Success"].Value,categoryDTOs} );     

        }
        [HttpGet("{id}")]
        public IActionResult GetCategory([FromRoute] long id)
        {
            CategoryResponseDTO categoryResponseDTO= _categoryRepository.GetCategory(id);
            if (categoryResponseDTO is null) return NotFound(new { message = _localizer["not found"].Value });
            return Ok(categoryResponseDTO);
        }
        [HttpPost]
        public async Task<IActionResult>  Create([FromBody]CategoryRequestDTO categoryDTO)
        {
             _categoryRepository.Create(categoryDTO);
            return StatusCode(StatusCodes.Status201Created);
        }
        [HttpPatch("{id}")]
        public async Task<IActionResult> Update([FromRoute]long id, [FromBody] CategoryRequestDTO categoryRequestDTO)
        {
            bool isExist = await _categoryRepository.Update(id, categoryRequestDTO);
            if (isExist is false) return NotFound(new { message = _localizer["not found"].Value });
            return Ok(new { message = _localizer["Updated"].Value });
        }
        [HttpPatch("{id}/toggle-status")]
        public async Task<IActionResult> ToggleStatus([FromRoute] long id)
        {
            bool isExist = await _categoryRepository.ToggleStatus(id);
            if (isExist is false) return NotFound(new { message = _localizer["not found"].Value });
            return Ok(new { message = _localizer["Updated"].Value });
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] long id)
        {
            bool isExist = await _categoryRepository.Delete(id);
            if (isExist is false) return NotFound(new { message = _localizer["not found"].Value });
            return Ok(new { message = _localizer["Deleted"].Value });
        }
    }
}
