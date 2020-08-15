using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int totalHealth = 1;
    public int deadPoints = 10;

    PlayerAttack player;
    private int health;
    private SpriteRenderer _renderer;
    private Color defaultColor;

    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
        defaultColor = _renderer.color;
        player = GameObject.Find("Player").GetComponent<PlayerAttack>();

    }

    public void AddDamage(int amount)
    {
        health -= amount;
        StartCoroutine("VisualFeedBack");

        if (health <= 0)
        {
            if (player)
            {
                player.AddScore(deadPoints);
            }
            Dead();
        }
    }

    public void Dead()
    {
        gameObject.SetActive(false);
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
