namespace SimpleExample.Dtos
{
    public class CustomerAggregateDto
    {
        public int CustomerId { get; set; }
        public string FullName { get; set; }
        public int OrderCount { get; set; }
        public decimal TotalSpent { get; set; }
    }
}
