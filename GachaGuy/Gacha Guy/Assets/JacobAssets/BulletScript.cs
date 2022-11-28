using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Code quaratined with "//-" is code added by Zoom

public class BulletScript : MonoBehaviour
{
    //-
    private GameManager gm;
    //-

    float moveSpeed = 20f;
    Rigidbody2D rb;
    private Transform target;
    Vector2 moveDirection;
    public GameObject enemy;
    public GameObject player;
    public PlayerMovement playMove;
    public bool ice;


    // Start is called before the first frame update
    void Start()
    {
        //-
        gm = FindObjectOfType<GameManager>();
        //-

        ice = false;
        enemy = GameObject.FindWithTag("Shooter");
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindWithTag("Player").transform;
        player = GameObject.FindWithTag("Player");
        playMove = player.GetComponentInChildren<PlayerMovement>();

        moveDirection = (target.position - transform.position).normalized * moveSpeed;
        rb.velocity = new Vector2(moveDirection.x, moveDirection.y);
        Destroy(gameObject, 5f);
        //Physics2D.IgnoreCollision(enemy.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        //Physics2D.IgnoreLayerCollision(6, 7, true);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

            if (this.gameObject.CompareTag("IceBullet") && ice == false)
            {
                ice = true;
                playMove.StartIced();
            }
            //-
            gm.playerDamaged(10);
            //-

            Destroy(gameObject);
        }
        else if(collision.gameObject.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }          
    }
}
