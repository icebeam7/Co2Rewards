using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace IsolatedStorageSettings
{
	public class Setting<T>
	{
		string name;
		T value;
		T defaultValue;
		bool hasValue;

		public Setting(string name, T defaultValue)
		{
			this.name = name;
			this.defaultValue = defaultValue;
		}

		public T Value
		{
			get
			{
				//checked for cached value
				if (!this.hasValue)
				{
					//try to get value from isolated storage
					if (!IsolatedStorageSettings.ApplicationSettings.TryGetValue(this.name, out this.value))
					{
						this.value = this.defaultValue;
						IsolatedStorageSettings.ApplicationSettings[this.name] = this.value;
					}

					this.hasValue = true;
				}

				return this.value;
			}

			set
			{
				//save value to isolated storage
				IsolatedStorageSettings.ApplicationSettings[this.name] = value;
				try
				{
					IsolatedStorageSettings.ApplicationSettings.Save();
				}
				catch { }
				this.value = value;
				this.hasValue = true;
			}
		}

		public T DefaultValue
		{
			get { return this.defaultValue; }
		}

		//clear cached value;
		public void ForceRefresh()
		{
			this.hasValue = false;
		}
	}
}