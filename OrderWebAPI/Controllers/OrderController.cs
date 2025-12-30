using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderWebAPI.Models;
using OrderWebAPI.Services;

namespace OrderWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {

        private readonly IOrderService _serviceOrder;

        public OrderController(IOrderService orderService)
        {

            _serviceOrder = orderService;

        }


        [HttpGet]
        public async Task<IActionResult> GetOrderAll() 
        {
            return Ok(await _serviceOrder.GetAllOrder());
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            var order = await _serviceOrder.GetOrderById(id);
            return Ok(order);
        }


        [HttpPost]
        public async Task<IActionResult> CreateOrder(OrderModel orderModel) 
        {
            return Ok(await _serviceOrder.CreateOrder(orderModel));
            
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(OrderModel orderModel)
        {

            return Ok(await _serviceOrder.UpdateOrder(orderModel));

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id) 
        {
            var order = await _serviceOrder.DeleteOrder(id);           

            return Ok(new { order, message = $"ID[{id}] deletado com sucesso ..." });
        }


    }
}
