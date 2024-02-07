using Microsoft.EntityFrameworkCore;

namespace SampleProject.Models
{
    public class CustomerDbContext : DbContext
    {
        public CustomerDbContext(DbContextOptions<CustomerDbContext> options) : base(options)
        {
        }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Pie> Pie { get; set; }
        public DbSet<Attendace> Attendaces { get; set;}
        public DbSet<Order> Orders { get; set; }

        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<OrderList> OrderLists { get; set; }
    }
}
