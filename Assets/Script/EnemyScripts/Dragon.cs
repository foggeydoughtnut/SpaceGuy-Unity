using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Dragon : MonoBehaviour
{
    private Rigidbody2D rb;
    private Transform player;
    public float speed = 1f;
    public float rotationSpeed = 1f;
    public float distanceToTrack = 1f;
    [SerializeField] private float attackDamage = 10f;
    [SerializeField] private float attackSpeed = 1f;
    [SerializeField] private float dashSpeed = 5f;
    private float dashCooldown;
    private float canAttack;
    private float enemySpeed;
    public float dashCooldownMax;
    public float tolerance;
    private Vector3 dashLocation;
    private bool isDashing;
    public float health = 50f;



    public Transform firePoint;
    public GameObject bulletPrefab;
    public float bulletForce = 20f;
    public float shotCooldown = 0.5f;
    private bool canShoot;
    private float cooldownTimer;


    // Start is called before the first frame update
    void Start()
    {
        canShoot = true;
        enemySpeed = speed;
        rb = this.GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player").transform;
        isDashing = false;
        cooldownTimer = Time.time;
    }

    private void Update()
    {        
        canAttack += Time.deltaTime;
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

    private void FixedUpdate()
    {
        float distance = Vector3.Distance(player.transform.position, rb.transform.position);
        if (distance < distanceToTrack)
        {
            if (dashCooldown <= 0 || isDashing)
            {
                DashAttack();

            }
            else
            {
                GoToPlayer();
                dashCooldown -= Time.fixedDeltaTime;
            }
        }
    }

    private void GoToPlayer()
    {
        Debug.Log("Moving towards player");
        enemySpeed = speed;
        Vector3 direction = player.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        
        float t = Time.deltaTime / rotationSpeed;
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(transform.rotation.x, transform.rotation.y, angle), Time.fixedDeltaTime * rotationSpeed);
        rb.MovePosition(Vector3.MoveTowards(transform.position, player.position, enemySpeed * Time.fixedDeltaTime));
    }

    private void DashAttack()
    {
        if (!isDashing)
        {
            Debug.Log("Dash attack starting");
            dashLocation = player.transform.position;
            Vector3 direction = dashLocation - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
            rb.rotation = angle;
            isDashing = true;
            
        }
        else
        {
            enemySpeed = dashSpeed;
            if (Vector3.Distance(gameObject.transform.position, dashLocation) > tolerance)
            {
                rb.MovePosition(Vector3.MoveTowards(transform.position, dashLocation, enemySpeed * Time.fixedDeltaTime));
            }
            else
            {
                isDashing = false;
                dashCooldown = dashCooldownMax;
            }
        }
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0) Die();

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Walls")
        {
            Debug.Log("Dragon hit wall");
            isDashing = false;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (attackSpeed <= canAttack)
            {
                collision.gameObject.GetComponent<PlayerHealth>().UpdateHealth(-attackDamage);
                canAttack = 0f;
                Debug.Log("Player Hurt");
            }
            else
            {
                canAttack += Time.deltaTime;
            }
        }
    }

   void Die()
    {
        FindObjectOfType<AudioManager>().Play("DragonDeath");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Destroy(gameObject);
    }



    void Shoot()
    {
        Debug.Log("Shoot");
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
        canShoot = false;
        cooldownTimer = Time.time + shotCooldown;
    }


}
