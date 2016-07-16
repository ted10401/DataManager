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
		PrintWeaponData();
	}


	private void PrintPlayerData()
	{
		PlayerDatabase playerDatabase = m_databaseManager.GetDatabase<PlayerDatabase>();
		PlayerData playerData = null;
		
		for(int cnt = 1; cnt < playerDatabase.GetCount(); cnt++)
		{
			playerData = playerDatabase.GetDataByKey(cnt.ToString());

			Debug.Log(string.Format("PlayerData_{0} : Key = {1}, Level = {2}, Hp = {3}, Exp = {4}",
			                        cnt, playerData.Key, playerData.Level, playerData.Hp, playerData.Exp));
		}
	}


	private void PrintMonsterData()
	{
		MonsterDatabase monsterDatabase = m_databaseManager.GetDatabase<MonsterDatabase>();
		MonsterData monsterData = null;
		
		for(int cnt = 1; cnt < monsterDatabase.GetCount(); cnt++)
		{
			monsterData = monsterDatabase.GetDataByKey(cnt.ToString());

			Debug.Log(string.Format("MonsterData_{0} : Key = {1}, Name = {2}, Hp = {3}",
			                        cnt, monsterData.Key, monsterData.Name, monsterData.Hp));
		}
	}


	private void PrintWeaponData()
	{
		WeaponDatabase weaponDatabase = m_databaseManager.GetDatabase<WeaponDatabase>();
		WeaponData weaponData = null;
		
		for(int cnt = 1; cnt < weaponDatabase.GetCount(); cnt++)
		{
			weaponData = weaponDatabase.GetDataByKey(cnt.ToString());
			
			Debug.Log(string.Format("MonsterData_{0} : Key = {1}, Name = {2}",
			                        cnt, weaponData.Key, weaponData.Name));

			for(int lv = 0; lv < weaponData.Atk.Length; lv++)
			{
				Debug.Log(string.Format("Lv.{0}, Atk = {1}", lv + 1, weaponData.Atk[lv]));
			}

			for(int lv = 0; lv < weaponData.Rarity.Length; lv++)
			{
				Debug.Log(string.Format("Lv.{0}, Rarity = {1}", lv + 1, weaponData.Rarity[lv]));
			}
		}
	}
}
