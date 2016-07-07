using UnityEngine;
using System.Collections;
using TEDTool.Data;

public class Example : MonoBehaviour
{
	private DataManager m_dataManager;

	void Awake()
	{
		m_dataManager = new DataManager();
		m_dataManager.Load();

		PrintPlayerData();
		PrintMonsterData();
	}


	private void PrintPlayerData()
	{
		PlayerDataType playerDataType = m_dataManager.GetDataType<PlayerDataType>();
		PlayerData playerData = null;
		
		for(int cnt = 0; cnt < playerDataType.GetCount(); cnt++)
		{
			playerData = playerDataType.GetData(cnt.ToString());

			Debug.Log(string.Format("PlayerData_{0} : Key = {1}, Level = {2}, Hp = {3}, Exp = {4}",
			                        cnt, playerData.Key, playerData.Level, playerData.Hp, playerData.Exp));
		}
	}


	private void PrintMonsterData()
	{
		MonsterDataType monsterDataType = m_dataManager.GetDataType<MonsterDataType>();
		MonsterData monsterData = null;
		
		for(int cnt = 0; cnt < monsterDataType.GetCount(); cnt++)
		{
			monsterData = monsterDataType.GetData(cnt.ToString());

			Debug.Log(string.Format("MonsterData_{0} : Key = {1}, Name = {2}, Hp = {3}",
			                        cnt, monsterData.Key, monsterData.Name, monsterData.Hp));
		}
	}
}
