using OrderWebAPI.DTOs.EntitieDTOs;

namespace OrderWebAPI.Services.Interfaces;

public interface IOrderService
{
    Task<IEnumerable<OrderDTO>> GetAllAsync();
    Task<OrderDTO> GetById(int id);
    Task<OrderDTO> CreateAsync(OrderDTO orderDTO);
    Task<OrderDTO> UpdateAsync(int id , OrderDTO orderDTO);
    Task<OrderDTO> DeleteAsync(int id);

}
