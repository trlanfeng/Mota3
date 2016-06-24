using UnityEngine;
using System.Collections;

public class level0 : MonoBehaviour {
    public GameObject jingling;
    //对话完成后，移除精灵
	public void clearJingLing()
    {
        tk2dTileMap tm = GameObject.Find("TileMap").GetComponent<tk2dTileMap>();
        int x, y;
        tm.GetTileAtPosition(jingling.transform.position, out x, out y);
        tm.ClearTile(x, y, 1);
        Destroy(jingling);
    }
    //惹祸上身，游戏结束，回到开始界面
    public void gameover()
    {
        Destroy(GameObject.Find("Game"));
        Application.LoadLevel("start");
    }
}
