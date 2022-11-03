using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhoneHandler : MonoBehaviour
{
    public GameManager gm;
    public AudioSource kaching;
    [SerializeField] public GameObject lootBoxButton;   //Gameobject holding lootboxButton object
    [SerializeField] public GameObject characterSelectButton;     //Gameobject holding charSelectButton object
    [SerializeField] public GameObject listHandler;     //Gameobject holding GachaList script
    [SerializeField] public GameObject lootbox;     //Gameobject hodling lootboxHandler script and transform
    [SerializeField] public GameObject charSlot1;   //Gameobjects to represent chosen characters
    [SerializeField] public GameObject charSlot2;   
    [SerializeField] public GameObject charSlot3;
    [SerializeField] public GameObject selectButton1;
    [SerializeField] public GameObject selectButton2;
    [SerializeField] public GameObject selectButton3;
    [SerializeField] public GameObject selectSlot1;
    [SerializeField] public GameObject selectSlot2;
    [SerializeField] public GameObject selectSlot3;
    [SerializeField] public GameObject selectSlot4;
    [SerializeField] public GameObject selectSlot5;
    [SerializeField] public GameObject selectSlot6;
    [SerializeField] public GameObject backButton;
    [SerializeField] public GameObject background;

    private int slotInt = 0;
    List<GachaCharacter> selectable = new List<GachaCharacter>();

    void Awake()
    {
        gm = FindObjectOfType<GameManager>();
    }

    /*public void setChar()       //Sets character slots with appropriate sprite/char (HARDCODED FOR FIRST LEVEL)
    {
        if (listHandler.GetComponent<GachaList>().playerInven.Count >= 1)       //CHAR SLOT 1
        {
            if (listHandler.GetComponent<GachaList>().playerInven[0] == null)   
            {
                charSlot1.GetComponent<SpriteRenderer>().sprite = null;
                listHandler.GetComponent<GachaList>().slot1 = listHandler.GetComponent<GachaList>().listChar[0];
            }
            else
            {
                charSlot1.GetComponent<SpriteRenderer>().sprite = listHandler.GetComponent<GachaList>().playerInven[0].charObj.GetComponent<SpriteRenderer>().sprite;
                listHandler.GetComponent<GachaList>().slot1 = listHandler.GetComponent<GachaList>().playerInven[0];
            }
        }

        if (listHandler.GetComponent<GachaList>().playerInven.Count >= 2)       //CHAR SLOT 2
        {
            if (listHandler.GetComponent<GachaList>().playerInven[1] == null) 
            {
                charSlot2.GetComponent<SpriteRenderer>().sprite = null;
                listHandler.GetComponent<GachaList>().slot2 = listHandler.GetComponent<GachaList>().listChar[0];
            }
            else
            {
                charSlot2.GetComponent<SpriteRenderer>().sprite = listHandler.GetComponent<GachaList>().playerInven[1].charObj.GetComponent<SpriteRenderer>().sprite;
                listHandler.GetComponent<GachaList>().slot2 = listHandler.GetComponent<GachaList>().playerInven[1];
            }
        }

        if (listHandler.GetComponent<GachaList>().playerInven.Count >= 2)       //CHAR SLOT 3
        {
            if (listHandler.GetComponent<GachaList>().playerInven[2] == null) 
            {
                charSlot3.GetComponent<SpriteRenderer>().sprite = null;
                listHandler.GetComponent<GachaList>().slot3 = listHandler.GetComponent<GachaList>().listChar[0];
            }
            else
            {
                charSlot3.GetComponent<SpriteRenderer>().sprite = listHandler.GetComponent<GachaList>().playerInven[2].charObj.GetComponent<SpriteRenderer>().sprite;
                listHandler.GetComponent<GachaList>().slot3 = listHandler.GetComponent<GachaList>().playerInven[2];
            }
        }
    }*/

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
        if ((true)&&(gm.ps.WAL >= 25)) 
        {
            kaching.Play();
            gm.ps.WAL -= 25;
            StartCoroutine(lootboxButtonPressed());
        }
        else
        {
            Debug.Log("Can't Roll");
        }
    }

    public IEnumerator lootboxButtonPressed()       //Coroutine to handle selected sprite appearing on screen
    {
        disableMainMenu();
        GachaCharacter chosenChar = lootbox.GetComponent<LootboxHandler>().roll();
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

    public void back()
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

    public void charSelectScreen(int slot) 
    {
        slotInt = slot;
        List<GachaCharacter> selectable = new List<GachaCharacter>();

        selectButton1.SetActive(false); //remove previous menu
        selectButton2.SetActive(false);
        selectButton3.SetActive(false);
        selectSlot1.SetActive(true);    //enable select menu
        selectSlot2.SetActive(true);
        selectSlot3.SetActive(true);
        selectSlot4.SetActive(true);
        selectSlot5.SetActive(true);
        selectSlot6.SetActive(true);

        for (int i = 0; i < 6; i++) //inititate and create selectable list of characters
        {                                //selectalbe = owned and not selected
            if (i < listHandler.GetComponent<GachaList>().playerInven.Count) {
                if (listHandler.GetComponent<GachaList>().playerInven[i] != listHandler.GetComponent<GachaList>().slot1 &&
                    listHandler.GetComponent<GachaList>().playerInven[i] != listHandler.GetComponent<GachaList>().slot2 &&  //check if char is selected
                    listHandler.GetComponent<GachaList>().playerInven[i] != listHandler.GetComponent<GachaList>().slot3)
                {
                    selectable.Add(listHandler.GetComponent<GachaList>().playerInven[i]);   //add unselected chars to temp list
                }
                else
                {
                    i++;
                }
            }
            else
            {
                selectable.Add(listHandler.GetComponent<GachaList>().listChar[0]); //add empty char to list
            }
        }
        selectSlot1.GetComponent<Image>().sprite = selectable[0].charObj.GetComponent<SpriteRenderer>().sprite;
        selectSlot2.GetComponent<Image>().sprite = selectable[1].charObj.GetComponent<SpriteRenderer>().sprite;
        selectSlot3.GetComponent<Image>().sprite = selectable[2].charObj.GetComponent<SpriteRenderer>().sprite;
        selectSlot4.GetComponent<Image>().sprite = selectable[3].charObj.GetComponent<SpriteRenderer>().sprite;
        selectSlot5.GetComponent<Image>().sprite = selectable[4].charObj.GetComponent<SpriteRenderer>().sprite;
        selectSlot6.GetComponent<Image>().sprite = selectable[5].charObj.GetComponent<SpriteRenderer>().sprite;
    }

    public void setChar(int numChar)
    {
        switch (slotInt)
        {
            case 0:
                Debug.Log("skipped menu?");
                break;
            case 1:
                listHandler.GetComponent<GachaList>().slot1 = selectable[numChar];
                break;
            case 2:
                listHandler.GetComponent<GachaList>().slot2 = selectable[numChar];
                break;
            case 3:
                listHandler.GetComponent<GachaList>().slot3 = selectable[numChar];
                break;
            default:
                Debug.Log("broke");
                break;
        }
    }
}
