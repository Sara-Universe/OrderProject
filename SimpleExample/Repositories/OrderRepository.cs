using Microsoft.EntityFrameworkCore;
using SimpleExample.Data;
using SimpleExample.Dtos;
using SimpleExample.Models;


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

        //Q6
        public List<MonthlyProfitDto> GetMonthlyProfit()
        {
            return _context.Orders
                .Where(o => o.OrderDate.Year == DateTime.Now.Year)
                .GroupBy(o => o.OrderDate.Month)
                .Select(g => new MonthlyProfitDto
                {
                    Month = g.Key,
                    Revenue = g.Sum(o => o.TotalAmount),
                    Cost = g.Sum(o => o.CostAmount),
                    Profit = g.Sum(o => o.TotalAmount) - g.Sum(o => o.CostAmount)
                })
                .OrderBy(x => x.Month)
                .ToList();
        }

        //Q7
        public List<OrderDto> GetOrdersAboveCustomerAverage()
        {
            //Calculate average per customer
            var customerAverages = _context.Orders
                .GroupBy(o => o.CustomerId)
                .Select(g => new
                {
                    CustomerId = g.Key,
                    AverageAmount = g.Average(x => x.TotalAmount)
                });

            // Join with Orders and filter
            var query = from o in _context.Orders
                        join avg in customerAverages
                            on o.CustomerId equals avg.CustomerId
                        where o.TotalAmount > avg.AverageAmount
                        select new OrderDto
                        {
                            Id = o.Id,
                            OrderDate = o.OrderDate,
                            TotalAmount = o.TotalAmount,
                            PaymentMethodId = o.PaymentMethodId,
                            CustomerId = o.CustomerId,
                            CostAmount = o.CostAmount
                        };

            return query.ToList();
        }
        //Q8
        public List<RecentOrderDto> GetMostRecentOrdersPerCustomer()
        {
            //For each customer, find the latest order date
            var latestOrders = _context.Orders
                .GroupBy(o => o.CustomerId)
                .Select(g => new
                {
                    CustomerId = g.Key,
                    LatestOrderDate = g.Max(o => o.OrderDate)
                });

            //Join with Orders and Customers to get full details
            var query = from o in _context.Orders
                        join lo in latestOrders
                            on new { o.CustomerId, o.OrderDate }
                            equals new { lo.CustomerId, OrderDate = lo.LatestOrderDate }
                        join c in _context.Customers
                            on o.CustomerId equals c.Id
                        select new RecentOrderDto
                        {
                            FullName = c.FullName,
                            OrderId = o.Id,
                            OrderDate = o.OrderDate,
                            TotalAmount = o.TotalAmount
                        };

            return query.ToList();
        }

        //Q9
        public List<DailySummaryDto> GetDailySummary()
        {
            return _context.Orders
                .Where(o => o.OrderDate.Year == DateTime.Now.Year)
                .GroupBy(o => o.OrderDate.Date) 
                .Where(g => g.Count() >= 2) 
                .Select(g => new DailySummaryDto
                {
                    OrderDate = g.Key,
                    OrderCount = g.Count(),
                    DailyRevenue = g.Sum(o => o.TotalAmount)
                })
                .OrderBy(g => g.OrderDate)
                .ToList();
        }



    }
}
