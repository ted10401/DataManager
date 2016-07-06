using UnityEngine;
using UnityEditor;
using System;
using System.IO;

public class ScriptGenerateTool
{
	private static string GENERATE_SCRIPT_PATH = Application.dataPath + "/DataTool/GenerateScripts/";
	private static string EDITOR_PATH = Application.dataPath + "/DataTool/Editor/";
	private static string TEMPLATE_IDATATYPE_PATH = "Assets/DataTool/Editor/Template_IDataType.txt";
	private static string TEMPLATE_DATATYPE_PATH = "Assets/DataTool/Editor/Template_DataType.txt";
	private static string TEMPLATE_DATAMANAGER_PATH = "Assets/DataTool/Editor/Template_DataManager.txt";
	private static string TEMPLATE_DATA_GENERATE_TOOL_PATH = "Assets/DataTool/Editor/Template_DataGenerateTool.txt";
	private static string CSV_PATH = Application.dataPath + "/DataTool/CsvResources/";

	private static int DATA_ID;
	private static string REGISTER_LIST;
	private static string CONVERT_LIST;

	[MenuItem("TEDTool/Data/Generate Script")]
	public static void GenerateScript()
	{
		Initialize();
		CreateIDataTypeScript();
		CreateDataTypeScript();
		CreateDataManagerScript();
		CreateDataGenerateToolScript();

		AssetDatabase.Refresh();
		Debug.Log("Auto generate scripts finished.");
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


	private static void CreateIDataTypeScript()
	{
		string template = GetTemplate(TEMPLATE_IDATATYPE_PATH);
		GenerateScript("IDataType", template);
	}


	private static void CreateDataTypeScript()
	{
		string[] csvPaths = Directory.GetFiles(CSV_PATH, "*.csv", SearchOption.AllDirectories);
		string assetPath = "";
		TextAsset textAsset = null;

		for(int cnt = 0; cnt < csvPaths.Length; cnt++)
		{
			assetPath = "Assets" + csvPaths[cnt].Replace(Application.dataPath, "").Replace('\\', '/');
			textAsset = (TextAsset)AssetDatabase.LoadAssetAtPath(assetPath, typeof(TextAsset));

			REGISTER_LIST += string.Format("RegisterDataType (new {0}DataType());\n", textAsset.name);
			if(cnt != csvPaths.Length - 1)
				REGISTER_LIST += "\t\t\t";
			
			CONVERT_LIST += string.Format("CsvToJsonConverter.Convert<{0}Data>(\"{0}\");\n", textAsset.name);
			if(cnt != csvPaths.Length - 1)
				CONVERT_LIST += "\t\t\t";

			CreateDataTypeScript(textAsset);
		}
	}


	private static void CreateDataManagerScript()
	{
		string template = GetTemplate(TEMPLATE_DATAMANAGER_PATH);
		template = template.Replace("$RegisterList", REGISTER_LIST);
		GenerateScript("DataManager", template);
	}


	private static void CreateDataTypeScript(TextAsset textAsset)
	{
		DATA_ID++;
		
		string parameters = GetClassParameters(textAsset);
		
		string template = GetTemplate(TEMPLATE_DATATYPE_PATH);
		template = template.Replace("$DataClassName", textAsset.name + "Data");
		template = template.Replace("$DataAttributes", parameters);
		template = template.Replace("$DataTypeName", textAsset.name + "DataType");
		template = template.Replace("$DataID", DATA_ID.ToString());
		template = template.Replace("$DataPath", "\"Database/" + textAsset.name + "\"");
		
		GenerateScript(textAsset.name + "DataType", template);
	}


	private static void CreateDataGenerateToolScript()
	{
		string template = GetTemplate(TEMPLATE_DATA_GENERATE_TOOL_PATH);
		template = template.Replace("$ConvertList", CONVERT_LIST);
		GenerateTool("DataGenerateTool", template);
	}


	private static string GetClassParameters(TextAsset textAsset)
	{
		string[][] csvData = CsvToJsonConverter.SerializeCSV(textAsset);
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
