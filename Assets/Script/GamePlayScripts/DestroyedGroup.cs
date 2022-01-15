using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyedGroup : MonoBehaviour
{
    public GameObject key;
    void Update()
    {
        if (gameObject.transform.childCount == 0)
        {
            if (key != null)
            {
                key.SetActive(true);
            }

        }
    }
}
