using UnityEngine;
using System;

public class GameDataManager : MonoBehaviour
{

    private HeroAttributes HA;
    private GameManager GM;
    private UIManager UM;

    void Awake()
    {
        clearTempData();
        GameObject hero = GameObject.Find("Hero").gameObject;
        HA = hero.GetComponent<HeroAttributes>();
        GM = this.GetComponent<GameManager>();
        UM = this.GetComponent<UIManager>();
	}
	
	public void SaveGame()
    {
		saveCurrentFloorToTemp();
		if (PlayerPrefs.GetInt("currentSaveData") == 0 || PlayerPrefs.GetInt("currentSaveData") > 3)
		{
			PlayerPrefs.SetInt("currentSaveData",1);
		}
        PlayerPrefs.SetInt("hasSaveData" + PlayerPrefs.GetInt("currentSaveData").ToString(), 1);
		string saveDataPre = "SaveData"+PlayerPrefs.GetInt("currentSaveData").ToString()+"/";
		//保存DiaQ数据
		//string data = DiaQEngine.Instance.GetSaveData();
		//PlayerPrefs.SetString(saveDataPre+"DIAQ", data);
		//保存Hero数据
		PlayerPrefs.SetInt(saveDataPre+"dengji",HA._dengji);
		PlayerPrefs.SetInt(saveDataPre+"jinbi",HA._jinbi);
		PlayerPrefs.SetInt(saveDataPre+"jingyan",HA._jingyan);
		PlayerPrefs.SetInt(saveDataPre+"shengming",HA._shengming);
		PlayerPrefs.SetInt(saveDataPre+"gongji",HA._gongji);
		PlayerPrefs.SetInt(saveDataPre+"fangyu",HA._fangyu);
		PlayerPrefs.SetInt(saveDataPre+"key_yellow",HA._key_yellow);
		PlayerPrefs.SetInt(saveDataPre+"key_blue",HA._key_blue);
		PlayerPrefs.SetInt(saveDataPre+"key_red",HA._key_red);
		PlayerPrefs.SetString(saveDataPre+"daoju_tujian",HA._daoju_tujian.ToString());
		PlayerPrefs.SetString(saveDataPre+"daoju_feixing",HA._daoju_feixing.ToString());
		PlayerPrefs.SetInt(saveDataPre+"maxFloor",GM.maxFloor);
		PlayerPrefs.SetInt(saveDataPre+"currentFloor",GM.currentFloor);
		PlayerPrefs.SetFloat(saveDataPre+"heroTransformX",HA.gameObject.transform.position.x);
		PlayerPrefs.SetFloat(saveDataPre+"heroTransformY",HA.gameObject.transform.position.y);
		PlayerPrefs.SetFloat(saveDataPre+"heroTransformZ",HA.gameObject.transform.position.z);
		//保存地图数据
        for (int i = 0; i < GM.maxFloor + 1; i++)
        {
			PlayerPrefs.SetString(saveDataPre+"Floor"+i, PlayerPrefs.GetString("tempFloor"+i));
        }
		PlayerPrefs.Save();
		//提示保存结果
        UM.state = "";
        UM.tipContent = "保存成功";
        UM.tipTime = 3;
    }

    public void LoadGame()
    {
		if (PlayerPrefs.GetInt("currentSaveData") == 0 || PlayerPrefs.GetInt("currentSaveData") > 3)
		{
			PlayerPrefs.SetInt("currentSaveData",1);
		}
		string saveDataPre = "SaveData"+PlayerPrefs.GetInt("currentSaveData").ToString()+"/";
		HA._dengji = PlayerPrefs.GetInt(saveDataPre+"dengji");
		HA._jinbi = PlayerPrefs.GetInt(saveDataPre+"jinbi");
		HA._jingyan = PlayerPrefs.GetInt(saveDataPre+"jingyan");
		HA._shengming = PlayerPrefs.GetInt(saveDataPre+"shengming");
		HA._gongji = PlayerPrefs.GetInt(saveDataPre+"gongji");
		HA._fangyu = PlayerPrefs.GetInt(saveDataPre+"fangyu");
		HA._key_yellow = PlayerPrefs.GetInt(saveDataPre+"key_yellow");
		HA._key_blue = PlayerPrefs.GetInt(saveDataPre+"key_blue");
		HA._key_red = PlayerPrefs.GetInt(saveDataPre+"key_red");
		HA._daoju_tujian =  Convert.ToBoolean(PlayerPrefs.GetString(saveDataPre+"daoju_tujian"));
		HA._daoju_feixing = Convert.ToBoolean(PlayerPrefs.GetString(saveDataPre+"daoju_feixing"));
		GM.maxFloor = PlayerPrefs.GetInt(saveDataPre+"maxFloor");
		GM.currentFloor = PlayerPrefs.GetInt(saveDataPre+"currentFloor");
		float positionX = PlayerPrefs.GetFloat(saveDataPre+"heroTransformX");
		float positionY = PlayerPrefs.GetFloat(saveDataPre+"heroTransformY");
		float positionZ = PlayerPrefs.GetFloat(saveDataPre+"heroTransformZ");
		Vector3 position = new Vector3(positionX,positionY,positionZ);
		HA.gameObject.transform.position = position;
		/*
		if (PlayerPrefs.HasKey(saveDataPre+"DIAQ"))
        {
			string data = PlayerPrefs.GetString(saveDataPre+"DIAQ", "");
            DiaQEngine.Instance.RestoreFromSaveData(data);
        }*/
		for (int i = 0; i < PlayerPrefs.GetInt(saveDataPre+"maxFloor") + 1; i++)
        {
			PlayerPrefs.SetString("tempFloor"+i, PlayerPrefs.GetString(saveDataPre+"Floor"+i));
        }
		GM.changeFloor(GM.currentFloor);
		UM.state = "";
        UM.tipContent = "读取成功";
        UM.tipTime = 3;
    }

    public bool checkGameData()
    {
        int i = PlayerPrefs.GetInt("currentSaveData");
        if (PlayerPrefs.HasKey("hasSaveData"+i) && (PlayerPrefs.GetInt("hasSaveData"+i) == 1))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

	public void saveCurrentFloorToTemp()
	{
		tk2dTileMap tk2dTM = GameObject.Find("TileMap").GetComponent<tk2dTileMap>();
		TileMapLoadSave TMLS = new TileMapLoadSave();
		string mapdata = TMLS.SaveTileMap(tk2dTM,1);
		PlayerPrefs.SetString("tempFloor"+Application.loadedLevelName, mapdata);
	}
	public void loadFloor()
	{
		string data = PlayerPrefs.GetString("tempFloor"+Application.loadedLevelName);
		if (String.IsNullOrEmpty(data)) return;
		tk2dTileMap tk2dTM = GameObject.Find("TileMap").GetComponent<tk2dTileMap>();
		TileMapLoadSave TMLS = new TileMapLoadSave();
		TMLS.LoadTileMap(tk2dTM, 1, data);
	}
    public void restartGame()
    {
        clearTempData();
        if (PlayerPrefs.GetInt("currentSaveData") == 0 || PlayerPrefs.GetInt("currentSaveData") > 3)
        {
            PlayerPrefs.SetInt("currentSaveData", 1);
        }
        PlayerPrefs.SetInt("hasSaveData" + PlayerPrefs.GetInt("currentSaveData").ToString(), 1);
        string saveDataPre = "SaveData" + PlayerPrefs.GetInt("currentSaveData").ToString() + "/";
        //保存DiaQ数据
        //string data = DiaQEngine.Instance.GetSaveData();
        //PlayerPrefs.SetString(saveDataPre+"DIAQ", data);
        //保存Hero数据
        PlayerPrefs.DeleteKey("hasSaveData" + PlayerPrefs.GetInt("currentSaveData"));
        PlayerPrefs.DeleteKey(saveDataPre + "dengji");
        PlayerPrefs.DeleteKey(saveDataPre + "jinbi");
        PlayerPrefs.DeleteKey(saveDataPre + "jingyan");
        PlayerPrefs.DeleteKey(saveDataPre + "shengming");
        PlayerPrefs.DeleteKey(saveDataPre + "gongji");
        PlayerPrefs.DeleteKey(saveDataPre + "fangyu");
        PlayerPrefs.DeleteKey(saveDataPre + "key_yellow");
        PlayerPrefs.DeleteKey(saveDataPre + "key_blue");
        PlayerPrefs.DeleteKey(saveDataPre + "key_red");
        PlayerPrefs.DeleteKey(saveDataPre + "daoju_tujian");
        PlayerPrefs.DeleteKey(saveDataPre + "daoju_feixing");
        PlayerPrefs.DeleteKey(saveDataPre + "maxFloor");
        PlayerPrefs.DeleteKey(saveDataPre + "currentFloor");
        PlayerPrefs.DeleteKey(saveDataPre + "heroTransformX");
        PlayerPrefs.DeleteKey(saveDataPre + "heroTransformY");
        PlayerPrefs.DeleteKey(saveDataPre + "heroTransformZ");
        HA._dengji = 1;
        HA._jingyan = 0;
        HA._jinbi = 0;
        HA._shengming = 1000;
        HA._gongji = 10;
        HA._fangyu = 10;
        HA._key_yellow = 1;
        HA._key_blue = 1;
        HA._key_red = 1;
        HA._daoju_tujian = false;
        HA._daoju_feixing = false;
        HA.hasFeixing = false;
        HA.hasTujian = false;
        HA.hasXie = false;
        //保存地图数据
        for (int i = 0; i < GM.maxFloor + 1; i++)
        {
            PlayerPrefs.DeleteKey(saveDataPre + "Floor" + i);
        }
        GM.changeFloor(0);
        //提示保存结果
        UM.state = "";
        UM.tipContent = "已重新开始游戏";
        UM.tipTime = 3;
    }
	public void clearTempData()
	{
        for (int i = 0; i < 60; i++)
        {
            if (PlayerPrefs.HasKey("tempFloor" + i.ToString()))
            {
                PlayerPrefs.DeleteKey("tempFloor" + i.ToString());
            }
        }
	}
}
