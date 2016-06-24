using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class DialogManager : MonoBehaviour
{
    //常用组件
    private HeroAttributes HA;
    //提示内容
    public string tipContent = "";
    public string infoDabuguo;

	void Start ()
    {

	}

    public void jia_shengming(int metadata)
    {
        HA._shengming += metadata;
    }
    public void jia_gongji(int metadata)
    {
        HA._gongji += metadata;
    }
    public void jia_fangyu(int metadata)
    {
        HA._fangyu += metadata;
    }
    public void jia_dengji(int metadata)
    {
        HA._dengji += metadata;
        HA._gongji += metadata * 3;
        HA._fangyu += metadata * 3;
        HA._shengming += metadata * 450;
    }

}
