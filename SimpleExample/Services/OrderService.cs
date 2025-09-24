using SimpleExample.Models;
using SimpleExample.Dtos;
using AutoMapper;
using SimpleExample.Repositories;

namespace SimpleExample.Services
{
    public class OrderService(GenericRepository<Order> orderRepository, IMapper mapper)
    {
        private readonly GenericRepository<Order> _orderRepository = orderRepository;
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
    }
}
