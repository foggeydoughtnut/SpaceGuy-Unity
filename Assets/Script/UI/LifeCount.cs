using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(TMPro.TextMeshProUGUI))]
public class LifeCount : MonoBehaviour
{
    private TMPro.TextMeshProUGUI text;
    public int startingLives;
    private int lifeCounter { get; set; }

    private int pointsSinceLastLife;

    private void Start()
    {
        lifeCounter = startingLives;

        text = GetComponent<TMPro.TextMeshProUGUI>();
    }

    private void Update()
    {
        text.SetText("x " + lifeCounter);
        
    }

    public void GiveLife()
    {
        Debug.Log("Gave Life");
        lifeCounter++;
    }

    public void TakeLife()
    {
        lifeCounter--;
    }


    
    public int getCurrentLives()
    {
        return lifeCounter;
    }
        
}

    /*    public void UpdateText(int value)
        {
            text.SetText("x " + value);
        }*/

