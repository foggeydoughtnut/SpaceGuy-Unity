using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunUpgrade : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            FindObjectOfType<AudioManager>().Play("PowerUp");
            Debug.Log("Shotgun picked up");
            PlayerInfo.hasShotgun = true;
            collision.gameObject.GetComponent<Shooting>().canShootFast = false;
            Destroy(gameObject);
        }
    }
}
