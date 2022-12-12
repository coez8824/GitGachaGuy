using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PhoneHandler : MonoBehaviour
{
    public GameManager gm;
    public GachaList ls;     //GachaList
    public LootboxHandler lh;
    public AudioSource kaching;
    [SerializeField] public GameObject lootBoxButton;   //Gameobject holding lootboxButton object
    [SerializeField] public GameObject characterSelectButton;     //Gameobject holding charSelectButton object
    [SerializeField] public GameObject levelText;
    [SerializeField] public GameObject lootbox;     //Gameobject hodling lootboxHandler script and transform
    [SerializeField] public GameObject charSlot1;   //Gameobjects to represent chosen characters
    [SerializeField] public GameObject charSlot2;   
    [SerializeField] public GameObject charSlot3;
    [SerializeField] public GameObject enemySlot;
    [SerializeField] public GameObject selectButton1;    //Gameobjects of slot buttons
    [SerializeField] public GameObject selectButton2;
    [SerializeField] public GameObject selectButton3;
    [SerializeField] public GameObject selectSlot1;     //Gameobjects of char buttons
    [SerializeField] public GameObject enemSlider;
    [SerializeField] public GameObject introSlot;
    public TMP_Text slot1Level;
    [SerializeField] public GameObject selectSlot2;
    public TMP_Text slot2Level;
    [SerializeField] public GameObject selectSlot3;
    public TMP_Text slot3Level;
    [SerializeField] public GameObject selectSlot4;
    public TMP_Text slot4Level;
    [SerializeField] public GameObject selectSlot5;
    public TMP_Text slot5Level;
    [SerializeField] public GameObject selectSlot6;
    public TMP_Text slot6Level;
    [SerializeField] public GameObject backButton;      //Gameobject of cancel button in char menu
    [SerializeField] public GameObject background;      //Gameobject of animated background

    public Sprite enemSkel;
    public Sprite enemLarry;
    public Sprite enemBox;
    public List<Sprite> enemySpriteList = new List<Sprite>();
    public int enemHTH;
    public int maxEnemHTH;

    private int slotInt = 0;
    public List<GachaCharacter> selectable = new List<GachaCharacter>();

    void Awake()
    {
        gm = FindObjectOfType<GameManager>();   //find gamemanager

        enemySpriteList.Add(enemSkel);          //adds gacha enemy sprites to a list
        enemySpriteList.Add(enemLarry);
        enemySpriteList.Add(enemBox);

        enemySlot.GetComponent<SpriteRenderer>().sprite = enemySpriteList[Random.Range(0, 3)];      //inital gacha enemy
        maxEnemHTH = 100;
        enemHTH = maxEnemHTH;      
        enemSlider.GetComponent<Slider>().maxValue = maxEnemHTH;
    }

    void Update()
    {
        enemSlider.GetComponent<Slider>().value = enemHTH;      //gacha enemy health bar update
    }

    public void enemHit(int charLevel)          //called if gacha enemy is hit
    {
        enemHTH -= (5 * charLevel);         //decreases health by 5 times the level of the char that hit it
        if (enemHTH <= 0)
        {
            enemySlot.GetComponent<SpriteRenderer>().sprite = enemySpriteList[Random.Range(0, 3)];  //selects new random enemy
            maxEnemHTH += 100;
            enemHTH = maxEnemHTH;                                                                   //resets health the 100
            if (ls.slot1 != ls.listChar[0])                                                 //increases level of present characters
            {
                ls.slot1.level++;
            }
            if (ls.slot2 != ls.listChar[0])
            {
                ls.slot2.level++;
            }
            if (ls.slot3 != ls.listChar[0])
            {
                ls.slot3.level++;
            }
        }
    }

    public void disableMainMenu()       //Shortcut to disable main menu objects
    {
        lootBoxButton.SetActive(false);
        characterSelectButton.SetActive(false);
        charSlot1.SetActive(false);
        charSlot2.SetActive(false);
        charSlot3.SetActive(false);
        background.SetActive(false);
        enemySlot.SetActive(false);
        enemSlider.SetActive(false);
    }

    public void enableMainMenu()        //Shortcut to enable main menu objects
    {
        lootBoxButton.SetActive(true);
        characterSelectButton.SetActive(true);
        charSlot1.SetActive(true);
        charSlot2.SetActive(true);
        charSlot3.SetActive(true);
        background.SetActive(true);
        enemySlot.SetActive(true);
        enemSlider.SetActive(true);
    }

    public void lootboxExecuter()       //in-script method to execute coroutine to avoid errors (called from lootboxButton object)
    {
        if ((true)&&(gm.ps.WAL >= gm.scalingGacha)) //if can afford
        {
            kaching.Play();
            gm.ps.WAL -= gm.scalingGacha;

            gm.scalingGacha += 25;
            gm.track.gachasRolled++;
            gm.track.moneySpent += gm.scalingGacha;

            StartCoroutine(lootboxButtonPressed());
        }
        else      //else cant afford
        {
            Debug.Log("Can't Roll");
        }
    }

    public IEnumerator lootboxButtonPressed()       //Coroutine to handle selected sprite appearing on screen
    {
        disableMainMenu();
        GachaCharacter chosenChar = lh.roll();
        levelText.SetActive(true);
        if (chosenChar.splashAnim)
        {
            introSlot.GetComponent<SpriteRenderer>().sprite = chosenChar.splashArt;
            introSlot.GetComponent<Animator>().runtimeAnimatorController = chosenChar.splashAnim;
            introSlot.SetActive(true);
            yield return new WaitForSeconds(3f);
            introSlot.SetActive(false);
        }
        else
        {
            GameObject temp = Instantiate(chosenChar.charObj, lootbox.transform);
            yield return new WaitForSeconds(3f);
        }
        levelText.SetActive(false);
        enableMainMenu();
    }

    public void charSelectButtonPressed()       //character select menu (NOT YET IMPLEMENTED)
    {
        disableMainMenu();
        selectButton1.SetActive(true);
        selectButton2.SetActive(true);
        selectButton3.SetActive(true);
        backButton.SetActive(true);
    }

    public void back()      //if cancel button pressed, returns to main menu
    {
        selectButton1.SetActive(false);
        selectButton2.SetActive(false);
        selectButton3.SetActive(false);
        backButton.SetActive(false);
        selectSlot1.SetActive(false);
        selectSlot2.SetActive(false);
        selectSlot3.SetActive(false);
        selectSlot4.SetActive(false);
        selectSlot5.SetActive(false);
        selectSlot6.SetActive(false);
        enableMainMenu();
    }

    public void charSelectScreen(int slot) //creates menu that shows after choosing char slot
    {
        slotInt = slot;
        selectable = new List<GachaCharacter>();

        for (int i = 0; i < 6; i++)   //fills the backend of selectable with empty chars
        {
            selectable.Add(ls.listChar[0]);
        }

        selectButton1.SetActive(false); //remove previous menu
        selectButton2.SetActive(false);
        selectButton3.SetActive(false);

        int num = 0;
        for(int i = 0; i < ls.playerInven.Count; i++) //inititate and create selectable list of characters
        {                                //selectable = owned and not selected
            if (i < ls.playerInven.Count)
            {
                if (ls.playerInven[i].name != ls.slot1.name &&
                    ls.playerInven[i].name != ls.slot2.name &&  //check if char is selected
                    ls.playerInven[i].name != ls.slot3.name)
                {
                    Debug.Log(ls.playerInven[i].name + " is not equivalent to " + ls.slot1.name + ", " + ls.slot2.name + ", or " + ls.slot2.name);
                    selectable[num] = ls.playerInven[i];   //add unselected chars to temp list
                    num++;
                }
            }
        }

        Debug.Log("selectable: " + selectable[0].name + selectable[1].name + selectable[2].name + selectable[3].name + selectable[4].name + selectable[5].name);
        Debug.Log("slots: " + ls.slot1.name + ls.slot2.name + ls.slot3.name);

        selectSlot1.GetComponent<Image>().sprite = selectable[0].charObj.GetComponent<SpriteRenderer>().sprite; //sets gameobject sprites
        selectSlot2.GetComponent<Image>().sprite = selectable[1].charObj.GetComponent<SpriteRenderer>().sprite; //to sprites of selectable
        selectSlot3.GetComponent<Image>().sprite = selectable[2].charObj.GetComponent<SpriteRenderer>().sprite; //(from char to empty char)
        selectSlot4.GetComponent<Image>().sprite = selectable[3].charObj.GetComponent<SpriteRenderer>().sprite;
        selectSlot5.GetComponent<Image>().sprite = selectable[4].charObj.GetComponent<SpriteRenderer>().sprite;
        selectSlot6.GetComponent<Image>().sprite = selectable[5].charObj.GetComponent<SpriteRenderer>().sprite;

        if (selectable[0].name == "Empty")      //assign text underneath sprite (level)
        {
            slot1Level.text = "???";
        }
        else
        {
            slot1Level.text = "Level " + selectable[0].level.ToString();
        }
        if (selectable[1].name == "Empty")
        {
            slot2Level.text = "???";
        }
        else
        {
            slot2Level.text = "Level " + selectable[1].level.ToString();
        }
        if (selectable[2].name == "Empty")
        {
            slot3Level.text = "???";
        }
        else
        {
            slot3Level.text = "Level " + selectable[2].level.ToString();
        }
        if (selectable[3].name == "Empty")
        {
            slot4Level.text = "???";
        }
        else
        {
            slot4Level.text = "Level " + selectable[3].level.ToString();
        }
        if (selectable[4].name == "Empty")
        {
            slot5Level.text = "???";
        }
        else
        {
            slot5Level.text = "Level " + selectable[4].level.ToString();
        }
        if (selectable[5].name == "Empty")
        {
            slot6Level.text = "???";
        }
        else
        {
            slot6Level.text = "Level " + selectable[5].level.ToString();
        }


        selectSlot1.SetActive(true);    //enable select menu
        selectSlot2.SetActive(true);
        selectSlot3.SetActive(true);
        selectSlot4.SetActive(true);
        selectSlot5.SetActive(true);
        selectSlot6.SetActive(true);
    }

    public void setChar(int numChar) //uses slot and selected char to assign char to a slot
    {                                        //numChar is the int of the index of selectable
        if (selectable[numChar].name != "Empty") //if select an empty char, nothing happens and you go back to previous method
        {
            switch (slotInt)    //slot int is the charSlot you're assigning (1, 2, or 3)
            {
                case 0:     //not possible to get, must've somehow skipped previous menu
                    Debug.Log("skipped menu?");
                    break;

                case 1:     
                    charSlot1.GetComponent<SpriteRenderer>().sprite = selectable[numChar].charObj.GetComponent<SpriteRenderer>().sprite;
                    ls.change(ls.slot1, selectable[numChar], 1);
                    break;  //updates sprite for slot 1, calls change() in GachaList to update lists

                case 2:
                    charSlot2.GetComponent<SpriteRenderer>().sprite = selectable[numChar].charObj.GetComponent<SpriteRenderer>().sprite;
                    ls.change(ls.slot2, selectable[numChar], 2);
                    break;  //updates sprite for slot 2, calls change() in GachaList to update lists

                case 3:
                    charSlot3.GetComponent<SpriteRenderer>().sprite = selectable[numChar].charObj.GetComponent<SpriteRenderer>().sprite;
                    ls.change(ls.slot3, selectable[numChar], 3);
                    break;  //updates sprite for slot 3, calls change() in GachaList to update lists

                default:
                    Debug.Log("broke");
                    break;
            }
            back(); //initiates the cancel method to return to main menu after your selection
        }
    }
}
