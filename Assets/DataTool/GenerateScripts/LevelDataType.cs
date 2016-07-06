using UnityEngine;
using System.Collections.Generic;
using TEDTool.Json;

namespace TEDTool.Data
{
	public class LevelData
	{
		public string Key;
		public int Level;
		public int Hp;
		public int Exp;
	}

	public class LevelDataType : IDataType
	{
		public const uint TYPE_ID = 1;
		public const string DATA_PATH = "Database/Level";

		private LevelData[] m_datas;

		public LevelDataType(){}

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
			m_datas = JsonConverter.DeserializeClasses<LevelData> (value);
		}


		public LevelData GetData(string key)
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
