using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttackCollision : MonoBehaviour
{
    private GameManager gm;
    public Collision col;
    // Start is called before the first frame update
    void Start()
    {
        gm = FindObjectOfType<GameManager>();
        col = FindObjectOfType<Collision>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            gm.playerDamaged(10);
            if (gm.thornsActive == true)
            {
                col.health -= (1 * gm.thornsLevel);
            }
        }
    }
}
