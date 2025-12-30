using OrderWebAPI.Models;

namespace OrderWebAPI.Services;

public interface ICategoryService
{
    Task<IEnumerable<CategoryModel>> GetCategoryAsyncAll();
    Task<CategoryModel> GetCategoryById(int id);

    Task<CategoryModel> CreateCategory(CategoryModel model);
    //Task<CategoryModel> UpdateCategory(int id, CategoryModel model);
    Task<CategoryModel> DeleteCategory(int id);

}
