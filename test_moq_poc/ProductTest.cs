using Moq;
using Moq_POC.HttpHelper;
using Moq_POC.Services;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Cache;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace test_moq_poc
{
	internal class ProductTest
	{
		IProductService productServiceMock;
		Mock<IHttpFacade> httpFacadeMock;
		[SetUp]
		public void Setup()
		{
			httpFacadeMock = new Mock<IHttpFacade>();
			productServiceMock = new ProductService(httpFacadeMock.Object);

		}

		[Test]
		[TestCase("", true, "Product code cannot be empty")] //fail as empty pcode
		[TestCase("1231242343", false, "Product Not Found")] //as product not found
		[TestCase("0501620068",true, "Current price of product 0501620068 is 9.99 USD")] //pass
		public async Task CorrectProdcutCode_Returns_Price(string pCode, bool validProdcut, string expectedResult)
		{
			
			ProductPrice price = new ProductPrice();
			using (StreamReader r = new StreamReader("C:\\Users\\rasrivas\\source\\repos\\Moq_POC\\test_moq_poc\\Mocked_Response\\MockedProductPrice.json"))
			{
				string json = r.ReadToEnd();
				price = JsonSerializer.Deserialize<ProductPrice>(json);
			}

			httpFacadeMock.Setup(x => x.GetProductPricefromHMByCode(It.IsAny<string>())).ReturnsAsync(validProdcut ? price : new ProductPrice());

			var result = await productServiceMock.GetProductPriceByCode(pCode);

			httpFacadeMock.Verify();

			Assert.AreEqual(expectedResult, result);
		}
		
	}
}
