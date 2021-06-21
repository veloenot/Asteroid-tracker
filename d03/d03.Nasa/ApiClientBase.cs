using System;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace d03.Nasa
{
	public abstract class ApiClientBase
	{
		protected string ApiKey;

		protected ApiClientBase(string apiKey)
		{
			ApiKey = apiKey;
		}

		protected async Task<T> HttpGetAsync<T>(string url)
		{
			var client = new HttpClient();
			var response = await client.GetAsync(url);
			var content = await response.Content.ReadAsStringAsync();
			var statusCode = response.StatusCode;

			if (statusCode != HttpStatusCode.OK)
			{
				throw new Exception($"GET\n\"{url}\" returned {statusCode}:\n{content}");
			}
				
			return JsonSerializer.Deserialize<T>(content);
		}
	}
}
