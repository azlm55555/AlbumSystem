using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Z_Select : MonoBehaviour
{
    private bool isSelect;
    private float time = 5;
    private Vector3 parentPos;
    private SpriteRenderer _sprr;
    private Rigidbody2D rig;

    private void Start()
    {
        _sprr = GetComponent<SpriteRenderer>();
        rig = GetComponent<Rigidbody2D>();
    }

    private void OnMouseDown()
    {
        _sprr.color = Color.white;
        rig.velocity = Vector2.zero;
        StartCoroutine(ScaleLarge());
    }

    private IEnumerator ScaleLarge()
    {
        float num = 1;
        while (num < 1.6f)    //放大系数
        {
            num += Time.deltaTime * 2;
            this.transform.parent.localScale = Vector3.one * num;
            yield return null;
        }
        
        parentPos = this.transform.parent.localPosition;
        this.transform.parent.localPosition = new Vector3(parentPos.x, parentPos.y, 2);
        
        isSelect = true;
    }
    
    private IEnumerator ScaleSmall()
    {
        float num = this.transform.parent.localScale.x;
        while (this.transform.parent.localScale.x >= 1)
        {
            num -= Time.deltaTime * 2;
            this.transform.parent.localScale = Vector3.one * num;
            yield return null;
        }
        
        this.transform.parent.localPosition = parentPos;
        rig.velocity = Vector2.left * Time.deltaTime;
        
        isSelect = false;
    }

    private void Update()
    {
        if (isSelect)
        {
            time -= Time.deltaTime;
        }

        if (time <= 0)
        {
            StartCoroutine(ScaleSmall());
            _sprr.color = Color.gray;
            time = 5;
        }
    }
}
