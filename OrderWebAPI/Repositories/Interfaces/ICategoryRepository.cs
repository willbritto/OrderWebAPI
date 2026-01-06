using OrderWebAPI.Models;
using OrderWebAPI.Services.Response;

namespace OrderWebAPI.Repositories.Interfaces
{
    public interface ICategoryRepository
    {

        Task<IEnumerable<CategoryModel>> GetAllAsync();
        Task<CategoryModel> GetById(int id);
        Task<CategoryModel> CreateAsync(CategoryModel model);
        //Task<CategoryModel> UpdateCategory(int id, CategoryModel model);
        Task<CategoryModel> DeleteAsync(int id);

    }
}
