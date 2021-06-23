using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace d03.Nasa.NeoWs.Models
{
	public class AsteroidInfo
	{
		public double Distance => double.Parse(CloseApproachData[0].MissDistance.Kilometers);
		
		[JsonPropertyName("id")]
		public string Id { get; set; }
		[JsonPropertyName("close_approach_data")]
		public List<closeApproachData> CloseApproachData { get; set; }

		public struct closeApproachData
		{
			[JsonPropertyName("miss_distance")]
			public missDistance MissDistance { get; set; }
		}

		public struct missDistance
		{
			[JsonPropertyName("kilometers")]
			public string Kilometers { get; set; }
		}
	}
}
