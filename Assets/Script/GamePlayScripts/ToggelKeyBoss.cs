using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggelKeyBoss : MonoBehaviour
{
    private Image keyImage;
    private void Start()
    {
        keyImage = GetComponent<Image>();
    }
    private void Update()
    {
        keyImage.enabled = PlayerInfo.hasBossKey;
    }
}

