using ProvaPub.Models;
using ProvaPub.Persistence.Interfaces;
using ProvaPub.Repository;

namespace ProvaPub.Persistence.ChangesNSaves
{
    public class Writer : IWriter
    {
        private readonly TestDbContext _context;
        public Writer(TestDbContext dbContext) 
        {
            _context = dbContext;
        }

        public async Task SaveNumber(int number) 
        {
            _context.Numbers.Add(new Models.RandomNumber { Number = number });
            await _context.SaveChangesAsync();
        }
        public async Task InsertOrder(Order order)
        {
            await _context.Orders.AddAsync(order);
        }
    }
}
