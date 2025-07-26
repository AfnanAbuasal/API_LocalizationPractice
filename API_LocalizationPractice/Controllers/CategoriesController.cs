using Microsoft.AspNetCore.Mvc;
using API_LocalizationPractice.DTO.Requests;
using Mapster;
using API_LocalizationPractice.DTO.Responses;
using Microsoft.Extensions.Localization;
using API_LocalizationPractice.Models.Category;
using API_LocalizationPractice.Data;
using API_LocalizationPractice.Models;
using Microsoft.EntityFrameworkCore;

namespace API_LocalizationPractice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        ApplicationDbContext context = new ApplicationDbContext();

        private readonly IStringLocalizer<SharedResource> _localizer;

        public CategoriesController(IStringLocalizer<SharedResource> localizer)
        {
            _localizer = localizer;
        }

        [HttpGet("")]
        public IActionResult Index([FromQuery] string lang = "en")
        {
            var categories = context.Categories.Include(c => c.CategoryTranslations).Where(c => c.Status == Status.Active).ToList().Adapt<List<CategoryResponseDTO>>();
            var categoriesOnLang = categories.Select(category => new
            {
                ID = category.ID,
                Name = category.CategoryTranslations.FirstOrDefault(t => t.Language == lang)?.Name
            });
            return Ok(new { message = _localizer["success"].Value, categoriesOnLang });
        }

        [HttpGet("All")]
        public IActionResult IndexAll([FromQuery] string lang = "en")
        {
            var categories = context.Categories.Include(c => c.CategoryTranslations).ToList().Adapt<List<CategoryResponseDTO>>();
            var categoriesOnLang = categories.Select(category => new
            {
                ID = category.ID,
                Name = category.CategoryTranslations.FirstOrDefault(t => t.Language == lang)?.Name
            });
            return Ok(new { message = _localizer["success"].Value, categoriesOnLang });
        }

        [HttpGet("{ID}")]
        public IActionResult Details([FromRoute] int ID)
        {
            var category = context.Categories.Find(ID);
            if(category is null)
            {
                return NotFound(new {message = _localizer["not-found"].Value });
            }
            var categoryResponse = category.Adapt<CategoryResponseDTO>();
            return Ok(new { message = _localizer["success"].Value, categoryResponse });
        }

        [HttpPost("")]
        public IActionResult Create([FromBody] CategoryRequestDTO request)
        {
            var categoryInDB = request.Adapt<Category>();
            context.Add(categoryInDB);
            context.SaveChanges();
            return Ok(new { message = _localizer["add-success"].Value });
        }

        [HttpPatch("{ID}")]
        public IActionResult Update([FromRoute] int ID, [FromBody] CategoryRequestDTO request)
        {
            var category = context.Categories.Find(ID);
            if (category is null)
            {
                return NotFound(new { message = _localizer["not-found"].Value });
            }
            //category.Name = request.Name;
            context.SaveChanges();
            return Ok(new { message = _localizer["update-success"].Value });
        }

        [HttpPatch("ToggleStatus/{ID}")]
        public IActionResult ToggleStatus([FromRoute] int ID)
        {
            var category = context.Categories.Find(ID);
            if (category is null)
            {
                return NotFound(new { message = _localizer["not-found"].Value });
            }
            category.Status = category.Status == Status.Active ? Status.Inactive : Status.Active;
            context.SaveChanges();
            return Ok(new { message = _localizer["status-update-success"].Value });
        }

        [HttpDelete("{ID}")]
        public IActionResult Delete([FromRoute] int ID)
        {
            var category = context.Categories.Find(ID);
            if (category is null)
            {
                return NotFound(new { message = _localizer["not-found"].Value });
            }
            context.Remove(category);
            context.SaveChanges();
            return Ok(new { message = _localizer["delete-success"].Value });
        }

        [HttpDelete("DeleteAll")]
        public IActionResult DeleteAll()
        {
            var categories = context.Categories.ToList();
            if (!categories.Any())
            {
                return NotFound(new { message = _localizer["not-found-plural"].Value });
            }
            context.RemoveRange(categories);
            context.SaveChanges();
            return Ok(new { message = _localizer["delete-all-success"].Value });
        }
    }
}
