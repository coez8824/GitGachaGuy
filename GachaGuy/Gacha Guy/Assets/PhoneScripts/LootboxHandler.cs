using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootboxHandler : MonoBehaviour
{
    [SerializeField] public GameObject listHandler;

    public void roll()
    {
        float currRarity = 0;
   
        for (int i = 0; i < listHandler.GetComponent<GachaList>().listChar.Count; i++)
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

        Debug.Log(listHandler.GetComponent<GachaList>().listChar[chosenInt].name);
    }
}
