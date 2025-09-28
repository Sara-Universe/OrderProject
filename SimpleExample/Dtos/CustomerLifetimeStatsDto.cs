namespace SimpleExample.Dtos
{
    public class CustomerLifetimeStatsDto
    {
        public int CustomerId { get; set; }
        public string FullName { get; set; }
        public int TotalOrders { get; set; }
        public decimal TotalSpent { get; set; }
        public DateTime? LastOrderDate { get; set; }

    }
}
