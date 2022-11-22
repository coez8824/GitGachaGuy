using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomManager : MonoBehaviour
{
    [SerializeField]
    private string[] roomNames;

    public SceneChange room1;
    public SceneChange room2;
    public SceneChange room3;

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
                        room1.sceneName = s;
                        i = 1;
                    }
                    else if (i == 1)
                    {
                        room2.sceneName = s;
                        i = 2;
                    }
                    else if (i == 2)
                    {
                        room3.sceneName = s;
                        i = 3;
                    }
                }
            }
        }
    }
}
