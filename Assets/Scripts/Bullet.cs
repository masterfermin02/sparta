using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage = 1;
    public float speed = 2f;
    public Vector2 direction;

    public float livingTime = 3f;
    public Color initialColor = Color.white;
    public Color finalColor;
    public GameObject explosionEffect;

    private SpriteRenderer _renderer;
    private float _startingTime;
    private Rigidbody2D _rigidBody;
    private bool _returning = false;


    void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _startingTime = Time.time;

        Destroy(this.gameObject, livingTime);
    }

    // Update is called once per frame
    void Update()
    {
        float _timeSinceStarted = Time.time - _startingTime;
        float _percentageCompleted = _timeSinceStarted / livingTime;

        _renderer.color = Color.Lerp(initialColor, finalColor, _percentageCompleted);
        if (_percentageCompleted >= 1f)
        {
            explosion();
        }
    }

    private void FixedUpdate()
    {
        Vector2 movement = direction.normalized * speed;
        _rigidBody.velocity = movement;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!_returning && collision.CompareTag("Player"))
        {
            collision.SendMessageUpwards("AddDamage", damage);
            explosion();
        }

        if (_returning && collision.CompareTag("Enemy"))
        {
            collision.SendMessageUpwards("AddDamage", 1);
            explosion();
        }
    }

    public void AddDamage(int amount)
    {
        _returning = true;
        direction = direction * -1f;
    }

    private void explosion()
    {
        speed = 0;
        _renderer.color = Color.Lerp(initialColor, finalColor, 1f);

        if (explosionEffect)
        {
            explosionEffect.SetActive(true);
        }

        Destroy(gameObject, 0.5f);
    }
}
