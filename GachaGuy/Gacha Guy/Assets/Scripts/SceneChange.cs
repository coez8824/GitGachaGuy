using System.Collections;
using System.Collections.Generic;
//using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    public string sceneName;

    public Scene sceneToGoTo;

    public GameObject player;
    public GameObject playerMove;
    public GameObject i;

    public float x;
    public float y;

    public GameObject door;

    public bool leave;
    //public DoorScript ds;

    public GameObject doorPickup;

    public RoomManager rm;

    public GameObject a;
    public bool b;

    public GameObject roomTXT;

    public CanvasGroup ui;
    public CanvasGroup hide;

    void Start()
    {
        roomTXT = GameObject.Find("RoomNumText");

        player = GameObject.FindWithTag("Collection");
        playerMove = GameObject.FindWithTag("Player");
        i = GameObject.FindWithTag("Important");
        rm = GameObject.FindWithTag("RoomManager").GetComponent<RoomManager>();
        DontDestroyOnLoad(i);

        ui = GameObject.Find("Canvas Variant").GetComponent<CanvasGroup>();
        hide = GameObject.Find("HIDE").GetComponent<CanvasGroup>();

        door = doorPickup.GetComponent<Pickup>().door;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if(b)
            {
                
                SceneManager.LoadScene(sceneName);
                player.transform.position = a.transform.position;
            }
            else if (leave)
                change();
            else
            {
                door.GetComponent<DoorScript>().open = true;
                door.GetComponent<DoorScript>().byeLock();
                Destroy(door.GetComponent<Collider2D>());

                rm.exitDecider(this);

                if(doorPickup != null)
                {
                    Destroy(doorPickup);
                }
            }
        }
    }

    public void bossDoor()
    {
        door.GetComponent<DoorScript>().makeBoss();
    }

    public void change()
    {
        //y += 5;

        //sceneName = "BossRoom"; //COMMENT/REMOVE AFTER TESTING
        //x = -38;
        //y = -12;

        playerMove.transform.position = new Vector3(x, y, 0);

        ui.alpha = 0;
        ui.blocksRaycasts = false;
        hide.alpha = 1;

        if (sceneName != "BossRoom")
        {
            roomTXT.SetActive(true);
        }
        

        SceneManager.LoadScene(sceneName);
        //playerMove.transform.position = new Vector3(0, 0, 0);
        //player.transform.position = new Vector3(x, y, 0);
    }
}
