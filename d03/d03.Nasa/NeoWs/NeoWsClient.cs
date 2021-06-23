using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using d03.Nasa.NeoWs.Models;

namespace d03.Nasa.NeoWs
{
	public class NeoWsClient : ApiClientBase, INasaClient<AsteroidRequest, Task<AsteroidLookup[]>>
	{
		public struct neoFeed
		{
			[JsonPropertyName("near_earth_objects")]
			public Dictionary<DateTime, AsteroidInfo[]> NearEarthObjects { get; set; }
		}

		public NeoWsClient(string apiKey) : base(apiKey) { }

		public async Task<AsteroidLookup[]> GetAsync(AsteroidRequest request)
		{
			var apiUrl = "https://api.nasa.gov/neo/rest/v1/";

			var url = $"{apiUrl}feed?start_date={request.StartDate}"
					+ $"&end_date={request.EndDate}&api_key={ApiKey}";

			var responce = await HttpGetAsync<neoFeed>(url);

			var asteroids = responce.NearEarthObjects
							.SelectMany(a => a.Value)
							.OrderBy(a => a.Distance)
							.Take(request.RequestCount)
							.ToList();

			return await Task.WhenAll(asteroids.Select(a =>
						HttpGetAsync<AsteroidLookup>($"{apiUrl}neo/{a.Id}?api_key={ApiKey}")));
		}
	}
}
