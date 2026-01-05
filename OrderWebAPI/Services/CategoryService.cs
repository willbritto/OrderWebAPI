using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Any;
using OrderWebAPI.Data;
using OrderWebAPI.DTOs.EntitieDTOs;
using OrderWebAPI.Models;
using OrderWebAPI.Services.Response;

namespace OrderWebAPI.Services;

public class CategoryService : ICategoryService
{

    private readonly ApplicationDbContext _context;

    public CategoryService(ApplicationDbContext dbContext)
    {
        _context = dbContext;
    }

    public async Task<ResponseAPI<IEnumerable<CategoryModel>>> GetCategoryAsyncAll()
    {
        var response = new ResponseAPI<IEnumerable<CategoryModel>>();
        try
        {
            var categories = await _context.categoryModels.AsNoTracking().ToListAsync();
            response.Data = categories;

            if (response.Data?.Count() == 0)
            {
                response.Message = "No data found";
            }
            else
            {
                response.Message = "Categories successfully listed";
            }
        }
        catch (Exception ex)
        {

            response.Message = ex.Message;
            response.Success = false;
            return response;

        }
        return response;
    }

    public async Task<ResponseAPI<CategoryModel>> GetCategoryById(int id)
    {
        var response = new ResponseAPI<CategoryModel>();
        try
        {
            CategoryModel category = await _context.categoryModels.FirstOrDefaultAsync(c => c.CategoryId == id);

            if (category == null)
            {
                response.Data = null;
                response.Message = "No category matching ID was found.";
                response.Success = false;

            }
            response.Data = category;
        }
        catch (Exception ex)
        {

            response.Message = ex.Message;
            response.Success = false;
        }
        return response;
    }

    public async Task<ResponseAPI<CategoryModel>> CreateCategory(CategoryModel model)
    {
        var response = new ResponseAPI<CategoryModel>();
        try
        {
            if (model == null)
            {
                response.Data = null;
                response.Message = "No data reported";
                response.Success = false;

                return response;

            }
            else
            {
                response.Message = "Category created successfully.";
            }

                _context.Add(model);
            await _context.SaveChangesAsync();

            response.Data = model;

        }
        catch (Exception ex)
        {
            response.Message = ex.Message;
            response.Success = false;
            return response;


        }
        return response;


    }

    public async Task<ResponseAPI<CategoryModel>> DeleteCategory(int id)
    {
        var response = new ResponseAPI<CategoryModel>();

        try
        {
            CategoryModel category = await _context.categoryModels.FirstOrDefaultAsync(c => c.CategoryId == id);

            if (category == null)
            {
                response.Data = null;
                response.Message = "No category matching ID was found.";
                response.Success = false;

                return response;
            }
            else
            {
                response.Message = "Category deletad successfully.";
            }

            _context.categoryModels.Remove(category);
            await _context.SaveChangesAsync();


            response.Data = category;

        }
        catch (Exception ex)
        {

            response.Message = ex.Message;
            response.Success = false;


        }
        return response;


    }

}
