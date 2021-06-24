using System;

namespace d03.Nasa.NeoWs.Models
{
	public record AsteroidRequest(string StartDate, string EndDate, int RequestCount);
}
