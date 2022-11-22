using Moq_POC.Services;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Moq_POC.HttpHelper
{
	public class HttpFacade : IHttpFacade
	{
		public async Task<ProductPrice> GetProductPricefromHMByCode(string pCode)
		{
			var client = new HttpClient();
			var request = new HttpRequestMessage
			{
				Method = HttpMethod.Get,
				RequestUri = new Uri("https://apidojo-hm-hennes-mauritz-v1.p.rapidapi.com/products/detail?lang=en&country=us&productcode=" + pCode),
				Headers =
					{
						{ "user-agent", "vscode-restclient" },
						{ "x-rapidapi-key", "c0ed669beamshd956824cd0516bap1bed7ejsnfb1246bdd8c5" },
						{ "x-rapidapi-host", "apidojo-hm-hennes-mauritz-v1.p.rapidapi.com" },
					},
			};
			using (var response = await client.SendAsync(request))
			{
				response.EnsureSuccessStatusCode();
				var body = await response.Content.ReadAsStringAsync();
				if (!string.IsNullOrEmpty(body))
				{
					var price = JsonSerializer.Deserialize<ProductPrice>(body);
					return price;
				}
				else
				{
					return new ProductPrice();
				}
			}
		}
	}
}
