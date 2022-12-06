using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoomEnter : MonoBehaviour
{
    public GameObject door;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            door.GetComponent<DoorScript>().open = true;
            Destroy(door.GetComponent<Collider2D>());
        }
    }
}
