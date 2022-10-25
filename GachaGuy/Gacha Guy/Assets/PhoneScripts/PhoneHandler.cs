using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneHandler : MonoBehaviour
{
    [SerializeField] public GameObject lootBoxButton;
    [SerializeField] public GameObject characterSelectButton;
    [SerializeField] public GameObject listHandler;
    [SerializeField] public GameObject lootbox;

    public void disableMainMenu()
    {
        lootBoxButton.SetActive(false);
        characterSelectButton.SetActive(false);
    }

    public void enableMainMenu()
    {
        lootBoxButton.SetActive(true);
        characterSelectButton.SetActive(true);
    }

    public void lootboxExecuter()
    {
        StartCoroutine(lootboxButtonPressed());
    }

    public IEnumerator lootboxButtonPressed()
    {
        disableMainMenu();
        GachaCharacter chosenChar = lootbox.GetComponent<LootboxHandler>().roll();
        GameObject temp = Instantiate(chosenChar.charObj, lootbox.transform);
        yield return new WaitForSeconds(3f);
        temp.SetActive(false);
        enableMainMenu();
    }

    public void charSelectButtonPressed()
    {
        disableMainMenu();
    }
}
