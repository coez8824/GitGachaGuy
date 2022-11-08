using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropScript : MonoBehaviour
{
    public void yell()
    {
        Debug.Log("AAAAAAAAA");

        int i = Random.Range(0, 4);

        if(i == 0)
        {
            Debug.Log("Drop Nothing");
        }
        else if (i == 1)
        {
            Debug.Log("Drop Gun");
            int j = Random.Range(0, 2);
            
            if(i == 0)
            {
                Debug.Log("Give Rifle");
            }
            else
            {
                Debug.Log("Give Sniper");
            }
        }
        else if (i == 2)
        {
            Debug.Log("Drop Health");
        }
        else if (i == 3)
        {
            Debug.Log("Drop Ammo");
        }
    }
}
