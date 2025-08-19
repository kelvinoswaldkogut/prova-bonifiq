using ProvaPub.Models;

namespace ProvaPub.Services.Interfaces
{
    public interface IOrderService
    {
        Task<string> PayOrder(string paymentMethod, decimal paymentValue, int customerId);
    }
}
