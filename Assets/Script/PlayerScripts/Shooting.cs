using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    PlayerControls controls;

    private bool isHoldingTrigger;

    public Transform firePoint;
    public GameObject bulletPrefab;
    public GameObject shotgunBullet;

    public float bulletForce = 20f;

    public float shotgunCooldown = 0.5f;
    public float slowShotCooldown = 0.5f;
    public float fastShotCooldown = 0.15f;
    private float shotCooldown;
    public float fastShootingTime = 5f;
    public bool canShootFast = false;
    [HideInInspector]
    public bool canShoot = true;
    private float cooldownTimer;

    [Header("Shotgun")]
    public float maxSpread;
    public int pelletCount;
/*    List<Quaternion> pellets;*/
    



    private void Awake()
    {
        controls = new PlayerControls();
/*        pellets = new List<Quaternion>(pelletCount);
        for (int i = 0; i < pelletCount; i++)
        {
            pellets.Add(Quaternion.Euler(Vector3.zero));
        }*/
    }

    private void OnEnable()
    {
        controls.Gameplay.Enable();
    }

    private void OnDisable()
    {
        controls.Gameplay.Disable();
    }


    private void Start()
    {
        controls.Gameplay.Shoot.performed += ctx => setTriggerShooting(true);
        controls.Gameplay.Shoot.canceled += ctx => setTriggerShooting(false);
        cooldownTimer = Time.time;

        if (canShootFast)
        {
            shotCooldown = fastShotCooldown;
        } else if (PlayerInfo.hasShotgun)
        {
            shotCooldown = shotgunCooldown;
        } else
        {
            shotCooldown = slowShotCooldown;
        }
        
    }

    private void setTriggerShooting(bool status)
    {
        isHoldingTrigger = status;
    }
    private void Update()
    {
/*        Debug.Log(isHoldingTrigger);*/
        if (isHoldingTrigger) Shoot();
    }

    IEnumerator CanShoot()
    {
        canShoot = false;
        yield return new WaitForSeconds(shotCooldown);
        canShoot = true;
    }

    void Shoot()
    {
       
        if (isHoldingTrigger)
        {
            if (PauseManager.paused) return;
            if (!canShoot) return;
            if (PlayerInfo.hasShotgun) shotCooldown = shotgunCooldown;
            else if (canShootFast) shotCooldown = fastShotCooldown;
            else shotCooldown = slowShotCooldown;

            if (PlayerInfo.hasShotgun)
            {
                Debug.Log("Shooting Shotgun");

                /*                int i = 0;
                                foreach (Quaternion quat in pellets)
                                {
                                    pellets[i] = Random.rotation;
                                    GameObject p = Instantiate(shotgunBullet, firePoint.position, firePoint.rotation);
                                    p.transform.rotation = Quaternion.RotateTowards(p.transform.rotation, pellets[i], maxSpread);
                                    p.GetComponent<Rigidbody2D>().AddForce(p.transform.right * bulletForce);

                                    i++;
                                }*/

                for (int i = 0; i <= pelletCount; i ++)
                {
                    /*GameObject spawnedBullet = Instantiate(shotgunBullet, firePoint.position, firePoint.rotation);
                    Rigidbody2D rb = spawnedBullet.GetComponent<Rigidbody2D>();
                    rb.AddForce(firePoint.up * bulletForce + new Vector3(0f, 0f, Random.Range(-maxSpread, maxSpread), ForceMode2D.Impulse));*/
                    GameObject spawnedBullet = Instantiate(shotgunBullet, firePoint.position, firePoint.rotation);
                    Rigidbody2D rb = spawnedBullet.GetComponent<Rigidbody2D>();
                    spawnedBullet.transform.Rotate(new Vector3(0, 0, -(maxSpread / 2) + (maxSpread / pelletCount) * i));
                    rb.AddForce(spawnedBullet.transform.up * bulletForce, ForceMode2D.Impulse);

                }


/*                GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation * Quaternion.Euler(0, 0, Random.Range(-maxSpread, maxSpread)));
                Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);*/





                FindObjectOfType<AudioManager>().Play("PlayerShoot");
            }
            else
            {
                GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
                Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
                FindObjectOfType<AudioManager>().Play("PlayerShoot");
            }
            
            StartCoroutine(CanShoot());
        }

    }
}
