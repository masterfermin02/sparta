using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public float longIdleTime = 5f;
	public float speed = 2.5f;
	public float jumpForce = 2.5f;

	public Transform groundCheck;
	public LayerMask groundLayer;
	public float groundCheckRadius;
	public Joystick _joystick;

	// References
	private Rigidbody2D _rigidbody;
	private Animator _animator;

	// Long Idle
	private float _longIdleTimer;

	// Movement
	private Vector2 _movement;
	private bool _facingRight = true;
	private bool _isGrounded;

	// Attack
	private bool _isAttacking;

	void Awake()
	{
		_rigidbody = GetComponent<Rigidbody2D>();
		_animator = GetComponent<Animator>();
	}

	void Start()
    {
        
    }

    void Update()
    {
		if (_isAttacking == false) {
			// Movement
			float horizontalInput = Input.GetAxisRaw("Horizontal");
			if (horizontalInput == 0f)
            {
				horizontalInput = _joystick.Horizontal;

			}

			_movement = new Vector2(horizontalInput, 0f);

			// Flip character
			if (horizontalInput < 0f && _facingRight == true) {
				Flip();
			} else if (horizontalInput > 0f && _facingRight == false) {
				Flip();
			}
		}

		// Is Grounded?
		_isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

		// Is Jumping?
		if (Input.GetButtonDown("Jump") || _joystick.Vertical > 0f) {
			jump();
		}

		// Wanna Attack?
		if (Input.GetButtonDown("Fire1")) {
			Attack();
		}
	}

	public void jump()
    {
		if (CheckCanDoAction())
        {
			_rigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
		}

    }

	public void Attack()
    {
		if (CheckCanDoAction())
        {
			_movement = Vector2.zero;
			_rigidbody.velocity = Vector2.zero;
			_animator.SetTrigger("Attack");
		}
    }

	public void moveLeft()
    {
		if (_facingRight)
		{
			Flip();
		}
		_movement = Vector2.left;

	}

	public void moveRight()
    {
		if (!_facingRight)
        {
			Flip();
        }
		_movement = Vector2.right;
	}

	private bool CheckCanDoAction()
    {
		return _isGrounded == true && _isAttacking == false;

	}

	void FixedUpdate()
	{
		if (_isAttacking == false) {
			float horizontalVelocity = _movement.normalized.x * speed;
			_rigidbody.velocity = new Vector2(horizontalVelocity, _rigidbody.velocity.y);
		}
	}

	void LateUpdate()
	{
		_animator.SetBool("Idle", _movement == Vector2.zero);
		_animator.SetBool("IsGrounded", _isGrounded);
		_animator.SetFloat("VerticalVelocity", _rigidbody.velocity.y);

		// Animator
		if (_animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack")) {
			_isAttacking = true;
		} else {
			_isAttacking = false;
		}

		// Long Idle
		if (_animator.GetCurrentAnimatorStateInfo(0).IsTag("Idle")) {
			_longIdleTimer += Time.deltaTime;

			if (_longIdleTimer >= longIdleTime) {
				_animator.SetTrigger("LongIdle");
			}
		} else {
			_longIdleTimer = 0f;
		}
	}

	private void Flip()
	{
		_facingRight = !_facingRight;
		float localScaleX = transform.localScale.x;
		localScaleX = localScaleX * -1f;
		transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
	}
}
