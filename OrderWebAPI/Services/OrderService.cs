using AutoMapper;
using OrderWebAPI.DTOs.EntitieDTOs;
using OrderWebAPI.Models;
using OrderWebAPI.Repositories.Interfaces;

namespace OrderWebAPI.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _repo;
    private readonly IMapper _mapper;

    public OrderService(IOrderRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }


    public async Task<IEnumerable<OrderDTO>> GetAllAsync()
    {
        var orders = await _repo.GetAllAsync();
        if (orders == null || !orders.Any())
            throw new KeyNotFoundException("Invalid order data");
        var orderList = _mapper.Map<IEnumerable<OrderDTO>>(orders);
        return orderList;
    }

    public async Task<OrderDTO> GetById(int id)
    {
        var order = await _repo.GetById(id);
        if (order == null)
            throw new KeyNotFoundException($"Order [{id}] not found");
        var orderId = _mapper.Map<OrderDTO>(order);
        return orderId;
    }



    public async Task<OrderDTO> CreateAsync(OrderDTO orderDTO)
    {
        if (orderDTO == null)
            throw new ArgumentNullException(nameof(orderDTO));

        ValidationStrings(orderDTO);

        var entity = _mapper.Map<OrderModel>(orderDTO);
        var order = await _repo.CreateAsync(entity);
        var result = _mapper.Map<OrderDTO>(order);
        return result;


    }


    public async Task<OrderDTO> UpdateAsync(int id, OrderDTO orderDTO)
    {
        if (orderDTO == null)
            throw new ArgumentNullException(nameof(orderDTO));

        ValidationStrings(orderDTO);

        var entity = await _repo.GetById(id);
        if (entity == null)
            throw new KeyNotFoundException($"Order [{id}] not found");

        _mapper.Map(orderDTO, entity);

        var updated = await _repo.UpdateAsync(entity);
        return _mapper.Map<OrderDTO>(updated);
    }

    public async Task<OrderDTO> DeleteAsync(int id)
    {
        var order = await _repo.DeleteAsync(id);
        if (order == null)
            throw new ArgumentException($"Order [{id}] not found");
        var deleteId = _mapper.Map<OrderDTO>(order);
        return deleteId;
    }

    private void ValidationStrings(OrderDTO orderDTO)
    {
        if (string.IsNullOrEmpty(orderDTO.NameFull)) throw new ArgumentException("Name Full is required");
        if (string.IsNullOrEmpty(orderDTO.Description)) throw new ArgumentException("Description is required");
        if (orderDTO.Price <= 0) throw new ArgumentException("Price cannot be 0");


    }
}

