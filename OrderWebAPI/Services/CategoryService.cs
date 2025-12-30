using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Any;
using OrderWebAPI.Data;
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
        var categorys = await _context.categoryModels.ToListAsync();
        return categorys;
    }

    public async Task<CategoryModel> GetCategoryById(int id)
    {
        var category = await _context.categoryModels.FirstOrDefaultAsync(c => c.CategoryId == id);
        return category;
        
    }

    public async Task<CategoryModel> CreateCategory(CategoryModel model)
    {
        if (model is null)
            throw new ArgumentNullException(nameof(model));

        _context.Add(model);
        await _context.SaveChangesAsync();

        return model;
    }

    public async Task<CategoryModel> DeleteCategory(int id)
    {
        var category = await _context.categoryModels.FindAsync(id);

        if (category is null)
            throw new ArgumentNullException(nameof(category));

        _context.Remove(category);
        await _context.SaveChangesAsync();

        return category;
    }

}
