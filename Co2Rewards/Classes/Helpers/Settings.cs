namespace IsolatedStorageSettings
{
	/// <summary>
	/// Settings class for storing values in app.
	/// </summary>
	public class Settings
	{
		//Store game highscore.
		public static readonly Setting<string> ID = new Setting<string>("ID", "");
	}
}