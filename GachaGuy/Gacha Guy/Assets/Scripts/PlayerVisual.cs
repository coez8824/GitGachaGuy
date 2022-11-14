using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVisual : MonoBehaviour
{
    public Transform player;

    public Transform target;

    public Animator anim;

    public bool actualVIS;

    public bool moving;

    // Update is called once per frame
    void Update()
    {
        if (!actualVIS)
        {
            target = null;
            anim = null;
            transform.position = player.transform.position + new Vector3(0, 1, 0);
        }
        else
        {
            anim.SetBool("isMoving", moving);
            transform.position = player.transform.position;

            anim.SetFloat("MoveY", target.position.y - transform.position.y);
            anim.SetFloat("MoveX", target.position.x - transform.position.x);
        }
    }
}
