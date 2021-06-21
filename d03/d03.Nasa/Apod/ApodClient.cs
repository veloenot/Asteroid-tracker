using System;
using System.Threading.Tasks;
using d03.Nasa.Apod.Models;

namespace d03.Nasa.Apod
{
	public class ApodClient : ApiClientBase, INasaClient<int, Task<MediaOfToday[]>>
	{
		public ApodClient(string apiKey) : base(apiKey) { }

		public async Task<MediaOfToday[]> GetAsync(int ResultCount)
		{
			var url = $"https://api.nasa.gov/planetary/apod?count={ResultCount}"
					+ $"&api_key={ApiKey}";
			
			return await HttpGetAsync<MediaOfToday[]>(url);
		}
	}
}
