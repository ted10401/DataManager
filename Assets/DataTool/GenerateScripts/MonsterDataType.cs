using UnityEngine;
using System.Collections.Generic;
using TEDTool.Json;

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

		private MonsterData[] m_datas;

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
			string jsonData = CsvToJsonConverter.ConvertToJson<MonsterData>(textData);
			m_datas = JsonConverter.DeserializeClasses<MonsterData> (jsonData);
		}


		public MonsterData GetData(string key)
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
