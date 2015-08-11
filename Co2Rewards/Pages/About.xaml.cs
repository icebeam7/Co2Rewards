using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Co2Rewards
{
	public partial class About : ContentPage
	{
		public About ()
		{
			InitializeComponent ();
		}

		public void btnMail_Clicked(object sender, EventArgs e)
		{
			Device.OpenUri(new Uri("mailto:luis.beltran@itcelaya.edu.mx"));
		}
	}
}

