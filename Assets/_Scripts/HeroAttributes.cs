using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HeroAttributes : MonoBehaviour {

    //玩家属性
    public int _dengji;
    public int _jingyan;
    public int _jinbi;
    public int _shengming;
    public int _gongji;
    public int _fangyu;
    public int _key_yellow;
    public int _key_blue;
    public int _key_red;
    public bool _daoju_tujian = false;
    public bool _daoju_feixing = false;
    //玩家属性UI
    public Text dengji;
    public Text jingyan;
    public Text jinbi;
    public Text shengming;
    public Text gongji;
    public Text fangyu;
    public Text key_yellow;
    public Text key_blue;
    public Text key_red;
    public Text floor;
    //玩家道具
	public bool hasTujian;
	public bool hasFeixing;
	public bool hasXie;

    private GameManager GM;

	void Start () {
        GM = this.gameObject.GetComponent<Hero>().GameController.GetComponent<GameManager>();
	}

	void Update () {
        dengji.text = _dengji.ToString();
        jingyan.text = _jingyan.ToString();
        jinbi.text = _jinbi.ToString();
        shengming.text = _shengming.ToString();
        gongji.text = _gongji.ToString();
        fangyu.text = _fangyu.ToString();
        key_yellow.text = _key_yellow.ToString();
        key_blue.text = _key_blue.ToString();
        key_red.text = _key_red.ToString();
        floor.text = "第  " + GM.currentFloor + "  层";
        /*
        DiaQEngine.Instance.graphManager.SetMetaDataValue("jinbi",_jinbi);
        DiaQEngine.Instance.graphManager.SetMetaDataValue("jingyan", _jingyan);
        DiaQEngine.Instance.graphManager.SetMetaDataValue("key_yellow", _key_yellow);
        DiaQEngine.Instance.graphManager.SetMetaDataValue("key_blue", _key_blue);
        DiaQEngine.Instance.graphManager.SetMetaDataValue("key_red", _key_red);
         * */
        LevelUp();
    }

    public void LevelUp()
    {
        if (_jingyan >= 100)
        {
            _jingyan -= 100;
            _dengji += 1;
            _gongji += 3;
            _fangyu += 3;
            _shengming += 300;
        }
    }
    public void plusJinbi(int number)
    {
        _jinbi += number;
        AchievementsManager AmM = GameObject.Find("GameController").GetComponent<AchievementsManager>();
        if (_jinbi >= 100 && _jinbi <500)
        {
            AmM.checkChengjiu(0);
        }
        if (_jinbi >= 500)
        {
            AmM.checkChengjiu(1);
        }
        if (_jinbi >= 1000)
        {
            AmM.checkChengjiu(2);
        }
        if (_jinbi >= 2000)
        {
            AmM.checkChengjiu(3);
        }
        if (_jinbi >= 5000)
        {
            AmM.checkChengjiu(4);
        }
        if (_jinbi >= 10000)
        {
            AmM.checkChengjiu(5);
        }
    }
}
