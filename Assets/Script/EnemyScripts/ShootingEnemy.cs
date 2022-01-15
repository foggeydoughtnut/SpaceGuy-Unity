using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingEnemy : MonoBehaviour
{
    public float distanceToTrack = 1f;
    public Transform firePoint;
    public GameObject bulletPrefab;
    public float bulletForce = 20f;
    public float shotCooldown = 0.5f;
    private bool canShoot;
    private float cooldownTimer;
    private Transform player;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        canShoot = true;
        cooldownTimer = Time.time;
        rb = this.GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(player.transform.position, rb.transform.position);
        if (distance < distanceToTrack)
        {
            if (canShoot) Shoot();
            if (!canShoot)
            {
                if (Time.time > cooldownTimer) canShoot = true;
            }
        }
    }


    void Shoot()
    {
        FindObjectOfType<AudioManager>().Play("EnemyShoot");
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
        canShoot = false;
        cooldownTimer = Time.time + shotCooldown;
    }
}
