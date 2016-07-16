using UnityEngine;
using System.Collections.Generic;

namespace TEDTool.Data
{
	public class PlayerData
	{
		public string Key;
		public int Level;
		public int Hp;
		public int Exp;
	}

	public class PlayerDataType : IDataType
	{
		public const uint TYPE_ID = 2;
		public const string DATA_PATH = "CsvResources/Player";

		private PlayerData m_tempData = new PlayerData();
		private string[][] m_datas;

		public PlayerDataType(){}

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


		public PlayerData GetData(string key)
		{
			int keyValue = 0;
			
			if(!int.TryParse(key, out keyValue))
				return null;

			if(keyValue >= m_datas.Length)
				return null;
				
			m_tempData.Key = m_datas[keyValue][0];
			
			if(!int.TryParse(m_datas[keyValue][1], out m_tempData.Level))
			{
				m_tempData.Level = 0;
			}

			
			if(!int.TryParse(m_datas[keyValue][2], out m_tempData.Hp))
			{
				m_tempData.Hp = 0;
			}

			
			if(!int.TryParse(m_datas[keyValue][3], out m_tempData.Exp))
			{
				m_tempData.Exp = 0;
			}

			
			return m_tempData;
		}


		public int GetCount()
		{
			return m_datas.Length;
		}
	}
}
