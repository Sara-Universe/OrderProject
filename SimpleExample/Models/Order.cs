using SimpleExample.Enums;

namespace SimpleExample.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public int PaymentMethodId { get; set; }        // FK
        public PaymentMethod PaymentMethod { get; set; } // Navigation
        public Customer Customer { get; set; }
        public int CustomerId { get; set; }
        public decimal CostAmount { get; set; }

    }
}
