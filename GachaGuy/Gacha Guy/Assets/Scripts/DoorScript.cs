using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public bool open;
    public Animator anim;

    [SerializeField]
    private bool isFront;

    public GameObject look;

    private void Update()
    {
        anim.SetBool("Open", open);
    }

    public void lockOpen()
    {
        Animator a;
        a = look.GetComponent<Animator>();
        a.SetBool("Open", open);
        StartCoroutine(l());
    }

    public void makeBoss()
    {
        if(isFront)
        {
            Animator a;
            a = look.GetComponent<Animator>();
            a.SetBool("Boss", true);
        }
    }

    public void byeLock()
    {
        Destroy(look);
    }

    IEnumerator l()
    {
        yield return new WaitForSeconds(.3f);
        Destroy(look);
    }
}
