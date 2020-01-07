using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Z_Grid
{
    private int rows;
    private int columns;
    public Vector3[,] gridPosition;
    private float oneUnitX;
    private float oneUnitY;

    public Z_Grid(int rows, int columns , int width, int height)
    {
        this.rows = rows;    //分成几行几列
        this.columns = columns;
        
        //计算出一个单位的长度
        oneUnitX = (float)width / columns;
        oneUnitY = (float)height / rows;
        
        gridPosition = new Vector3[columns,rows];

        for (int y = 0; y < rows; y++)
        {
            for (int x = 0; x < columns; x++)
            {
                gridPosition[x, y] = GetWorldPosition(x, y); 
            }
        }
    }

    //屏幕坐标转世界坐标
    private Vector3 GetWorldPosition(int x, int y)
    {
        return Camera.main.ScreenToWorldPoint(new Vector3( x * oneUnitX , y * oneUnitY, 3));
    }

}
