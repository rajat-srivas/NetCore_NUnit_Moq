using System.Threading.Tasks;

namespace Moq_POC.Services
{
	public interface IProductService
	{
		Task<string> GetProductPriceByCode(string pCode);
	}
}
