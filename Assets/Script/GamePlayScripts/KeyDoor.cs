using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyDoor : MonoBehaviour
{
    public GameObject regularKey;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && PlayerInfo.hasKey)
        {
            gameObject.SetActive(false);
            PlayerInfo.hasKey = false;
            regularKey.SetActive(false);

        }
    }
}
