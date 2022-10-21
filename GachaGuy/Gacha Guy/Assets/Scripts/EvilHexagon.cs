using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilHexagon : MonoBehaviour
{
    private GameManager gm;

    private void Start()
    {
        gm = FindObjectOfType<GameManager>();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            gm.playerDamaged(1);
        }
    }
}
