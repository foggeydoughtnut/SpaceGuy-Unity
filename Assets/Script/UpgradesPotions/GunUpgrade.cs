using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunUpgrade : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerInfo.hasShotgun = false;
            FindObjectOfType<AudioManager>().Play("PowerUp");
            Debug.Log("Gun upgrade picked up");
            collision.gameObject.GetComponent<Shooting>().canShootFast = true;
            Destroy(gameObject);
        }
    }
}
