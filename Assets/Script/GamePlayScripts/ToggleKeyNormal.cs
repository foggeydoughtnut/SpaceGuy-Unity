using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleKeyNormal : MonoBehaviour
{
    private Image keyImage;
    private void Start()
    {
        keyImage = GetComponent<Image>();
    }
    private void Update()
    {
        keyImage.enabled = PlayerInfo.hasKey;
    }
}
