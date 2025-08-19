using ProvaPub.Models;

namespace ProvaPub.Persistence.Interfaces
{
    public interface IReader
    {
        Task<bool> CheckSameNumbr(int number);
        Task<List<Product>> GetProductListAsync();
        Task<List<Customer>> GetListCustomersAsync(int page);
        Task<Customer> FindCustomerById(int customerId);
        Task<int> GetOrdersInthisMonthByCustomersIdandDate(int customerId, DateTime baseDate);
        Task<bool> VerifyFirstBought(int customerId);
    }
}
