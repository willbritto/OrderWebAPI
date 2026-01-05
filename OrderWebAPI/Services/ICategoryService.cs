using OrderWebAPI.DTOs.EntitieDTOs;
using OrderWebAPI.Models;
using OrderWebAPI.Services.Response;

namespace OrderWebAPI.Services;

public interface ICategoryService
{
    Task<ResponseAPI<IEnumerable<CategoryModel>>> GetCategoryAsyncAll();
    Task<ResponseAPI<CategoryModel>> GetCategoryById(int id);

    Task<ResponseAPI<CategoryModel>> CreateCategory(CategoryModel model);
    //Task<CategoryModel> UpdateCategory(int id, CategoryModel model);
    Task<ResponseAPI<CategoryModel>> DeleteCategory(int id);

}
