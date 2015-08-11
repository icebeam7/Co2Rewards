using System;
using Newtonsoft.Json;

namespace Co2Rewards
{
	public class Reward
	{
		public string Id { get; set; }

		[JsonProperty(PropertyName = "title")]
		public string Title { get; set; }

		[JsonProperty(PropertyName = "description")]
		public string Description { get; set; }

		[JsonProperty(PropertyName = "minco2level")]
		public decimal MinCO2Level { get; set; }

		[JsonProperty(PropertyName = "prize")]
		public string Prize { get; set; }

		[JsonProperty(PropertyName = "prize_description")]
		public string PrizeDescription { get; set; }

		[JsonProperty(PropertyName = "min_date")]
		public DateTime MinDate { get; set; }

		[JsonProperty(PropertyName = "max_date")]
		public DateTime MaxDate { get; set; }

		[JsonProperty(PropertyName = "image")]
		public string Image { get; set; }

		[JsonProperty(PropertyName = "limit")]
		public int Limit { get; set; }

		[JsonProperty(PropertyName = "id_company")]
		public string IDCompany { get; set; }
	}
}