using ProvaPub.Models;
using ProvaPub.Persistence.Interfaces;
using ProvaPub.Repository;
using ProvaPub.Services.Interfaces;

namespace ProvaPub.Services
{
	public class ProductService : IProductService
	{
	
		private readonly IReader _reader;

		public ProductService(IReader reader)
		{
			_reader = reader;
		}

		public async Task<PagLists<Product>>  ListProducts(int page)
		{
			var prodResponse = new PagLists<Product>();
			var productList = await  _reader.GetProductListAsync();

			
			if (productList != null ) 
			{
                var pagedProducts = productList
                .Skip((page - 1) * Rules.PageSize) 
                .Take(Rules.PageSize)           
                .ToList();

                prodResponse = new PagLists<Product>()
                {
                    HasNext = productList.Count > page * Rules.PageSize,
                    TotalCount = productList.Count,
                    Itens = pagedProducts
                };

            }
			return prodResponse;
           
        }
        private static class Rules
        {
            public static int PageSize = 10;
        }

    }
	
}
