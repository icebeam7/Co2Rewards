using System;
using Newtonsoft.Json;

namespace Co2Rewards
{
	public class Advertisement
	{
		public string Id { get; set; }

		[JsonProperty(PropertyName = "image")]
		public string Image { get; set; }

		[JsonProperty(PropertyName = "min_date")]
		public DateTime MinDate { get; set; }

		[JsonProperty(PropertyName = "max_date")]
		public DateTime MaxDate { get; set; }

		[JsonProperty(PropertyName = "id_company")]
		public string IDCompany { get; set; }
	}
}

