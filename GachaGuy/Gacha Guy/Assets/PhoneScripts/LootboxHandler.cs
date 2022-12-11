using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LootboxHandler : MonoBehaviour
{
    public GachaList ls;
    public TMP_Text levelLabel;

    public GachaCharacter roll()    //method for getting which char you get from a lootbox
    {
        float currRarity = 0;   //value to determine the max range to find random float
   
        for (int i = 1; i < ls.listChar.Count; i++) //goes through listChar, sets lowerRarity to curr value, adds its rarity,
        {                                           //then sets its upperRarity (minus 0.1) and this determines its range for selection
            ls.listChar[i].setLowerRarity(currRarity);
            currRarity += ls.listChar[i].rarity;
            ls.listChar[i].setUpperRarity(currRarity - 0.1f);
        }

        float chosenValue = Random.Range(0.1f, currRarity - 0.1f); //gets and stores randomized value
        int chosenInt = 1;                              //value to be used to get the index of chosen char

        for (int i = 1; i < ls.listChar.Count; i++)     //searches through listChar to find the rolled char
        {
            if (chosenValue >= ls.listChar[i].lowerRarity && chosenValue <= ls.listChar[i].upperRarity)
            {                                    //uses the stored lower/upper rarities to check if the selected value lies between them
                chosenInt = i;               //sets index 
                break;                      //ends the loop
            }
        }

        GachaCharacter chosenChar = new GachaCharacter(ls.listChar[chosenInt]); //local variable for new character


        if (ls.playerInven.Find(x => x.id == chosenChar.id) == null) //if rolled char IS NOT in inventory
        {
            chosenChar.level++;
            ls.playerInven.Add(chosenChar);             //add to inventory
        }
        else                                        //if rolled char IS in inventory
        {
            ls.playerInven.Find(x => x.id == chosenChar.id).level++; //increment level of char by 1
            if (ls.playerInven.Find(x => x.id == chosenChar.id).name == ls.slot1.name)
            {
                ls.change(ls.slot1, ls.playerInven.Find(x => x.id == chosenChar.id), 1);
            }
            if (ls.playerInven.Find(x => x.id == chosenChar.id).name == ls.slot2.name)
            {
                ls.change(ls.slot2, ls.playerInven.Find(x => x.id == chosenChar.id), 2);
            }
            if (ls.playerInven.Find(x => x.id == chosenChar.id).name == ls.slot3.name)
            {
                ls.change(ls.slot3, ls.playerInven.Find(x => x.id == chosenChar.id), 3);
            }
        }

        levelLabel.text = (ls.playerInven.Find(x => x.id == chosenChar.id).level - 1).ToString() + " >>> " + ls.playerInven.Find(x => x.id == chosenChar.id).level.ToString();

        Debug.Log(chosenChar.name + " (" + ls.playerInven.Find(x => x.id == chosenChar.id).level + ")"); //outputs name/level

        return chosenChar;
    }
}
