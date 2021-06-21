using System;
using System.IO;
using System.Threading.Tasks;
using System.Globalization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using d03.Nasa;
using d03.Nasa.Apod;
using d03.Nasa.Apod.Models;

namespace d03.Host
{
	class Program
	{
		static async Task Main(string[] args)
		{
			CultureInfo.CurrentCulture = new CultureInfo("en-GB", false);

			var configuration = new ConfigurationBuilder()
				.AddJsonFile("appsettings.json")
				.SetBasePath(Directory.GetCurrentDirectory())
				.Build();

			var api_key = configuration["ApiKey"];
		
			if (args[0].Trim() == "apod")
			{
				try
				{
					INasaClient<int, Task<MediaOfToday[]>> apod = new ApodClient(api_key);
					var result = await apod.GetAsync(int.Parse(args[1].Trim()));
	
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

//			var start_date = "2020-07-14";
//			var end_date = "2020-07-14";

//			var req2 = $"{url}start_date={start_date}&end_date={end_date}&api_key={api_key}";
