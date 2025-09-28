using AutoMapper;
using SimpleExample.Dtos;
using SimpleExample.Models;
using SimpleExample.Repositories;

namespace SimpleExample.Services
{
    public class CustomerService(GenericRepository<Customer> customerRepository, IMapper mapper, CustomerRepository customerRepository1)
    {
        private readonly GenericRepository<Customer> _customerRepository = customerRepository;
        private readonly CustomerRepository _customerRepository1 = customerRepository1;
        private readonly IMapper _mapper = mapper;


        public List<CustomerDto>? GetAllCustomers()
        {
            var customers = _customerRepository1.GetAll();
         

            if (customers == null)
                return null;

            return _mapper.Map<List<CustomerDto>>(customers);
        }

        public void AddCustomer(CustomerBodyDto customerdto)
        {
            var customer = _mapper.Map<Customer>(customerdto);
     
            _customerRepository.Add(customer);
        }


        public void UpdateCustomer(int id, CustomerBodyDto customerdto)
        {
            var exist = _customerRepository.GetById(id) ?? throw new InvalidOperationException("Entity not found");

            var customer = _mapper.Map<Customer>(customerdto);
            customer.Id = exist.Id;
            _customerRepository.Update(exist, customer);
        }

        public void DeleteCustomer(int id)
        {
            var exist = _customerRepository.GetById(id) ?? throw new InvalidOperationException("Entity not found");
            _customerRepository.Delete(exist);
        }

        public CustomerDto GetById(int id)
        {
            var exist = _customerRepository1.GetById(id) ?? throw new InvalidOperationException("Entity not found");
            return _mapper.Map<CustomerDto>(exist);
        }

        public List<CustomerWithoutOrdersDto> GetCustomersWithoutOrders()
        {
            var customers =  _customerRepository1.GetCustomersWithoutOrders();

            if (customers == null || !customers.Any())
                throw new InvalidOperationException("All customers have at least one order.");

            return customers;
        }



    }

}





