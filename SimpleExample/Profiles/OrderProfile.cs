using AutoMapper;
using SimpleExample.Dtos;
using SimpleExample.Models;


namespace SimpleExample.Profiles
{
    public class OrderProfiles : Profile
    {
        public OrderProfiles() {

            CreateMap<Order, OrderDto>();
            CreateMap<OrderDto, Order>();
            CreateMap<Order, OrderBodyDto>();
            CreateMap<OrderBodyDto, Order >();
        }
    }
}
