using System;
using System.IO;
using System.Globalization;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using d03.Nasa;
using d03.Nasa.Apod;
using d03.Nasa.Apod.Models;
using d03.Nasa.NeoWs;
using d03.Nasa.NeoWs.Models;

namespace d03.Host
{
	class Program
	{
		static async Task Main(string[] args)
		{
			if (args.Length == 0)
			{
				return;
			}
			
			CultureInfo.CurrentCulture = new CultureInfo("en-GB", false);

			var configuration = new ConfigurationBuilder()
				.AddJsonFile("appsettings.json")
				.SetBasePath(Directory.GetCurrentDirectory())
				.Build();

			var apiKey = configuration["ApiKey"];

			if (args[0].Trim() == "apod")
			{
				if (args.Length == 1 || !int.TryParse(args[1].Trim(), out int requestCount))
				{
					return;
				}

				try
				{
					INasaClient<int, Task<MediaOfToday[]>> apod = new ApodClient(apiKey);
					var result = await apod.GetAsync(requestCount);
	
					Console.WriteLine(string.Join("\n\n", (object[])result));
				}
				catch (Exception e)
				{
					Console.WriteLine(e.Message);
				}
			}
			else if (args[0].Trim() == "neows")
			{
				var startDate = configuration.GetSection("NeoWs")["StartDate"];
				var endDate = configuration["NeoWs:EndDate"];

				if (args.Length == 1 || !int.TryParse(args[1].Trim(), out int requestCount))
				{
					requestCount = int.MaxValue;
				}

				var request = new AsteroidRequest(startDate, endDate, requestCount);

				try
				{
					INasaClient<AsteroidRequest, Task<AsteroidLookup[]>> neows = new NeoWsClient(apiKey);
					var result = await neows.GetAsync(request);

					Console.WriteLine(string.Join("\n\n", (object[])result));
				}
				catch (Exception e)
				{
					Console.WriteLine(e.Message);
				}
			}
		}
	}
}
