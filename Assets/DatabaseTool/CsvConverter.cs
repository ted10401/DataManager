using UnityEngine;
using System.Collections.Generic;
using System;

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
	
	
	public static T[] ConvertToArray<T>(string value)
	{
		string[] temp = value.Split (';');

		int arrayLength = 0;
		
		for(int cnt = 0; cnt < temp.Length; cnt++)
		{
			if(string.IsNullOrEmpty(temp[cnt]))
				continue;
			
			arrayLength++;
		}

		T[] array = new T[arrayLength];
		int pointer = 0;

		for(int cnt = 0; cnt < temp.Length; cnt++)
		{
			if(string.IsNullOrEmpty(temp[cnt]))
				continue;

			array[pointer] = (T)Convert.ChangeType(temp[cnt], typeof(T));
			pointer++;
		}
		
		return array;
	}
}
