using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject bulletPrefab;
    public GameObject shooter;

    private Transform _firePoint;

    void Awake()
    {
        _firePoint = transform.Find("FirePoint");
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Shoot()
    {
        if (canShoot()) {
            GameObject myBullet = Instantiate(bulletPrefab, _firePoint.position, Quaternion.identity) as GameObject;
            Bullet bulletComponent = myBullet.GetComponent<Bullet>();
            bulletComponent.direction = getShooterDirection();
        }

    }

    private bool canShoot() 
    {
        return bulletPrefab != null && _firePoint != null && shooter != null;
    }

    private Vector2 getShooterDirection() 
    {
        if (shooter.transform.localScale.x < 0f) {
            return Vector2.left;
        }

        return Vector2.right;
    }
}
