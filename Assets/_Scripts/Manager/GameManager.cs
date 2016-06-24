using UnityEngine;

public class GameManager : MonoBehaviour
{

    public int currentFloor = 0;
    public int maxFloor = 0;
    public GameObject globalGameObject;
    public GameObject[] floorGO;

    private UIManager UM;
    public Transform heroTransform;
    private string floorArrow = "";
    private GameObject hero;
	private GameDataManager GDM;

    void Start()
    {
        hero = GameObject.Find("Hero").gameObject;
        UM = this.GetComponent<UIManager>();
		GDM = this.GetComponent<GameDataManager>();
    }
    public void changeFloor(int floor,string checkUpDown = "up")
    {
        //if (currentFloor != floor)
        {
            floorArrow = checkUpDown;
            Application.LoadLevel(floor.ToString());
            currentFloor = floor;
            UM.floorTime = 2f;
            UM.floorText.text = "第  " + currentFloor + "  层";
            UM.floorPanel.gameObject.SetActive(true);
            if (currentFloor > maxFloor)
            {
                maxFloor = currentFloor;
            }
        }
    }
    public void upstair()
    {
        heroTransform.position = GameObject.FindGameObjectWithTag("StairDown").gameObject.transform.position;
        heroTransform.gameObject.SetActive(true);
    }
    public void downstair()
    {
        heroTransform.position = GameObject.Find("StairUp").gameObject.transform.position;
        heroTransform.gameObject.SetActive(true);
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            if (UM.state == "")
            {
                UM.state = "menu";
            }
            if (UM.state == "menu")
            {
                UM.state = "";
            }
        }
    }
    public void GameOver(string jieju)
    {
        switch (jieju)
        {
            case "siyugongzhu":
                PlayerPrefs.SetString("jieju", "siyugongzhu");
                Application.LoadLevel("end");
                break;
            case "jiehun":
                PlayerPrefs.SetString("jieju", "jiehun");
                Application.LoadLevel("end");
                break;
            case "dabaiBOSS":
                PlayerPrefs.SetString("jieju", "dabaiBOSS");
                Application.LoadLevel("end");
                break;
            case "dabaiBBOSS":
                PlayerPrefs.SetString("jieju", "dabaiBBOSS");
                Application.LoadLevel("end");
                break;
            case "siyuBBOSS":
                PlayerPrefs.SetString("jieju", "siyuBBOSS");
                Application.LoadLevel("end");
                break;
        }
    }

    public Vector3 getHeroPosition()
    {
        if (floorArrow == "up")
        {
            heroTransform.position = GameObject.FindGameObjectWithTag("StairDown").gameObject.transform.position;
        }
        else if (floorArrow == "down")
        {
            heroTransform.position = GameObject.FindGameObjectWithTag("StairUp").gameObject.transform.position;
        }
        return heroTransform.position;
    }
}
