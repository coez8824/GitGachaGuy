using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunSetter : MonoBehaviour
{
    private GameManager gm;

    public GunScript GS;

    private void Start()
    {
        gm = GetComponent<GameManager>();
    }

    public void gunSetter(string s, int n) //DAM = d; RAT = r; CLP = c; ACC = a; curNUM = n;
    {
        GS.inf = false;

        if (s == "Pistol")
        {
            GS.setStats(1, 1, 7, .1f, n);
            GS.inf = true;
        }
        else if (s == "Rifle")
        {
            GS.setStats(1, .1f, 25, .1f, n);
        }
        else
        {
            Debug.Log("If this message pops up then the game is probably broken.");
        }
    }
}
