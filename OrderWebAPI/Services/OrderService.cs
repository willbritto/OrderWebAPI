using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using OrderWebAPI.Data;
using OrderWebAPI.Models;

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
            throw new ArgumentException ($"Erro ao tentar buscar ordens : {ex.Message}");
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
            throw new ArgumentException($"Erro ao tentar buscar ordem do [{id}]  : {ex.Message}");
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

            throw new ArgumentException($"Erro ao tentar criar novas ordem : {ex.Message}");
        }


    }


    public async Task<OrderModel> UpdateOrder(OrderModel orderModel)
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

            throw new ArgumentException($"Erro ao tentar atualizar ordem : {ex.Message}");
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

            throw new ArgumentException($"Erro ao tentar deletar ordem : {ex.Message}");
        }

        
        
    }

}
