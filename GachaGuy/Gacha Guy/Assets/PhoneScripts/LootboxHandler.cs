using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootboxHandler : MonoBehaviour
{
    [SerializeField] public GameObject listHandler;
    
    public GachaCharacter roll()
    {
        float currRarity = 0;
   
        for (int i = 1; i < listHandler.GetComponent<GachaList>().listChar.Count; i++)
        {
            listHandler.GetComponent<GachaList>().listChar[i].setLowerRarity(currRarity);
            currRarity += listHandler.GetComponent<GachaList>().listChar[i].rarity;
            listHandler.GetComponent<GachaList>().listChar[i].setUpperRarity(currRarity - 0.1f);
        }

        float chosenValue = Random.Range(0.0f, currRarity - 0.1f);
        int chosenInt = 0;

        for (int i = 0; i < listHandler.GetComponent<GachaList>().listChar.Count; i++)
        {
            if (chosenValue >= listHandler.GetComponent<GachaList>().listChar[i].lowerRarity && chosenValue <= listHandler.GetComponent<GachaList>().listChar[i].upperRarity)
            {
                chosenInt = i;
                break;
            }
        }

        GachaCharacter chosenChar = new GachaCharacter(listHandler.GetComponent<GachaList>().listChar[chosenInt]);

        if (listHandler.GetComponent<GachaList>().playerInven.Find(x => x.id == chosenChar.id) == null)
        {
            listHandler.GetComponent<GachaList>().playerInven.Add(chosenChar);
        }
        else 
        {
            listHandler.GetComponent<GachaList>().playerInven.Find(x => x.id == chosenChar.id).level++;
        }

        Debug.Log(chosenChar.name + " (" + listHandler.GetComponent<GachaList>().playerInven.Find(x => x.id == chosenChar.id).level + ")");

        return chosenChar;
    }
}
