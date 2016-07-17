using UnityEngine;
using System.Collections.Generic;

namespace TEDTool.Database
{
	public class MonsterData
	{
		public string Key;
		public string Name;
		public int Hp;
	}

	public class MonsterDatabase : IDatabase
	{
		public const uint TYPE_ID = 1;
		public const string DATA_PATH = "CsvResources/Monster";

		private MonsterData m_tempData = new MonsterData();
		private string[][] m_datas;

		public MonsterDatabase(){}

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


		public MonsterData GetDataByKey(string key)
		{
			for(int cnt = 0; cnt < m_datas.Length; cnt++)
			{
				if(m_datas[cnt][0] == key)
				{
					m_tempData.Key = m_datas[cnt][0];
			m_tempData.Name = m_datas[cnt][1];
			
			if(!int.TryParse(m_datas[cnt][2], out m_tempData.Hp))
			{
				m_tempData.Hp = 0;
			}


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
