using UnityEditor;
using UnityEngine;
using System.IO;

namespace TEDTool.Data
{
	public class DataGenerateTool
	{
		[MenuItem("TEDTool/Data/Generate Data")]
		private static void GenerateData()
		{
			Initialize();

			CsvToJsonConverter.Convert<LevelData>("Level");
			CsvToJsonConverter.Convert<MonsterData>("Monster");

			AssetDatabase.Refresh();
			Debug.Log("Auto generate datas finished.");
		}


		private static void Initialize()
		{
			if(Directory.Exists(CsvToJsonConverter.OUTPUT_PATH))
				Directory.Delete(CsvToJsonConverter.OUTPUT_PATH, true);
			
			Directory.CreateDirectory(CsvToJsonConverter.OUTPUT_PATH);
		}
	}
}
