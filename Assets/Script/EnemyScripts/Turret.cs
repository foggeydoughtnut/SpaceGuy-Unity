using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    private Rigidbody2D rb;
    private Transform player;
    public float rotationSpeed = 1f;
    public float distanceToTrack = 1f;
    public Animator animator;
    public float health = 50f;
    private bool isAlive;
    public int points;


    public Transform firePoint;
    public GameObject bulletPrefab;
    public float bulletForce = 20f;
    public float shotCooldown = 0.5f;
    private bool canShoot;
    private float cooldownTimer;



    private bool coolingDown = false;
    public float shootingDuration = 2f; // How long the turret will shoot for before cooling down
    public float burstCooldownTime; // How long it takes to cooldown, before shooting again
    private float shootTimeCurrent; // How long it has been shooting for

    public GameObject playerGameObject;

    private bool gavePoints;


    // Start is called before the first frame update
    void Start()
    {
        gavePoints = false;
        canShoot = true;
        cooldownTimer = Time.time;
        isAlive = true;
        rb = this.GetComponent<Rigidbody2D>();
        /*player = GameObject.FindWithTag("Player").transform;*/
        shootTimeCurrent = 0.0f;
        
    }

    private void Update()
    {
        player = GameObject.FindWithTag("Player").transform;
        Vector3 direction = player.position - transform.position;
        if (isAlive)
        {
            //Debug.Log(direction);
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
            rb.rotation = angle;
        }

        RaycastHit2D rayInfo = Physics2D.Raycast(transform.position, direction, distanceToTrack);

        

        if (coolingDown)
        {
            shootTimeCurrent -= Time.deltaTime;
        }
        if (shootTimeCurrent >= shootingDuration && !coolingDown)
        {
            coolingDown = true;
            shootTimeCurrent = burstCooldownTime;
        }
        else if (shootTimeCurrent <= 0)
        {
            coolingDown = false;
            shootTimeCurrent = 0;
        }


        float distance = Vector3.Distance(player.transform.position, rb.transform.position);
        if (distance < distanceToTrack)
        {
            if (!coolingDown)
            {
                shootTimeCurrent += Time.deltaTime;
            }
            

            if (canShoot && !coolingDown)
            {
                Shoot();
            }
            else if (Time.time > cooldownTimer)
            {
                 canShoot = true;
            }

        }

        



    }

    private void FixedUpdate()
    {

    }

    private void OnDrawGizmosSelected()
    {

        Gizmos.DrawWireSphere(transform.position, distanceToTrack);
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0)
        {
            Die();
        }

    }

    private void Die()
    {
        animator.SetBool("IsDead", true);
        FindObjectOfType<AudioManager>().Play("RegEnemyDeath");
        isAlive = false;
        /* PlayerInfo.UpdateScore(points);*/
        if (!gavePoints)
        {
            playerGameObject.GetComponent<PlayerInfo>().UpdateScore(points);
            gavePoints = true;
        }
        
    }

    private void DestroyObject()
    {
        Destroy(gameObject);
    }

    void Shoot()
    {
        
        //Debug.Log("Shoot");
        FindObjectOfType<AudioManager>().Play("EnemyShoot");
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
        canShoot = false;
        cooldownTimer = Time.time + shotCooldown;
    }
}
