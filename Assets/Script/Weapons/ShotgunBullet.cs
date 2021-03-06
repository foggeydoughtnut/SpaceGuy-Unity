using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunBullet : MonoBehaviour
{
    public GameObject hitEffect;
    public float damage;
    public float timeToDespawn;

    public GameObject bullet;
    public GameObject missile;

    private Transform player;
    private Transform playerSecondCollider;

    private void Start()
    {

        player = GameObject.FindWithTag("Player").transform;
        playerSecondCollider = GameObject.FindWithTag("PlayerCollider").transform;

        StartCoroutine(SelfDestruct());
        bullet = GameObject.FindGameObjectWithTag("ShotgunShell");
        missile = GameObject.FindGameObjectWithTag("Missile");

        Physics2D.IgnoreCollision(bullet.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        Physics2D.IgnoreCollision(missile.GetComponent<Collider2D>(), GetComponent<Collider2D>());

        Physics2D.IgnoreCollision(player.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        Physics2D.IgnoreCollision(playerSecondCollider.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
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
        Damage(collision.transform);
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
        if (enemy.gameObject.tag == "ShotgunShell")
        {
            return;            
        }
        Destroy(gameObject);
    }
}
