using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    public GameObject hitEffect;
    public float damage;
    public float timeToDespawn;
    public float explosionRadius;

    public GameObject playerCollider;

    private Transform player;
    private Transform playerSecondCollider;

    public Animator animator;


    private void Start()
    {

        player = GameObject.FindWithTag("Player").transform;
        playerSecondCollider = GameObject.FindWithTag("PlayerCollider").transform;
        StartCoroutine(SelfDestruct());
        Physics2D.IgnoreCollision(player.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        Physics2D.IgnoreCollision(playerSecondCollider.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
    }

    private void Update()
    {
        player = GameObject.FindWithTag("Player").transform;
        Vector3 direction = player.position - transform.position;
        RaycastHit2D rayInfo = Physics2D.Raycast(transform.position, direction, explosionRadius);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("MainCamera"))
        {
            Destroy(gameObject);
        }
    }


    IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(timeToDespawn);
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "Player")
        {
            Explode();

        }
        

    }

    void Explode()
    {
        
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
        
        foreach (Collider2D collider in colliders)
        {

            Vector2 direction = collider.transform.position - transform.position;

            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, direction.magnitude, LayerMask.GetMask("Walls"));

            if (!hit && collider.CompareTag("Enemy"))
            {
                Damage(collider.transform);
            }
        }
        animator.SetBool("hitObject", true);
        Debug.Log("I hit something");
        /*startExplosion();*/

        FindObjectOfType<AudioManager>().Play("RegEnemyDeath");
    }

    private void startExplosion()
    {
        gameObject.transform.localScale.x.Equals(5);
        gameObject.transform.localScale.y.Equals(5);
    }


    void Damage(Transform enemy)
    {
        Enemy e = enemy.GetComponent<Enemy>();
        spawner sp = enemy.GetComponent<spawner>();
        Dragon dr = enemy.GetComponent<Dragon>();
        Turret tr = enemy.GetComponent<Turret>();
        BuildingBoss bb = enemy.GetComponent<BuildingBoss>();
        Explosion ep = enemy.GetComponent<Explosion>();

        if (e != null)
        {
            e.TakeDamage(damage);

        }
        if (sp != null)
        {
            sp.TakeDamage(damage);
        }
        if (dr != null)
        {
            dr.TakeDamage(damage);
        }
        if (tr != null)
        {
            tr.TakeDamage(damage);
        }
        if (bb != null)
        {
            bb.TakeDamage(damage);
        }
        if (ep != null)
        {
            ep.TakeDamage(damage);
        }

    }

    private void OnDrawGizmosSelected()
    {

        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }

    private void DestroyObject()
    {
        Destroy(gameObject);
    }
}
