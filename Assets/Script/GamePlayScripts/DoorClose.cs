using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorClose : MonoBehaviour
{
    public GameObject door;
    

    public enum EnterFrom
    {
        Left,
        Right,
        Bottom,
        Top
    }

    public EnterFrom enterFrom;
    private bool activated = false;


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            bool valid = false;
            switch (enterFrom)
            {
                case EnterFrom.Left:
                    valid = collision.transform.position.x > transform.position.x;
                    break;
                case EnterFrom.Right:
                    valid = collision.transform.position.x < transform.position.x;
                    break;
                case EnterFrom.Bottom:
                    valid = collision.transform.position.y > transform.position.y;
                    break;
                case EnterFrom.Top:
                    valid = collision.transform.position.y < transform.position.y;
                    break;

            }

            if (valid && !activated)
            {
                activated = true;
                Debug.Log("Door Shut");
                door.SetActive(true);
            }



        }
    }
}
