using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GachaList : MonoBehaviour
{
    public List<GachaCharacter> listChar = new List<GachaCharacter>();
    public List<GachaCharacter> playerInven = new List<GachaCharacter>();

    [SerializeField] public GameObject nosfieDracul;
    [SerializeField] public GameObject maFaker;
    [SerializeField] public GameObject aliceStudos;
    [SerializeField] public GameObject fluorideSuninwind;
    [SerializeField] public GameObject pennyPoe;
    [SerializeField] public GameObject grelian;

    void Awake()
    {
        buildGachaList();
    }

    void buildGachaList()
    {
        //listChar.Add(new GachaCharacter(1, "Nosfie Dracul", "Health Steal", 5, nosfieDracul));
        listChar.Add(new GachaCharacter(2, "Ma Faker", "Income", 10, maFaker));
        //listChar.Add(new GachaCharacter(3, "Alice Studos", "Aggro Buff", 15, aliceStudos));
        listChar.Add(new GachaCharacter(4, "Fluoride Suninwind", "Pre-Heal", 10, fluorideSuninwind));
        //listChar.Add(new GachaCharacter(5, "Penny Poe", "Thorns", 20, pennyPoe));
        listChar.Add(new GachaCharacter(6, "Grelian", "Sheild Buff", 15, grelian));
    }

}
