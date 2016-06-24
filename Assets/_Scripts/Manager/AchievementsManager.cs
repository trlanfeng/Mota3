using UnityEngine;
using System.Collections;
//true代表成就已完成,false代表未完成
public class AchievementsManager : MonoBehaviour
{
    private string chengjiu;
    private JSONObject JSONO;
    private UIManager UM;
    void Awake()
    {
        if (!PlayerPrefs.HasKey("chengjiu"))
        {
            TextAsset chengjiuText = Resources.Load("chengjiu") as TextAsset;
            chengjiu = chengjiuText.text;
            JSONO = new JSONObject(chengjiu);
            PlayerPrefs.SetString("chengjiu", JSONO.Print());
        }
        else
        {
            chengjiu = PlayerPrefs.GetString("chengjiu");
        }
        JSONO = new JSONObject(chengjiu);
        UM = this.GetComponent<UIManager>();
    }
    public void checkChengjiu(int id)
    {
        bool hasGet = false;
        JSONO.GetField("chengjiu")[id].GetField(ref hasGet, "isGet");
        print("成就ID：" + id.ToString());
        print(hasGet);
        if (hasGet == false)
        {
            getChengjiu(id);
        }
    }
    public void getChengjiu(int id)
    {
        JSONO.GetField("chengjiu")[id].SetField("isGet", true);
        chengjiu = JSONO.Print();
        PlayerPrefs.SetString("chengjiu", chengjiu);
        string content = "";
        JSONO.GetField("chengjiu")[id].GetField(ref content,"content");
        UM.tipContent = "获得成就：" + content;
        UM.tipTime = 3f;
    }
}
