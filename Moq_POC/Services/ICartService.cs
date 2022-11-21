using System.Collections.Generic;

namespace Moq_POC.Services
{
	public interface ICartService
	{
		string ValidateCart(Order order);
	}
}
