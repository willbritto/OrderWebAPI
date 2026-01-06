using Microsoft.EntityFrameworkCore;
using OrderWebAPI.Data;
using OrderWebAPI.Models;
using OrderWebAPI.Repositories.Interfaces;
using OrderWebAPI.Services.Response;

namespace OrderWebAPI.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {

        private readonly ApplicationDbContext _context;

        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<IEnumerable<CategoryModel>> GetAllAsync()
        {
             return  await _context.categoryModels.AsNoTracking().ToListAsync();
         
        }

        public async Task<CategoryModel> GetById(int id)
        {
            var category = await _context.categoryModels.FirstOrDefaultAsync(c => c.CategoryId == id);
            return category;
        }

        public async Task<CategoryModel> CreateAsync(CategoryModel model)
        {
            _context.categoryModels.Add(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<CategoryModel> DeleteAsync(int id)
        {
            var category = await _context.categoryModels.FirstOrDefaultAsync(c => c.CategoryId == id);
            _context.categoryModels.Remove(category);
            await _context.SaveChangesAsync();

            return category;
        }

       
    }
}
