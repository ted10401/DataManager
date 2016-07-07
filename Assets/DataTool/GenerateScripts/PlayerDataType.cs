using UnityEngine;
using System.Collections.Generic;
using TEDTool.Json;

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

		private PlayerData[] m_datas;

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
			string jsonData = CsvToJsonConverter.ConvertToJson<PlayerData>(textData);
			m_datas = JsonConverter.DeserializeClasses<PlayerData> (jsonData);
		}


		public PlayerData GetData(string key)
		{
			for(int cnt = 0; cnt < m_datas.Length; cnt++)
			{
				if(m_datas[cnt].Key == key)
				{
					return m_datas[cnt];
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
