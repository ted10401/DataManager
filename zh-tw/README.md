# DataManager

DataManager 是一個簡易的本地資料庫工具，依據企劃內容來自動化產出相對應的程式碼

## 支援資料型態
* bool
* float
* int
* string
* List\<string\>

## Excel 資料範例
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

轉換後類別
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

轉換後類別
```C#
public class MonsterData
{
	public string Key;
	public string Name;
	public int Hp;
}
```

## 自動化腳本生成流程
1. 使用 Excel 工具編輯資料表內容
2. 將資料表匯出成 .csv 格式
3. 將 .csv 檔案放入 DataTool/Resources/CsvResources/ 目錄下
4. 點選 TEDTool/Data/Generate Script

## 使用範例
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