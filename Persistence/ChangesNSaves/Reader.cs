using Microsoft.EntityFrameworkCore;
using ProvaPub.Models;
using ProvaPub.Persistence.Interfaces;
using ProvaPub.Repository;

namespace ProvaPub.Persistence.ChangesNSaves
{

    public class Reader : IReader
    {
        private readonly TestDbContext _context;
        public Reader(TestDbContext testDbContext)
        {
            _context = testDbContext;
        }
        public async Task<bool> CheckSameNumbr(int number)
        {
            var verify  = await _context.Numbers.AnyAsync(numero => numero.Number == number);
            return verify;
        }
        public async Task<List<Product>> GetProductListAsync()
        {
            var prodList = await _context.Products.ToListAsync();
            return prodList;
        }
        public async Task<List<Customer>> GetListCustomersAsync(int page)
        {
            var customerList = await _context.Customers.ToListAsync();
            return customerList.ToList();
        }
        public async Task<Customer> FindCustomerById(int customerId)
        {
            return await _context.Customers.FindAsync(customerId);
        }
        public async Task<int> GetOrdersInthisMonthByCustomersIdandDate(int customerId, DateTime baseDate)
        {
            return await _context.Orders.CountAsync(c => c.CustomerId == customerId && c.OrderDate >= baseDate);
        }
        public async Task<bool> VerifyFirstBought(int customerId)
        {
           return await _context.Orders.AnyAsync(c => c.CustomerId == customerId);
            
        }
    }
}
