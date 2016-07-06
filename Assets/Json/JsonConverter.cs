using Pathfinding.Serialization.JsonFx;

namespace TEDTool.Json
{
	public class JsonConverter
	{
		public static T[] DeserializeClasses<T>(string json) where T : class
		{
			T[] list = JsonReader.Deserialize<T[]> (json);
			return list;
		}


		public static T Deserialize<T>(string json) where T : class
		{
			return JsonReader.Deserialize<T> (json);
		}


		public static string Serialize(object data)
		{
			return JsonWriter.Serialize (data);
		}
	}
}