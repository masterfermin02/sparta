﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiantePrefab : MonoBehaviour
{
    public GameObject prefab;
    public Transform point;
    public float livingTime;

    public void Instantiate() 
    {
        if (livingTime > 0f) {
            Destroy(Instantiate(prefab, point.position, Quaternion.identity) as GameObject, livingTime);
        }
    }
}
