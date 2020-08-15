using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiantePrefab : MonoBehaviour
{
    public GameObject prefab;
    public Transform point;
    public float livingTime;

    public void Instantiate() 
    {
        if (livingTime > 0f && point) {
            render();
        }
    }

    private void render()
    {
        CheckAndFlip();
        Destroy(Instantiate(prefab, point.position, Quaternion.identity) as GameObject, livingTime);
    }

    private void CheckAndFlip()
    {
        if (facingDifferent())
        {
            FlipPrefab();
        }
    }

    private bool isFacingRight(float value)
    {
        return value > 0f;
    }

    private bool facingDifferent()
    {
        return isFacingRight(point.localScale.x) != isFacingRight(prefab.transform.localScale.x);
    }

    private void FlipPrefab()
    {
        prefab.transform.localScale = new Vector3(prefab.transform.localScale.x * -1f, prefab.transform.localScale.y, prefab.transform.localScale.z);
    }
}
