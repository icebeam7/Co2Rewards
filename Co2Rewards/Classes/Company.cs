using System;
using Newtonsoft.Json;

namespace Co2Rewards
{
	public class Company
	{
		public string Id { get; set; }

		[JsonProperty(PropertyName = "name")]
		public string Name { get; set; }

		[JsonProperty(PropertyName = "logo")]
		public string Logo { get; set; }

		[JsonProperty(PropertyName = "link")]
		public string Link { get; set; }

		[JsonProperty(PropertyName = "description")]
		public string Description { get; set; }

		[JsonProperty(PropertyName = "email")]
		public string Email { get; set; }

		[JsonProperty(PropertyName = "phone")]
		public string Phone { get; set; }
	}
}

