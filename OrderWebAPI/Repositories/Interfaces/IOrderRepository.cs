using OrderWebAPI.Models;

namespace OrderWebAPI.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        Task<IEnumerable<OrderModel>> GetAllAsync();
        
        //Filter in ID
        Task<OrderModel> GetById(int id);        
        //Filter in Name
        Task<IEnumerable<OrderModel>> GetByName(string name);
        Task<OrderModel> CreateAsync(OrderModel model);
        Task<OrderModel> UpdateAsync(OrderModel orderModel);
        Task<OrderModel> DeleteAsync(int id);
    }
}
