using UnityEngine;
using System.Collections.Generic;
using Pathfinding.Serialization.JsonFx;
using System.IO;
using UnityEditor;
using System.Reflection;

public class CsvToJsonConverter
{
	public static string ConvertToJson<T>(TextAsset csvDatas) where T : new()
	{
		string[][] csvData = SerializeCSV(csvDatas);

		object outputData = CsvToJson<T>(csvDatas.name, csvData);

		return JsonWriter.Serialize (outputData);
	}


	public static string[][] SerializeCSV(TextAsset csvData)
	{
		string[][] csv;
		string[] lineArray = csvData.text.Replace("\n", string.Empty).Split ("\r"[0]);
		
		csv = new string [lineArray.Length][];
		
		for(int i =0; i < lineArray.Length; i++)  
		{  
			csv[i] = lineArray[i].Split (',');  
		}
		
		return csv;
	}


	private static object CsvToJson<T>(string fileName, string[][] csvData) where T : new()
	{
		T[] outputData;
		
		outputData = new T[csvData.Length - 1];
		
		for(int cnt = 0; cnt < outputData.Length; cnt++)
		{
			outputData[cnt] = new T();
			int temp = -1;
			
			FieldInfo[] fieldInfos = outputData[cnt].GetType().GetFields();
			
			for(int parameter = 0; parameter < fieldInfos.Length; parameter++)
			{
				temp++;
				if(fieldInfos[parameter].FieldType.ToString() == "System.Boolean")
				{
					fieldInfos[parameter].SetValue(outputData[cnt], bool.Parse(csvData[cnt + 1][temp]));
				}
				else if(fieldInfos[parameter].FieldType.ToString() == "System.Collections.Generic.List`1[System.String]")
					fieldInfos[parameter].SetValue(outputData[cnt], ConvertStringToList(csvData[cnt + 1][temp]));
				else if(fieldInfos[parameter].FieldType.ToString() == "System.Single")
					fieldInfos[parameter].SetValue(outputData[cnt], float.Parse(csvData[cnt + 1][temp]));
				else if(fieldInfos[parameter].FieldType.ToString() == "System.Int32")
					fieldInfos[parameter].SetValue(outputData[cnt], int.Parse(csvData[cnt + 1][temp]));
				else
					fieldInfos[parameter].SetValue(outputData[cnt], csvData[cnt + 1][temp]);
			}
		}
		
		return outputData;
	}


	public static List<string> ConvertStringToList(string value)
	{
		List<string> data = new List<string> ();
		string[] temp = value.Split (';');

		for(int cnt = 0; cnt < temp.Length; cnt++)
		{
			if(string.IsNullOrEmpty(temp[cnt]))
				continue;

			data.Add(temp[cnt]);
		}

		return data;
	}
}
