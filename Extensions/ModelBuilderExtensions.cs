using Bogus;
using ProvaPub.Models;
using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;

namespace ProvaPub.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static Customer[] getCustomerSeed(this ModelBuilder model)
        {
            List<Customer> customers = new();
            for (int i = 0; i < 20; i++)
            {
                customers.Add(new Customer()
                {
                    Id = i + 1,
                    Name = new Faker().Person.FullName,
                });
            }
            return customers.ToArray();
        }
        public static Product[] getProductSeed(this ModelBuilder model)
        {
            List<Product> products = new();
            for (int i = 0; i < 20; i++)
            {
                products.Add(new Product()
                {
                    Id = i + 1,
                    Name = new Faker().Commerce.ProductName()
                });
            }
            return products.ToArray();
        }
    }

}
