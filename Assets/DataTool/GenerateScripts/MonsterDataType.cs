using UnityEngine;
using System.Collections.Generic;

namespace TEDTool.Data
{
	public class MonsterData
	{
		public string Key;
		public string Name;
		public int Hp;
	}

	public class MonsterDataType : IDataType
	{
		public const uint TYPE_ID = 1;
		public const string DATA_PATH = "CsvResources/Monster";

		private MonsterData m_tempData = new MonsterData();
		private string[][] m_datas;

		public MonsterDataType(){}

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


		public MonsterData GetData(string key)
		{
			int keyValue = 0;
			
			if(!int.TryParse(key, out keyValue))
				return null;

			if(keyValue >= m_datas.Length)
				return null;
				
			m_tempData.Key = m_datas[keyValue][0];
			m_tempData.Name = m_datas[keyValue][1];
			
			if(!int.TryParse(m_datas[keyValue][2], out m_tempData.Hp))
			{
				m_tempData.Hp = 0;
			}

			
			return m_tempData;
		}


		public int GetCount()
		{
			return m_datas.Length;
		}
	}
}
