using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace IsolatedStorageSettings
{
	public sealed class IsolatedStorageSettings
	{
		static string name = "PinpointApplicationSettings.data";
		static string directory = "data";

		public static Dictionary<string, object> Data = new Dictionary<string, object>();

		// Summary:
		//     Gets an instance of System.IO.IsolatedStorage.IsolatedStorageSettings that
		//     contains the contents of the application's System.IO.IsolatedStorage.IsolatedStorageFile,
		//     scoped at the application level, or creates a new instance of System.IO.IsolatedStorage.IsolatedStorageSettings
		//     if one does not exist.
		//
		// Returns:
		//     An System.IO.IsolatedStorage.IsolatedStorageSettings object that contains
		//     the contents of the application's System.IO.IsolatedStorage.IsolatedStorageFile,
		//     scoped at the application level. If an instance does not already exist, a
		//     new instance is created.
		private static IsolatedStorageSettings applicationSettings;
		public static IsolatedStorageSettings ApplicationSettings
		{
			get
			{
				//try and load settings file otherwise new instance
				if (applicationSettings == null)
				{
					applicationSettings = IsolatedStorageHelper.ReadObject<IsolatedStorageSettings>(name, directory);
					if (applicationSettings == null) applicationSettings = new IsolatedStorageSettings();
				}
				return applicationSettings;
			}
		}

		//
		// Summary:
		//     Gets a value for the specified key.
		//
		// Parameters:
		//   key:
		//     The key of the value to get.
		//
		//   value:
		//     When this method returns, the value associated with the specified key if
		//     the key is found; otherwise, the default value for the type of the value
		//     parameter. This parameter is passed uninitialized.
		//
		// Type parameters:
		//   T:
		//     The System.Type of the value parameter.
		//
		// Returns:
		//     true if the specified key is found; otherwise, false.
		//
		// Exceptions:
		//   System.ArgumentNullException:
		//     key is null.
		//
		//   System.InvalidCastException:
		//     The type of the value returned from the key cannot be implicitly cast to
		//     the type of the value parameter.
		public bool TryGetValue<T>(string key, out T value)
		{
			if (Data.ContainsKey(key))
			{
				value = (T)Data[key];
				return true;
			}

			value = default(T);
			return false;
		}

		// Summary:
		//     Gets or sets the value associated with the specified key.
		//
		// Parameters:
		//   key:
		//     The key of the item to get or set.
		//
		// Returns:
		//     The value associated with the specified key. If the specified key is not
		//     found, a get operation throws a System.Collections.Generic.KeyNotFoundException,
		//     and a set operation creates a new element that has the specified key.
		public object this[string key]
		{
			get
			{
				return Data[key];
			}
			set
			{
				Data[key] = value;
			}
		}

		//
		// Summary:
		//     Gets the number of key-value pairs that are stored in the dictionary.
		//
		// Returns:
		//     The number of key-value pairs that are stored in the dictionary.
		public int Count
		{
			get { return Data.Keys.Count(); }
		}

		//
		// Summary:
		//     Gets a collection that contains the keys in the dictionary.
		//
		// Returns:
		//     A System.Collections.Generic.Dictionary<TKey,TValue>.KeyCollection that contains
		//     the keys in the System.Collections.Generic.Dictionary<TKey,TValue>.
		public ICollection Keys
		{
			get { return Data.Keys; }
		}

		//
		// Summary:
		//     Gets a collection that contains the values in the dictionary.
		//
		// Returns:
		//     A System.Collections.Generic.Dictionary<TKey,TValue>.ValueCollection that
		//     contains the values in the System.Collections.Generic.Dictionary<TKey,TValue>.
		public ICollection Values
		{
			get { return Data.Values; }
		}

		// Summary:
		//     Adds an entry to the dictionary for the key-value pair.
		//
		// Parameters:
		//   key:
		//     The key for the entry to be stored.
		//
		//   value:
		//     The value to be stored.
		//
		// Exceptions:
		//   System.ArgumentNullException:
		//     key is null.
		//
		//   System.ArgumentException:
		//     key already exists in the dictionary.
		public void Add(string key, object value)
		{
			Data.Add(key, value);
		}

		//
		// Summary:
		//     Resets the count of items stored in System.IO.IsolatedStorage.IsolatedStorageSettings
		//     to zero and releases all references to elements in the collection.
		public void Clear()
		{
			Data = new Dictionary<string, object>();
		}

		//
		// Summary:
		//     Determines if the application settings dictionary contains the specified
		//     key.
		//
		// Parameters:
		//   key:
		//     The key for the entry to be located.
		//
		// Returns:
		//     true if the dictionary contains the specified key; otherwise, false.
		//
		// Exceptions:
		//   System.ArgumentNullException:
		//     key is null.
		public bool Contains(string key)
		{
			return Data.ContainsKey(key);
		}

		//
		// Summary:
		//     Removes the entry with the specified key.
		//
		// Parameters:
		//   key:
		//     The key for the entry to be deleted.
		//
		// Returns:
		//     true if the specified key was removed; otherwise, false.
		//
		// Exceptions:
		//   System.ArgumentNullException:
		//     key is null.
		public bool Remove(string key)
		{
			return Data.Remove(key);
		}

		//
		// Summary:
		//     Saves data written to the current System.IO.IsolatedStorage.IsolatedStorageSettings
		//     object.
		//
		// Exceptions:
		//   System.IO.IsolatedStorage.IsolatedStorageException:
		//     The System.IO.IsolatedStorage.IsolatedStorageFile does not have enough available
		//     free space.
		public void Save()
		{
			IsolatedStorageHelper.SaveObjectAsync(name, directory, this);
		}
	}
}