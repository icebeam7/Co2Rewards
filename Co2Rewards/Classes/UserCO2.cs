using System;
using Newtonsoft.Json;

namespace Co2Rewards
{
	public class UserCO2
	{
		public string Id { get; set; }

		[JsonProperty(PropertyName = "month")]
		public int Month { get; set; }

		[JsonProperty(PropertyName = "year")]
		public int Year { get; set; }

		[JsonProperty(PropertyName = "co2level")]
		public decimal CO2Level { get; set; }

		[JsonProperty(PropertyName = "id_user")]
		public string IDUser { get; set; }
	}
}

