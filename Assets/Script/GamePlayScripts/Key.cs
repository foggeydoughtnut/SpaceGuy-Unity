using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    public bool isBossKey;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Key Picked up");
            if (!isBossKey)
            {
                PlayerInfo.hasKey = true;
            } else
            {
                PlayerInfo.hasBossKey = true;
            }
            
            Destroy(gameObject);
            
        }
    }
}
