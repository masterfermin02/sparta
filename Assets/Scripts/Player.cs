using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D _player;

    private SpriteRenderer _playerSpriteRenderer;

    public float speedforce;
    
    private void Awake() {
        _player = GetComponent<Rigidbody2D>();
        _playerSpriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");

        _player.velocity = new Vector2(horizontalInput * speedforce, _player.velocity.y);

        _playerSpriteRenderer.flipX = (horizontalInput < 0);
        
    }
}
