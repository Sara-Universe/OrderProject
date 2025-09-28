using SimpleExample.Enums;
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
        [EnumDataType(typeof(PaymentMethod), ErrorMessage = "Invalid order status")]
        public PaymentMethod Payment { get; set; }
        [Required(ErrorMessage = "CustomerId is required")]
        public int CustomerId { get; set; }

        public decimal CostAmount { get; set; }
    }
}
