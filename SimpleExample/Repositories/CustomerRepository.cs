using Microsoft.EntityFrameworkCore;
using SimpleExample.Data;
using SimpleExample.Dtos;
using SimpleExample.Models;

namespace SimpleExample.Repositories
{
    public class CustomerRepository
    {

        private readonly MyDbContext _context;
     
        public CustomerRepository(MyDbContext context)
        {
            _context = context;
            
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
    }
}
