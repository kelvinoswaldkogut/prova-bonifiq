
using ProvaPub.Models;

namespace ProvaPub.Persistence.Interfaces
{
    public interface IWriter 
    {
        Task InsertOrder(Order order);
        Task SaveNumber(int randomNumbr);
    }
}
