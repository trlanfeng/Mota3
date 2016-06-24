using UnityEngine;
using System.Collections;

public class Test : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            if (PlayerPrefs.HasKey("floor1"))
            {
                tk2dTileMap tk2dTM = GameObject.Find("TileMap").GetComponent<tk2dTileMap>();
                TileMapLoadSave TMLS = new TileMapLoadSave();
                string data = PlayerPrefs.GetString("floor1");
                print("load -> " + data);
                TMLS.LoadTileMap(tk2dTM, 1, data);
            }
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            tk2dTileMap tk2dTM = GameObject.Find("TileMap").GetComponent<tk2dTileMap>();
            TileMapLoadSave TMLS = new TileMapLoadSave();
            string data = TMLS.SaveTileMap(tk2dTM,1);
            PlayerPrefs.SetString("floor1", data);
            print("save -> " + data);
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            tk2dTileMap tk2dTM = GameObject.Find("TileMap").GetComponent<tk2dTileMap>();
            TileMapLoadSave TMLS = new TileMapLoadSave();
            TMLS.LoadCVSMap(tk2dTM, 1, "text1");
        }
    }
}
