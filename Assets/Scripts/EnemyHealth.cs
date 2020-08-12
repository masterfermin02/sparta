using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int totalHealth = 1;

    private int health;
    private SpriteRenderer _renderer;
    private Color defaultColor;

    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
        defaultColor = _renderer.color;
    }

    public void AddDamage(int amount)
    {
        health -= amount;
        StartCoroutine("VisualFeedBack");

        if (health <= 0)
        {
            gameObject.SetActive(false);
        }



        Debug.Log("Enemy health " + health);
    }

    private IEnumerator VisualFeedBack()
    {
        _renderer.color = Color.red;

        yield return new WaitForSeconds(0.1f);

        _renderer.color = defaultColor;
    }

    private void OnEnable()
    {
        health = totalHealth;
        _renderer.color = defaultColor;
    }
}
