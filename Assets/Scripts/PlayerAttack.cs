using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAttack : MonoBehaviour
{
    public Text scoreUI;
    public Text hightestScoreUI;

    private bool _isAttacking;
    private Animator _animator;
    private int score = 0;
    private int hightestScore = 0;
    private int currentScore = 0;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void LateUpdate()
    {
        if (hightestScore < score)
        {
            hightestScore = score;
        }

        if (currentScore != score)
        {
            currentScore = score;
            updateUI();
        }

        if (_animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        {
            _isAttacking = true;
            return;
        }

        _isAttacking = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_isAttacking
            && (collision.CompareTag("Enemy") || collision.CompareTag("BigBullet"))
            )
        {
            collision.SendMessageUpwards("AddDamage", 1);
        }
    }

    public void AddScore(int amount)
    {
        score += amount;
    }

    private void updateUI()
    {
        if (scoreUI)
        {
            scoreUI.text = currentScore.ToString();
        }
    }

    private void OnEnable()
    {
        score = 0;
    }

    private void OnDisable()
    {
        hightestScoreUI.text = hightestScore.ToString();
    }
}
