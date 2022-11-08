using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootboxHandler : MonoBehaviour
{
    public GachaList ls;
    
    public GachaCharacter roll()
    {
        float currRarity = 0;
   
        for (int i = 1; i < ls.listChar.Count; i++)
        {
            ls.listChar[i].setLowerRarity(currRarity);
            currRarity += ls.listChar[i].rarity;
            ls.listChar[i].setUpperRarity(currRarity - 0.1f);
        }

        float chosenValue = Random.Range(0.0f, currRarity - 0.1f);
        int chosenInt = 0;

        for (int i = 0; i < ls.listChar.Count; i++)
        {
            if (chosenValue >= ls.listChar[i].lowerRarity && chosenValue <= ls.listChar[i].upperRarity)
            {
                chosenInt = i;
                break;
            }
        }

        GachaCharacter chosenChar = new GachaCharacter(ls.listChar[chosenInt]);

        if (ls.playerInven.Find(x => x.id == chosenChar.id) == null)
        {
            ls.playerInven.Add(chosenChar);
        }
        else 
        {
            ls.playerInven.Find(x => x.id == chosenChar.id).level++;
        }

        Debug.Log(chosenChar.name + " (" + ls.playerInven.Find(x => x.id == chosenChar.id).level + ")");

        return chosenChar;
    }
}
