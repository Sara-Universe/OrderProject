using SimpleExample.Enums;

namespace SimpleExample.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public PaymentMethod Payment { get; set; }

        public Customer Customer { get; set; }
        public int CustomerId { get; set; }
    }
}
