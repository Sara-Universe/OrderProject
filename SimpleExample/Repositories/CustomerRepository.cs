using Microsoft.EntityFrameworkCore;
using SimpleExample.Data;
using SimpleExample.Dtos;
using SimpleExample.Models;

namespace SimpleExample.Repositories
{
    public class CustomerRepository
    {

        private readonly MyDbContext _context;
        private readonly IConfiguration _configuration;

        public CustomerRepository(MyDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public List<Customer> GetAll()
        {
            return _context.Customers.AsNoTracking().Include(c => c.Orders).AsSplitQuery().ToList();
        }
        public Customer? GetById(int id)
        {
            return _context.Customers.Include(c => c.Orders).AsSplitQuery().FirstOrDefault(c => c.Id == id);
        }

        public List<CustomerWithoutOrdersDto> GetCustomersWithoutOrders()
        {
            return  _context.Customers
                .Where(c => !c.Orders.Any())
                .Select(c => new CustomerWithoutOrdersDto
                {
                    Id = c.Id,
                    FullName = c.FullName
                })
                .ToList();
        }

        //Q4
        public List<CustomerAverageDto> GetCustomersAverageOrder()
        {
            int minOrders = _configuration.GetValue<int>("Settings:MinOrders");
            return _context.Customers
                .Where(c => c.Orders.Count >= minOrders)
                .Select(c => new CustomerAverageDto
                {
                    FullName = c.FullName,
                    OrderCount = c.Orders.Count,
                    AverageOrderValue = c.Orders.Average(o => o.TotalAmount)
                })
                .ToList();
        }

        //Q5
        public List<CustomerLifetimeStatsDto> GetCustomerLifetimeStats()
        {
            return _context.Customers
                .Select(c => new CustomerLifetimeStatsDto
                {
                    CustomerId = c.Id,
                    FullName = c.FullName,
                    TotalOrders = c.Orders.Count,
                    TotalSpent = c.Orders.Sum(o => (decimal?)o.TotalAmount) ?? 0,
                    LastOrderDate = c.Orders.Max(o => (DateTime?)o.OrderDate)    
                })
                .ToList();
        }
        //Q10
        public List<CustomerAggregateDto> GetCustomerAggregates()
        {
            return _context.Customers
                .Select(c => new CustomerAggregateDto
                {
                    CustomerId = c.Id,
                    FullName = c.FullName,
                    OrderCount = c.Orders.Count(),
                    TotalSpent = c.Orders.Sum(o => (decimal?)o.TotalAmount) ?? 0
                })
                .ToList();
        }
    }
}
