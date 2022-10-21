using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    //This script will hold the modifiable variables affecting the player

    //Private vars
    private int HTH; //Max health
    private int SHD; //Max shield

    //Public vars
    public int PAM; //Player damage, is added to the gun damage
    public float SPD; //Player speed
    public float HND; //Gun handling, improves gun ACC (Closer HND is to 0, the less deviation occurs. I.E. HND = 0 means perfect accuracy)
    public float LCK; //Affects drops and money gain

    //Vars most often changed
    public int currHTH; //Current health
    public int currSHD; //Current shield
    public int WAL; //Wallet, how much cash you have
    public int AMM; //How much ammo you have

    //Guns, will use gun name to search through list of guns and set current gun stats accordingly
    public string gun1;
    public string gun2;

    //Functions
    private void Start()
    {
        HTH = 100;
        SHD = 50;

        PAM = 0;
        SPD = 10;
        HND = 1;
        LCK = 0;

        currHTH = HTH;
        currSHD = SHD;
        WAL = 0;
        AMM = 999;

        gun1 = "Pistol";
        gun2 = "";
    }

    public int getHTH()
    {
        return HTH;
    }

    public int getSHD()
    {
        return SHD;
    }
}
