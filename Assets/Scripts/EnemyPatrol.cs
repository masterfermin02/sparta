﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public float speed = 1f;
    public float waitingTime = 2f;
    public float wallAware = 0.5f;
    public LayerMask groundLayer;
    public float playerAware = 3f;
    public float aimingTime = 0.5f;
    public float shootingTime = 1.5f;
    public int shootingRepeat = 1;
    public Transform floorPoint;

    private Animator _animator;
    private Weapon _weapon;
    private Rigidbody2D _rigidbody;
    private bool _facingRight;
    private bool _attacking = false;
    private AudioSource _audio;

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _weapon = GetComponentInChildren<Weapon>();
        _audio = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        if (transform.localScale.x < 0f)
        {
            _facingRight = false;
        } else if (transform.localScale.x > 0f)
        {
            _facingRight = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 direction = Vector2.right;
        if (!_facingRight)
        {
            direction = Vector2.left;
        }

        if(!_attacking)
        {
            if (Physics2D.Raycast(transform.position, direction, wallAware, groundLayer))
            {
                Flip();
            }
        }
    }

    void Flip()
    {
        _facingRight = !_facingRight;
        transform.localScale = new Vector3(FlipFloat(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        if (floorPoint)
        {
            floorPoint.localScale = new Vector3(FlipFloat(floorPoint.localScale.x), transform.localScale.y, transform.localScale.z);
        }
    }

    float FlipFloat(float value)
    {
        return value * -1f;
    }

    private void LateUpdate()
    {
        _animator.SetBool("Idle", _rigidbody.velocity == Vector2.zero);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!_attacking && collision.CompareTag("Player") && gameObject.activeSelf)
        {
            StartCoroutine("AimAndShoot");
        }
    }

    private IEnumerator AimAndShoot()
    {
        _attacking = true;

        yield return new WaitForSeconds(aimingTime);

         _animator.SetTrigger("Shoot");

        yield return new WaitForSeconds(shootingTime);

        _attacking = false;
    }

    private void FixedUpdate()
    {
        _rigidbody.velocity = new Vector2(GetSpeed(), _rigidbody.velocity.y);
    }

    private float GetSpeed()
    {
        if (_attacking)
        {
            return 0f;
        }

        if (!_facingRight)
        {
            return speed * -1f;
        }

        return speed;
    }

    public void Shoot()
    {
        if (_weapon != null)
        {
            StartCoroutine("MultiShooting");
        }
    }

    private IEnumerator MultiShooting()
    {
        int shoot = 0;

        while (shoot < shootingRepeat)
        {
            _weapon.Shoot();
            _audio.Play();
            shoot++;
            yield return new WaitForSeconds(0.2f);
        }
    }

    private void OnEnable()
    {
        _attacking = false;
    }

    private void OnDisable()
    {
        _attacking = false;
        StopCoroutine("AimAndShoot");
        StopCoroutine("MultiShooting");
    }
}
