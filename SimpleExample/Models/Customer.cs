namespace SimpleExample.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public ICollection<Order> Orders { get; set; }

    }
}
