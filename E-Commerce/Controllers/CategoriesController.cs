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
            Console.WriteLine("hiiiiiiiii");
            var culture = Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;

            var currentCulture = CultureInfo.CurrentCulture;       // الثقافة العامة (مثل التاريخ، الأرقام)
            var currentUICulture = CultureInfo.CurrentUICulture;   // الثقافة الخاصة بالنصوص والـ resources

            Console.WriteLine($"CurrentCulture: {currentCulture.Name}");
            Console.WriteLine($"CurrentUICulture: {currentUICulture.Name}");
            var resourcePath = Path.Combine(AppContext.BaseDirectory, "Resources", "SharedResources.ar.resx");
            Console.WriteLine(value: $"AR resource file exists: {System.IO.File.Exists(resourcePath)}");

            var categories = _dbContext.Categories
                .OrderByDescending(c=>c.CreatedAt)
                .Select(c=>new CategoryResponseDTO
                {
                    Id=c.Id,
                    Name=(culture=="ar")?c.NameAr:c.NameEn
                })
                .ToList();
            Console.WriteLine(_localizer["Success"].Value);
            return Ok(new {message= _localizer["Success"].Value,categories} );     

        }
        [HttpGet("{id}")]
        public IActionResult GetCategory(long id)
        {
            var category = _dbContext.Categories.Find(id);
            if (category is null) return NotFound(new { message = _localizer["not found"].Value });
            return Ok(category.Adapt<CategoryResponseDTO>());
        }
        [HttpPost]
        public async Task<IActionResult>  Create(CategoryRequestDTO categoryDTO)
        {
            var culture = Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;

            var category =new Category();
            if (culture == "ar")
                category.NameAr = categoryDTO.Name;
            else category.NameEn = categoryDTO.Name;
                await _dbContext.Categories.AddAsync(category);
            _dbContext.SaveChanges();
            return StatusCode(StatusCodes.Status201Created);
        }
        [HttpPatch("{id}")]
        public IActionResult UpdateCategoryName(long id,CategoryRequestDTO categoryRequestDTO)
        {
            var category = _dbContext.Categories.Find(id);
            if (category is null) return NotFound(new { message = _localizer["not found"].Value });
            var culture = Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
            if(culture == "ar")
            {
                category.NameAr = ((categoryRequestDTO.Name == null) ? category.NameAr : categoryRequestDTO.Name);

            }
            else 
                category.NameEn = ((categoryRequestDTO.Name == null) ? category.NameEn : categoryRequestDTO.Name);
            _dbContext.SaveChanges();
            return Ok(new { message = _localizer["Updated"].Value });
        }
        [HttpPatch("{id}/toggle-status")]
        public IActionResult ToggleStatus(long id)
        {
            var category = _dbContext.Categories.Find(id);
            if (category is null) return NotFound(new { message = _localizer["not found"].Value });
            category.Status = (category.Status == Status.Active) ? Status.In_active : Status.Active;
            _dbContext.SaveChanges();
            return Ok(new { message = _localizer["Updated"].Value });
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var category = _dbContext.Categories.Find(id);
            if (category is null) return NotFound(new { message = _localizer["not found"].Value });
             _dbContext.Categories.Remove(category);
            _dbContext.SaveChanges();
            return Ok(new { message = _localizer["Deleted"].Value });
        }
    }
}
