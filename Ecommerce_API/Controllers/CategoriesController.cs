using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ecommerce_API.Data;
using Ecommerce_API.Models;
using Ecommerce_API.DTO.Requests;
using Mapster;
using Ecommerce_API.DTO.Responses;
using Microsoft.Extensions.Localization;
namespace Ecommerce_API.Controllers
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
        public IActionResult Index()
        {
            var categories = context.Categories.Where(c => c.Status == Status.Active).ToList().Adapt<List<CategoryResponseDTO>>();
            return Ok(new { message = _localizer["success"].Value, categories });
        }

        [HttpGet("All")]
        public IActionResult IndexAll()
        {
            var categories = context.Categories.ToList().Adapt<List<CategoryResponseDTO>>();
            return Ok(new { message = _localizer["success"].Value, categories });
        }

        [HttpGet("{ID}")]
        public IActionResult Details(int ID)
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
        public IActionResult Create(CategoryRequestDTO request)
        {
            var categoryInDB = request.Adapt<Category>();
            context.Add(categoryInDB);
            context.SaveChanges();
            return Ok(new { message = _localizer["add-success"].Value });
        }

        [HttpPatch("{ID}")]
        public IActionResult Update(int ID, CategoryRequestDTO request)
        {
            var category = context.Categories.Find(ID);
            if (category is null)
            {
                return NotFound(new { message = _localizer["not-found"].Value });
            }
            category.Name = request.Name;
            context.SaveChanges();
            return Ok(new { message = _localizer["update-success"].Value });
        }

        [HttpPatch("ToggleStatus/{ID}")]
        public IActionResult ToggleStatus(int ID)
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
        public IActionResult Delete(int ID)
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
