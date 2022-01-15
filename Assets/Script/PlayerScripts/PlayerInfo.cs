using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    public static bool hasKey;
    public static bool hasBossKey;
    public static bool hasShotgun;
    public static int totalPoints;
    public static int startingLives;
    public static int currentLives;

    public TMPro.TextMeshProUGUI scoreText;

    

    private void Start()
    {
    }
    private void Update()
    {
        scoreText.SetText("" + totalPoints);
    }


    public void UpdateScore(int score)
    {
        totalPoints += score;



    }
}
