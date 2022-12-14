using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GachaCharacter
{
    public int id;      //assigned ID to find in list easier
    public string name;     //name of character
    public string passive;      //name of passive 
    public float rarity;        //rarity value (used in roll())
    public float lowerRarity;       //rarity values used in roll()
    public float upperRarity;
    public int level = 0;       //current level of character (changes)
    public GameObject charObj;      //Prefab of character sprite
    public Sprite splashArt;
    public RuntimeAnimatorController splashAnim;

    public GachaCharacter(int charID, string charName, string passiveAbility, float charRarity, GameObject itsme, Sprite splash, RuntimeAnimatorController anim)
    {
        id = charID;        //Constructor
        name = charName;
        passive = passiveAbility;
        rarity = charRarity;
        charObj = itsme;
        splashArt = splash;
        splashAnim = anim;
    }

    public GachaCharacter(GachaCharacter character)
    {
        id = character.id;      //Copy constructor
        name = character.name;
        passive = character.passive;
        rarity = character.rarity;
        charObj = character.charObj;
        splashArt = character.splashArt;
        splashAnim = character.splashAnim;
    }

    public void setLowerRarity(float newRarity)
    {
        lowerRarity = newRarity;
    }

    public void setUpperRarity(float newRarity)
    {
        upperRarity = newRarity;
    }
}
