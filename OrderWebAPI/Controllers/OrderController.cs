using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderWebAPI.Models;
using OrderWebAPI.Services;

namespace OrderWebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {

        private readonly IOrderService _serviceOrder;
        private readonly IPrintService _printService;

        public OrderController(IOrderService orderService, IPrintService printService)
        {

            _serviceOrder = orderService;
            _printService = printService;
        }


        [HttpGet("{id}/print")]
        public async Task<IActionResult> PrinterOrder(int id) 
        {
            var order = await _serviceOrder.GetOrderById(id);
            if (order == null)
                return NotFound();

            var pdfBytes = _printService.GenerateOrderPdf(order);
            return File(pdfBytes, "application/pdf", $"Order_{id}.pdf");
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
