using Moq_POC.Services;
using System.Threading.Tasks;

namespace Moq_POC.HttpHelper
{
	public interface IHttpFacade
	{
		Task<ProductPrice> GetProductPricefromHMByCode(string pCode);
	}
}
