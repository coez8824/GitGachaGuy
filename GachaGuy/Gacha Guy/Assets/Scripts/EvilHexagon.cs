using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilHexagon : MonoBehaviour
{
    private GameManager gm;
    private DropScript ds;

    private void Start()
    {
        ds = gameObject.AddComponent<DropScript>();
        gm = FindObjectOfType<GameManager>();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            gm.playerDamaged(1);
        }
    }

    public void rip()
    {
        gm.ps.WAL += (25 + (2 * gm.ps.LCK)); //Pays a base 25 cash + bonus based on luck

        ds.yell();

        Destroy(gameObject);
    }
}
