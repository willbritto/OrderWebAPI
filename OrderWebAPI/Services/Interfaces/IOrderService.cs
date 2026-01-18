using OrderWebAPI.DTOs.EntitieDTOs;
using OrderWebAPI.Models;

namespace OrderWebAPI.Services.Interfaces;

public interface IOrderService
{
    Task<IEnumerable<OrderDTO>> GetAllAsync();
    
    //Filter in ID
    Task<OrderDTO> GetById(int id);

    //Filter in Name
    Task<OrderDTO> GetByName(string name);

    Task<OrderDTO> CreateAsync(OrderDTO orderDTO);
    Task<OrderDTO> UpdateAsync(int id , OrderDTO orderDTO);
    Task<OrderDTO> DeleteAsync(int id);

}
