using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private bool _isAttacking;
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void LateUpdate()
    {
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
}
