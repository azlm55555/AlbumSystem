using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Z_SetPostion : MonoBehaviour
{
    public GameObject prefab;
    public int rows;
    public int columns;
    public float photoHeight;
    public float gapX;
    private Texture2D[] allTex;
    private List<Sprite> allSpr = new List<Sprite>();    //所有图片
    private List<Transform> trans = new List<Transform>();    //第一次grid出的位置
    private List<Vector2> sizes = new List<Vector2>();    //所有图片宽度
    public static List<Transform> finalTF = new List<Transform>();    //最终图片位置
    public static List<SpriteRenderer> finalSPRR = new List<SpriteRenderer>();    //最终精灵

    private void OnEnable()
    {
        Camera.main.orthographicSize = Screen.height / 100.0f / 2;
        
        LoadAllSprite();

        Z_Grid grid = new Z_Grid(rows,columns, Screen.width * 2, Screen.height);
        var mPos = grid.gridPosition;
        
        int sprIndex = 0;
        int index = 0;
        for (int yIndex = 0; yIndex < mPos.GetLength(1); yIndex++)
        {
            for (int xIndex = 0; xIndex < mPos.GetLength(0); xIndex++)
            {
                sprIndex = (sprIndex + 1) % allTex.Length;
                InitSprite(mPos[xIndex, yIndex], sprIndex, index);
                index++;
            }
        }
    }

    private void Start()
    {
        for (int i = 0; i < trans.Count; i++)
        {
            int x = i % columns;    //积算是否在第一列
            
            float newX = trans[i].position.x;
            if (x != 0)    //如果不在第一列，算出前一位的照片位置
            {
                float prevPosX = trans[i - 1].position.x;
                newX = prevPosX + sizes[i - 1].x + gapX;
            }
            trans[i].position = new Vector3(newX, trans[i].position.y, trans[i].position.z);
        }
        
        //设置全部坐标到图片中心点
        for (int i = 0; i < trans.Count; i++)
        {
            GameObject obj = new GameObject(i.ToString());
            var position = trans[i].position;
            obj.transform.position = new Vector3(position.x + sizes[i].x / 2, position.y + sizes[i].y / 2, position.z);
            trans[i].SetParent(obj.transform);
            finalTF.Add(obj.transform);
        }
        
    }

    private void LoadAllSprite()
    {
        allTex = Resources.LoadAll<Texture2D>("Photos/");
        
        foreach (var item in allTex)
        {
            var spr = Sprite.Create(item, new Rect(0, 0, item.width, item.height), Vector2.zero);
            allSpr.Add(spr);
        }
    }

    private void InitSprite(Vector3 position , int sprIndex, int nameIndex)
    {
        var newObj = Instantiate(prefab, position, Quaternion.identity);
        var newSpr = newObj.GetComponent<SpriteRenderer>();
        var newCol = newObj.GetComponent<BoxCollider2D>();
        var newRig = newObj.GetComponent<Rigidbody2D>();
        newObj.name = nameIndex.ToString();
        newSpr.sprite = allSpr[sprIndex];
        float ratio = allSpr[sprIndex].rect.width / allSpr[sprIndex].rect.height;
        Vector2 currectRatio = new Vector2(ratio * photoHeight, photoHeight);
        newCol.size = newSpr.size = currectRatio;
        newCol.offset = currectRatio / 2;
        newRig.velocity = Vector2.left * Time.deltaTime;
        trans.Add(newObj.transform);
        sizes.Add(newSpr.size);
        finalSPRR.Add(newSpr);
    }

}
