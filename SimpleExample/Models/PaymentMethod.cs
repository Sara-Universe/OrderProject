namespace SimpleExample.Models
{
    public class PaymentMethod
    {
        public int Id { get; set; }           // PK
        public string Name { get; set; }      // Human-readable name
        public ICollection<Order> Orders { get; set; }  // Navigation
    }
}
