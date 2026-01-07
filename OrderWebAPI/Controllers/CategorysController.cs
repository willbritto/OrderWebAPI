using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using OrderWebAPI.DTOs.EntitieDTOs;
using OrderWebAPI.Services.Interfaces;
using OrderWebAPI.Services.Response;

namespace OrderWebAPI.Controllers
{

    [EnableCors]
    [EnableRateLimiting("fixedRL")]
    [Route("api/[controller]")]
    [ApiController]
    public class CategorysController : ControllerBase
    {

        private readonly ICategoryService _categoryService;
        private readonly ILogger<CategorysController> _logger;

        public CategorysController(ICategoryService categoryService, ILogger<CategorysController> logger)
        {
            _categoryService = categoryService;
            _logger = logger;
        }

        /// <summary>
        /// Retrieves all categories.
        /// </summary>
        /// <returns>Return all list category </returns>

        [HttpGet]
        public async Task<IActionResult> GetCategoryAll()
        {
            _logger.LogInformation("\n =============================");
            _logger.LogInformation(" == Filter all category /GetAllCategories == ");
            _logger.LogInformation(" ============================= \n");

            try
            {
                return Ok(await _categoryService.GetAllAsync());
            }
            catch (Exception ex)
            {

                return BadRequest(ResponseAPI<string>.Fail(ex.Message));

            }


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
            _logger.LogInformation("\n =============================");
            _logger.LogInformation($"== Filter category by ID only /GetCategoryById/{id} == ");
            _logger.LogInformation(" ============================= \n");

            try
            {
                var category = await _categoryService.GetById(id);
                return Ok(category);
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseAPI<string>.Fail(ex.Message));
            }

        }
        /// <summary>
        /// Creates a new category using the specified category model.
        /// </summary>
        /// <param name="categoryModel">The model containing the details of the category to create. Cannot be null.</param>
        /// <returns>An IActionResult that represents the result of the create operation. Returns a 200 OK response with the
        /// created category data if successful.</returns>

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateCategory(CategoryDTO categoryDTO)
        {
            _logger.LogInformation("\n =============================");
            _logger.LogInformation(" == Create new category /CreateCategory == ");
            _logger.LogInformation(" ============================= \n");

            try
            {

                await _categoryService.CreateAsync(categoryDTO);
                return Ok(categoryDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseAPI<string>.Fail(ex.Message));

            }


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
            _logger.LogInformation("\n =============================");
            _logger.LogInformation($" == Delete category by ID /DeleteCategory/{id} == ");
            _logger.LogInformation(" ============================= \n");

            try
            {
                var category = await _categoryService.DeleteAsync(id);
                return Ok(category);
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseAPI<string>.Fail(ex.Message));
            }


        }

    }
}
