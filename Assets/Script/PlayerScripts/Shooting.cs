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
    
    
    [Header("Missile")]
    public float missileShotCooldown;
    public float missileForce = 20f;
    public GameObject missilePrefab;
    public Transform fireMissilePoint1;
    public Transform fireMissilePoint2;
    private bool canShootMissile = true;


    private void Awake()
    {
        controls = new PlayerControls();
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
        controls.Gameplay.MissileLaunch.performed += ctx => MissileLaunch();
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

    private void MissileLaunch()
    {
        Debug.Log("Launched Missile"); 
        if (PauseManager.paused || !canShootMissile) return;

        GameObject missile1 = Instantiate(missilePrefab, fireMissilePoint1.position, fireMissilePoint1.rotation);
        GameObject missile2 = Instantiate(missilePrefab, fireMissilePoint2.position, fireMissilePoint2.rotation);
        Rigidbody2D rb1 = missile1.GetComponent<Rigidbody2D>();
        Rigidbody2D rb2 = missile2.GetComponent<Rigidbody2D>();
        rb1.AddForce(fireMissilePoint1.up * missileForce, ForceMode2D.Impulse);
        rb2.AddForce(fireMissilePoint2.up * missileForce, ForceMode2D.Impulse);
        
        FindObjectOfType<AudioManager>().Play("PlayerShoot");
        StartCoroutine(CanShootMissile());
    }

    private void setTriggerShooting(bool status)
    {
        isHoldingTrigger = status;
    }
    private void Update()
    {
        if (isHoldingTrigger) Shoot();
    }

    IEnumerator CanShoot()
    {
        canShoot = false;
        yield return new WaitForSeconds(shotCooldown);
        canShoot = true;
    }

    IEnumerator CanShootMissile()
    {
        canShootMissile = false;
        yield return new WaitForSeconds(missileShotCooldown);
        canShootMissile = true;
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

                for (int i = 0; i <= pelletCount; i ++)
                {

                    GameObject spawnedBullet = Instantiate(shotgunBullet, firePoint.position, firePoint.rotation);
                    Rigidbody2D rb = spawnedBullet.GetComponent<Rigidbody2D>();
                    spawnedBullet.transform.Rotate(new Vector3(0, 0, -(maxSpread / 2) + (maxSpread / pelletCount) * i));
                    rb.AddForce(spawnedBullet.transform.up * bulletForce, ForceMode2D.Impulse);

                }

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
