using SimpleExample.Data;
using SimpleExample.Dtos;


namespace SimpleExample.Repositories
{
    public class OrderRepository
    {

        private readonly MyDbContext _context;
        private readonly IConfiguration _configuration;

        public OrderRepository(MyDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public List<MonthlyRevenueDto> GetMonthlyRevenue()
        {
            var query = _context.Orders
            .Where(o => o.OrderDate.Year == DateTime.Now.Year);

            if (!query.Any())
                return new List<MonthlyRevenueDto>(); 

            return query
                .GroupBy(o => o.OrderDate.Month)
                .Select(g => new MonthlyRevenueDto
                {
                    Month = g.Key,
                    TotalRevenue = g.Sum(o => o.TotalAmount)
                })
                .ToList();
        }

        public List<TopCustomerDto> GetTopCustomers()
        {
            int topCount = _configuration.GetValue<int>("Settings:TopCustomersCount");
            var query = _context.Orders
                .GroupBy(o => o.CustomerId)
                .Select(g => new
                {
                    CustomerId = g.Key,
                    TotalSpent = g.Sum(o => o.TotalAmount)
                })
                .OrderByDescending(x => x.TotalSpent)
                .Take(topCount)
                .Join(_context.Customers,
                      o => o.CustomerId,
                      c => c.Id,
                      (o, c) => new TopCustomerDto
                      {
                          FullName = c.FullName,
                          TotalSpent = o.TotalSpent
                      });

            return query.ToList();
        }
    }
}
