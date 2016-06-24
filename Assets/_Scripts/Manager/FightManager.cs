using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FightManager : MonoBehaviour
{
    /*
    public Image Panel_Fight;
    private Image EnemyImage;
    private Text EnemyShengming;
    private Text EnemyGongji;
    private Text EnemyFangyu;
    private Image PlayerImage;
    private Text PlayerShengming;
    private Text PlayerGongji;
    private Text PlayerFangyu;

    private HeroAttributes HA;

    void Start()
    {
        Panel_Fight.gameObject.SetActive(false);
        HA = GameObject.Find("Hero").GetComponent<HeroAttributes>();
    }

    public void OpenFight(Enemy enemy)
    {
        Panel_Fight.gameObject.SetActive(true);

        EnemyImage = GameObject.Find("Image_Enemy").GetComponent<Image>();
        EnemyShengming = GameObject.Find("Info_Enemy/shengming").GetComponent<Text>();
        EnemyGongji = GameObject.Find("Info_Enemy/gongji").GetComponent<Text>();
        EnemyFangyu = GameObject.Find("Info_Enemy/fangyu").GetComponent<Text>();
        PlayerImage = GameObject.Find("Image_Player").GetComponent<Image>();
        PlayerShengming = GameObject.Find("Info_Player/shengming").GetComponent<Text>();
        PlayerGongji = GameObject.Find("Info_Player/shengming").GetComponent<Text>();
        PlayerFangyu = GameObject.Find("Info_Player/shengming").GetComponent<Text>();

        tk2dSprite enemySprite = enemy.gameObject.GetComponent<tk2dSprite>();
        tk2dSpriteDefinition SD = enemySprite.CurrentSprite;
        print(SD.positions[0]);
        //EnemyImage.sprite = Sprite.Create(tt[0] as Texture2D,new Rect(0,0,50,50),new Vector2(0,0));
        //左侧怪物图像
        EnemyShengming.text = enemy.shengming.ToString();
        EnemyGongji.text = enemy.gongji.ToString();
        EnemyFangyu.text = enemy.fangyu.ToString();

        PlayerShengming.text = HA._shengming.ToString();
        PlayerGongji.text = HA._gongji.ToString();
        PlayerFangyu.text = HA._fangyu.ToString();

    }
     * */
}
