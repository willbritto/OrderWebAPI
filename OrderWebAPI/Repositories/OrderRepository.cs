using Microsoft.EntityFrameworkCore;
using OrderWebAPI.Data;
using OrderWebAPI.Models;
using OrderWebAPI.Repositories.Interfaces;

namespace OrderWebAPI.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _context;

        public OrderRepository(ApplicationDbContext context) 
        {
            _context = context;
        }

        public async Task<IEnumerable<OrderModel>> GetAllAsync()
        {
            return await _context.orderModels.AsNoTracking().ToListAsync();
        }

        public async Task<OrderModel> GetById(int id)
        {
            var order = await _context.orderModels.FirstOrDefaultAsync(o => o.OrderId == id);
            return order;
        }

        public async Task<IEnumerable<OrderModel>> GetByName(string name)
        {
            var orderName = await _context.orderModels
                .Where(n => n.NameFull
                .Contains(name))
                .ToListAsync();

            return orderName;
        }

        public async Task<OrderModel> CreateAsync(OrderModel model)
        {
            _context.Add(model);
            await _context.SaveChangesAsync();
            return model;
        }


        public async Task<OrderModel> UpdateAsync(OrderModel orderModel)
        {           
            _context.Update(orderModel);
            await _context.SaveChangesAsync();
            return orderModel;
        }

        public async Task<OrderModel> DeleteAsync(int id)
        {
            var order = await _context.orderModels.FirstOrDefaultAsync(o => o.OrderId == id);
            _context.Remove(order);
            await _context.SaveChangesAsync();
            return order;
        }
              

    }
}
