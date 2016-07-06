using UnityEngine;
using System.Collections;
using TEDTool.Data;
using TEDTool.Json;

public class Example : MonoBehaviour
{
	private DataManager m_dataManager;

	void Awake()
	{
		m_dataManager = new DataManager();
		m_dataManager.Load();

		LoadLevelData();
		LoadMonsterData();
	}


	private void LoadLevelData()
	{
		LevelDataType levelDataType = m_dataManager.GetDataType<LevelDataType>();
		LevelData levelData = null;
		
		for(int cnt = 0; cnt < levelDataType.GetCount(); cnt++)
		{
			levelData = levelDataType.GetData((cnt + 1).ToString());

			Debug.Log(string.Format("LevelData_{0} : Key = {1}, Level = {2}, Hp = {3}, Exp = {4}",
			                        cnt + 1, levelData.Key, levelData.Level, levelData.Hp, levelData.Exp));
		}
	}


	private void LoadMonsterData()
	{
		MonsterDataType monsterDataType = m_dataManager.GetDataType<MonsterDataType>();
		MonsterData monsterData = null;
		
		for(int cnt = 0; cnt < monsterDataType.GetCount(); cnt++)
		{
			monsterData = monsterDataType.GetData((cnt + 1).ToString());

			Debug.Log(string.Format("MonsterData_{0} : Key = {1}, Name = {2}, Hp = {3}",
			                        cnt + 1, monsterData.Key, monsterData.Name, monsterData.Hp));
		}
	}
}
