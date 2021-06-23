using System;
using System.Text.Json.Serialization;

namespace d03.Nasa.NeoWs.Models
{
	public class AsteroidLookup
	{
		public string OrbitClassType => OrbitalData.OrbitClass.Type;
		public string OrbitClassDescription => OrbitalData.OrbitClass.Description;
		
		[JsonPropertyName("neo_reference_id")]
		public string Id { get; set; }
		[JsonPropertyName("name")]
		public string Name { get; set; }
		[JsonPropertyName("nasa_jpl_url")]
		public string NasaUrl { get; set; }
		[JsonPropertyName("is_potentially_hazardous_asteroid")]
		public bool Hazardous { get; set; }
		[JsonPropertyName("orbital_data")]
		public orbitalData OrbitalData { get; set; }

		public struct orbitalData
		{
			[JsonPropertyName("orbit_class")]
			public orbitClass OrbitClass { get; set; }
		}

		public struct orbitClass
		{
			[JsonPropertyName("orbit_class_type")]
			public string Type { get; set; }
			[JsonPropertyName("orbit_class_description")]
			public string Description { get; set; }
		}

		public override string ToString()
		{
			var result = $"-Asteroid {Name}, SPK-ID: {Id}\n";

			if (Hazardous)
			{
				result += "IS POTENTIALLY HAZARDOUS!\n";
			}

			result += $"Classification: {OrbitClassType}, {OrbitClassDescription}.\n"
					+ $"Url: {NasaUrl}.";

			return result;
		}
	}
}	
