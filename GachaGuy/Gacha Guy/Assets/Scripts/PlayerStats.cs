using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    private GameManager gm;

    //This script will hold the modifiable variables affecting the player

    //Private vars
    private int HTH; //Max health
    private int SHD; //Max shield

    //Public vars
    public int PAM; //Player damage, is added to the gun damage
    public float SPD; //Player speed
    public float HND; //Gun handling, improves gun ACC (Closer HND is to 0, the less deviation occurs. I.E. HND = 0 means perfect accuracy)
    public int LCK; //Affects drops and money gain
    public float RCH; //Shield recharge delay

    //Vars most often changed
    public int currHTH; //Current health
    public int currSHD; //Current shield
    public int WAL; //Wallet, how much cash you have
    public int AMM; //How much ammo you have

    //Guns, will use gun name to search through list of guns and set current gun stats accordingly
    public string gun1;
    public string gun2;
    public bool using1;

    public int aggro; //Variable based on amount of damage being dealt

    //Functions
    private void Start()
    {
        gm = GetComponent<GameManager>();
    }

    public int getHTH()
    {
        return HTH;
    }

    public void setHTH(int i)
    {
        HTH = i;
    }

    public int getSHD()
    {
        return SHD;
    }

    public void setSHD(int i)
    {
        SHD = i;
    }
}
