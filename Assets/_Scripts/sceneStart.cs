using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class sceneStart : MonoBehaviour
{

    private float position_left;
    private float position_center;
    private float position_right;
    private float position_up;
    private float position_down;

    public Text saveData1;
    public Text saveData2;
    public Text saveData3;

    private JSONObject JSONO;
    private string chengjiu;
    public GameObject chengjiuItem;
    public GameObject chengjiuContainer;

    public RectTransform panelStart;
    public RectTransform panelAchievements;

    RectTransform CanvasRT;
    public RectTransform panelMenu;

    void Start()
    {
        CanvasRT = GameObject.Find("Canvas").GetComponent<RectTransform>();
        position_left = CanvasRT.rect.width * -1;
        position_center = 0;
        position_right = CanvasRT.rect.width;
        position_up = CanvasRT.rect.height;
        position_down = CanvasRT.rect.height * -1;

        panelStart.anchoredPosition = new Vector2(panelStart.anchoredPosition.x, 0);
        panelAchievements.anchoredPosition = new Vector2(panelAchievements.anchoredPosition.x, position_down);

        PlayerPrefs.DeleteKey("currentSaveData");
        PlayerPrefs.DeleteKey("isLoadGame");

        if (PlayerPrefs.HasKey("hasSaveData1") && (PlayerPrefs.GetInt("hasSaveData1") == 1))
        {
            saveData1.text = "LV:" + PlayerPrefs.GetInt("SaveData1/dengji") + "  /  " + "Floor:" + PlayerPrefs.GetInt("SaveData1/currentFloor");
        }
        else
        {
            saveData1.text = "空";
        }
        if (PlayerPrefs.HasKey("hasSaveData2") && (PlayerPrefs.GetInt("hasSaveData2") == 1))
        {
            saveData2.text = "LV:" + PlayerPrefs.GetInt("SaveData2/dengji") + "  /  " + "Floor:" + PlayerPrefs.GetInt("SaveData2/currentFloor");
        }
        else
        {
            saveData2.text = "空";
        }
        if (PlayerPrefs.HasKey("hasSaveData3") && (PlayerPrefs.GetInt("hasSaveData3") == 1))
        {
            saveData3.text = "LV:" + PlayerPrefs.GetInt("SaveData3/dengji") + "  /  " + "Floor:" + PlayerPrefs.GetInt("SaveData3/currentFloor");
        }
        else
        {
            saveData3.text = "空";
        }
        showAchievements();
    }
    public void buttonDefault()
    {
        panelMenu.DOAnchorPosX(0, 0.2f);
    }
    public void buttonStart()
    {
        panelMenu.DOAnchorPosX(position_left, 0.2f);
    }
    public void buttonSetting()
    {
        panelMenu.DOAnchorPosX(position_right, 0.2f);
    }
    public void startGame(int saveDataID)
    {
        PlayerPrefs.SetInt("currentSaveData", saveDataID);
        SceneManager.LoadScene("init");
    }

    public void buttonEnd()
    {
        Application.Quit();
    }
    void showAchievements()
    {
        if (PlayerPrefs.HasKey("chengjiu"))
        {
            chengjiu = PlayerPrefs.GetString("chengjiu");
            JSONO = new JSONObject(chengjiu);
            for (int i = 0; i < JSONO.GetField("chengjiu").Count; i++)
            {
                JSONObject one = JSONO.GetField("chengjiu")[i];
                bool isGet = false;
                one.GetField(ref isGet, "isGet");
                if (isGet == true)
                {
                    GameObject GOone = Instantiate(chengjiuItem);
                    GOone.transform.SetParent(chengjiuContainer.transform);
                    GOone.transform.localScale = new Vector3(1, 1, 1);
                    string name = string.Empty;
                    string comment = string.Empty;
                    one.GetField(ref name, "content");
                    one.GetField(ref comment, "comment");
                    GOone.transform.FindChild("Text").GetComponent<Text>().text = name;
                    GOone.transform.FindChild("Text 1").GetComponent<Text>().text = comment;
                }
            }
            int height = chengjiuContainer.transform.childCount * 80;
            RectTransform chengjiuContainerRT = chengjiuContainer.GetComponent<RectTransform>();
            chengjiuContainerRT.sizeDelta = new Vector2(400f, height);
        }
    }
    public void showAchievementsPanel()
    {
        panelStart.DOAnchorPosY(position_up, 0.2f);
        panelAchievements.DOAnchorPosY(position_center, 0.2f);
    }
    public void hideAchievementsPanel()
    {
        panelStart.DOAnchorPosY(position_center, 0.2f);
        panelAchievements.DOAnchorPosY(position_down, 0.2f);
    }
}
