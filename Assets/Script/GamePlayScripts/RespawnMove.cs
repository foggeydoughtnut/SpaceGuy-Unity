using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnMove : MonoBehaviour
{
    public GameObject respawnPoint;



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Respawn Point Changed");
            respawnPoint.transform.position = gameObject.transform.position;
        }
    }
}
