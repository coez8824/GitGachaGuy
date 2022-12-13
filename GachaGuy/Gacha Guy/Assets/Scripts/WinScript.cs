using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinScript : MonoBehaviour
{
    Tracker tracker;

    public Text rank;
    public Text stuff;

    string r;

    void Start()
    {
        tracker = GameObject.FindWithTag("TrackerB").GetComponent<Tracker>();
    }

    // Update is called once per frame
    void Update()
    {
        stuff.text =
            "Robots Destroyed: " + tracker.robotsDestroyed.ToString() + "\n" +
            "Rooms Traversed: " + tracker.roomsTraversed.ToString() + "\n" +
            "Stuff Bought: " + tracker.thingsBought.ToString() + "\n" +
            "Gachas Rolled: " + tracker.gachasRolled.ToString() + "\n" +
            "Score: " + tracker.score.ToString() + "\n\n" +
            "Money Spent: " + tracker.moneySpent.ToString();

        if (tracker.score >= 100000000)
        {
            r = "S+++";
        }
        else if (tracker.score >= 10000000)
        {
            r = "S++";
        }
        else if (tracker.score >= 1000000)
        {
            r = "S+";
        }
        else if (tracker.score >= 100000)
        {
            r = "S";
        }
        else if (tracker.score >= 10000)
        {
            r = "A";
        }
        else if (tracker.score >= 1000)
        {
            r = "B";
        }
        else if (tracker.score >= 100)
        {
            r = "C";
        }
        else
        {
            r = "D";
        }

        rank.text = "Rank: " + r;
    }

    public void toMenu()
    {
        Destroy(GameObject.Find("TTracker"));
        SceneManager.LoadScene("Menu");
    }
}
