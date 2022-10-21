using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GachaList : MonoBehaviour
{
    public List<GachaCharacter> listChar = new List<GachaCharacter>();

    void Awake()
    {
        buildGachaList();
    }

    void buildGachaList()
    {
        listChar.Add(new GachaCharacter(1, "Vampire", 5));
        listChar.Add(new GachaCharacter(2, "Business Lady", 20));
    }

}
