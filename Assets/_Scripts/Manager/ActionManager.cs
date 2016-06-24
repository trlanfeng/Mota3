using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;

public class ActionManager : MonoBehaviour
{

    private AudioManager AuM;
    private HeroAttributes HA;
    private GameDataManager GDM;
    private GameManager GM;
    private DialogManager DM;
    private UIManager UM;
    private FightManager FM;
	private Hero H;

    public GameObject fightAnimation;

    private tk2dSpriteAnimator DoorAnim;

	public float moveSpeed = 0.2f;
	public float coldTime = 0f;

    void Start()
    {
        GameObject hero = GameObject.Find("Hero").gameObject;
        HA = hero.GetComponent<HeroAttributes>();
		H = hero.GetComponent<Hero>();
        AuM = this.GetComponent<AudioManager>();
        GM = this.GetComponent<GameManager>();
        GDM = this.GetComponent<GameDataManager>();
        DM = this.GetComponent<DialogManager>();
        UM = this.GetComponent<UIManager>();
        FM = this.GetComponent<FightManager>();
    }
	void Update()
	{
		if (coldTime > 0)
		{
			coldTime -= Time.deltaTime;
			H.canMove = false;
		}
		else
		{
			H.canMove = true;
			coldTime = 0;
		}
	}
    public void talk(Talk talk)
    {
        AuM.playAudio("talk");
        Dialoguer.StartDialogue(0);
        UM.state = "dialog";
        if (talk.destroyOnFinish)
        {

        }
    }
    public bool baoshi(Baoshi baoshi)
    {
        AuM.playAudio("daoju");
        HA._gongji += baoshi.gongji;
        HA._fangyu += baoshi.fangyu;
        UM.tipContent = baoshi.tip;
        UM.tipTime = 3f;
        AnimateGameobject(baoshi.gameObject);
		return true;
    }
    public bool key(Key key)
    {
        AuM.playAudio("daoju");
        HA._key_yellow += key.key_yellow;
        HA._key_blue += key.key_blue;
        HA._key_red += key.key_red;
        UM.tipContent = key.tip;
        UM.tipTime = 3f;
        AnimateGameobject(key.gameObject);
		return true;
    }
    public bool pingzi(Pingzi pingzi)
    {
        AuM.playAudio("daoju");
        if (!pingzi.isDouble)
        {
            HA._shengming += pingzi.shengming;
            HA._gongji += pingzi.gongji;
            HA._fangyu += pingzi.fangyu;
        }
        else
        {
            HA._shengming = HA._shengming * 2;
            HA._gongji = HA._gongji * 2;
            HA._fangyu = HA._fangyu * 2;
        }
        UM.tipContent = pingzi.tip;
        UM.tipTime = 3f;
        AnimateGameobject(pingzi.gameObject);
		return true;
    }
    public bool yaoshui(Yaoshui yaoshui)
    {
        AuM.playAudio("daoju");
        HA._shengming += yaoshui.shengming;
        UM.tipContent = yaoshui.tip;
        UM.tipTime = 3f;
        AnimateGameobject(yaoshui.gameObject);
		return true;
    }
    public bool zhuangbei(Zhuangbei zhuangbei)
    {
        AuM.playAudio("daoju");
        HA._gongji += zhuangbei.gongji;
        HA._fangyu += zhuangbei.fangyu;
        UM.tipContent = zhuangbei.tip;
        UM.tipTime = 3f;
        AnimateGameobject(zhuangbei.gameObject);
		return true;
    }
    public bool door(Door door)
    {
        DoorAnim = door.gameObject.GetComponent<tk2dSpriteAnimator>();
        if (HA._key_yellow > 0 && door.type == Door.doorType.yellow && !DoorAnim.Playing)
        {
            AuM.playAudio("door");
            DoorAnim.Play();
            Destroy(door.gameObject, 0.49f);
            HA._key_yellow -= 1;
            UM.tipContent = "黄钥匙-1";
            UM.tipTime = 3f;
			coldTime = 0.49f;
			return true;
        }
        if (HA._key_blue > 0 && door.type == Door.doorType.blue && !DoorAnim.Playing)
        {
            AuM.playAudio("door");
            DoorAnim.Play();
            Destroy(door.gameObject, 0.49f);
            HA._key_blue -= 1;
            UM.tipContent = "蓝钥匙-1";
            UM.tipTime = 3f;
			coldTime = 0.49f;
			return true;
        }
        if (HA._key_red > 0 && door.type == Door.doorType.red && !DoorAnim.Playing)
        {
            AuM.playAudio("door");
            DoorAnim.Play();
            Destroy(door.gameObject, 0.49f);
            HA._key_red -= 1;
            UM.tipContent = "红钥匙-1";
            UM.tipTime = 3f;
			coldTime = 0.49f;
			return true;
        }
		return false;
    }
    public bool doorwall(DoorWall doorwall)
    {
        DoorAnim = doorwall.gameObject.GetComponent<tk2dSpriteAnimator>();
        if (!DoorAnim.Playing)
        {
            AuM.playAudio("door");
            DoorAnim.Play();
            Destroy(doorwall.gameObject, 0.49f);
			coldTime = 0.49f;
        }
		return true;
    }
    public bool enemy(Enemy enemy)
    {
        UM.infoDabuguo = "你打不过他\n\n";
        UM.infoDabuguo += "怪物属性：\n";
        UM.infoDabuguo += "生命：" + enemy.shengming + "\n";
        UM.infoDabuguo += "攻击：" + enemy.gongji + "\n";
        UM.infoDabuguo += "防御：" + enemy.fangyu + "\n";
        UM.infoDabuguo += "金币：" + enemy.jinbi + "\n";
        UM.infoDabuguo += "经验：" + enemy.jingyan + "\n";
        if (HA._gongji <= enemy.fangyu)
        {
            UM.state = "dabuguo";
        }
        else
        {
            int shanghai = HA._gongji - enemy.fangyu;
            float cishu = Mathf.Floor(enemy.shengming / shanghai);
            float zongshanghai = 0;
            if (enemy.gongji > HA._fangyu)
            {
                float shoushang = enemy.gongji - HA._fangyu;
                zongshanghai = shoushang * cishu;
            }
            if (zongshanghai >= HA._shengming)
            {
                UM.state = "dabuguo";
            }
            else
            {
                AuM.playAudio("fight");
                Instantiate(fightAnimation, enemy.gameObject.transform.position, enemy.gameObject.transform.rotation);
                HA._shengming -= (int)zongshanghai;
                HA._jingyan += enemy.jingyan;
                HA.plusJinbi(enemy.jinbi);
                UM.tipContent = "经验+" + enemy.jingyan + "，金币+" + enemy.jinbi;
                UM.tipTime = 3f;
                if (enemy.isDoorProtector)
                {
                    List<GameObject> protectorGameObject = enemy.doorComponent.protectorGameObject;
                    if (protectorGameObject.Remove(enemy.gameObject))
                    {
                        if (protectorGameObject.Count == 0)
                        {
                            DoorAnim = enemy.doorComponent.gameObject.GetComponent<tk2dSpriteAnimator>();
                            DoorAnim.Play("DoorBig");
                            Destroy(enemy.doorComponent.gameObject, 0.5f);
							tk2dTileMap tm = GameObject.Find("TileMap").GetComponent<tk2dTileMap>();
							int x,y;
							tm.GetTileAtPosition(enemy.doorComponent.gameObject.transform.position,out x,out y);
							tm.ClearTile(x,y,1);
                        }
                    }
                }
                Destroy(enemy.gameObject);
				return true;
            }
        }
		return false;
    }
	public bool mttool(mtTool mttool)
	{
		switch (mttool.type)
		{
			case mtTool.toolType.chibang:
				HA._dengji += 1;
				HA._shengming += 300;
				HA._gongji += 3;
				HA._fangyu += 3;
				Destroy(mttool.gameObject);
				return true;
			case mtTool.toolType.jinbi:
                HA.plusJinbi(100);
				Destroy(mttool.gameObject);
				return true;
			case mtTool.toolType.feixing:
				HA.hasFeixing = true;
				UM.buttonImage2.color = new Color(1,1,1,60/255f);
				Destroy(mttool.gameObject);
				return true;
			case mtTool.toolType.tujian:
				HA.hasTujian = true;
				UM.buttonImage1.color = new Color(1,1,1,60/255f);
				Destroy(mttool.gameObject);
				return true;
			case mtTool.toolType.xie:
				HA.hasXie = true;
				UM.buttonImage3.color = new Color(1,1,1,60/255f);
				Destroy(mttool.gameObject);
				return true;
			default:
				return false;
		}
	}
    public void stair(Stair stair)
    {
        AuM.playAudio("door");
		GDM.saveCurrentFloorToTemp();
        if (stair.type == Stair.stairType.up)
        {
            int floor = Convert.ToInt16(Application.loadedLevelName) + 1;
            GM.changeFloor(floor, "up");
        }
        else if (stair.type == Stair.stairType.down)
        {
            int floor = Convert.ToInt16(Application.loadedLevelName) - 1;
            GM.changeFloor(floor, "down");
        }
    }
    public void feixing(int x, int y, GameObject otherTileData)
    {
        AuM.playAudio("daoju");
        UM.tipContent = "开启“传送”，可传送到其他楼层";
        UM.tipTime = 3f;
        HA._daoju_feixing = true;
        GameObject.Destroy(otherTileData.gameObject);
    }
    public void tujian(int x, int y, GameObject otherTileData)
    {
        AuM.playAudio("daoju");
        UM.tipContent = "开启“图鉴”，开启后可点击怪物查看信息";
        UM.tipTime = 3f;
        HA._daoju_tujian = true;
        GameObject.Destroy(otherTileData.gameObject);
    }
    private void AnimateGameobject(GameObject GO){
        tk2dSprite sprite = GO.GetComponent<tk2dSprite>();
		DOTween.To(() => sprite.color, x => sprite.color = x, new Color(1, 1, 1, 0), moveSpeed);
		GO.transform.DOMoveY(GO.transform.position.y + 1f, moveSpeed).OnComplete(()=>onAnimationComplete(GO));
    }
    private void onAnimationComplete(GameObject GO)
    {
        Destroy(GO);
    }
}
