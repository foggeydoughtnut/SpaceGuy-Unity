using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawner : MonoBehaviour
{
    public GameObject objectToSpawn;
    public float spawnTime = 10f;
    private float currentTime;
    private bool canSpawn;
    public float spawnRadius = 10f;
    private Transform player;
    private Rigidbody2D rb;
    public Animator animator;
    private bool isDead;
    public int points;
    private bool gavePoints;


    public GameObject playerGameObject;

    public float health = 50f;

    // Start is called before the first frame update
    void Start()
    {
        gavePoints = false;
        isDead = false;
        player = GameObject.FindWithTag("Player").transform;
        rb = this.GetComponent<Rigidbody2D>();
        currentTime = 0;
        canSpawn = false;
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(player.transform.position, rb.transform.position);
        if (distance <= spawnRadius)
        {
            canSpawn = true;
        } 
        else
        {
            canSpawn = false;
        }
        currentTime += Time.deltaTime;
        if (currentTime >= spawnTime)
        {
            SpawnEnemy(objectToSpawn);
            currentTime = 0;
        }
    }

    public void SpawnEnemy(GameObject obj)
    {
        if (canSpawn)
        {
            GameObject newObj = Instantiate(obj, transform);
            newObj.transform.parent = null;
        }
        
    }


    public void TakeDamage(float amount)
    {
        health -= amount;

        if (health <= 0) Die();

    }

    private void Die()
    {
        if (!isDead)
        {
            animator.SetBool("IsDead", true);
            FindObjectOfType<AudioManager>().Play("SpawnerDeath");
            isDead = true;
            /*PlayerInfo.UpdateScore(points);*/
            if (!gavePoints)
            {
                playerGameObject.GetComponent<PlayerInfo>().UpdateScore(points);
                gavePoints = true;
            }
            
        }

    }

    private void DestroyObject()
    {
        Destroy(gameObject);
    }

}
