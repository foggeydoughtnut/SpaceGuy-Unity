using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyedEverythingDoor : MonoBehaviour
{
    public GameObject door;
    public GameObject gunUpgrade;
    public GameObject shotgun;
    private void Update()
    {
        if (gameObject.transform.childCount == 0)
        {
            Debug.Log("Everything is dead");
            door.SetActive(false);
            if (gunUpgrade != null)
            {
                gunUpgrade.SetActive(true);   
            }
            if (shotgun != null)
            {
                shotgun.SetActive(true);
            }

        } 
    }
}
