using UnityEngine;

public class TileManager : MonoBehaviour
{
    public tk2dTileMap tm;
    private ActionManager AM;
    private Hero hero;

    void Start()
    {
        //tm = GameObject.Find("TileMap").GetComponent<tk2dTileMap>();
        AM = GetComponent<ActionManager>();
        hero = GameObject.Find("Hero").GetComponent<Hero>();
    }
    //false：不可通过
    //true：可通过
    public bool checkTile(int x,int y)
    {
        if (!(x < 11 && y < 11 && x > -1 && y > -1))
        {
            return false;
        }
        tm = GameObject.Find("TileMap").GetComponent<tk2dTileMap>();
        AM = GetComponent<ActionManager>();
        int tileID = tm.GetTile(x, y, 1);
        if (tileID == -1)
        {
            return true;
        }
        tk2dRuntime.TileMap.TileInfo tileinfo = tm.GetTileInfoForTileId(tileID);
        if (tileinfo != null)
        {
            if (tileinfo.stringVal == "Wall")
            {
                return false;
            }
            //拥有Prefab
            Vector2 startPosition = new Vector2(x - 0.2f, y);
            Vector2 Direction = new Vector2(x, y);
            RaycastHit2D hit = Physics2D.Raycast(startPosition, Direction, 0.4f);
            //Debug.DrawLine(new Vector3(x - 0.2f, y, 0), new Vector3(x + 0.2f, y, 0), Color.red);
            if (hit.collider != null)
            {
                switch (hit.collider.tag)
                {
                    case "Talk":
                        AM.talk(hit.collider.gameObject.GetComponent<Talk>());
                        return false;
                    case "Key":
                    if (AM.key(hit.collider.gameObject.GetComponent<Key>()))
						tm.ClearTile(x,y,1);
                        break;
                    case "Door":
					if (AM.door(hit.collider.gameObject.GetComponent<Door>()))
						tm.ClearTile(x,y,1);
						return false;
                    case "DoorWall":
					if (AM.doorwall(hit.collider.gameObject.GetComponent<DoorWall>()))
						tm.ClearTile(x,y,1);
                        return false;
                    case "Baoshi":
					if (AM.baoshi(hit.collider.gameObject.GetComponent<Baoshi>()))
						tm.ClearTile(x,y,1);
                        break;
                    case "Pingzi":
					if (AM.pingzi(hit.collider.gameObject.GetComponent<Pingzi>()))
						tm.ClearTile(x,y,1);
                        break;
                    case "Yaoshui":
					if (AM.yaoshui(hit.collider.gameObject.GetComponent<Yaoshui>()))
						tm.ClearTile(x,y,1);
                        break;
                    case "Zhuangbei":
					if (AM.zhuangbei(hit.collider.gameObject.GetComponent<Zhuangbei>()))
						tm.ClearTile(x,y,1);
                        break;
                    case "Enemy":
					if (AM.enemy(hit.collider.gameObject.GetComponent<Enemy>()))
						tm.ClearTile(x,y,1);
                        return false;
                    case "StairUp":
                        AM.stair(hit.collider.gameObject.GetComponent<Stair>());
                        break;
                    case "StairDown":
                        AM.stair(hit.collider.gameObject.GetComponent<Stair>());
                        break;
					case "Tool":
					if (AM.mttool(hit.collider.gameObject.GetComponent<mtTool>()))
						tm.ClearTile(x,y,1);
			     		break;
                }
                return true;
            }
            else
            {
                return true;
            }
        }
        else
        {
            return false;
        }
    }
    public bool changeTileData(int x, int y, int tileID)
    {
        return true;
    }
}
