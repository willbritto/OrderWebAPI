using OrderWebAPI.Models;

namespace OrderWebAPI.Services;

public interface IOrderService
{
    Task<IEnumerable<OrderModel>> GetAllOrder();
    Task<OrderModel> GetOrderById(int id);
    Task<OrderModel> CreateOrder(OrderModel model);
    Task<OrderModel> UpdateOrder(OrderModel orderModel);
    Task<OrderModel> DeleteOrder(int id);
   
}
