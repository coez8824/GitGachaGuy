using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunSetter : MonoBehaviour
{
    private GameManager gm;

    public GunScript GS;

    public Image i;
    public Sprite pistol;
    public Sprite rifle;
    public Sprite sniper;

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
            i.sprite = pistol;
        }
        else if (s == "Rifle")
        {
            GS.setStats(1, .1f, 25, .1f, n);
            i.sprite = rifle;
        }
        else if (s == "Sniper")
        {
            GS.setStats(10, 2, 12, 0, n);
            i.sprite = sniper;
        }
        else
        {
            Debug.Log("If this message pops up then the game is probably broken.");
        }
    }
}
