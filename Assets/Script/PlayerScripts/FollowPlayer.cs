using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    private Transform player;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<AudioManager>().Play("LevelMusic");
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        rb.MovePosition(player.position);
    }
}
