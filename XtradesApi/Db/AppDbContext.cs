using Microsoft.EntityFrameworkCore;
using XtradesApi.Entities;

namespace XtradesApi.Db
{
    public class AppDbContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>()
                .HasData(new
                {
                    Id = 1,
                    Name = "Bill Gates",
                    Phone = "+123123123",
                    Email = "bill@myemail.com"
                },
                new
                {
                    Id = 2,
                    Name = "Philip Morris",
                    Phone = "+456456456",
                    Email = "morris@myemail.com"
                });
        }
    }
}
