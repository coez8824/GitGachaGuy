using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public bool open;
    public Animator anim;

    private void Update()
    {
        anim.SetBool("Open", open);
    }
}
