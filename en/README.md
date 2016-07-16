# DataManager

DataManager is a simple client database tool, depending on the contents to generate the script automatically.

## Support data type
* bool
* float
* int
* string
* List\<string\>

## Excel Example
### Player.csv
|string/Key|int/Level|int/Hp|int/Exp|
|:-------------:|:-------------:|:-------------:|:-------------:|
|0	|1	|10	|10|
|1	|2	|20	|20|
|2	|3	|30	|30|
|3	|4	|40	|40|
|4	|5	|50	|50|
|5	|6	|60	|60|
|6	|7	|70	|70|
|7	|8	|80	|80|
|8	|9	|90	|90|
|9	|10	|100	|100|

The class after generate automatically
```C#
public class PlayerData
{
	public string Key;
	public int Level;
	public int Hp;
	public int Exp;
}
```
### Monster.csv
|string/Key|string/Name|int/Hp|
|:-------------:|:-------------:|:-------------:|
|0	|Monster_1	|10	|
|1	|Monster_2	|20	|
|2	|Monster_3	|30	|
|3	|Monster_4	|40	|
|4	|Monster_5	|50	|
|5	|Monster_6	|60	|
|6	|Monster_7	|70	|
|7	|Monster_8	|80	|
|8	|Monster_9	|90	|
|9	|Monster_10	|100	|

The class after generate automatically
```C#
public class MonsterData
{
	public string Key;
	public string Name;
	public int Hp;
}
```

## Generate script workflow
1. Use Excel to edit the database content
2. Export Excel database to .csv file
3. Put .csv to DataTool/Resources/CsvResources/
4. Click TEDTool/Data/Generate Script

## Method Example
```C#
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
```