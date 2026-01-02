using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using OrderWebAPI.Models;
using OrderWebAPI.Services;

namespace OrderWebAPI.Controllers
{
    
    [EnableCors]
    [EnableRateLimiting("fixedRL")]
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {

        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        /// <summary>
        /// Retrieves all categories.
        /// </summary>
        /// <returns>Return all list category </returns>
        
        [HttpGet]
        public async Task<IActionResult> GetCategoryAll()
        {
            return Ok(await _categoryService.GetCategoryAsyncAll());

        }
        /// <summary>
        /// Retrieves the category with the specified identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the category to retrieve.</param>
        /// <returns>An <see cref="IActionResult"/> containing the category data if found; otherwise, a result indicating that
        /// the category was not found.</returns>
        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            var category = await _categoryService.GetCategoryById(id);
            return Ok(category);
        }
        /// <summary>
        /// Creates a new category using the specified category model.
        /// </summary>
        /// <param name="categoryModel">The model containing the details of the category to create. Cannot be null.</param>
        /// <returns>An IActionResult that represents the result of the create operation. Returns a 200 OK response with the
        /// created category data if successful.</returns>
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateCategory(CategoryModel categoryModel)
        {
            return Ok(await _categoryService.CreateCategory(categoryModel));
        }
        /// <summary>
        /// Deletes the category with the specified identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the category to delete.</param>
        /// <returns>An <see cref="IActionResult"/> that contains the deleted category and a success message if the operation is
        /// successful.</returns>
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = await _categoryService.DeleteCategory(id);            

            return Ok(new { category ,  message = $"D[{id}] successfully deleted .." });

        }

    }
}
