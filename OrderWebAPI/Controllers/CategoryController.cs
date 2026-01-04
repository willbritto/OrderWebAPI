using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using OrderWebAPI.DTOs.EntitieDTOs;
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
        private readonly IMapper _mapper;
        private readonly ILogger<CategoryController> _logger;

        public CategoryController(ICategoryService categoryService, IMapper mapper, ILogger<CategoryController> logger)
        {
            _categoryService = categoryService;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Retrieves all categories.
        /// </summary>
        /// <returns>Return all list category </returns>

        [HttpGet("GetAllCategorys")]
        public async Task<IActionResult> GetCategoryAll()
        {
            _logger.LogInformation("\n =============================");
            _logger.LogInformation("\n == Filter all category /GetAllCategorys == \n");
            _logger.LogInformation(" ============================= \n");

            return Ok(await _categoryService.GetCategoryAsyncAll());

        }
        /// <summary>
        /// Retrieves the category with the specified identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the category to retrieve.</param>
        /// <returns>An <see cref="IActionResult"/> containing the category data if found; otherwise, a result indicating that
        /// the category was not found.</returns>

        [Authorize]
        [HttpGet("GetCategoryById/{id}")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            _logger.LogInformation("\n =============================");
            _logger.LogInformation($"\n == Filter category by ID only /GetCategoryById/{id} == \n");
            _logger.LogInformation(" ============================= \n");

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
        [HttpPost("CreateCategory")]
        public async Task<IActionResult> CreateCategory(CategoryDTO categoryDTO)
        {
            _logger.LogInformation("\n =============================");
            _logger.LogInformation("\n == Create new cateogry /CreateCategory == \n");
            _logger.LogInformation(" ============================= \n");

            var categoryEntity = _mapper.Map<CategoryModel>(categoryDTO);
            var result = await _categoryService.CreateCategory(categoryEntity);
            return Ok(new { Data = result, Status = "Success", Message = "Category created successfully" });
        }
        /// <summary>
        /// Deletes the category with the specified identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the category to delete.</param>
        /// <returns>An <see cref="IActionResult"/> that contains the deleted category and a success message if the operation is
        /// successful.</returns>

        [Authorize]
        [HttpDelete("DeleteCategory/{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            _logger.LogInformation("\n =============================");
            _logger.LogInformation($"\n == Delete category by ID /DeleteCategory/{id} == \n");
            _logger.LogInformation(" ============================= \n");

            var category = await _categoryService.DeleteCategory(id);

            return Ok(new { category, message = $"D[{id}] successfully deleted .." });

        }

    }
}
