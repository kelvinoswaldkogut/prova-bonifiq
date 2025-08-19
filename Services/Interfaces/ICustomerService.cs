
using ProvaPub.Models;

namespace ProvaPub.Services.Interfaces
{
    public interface ICustomerService
    {
        Task<PagLists<Customer>> ListCustomers(int page);
        Task<bool> CanPurchase(int customerId, decimal purchaseValue);
    }
}
