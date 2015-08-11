using System;
using Newtonsoft.Json;

namespace Co2Rewards
{
	public class User
	{
		public string Id { get; set; }

		[JsonProperty(PropertyName = "name")]
		public string Name { get; set; }
	}

}

