using Bogus;
using Microsoft.EntityFrameworkCore;
using ProvaPub.Models;
using System.Collections.Generic;
using System.Reflection.Emit;
using ProvaPub.Extensions;

namespace ProvaPub.Repository
{

    public class TestDbContext : DbContext
    {
       
        public TestDbContext(DbContextOptions<TestDbContext> options    ) : base(options)
        {
           
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Customer>().HasData(modelBuilder.getCustomerSeed());
            modelBuilder.Entity<Product>().HasData(modelBuilder.getProductSeed());

            modelBuilder.Entity<RandomNumber>().HasIndex(s => s.Number).IsClustered();
        }
        

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<RandomNumber> Numbers { get; set; }

    }
}
