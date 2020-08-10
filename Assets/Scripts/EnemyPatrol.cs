using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public float speed = 1f;
    public float minX;
    public float maxX;
    public float waitingTime = 2f;
    public bool facingRight = true;

    private GameObject _target;
    private Animator _animator;
    private Weapon _weapon;

    void Awake()
    {
        _animator = GetComponent<Animator>();
        _weapon = GetComponentInChildren<Weapon>();
    }

    // Start is called before the first frame update
    void Start()
    {

        UpdateTarget();
        StartCoroutine("PatrolToTarget");
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void UpdateTarget()
    {
        if (_target == null) {
            _target = new GameObject("Target");
            _target.transform.position = new Vector2(minX, transform.position.y);
            if (facingRight) {
               transform.localScale = new Vector3(-1, 1, 1);
            }

            return;
        }

        if (_target.transform.position.x == minX && facingRight) {
            _target.transform.position = new Vector2(maxX, transform.position.y);
            transform.localScale = new Vector3(1, 1, 1);
            return;
        }

        if (_target.transform.position.x == minX && !facingRight)
        {
            _target.transform.position = new Vector2(maxX, transform.position.y);
            transform.localScale = new Vector3(-1, 1, 1);
            return;
        }

        if (facingRight)
        {
            _target.transform.position = new Vector2(minX, transform.position.y);
            transform.localScale = new Vector3(-1, 1, 1);
            return;
        }

        _target.transform.position = new Vector2(minX, transform.position.y);
        transform.localScale = new Vector3(1, 1, 1);

    }

    private IEnumerator PatrolToTarget()
    {
        while(Vector2.Distance(transform.position, _target.transform.position) > 0.05f) {
            
            _animator.SetBool("Idle", false);

            Vector2 direction = _target.transform.position - transform.position;

            transform.Translate(direction.normalized * speed * Time.deltaTime);

            yield return null;
        }

        Debug.Log("Target reached");
        transform.position = new Vector2(_target.transform.position.x, transform.position.y);
        UpdateTarget();
        _animator.SetBool("Idle", true);
        _animator.SetTrigger("Shoot");

        Debug.Log("Waiting for " + waitingTime + " seconds");
        yield return new WaitForSeconds(waitingTime);

        Debug.Log("Waited enough, let's update the target and move again");
        StartCoroutine("PatrolToTarget");
    }

    public void Shoot()
    {
        if (_weapon != null)
        {
            _weapon.Shoot();
        }
    }
}
