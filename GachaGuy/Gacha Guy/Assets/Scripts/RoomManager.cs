using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomManager : MonoBehaviour
{
    [SerializeField]
    private string[] roomNames;

    public SceneChange exit1;
    public SceneChange exit2;
    public SceneChange exit3;

    public void exitDecider(SceneChange x)
    {
       if (x == GameObject.FindWithTag("ExitA").GetComponent<SceneChange>())
       {
            exit1 = GameObject.FindWithTag("ExitB").GetComponent<SceneChange>();
            exit2 = GameObject.FindWithTag("ExitC").GetComponent<SceneChange>();
            exit1 = GameObject.FindWithTag("ExitD").GetComponent<SceneChange>();
            Debug.Log("Enter A");
       }
       else if (x == GameObject.FindWithTag("ExitB").GetComponent<SceneChange>())
       {
            exit1 = GameObject.FindWithTag("ExitA").GetComponent<SceneChange>();
            exit2 = GameObject.FindWithTag("ExitC").GetComponent<SceneChange>();
            exit1 = GameObject.FindWithTag("ExitD").GetComponent<SceneChange>();
            Debug.Log("Enter B");
        }
       else if (x == GameObject.FindWithTag("ExitC").GetComponent<SceneChange>())
       {
            exit1 = GameObject.FindWithTag("ExitB").GetComponent<SceneChange>();
            exit2 = GameObject.FindWithTag("ExitA").GetComponent<SceneChange>();
            exit1 = GameObject.FindWithTag("ExitD").GetComponent<SceneChange>();
            Debug.Log("Enter C");
        }
       else if (x == GameObject.FindWithTag("ExitD").GetComponent<SceneChange>())
       {
            exit1 = GameObject.FindWithTag("ExitB").GetComponent<SceneChange>();
            exit2 = GameObject.FindWithTag("ExitC").GetComponent<SceneChange>();
            exit1 = GameObject.FindWithTag("ExitA").GetComponent<SceneChange>();
            Debug.Log("Enter D");
        }

        pickRooms();
    }

    public void pickRooms()
    {
        int i = 0;

        while(i != 3)
        {
            foreach(string s in roomNames)
            {
                int j = Random.Range(0, roomNames.Length - 1);

                if(j == 0)
                {
                    if(i == 0)
                    {
                        exit1.sceneName = s;
                        i = 1;
                    }
                    else if (i == 1)
                    {
                        exit2.sceneName = s;
                        i = 2;
                    }
                    else if (i == 2)
                    {
                        exit3.sceneName = s;
                        i = 3;
                    }
                }
            }
        }
    }
}
