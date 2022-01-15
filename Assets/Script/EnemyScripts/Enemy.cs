using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody2D rb;
    private Transform player;
    public float speed = 1f;
    public float rotationSpeed = 1f;
    public float distanceToTrack = 1f;
    [SerializeField] private float attackDamage = 10f;
    [SerializeField] private float attackSpeed = 1f;
    private float canAttack;
    public Animator animator;
    public float health = 50f;
    private bool isAlive;
    public int points;
    bool gavePoints;

    public GameObject playerGameObject;

    // Start is called before the first frame update
    void Start()
    {
        gavePoints = false;
        isAlive = true;
        rb = this.GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player").transform;
    }


    private void Update()
    {
        if (isAlive)
        {
            Vector3 direction = player.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
            rb.rotation = angle;
            canAttack += Time.deltaTime;
        }

    }

    private void FixedUpdate()
    {
        if (isAlive)
        {
            float distance = Vector3.Distance(player.transform.position, rb.transform.position);
            if (distance < distanceToTrack)
            {
                rb.MovePosition(Vector3.MoveTowards(transform.position, player.position, speed * Time.fixedDeltaTime));
            }
        }

    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0)
        {
            Die();
        }

    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (isAlive)
        {
            if (collision.gameObject.tag == "Player")
            {
                if (attackSpeed <= canAttack)
                {
                    collision.gameObject.GetComponent<PlayerHealth>().UpdateHealth(-attackDamage);
                    canAttack = 0f;
                    FindObjectOfType<AudioManager>().Play("PlayerHurt");
                }
                else
                {
                    canAttack += Time.deltaTime;
                }
            }
        }

    }



    private void Die()
    {
        animator.SetBool("IsDead", true);
        FindObjectOfType<AudioManager>().Play("RegEnemyDeath");
        isAlive = false;
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
}



