using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderWebAPI.Models;
using OrderWebAPI.Services;

namespace OrderWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {

        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }


        [HttpGet]
        public async Task<IActionResult> GetCategoryAll() 
        {
            return Ok(await _categoryService.GetCategoryAsyncAll());

        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(CategoryModel categoryModel) 
        {
            return Ok(await _categoryService.CreateCategory(categoryModel));
        }

    }
}
