using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using OrderWebAPI.DTOs.EntitieDTOs;
using OrderWebAPI.Services.Interfaces;

namespace OrderWebAPI.Controllers
{

    [EnableCors]
    [EnableRateLimiting("fixedRL")]
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {

        private readonly IOrderService _serviceOrder;
        private readonly IPrintService _printService;
        private readonly ILogger<OrdersController> _logger;
        private readonly IMapper _mapper;

        public OrdersController(IOrderService orderService, IPrintService printService, ILogger<OrdersController> logger, IMapper mapper)
        {

            _serviceOrder = orderService;
            _printService = printService;

            _logger = logger;
            _mapper = mapper;
        }

        /// <summary>
        /// Generates and returns a PDF file containing the details of the specified order for printing.
        /// </summary>
        /// <param name="id">The unique identifier of the order to print. Must correspond to an existing order.</param>
        /// <returns>An <see cref="FileContentResult"/> containing the PDF file of the order if found; otherwise, a <see
        /// cref="NotFoundResult"/> if the order does not exist.</returns>

        [Authorize]
        [HttpGet("{id}/print")]
        public async Task<IActionResult> PrinterOrder(int id)
        {
            _logger.LogInformation("\n =============================");
            _logger.LogInformation($" == Printer order ID /PrinterOrder/{id} == ");
            _logger.LogInformation(" ============================= \n");

            var order = await _serviceOrder.GetById(id);
            if (order == null)
                return NotFound($"Order [{id}] not found");

            var pdfBytes = _printService.GenerateOrderPdf(order);
            return File(pdfBytes, "application/pdf", $"Order_{id}.pdf");
        }

        /// <summary>
        /// Retrieves all orders.
        /// </summary>
        /// <returns>An <see cref="IActionResult"/> containing a collection of all orders. The response has a status code of 200
        /// (OK) with the list of orders in the response body.</returns>


        [HttpGet]
        public async Task<IActionResult> GetOrderAll()
        {
            _logger.LogInformation("\n =============================");
            _logger.LogInformation(" == Filter all orders /GetAllOrders == ");
            _logger.LogInformation(" ============================= \n");

            return Ok(await _serviceOrder.GetAllAsync());
        }

        /// <summary>
        /// Retrieves the details of an order with the specified identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the order to retrieve.</param>
        /// <returns>An <see cref="IActionResult"/> containing the order details if found; otherwise, a result indicating that
        /// the order was not found.</returns>

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            _logger.LogInformation("\n =============================");
            _logger.LogInformation($" == Filter order by ID only /GetOrderById/{id} == ");
            _logger.LogInformation(" ============================= \n");

            var order = await _serviceOrder.GetById(id);
            return Ok(order);
        }

        /// <summary>
        /// Returns service order by its name.
        /// </summary>
        /// <param name="name">The unique identifier of the order to retrieve.</param>
        /// <returns>An <see cref="IActionResult"/> containing the order details if found; otherwise, a result indicating that
        /// the order was not found.</returns>

        

        /// <summary>
        /// Creates a new order based on the specified order details.
        /// </summary>
        /// <remarks>Requires authentication. This endpoint is accessible only to authorized
        /// users.</remarks>
        /// <param name="orderModel">The order information to create. Must contain all required order fields; cannot be null.</param>
        /// <returns>An IActionResult containing the result of the order creation operation. Returns a 200 OK response with the
        /// created order details if successful.</returns>

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateOrder(OrderDTO orderDTO)
        {
            _logger.LogInformation("\n =============================");
            _logger.LogInformation(" == Create new order /CreateOrder == ");
            _logger.LogInformation(" ============================= \n");


            var result = await _serviceOrder.CreateAsync(orderDTO);
            return Ok(result);

        }

        /// <summary>
        /// Updates an existing order with the specified details.
        /// </summary>
        /// <remarks>Requires authentication. The order identifier is taken from the route parameter and
        /// must match the identifier in <paramref name="orderModel"/>. If the order does not exist, a not found result
        /// may be returned.</remarks>
        /// <param name="orderModel">The order data to update. Must include a valid order identifier and the updated order information. Cannot be
        /// null.</param>
        /// <returns>An <see cref="IActionResult"/> indicating the result of the update operation. Returns <see
        /// cref="OkObjectResult"/> with the updated order if successful.</returns>

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, OrderDTO orderDTO)
        {
            _logger.LogInformation("\n =============================");
            _logger.LogInformation($" == Update order by id /UpdateOrder/{id} == ");
            _logger.LogInformation(" ============================= \n");


            var result = await _serviceOrder.UpdateAsync(id, orderDTO);
            return Ok(result);

        }

        /// <summary>
        /// Deletes the order with the specified identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the order to delete.</param>
        /// <returns>An IActionResult containing the deleted order and a confirmation message if the deletion is successful.</returns>

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            _logger.LogInformation("\n =============================");
            _logger.LogInformation($" == Delete order by id /DeleteOrder/{id} == ");
            _logger.LogInformation(" ============================= \n");

            var order = await _serviceOrder.DeleteAsync(id);
            return Ok(order);
        }


    }
}
