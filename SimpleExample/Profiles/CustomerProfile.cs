using AutoMapper;
using SimpleExample.Dtos;
using SimpleExample.Models;
namespace SimpleExample.Profiles
{
    public class CustomerProfile: Profile
    {
        public CustomerProfile()
        {
            CreateMap<Customer, CustomerDto>();
            CreateMap<CustomerDto, Customer>();
            CreateMap<CustomerBodyDto, Customer>();
            CreateMap<Customer, CustomerBodyDto>();


        }
    }
}
