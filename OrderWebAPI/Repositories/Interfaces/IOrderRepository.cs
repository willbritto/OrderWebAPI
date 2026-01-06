using OrderWebAPI.Models;

namespace OrderWebAPI.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        Task<IEnumerable<OrderModel>> GetAllAsync();
        Task<OrderModel> GetById(int id);
        Task<OrderModel> CreateAsync(OrderModel model);
        Task<OrderModel> UpdateAsync(OrderModel orderModel);
        Task<OrderModel> DeleteAsync(int id);
    }
}
