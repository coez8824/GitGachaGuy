using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GachaList : MonoBehaviour
{
    public GameManager gm;
    public PlayerStats ps;

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
        gm = FindObjectOfType<GameManager>();
    }

    void buildGachaList()   //method to build listChar and assign slots to empty char
    {
        listChar.Add(new GachaCharacter(0, "Empty", "...", 0, empty));  //empty char
        listChar.Add(new GachaCharacter(1, "Nosfie Dracul", "Health Steal", 10, nosfieDracul));
        listChar.Add(new GachaCharacter(2, "Ma Faker", "Income", 10, maFaker));
        listChar.Add(new GachaCharacter(3, "Alice Studos", "Aggro Buff", 10, aliceStudos));
        listChar.Add(new GachaCharacter(4, "Fluoride Suninwind", "Pre-Heal", 10, fluorideSuninwind));
        listChar.Add(new GachaCharacter(5, "Penny Poe", "Thorns", 10, pennyPoe));
        listChar.Add(new GachaCharacter(6, "Grelian", "Sheild Buff", 10, grelian));

        slot1 = listChar[0];    //assigning slots to empty char
        slot2 = listChar[0];
        slot3 = listChar[0];
    }

    public void change (GachaCharacter slot, GachaCharacter newChar, int slotNum)    //method that implements changes to charSlot and abilities
    {
        int tempCurrHTH = ps.currHTH;   //getting max and current values for health/shield
        int tempMaxHTH = ps.getHTH();   //BEFORE changing abilities so if an ability that changes
        int tempCurrSHD = ps.currSHD;   //these values is removed, it doesn't give you over max health/shield
        int tempMaxSHD = ps.getSHD();

        switch (slot.id)     //DISABLE OLD ABILITY
        {   //slot.id is the index for the char in listChar
            case 0:
                //Do nothing
                break;

            case 1:
                nosVampirismDisable(); //Not implemented
                break;

            case 2:
                maLuckIncreaseDisable();
                break;

            case 3:
                aliAggroBuffDisable(); //Not implemented
                break;

            case 4:
                fluHealthIncreaseDisable();
                break;

            case 5:
                penThornsDisable(); //Not implemented
                break;

            case 6:
                greShieldBuffDisable();
                break;

            default:
                Debug.Log("Broke");
                break;
        }

        if (slotNum == 1)    //these if statements just finds and assigns the slot you are changing
        {
            slot1 = newChar;
        }
        if (slotNum == 2)
        {
            slot2 = newChar;
        }
        if (slotNum == 3)
        {
            slot3 = newChar;
        }

        switch (newChar.id)     //ENABLE NEW ABILITY
        {
            case 0 : 
                //Do nothing
                break;
            
            case 1 : 
                nosVampirism(slot.level); //Not implemented
                break;
            
            case 2:
                maLuckIncrease(slot.level);
                break;

            case 3:
                aliAggroBuff(slot.level); //Not implemented
                break;

            case 4:
                fluHealthIncrease(slot.level);
                break;

            case 5:
                penThorns(slot.level); //Not implemented
                break;

            case 6:
                greShieldBuff(slot.level);
                break;

            default:
                Debug.Log("Broke");
                break;
        }
    }

    public void maLuckIncrease(int level)   //MA FAKER ACTIVE
    {
        ps.LCK = level;
    }

    public void maLuckIncreaseDisable()   //MA FAKER INACTIVE
    {
        ps.LCK = 0;
    }

    public void fluHealthIncrease(int level)    //FLU ACTIVE
    {
        ps.setHTH(100 + (level * 10));
    }

    public void fluHealthIncreaseDisable()    //FLU INACTIVE
    {
        ps.setHTH(100);
        if (ps.currHTH > 100)
        {
            ps.currHTH = 100;
        }
    }

    public void greShieldBuff(int level)    //GRELIAN ACTIVE
    {
        ps.setSHD(50 + (level * 5));
    }

    public void greShieldBuffDisable()    //GRELIAN INACTIVE
    {
        ps.setSHD(50);
        if(ps.currSHD > 50)
        {
            ps.currSHD = 50;
        }
    }

    public void nosVampirism(int level)     //NOSFIE ACTIVE
    {
        gm.vampirismActive = true;
        gm.vampirismLevel = level;
    }

    public void nosVampirismDisable()     //NOSFIE INACTIVE
    {
        gm.vampirismActive = false;
    }

    public void aliAggroBuff(int level)     //ALI ACTIVE
    {
        //aggro not implemented
    }

    public void aliAggroBuffDisable()      //ALI INACTIVE
    {
        //aggro not implemented
    }

    public void penThorns(int level)    //PENNY ACTIVE
    {
        gm.thornsActive = true;
        gm.thornsLevel = level;
    }

    public void penThornsDisable()     //PENNY INACTIVE
    {
        gm.thornsActive = false;
    }
}
