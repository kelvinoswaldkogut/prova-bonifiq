using ProvaPub.Models;

namespace ProvaPub.Services.Interfaces
{
    public interface IProductService
    {
       Task<PagLists<Product>> ListProducts(int page);
    }
}
