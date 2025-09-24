using Microsoft.EntityFrameworkCore;
using SimpleExample.Models;

namespace SimpleExample.Data
{
    public class MyDbContext(DbContextOptions<MyDbContext> options) : DbContext(options)
    {
        public DbSet<Order> Orders { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

             modelBuilder.Entity<Order>()
            .Property(o => o.Status)
            .HasConversion<string>();

            // Seed some orders
            modelBuilder.Entity<Order>().HasData(
                new Order
                {
                    Id = 1,
                    OrderDate = new DateTime(2025, 9, 16),
                    TotalAmount = 100.50m,
                    Status = OrderStatus.Pending
                },
                new Order
                {
                    Id = 2,
                    OrderDate = new DateTime(2025, 9, 15),
                    TotalAmount = 250.00m,
                    Status = OrderStatus.Paid
                },
                new Order
                {
                    Id = 3,
                    OrderDate = new DateTime(2025, 9, 14),
                    TotalAmount = 75.75m,
                    Status = OrderStatus.Shipped
                }
            );
        }
    }
}
