using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
//using UnityEditorInternal;
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
    public GachaList cl;

    public Tracker track;

    public TMP_Text moneyCount;
    public GameObject player;

    public Text clpTXT;

    public Text statsText;
    private int damage;

    private bool recharging;

    public int dangerLevel;

    public bool thornsActive = false;
    public int thornsLevel = 0;
    public bool vampirismActive = false;
    public int vampirismLevel = 0;

    public int scalingPrice;
    public int scalingDoor;
    public int scalingGacha;

    public Text gachaPriceText;

    public int roomNum;

    public bool dead;
    //public GameObject phoneBlocker;
    public GameObject spawner;
    public LineRenderer lr;
    public AudioSource music;
    public GameObject loseTXT;

    public GameObject stats;

    // Start is called before the first frame update
    void Start()
    {
        spawner = GameObject.FindWithTag("Spawner");
        StartCoroutine(aggroDecrease());
     //   testDEF();
    }

    public void testDEF() //Default stats set up for testing
    {
        scalingPrice = 0;
        scalingDoor = 0;
        scalingGacha = 25;
        dangerLevel = 0;

        ps.setHTH(100);
        ps.setSHD(50);

        ps.PAM = 0;
        ps.SPD = 10;
        ps.HND = 1;
        ps.LCK = 0;
        ps.RCH = 10;

        ps.currHTH = ps.getHTH();
        ps.currSHD = ps.getSHD();
        ps.WAL = 100;
        ps.AMM = 99;

        ps.aggro = 0;
        ps.aggroBonus = 0;

        ps.gun1 = "Pistol";
        ps.gun2 = "";

        ps.using1 = true;

        gSetter.gunSetter("Pistol", 0);
    }

    // Update is called once per frame
    void Update()
    {
        /*if(ps.WAL >= 1000)
        {
            SceneManager.LoadScene("YouWin");
        }*/

        //gachaPriceText.GetComponent<TextMeshPro>().text = "$" + scalingGacha.ToString();

        gachaPriceText.text = "$"+scalingGacha.ToString();

        if (ps.currHTH <= 0)
        {
            //Destroy(GameObject.Find("Important"));
            //SceneManager.LoadScene("GameOver");
            dead = true;
            

            if(spawner != null)
            {
                spawner.SetActive(false);
            }

            lr.enabled = false;
            music.enabled = false;
            loseTXT.SetActive(true);
        }

        /*if(recharging)
            StartCoroutine(rechargeShield());*/

        if (ps.aggro > 50)
        {
            ps.aggroBonus = 1;
        }
        else if (ps.aggro > 100)
        {
            ps.aggroBonus = 2;
        }
        if (ps.aggro > 150)
        {
            ps.aggroBonus = 3;
        }
        else if (ps.aggro > 200)
        {
            ps.aggroBonus = 4;
        }

        clpTXT.text = gs.curr + "/" + ps.AMM;

        if (ps.WAL > 0) //Displays money in moneyCount
            moneyCount.text = "$" + ps.WAL.ToString();
        else
            moneyCount.text = "BROKE";

        damage = ps.PAM + gs.DAM;

        statsText.text =
            "DAM       " + damage + "\n" +
            "RAT        " + gs.RAT + "\n" +
            "SPD        " + ps.SPD + "\n" +
            "HND        " + ps.HND + "\n" +
            "ACC        " + gs.ACC + "\n" +
            "LCK         " + ps.PAM + "\n" +
            "RCH        " + ps.PAM + "\n" +
            "AGG        " + ps.aggro
            ;
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

        int shdVal = ps.getSHD();

        if(ps.currSHD < shdVal) //Prevents from going over max
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
            Debug.Log("Nah");
            //if (ps.using1)
            //{
            //    ps.using1 = false;
            //    //gs.currSetter(2);
            //    gSetter.gunSetter(ps.gun2, 2);
            //}
            //else
            //{
            //    ps.using1 = true;
            //    //gs.currSetter(1);
            //    gSetter.gunSetter(ps.gun1, 1);
            //}
        }
        else
        {
            Debug.Log("Swap");
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

    public void vampirismOnKill()
    {
        if (vampirismActive == true) {
            vampirismLevel = cl.playerInven.Find(x => x.id == 1).level;

            if (ps.currHTH < ps.getHTH())
            {
                if ((ps.currHTH + (1 * vampirismLevel) > ps.getHTH()))
                {
                    ps.currHTH = ps.getHTH();
                }
                else
                {
                    ps.currHTH += (1 * vampirismLevel);
                }
            }
        }
    }

    public void toMenu()
    {
        Destroy(GameObject.Find("Important"));
        SceneManager.LoadScene("Menu");
    }

    public void statsButton()
    {
        if(statsText.IsActive())
        {
            stats.SetActive(false);
        }
        else
        {
            stats.SetActive(true);
        }
    }

    IEnumerator aggroDecrease()
    {
        if (ps.aggro > 0) 
        {
            ps.aggro -= 1;
        }
        yield return new WaitForSeconds(5f);
        StartCoroutine(aggroDecrease());
    }
}
