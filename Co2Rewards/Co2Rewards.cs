using System;

using Xamarin.Forms;

namespace Co2Rewards
{
	public class App : Application
	{
		public static string UserID;

		public App ()
		{
			// The root page of your application
			MainPage = new NavigationPage(new StartScreen());
		}

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}

