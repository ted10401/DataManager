using UnityEngine;
using System.Collections;
using TEDTool.Database;

public class Example : MonoBehaviour
{
	private DatabaseManager m_databaseManager;

	void Awake()
	{
		m_databaseManager = new DatabaseManager();
		m_databaseManager.Load();

		PrintPlayerData();
		PrintMonsterData();
	}


	private void PrintPlayerData()
	{
		PlayerDatabase playerDataType = m_databaseManager.GetDatabase<PlayerDatabase>();
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
		MonsterDatabase monsterDataType = m_databaseManager.GetDatabase<MonsterDatabase>();
		MonsterData monsterData = null;
		
		for(int cnt = 0; cnt < monsterDataType.GetCount(); cnt++)
		{
			monsterData = monsterDataType.GetData(cnt.ToString());

			Debug.Log(string.Format("MonsterData_{0} : Key = {1}, Name = {2}, Hp = {3}",
			                        cnt, monsterData.Key, monsterData.Name, monsterData.Hp));
		}
	}
}
