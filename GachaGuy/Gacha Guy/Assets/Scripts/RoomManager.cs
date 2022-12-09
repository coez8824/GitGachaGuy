using System.Collections;
using System.Collections.Generic;
//using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RoomManager : MonoBehaviour
{
    private GameManager gm;

    [SerializeField]
    private string[] roomNames;

    public SceneChange exit1;
    public SceneChange exit2;
    public SceneChange exit3;

    string decide;

    public GameObject roomTXT;

    private bool canBoss;

    private void Awake()
    {
        gm = FindObjectOfType<GameManager>();

        gm.roomNum++;

        

        roomTXT = GameObject.Find("RoomNumText");

        roomTXT.GetComponent<Text>().text = "ROOM " + gm.roomNum;


        StartCoroutine(turnOff());
    }

    IEnumerator turnOff()
    {
        yield return new WaitForSeconds(1);
        roomTXT.SetActive(false);
    }

    //ExitA = Bottom, ExitB = Left, ExitC = Right, ExitD = Up

    public void exitDecider(SceneChange x)
    {
       if (x == GameObject.FindWithTag("ExitA").GetComponent<SceneChange>())
       {
            exit1 = GameObject.FindWithTag("ExitB").GetComponent<SceneChange>();
            exit2 = GameObject.FindWithTag("ExitC").GetComponent<SceneChange>();
            exit3 = GameObject.FindWithTag("ExitD").GetComponent<SceneChange>();
            Debug.Log("Enter A");
            decide = "EnterA";
       }
       else if (x == GameObject.FindWithTag("ExitB").GetComponent<SceneChange>())
       {
            exit1 = GameObject.FindWithTag("ExitA").GetComponent<SceneChange>();
            exit2 = GameObject.FindWithTag("ExitC").GetComponent<SceneChange>();
            exit3 = GameObject.FindWithTag("ExitD").GetComponent<SceneChange>();
            Debug.Log("Enter B");
            decide = "EnterB";
        }
       else if (x == GameObject.FindWithTag("ExitC").GetComponent<SceneChange>())
       {
            exit1 = GameObject.FindWithTag("ExitB").GetComponent<SceneChange>();
            exit2 = GameObject.FindWithTag("ExitA").GetComponent<SceneChange>();
            exit3 = GameObject.FindWithTag("ExitD").GetComponent<SceneChange>();
            Debug.Log("Enter C");
            decide = "EnterC";
        }
       else if (x == GameObject.FindWithTag("ExitD").GetComponent<SceneChange>())
       {
            exit1 = GameObject.FindWithTag("ExitB").GetComponent<SceneChange>();
            exit2 = GameObject.FindWithTag("ExitC").GetComponent<SceneChange>();
            exit3 = GameObject.FindWithTag("ExitA").GetComponent<SceneChange>();
            Debug.Log("Enter D");
            decide = "EnterD";
        }

        pickRooms();
    }

    public void pickRooms()
    {
        //Instead of this, maybe make the player teleport to exit#.transform.position

        int i = Random.Range(0, roomNames.Length - 1);

        //i = 8;

        if(decide != "EnterB") 
        {
            if(i == 0)
            {
                exit1.x = 19;
                exit1.y = -3;
            }
            else if (i == 1)
            {
                exit1.x = 15;
                exit1.y = -19;
            }
            else if (i == 2)
            {
                exit1.x = 35;
                exit1.y = 9;
            }
            else if (i == 3)
            {
                exit1.x = 28;
                exit1.y = 24;
            }
            else if (i == 4)
            {
                exit1.x = 30;
                exit1.y = 9;
            }
            else if (i == 5)
            {
                exit1.x = 42;
                exit1.y = 4;
            }
            else if (i == 6)
            {
                exit1.x = 27;
                exit1.y = -3;
            }
            else if (i == 7)
            {
                exit1.x = 18;
                exit1.y = 21;
            }
            else if (i == 8)
            {
                exit1.x = 17;
                exit1.y = 11;
            }
        }
        else
        {
            if (i == 0)
            {
                exit1.x = 0;
                exit1.y = 9;
            }
            else if (i == 1)
            {
                exit1.x = 0;
                exit1.y = 17;
            }
            else if (i == 2)
            {
                exit1.x = -22;
                exit1.y = 26;
            }
            else if (i == 3)
            {
                exit1.x = 5;
                exit1.y = 43;
            }
            else if (i == 4)
            {
                exit1.x = 9;
                exit1.y = 31;
            }
            else if (i == 5)
            {
                exit1.x = -2;
                exit1.y = 13;
            }
            else if (i == 6)
            {
                exit1.x = 0;
                exit1.y = 25;
            }
            else if (i == 7)
            {
                exit1.x = 7;
                exit1.y = 67;
            }
            else if (i == 8)
            {
                exit1.x = -11;
                exit1.y = 24;
            }
        }

        exit1.sceneName = roomNames[i];

        int j = Random.Range(0, roomNames.Length - 1);

        //j = 8;

        if (decide != "EnterC")
        {
            if (j == 0)
            {
                exit2.x = -19;
                exit2.y = -3;
            }
            else if (j == 1)
            {
                exit2.x = -15;
                exit2.y = -16;
            }
            else if (j == 2)
            {
                exit2.x = -36;
                exit2.y = -11;
            }
            else if (j == 3)
            {
                exit2.x = -26;
                exit2.y = -1;
            }
            else if (j == 4)
            {
                exit2.x = -12;
                exit2.y = 9;
            }
            else if (j == 5)
            {
                exit2.x = -62;
                exit2.y = 3;
            }
            else if (j == 6)
            {
                exit2.x = -27;
                exit2.y = -3;
            }
            else if (j == 7)
            {
                exit2.x = -19;
                exit2.y = 28;
            }
            else if (j == 8)
            {
                exit2.x = -27;
                exit2.y = -4;
            }
        }
        else
        {
            if (j == 0)
            {
                exit2.x = 0;
                exit2.y = 9;
            }
            else if (j == 1)
            {
                exit2.x = 0;
                exit2.y = 17;
            }
            else if (j == 2)
            {
                exit2.x = -22;
                exit2.y = 26;
            }
            else if (j == 3)
            {
                exit2.x = 5;
                exit2.y = 43;
            }
            else if (j == 4)
            {
                exit2.x = 9;
                exit2.y = 31;
            }
            else if (j == 5)
            {
                exit2.x = -2;
                exit2.y = 13;
            }
            else if (j == 6)
            {
                exit2.x = 0;
                exit2.y = 25;
            }
            else if (j == 7)
            {
                exit2.x = 7;
                exit2.y = 67;
            }
            else if (j == 8)
            {
                exit2.x = -11;
                exit2.y = 24;
            }
        }

        exit2.sceneName = roomNames[j];

        int k=-1;
        if (gm.roomNum > 5)
        {
            k = Random.Range(0, roomNames.Length);

            k = 9;
        }
        else
        {
            k = Random.Range(0, roomNames.Length - 1);
        }
        

        //k = 8;

        if (decide != "EnterD")
        {
            if (k == 0)
            {
                exit3.x = 0;
                exit3.y = -15;
            }
            else if (k == 1)
            {
                exit3.x = -6;
                exit3.y = -26;
            }
            else if (k == 2)
            {
                exit3.x = 26;
                exit3.y = -26;
            }
            else if (k == 3)
            {
                exit3.x = -14;
                exit3.y = -10;
            }
            else if (k == 4)
            {
                exit3.x = 9;
                exit3.y = -12;
            }
            else if (k == 5)
            {
                exit3.x = -2;
                exit3.y = -8;
            }
            else if (k == 6)
            {
                exit3.x = 0;
                exit3.y = -30;
            }
            else if (k == 7)
            {
                exit3.x = -9;
                exit3.y = -9;
            }
            else if (k == 8)
            {
                exit3.x = -7;
                exit3.y = -17;
            }
            else if (k == 9)
            {
                exit3.bossDoor();
                exit3.x = -38;
                exit3.y = -12;
            }
        }
        else
        {
            if (k == 0)
            {
                exit3.x = 0;
                exit3.y = 9;
            }
            else if (k == 1)
            {
                exit3.x = 0;
                exit3.y = 17;
            }
            else if (k == 2)
            {
                exit3.x = -22;
                exit3.y = 26;
            }
            else if (k == 3)
            {
                exit3.x = 5;
                exit3.y = 43;
            }
            else if (k == 4)
            {
                exit3.x = 9;
                exit3.y = 31;
            }
            else if (k == 5)
            {
                exit3.x = -2;
                exit3.y = 13;
            }
            else if (k == 6)
            {
                exit3.x = 0;
                exit3.y = 25;
            }
            else if (k == 7)
            {
                exit3.x = 7;
                exit3.y = 67;
            }
            else if (k == 8)
            {
                exit3.x = -11;
                exit3.y = 24;
            }
        }

        if(k==9)
        {
            exit3.sceneName = "BossRoom";
        }
        else
        {
            exit3.sceneName = roomNames[k];
        }
        
    }
}
