using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq_POC.Services;

namespace Moq_POC.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CartController : ControllerBase
	{
		 IPaymentService _paymentService;
		ICartService _cartService;
		public CartController(IPaymentService paymentService, ICartService cartService)
		{
			_paymentService = paymentService;
			_cartService = cartService;
		}

		[HttpPost]
		public string CheckOut(Order order)
		{
			var result = _cartService.ValidateCart(order);
			return result;
		}
	}
}
