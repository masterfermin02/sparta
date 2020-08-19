using System.Collections;
using System.Collections.Generic;
using GooglePlayGames;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAttack : MonoBehaviour
{
    public Text scoreUI;
    public Text highestScoreUI;
    public Text gameOverScoreUI;

    private bool _isAttacking;
    private Animator _animator;
    private int score = 0;
    private int highestScore = 0;
    private int currentScore = 0;
    private AudioSource _audio;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _audio = GameObject.Find("BackGroundAudio").GetComponent<AudioSource>();
    }

    private void LateUpdate()
    {
        if (currentScore != score)
        {
            currentScore = score;
            UpdateUI();
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
            collision.SendMessageUpwards("AddDamage", 1, SendMessageOptions.DontRequireReceiver);
        }
    }

    public void AddScore(int amount)
    {
        score += amount;
    }

    private void UpdateUI()
    {
        if (scoreUI)
        {
            scoreUI.text = currentScore.ToString();
        }
    }

    private void UpdateHighestScore()
    {
        if (highestScore < score)
        {
            highestScore = score;
            OnAddScoreToLeaderBorad(score);
        }
    }

    private void OnEnable()
    {
        score = 0;
        if (!_audio.isPlaying)
        {
            _audio.Play();
        }
    }

    private void OnDisable()
    {
        UpdateHighestScore();

        if (highestScoreUI)
        {
            highestScoreUI.text = highestScore.ToString();
        }

        if (gameOverScoreUI)
        {
            gameOverScoreUI.text = score.ToString();
        }

        if (_audio)
        {
            _audio.Pause();
        }
        
    }

    public void OnAddScoreToLeaderBorad(int score)
    {
        if (PlayGamesPlatform.Instance.localUser.authenticated)
        {
            PlayGamesPlatform.Instance.ReportScore(score, GPGSIds.leaderboard_score, (bool success) =>
            {
                if (success)
                {
                    Debug.Log("Update Score Success");

                }
                else
                {
                    Debug.Log("Update Score Fail");
                }
            });
        }
    }
}
