using DG.Tweening;
using UnityEngine;

public class Hero : MonoBehaviour
{
    //常用组件
    public GameObject GameController;
    private TileManager TM;
    private UIManager UM;
    //角色移动相关
    public Vector3 leftDistance = Vector3.zero;
    public bool isMoving = false;
    public bool canMove = true;
    public float moveSpeed = 0.3f;
    //英雄位置
    private int playerX;
    private int playerY;

    private tk2dSpriteAnimator HeroAnim;

	void Start ()
    {
        HeroAnim = GetComponent<tk2dSpriteAnimator>();
        TM = GameController.GetComponent<TileManager>();
        UM = GameController.GetComponent<UIManager>();
        playerX = (int)transform.position.x;
        playerY = (int)transform.position.y;
	}
	
	void Update () {
        playerX = (int)transform.position.x;
        playerY = (int)transform.position.y;
        if (Time.frameCount % 1000 == 0)
        {
            System.GC.Collect();
        }
        if (isMoving)
        {
            HeroAnim.Play();
        }
        else
        {
            HeroAnim.Stop();
        }
        if (canMove == true && isMoving == false && UM.isShowing == false)
        {
            playerX = (int)transform.position.x;
            playerY = (int)transform.position.y;
            if (Input.GetKey(KeyCode.UpArrow) || UM.arrow == "up")
            {
                HeroAnim.Play("PlayerUp");
                if (TM.checkTile(playerX, playerY+1))
                {
                    isMoving = true;
                    leftDistance = Vector3.up;
                    movePlayer();
                }
            }
            else if (Input.GetKey(KeyCode.DownArrow) || UM.arrow == "down")
            {
                HeroAnim.Play("PlayerDown");
                if (TM.checkTile(playerX, playerY -1))
                {
                    isMoving = true;
                    leftDistance = Vector3.down;
                    movePlayer();
                }
            }
            else if (Input.GetKey(KeyCode.LeftArrow) || UM.arrow == "left")
            {
				tk2dTileMap tkTM = GameObject.Find("TileMap").GetComponent<tk2dTileMap>();
                HeroAnim.Play("PlayerLeft");
                if (TM.checkTile(playerX - 1, playerY))
                {
                    isMoving = true;
                    leftDistance = Vector3.left;
                    movePlayer();
                }
            }
            else if (Input.GetKey(KeyCode.RightArrow) || UM.arrow == "right")
            {
                HeroAnim.Play("PlayerRight");
                if (TM.checkTile(playerX+1,playerY))
                {
                    isMoving = true;
                    leftDistance = Vector3.right;
                    movePlayer();
                }
            }
        }
	}

    void movePlayer()
    {
        Vector3 nowPosition = transform.position;
        transform.DOMove(nowPosition + leftDistance, moveSpeed).OnComplete(changeMovingState);
    }
    void changeMovingState()
    {
        isMoving = false;
        if (isMoving)
        {
            HeroAnim.Play();
        }
        else
        {
            HeroAnim.Stop();
        }
        playerX = (int)transform.position.x;
        playerY = (int)transform.position.y;
    }
}
