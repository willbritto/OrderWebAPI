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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            var category = await _categoryService.GetCategoryById(id);
            return Ok(category);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(CategoryModel categoryModel)
        {
            return Ok(await _categoryService.CreateCategory(categoryModel));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = await _categoryService.DeleteCategory(id);

            //if (category  || category == )            
            //    return NotFound($"ID[{id}] não existente/ou cadastrado no banco de dados .. ");
            
            
            return Ok(new { category ,  message = $"ID[{id}] deletado com sucesso .." });

        }

    }
}
