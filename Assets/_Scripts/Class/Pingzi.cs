using UnityEngine;
public class Pingzi : MonoBehaviour
{
    public int gongji;
    public int fangyu;
    public int shengming;
    public bool isDouble;
    public int doubleNum;
    public string tip;
    void Start()
    {
        if (gongji != 0 && fangyu == 0 && shengming == 0)
        {
            tip = "攻击+" + gongji;
        }
        if (fangyu != 0 && gongji == 0 && shengming == 0)
        {
            tip = "防御+" + fangyu;
        }
        if (shengming != 0 && gongji == 0 && fangyu == 0)
        {
            tip = "生命值+" + shengming;
        }
        if (gongji != 0 && fangyu != 0 && shengming != 0)
        {
            tip = "生命值+" + shengming + "，";
            tip += "攻击+" + gongji + "，";
            tip += "防御+" + fangyu;
        }
    }
}
