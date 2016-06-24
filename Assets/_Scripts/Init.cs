using UnityEngine;

public class Init : MonoBehaviour {
    public GameDataManager GDM;
	void Awake()
	{
		DontDestroyOnLoad(GameObject.Find("Game").gameObject);
	}
	void Start()
	{
		if (PlayerPrefs.GetInt("currentSaveData") != 0)
		{
			int x = PlayerPrefs.GetInt("currentSaveData");
			if (PlayerPrefs.GetInt("hasSaveData"+x.ToString()) != 0)
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
