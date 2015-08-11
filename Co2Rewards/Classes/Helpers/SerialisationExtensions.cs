using System.IO;
using System.Runtime.Serialization;
using System.Xml;

namespace IsolatedStorageSettings.Extensions
{
	/// <summary>
	/// Useful extensions for Serialising/Deserialsing data.
	/// </summary>
	public static class SerialisationExtensions
	{
		/// <summary>
		/// Serialise data.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="obj"></param>
		/// <returns></returns>
		public static string Serialize<T>(this T obj)
		{
			var serializer = new DataContractSerializer(obj.GetType());
			using (var writer = new StringWriter())
			using (var stm = new XmlTextWriter(writer))
			{
				serializer.WriteObject(stm, obj);
				return writer.ToString();
			}
		}

		/// <summary>
		/// Deserialise data.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="serialized"></param>
		/// <returns></returns>
		public static T Deserialize<T>(this string serialized)
		{
			if (serialized == null) return default(T);
			var serializer = new DataContractSerializer(typeof(T));
			using (var reader = new StringReader(serialized))
			using (var stm = new XmlTextReader(reader))
			{
				return (T)serializer.ReadObject(stm);
			}
		}
	}
}