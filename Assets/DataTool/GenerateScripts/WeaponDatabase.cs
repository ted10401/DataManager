using UnityEngine;
using System.Collections.Generic;

namespace TEDTool.Database
{
	public class WeaponData
	{
		public string Key;
		public string Name;
		public int[] Atk;
		public string[] Rarity;
	}

	public class WeaponDatabase : IDatabase
	{
		public const uint TYPE_ID = 3;
		public const string DATA_PATH = "CsvResources/Weapon";

		private WeaponData m_tempData = new WeaponData();
		private string[][] m_datas;

		public WeaponDatabase(){}

		public uint TypeID ()
		{
			return TYPE_ID;
		}


		public string DataPath ()
		{
			return DATA_PATH;
		}


		public void Load ()
		{
			TextAsset textData = Resources.Load<TextAsset>(DataPath());
			m_datas = CsvConverter.SerializeCSV(textData);
		}


		public WeaponData GetDataByKey(string key)
		{
			int keyValue = 0;
			
			if(!int.TryParse(key, out keyValue))
				return null;

			if(keyValue >= m_datas.Length)
				return null;
				
			m_tempData.Key = m_datas[keyValue][0];
			m_tempData.Name = m_datas[keyValue][1];
			m_tempData.Atk = CsvConverter.ConvertToArray<int>(m_datas[keyValue][2]);
			m_tempData.Rarity = CsvConverter.ConvertToArray<string>(m_datas[keyValue][3]);
			
			return m_tempData;
		}


		public int GetCount()
		{
			return m_datas.Length;
		}
	}
}
