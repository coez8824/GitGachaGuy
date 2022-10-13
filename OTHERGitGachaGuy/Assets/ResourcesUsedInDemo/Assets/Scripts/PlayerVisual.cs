using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVisual : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public GameObject realPlayer;
    //public PlayerController controller;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = realPlayer.transform.position;
    }

    public void changeSprite(Sprite s)
    {
        spriteRenderer.sprite = s;
    }

}
