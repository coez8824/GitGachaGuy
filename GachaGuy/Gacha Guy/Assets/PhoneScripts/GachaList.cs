using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GachaList : MonoBehaviour
{
    [SerializeField] public GameObject gm;      //Gamemanager to access stats

    public List<GachaCharacter> listChar = new List<GachaCharacter>();      //List of all possible characters
    public List<GachaCharacter> playerInven = new List<GachaCharacter>();   //List of all obtained characters
    public GachaCharacter slot1;    //Slots for currently selected characters
    public GachaCharacter slot2;
    public GachaCharacter slot3;

    [SerializeField] public GameObject nosfieDracul;    //Adding all character prefabs for menus
    [SerializeField] public GameObject maFaker;
    [SerializeField] public GameObject aliceStudos;
    [SerializeField] public GameObject fluorideSuninwind;
    [SerializeField] public GameObject pennyPoe;
    [SerializeField] public GameObject grelian;
    [SerializeField] public GameObject empty;

    void Awake()
    {
        buildGachaList();   //builds lists on start
    }

    void buildGachaList()   //method to build listChar and assign slots to empty char
    {
        listChar.Add(new GachaCharacter(0, "Empty", "...", 0, empty));
        //listChar.Add(new GachaCharacter(1, "Nosfie Dracul", "Health Steal", 5, nosfieDracul));
        listChar.Add(new GachaCharacter(2, "Ma Faker", "Income", 10, maFaker));
        //listChar.Add(new GachaCharacter(3, "Alice Studos", "Aggro Buff", 15, aliceStudos));
        listChar.Add(new GachaCharacter(4, "Fluoride Suninwind", "Pre-Heal", 10, fluorideSuninwind));
        //listChar.Add(new GachaCharacter(5, "Penny Poe", "Thorns", 20, pennyPoe));
        listChar.Add(new GachaCharacter(6, "Grelian", "Sheild Buff", 15, grelian));

        slot1 = listChar[0];
        slot2 = listChar[0];
        slot3 = listChar[0];
    }

    public void abilityChange(GachaCharacter slot)
    {
        switch (slot.id) 
        {
            case 0 : 
                //Do nothing
                break;
            
            case 1 : 
                Debug.Log(slot.passive); //Not implemented
                break;
            
            case 2:
                maLuckIncrease(slot.level);
                break;

            case 3:
                Debug.Log(slot.passive); //Not implemented
                break;

            case 4:
                fluHealthIncrease(slot.level);
                break;

            case 5:
                Debug.Log(slot.passive); //Not implemented
                break;

            case 6:
                greShieldBuff(slot.level);
                break;

            default:
                Debug.Log("Broke");
                break;
        }
    }

    public void maLuckIncrease(int level)
    {
        gm.GetComponent<PlayerStats>().LCK = level;
    }

    public void fluHealthIncrease(int level)
    {
        gm.GetComponent<PlayerStats>().setHTH(100 + (level * 10));
    }

    public void greShieldBuff(int level)
    {
        gm.GetComponent<PlayerStats>().setSHD(50 + (level * 5));
    }
}
