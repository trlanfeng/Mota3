using UnityEngine;

public class Game : MonoBehaviour
{
    public static Game instance = null;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
    public GameDataManager GDM;

    void Start()
    {
        if (PlayerPrefs.GetInt("currentSaveData") != 0)
        {
            int x = PlayerPrefs.GetInt("currentSaveData");
            if (PlayerPrefs.GetInt("hasSaveData" + x.ToString()) != 0)
            {
                GDM.LoadGame();
            }
            else
            {
                Application.LoadLevel("0");
            }
        }
        else
        {
            Application.LoadLevel("0");
        }
    }
}
