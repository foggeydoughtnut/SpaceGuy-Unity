using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BuildingBoss : MonoBehaviour
{
    public float health = 50f;
    private float maxHealth;
    public GameObject[] secondPhase;
    [SerializeField] private Slider healthSlider;
    [SerializeField] private GameObject healthBar;
    [SerializeField] private float attackDamage = 10f;
    [SerializeField] private float attackSpeed = 1f;
    private float canAttack;
    private bool isAlive;
    private bool activatedSecondPhase;

    private void Start()
    {
        activatedSecondPhase = false;
        isAlive = true;
        maxHealth = health;
        healthSlider.maxValue = maxHealth;
    }
    private void Update()
    {
        if (health <= maxHealth / 2 && !activatedSecondPhase)
        {
            foreach (GameObject turret in secondPhase)
            {
                turret.SetActive(true);
            }
            activatedSecondPhase = true;
        }
        canAttack += Time.deltaTime;
    }
    public void TakeDamage(float amount)
    {

        health -= amount;
        if (health <= 0)
        {
            healthSlider.value = 0f;
            Die(); 
        }
    }

    void Die()
    {
        isAlive = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Destroy(gameObject);
        healthBar.SetActive(false);
        return;
    }

    private void OnGUI()
    {
        float t = Time.deltaTime / 1f;
        healthSlider.value = Mathf.Lerp(healthSlider.value, health, t);
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
                    Debug.Log("Player Hurt");
                }
                else
                {
                    canAttack += Time.deltaTime;
                }
            }
        }

    }
}
