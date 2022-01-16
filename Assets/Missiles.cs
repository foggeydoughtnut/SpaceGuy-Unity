using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missiles : MonoBehaviour
{
    public int missiles;
    private MissileCount missileCount;

    private void Start()
    {
        missileCount = FindObjectOfType<MissileCount>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Player Entered");
        if (collision.gameObject.tag == "Player")
        {
            missileCount.GiveMissile(missiles);
            Destroy(gameObject);
        }
    }
}
