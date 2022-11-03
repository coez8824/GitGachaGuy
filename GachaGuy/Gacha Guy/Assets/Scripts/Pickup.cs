using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    private GameManager gm;

    public int price;

    public GameObject buyButton;

    public string effect;

    private bool canBuy;

    private void Start()
    {
        gm = FindObjectOfType<GameManager>();

        buyButton = GameObject.Find("BuyButton");
        buyButton.SetActive(false);

        buyButton.GetComponent<Button>().onClick.AddListener(purchase);

        canBuy = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            buyButton.SetActive(true); //Buy button in UI becomes visible when player steps over object
            canBuy = true; //Set canBuy to true so only this item can be bought
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            buyButton.SetActive(false); //Buy button becomes invisble when player steps over object
            canBuy=false; //Set canBuy to false so it isn't bought when buying another item
        }
    }

    private void purchase()
    {
        if((gm.ps.WAL >= price)&&(canBuy)) //If canBuy and the player has the money
        {
            gm.ps.WAL -= price; //Remove money from wallet
            gm.dangerLevel += price; //Increase danger level based on money just spent
            doEffect();
            Destroy(gameObject); //Destroy pickup
        }
    }

    private void doEffect()
    {
        if(effect == "newGun")
        {

            if(gm.ps.gun2 == "")
            {
                gm.ps.gun2 = "Rifle";
            }
            else if (gm.ps.using1)
            {
                gm.ps.gun1 = "Rifle";
            }
            else
            {
                gm.ps.gun2 = "Rifle";
            }

            gm.gSetter.gunSetter("Rifle");
        }
        else
        {
            Debug.Log("If this message pops up then you just bought a pickup without an effect.");
        }
    }
}
