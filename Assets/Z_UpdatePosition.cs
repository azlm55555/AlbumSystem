using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Z_UpdatePosition : MonoBehaviour
{
    private int num;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.name);
        other.transform.localPosition =
            new Vector3(18, other.transform.localPosition.y, other.transform.localPosition.z);
    }
    
}
