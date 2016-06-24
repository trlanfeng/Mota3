using UnityEngine;
public class Yaoshui : MonoBehaviour
{
    public int shengming;
    public string tip;
    void Start()
    {
        if (shengming != 0)
            tip = "生命值+" + shengming;
    }
}
