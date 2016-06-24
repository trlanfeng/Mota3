using UnityEngine;
public class Key : MonoBehaviour {
    public int key_yellow;
    public int key_blue;
    public int key_red;
    public string tip;
    void Start()
    {
        if (key_yellow != 0 && key_blue == 0 && key_red==0)
        {
            tip = "黄钥匙+" + key_yellow;
        }
        if (key_blue != 0 && key_yellow == 0 && key_red == 0)
        {
            tip = "蓝钥匙+" + key_blue;
        }
        if (key_red != 0 && key_blue == 0 && key_yellow == 0)
        {
            tip = "红钥匙+" + key_red;
        }
        if (key_red != 0 && key_blue != 0 && key_yellow != 0)
        {
            tip = "黄钥匙+" + key_yellow + "，";
            tip += "蓝钥匙+" + key_blue + "，";
            tip += "红钥匙+" + key_red;
        }
    }
}
