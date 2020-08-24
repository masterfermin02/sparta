using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricAttack : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (
            (collision.CompareTag("Enemy") || collision.CompareTag("BigBullet"))
            )
        {
            collision.SendMessageUpwards("AddDamage", 3, SendMessageOptions.DontRequireReceiver);
            Debug.Log("Enemy collision");
        }
    }

    void OnTriggerStay(Collider collision)
    {
        if (
            (collision.CompareTag("Enemy") || collision.CompareTag("BigBullet"))
            )
        {
            collision.SendMessageUpwards("AddDamage", 3, SendMessageOptions.DontRequireReceiver);
            Debug.Log("Enemy collision");
        }
    }
}
