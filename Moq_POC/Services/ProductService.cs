using Moq_POC.HttpHelper;
using System.Threading.Tasks;

namespace Moq_POC.Services
{
	public class ProductService : IProductService
	{
		IHttpFacade _httpFacade;
		public ProductService(IHttpFacade httpFacade)
		{
			_httpFacade = httpFacade;
		}
		public async Task<string> GetProductPriceByCode(string pCode)
		{
			if (string.IsNullOrEmpty(pCode))
			{
				return "Product code cannot be empty";
			}
			else
			{
				var product = await _httpFacade.GetProductPricefromHMByCode(pCode);
				if (product.Product == null)
					return "Product Not Found";
				else
				{
					return $"Current price of product {pCode} is {product.Product.RedPrice.price.ToString()} {product.Product.RedPrice.currency}";
				}
			}

		}
	}
}
