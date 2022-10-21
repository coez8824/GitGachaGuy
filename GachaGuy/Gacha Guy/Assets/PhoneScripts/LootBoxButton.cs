using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootBoxButton : MonoBehaviour
{
    [SerializeField] public GameObject listHandler;
    [SerializeField] public GameObject lootbox;

    void OnMouseDown()
    {
        //hideAutoBattler();

        //this.gameObject.SetActive(false);

        lootbox.GetComponent<LootboxHandler>().roll();
    }
}
