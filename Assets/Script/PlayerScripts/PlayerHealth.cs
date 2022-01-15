using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    private float health = 0f;
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private Slider healthSlider;
    private int lives;

    public int pointsForLife;
    private int lastLifePoints;
    

    private Rigidbody2D rb;

    private LifeCount lifeCount;

    private void Awake()
    {
        /*lives = PlayerPrefs.GetInt("PlayerCurrentLives");*/
    }
    // Start is called before the first frame update
    void Start()
    {
        lifeCount = FindObjectOfType<LifeCount>();
/*        Debug.Log(lives);
        lives += 1;
        Debug.Log("After Add: " + lives);
        
                lifeCount.UpdateText(lives);*/

        health = maxHealth;
        healthSlider.maxValue = maxHealth;
        rb = this.GetComponent<Rigidbody2D>();

        lastLifePoints = PlayerInfo.totalPoints;
        
    }

    private void Update()
    {
        if (PlayerInfo.totalPoints >= lastLifePoints + pointsForLife)
        {
            lifeCount.GiveLife();
            lastLifePoints = PlayerInfo.totalPoints;
        }

        
        /*if (PlayerInfo.totalPoints >= 50)
        {
            lifeCount.GiveLife();
        }*/


    }

    public void UpdateHealth(float mod)
    {
        health += mod;

        if (health > maxHealth)
        {
            health = maxHealth;
        }
        else if (health <= 0f)
        {
            if (lifeCount.getCurrentLives() - 1 == -1)
            {
                PlayerInfo.totalPoints = 0;
                SceneManager.LoadScene("GameOver");
            } else
            {
                lifeCount.TakeLife();
                PlayerPrefs.SetInt("PlayerCurrentLives", lifeCount.getCurrentLives());
                health = 0f;
                healthSlider.value = 0f;


                /*Debug.Log(lives); */
                transform.position = GameObject.FindWithTag("Respawn").transform.position;
                gameObject.GetComponent<Shooting>().canShootFast = false;
                PlayerInfo.hasShotgun = false;
                health = maxHealth;
                healthSlider.value = health;
            }
            
        }
    }

/*    public void addLife()
    {
        Debug.Log(lives);
        lives += 1;
        
        PlayerPrefs.SetInt("PlayerCurrentLives", lives);
        Debug.Log("Added Lives : " + lives);
        lifeCount.UpdateText(lives);
    }*/


    private void OnGUI() 
    {
        float t = Time.deltaTime / 1f;
        healthSlider.value = Mathf.Lerp(healthSlider.value, health, t);
    }



}
