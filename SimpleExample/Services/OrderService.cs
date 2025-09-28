using SimpleExample.Models;
using SimpleExample.Dtos;
using AutoMapper;
using SimpleExample.Repositories;

namespace SimpleExample.Services
{
    public class OrderService(GenericRepository<Order> orderRepository, IMapper mapper , OrderRepository orderRepository1)
    {
        private readonly GenericRepository<Order> _orderRepository = orderRepository;
        private readonly OrderRepository _orderRepository1 = orderRepository1;
        private readonly IMapper _mapper = mapper;

        public List<OrderDto>? GetAllOrders()
        {
            var orders = _orderRepository.GetAll();

            if (orders == null)
                return null;
            
             return _mapper.Map<List<OrderDto>>(orders);
        }
        public void AddOrder (OrderBodyDto orderdto)
        {
            var order = _mapper.Map<Order>(orderdto);
           _orderRepository.Add(order);
        }
        public void UpdateOrder (int id, OrderBodyDto orderdto)
        {
            var exist = _orderRepository.GetById(id) ?? throw new InvalidOperationException("Entity not found");

            var order = _mapper.Map<Order>(orderdto);
            order.Id = exist.Id;
            _orderRepository.Update(exist, order);
        }
        public void DeleteOrder(int id)
        {
            var exist = _orderRepository.GetById(id) ?? throw new InvalidOperationException("Entity not found");
            _orderRepository.Delete(exist);
        }

        public OrderDto GetById (int id)
        {
            var exist = _orderRepository.GetById(id) ?? throw new InvalidOperationException("Entity not found");
            return _mapper.Map<OrderDto>(exist);
        }

        public List<MonthlyRevenueDto> GetMonthlyRevenue()
        {
            var report = _orderRepository1.GetMonthlyRevenue();

        
            if (report == null || !report.Any())
                throw new InvalidOperationException("No revenue data found for the current year.");

        
            foreach (var r in report)
            {
                if (r.TotalRevenue < 0)
                    throw new InvalidOperationException($"Invalid revenue detected for month {r.Month}.");
            }

            return report;
        }

        public List<TopCustomerDto> GetTopCustomers()
        {
            var customers = _orderRepository1.GetTopCustomers();

            if (customers == null || !customers.Any())
                throw new InvalidOperationException("No customers found with spending data.");

            // Validate values
            foreach (var c in customers)
            {
                if (c.TotalSpent < 0)
                    throw new InvalidOperationException($"Invalid total spent for customer {c.FullName}.");
            }

            return customers;
        }

        public List<MonthlyProfitDto> GetMonthlyProfit()
        {
            return _orderRepository1.GetMonthlyProfit();
        }

        public List<OrderDto> GetOrdersAboveCustomerAverage()
        {
           return _orderRepository1.GetOrdersAboveCustomerAverage();
            
        }

        public List<RecentOrderDto> GetMostRecentOrdersPerCustomer()
        {
            return _orderRepository1.GetMostRecentOrdersPerCustomer();
        }

        public List<DailySummaryDto> GetDailySummary()
        {
            return _orderRepository1.GetDailySummary();
        }
    }
}
