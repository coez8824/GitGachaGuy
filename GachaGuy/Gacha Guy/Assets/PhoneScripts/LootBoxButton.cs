using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootBoxButton : MonoBehaviour
{
    [SerializeField] public GameObject phone;
    
    void OnMouseDown()
    {
        Debug.Log("Outch");
        phone.GetComponent<PhoneHandler>().lootboxExecuter();
    }
}
//StartCoroutine(phone.GetComponent<PhoneHandler>().lootboxButtonPressed());
