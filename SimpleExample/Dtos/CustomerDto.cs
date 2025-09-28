using SimpleExample.Models;

namespace SimpleExample.Dtos
{
    public class CustomerDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public ICollection<OrderDto> Orders { get; set; } = new List<OrderDto>();

    }
}
