using SimpleExample.Enums;
using System.ComponentModel.DataAnnotations;

namespace SimpleExample.Dtos
{
    public class OrderDto
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public PaymentMethod Payment { get; set; }
        public int CustomerId { get; set; }
        public decimal CostAmount { get; set; }
    }
}
