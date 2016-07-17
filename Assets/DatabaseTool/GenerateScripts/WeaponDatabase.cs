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
			m_datas = CsvConverter.SerializeCSVData(textData);
		}


		public WeaponData GetDataByKey(string key)
		{
			for(int cnt = 0; cnt < m_datas.Length; cnt++)
			{
				if(m_datas[cnt][0] == key)
				{
					m_tempData.Key = m_datas[cnt][0];
			m_tempData.Name = m_datas[cnt][1];
			m_tempData.Atk = CsvConverter.ConvertToArray<int>(m_datas[cnt][2]);
			m_tempData.Rarity = CsvConverter.ConvertToArray<string>(m_datas[cnt][3]);

					return m_tempData;
				}
			}

			return null;
		}


		public int GetCount()
		{
			return m_datas.Length;
		}
	}
}
