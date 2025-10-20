using E_Commerce.Data;
using E_Commerce.DTO.Request;
using E_Commerce.DTO.Response;
using E_Commerce.Model;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace E_Commerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        public CategoriesController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpGet]
        public async Task<ActionResult<List<CategoryResponseDTO>>> GetAll()
        {
            var categories = _dbContext.Categories.OrderByDescending(c=>c.CreatedAt).ToList().Adapt<List<CategoryResponseDTO>>();
            return Ok(new {message="Success",categories} );     

        }
        [HttpGet("{id}")]
        public IActionResult GetCategory(long id)
        {
            var category = _dbContext.Categories.Find(id);
            if (category is null) return NotFound(new { message = "Category not found" });
            return Ok(category.Adapt<CategoryResponseDTO>());
        }
        [HttpPost]
        public async Task<IActionResult>  Create(CategoryRequestDTO categoryDTO)
        {
            var category = categoryDTO.Adapt<Category>();
            await _dbContext.Categories.AddAsync(category);
            _dbContext.SaveChanges();
            return StatusCode(StatusCodes.Status201Created);
        }
        [HttpPatch("{id}")]
        public IActionResult UpdateCategoryName(long id,CategoryRequestDTO categoryRequestDTO)
        {
            var category = _dbContext.Categories.Find(id);
            if (category is null) return NotFound(new { message = "Category id not found" });
            category.Name = ((categoryRequestDTO.Name==null)?category.Name:categoryRequestDTO.Name);
            _dbContext.SaveChanges();
            return Ok(new { message = "Updated Successfully" });
        }
        [HttpPatch("{id}/toggle-status")]
        public IActionResult ToggleStatus(long id)
        {
            var category = _dbContext.Categories.Find(id);
            if (category is null) return NotFound(new { message = "Category id not found" });
            category.Status = (category.Status == Status.Active) ? Status.In_active : Status.Active;
            _dbContext.SaveChanges();
            return Ok(new { message = "Updated Successfully" });
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var category = _dbContext.Categories.Find(id);
            if (category is null) return NotFound(new { message = "Category id not found" });
             _dbContext.Categories.Remove(category);
            _dbContext.SaveChanges();
            return Ok(new { message = "Deleted Successfully" });
        }
    }
}
