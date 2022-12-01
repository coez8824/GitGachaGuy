using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    public string sceneName;

    public GameObject player;
    public GameObject i;

    public float x;
    public float y;

    public GameObject door;

    public bool leave;
    //public DoorScript ds;

    public GameObject doorPickup;

    public RoomManager rm;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        i = GameObject.FindWithTag("Important");
        rm = GameObject.FindWithTag("RoomManager").GetComponent<RoomManager>();
        DontDestroyOnLoad(i);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (leave)
                change();
            else
            {
                door.GetComponent<DoorScript>().open = true;
                Destroy(door.GetComponent<Collider2D>());

                rm.exitDecider(this);

                if(doorPickup != null)
                {
                    Destroy(doorPickup);
                }
            }
        }
    }

    public void change()
    {
        
        SceneManager.LoadScene(sceneName);
        player.transform.position = new Vector3(x, y, 0);
    }
}
