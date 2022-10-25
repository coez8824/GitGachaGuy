using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GachaCharacter
{
    public int id;
    public string name;
    public string passive;
    public float rarity;
    public float lowerRarity;
    public float upperRarity;
    public int level = 1;
    public GameObject charObj;

    public GachaCharacter(int charID, string charName, string passiveAbility, float charRarity, GameObject itsme)
    {
        id = charID;
        name = charName;
        passive = passiveAbility;
        rarity = charRarity;
        charObj = itsme;
    }

    public GachaCharacter(GachaCharacter character)
    {
        id = character.id;
        name = character.name;
        passive = character.passive;
        rarity = character.rarity;
        charObj = character.charObj;
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
