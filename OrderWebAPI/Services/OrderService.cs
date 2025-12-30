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
        throw new NotImplementedException();
    }

    public async Task<OrderModel> GetOrderById(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<OrderModel> CreateOrder(OrderModel model)
    {
        throw new NotImplementedException();
    }


    public async Task<OrderModel> UpdateOrder(int id, OrderModel orderModel)
    {
        throw new NotImplementedException();
    }

    public async Task<OrderModel> DeleteOrder(int id)
    {
        throw new NotImplementedException();
    }

}
