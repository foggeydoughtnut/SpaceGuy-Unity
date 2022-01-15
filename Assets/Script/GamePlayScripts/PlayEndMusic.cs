using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayEndMusic : MonoBehaviour
{
    private void Start()
    {
        FindObjectOfType<AudioManager>().Play("EndScene");
    }
}
