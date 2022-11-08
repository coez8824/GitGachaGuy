using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public PlayerStats ps;
    public GunScript gs;
    public GunSetter gSetter;

    public TMP_Text moneyCount;
    public GameObject player;

    public Text clpTXT;

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
        ps.AMM = 40;

        ps.gun1 = "Pistol";
        ps.gun2 = "";

        ps.using1 = true;

        gSetter.gunSetter("Pistol", 0);
    }

    // Update is called once per frame
    void Update()
    {
        if(ps.WAL >= 1000)
        {
            SceneManager.LoadScene("YouWin");
        }

        if(ps.currHTH <= 0)
        {
            SceneManager.LoadScene("GameOver");
        }

        /*if(recharging)
            StartCoroutine(rechargeShield());*/

        clpTXT.text = gs.curr + "/" + ps.AMM;

        if (ps.WAL > 0) //Displays money in moneyCount
            moneyCount.text = "$" + ps.WAL.ToString();
        else
            moneyCount.text = "BROKE";
    }

    public void playerDamaged(int i)
    {
        //Debug.Log("Hit for " + i);

        recharging = false;

        if (ps.currSHD > 0) //Always damages shield first
        {
            ps.currSHD -= i;
        }
        else //But damages health if there is no shield left
        {
            ps.currHTH -= i;
        }

        if(!recharging) //If recharging isn't true, start the shield recharge
            StartCoroutine(startRecharge());
    }

    IEnumerator startRecharge()
    {
        yield return new WaitForSeconds(ps.RCH); //Delayed based on RCH stat

        recharging = true;
        StartCoroutine(rechargeShield()); //Actual shield recharge
    }

    IEnumerator rechargeShield()
    {
        yield return new WaitForSeconds(1); //By every 1 second

        if (!recharging) //Cancels if player is hit
            yield break;

        if(ps.currSHD < ps.getSHD()) //Prevents from going over max
        {
            ps.currSHD++; //Increases by 1
        }
        else
        {
            yield break; //Stops recharge if shield is full
        }

        StartCoroutine(rechargeShield()); //Continues recharge if shield isn't full
    }

    public void swapGun()
    {
        if((ps.gun1 == "") || (ps.gun2 == ""))
        {
            if (ps.using1)
            {
                ps.using1 = false;
                //gs.currSetter(2);
                gSetter.gunSetter(ps.gun2, 2);
            }
            else
            {
                ps.using1 = true;
                //gs.currSetter(1);
                gSetter.gunSetter(ps.gun1, 1);
            }
        }
        else
        {
            Debug.Log("Nah");
        }
    }

    public void tossGun()
    {
        if ((ps.using1) && (ps.gun1 == "Pistol"))
        {
            Debug.Log("Nah");
        }
        else if ((!ps.using1) && (ps.gun2 == "Pistol"))
        {
            Debug.Log("Nah");
        }
        else if ((ps.using1)&&(ps.gun2 != "Pistol"))
        {
            ps.gun1 = "Pistol";
            gSetter.gunSetter("Pistol", 0);
        }
        else if ((!ps.using1) && (ps.gun1 != "Pistol"))
        {
            ps.gun2 = "Pistol";
            gSetter.gunSetter("Pistol", 0);
        }
        else if ((ps.using1) && (ps.gun2 == "Pistol"))
        {
            ps.gun1 = "";
            gSetter.gunSetter(ps.gun2, 2);
        }
        else if ((!ps.using1) && (ps.gun1 == "Pistol"))
        {
            ps.gun2 = "";
            gSetter.gunSetter(ps.gun1, 1);
        }
    }
}
