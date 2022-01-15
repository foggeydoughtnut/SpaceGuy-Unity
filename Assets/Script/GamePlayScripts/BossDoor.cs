using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDoor : MonoBehaviour
{
    public GameObject bossKey;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && PlayerInfo.hasBossKey)
        {
            gameObject.SetActive(false);            
            PlayerInfo.hasBossKey = false;
            bossKey.SetActive(false);
        }
    }

}
