using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Co2Rewards
{
	public partial class StartScreen : ContentPage
	{
		public StartScreen ()
		{
			InitializeComponent ();
			App.UserID = Methods<string>.GetID ();
		}

		public void btnTrip_Clicked(object sender, EventArgs e)
		{
			Navigation.PushAsync (new StartTrip ());
		}

		public void btnRewards_Clicked(object sender, EventArgs e)
		{
			Navigation.PushAsync (new AvailableRewards ());
		}

		public void btnInfo_Clicked(object sender, EventArgs e)
		{
			Navigation.PushAsync (new MyInfo ());
		}

		public void btnPartners_Clicked(object sender, EventArgs e)
		{
			Navigation.PushAsync (new Partners ());
		}

		public void btnAbout_Clicked(object sender, EventArgs e)
		{
			Navigation.PushAsync (new About ());
		}
	}
}

