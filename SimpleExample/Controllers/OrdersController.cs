using Microsoft.AspNetCore.Mvc;
using SimpleExample.Dtos;
using SimpleExample.Services;

namespace SimpleExample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController(OrderService orderService) : ControllerBase
    {
        private readonly OrderService _orderService = orderService;

        [HttpGet]
        public IActionResult GetOrders()
        {
            var orders = _orderService.GetAllOrders();
            if (orders == null)
            {
                return NotFound("There is no orders");
            }
            return Ok(orders);
        }

        [HttpPost]
        public IActionResult AddOrder([FromBody] OrderBodyDto orderdto)
        {
            _orderService.AddOrder(orderdto);
            return Ok("Order has been added successfully!");
        }

        [HttpPut("{id}")]
        public IActionResult UpdateOrder(int id, [FromBody] OrderBodyDto orderdto)
        {
            _orderService.UpdateOrder(id, orderdto);

            return Ok("Updated Successfully!");
        }
   
        [HttpDelete ("{id}")]
        public IActionResult DeleteOrder(int id)
        {
            _orderService?.DeleteOrder(id);
            return Ok("Deleted Successfully!");
        }

        [HttpGet ("{id}")]
        public IActionResult GetById(int id)
        {
            return Ok(_orderService.GetById(id));
        }

    }
}
