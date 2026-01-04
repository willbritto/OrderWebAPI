using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using OrderWebAPI.Data;
using OrderWebAPI.DTOs.EntitieDTOs;
using OrderWebAPI.Models;
using Serilog;

namespace OrderWebAPI.Services;

public class OrderService : IOrderService
{
    private readonly ApplicationDbContext _context;

    public OrderService(ApplicationDbContext dbContext)
    {
        _context = dbContext;
    }

    public async Task<IEnumerable<OrderModel>> GetAllOrder()
    {
        try
        {
            var orders = await _context.orderModels.ToListAsync();
            return orders;
        }
        catch (Exception ex)
        {
            throw new ArgumentException ($"Error while trying to retrieve orders: {ex.Message}");
        }
    }

    public async Task<OrderModel> GetOrderById(int id)
    {
        try
        {
            var order = await _context.orderModels.FirstOrDefaultAsync(o => o.OrderId == id);
            return order;
        }
        catch (Exception ex)
        {
            throw new ArgumentException($"Error while trying to retrieve order of [{id}]: {ex.Message}");
        }
    }

    public async Task<OrderModel> CreateOrder(OrderModel model)
    {
        try
        {
            
            if (model == null)
                return null;

            _context.Add(model);
            await _context.SaveChangesAsync();

            return model;
        }
        catch (Exception ex)
        {

            Log.Error(ex, "Error save entity");
            throw;
            
        }


    }


    public async Task<OrderModel> UpdateOrder(int id, OrderModel orderModel)
    {

        try
        {
            var order = await _context.orderModels.FirstOrDefaultAsync(o => o.OrderId == orderModel.OrderId);

            if (order == null)
                return null;

            order.Description = order.Description;
            order.Price = order.Price;
            order.Status = order.Status;
            order.CategoryId = order.CategoryId;

            await _context.SaveChangesAsync();

            return orderModel;
        }
        catch (Exception ex)
        {

            throw new ArgumentException($"Error while trying to update order: {ex.Message}");
        }

        

    }

    public async Task<OrderModel> DeleteOrder(int id)
    {

        try
        {
            var order = await _context.orderModels.FirstOrDefaultAsync(o => o.OrderId == id);

            if (order == null)
                return null;

            _context.Remove(order);
            await _context.SaveChangesAsync();

            return order;

        }
        catch (Exception ex)
        {

            throw new ArgumentException($"Error while trying to delete order: {ex.Message}");
        }

        
        
    }

}
