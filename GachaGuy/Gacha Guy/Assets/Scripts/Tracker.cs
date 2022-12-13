using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tracker : MonoBehaviour
{
    public int robotsDestroyed;
    public int moneySpent;
    public int thingsBought;
    public int gachasRolled;
    public int roomsTraversed;

    public int score;

    private void Update()
    {
        score = robotsDestroyed + thingsBought + roomsTraversed + gachasRolled;
    }

    public void give(Tracker t)
    {
        robotsDestroyed = t.robotsDestroyed;
        moneySpent = t.moneySpent;
        thingsBought = t.thingsBought;
        gachasRolled = t.gachasRolled;
        roomsTraversed = t.roomsTraversed;
    }
}
