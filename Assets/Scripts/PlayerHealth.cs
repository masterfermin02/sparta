using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int totalHealth = 3;
    public RectTransform heartUI;
    public RectTransform gameOverMenu;
    public GameObject hordes;
    public GameObject startPosition;
    public GameObject _props;

    private int health;
    private float heartSize = 16f;

    private SpriteRenderer _renderer;
    private PlayerController _controller;

    // Use this for initialization
    void Start()
    {
        health = totalHealth;
    }

    void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _controller = GetComponent<PlayerController>();
    }

    public void AddDamage(int amount)
    {
        health -= amount;

        StartCoroutine("VisualFeedBack");

        //Game Over
        if (health <= 0)
        {
            health = 0;
            DisableEnemies();
            gameObject.SetActive(false);
        }

        UpdateHeartUI();
        Debug.Log(" health " + health);
    }

    public void AddHealth(int amount)
    {
        health += amount;

        //Game Over
        if (health > totalHealth)
        {
            health = totalHealth;
        }

        UpdateHeartUI();
        Debug.Log(" health " + health);
    }

    private IEnumerator VisualFeedBack()
    {
        _renderer.color = Color.red;

        yield return new WaitForSeconds(0.1f);

        _renderer.color = Color.white;
    }

    private void UpdateHeartUI()
    {
        if (heartUI != null)
        {
            heartUI.sizeDelta = new Vector2(heartSize * health, heartSize);
        }
    }

    private void OnEnable()
    {
        health = totalHealth;
        UpdateHeartUI();
        if (_renderer != null)
        {
            _renderer.color = Color.white;
        }    
        
        if (startPosition != null)
        {
            _controller.gameObject.transform.position = startPosition.gameObject.transform.position;
        }

        EnableRecoveryHealths();
    }

    private void EnableRecoveryHealths()
    {
        if (!_props)
        {
            return;
        }

        for (int i = 0; i < _props.transform.childCount; i++)
        {
            GameObject obj = _props.transform.GetChild(i).gameObject;
            if (!obj.activeSelf && obj.CompareTag("RecoveryHeart"))
            {
                obj.SetActive(true);
            }
        }
    }

    private void OnDisable()
    {
        if (gameOverMenu != null)
        {
            gameOverMenu.gameObject.SetActive(true);
        }
        
        if (hordes != null)
        {
            hordes.gameObject.SetActive(false);
        }
        StopCoroutine("VisualFeedBack");
    }

    private void DisableEnemies()
    {
        Array.ForEach(GetActiveSelfEnemies(), enemy => enemy.SendMessageUpwards("AddDamage", 2, SendMessageOptions.DontRequireReceiver));
    }

    private GameObject[] GetActiveSelfEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        return Array.FindAll(enemies, enemy => enemy.activeSelf);
    }
}
