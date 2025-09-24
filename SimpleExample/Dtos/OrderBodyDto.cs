using SimpleExample.Models;
using System.ComponentModel.DataAnnotations;

namespace SimpleExample.Dtos
{
    public class OrderBodyDto
    {

        [Required(ErrorMessage = "Order date is required")]
        public DateTime OrderDate { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Total amount must be 0 or greater")]
        public decimal TotalAmount { get; set; }

        [Required(ErrorMessage = "Status is required")]
        [EnumDataType(typeof(OrderStatus), ErrorMessage = "Invalid order status")]
        public OrderStatus Status { get; set; }
    }
}
