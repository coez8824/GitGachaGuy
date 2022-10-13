using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public int cash;

    public GameObject loseTXT;
    public GameObject winTXT;

    public TMP_Text cashTXT;

    public GameObject spawners;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Goal: Make a 500 bucks.");
        cash = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if ((cash > 0) && (cash < 10))
            cashTXT.text = "$00" + cash.ToString();
        else if ((cash >= 10) && (cash < 100))
            cashTXT.text = "$0" + cash.ToString();
        else if (cash >= 100)
            cashTXT.text = "$" + cash.ToString();
        else
            cashTXT.text = "$000";

        if (GameObject.FindWithTag("Player")==null)
        {
            spawners.SetActive(false);
            loseTXT.SetActive(true);
        }

        if(cash >= 50)
        {
            spawners.SetActive(false);
            winTXT.SetActive(true);
            Debug.Log("You win!");
        }
    }
}
