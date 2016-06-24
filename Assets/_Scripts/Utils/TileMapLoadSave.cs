using System;
using UnityEngine;
public class TileMapLoadSave
{
    //定义tilemap的宽度和高度
    private int width;
    private int height;
    //定义用于存档每个tile的tileID的二维数组
    private int[,] tileID;
    //保存当前tilemap的数据为一个字符串，格式与CVS相同
    public string SaveTileMap(tk2dTileMap tk2dTM, int layerID)
    {
        try
        {
            string result = "";
            width = tk2dTM.width;
            height = tk2dTM.height;
            //根据tilemap的宽度和高度初始化tileID数组
            tileID = new int[width, height];
            //从第一行开始，依次读取每一行，即row
            for (int y = 0; y < height; y++)
            {
                //从第一列开始，一次读取每一列，即column
                for (int x = 0; x < width; x++)
                {
                    //因为tk2dTileMap与Tiled的坐标系不同，y正好是相反的，所以从tk2dTileMap中取tileID时，y的值为height-1-y
                    tileID[x, y] = tk2dTM.GetTile(x, height - 1 - y, layerID);
                    //如果没有达到tilemap的宽度，则添加 "," 区分每一个tile
                    if (x != width - 1)
                    {
                        result += tileID[x, y].ToString() + ",";
                    }
                    //否则，一行完成后，进行换行
                    else
                    {
                        result += tileID[x, y].ToString() + "\r\n";
                    }
                }
            }
            return result;
        }
        catch
        {
            //throw;
            return "";
        }
    }
    //从一个CVS格式内容，读取数据，并根据数据生成当前tilemap
    public bool LoadTileMap(tk2dTileMap tk2dTM, int layerID, string result)
    {
        try
        {
            //清空layerID此层的所有数据，若不清空，之前拥有Prefab的tile会继续保留在场景中，所以要销毁此层所有数据
            tk2dTM.Layers[layerID].DestroyGameData(tk2dTM);
            width = tk2dTM.width;
            height = tk2dTM.height;
            int tileID;
            //根据换行符 "\r\n" 将数据分割为数组，每行对应一个元素   ★重点应用★
            string[] resultArray = result.Split(new string[] { "\r\n" }, StringSplitOptions.None);
            //从第一行开始，读取数据，tiled中的第一行，对应的是tk2dTileMap中的 height-1-y 行
            for (int y = height-1; y>-1; y--)
            {
                //将每行的数据分割为数组，对应为每个tile的tileID
                string[] row = resultArray[height - 1 - y].Split(new Char[] { ',' });
                //循环读取行数组的数据，并将每个tile设置为对应内容
                for (int x=0; x<width ; x++)
                {
                    tileID = Convert.ToInt16(row[x]);
                    //设置前，清除当前位置的tile
                    tk2dTM.ClearTile(x, y, layerID);
                    tk2dTM.SetTile(x, y, layerID, tileID);
                }
            }
            //对tk2dTileMap进行修改后，需要build才能生效
            tk2dTM.Build();
            return true;
        }
        catch
        {
            //throw;
            return false;
        }
    }
    //从一个txt文件中，读取tilemap数据（数据格式为CVS格式，但Unity3D并不支持直接读取CVS数据，所以将后缀名改为txt进行读取）
    public bool LoadCVSMap(tk2dTileMap tk2dTM, int layerID,string filename)
    {
        TextAsset mapdata = Resources.Load(filename) as TextAsset;
        string text = mapdata.text;
        return LoadTileMap(tk2dTM, layerID, text);
    }
}
