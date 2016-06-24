using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    //常用组件
    private HeroAttributes HA;
    private GameManager GM;
    private GameDataManager GDM;
	private ActionManager AM;
    private AudioManager AuM;
    private Hero hero;
    //对话框状态
    public bool isShowing = false;
    public string state = "";
    //对话框内容
    private string _text;
    private int _choicesCount;
    //提示内容
    public string tipContent = "";
    public string infoDabuguo;
    //对话框计时
    public float tipTime = 0;
    public float dialogTime = 0;
    //对话框样式
    public Texture2D dialogboxbg;
    private float displayScale = 1;
    private Rect dialogbox;
    //选项次数
    private int times1 = 0;
    private int times2 = 0;
    private int times3 = 0;
    //摇杆变量
    public Sprite moren;
    public Sprite up;
    public Sprite down;
    public Sprite left;
    public Sprite right;
    private Button buttonController;
    public string arrow = "moren";
    private Transform canvasTrans;
    //商店面板
    public GameObject panelShop;

    private float panelinfoCenterX; //摇杆Panel的中心位置X坐标
    private float panelinfoCenterY; //摇杆Panel的中心位置Y坐标
    private float beishu;
    //摇杆与Panel的比例
    private const float yaoganBili = 200f * (115f / 128f) / 213f;
    //楼层Panel
    public Image floorPanel;
    public Text floorText;
    public float floorTime = -1f;
	//
	public Image buttonImage1;
	public Image buttonImage2;
	public Image buttonImage3;
	public Image buttonImage4;
	//
	private Color colorHide = new Color(1,1,1,60/255f);
	private Color colorShow = new Color(1,1,1,1);

    void Start()
    {
        GM = this.GetComponent<GameManager>();
        GDM = this.GetComponent<GameDataManager>();
		AM = this.GetComponent<ActionManager>();
        AuM = this.GetComponent<AudioManager>();
        hero = GameObject.Find("Hero").gameObject.GetComponent<Hero>();
        HA = hero.gameObject.GetComponent<HeroAttributes>();
        displayScale = Screen.width / 400f;
        //摇杆位置的自适应
        buttonController = GameObject.Find("Button_Controller").gameObject.GetComponent<Button>();
        canvasTrans = GameObject.Find("Canvas").GetComponent<Transform>();
        RectTransform PanelInfoRT = GameObject.Find("Panel_Info").gameObject.GetComponent<RectTransform>();
        panelinfoCenterX = Screen.width / 2;
        beishu = canvasTrans.localScale.x / 0.023075f;
        panelinfoCenterY = PanelInfoRT.rect.height * beishu / 2;
    }
    //GUI大小自动缩放
    int scaleGUI(float number)
    {
        int resultNumber = Mathf.RoundToInt(number * displayScale);
        return resultNumber;
    }
    void Update()
    {
        if (!Input.GetMouseButton(0))
        {
            buttonController.image.sprite = moren;
            arrow = "moren";
        }
        else
        {
            //鼠标坐标
            int mouseX = (int)Input.mousePosition.x;
            int mouseY = (int)Input.mousePosition.y;
            //取得当前鼠标位置相对于在摇杆上的坐标位置
            int x = mouseX - (int)panelinfoCenterX;
            int y = mouseY - (int)panelinfoCenterY;
            //摇杆所在范围的X最小值，即方形左边的X值
            int x1 = (int)(panelinfoCenterX - panelinfoCenterY * yaoganBili);
            //摇杆所在范围的X最大值，即方形右边的X值
            int x2 = (int)(panelinfoCenterX + panelinfoCenterY * yaoganBili);
            //摇杆所在范围的y最小值，即方形上边的X值
            int y1 = (int)(panelinfoCenterY - panelinfoCenterY * yaoganBili);
            //摇杆所在范围的y最小值，即方形下边的X值
            int y2 = (int)(panelinfoCenterY + panelinfoCenterY * yaoganBili);
            if (mouseX > x1 && mouseX < x2 && mouseY > y1 && mouseY < y2)
            {
                if ((Mathf.Abs(y) > Mathf.Abs(x) && y > 10))
                {
                    buttonController.image.sprite = up;
                    arrow = "up";
                }
                else if (Mathf.Abs(y) > Mathf.Abs(x) && y < -10)
                {
                    buttonController.image.sprite = down;
                    arrow = "down";
                }
                else if (Mathf.Abs(x) > Mathf.Abs(y) && x < -10)
                {
                    buttonController.image.sprite = left;
                    arrow = "left";
                }
                else if (Mathf.Abs(x) > Mathf.Abs(y) && x > 10)
                {
                    buttonController.image.sprite = right;
                    arrow = "right";
                }
            }
            else
            {
                buttonController.image.sprite = moren;
                arrow = "moren";
            }
        }
    }
    void OnGUI()
    {
        //定义GUI皮肤
        if (!string.IsNullOrEmpty(state) || !string.IsNullOrEmpty(tipContent))
        {
            GUI.skin.box.fontSize = scaleGUI(22);
            GUI.skin.button.fontSize = scaleGUI(22);
            GUI.skin.box.normal.textColor = new Vector4(1, 1, 1, 1);
            GUI.skin.box.padding = new RectOffset(15, 15, 15, 15);
            GUI.skin.box.alignment = TextAnchor.UpperLeft;
            GUI.skin.box.normal.background = dialogboxbg;
            GUI.skin.box.wordWrap = true;
            GUI.skin.box.richText = true;
        }
        //GUI状态
        if (state != "")
        {
            switch (state)
            {
                case "nodata":
                    tipNoData();
                    break;
                case "dabuguo":
                    tipDabuguo();
                    break;
                case "menu":
                    showMenu();
                    break;
                case "feixing":
                    showFeixing();
                    break;
                case "dialog":
                    showDialog();
                    break;
                default:
                    break;
            }
        }
        if (tipTime > 0 && tipContent != "")
        {
            GUI.skin.box.alignment = TextAnchor.MiddleCenter;
            GUI.skin.box.border = new RectOffset(7, 7, 7, 7);
            GUI.Box(new Rect(Screen.width / 2 - scaleGUI(200), Screen.height / 2 + scaleGUI(115), scaleGUI(400), scaleGUI(50)), tipContent);
            tipTime -= Time.deltaTime;
        }
        else
        {
            tipTime = 0;
            tipContent = "";
        }
        if (Input.GetMouseButton(0))
        {
            if (state == "" && hero.isMoving == false)
            {
                showTujian();
            }
        }
        if (floorTime > 0)
        {
            floorTime -= Time.deltaTime;
            hero.canMove = false;
        }
        else if (floorTime < 0)
        {
			GDM.loadFloor();
            hero.transform.position = GM.getHeroPosition();
            hero.canMove = true;
            floorTime = 0;
            floorPanel.gameObject.SetActive(false);
        }
    }
    private void showDialog()
    {
        //if (DiaQEngine.Instance.graphManager.ActiveGraph() == null)
        //{
        //    hero.canMove = true;
        //    state = "";
        //    times1 = 0;
        //    times2 = 0;
        //    times3 = 0;
        //}
        //else
        //{
        //    hero.canMove = false;
        //    DiaQNode_Dlg dlg = DiaQEngine.Instance.graphManager.NodeWaitingForData() as DiaQNode_Dlg;
        //    if (dlg != null)
        //    {
        //        _text = dlg.dialogueText;
        //        dialogbox = new Rect(Screen.width / 2 - scaleGUI(150), Screen.height / 2 - scaleGUI(150), scaleGUI(300), scaleGUI(300));
        //        GUI.Box(dialogbox, _text);
        //        _choicesCount = dlg.responses.Length;
        //        for (int i = _choicesCount - 1; i > -1; i--)
        //        {
        //            if (dlg.responses[i] == "")
        //            {
        //                dlg.responses[i] = "继续";
        //            }
        //            if (_choicesCount == 4)
        //            {
        //                switch (i)
        //                {
        //                    case 0:
        //                        if (GUI.Button(new Rect(Screen.width / 2 - scaleGUI(135), Screen.height / 2 + scaleGUI(50 + (i - _choicesCount + 2) * 45), scaleGUI(270), scaleGUI(40)), dlg.responses[i] + "（" + times1.ToString() + "）"))
        //                        {
        //                            times1 += 1;
        //                            DiaQEngine.Instance.graphManager.SendDataToNode(i);
        //                        }
        //                        break;
        //                    case 1:
        //                        if (GUI.Button(new Rect(Screen.width / 2 - scaleGUI(135), Screen.height / 2 + scaleGUI(50 + (i - _choicesCount + 2) * 45), scaleGUI(270), scaleGUI(40)), dlg.responses[i] + "（" + times2.ToString() + "）"))
        //                        {
        //                            times2 += 1;
        //                            DiaQEngine.Instance.graphManager.SendDataToNode(i);
        //                        }
        //                        break;
        //                    case 2:
        //                        if (GUI.Button(new Rect(Screen.width / 2 - scaleGUI(135), Screen.height / 2 + scaleGUI(50 + (i - _choicesCount + 2) * 45), scaleGUI(270), scaleGUI(40)), dlg.responses[i] + "（" + times3.ToString() + "）"))
        //                        {
        //                            times3 += 1;
        //                            DiaQEngine.Instance.graphManager.SendDataToNode(i);
        //                        }
        //                        break;
        //                    case 3:
        //                        if (GUI.Button(new Rect(Screen.width / 2 - scaleGUI(135), Screen.height / 2 + scaleGUI(50 + (i - _choicesCount + 2) * 45), scaleGUI(270), scaleGUI(40)), dlg.responses[i]))
        //                        {
        //                            DiaQEngine.Instance.graphManager.SendDataToNode(i);
        //                        }
        //                        break;
        //                }
        //            }
        //            else
        //            {
        //                if (GUI.Button(new Rect(Screen.width / 2 - scaleGUI(135), Screen.height / 2 + scaleGUI(50 + (i - _choicesCount + 2) * 45), scaleGUI(270), scaleGUI(40)), dlg.responses[i]))
        //                {
        //                    DiaQEngine.Instance.graphManager.SendDataToNode(i);
        //                }
        //            }
        //        }
        //    }
        //}
    }
    private void showFeixing()
    {
        hero.canMove = false;
        GUI.Box(new Rect(Screen.width / 2 - 195, Screen.height / 2 - 267, 390, 440), "");
        for (int i = 1; i < GM.maxFloor + 1; i++)
        {
            int x = i % 6;
            if (x == 0) { x = 6; }
            int y = (i - 1) / 6;
            if (GUI.Button(new Rect(Screen.width / 2 - scaleGUI(145 + (x - 1) * -50), Screen.height / 2 - scaleGUI(210) + scaleGUI(50 * y), scaleGUI(40), scaleGUI(40)), i.ToString()))
            {
                AuM.playAudio("feixing");
                GM.changeFloor(i);
                state = "";
                hero.canMove = true;
            }
        }
    }
    private void showTujian()
    {
        //GameObject mycamera = GameObject.Find("tk2dCamera");
        Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float x = position.x;
        float y = position.y;
        Vector2 startPosition = new Vector2(x - 0.2f, y);
        Vector2 Direction = new Vector2(x, y);
        RaycastHit2D hit = Physics2D.Raycast(startPosition, Direction, 0.4f);
        if (hit.collider != null && hit.collider.tag == "Enemy")
        {
            AuM.playAudio("talk");
            Enemy enemy = hit.collider.gameObject.GetComponent<Enemy>();
            string info = "";
            if (HA._gongji <= enemy.fangyu)
            {
                info = "你破不了它的防御。\n";
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
                info = "战胜它你将<color=red>损失：" + zongshanghai + "生命</color>。\n";
            }
            info += "生命：" + enemy.shengming + "  /  ";
            info += "攻击：" + enemy.gongji + "  /  ";
            info += "防御：" + enemy.fangyu + "\n";
            info += "金币：" + enemy.jinbi + "  /  ";
            info += "经验：" + enemy.jingyan;
            if (y> 5)
            {
                //显示在下面
                GUI.Box(new Rect(0, Screen.height / 2 + scaleGUI(63), Screen.width, scaleGUI(100)), info);
            }
            else
            {
                //显示在上面
                GUI.Box(new Rect(0, Screen.height / 2 - scaleGUI(237), Screen.width, scaleGUI(100)), info);
            }
        }
    }
    private void showMenu()
    {
        hero.canMove = false;
        dialogbox = new Rect(Screen.width / 2 - scaleGUI(110), Screen.height / 2 - scaleGUI(170), scaleGUI(220), scaleGUI(290));
        GUI.Box(dialogbox, "");
        if (GUI.Button(new Rect(Screen.width / 2 - scaleGUI(90), Screen.height / 2 - scaleGUI(160), scaleGUI(180), scaleGUI(45)), "保存进度"))
        {
            GDM.SaveGame();
            hero.canMove = true;
        }
        if (GUI.Button(new Rect(Screen.width / 2 - scaleGUI(90), Screen.height / 2 - scaleGUI(105), scaleGUI(180), scaleGUI(45)), "读取进度"))
        {
            if (GDM.checkGameData())
            {
                GDM.LoadGame();
            }
            else
            {
                state = "nodata";
            }
            hero.canMove = true;
        }
        if (GUI.Button(new Rect(Screen.width / 2 - scaleGUI(90), Screen.height / 2 - scaleGUI(50), scaleGUI(180), scaleGUI(45)), "重新开始"))
        {
            GDM.restartGame();
        }
        if (GUI.Button(new Rect(Screen.width / 2 - scaleGUI(90), Screen.height / 2 + scaleGUI(5), scaleGUI(180), scaleGUI(45)), "退出游戏"))
        {
            Destroy(GameObject.Find("Game"));
            Application.LoadLevel("start");
        }
        if (GUI.Button(new Rect(Screen.width / 2 - scaleGUI(90), Screen.height / 2 + scaleGUI(60), scaleGUI(180), scaleGUI(45)), "取消"))
        {
            state = "";
            hero.canMove = true;
        }
    }
    private void tipDabuguo()
    {
        hero.canMove = false;
        GUI.Box(new Rect(Screen.width / 2 - scaleGUI(72), Screen.height / 2 - scaleGUI(150), scaleGUI(150), scaleGUI(270)), infoDabuguo);
        if (GUI.Button(new Rect(Screen.width / 2 - scaleGUI(60), Screen.height / 2 + scaleGUI(70), scaleGUI(130), scaleGUI(35)), "知道了"))
        {
            state = "";
            hero.canMove = true;
        }
    }
    private void tipNoData()
    {
        hero.canMove = false;
        dialogbox = new Rect(Screen.width / 2 - scaleGUI(110), Screen.height / 2 - scaleGUI(150), scaleGUI(220), scaleGUI(250));
        GUI.skin.box.alignment = TextAnchor.UpperCenter;
        GUI.Box(dialogbox, "当前没有存档\n...无法读取...\n是否重新开始？");
        if (GUI.Button(new Rect(Screen.width / 2 - scaleGUI(90), Screen.height / 2 - scaleGUI(20), scaleGUI(180), scaleGUI(45)), "确定"))
        {
            Application.LoadLevel(1);
        }
        if (GUI.Button(new Rect(Screen.width / 2 - scaleGUI(90), Screen.height / 2 + scaleGUI(35), scaleGUI(180), scaleGUI(45)), "取消"))
        {
            hero.canMove = true;
            state = "";
        }
    }
	public void buttonTujian()
	{
		if (buttonImage1.color == colorShow) {
			buttonImage1.color = colorHide;
		}else if (buttonImage1.color == colorHide){
			buttonImage1.color = colorShow;
		}
	}
    public void buttonFeixing()
    {
        if (state != "dialog" && state != "feixing")
        {
            state = "feixing";
			buttonImage2.color = colorShow;
            return;
        }
        if (state == "feixing")
        {
            state = "";
			buttonImage2.color = colorHide;
            hero.canMove = true;
        }
    }
	public void buttonXie()
	{
		if (buttonImage3.color == colorShow) {
			buttonImage3.color = colorHide;
			hero.moveSpeed = 0.3f;
			AM.moveSpeed = 0.2f;
		}else if (buttonImage3.color == colorHide){
			buttonImage3.color = colorShow;
			hero.moveSpeed = 0.15f;
			AM.moveSpeed = 0.08f;
		}
	}
    public void buttonMenu()
    {
        if (state != "dialog" && state != "menu")
        {
            state = "menu";
            return;
        }
        if (state == "menu")
        {
            state = "";
            hero.canMove = true;
        }
    }
    public void buttonShop()
    {
        if (panelShop.activeInHierarchy)
        {
            panelShop.SetActive(false);
        }
        else
        {
            panelShop.SetActive(true);
        }
    }
    public void buyItem(int x)
    {
        switch (x)
        {
            case 11:
                if (HA._jinbi >= 10)
                {
                    HA.plusJinbi(-10);
                    HA._key_yellow += 1;
                }
                break;
            case 12:
                if (HA._jinbi >= 50)
                {
                    HA.plusJinbi(-50);
                    HA._key_blue += 1;
                }
                break;
            case 13:
                if (HA._jinbi >= 100)
                {
                    HA.plusJinbi(-100);
                    HA._key_red += 1;
                }
                break;
            case 14:
                if (HA._jinbi >= 150)
                {
                    HA.plusJinbi(-150);
                    HA._key_yellow += 1;
                    HA._key_blue += 1;
                    HA._key_red += 1;
                }
                break;
            case 21:
                if (HA._jinbi >= 25)
                {
                    HA.plusJinbi(-25);
                    HA._gongji += 1;
                }
                break;
            case 22:
                if (HA._jinbi >= 25)
                {
                    HA.plusJinbi(-25);
                    HA._fangyu += 1;
                }
                break;
            case 23:
                if (HA._jinbi >= 50)
                {
                    HA.plusJinbi(-50);
                    HA._gongji += 1;
                    HA._fangyu += 1;
                }
                break;
            case 24:
                if (HA._jinbi >= 100)
                {
                    HA.plusJinbi(-100);
                    HA._gongji += 2;
                    HA._fangyu += 2;
                }
                break;
            case 31:
                if (HA._jinbi >= 25)
                {
                    HA.plusJinbi(-25);
                    HA._shengming += 100;
                }
                break;
            case 32:
                if (HA._jinbi >= 50)
                {
                    HA.plusJinbi(-50);
                    HA._shengming += 200;
                }
                break;
            case 33:
                if (HA._jinbi >= 100)
                {
                    HA.plusJinbi(-100);
                    HA._key_yellow += 500;
                }
                break;
            case 34:
                if (HA._jinbi >= 200)
                {
                    HA.plusJinbi(-200);
                    HA._key_yellow += 1000;
                }
                break;
            case 41:
                if (HA._jinbi >= 250)
                {
                    HA.plusJinbi(-250);
                    HA._gongji += 10;
                }
                break;
            case 42:
                if (HA._jinbi >= 500)
                {
                    HA.plusJinbi(-500);
                    HA._gongji += 20;
                }
                break;
            case 43:
                if (HA._jinbi >= 1000)
                {
                    HA.plusJinbi(-1000);
                    HA._gongji += 40;
                }
                break;
            case 44:
                if (HA._jinbi >= 2000)
                {
                    HA.plusJinbi(-2000);
                    HA._gongji += 80;
                }
                break;
            case 51:
                if (HA._jinbi >= 3500)
                {
                    HA.plusJinbi(-3500);
                    HA._gongji += 150;
                }
                break;
            case 52:
                if (HA._jinbi >= 250)
                {
                    HA.plusJinbi(-250);
                    HA._fangyu += 10;
                }
                break;
            case 53:
                if (HA._jinbi >= 500)
                {
                    HA.plusJinbi(-500);
                    HA._fangyu += 20;
                }
                break;
            case 54:
                if (HA._jinbi >= 1000)
                {
                    HA.plusJinbi(-1000);
                    HA._fangyu += 40;
                }
                break;
            case 61:
                if (HA._jinbi >= 2000)
                {
                    HA.plusJinbi(-2000);
                    HA._fangyu += 80;
                }
                break;
            case 62:
                if (HA._jinbi >= 3500)
                {
                    HA.plusJinbi(-3500);
                    HA._fangyu += 150;
                }
                break;
            default:
                break;
        }
    }
}
