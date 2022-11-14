using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.FilePathAttribute;

public class DropScript : MonoBehaviour
{
    public GameObject health;
    public GameObject ammo;
    public GameObject rifle;
    public GameObject sniper;
    public GameObject luck;


    public void yell()
    {
        Debug.Log("AAAAAAAAA");

        int i = Random.Range(0, 10);

        
        if (i == 1)
        {
            Debug.Log("Drop Gun");
            int j = Random.Range(0, 2);
            
            if(i == 0)
            {
                Debug.Log("Give Rifle");
                Instantiate(rifle, new Vector3(this.transform.position.x, this.transform.position.y, 0), Quaternion.identity);
            }
            else
            {
                Debug.Log("Give Sniper");
                Instantiate(sniper, new Vector3(this.transform.position.x, this.transform.position.y, 0), Quaternion.identity);
            }
        }
        else if (i == 2)
        {
            Debug.Log("Drop Health");
            Instantiate(health, new Vector3(this.transform.position.x, this.transform.position.y, 0), Quaternion.identity);
        }
        else if (i == 3)
        {
            Debug.Log("Drop Ammo");
            Instantiate(ammo, new Vector3(this.transform.position.x, this.transform.position.y, 0), Quaternion.identity);
        }
        else if (i == 3)
        {
            Debug.Log("Drop Luck");
            Instantiate(luck, new Vector3(this.transform.position.x, this.transform.position.y, 0), Quaternion.identity);
        }
        else
        {
            Debug.Log("Drop Nothing");
        }
    }
}
