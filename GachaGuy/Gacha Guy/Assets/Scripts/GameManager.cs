using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;
using TMPro;

public class GameManager : MonoBehaviour
{
    public PlayerStats ps;
    public GunScript gs;
    public GunSetter gSetter;

    public TMP_Text moneyCount;

    private bool recharging;

    public int dangerLevel;

    // Start is called before the first frame update
    void Start()
    {
        dangerLevel = 0;
        testDEF();
    }

    public void testDEF() //Default stats set up for testing
    {
        ps.setHTH(100);
        ps.setSHD(50);

        ps.PAM = 0;
        ps.SPD = 10;
        ps.HND = 1;
        ps.LCK = 0;
        ps.RCH = 10;

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
        if(dangerLevel>=500)
            dangerLevel -= 500;

        if(ps.currHTH <= 0)
        {
            Debug.Log("GAME OVER");
        }

        /*if(recharging)
            StartCoroutine(rechargeShield());*/

        if (ps.WAL > 0)
            moneyCount.text = "$" + ps.WAL.ToString();
        else
            moneyCount.text = "BROKE";
    }

    public void playerDamaged(int i)
    {
        //Debug.Log("Hit for " + i);

        recharging = false;

        if (ps.currSHD > 0)
        {
            ps.currSHD -= i;
        }
        else
        {
            ps.currHTH -= i;
        }

        if(!recharging)
            StartCoroutine(startRecharge());
    }

    IEnumerator startRecharge()
    {
        yield return new WaitForSeconds(ps.RCH);

        recharging = true;
        StartCoroutine(rechargeShield());
    }

    IEnumerator rechargeShield()
    {
        yield return new WaitForSeconds(1);

        if (!recharging)
            yield break;

        if(ps.currSHD < ps.getSHD())
        {
            ps.currSHD++;
        }
        else
        {
            yield break;
        }

        StartCoroutine(rechargeShield());
    }
}
