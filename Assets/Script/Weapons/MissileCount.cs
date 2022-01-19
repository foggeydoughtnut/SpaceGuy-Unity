using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileCount : MonoBehaviour
{
    private TMPro.TextMeshProUGUI text;
    public int startingMissiles;
    private int missileCounter { get; set; }

    private void Start()
    {
        
        missileCounter = startingMissiles;

        text = GetComponent<TMPro.TextMeshProUGUI>();
    }

    private void Update()
    {
        text.SetText("x " + missileCounter);

    }

    public void GiveMissile(int missiles)
    {
        Debug.Log("Gave Missile");
        
        if (missileCounter + missiles > startingMissiles)
        {
            missileCounter = startingMissiles;
        } else
        {
            missileCounter += missiles;
        }

/*        missileCounter += missiles;
        if (missileCounter > startingMissiles)
        {
            missileCounter = startingMissiles;
        }*/
    }

    public void TakeMissile()
    {
        missileCounter--;
    }



    public int getCurrentMissiles()
    {
        return missileCounter;
    }
}
