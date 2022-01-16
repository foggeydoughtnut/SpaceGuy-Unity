using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{

    public Animator animator;
    public float health = 50f;
    public float explosionRadius;
    public float damage;
    public float delayForExplosion;

    private Transform player;
    private bool canTakeDamage;

    // Start is called before the first frame update
    void Start()
    {
        canTakeDamage = true;
    }

    private void Update()
    {
        player = GameObject.FindWithTag("Player").transform;
        Vector3 direction = player.position - transform.position;
        RaycastHit2D rayInfo = Physics2D.Raycast(transform.position, direction, explosionRadius);
    }
    public void TakeDamage(float amount)
    {
        if (canTakeDamage)
        {
            health -= amount;
            if (health <= 0)
            {
                StartCoroutine(Delay());
            }
        }


    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(delayForExplosion);
        Die();
    }


    private void Die()
    {
        canTakeDamage = false;
        Explode();
        animator.SetBool("isDead", true);
        FindObjectOfType<AudioManager>().Play("RegEnemyDeath");
    }

    void Explode()
    {
        Debug.Log("BOOOOOOM");
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
        Debug.Log(colliders.Length);
        foreach (Collider2D collider in colliders)
        {
            
            if (collider.tag == "Enemy")
            {
                Damage(collider.transform);
            }
        }
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
