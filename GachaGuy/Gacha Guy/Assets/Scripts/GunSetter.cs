using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunSetter : MonoBehaviour
{
    private GameManager gm;

    private void Start()
    {
        gm = GetComponent<GameManager>();
    }

    public void gunSetter(string s)
    {
        if(s == "Pistol")
        {
            gm.gs.setStats(1, 1, 7, .1f);
        }
        else
        {
            Debug.Log("If this message pops up then the game is probably broken.");
        }
    }
}
