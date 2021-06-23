using System;

namespace d03.Nasa.NeoWs.Models
{
	public record AsteroidRequest(string StartDate, string EndDate)
	{ 
		public int RequestCount { get; private set; } = int.MaxValue;
		
		public AsteroidRequest(string StartDate, string EndDate, int count) : this(StartDate, EndDate) 
		{
			RequestCount = count;
		}
	}
}
