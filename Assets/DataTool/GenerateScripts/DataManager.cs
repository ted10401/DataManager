using UnityEngine;
using System.Collections.Generic;

namespace TEDTool.Data
{
	public class DataManager
	{
		private Dictionary<uint, IDataType> m_dataTypes;

		public DataManager()
		{
			m_dataTypes = new Dictionary<uint, IDataType>();

			RegisterDataType (new LevelDataType());
			RegisterDataType (new MonsterDataType());

			Load();
		}


		public void Load()
		{
			foreach(KeyValuePair<uint, IDataType> data in m_dataTypes)
			{
				data.Value.Load();
			}
		}


		public T GetDataType<T>() where T : IDataType, new()
		{
			T result = new T();
			if(m_dataTypes.ContainsKey(result.TypeID()))
			{
				return (T)m_dataTypes[result.TypeID()];
			}

			return default(T);
		}


		private void RegisterDataType(IDataType dataType)
		{
			m_dataTypes[dataType.TypeID()] = dataType;
		}
	}
}

