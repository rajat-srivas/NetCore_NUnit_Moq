using System.Collections.Generic;
using System.Linq;

namespace Moq_POC.Services
{
	public class CartService : ICartService
	{
		IPaymentService _paymentService;
		public CartService(IPaymentService paymentService)
		{
			_paymentService = paymentService;
		}
		public string ValidateCart(Order order)
		{
			if (order.CartItems.Count < 0)
				return "Invalid Cart";
			if (order.CartItems.Any(x => x.Quantity < 0 || x.Quantity > 10))
				return "Invalid Product Quantity";

			return _paymentService.ChargeAndShip(order);

		}
	}
}
