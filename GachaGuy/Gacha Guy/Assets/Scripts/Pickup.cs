using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    private GameManager gm;

    public int price;

    public Button buyButton;

    public string effect;
    public string gun;
    public int num;

    private bool canBuy;

    public GameObject door;
    public GameObject exit;

    private GameObject p;
    private GameObject d;

    public string des;

    public bool scaling;

    public AudioSource aud;

    private void Start()
    {
        gm = FindObjectOfType<GameManager>();

        if (scaling)
            price += gm.scalingPrice;
        if (effect == "Door")
            price += gm.scalingDoor;

        p = GameObject.Find("Price");
        d = GameObject.Find("Description");

        buyButton = GameObject.Find("BuyButton").GetComponent<Button>();
        buyButton.interactable = false;

        buyButton.GetComponent<Button>().onClick.AddListener(purchase);

        canBuy = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            purchase();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            p.GetComponent<Text>().text = "$"+price.ToString();
            d.GetComponent<Text>().text = des;

            buyButton.interactable = true;
            //buyButton.SetActive(true); //Buy button in UI becomes visible when player steps over object
            canBuy = true; //Set canBuy to true so only this item can be bought
        }
        if (collision.gameObject.tag == "PickUp")
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            p.GetComponent<Text>().text = "";
            d.GetComponent<Text>().text = "";

            buyButton.interactable = false;
            //buyButton.SetActive(false); //Buy button becomes invisble when player steps over object
            canBuy=false; //Set canBuy to false so it isn't bought when buying another item
        }
    }

    private void purchase()
    {
        if((gm.ps.WAL >= price)&&(canBuy)) //If canBuy and the player has the money
        {
            aud.Play();

            gm.ps.WAL -= price; //Remove money from wallet
            gm.dangerLevel += price; //Increase danger level based on money just spent
            doEffect();

            if (scaling)
                gm.scalingPrice += 5;
            if (effect == "Door")
                gm.scalingDoor += 10;

            gm.track.thingsBought++;
            gm.track.moneySpent += price;

            Destroy(this.gameObject.GetComponent<Collider2D>());
            Destroy(this.gameObject.GetComponent<SpriteRenderer>());

            StartCoroutine(holdon());
            //Destroy(gameObject); //Destroy pickup
        }
    }

    IEnumerator holdon()
    {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }

    private void doEffect()
    {
        switch (effect)
        {
            case "newGun":
                if((gm.ps.gun1 == gun)||(gm.ps.gun2 == gun))
                {
                    gm.ps.AMM += 400;
                }
                else if (gm.ps.gun2 == "")
                {
                    gm.gs.curr1 = gm.gs.curr;
                    gm.ps.using1 = false;
                    gm.ps.gun2 = gun;
                }
                else if (gm.ps.using1)
                {
                    gm.ps.gun1 = gun;
                }
                else
                {
                    gm.ps.gun2 = gun;
                }

                gm.gSetter.gunSetter(gun, 0);
                break;

            case "Heal":
                gm.ps.currHTH += num;
                if (gm.ps.currHTH > gm.ps.getHTH())
                {
                    gm.ps.currHTH = gm.ps.getHTH();
                }
                break;

            case "Ammo":
                gm.ps.AMM += num;
                break;

            case "Door":
                DoorScript ds = door.GetComponent<DoorScript>();
                ds.open = true;
                ds.lockOpen();
                SceneChange sc = exit.GetComponent<SceneChange>();
                sc.leave = true;
                Destroy(door.GetComponent<Collider2D>());
                break;

            case "Luck":
                gm.ps.LCK += num;
                break;

            case "Damage":
                gm.ps.PAM += num;
                break;

            case "Handling":
                gm.ps.HND -= 0.1f;  //num is an int, and can't be used for this. HARDCODED
                break;

            case "Speed":
                gm.ps.SPD += 0.1f;  //num is an int and doesn't support this. HARDCODED
                break;

            default:
                Debug.Log("If this message pops up then you just bought a pickup without an effect.");
                break;
        }

        /*if(effect == "newGun")
        {

            if(gm.ps.gun2 == "")
            {
                gm.gs.curr1 = gm.gs.curr;
                gm.ps.using1 = false;
                gm.ps.gun2 = gun;
            }
            else if (gm.ps.using1)
            {
                gm.ps.gun1 = gun;
            }
            else
            {
                gm.ps.gun2 = gun;
            }

            gm.gSetter.gunSetter(gun, 0);
        }
        else if(effect == "Heal")
        {
            gm.ps.currHTH += num;
            if (gm.ps.currHTH > gm.ps.getHTH())
                gm.ps.currHTH = gm.ps.getHTH();
        }
        else if (effect == "Ammo")
        {
            gm.ps.AMM += num;
        }
        else if (effect == "Door")
        {
            DoorScript ds = door.GetComponent<DoorScript>();
            ds.open = true;
            Destroy(door.GetComponent<Collider2D>());
        }
        else if (effect == "Luck")
        {
            gm.ps.LCK += num;
        }
        else
        {
            Debug.Log("If this message pops up then you just bought a pickup without an effect.");
        }*/
    }
}
