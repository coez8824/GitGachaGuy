using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JIC : MonoBehaviour
{
    public GameManager gm;

    void Start()
    {
        if(gm != null)
        gm.testDEF();
    }
}
