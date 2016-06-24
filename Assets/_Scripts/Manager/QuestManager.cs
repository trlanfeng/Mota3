using UnityEngine;
using System.Collections;
//using Rotorz.Tile;

public class QuestManager : MonoBehaviour {
    /*
    public HeroAttributes HA;
    private GameDataManager GDM;

    void Start()
    {
        GDM = this.GetComponent<GameDataManager>();
    }

    public void GiveRewards(DiaQuest quest)
    {
        string rewardType = "";
        for (int i = 0; i < quest.rewards.Count; i++)
        {
            rewardType = quest.rewards[i].keyString;
            switch (rewardType)
            {
                case "key_yellow":
                    HA._key_yellow += quest.rewards[i].value;
                    break;
                case "key_blue":
                    HA._key_blue += quest.rewards[i].value;
                    break;
                case "key_red":
                    HA._key_red += quest.rewards[i].value;
                    break;
                case "jinbi":
                    HA._jinbi += quest.rewards[i].value;
                    break;
                case "jingyan":
                    HA._jingyan += quest.rewards[i].value;
                    break;
                case "gongji":
                    HA._gongji += quest.rewards[i].value;
                    break;
                case "fangyu":
                    HA._fangyu += quest.rewards[i].value;
                    break;
                default:
                    break;
            }
        }
    }
    public void moveGongzhu()
    {
        GameObject currentFloor = GameObject.Find("Floor20").gameObject;
        TileSystem ts_object = currentFloor.GetComponent<TileSystem>();
        TileData gongzhu = ts_object.GetTile(0,5);
        ts_object.SetTile(2,5,gongzhu);
        gongzhu.Clear();
        GameObject goGongzhu = GameObject.Find("Floor20/chunk_0_0/npc-02_12").gameObject;
        Vector3 gzposition = new Vector3(5.5f + 20 * 12,-2.5f,0);
        goGongzhu.transform.position = gzposition;
    }
    public void moveGongzhuMen()
    {
        GameObject currentFloor = GameObject.Find("Floor20").gameObject;
        TileSystem ts_object = currentFloor.GetComponent<TileSystem>();

        TileData men = ts_object.GetTile(1, 5);
        men.Clear();
        GameObject gomen = GameObject.Find("Floor20/chunk_0_0/door-01_3").gameObject;
        GameObject.Destroy(gomen);

        TileData gongzhu = ts_object.GetTile(0, 5);
        ts_object.SetTile(1, 5, gongzhu);
        gongzhu.Clear();
        GameObject goGongzhu = GameObject.Find("Floor20/chunk_0_0/npc-02_12").gameObject;
        Vector3 gzposition = new Vector3(5.5f + 20 * 12, -1.5f, 0);
        goGongzhu.transform.position = gzposition;
    }
    public void daGongzhu()
    {
        //daGongZhu
        //True : 打败公主
        //False: 打不过公主
        int gongji = 350;
        int fangyu = 200;
        int shengming = 3000;
        if (gongji > HA._fangyu)
        {
            int shanghai = HA._gongji - fangyu;
            if (shanghai > shengming)
            {
                DiaQEngine.Instance.graphManager.SetMetaDataValue("daGongzhu", true);
                PlayerPrefs.SetInt("gongzhuDead", 1);
            }
            else
            {
                int cishu = (int)Mathf.Floor(shengming / shanghai);
                int zongshanghai = 0;
                int shoushang = gongji - HA._fangyu;
                zongshanghai = shoushang * cishu;
                if (zongshanghai > HA._shengming)
                {
                    DiaQEngine.Instance.graphManager.SetMetaDataValue("daGongzhu", false);
                }
                else
                {
                    HA._shengming -= zongshanghai;
                    DiaQEngine.Instance.graphManager.SetMetaDataValue("daGongzhu", true);
                    PlayerPrefs.SetInt("gongzhuDead", 1);
                }
            }
        }
        else
        {
            DiaQEngine.Instance.graphManager.SetMetaDataValue("daGongzhu", true);
            PlayerPrefs.SetInt("gongzhuDead", 1);
        }
    }
    public void clearGongzhuGuai()
    {
        GameObject currentFloor = GameObject.Find("Floor20").gameObject;
        TileSystem ts_object = currentFloor.GetComponent<TileSystem>();
        //TileData gongzhu = ts_object.GetTile(2, 5);
        //gongzhu.Clear();
        GameObject goGongzhu = GameObject.Find("Floor20/chunk_0_0/npc-02_12").gameObject;
        GameObject.Destroy(goGongzhu);

        TileData men = ts_object.GetTile(1, 5);
        men.Clear();
        GameObject gomen = GameObject.Find("Floor20/chunk_0_0/door-01_3").gameObject;
        GameObject.Destroy(gomen);

        TileData qiang = ts_object.GetTile(9, 5);
        qiang.Clear();
        GameObject goqiang = GameObject.Find("Floor20/chunk_0_0/bg_1").gameObject;
        GameObject.Destroy(goqiang);

        GDM.sceneData[20][0, 5] = 1;
        GDM.sceneData[20][1, 5] = 1;
        GDM.sceneData[20][9, 5] = 1;
    }
    public void clearGongzhuGuai2()
    {
        GameObject currentFloor = GameObject.Find("Floor20").gameObject;
        TileSystem ts_object = currentFloor.GetComponent<TileSystem>();
        //TileData gongzhu = ts_object.GetTile(1, 5);
        //gongzhu.Clear();
        GameObject goGongzhu = GameObject.Find("Floor20/chunk_0_0/npc-02_12").gameObject;
        GameObject.Destroy(goGongzhu);

        TileData qiang = ts_object.GetTile(9, 5);
        qiang.Clear();
        GameObject goqiang = GameObject.Find("Floor20/chunk_0_0/bg_1").gameObject;
        GameObject.Destroy(goqiang);

        GDM.sceneData[20][0, 5] = 1;
        GDM.sceneData[20][1, 5] = 1;
        GDM.sceneData[20][9, 5] = 1;
    }
    public void clearGongzhuRen()
    {
        GameObject currentFloor = GameObject.Find("Floor20").gameObject;
        TileSystem ts_object = currentFloor.GetComponent<TileSystem>();
        TileData gongzhu = ts_object.GetTile(0, 5);
        gongzhu.Clear();
        GameObject goGongzhu = GameObject.Find("Floor20/chunk_0_0/npc-02_12").gameObject;
        GameObject.Destroy(goGongzhu);

        TileData men = ts_object.GetTile(1, 5);
        men.Clear();
        GameObject gomen = GameObject.Find("Floor20/chunk_0_0/door-01_3").gameObject;
        GameObject.Destroy(gomen);

        TileData qiang = ts_object.GetTile(9, 5);
        qiang.Clear();
        GameObject goqiang = GameObject.Find("Floor20/chunk_0_0/bg_1").gameObject;
        GameObject.Destroy(goqiang);

        PlayerPrefs.SetInt("gongzhuDead", 1);
        GDM.sceneData[20][0, 5] = 1;
        GDM.sceneData[20][1, 5] = 1;
        GDM.sceneData[20][9, 5] = 1;
    }
    public void clearGongzhuRen2()
    {
        GameObject currentFloor = GameObject.Find("Floor20").gameObject;
        TileSystem ts_object = currentFloor.GetComponent<TileSystem>();
        TileData gongzhu = ts_object.GetTile(0, 5);
        gongzhu.Clear();
        GameObject goGongzhu = GameObject.Find("Floor20/chunk_0_0/npc-02_12").gameObject;
        GameObject.Destroy(goGongzhu);

        TileData qiang = ts_object.GetTile(9, 5);
        qiang.Clear();
        GameObject goqiang = GameObject.Find("Floor20/chunk_0_0/bg_1").gameObject;
        GameObject.Destroy(goqiang);

        if (PlayerPrefs.HasKey("gongzhuDead"))
        {
            PlayerPrefs.DeleteKey("gongzhuDead");
        }

        GDM.sceneData[20][0, 5] = 1;
        GDM.sceneData[20][1, 5] = 1;
        GDM.sceneData[20][9, 5] = 1;
    }
    public void dakaimen()
    {
        GameObject currentFloor = GameObject.Find("Floor20").gameObject;
        TileSystem ts_object = currentFloor.GetComponent<TileSystem>();
        TileData men = ts_object.GetTile(1, 5);
        men.Clear();
        GameObject gomen = GameObject.Find("Floor20/chunk_0_0/door-01_3").gameObject;
        GameObject.Destroy(gomen);
        HA._shengming = 1;
        HA._gongji = 1;
        HA._fangyu = 1;
    }
    public void huodehushenfu()
    {
        DiaQEngine.Instance.graphManager.SetMetaDataValue("hasHushenfu", true);
    }
    public void shiquhushenfu()
    {
        DiaQEngine.Instance.graphManager.SetMetaDataValue("hasHushenfu", false);
    }
    public void mode_easy()
    {
        HA._shengming = 2000;
        HA._gongji = 100;
        HA._fangyu = 100;
        HA._key_yellow = 10;
        HA._key_blue = 10;
        HA._key_red = 10;
    }
     * */
}
