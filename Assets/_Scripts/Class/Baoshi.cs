using UnityEngine;
public class Baoshi : MonoBehaviour {
    public int gongji;
    public int fangyu;
    public string tip;
    void Start()
    {
        if (gongji != 0 && fangyu == 0)
        {
            tip = "攻击+" + gongji;
        }
        if (fangyu != 0 && gongji == 0)
        {
            tip = "防御+" + fangyu;
        }
        if (gongji != 0 && fangyu != 0)
        {
            tip = "攻击+" + gongji + "，";
            tip += "防御+" + fangyu;
        }
    }
}
