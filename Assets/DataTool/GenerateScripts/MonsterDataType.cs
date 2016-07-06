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
		public const uint TYPE_ID = 2;
		public const string DATA_PATH = "Database/Monster";

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


		public void Load (string value)
		{
			m_datas = JsonConverter.DeserializeClasses<MonsterData> (value);
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
