using System.Collections;
using System.Collections.Generic;
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
    [SerializeField] public GameObject lootbox;     //Gameobject hodling lootboxHandler script and transform
    [SerializeField] public GameObject charSlot1;   //Gameobjects to represent chosen characters
    [SerializeField] public GameObject charSlot2;   
    [SerializeField] public GameObject charSlot3;
    [SerializeField] public GameObject selectButton1;    //Gameobjects of slot buttons
    [SerializeField] public GameObject selectButton2;
    [SerializeField] public GameObject selectButton3;
    [SerializeField] public GameObject selectSlot1;     //Gameobjects of char buttons
    [SerializeField] public GameObject selectSlot2;
    [SerializeField] public GameObject selectSlot3;
    [SerializeField] public GameObject selectSlot4;
    [SerializeField] public GameObject selectSlot5;
    [SerializeField] public GameObject selectSlot6;
    [SerializeField] public GameObject backButton;      //Gameobject of cancel button in char menu
    [SerializeField] public GameObject background;      //Gameobject of animated background

    private int slotInt = 0;
    public List<GachaCharacter> selectable = new List<GachaCharacter>();
    public Vector3 charLoc1;
    public Vector3 charLoc2;
    public Vector3 charLoc3;
    public float xChar1;
    public float yChar1;
    public float xChar2;
    public float yChar2;
    public float xChar3;
    public float yChar3;

    void Awake()
    {
        gm = FindObjectOfType<GameManager>();

        charLoc1 = charSlot1.transform.position; //finds original vector posistion
        charLoc2 = charSlot2.transform.position;
        charLoc3 = charSlot3.transform.position;
    }

    void Update()
    {
        charLoc1.x += (float)((System.Math.Sin(Time.time + 1f) * 0.0005f));     //slight sway to sprites
        charLoc1.y += (float)((System.Math.Sin(Time.time - 0.4f) * 0.00025f));
        charLoc2.x += (float)((System.Math.Sin(Time.time + 2f) * 0.0005f));
        charLoc2.y += (float)((System.Math.Sin(Time.time - 0.3f) * 0.00025f));
        charLoc3.x += (float)((System.Math.Sin(Time.time + 3f) * 0.0005f));
        charLoc3.y += (float)((System.Math.Sin(Time.time - 0.5f) * 0.00025f));

        charSlot1.transform.position = charLoc1;    //sets new sway translation
        charSlot2.transform.position = charLoc2;
        charSlot3.transform.position = charLoc3;
    }

    public void disableMainMenu()       //Shortcut to disable main menu objects
    {
        lootBoxButton.SetActive(false);
        characterSelectButton.SetActive(false);
        charSlot1.SetActive(false);
        charSlot2.SetActive(false);
        charSlot3.SetActive(false);
        background.SetActive(false);
    }

    public void enableMainMenu()        //Shortcut to enable main menu objects
    {
        lootBoxButton.SetActive(true);
        characterSelectButton.SetActive(true);
        charSlot1.SetActive(true);
        charSlot2.SetActive(true);
        charSlot3.SetActive(true);
        background.SetActive(true);
    }

    public void lootboxExecuter()       //in-script method to execute coroutine to avoid errors (called from lootboxButton object)
    {
        if ((true)&&(gm.ps.WAL >= 25)) //if can afford
        {
            kaching.Play();
            gm.ps.WAL -= 25;
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
        GameObject temp = Instantiate(chosenChar.charObj, lootbox.transform);
        yield return new WaitForSeconds(3f);
        temp.SetActive(false);
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
