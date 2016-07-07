# DataManager

DataManager 是一個簡易的本地資料庫工具，依據企劃內容來自動化產出相對應的程式嗎

## Excel 資料範例
### Level.csv
|string/Key|int/Level|int/Hp|int/Exp|
|:-------------:|:-------------:|:-------------:|:-------------:|
|1	|1	|10	|10|
|2	|2	|20	|20|
|3	|3	|30	|30|
|4	|4	|40	|40|
|5	|5	|50	|50|
|6	|6	|60	|60|
|7	|7	|70	|70|
|8	|8	|80	|80|
|9	|9	|90	|90|
|10	|10	|100	|100|

轉換後的類別為
```C#
public class LevelData
{
	public string Key;
	public int Level;
	public int Hp;
	public int Exp;
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
using TEDTool.Data;

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
```