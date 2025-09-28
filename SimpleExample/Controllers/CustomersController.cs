using Microsoft.AspNetCore.Mvc;
using SimpleExample.Dtos;
using SimpleExample.Services;

namespace SimpleExample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController(CustomerService customerService) : ControllerBase
    {
        private readonly CustomerService _customerService = customerService;

        [HttpGet]
        public IActionResult GetCustomers()
        {
            var customers = _customerService.GetAllCustomers();
            if (customers == null)
            {
                return NotFound("There is no customers");
            }
            return Ok(customers);
        }

        [HttpPost]
        public IActionResult AddCustomer([FromBody] CustomerBodyDto customerdto)
        {
            _customerService.AddCustomer(customerdto);
            return Ok("Customer has been added successfully!");
        }

        [HttpPut("{id}")]
        public IActionResult UpdateCustomer(int id, [FromBody] CustomerBodyDto customerdto)
        {
            _customerService.UpdateCustomer(id, customerdto);

            return Ok("Updated Successfully!");
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteCustomer(int id)
        {
            _customerService?.DeleteCustomer(id);
            return Ok("Deleted Successfully!");
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            return Ok(_customerService.GetById(id));
        }

        [HttpGet("WithoutOrders")]
        public IActionResult GetCustomersWithoutOrders()
        {
            try
            {
                var result = _customerService.GetCustomersWithoutOrders();
                return Ok(result);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while fetching customers without orders.", detail = ex.Message });
            }
        }

    }
}
