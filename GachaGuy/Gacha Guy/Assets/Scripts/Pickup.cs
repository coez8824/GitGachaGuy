using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    private GameManager gm;

    public int price;

    public GameObject buyButton;

    private void Start()
    {
        gm = FindObjectOfType<GameManager>();

        buyButton = GameObject.Find("BuyButton");
        buyButton.SetActive(false);

        buyButton.GetComponent<Button>().onClick.AddListener(purchase);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            buyButton.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            buyButton.SetActive(false);
        }
    }

    private void purchase()
    {
        if(gm.ps.WAL >= price)
        {
            gm.ps.WAL -= price;
            Destroy(gameObject);
        }
    }
}
