using OrderWebAPI.DTOs.EntitieDTOs;
using OrderWebAPI.Models;
using OrderWebAPI.Services.Response;

namespace OrderWebAPI.Services.Interfaces;

public interface ICategoryService
{
    Task<IEnumerable<CategoryDTO>> GetAllAsync();
    Task<CategoryDTO> GetById(int id);
    Task<CategoryDTO> CreateAsync(CategoryDTO model);
    Task<CategoryDTO> DeleteAsync(int id);

}
