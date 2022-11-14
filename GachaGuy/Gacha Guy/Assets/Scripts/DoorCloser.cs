using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography;
using UnityEngine;

public class DoorCloser : MonoBehaviour
{
    public DoorScript ds;
    public GameObject door;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            ds.open = false;
            door.AddComponent<Collider2D>();
        }
    }
}
