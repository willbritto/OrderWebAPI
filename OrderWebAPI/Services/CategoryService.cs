using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Any;
using OrderWebAPI.Data;
using OrderWebAPI.DTOs.EntitieDTOs;
using OrderWebAPI.Models;

namespace OrderWebAPI.Services;

public class CategoryService : ICategoryService
{

    private readonly ApplicationDbContext _context;

    public CategoryService(ApplicationDbContext dbContext)
    {
        _context = dbContext;
    }

    public async Task<IEnumerable<CategoryModel>> GetCategoryAsyncAll()
    {
        try
        {
            var categorys = await _context.categoryModels.ToListAsync();
            return categorys;
        }
        catch (Exception ex)
        {

            throw new ArgumentException($"Error when searching categories: {ex.Message}");
           
        }
    }

    public async Task<CategoryModel> GetCategoryById(int id)
    {
        try
        {
            var category = await _context.categoryModels.FirstOrDefaultAsync(c => c.CategoryId == id);
            return category;
        }
        catch (Exception ex)
        {

            throw new ArgumentException($"Error retrieving category from ID [{id}]: {ex.Message}");
        }
    }

    public async Task<CategoryModel> CreateCategory(CategoryModel model)
    {

        try
        {
            if (model is null)
                throw new ArgumentNullException(nameof(model));

            _context.Add(model);
            await _context.SaveChangesAsync();

            return model;

        }
        catch (Exception ex)
        {

            throw new ArgumentException($"Error while trying to create a new category: {ex.Message}");
        }

        
    }

    public async Task<CategoryModel> DeleteCategory(int id)
    {

        try
        {
            var category = await _context.categoryModels.FirstOrDefaultAsync(c => c.CategoryId == id);

            if (category == null)
                return null;

            _context.Remove(category);
            await _context.SaveChangesAsync();

            return category;
        }
        catch (Exception ex )
        {

            throw new ArgumentException($"Error while trying to delete ID [{id}]: {ex.Message}");
        }

       
    }

}
