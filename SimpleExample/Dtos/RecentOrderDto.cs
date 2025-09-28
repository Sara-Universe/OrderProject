namespace SimpleExample.Dtos
{
    public class RecentOrderDto
    {
        public string FullName { get; set; }
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
