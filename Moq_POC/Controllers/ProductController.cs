using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq_POC.Services;
using System.Threading.Tasks;

namespace Moq_POC.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProductController : ControllerBase
	{
		IProductService _productService;
		public ProductController(IProductService productService)
		{
			_productService = productService;
		}

		[HttpGet]
		public async Task<IActionResult> GetProductById(string pCode)
		{
			var response = await _productService.GetProductPriceByCode(pCode);
			return Ok(response);
		}
	}
}
