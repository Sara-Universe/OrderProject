using Microsoft.EntityFrameworkCore;
using SimpleExample.Enums;
using SimpleExample.Models;

namespace SimpleExample.Data
{
    public class MyDbContext(DbContextOptions<MyDbContext> options) : DbContext(options)
    {
        public DbSet<Order> Orders { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<PaymentMethod> PaymentMethods { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);




            // Seed some orders
            modelBuilder.Entity<Customer>().HasData(
                new Customer
                {
                    Id = 1,
                    FullName = "Sara Allahaleh"
                },
                new Customer
                {
                    Id = 2,
                    FullName = "Sama Alkarazon"

                }

            );

            modelBuilder.Entity<PaymentMethod>().HasData(
                new PaymentMethod { Id = 1, Name = "Cash" },
                new PaymentMethod { Id = 2, Name = "CreditCard" },
                new PaymentMethod { Id = 3, Name = "DigitalWallets" }
);


            //modelBuilder.Entity<Order>()
            //.Property(o => o.Payment)
            //.HasConversion<string>();

            // Seed some orders
            modelBuilder.Entity<Order>().HasData(
                new Order
                {
                    Id = 1,
                    OrderDate = new DateTime(2025, 9, 16),
                    TotalAmount = 100.50m,
                    PaymentMethodId = 1,
                    CustomerId = 1,
                    CostAmount = 60.00m
                },
                new Order
                {
                    Id = 2,
                    OrderDate = new DateTime(2025, 9, 15),
                    TotalAmount = 250.00m,
                    PaymentMethodId = 2
                    , CustomerId = 2,
                    CostAmount = 160.00m
                },
                new Order
                {
                    Id = 3,
                    OrderDate = new DateTime(2025, 9, 14),
                    TotalAmount = 75.75m,
                    PaymentMethodId = 3
                    , CustomerId = 1,
                    CostAmount = 100.00m
                }


            );

      
        }
    }
}
