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
namespace E_Commerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IStringLocalizer<SharedResources> _localizer;

        private readonly ApplicationDbContext _dbContext;
        public CategoriesController(
            ApplicationDbContext dbContext, 
            IStringLocalizer<SharedResources> localizer)
        {
            _dbContext = dbContext;
            _localizer = localizer;
        }
        [HttpGet]
        public async Task<ActionResult<List<CategoryResponseDTO>>> GetAll()
        {
            var categories = _dbContext.Categories
                .Include(c=>c.categoryTranslations)
                .OrderByDescending(c=>c.CreatedAt)
                .ToList();
            var categoryDTOs = categories.Select(c => new CategoryResponseDTO
            {
                Id = c.Id,
                CategoryTranslationResponses = c.categoryTranslations.Select(t => new CategoryTranslationResponse
                {
                    Name = t.Name,
                    Language = t.Language
                }).ToList()
            }).ToList();
            Console.WriteLine(_localizer["Success"].Value);
            return Ok(new {message= _localizer["Success"].Value,categoryDTOs} );     

        }
        [HttpGet("{id}")]
        public IActionResult GetCategory([FromRoute] long id)
        {
            var category = _dbContext.Categories.Find(id);
            if (category is null) return NotFound(new { message = _localizer["not found"].Value });
            return Ok(category.Adapt<CategoryResponseDTO>());
        }
        [HttpPost]
        public async Task<IActionResult>  Create([FromBody]CategoryRequestDTO categoryDTO)
        {

            Category category = new Category()
            {
                Status = categoryDTO.Status,
                categoryTranslations = categoryDTO.categoryTranslations.Select(
                   t => new CategoryTranslation()
                   {
                       Name = t.Name,
                       Language = t.Language
                   }
                    ).ToList()
            };

            Console.WriteLine(System.Text.Json.JsonSerializer.Serialize(categoryDTO));
            Console.WriteLine("1111111111111111111111");
            await _dbContext.Categories.AddAsync(category);

            _dbContext.SaveChanges();
            return StatusCode(StatusCodes.Status201Created);
        }
        [HttpPatch("{id}")]
        public IActionResult UpdateCategoryName([FromRoute]long id, [FromBody] CategoryRequestDTO categoryRequestDTO)
        {
            var category = _dbContext.Categories.Find(id);
            if (category is null) return NotFound(new { message = _localizer["not found"].Value });
            var culture = Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
            if(culture == "ar")
            {
             //   category.NameAr = ((categoryRequestDTO.Name == null) ? category.NameAr : categoryRequestDTO.Name);

            }
            else 
               // category.NameEn = ((categoryRequestDTO.Name == null) ? category.NameEn : categoryRequestDTO.Name);
            _dbContext.SaveChanges();
            return Ok(new { message = _localizer["Updated"].Value });
        }
        [HttpPatch("{id}/toggle-status")]
        public IActionResult ToggleStatus([FromRoute] long id)
        {
            var category = _dbContext.Categories.Find(id);
            if (category is null) return NotFound(new { message = _localizer["not found"].Value });
            category.Status = (category.Status == Status.Active) ? Status.In_active : Status.Active;
            _dbContext.SaveChanges();
            return Ok(new { message = _localizer["Updated"].Value });
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] long id)
        {
            var category = _dbContext.Categories.Find(id);
            if (category is null) return NotFound(new { message = _localizer["not found"].Value });
             _dbContext.Categories.Remove(category);
            _dbContext.SaveChanges();
            return Ok(new { message = _localizer["Deleted"].Value });
        }
    }
}
