using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneHandler : MonoBehaviour
{
    [SerializeField] public GameObject lootBoxButton;   //Gameobject holding lootboxButton object
    [SerializeField] public GameObject characterSelectButton;     //Gameobject holding charSelectButton object
    [SerializeField] public GameObject listHandler;     //Gameobject holding GachaList script
    [SerializeField] public GameObject lootbox;     //Gameobject hodling lootboxHandler script and transform
    [SerializeField] public GameObject charSlot1;   //Gameobjects to represent chosen characters
    [SerializeField] public GameObject charSlot2;   
    [SerializeField] public GameObject charSlot3;

    void Awake()
    {
        setChar();      //default parameters set all 3 charSlots to null (no sprites)
    }

    public void setChar()       //Sets character slots with appropriate sprite/char (HARDCODED FOR FIRST LEVEL)
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
    }

    public void disableMainMenu()       //Shortcut to disable main menu objects
    {
        lootBoxButton.SetActive(false);
        //characterSelectButton.SetActive(false);
        charSlot1.SetActive(false);
        charSlot2.SetActive(false);
        charSlot3.SetActive(false);
    }

    public void enableMainMenu()        //Shortcut to enable main menu objects
    {
        lootBoxButton.SetActive(true);
        //characterSelectButton.SetActive(true);
        charSlot1.SetActive(true);
        charSlot2.SetActive(true);
        charSlot3.SetActive(true);
    }

    /*public void enableCharSelect()        //Shortcut to enable charSelect screen functionality
    {
                                        //not yet implemented
    }*/

    public void lootboxExecuter()       //in-script method to execute coroutine to avoid errors (called from lootboxButton object)
    {
        if (true) 
        {
            StartCoroutine(lootboxButtonPressed());
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
        setChar();
    }

    public void charSelectButtonPressed()       //character select menu (NOT YET IMPLEMENTED)
    {
        disableMainMenu();
        //enableCharSelect();
    }
}
