using System;
using System.Globalization;
using System.Text.Json.Serialization;

namespace d03.Nasa.Apod.Models
{
	public class MediaOfToday
	{
		public DateTime Date => DateTime.Parse(DateString);

		[JsonPropertyName("date")]
		public string DateString { get; set; }
		[JsonPropertyName("title")]
		public string Title { get; set; }
		[JsonPropertyName("copyright")]
		public string Copyright { get; set; }
		[JsonPropertyName("explanation")]
		public string Explanation { get; set; }
		[JsonPropertyName("url")]
		public string Url { get; set; }

		public override string ToString() => $"{Date:d}\n'{Title}' by {Copyright}\n{Explanation}\n{Url}";
	}
}
