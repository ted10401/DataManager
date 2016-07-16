using UnityEngine;
using UnityEditor;
using System;
using System.IO;

public class ScriptGenerate
{
	private static string GENERATE_SCRIPT_PATH = Application.dataPath + "/DatabaseTool/GenerateScripts/";
	private static string EDITOR_PATH = Application.dataPath + "/DatabaseTool/Editor/";
	private static string TEMPLATE_IDATABASE_PATH = "Assets/DatabaseTool/Editor/Template_IDatabase.txt";
	private static string TEMPLATE_DATABASE_PATH = "Assets/DatabaseTool/Editor/Template_Database.txt";
	private static string TEMPLATE_DATABASEMANAGER_PATH = "Assets/DatabaseTool/Editor/Template_DatabaseManager.txt";
	private static string CSV_PATH = Application.dataPath + "/DatabaseTool/Resources/CsvResources/";

	private static int DATA_ID;
	private static string REGISTER_LIST;
	private static string CONVERT_LIST;

	[MenuItem("TEDTool/Database/Generate Script")]
	public static void GenerateScript()
	{
		Initialize();
		CreateIDatabaseScript();
		CreateDatabaseScript();
		CreateDatabaseManagerScript();

		AssetDatabase.Refresh();
		Debug.Log("Database scripts generate have completed.");
	}


	private static void Initialize()
	{
		DATA_ID = 0;
		REGISTER_LIST = string.Empty;
		CONVERT_LIST = string.Empty;

		if(Directory.Exists(GENERATE_SCRIPT_PATH))
			Directory.Delete(GENERATE_SCRIPT_PATH, true);
		
		Directory.CreateDirectory(GENERATE_SCRIPT_PATH);
	}


	private static void CreateIDatabaseScript()
	{
		string template = GetTemplate(TEMPLATE_IDATABASE_PATH);
		GenerateScript("IDatabase", template);
	}


	private static void CreateDatabaseScript()
	{
		string[] csvPaths = Directory.GetFiles(CSV_PATH, "*.csv", SearchOption.AllDirectories);
		string assetPath = "";
		TextAsset textAsset = null;

		for(int cnt = 0; cnt < csvPaths.Length; cnt++)
		{
			assetPath = "Assets" + csvPaths[cnt].Replace(Application.dataPath, "").Replace('\\', '/');
			textAsset = (TextAsset)AssetDatabase.LoadAssetAtPath(assetPath, typeof(TextAsset));

			REGISTER_LIST += string.Format("RegisterDataType (new {0}Database());\n", textAsset.name);
			if(cnt != csvPaths.Length - 1)
				REGISTER_LIST += "\t\t\t";
			
			CONVERT_LIST += string.Format("CsvToJsonConverter.Convert<{0}Data>(\"{0}\");\n", textAsset.name);
			if(cnt != csvPaths.Length - 1)
				CONVERT_LIST += "\t\t\t";

			CreateDatabaseScript(textAsset);
		}
	}


	private static void CreateDatabaseManagerScript()
	{
		string template = GetTemplate(TEMPLATE_DATABASEMANAGER_PATH);
		template = template.Replace("$RegisterList", REGISTER_LIST);
		GenerateScript("DatabaseManager", template);
	}


	private static void CreateDatabaseScript(TextAsset textAsset)
	{
		DATA_ID++;
		
		string template = GetTemplate(TEMPLATE_DATABASE_PATH);
		template = template.Replace("$DataClassName", textAsset.name + "Data");
		template = template.Replace("$DataAttributes", GetClassParameters(textAsset));
		template = template.Replace("$CsvSerialize", GetCsvSerialize(textAsset));
		template = template.Replace("$DataTypeName", textAsset.name + "Database");
		template = template.Replace("$DataID", DATA_ID.ToString());
		template = template.Replace("$DataPath", "\"CsvResources/" + textAsset.name + "\"");
		
		GenerateScript(textAsset.name + "Database", template);
	}


	private static string GetClassParameters(TextAsset textAsset)
	{
		string[][] csvData = CsvConverter.SerializeCSV(textAsset);
		int keyCount = csvData[0].Length;
		
		string classParameters = string.Empty;
		
		for(int cnt = 0; cnt < keyCount; cnt++)
		{
			string[] attributes = csvData[0][cnt].Split(new char[]{'/'}, System.StringSplitOptions.RemoveEmptyEntries);
			classParameters += string.Format("public {0} {1};", attributes[0], attributes[1]);

			if(cnt != keyCount - 1)
			{
				classParameters += "\n";
				classParameters += "\t\t";
			}
		}

		return classParameters;
	}


	private static string GetCsvSerialize(TextAsset textAsset)
	{
		string[][] csvData = CsvConverter.SerializeCSV(textAsset);
		int keyCount = csvData[0].Length;
		
		string csvSerialize = string.Empty;
		
		for(int cnt = 0; cnt < keyCount; cnt++)
		{
			string[] attributes = csvData[0][cnt].Split(new char[]{'/'}, System.StringSplitOptions.RemoveEmptyEntries);
			
			if(attributes[0] == "string")
			{
				csvSerialize += string.Format("m_tempData.{0} = m_datas[keyValue][{1}];", attributes[1], cnt);
			}
			else if(attributes[0] == "bool")
			{
				csvSerialize += GetCsvSerialize(attributes, cnt, "false");
			}
			else if(attributes[0] == "int")
			{
				csvSerialize += GetCsvSerialize(attributes, cnt, "0");
			}
			else if(attributes[0] == "float")
			{
				csvSerialize += GetCsvSerialize(attributes, cnt, "0.0f");
			}
			else if(attributes[0] == "string[]")
			{
				csvSerialize += string.Format("m_tempData.{0} = CsvConverter.ConvertToArray<string>(m_datas[keyValue][{1}]);", attributes[1], cnt);
			}
			else if(attributes[0] == "bool[]")
			{
				csvSerialize += string.Format("m_tempData.{0} = CsvConverter.ConvertToArray<bool>(m_datas[keyValue][{1}]);", attributes[1], cnt);
			}
			else if(attributes[0] == "int[]")
			{
				csvSerialize += string.Format("m_tempData.{0} = CsvConverter.ConvertToArray<int>(m_datas[keyValue][{1}]);", attributes[1], cnt);
			}
			else if(attributes[0] == "float[]")
			{
				csvSerialize += string.Format("m_tempData.{0} = CsvConverter.ConvertToArray<float>(m_datas[keyValue][{1}]);", attributes[1], cnt);
			}
			
			if(cnt != keyCount - 1)
			{
				csvSerialize += "\n";
				csvSerialize += "\t\t\t";
			}
		}
		
		return csvSerialize;
	}
	
	
	private static string GetCsvSerialize(string[] attributes, int arrayCount, string defaultValue)
	{
		string csvSerialize = "";
		
		csvSerialize += string.Format("\n\t\t\tif(!{0}.TryParse(m_datas[keyValue][{1}], out m_tempData.{2}))\n", attributes[0], arrayCount, attributes[1]);
		csvSerialize += "\t\t\t{\n";
		csvSerialize += string.Format("\t\t\t\tm_tempData.{0} = {1};\n", attributes[1], defaultValue);
		csvSerialize += "\t\t\t}\n";
		
		return csvSerialize;
	}


	private static string GetTemplate(string path)
	{
		TextAsset txt = (TextAsset)AssetDatabase.LoadAssetAtPath(path, typeof(TextAsset));

		return txt.text;
	}


	public static void GenerateScript(string dataName, string data)
	{
		dataName = GENERATE_SCRIPT_PATH + dataName + ".cs";
		
		if (File.Exists(dataName))
		{
			File.Delete(dataName);
		}
		
		StreamWriter sr = File.CreateText(dataName);
		sr.WriteLine (data);
		sr.Close();
	}


	public static void GenerateTool(string dataName, string data)
	{
		dataName = EDITOR_PATH + dataName + ".cs";
		
		if (File.Exists(dataName))
		{
			File.Delete(dataName);
		}
		
		StreamWriter sr = File.CreateText(dataName);
		sr.WriteLine (data);
		sr.Close();
	}
}
