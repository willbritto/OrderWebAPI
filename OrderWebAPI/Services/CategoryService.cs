using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Any;
using OrderWebAPI.Data;
using OrderWebAPI.DTOs.EntitieDTOs;
using OrderWebAPI.Models;
using OrderWebAPI.Repositories.Interfaces;
using OrderWebAPI.Services.Response;

namespace OrderWebAPI.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _repo;
    private readonly IMapper _mapper;

    public CategoryService(ICategoryRepository category, IMapper mapper)
    {
        _repo = category;
        _mapper = mapper;
    }

    public async Task<IEnumerable<CategoryDTO>> GetAllAsync()
    {
        var categories = await _repo.GetAllAsync();
        if (categories == null || !categories.Any())
            throw new KeyNotFoundException("No categories found.");
        var dtoList = _mapper.Map<IEnumerable<CategoryDTO>>(categories);
        return dtoList;

    }

    public async Task<CategoryDTO> GetById(int id)
    {
        var category = await _repo.GetById(id);
        if (category == null)
            throw new KeyNotFoundException($"ID [{id}] Category not exists");
        var result = _mapper.Map<CategoryDTO>(category);
        return result;
    }

    public async Task<CategoryDTO> CreateAsync(CategoryDTO model)
    {

        if (model == null)
            throw new ArgumentNullException(nameof(model));

        if (string.IsNullOrWhiteSpace(model.Service_Type))
            throw new ArgumentException("Name is required");
        

        var entity = _mapper.Map<CategoryModel>(model);
        var category = await _repo.CreateAsync(entity);
        var dto = _mapper.Map<CategoryDTO>(category);
        return dto;

    }

    public async Task<CategoryDTO> DeleteAsync(int id)
    {
        var category = await _repo.DeleteAsync(id);
        if (category == null)
            throw new KeyNotFoundException($"ID [{id}] Category not exists");
        var resultDelete = _mapper.Map<CategoryDTO>(category);
        return resultDelete;
    }


}