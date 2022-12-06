using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.SceneManagement;

public class roomShroom
{
    Scene room;
    public GameObject exit1;
    public GameObject exit2;
    public GameObject exit3;
    public GameObject exit4;
    public string type;

    public roomShroom(Scene ourRoom, GameObject north, GameObject east, GameObject south, GameObject west)
    {
        exit1 = north;
        exit2 = east;
        exit3 = south;
        exit4 = west;
    }

    public roomShroom(Scene ourRoom, GameObject north, GameObject east, GameObject south, GameObject west, string typeName)
    {
        exit1 = north;
        exit2 = east;
        exit3 = south;
        exit4 = west;
        type = typeName;
    }
}

