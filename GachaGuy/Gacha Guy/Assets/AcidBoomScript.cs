using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidBoomScript : MonoBehaviour
{
    private GameManager gm;
    //public Boss1Script Boss1;
    public Animator boomAnimator;
    // Start is called before the first frame update
    void Start()
    {
        gm = FindObjectOfType<GameManager>();
        //Boss1 = FindObjectOfType<Boss1Script>();

    }

    // Update is called once per frame
    void Update()
    {
        if (this.enabled == true)
        {
            boomAnimator.SetBool("isBooming", true);
        }
        else
        {
            boomAnimator.SetBool("isBooming", false);
        }
    }
    //private void OnTriggerStay2D(Collider2D collision)
    //{
    //    if (collision.gameObject.tag == "Player")
    //    {
    //        gm.playerDamaged(10);
    //    }
    //}
}
