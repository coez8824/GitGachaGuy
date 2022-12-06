using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spriteSlider : MonoBehaviour
{
    public PhoneHandler phn;
    public GachaList ls;
    public PlayerStats ps;
    public GameObject aggroSlider;
    public Vector3 aggroStart;

    public Vector3 charLoc1;
    public Vector3 charLoc2;
    public Vector3 charLoc3;
    public Vector3 ogLoc1;
    public Vector3 ogLoc2;
    public Vector3 ogLoc3;
    public Vector3 enemLoc;
    public float time1 = 0.0f;
    public float time2 = 0.0f;
    public float time3 = 0.0f;
    public bool whack1 = true;
    public bool whack2 = true;
    public bool whack3 = true;
    public float xChar1;
    public float yChar1;
    public float xChar2;
    public float yChar2;
    public float xChar3;
    public float yChar3;

    void Awake()
    {
        ogLoc1 = phn.charSlot1.transform.position;      //finds original vector posistion
        ogLoc2 = phn.charSlot2.transform.position;
        ogLoc3 = phn.charSlot3.transform.position;
        charLoc1 = phn.charSlot1.transform.position;
        charLoc2 = phn.charSlot2.transform.position;
        charLoc3 = phn.charSlot3.transform.position;
        enemLoc = phn.enemySlot.transform.position;
        aggroStart = aggroSlider.transform.position;
    }

    void Update()
    {
        charLoc1.x += (float)((System.Math.Sin(Time.time + 1f) * 0.0005f));     //slight sway to sprites
        charLoc1.y += (float)((System.Math.Sin(Time.time - 0.4f) * 0.0003f));
        charLoc2.x += (float)((System.Math.Sin(Time.time + 2f) * 0.0005f));
        charLoc2.y += (float)((System.Math.Sin(Time.time - 0.3f) * 0.0003f));
        charLoc3.x += (float)((System.Math.Sin(Time.time + 3f) * 0.0005f));
        charLoc3.y += (float)((System.Math.Sin(Time.time - 0.5f) * 0.0003f));
        enemLoc.x += (float)((System.Math.Sin(Time.time + 1.5f) * 0.001f));
        enemLoc.y += (float)((System.Math.Sin(Time.time + 0.2f) * 0.0003f));

        phn.charSlot1.transform.position = charLoc1;    //sets new sway translation
        phn.charSlot2.transform.position = charLoc2;
        phn.charSlot3.transform.position = charLoc3;
        phn.enemySlot.transform.position = enemLoc;

        //////////////////////////////////////////////////////////////////////

        if (phn.charSlot1.GetComponent<SpriteRenderer>().sprite != null)       //increments times if slot has a sprite
        {
            time1 += Time.deltaTime;
        }
        if (phn.charSlot2.GetComponent<SpriteRenderer>().sprite != null)
        {
            time2 += Time.deltaTime;
        }
        if (phn.charSlot3.GetComponent<SpriteRenderer>().sprite != null)
        {
            time3 += Time.deltaTime;
        }

        //////////////////////////////////////////////////////////////////
        
        if (time1 > 5.0f && whack1 == true)                                   //slot1 hit timer and slider
        {
            charLoc1 = Vector3.MoveTowards(charLoc1, enemLoc, 0.05f);
            if (charLoc1 == enemLoc)
            {
                whack1 = false;
                phn.enemHit(ls.slot1.level);
            }
        }
        if (whack1 == false)
        {
            charLoc1 = Vector3.MoveTowards(charLoc1, ogLoc1, 0.05f);
            if (charLoc1 == ogLoc1)
            {
                time1 = 0.0f;
                whack1 = true;
            }
        }

        if (time2 > 5.0f && whack2 == true)                                   //slot2 hit timer and slider
        {
            charLoc2 = Vector3.MoveTowards(charLoc2, enemLoc, 0.05f);
            if (charLoc2 == enemLoc)
            {
                whack2 = false;
                phn.enemHit(ls.slot2.level);
            }
        }
        if (whack2 == false)
        {
            charLoc2 = Vector3.MoveTowards(charLoc2, ogLoc2, 0.05f);
            if (charLoc2 == ogLoc2)
            {
                time2 = 0.0f;
                whack2 = true;
            }
        }

        if (time3 > 5.0f && whack3 == true)                                   //slot3 hit timer and slider
        {
            charLoc3 = Vector3.MoveTowards(charLoc3, enemLoc, 0.05f);
            if (charLoc3 == enemLoc)
            {
                whack3 = false;
                phn.enemHit(ls.slot3.level);
            }
        }
        if (whack3 == false)
        {
            charLoc3 = Vector3.MoveTowards(charLoc3, ogLoc3, 0.05f);
            if (charLoc3 == ogLoc3)
            {
                time3 = 0.0f;
                whack3 = true;
            }
        }

        if (ps.aggro <= 100)
        {
            float tempNum = (0.368f * ps.aggro);
            Vector3 tempVec = aggroSlider.transform.position;

            tempVec.x = aggroStart.x - tempNum;
            aggroSlider.transform.position = tempVec;
        }
    }
}
