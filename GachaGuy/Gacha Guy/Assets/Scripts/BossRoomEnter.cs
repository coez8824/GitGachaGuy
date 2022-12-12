using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoomEnter : MonoBehaviour
{
    public GameObject door;

    public CanvasGroup ui;
    public CanvasGroup hide;

    private void Start()
    {
        ui = GameObject.Find("Canvas Variant").GetComponent<CanvasGroup>();
        hide = GameObject.Find("HIDE").GetComponent<CanvasGroup>();

        ui.alpha = 1;
        ui.blocksRaycasts = true;
        hide.alpha = 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            door.GetComponent<DoorScript>().open = true;
            Destroy(door.GetComponent<Collider2D>());
        }
    }
}
