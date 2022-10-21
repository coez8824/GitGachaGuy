using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class GameManager : MonoBehaviour
{
    public PlayerStats ps;
    public GunScript gs;
    public GunSetter gSetter;

    // Start is called before the first frame update
    void Start()
    {
        testDEF();
    }

    public void testDEF()
    {
        ps.setHTH(100);
        ps.setSHD(50);

        ps.PAM = 0;
        ps.SPD = 10;
        ps.HND = 1;
        ps.LCK = 0;

        ps.currHTH = ps.getHTH();
        ps.currSHD = ps.getSHD();
        ps.WAL = 0;
        ps.AMM = 999;

        ps.gun1 = "Pistol";
        ps.gun2 = "";

        gSetter.gunSetter("Pistol");
    }

    // Update is called once per frame
    void Update()
    {
        if(ps.currHTH <= 0)
        {
            Debug.Log("GAME OVER");
        }
    }

    public void playerDamaged(int i)
    {
        if(ps.currSHD > 0)
        {
            ps.currSHD--;
        }
        else
        {
            ps.currHTH--;
        }
    }
}
