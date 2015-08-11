using System;
using Newtonsoft.Json;


namespace Co2Rewards
{
	public class UserReward
	{
		public string Id { get; set; }

		[JsonProperty(PropertyName = "claim_date")]
		public DateTime ClaimDate { get; set; }

		[JsonProperty(PropertyName = "id_user")]
		public string IDUser { get; set; }

		[JsonProperty(PropertyName = "id_reward")]
		public string IDReward { get; set; }
	}
}

