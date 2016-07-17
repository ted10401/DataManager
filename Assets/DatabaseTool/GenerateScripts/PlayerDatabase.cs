using UnityEngine;
using System.Collections.Generic;

namespace TEDTool.Database
{
	public class PlayerData
	{
		public string Key;
		public int Level;
		public int Hp;
		public int Exp;
	}

	public class PlayerDatabase : IDatabase
	{
		public const uint TYPE_ID = 2;
		public const string DATA_PATH = "CsvResources/Player";

		private PlayerData m_tempData = new PlayerData();
		private string[][] m_datas;

		public PlayerDatabase(){}

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


		public PlayerData GetDataByKey(string key)
		{
			for(int cnt = 0; cnt < m_datas.Length; cnt++)
			{
				if(m_datas[cnt][0] == key)
				{
					m_tempData.Key = m_datas[cnt][0];
			
			if(!int.TryParse(m_datas[cnt][1], out m_tempData.Level))
			{
				m_tempData.Level = 0;
			}

			
			if(!int.TryParse(m_datas[cnt][2], out m_tempData.Hp))
			{
				m_tempData.Hp = 0;
			}

			
			if(!int.TryParse(m_datas[cnt][3], out m_tempData.Exp))
			{
				m_tempData.Exp = 0;
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
