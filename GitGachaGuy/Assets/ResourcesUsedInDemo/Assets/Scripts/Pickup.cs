using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{

    public GameManager gM;
    public GameObject pC;

    // Start is called before the first frame update
    void Start()
    {
        gM = FindObjectOfType<GameManager>();
        pC = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if ((Input.GetKeyDown("joystick button 1")) && (collision.gameObject.tag == "Player"))
            if(gM.cash > 5)
            {
                gM.cash -= 5;
                pC.GetComponent<PlayerController>().playerHealth = pC.GetComponent<PlayerController>().maxHealth;
                Destroy(gameObject);
            }
            else
            {
                Debug.Log("Not enough dough.");
            }
            
    }
}
