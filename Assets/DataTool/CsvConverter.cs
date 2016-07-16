using UnityEngine;
using System.Collections.Generic;

public class CsvConverter
{
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
