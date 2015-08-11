using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using Android.OS;
using System.Threading.Tasks;
using IsolatedStorageSettings.Extensions;
using Android.App;

namespace IsolatedStorageSettings
{
	/// <summary>
	/// Useful methods for saving and loading data.
	/// </summary>
	public static class IsolatedStorageHelper
	{
		/// <summary>
		/// Save an object.
		/// </summary>
		/// <param name="name"></param>
		/// <param name="directory"></param>
		/// <param name="data"></param>
		/// <returns></returns>
		public static async Task SaveObjectAsync(string name, string directory, object data)
		{
			await SaveDataAsync(name, directory, data.Serialize());
		}

		/// <summary>
		/// Read an object.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="name"></param>
		/// <param name="directory"></param>
		/// <returns></returns>
		public static T ReadObject<T>(string name, string directory)
		{
			string data = ReadTextFile(name, directory);
			return data.Deserialize<T>();
		}

		/// <summary>
		/// Save string data to storage.
		/// </summary>
		/// <param name="name"></param>
		/// <param name="directory"></param>
		/// <param name="data"></param>
		/// <returns></returns>
		public static async Task SaveDataAsync(string name, string directory, string data)
		{
			//Get folder, create if doesn't exist.
			string folderPath = Path.Combine(getPath(), directory);
			if (!Directory.Exists(folderPath)) Directory.CreateDirectory(folderPath);

			//Compose filepath.
			string filePath = Path.Combine(folderPath, name);

			//Save data.
			using (FileStream file = new FileStream(filePath, FileMode.OpenOrCreate))
			{
				await CopyStreamAsync(StringToStream(data), file);
				file.Close();
			}
		}

		/// <summary>
		/// Read text file synchronously.
		/// </summary>
		/// <param name="name"></param>
		/// <param name="directory"></param>
		/// <returns></returns>
		public static string ReadTextFile(string name, string directory)
		{
			//Compose path.
			string path = Path.Combine(getPath(), directory, name);

			//If file doesn't exist, return null.
			if (!File.Exists(path)) return null;

			//Open file and read data.
			using (FileStream file = new FileStream(path, FileMode.Open))
			{
				MemoryStream ms = new MemoryStream();
				file.CopyTo(ms);
				ms.Seek(0, SeekOrigin.Begin);
				file.Close();
				return StreamToString(ms);
			}
		}

		/// <summary>
		/// Copy stream to another stream.
		/// </summary>
		/// <param name="input"></param>
		/// <param name="output"></param>
		/// <returns></returns>
		public static async Task CopyStreamAsync(Stream input, Stream output)
		{
			byte[] buffer = new byte[8 * 1024];
			int len;
			while ((len = input.Read(buffer, 0, buffer.Length)) > 0)
			{
				await output.WriteAsync(buffer, 0, len);
			}
		}

		/// <summary>
		/// Convert string to stream.
		/// </summary>
		/// <param name="text"></param>
		/// <returns></returns>
		public static Stream StringToStream(string text)
		{
			byte[] byteArray = Encoding.UTF8.GetBytes(text);
			return new MemoryStream(byteArray);
		}

		/// <summary>
		///Convert stream to string. 
		/// </summary>
		/// <param name="data"></param>
		/// <returns></returns>
		public static async Task<String> StreamToStringAsync(Stream data)
		{
			StreamReader reader = new StreamReader(data);
			return await reader.ReadToEndAsync();
		}

		/// <summary>
		/// Convert stream to string.
		/// </summary>
		/// <param name="data"></param>
		/// <returns></returns>
		public static String StreamToString(Stream data)
		{
			StreamReader reader = new StreamReader(data);
			return reader.ReadToEnd();
		}

		/// <summary>
		/// Method to get the path of a file.
		/// </summary>
		/// <returns></returns>
		public static string getPath()
		{
			if (Debugger.IsAttached) return Android.OS.Environment.ExternalStorageDirectory.Path;
			else return Application.Context.FilesDir.Path;
		}
	}
}