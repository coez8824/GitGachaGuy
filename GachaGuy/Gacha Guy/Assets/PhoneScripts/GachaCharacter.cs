using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GachaCharacter
{
    public int id;
    public string name;
    public float rarity;
    public float lowerRarity;
    public float upperRarity;

    public GachaCharacter(int charID, string charName, float charRarity)
    {
        id = charID;
        name = charName;
        rarity = charRarity;
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
