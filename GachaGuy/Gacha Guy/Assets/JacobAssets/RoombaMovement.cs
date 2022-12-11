using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

//Code quaratined with "//-" is code added by Zoom

public class RoombaMovement : MonoBehaviour
{
    //-
    private GameManager gm;
    public AudioSource ping;
    //-

    float moveSpeed = 10f;
    Rigidbody2D rb;
    private Transform target;
    private Transform wall;
    private Transform obstacle;
    public Vector3 moveDirection;

    //-
    public GameObject enemy;
    //-

    public int health = 1;

    public Animator enemyAnimator;

    // Start is called before the first frame update
    void Start()
    {
        //-
        gm = FindObjectOfType<GameManager>();
        //-

        //Physics2D.IgnoreLayerCollision(6, 7, true);
        //Physics2D.IgnoreLayerCollision(7, 7, true);

        //-
        //enemy = GameObject.FindWithTag("Shooter");
        //-

        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindWithTag("Player").transform;
        moveDirection = (target.position - transform.position).normalized * moveSpeed;

        //-
        //obstacle = GameObject.FindWithTag("Obstacle").transform;
        //-

        enemyAnimator = GetComponent<Animator>();

        rb.velocity = new Vector2(moveDirection.x, moveDirection.y);

    }

    // Update is called once per frame
    void Update()
    {

        

        if(health <= 0)
        {
            ping.Play();
            gm.ps.WAL += (1 + (2 * Random.Range(0, (gm.ps.LCK + 1)))); //Pays a base 25 cash + bonus based on luck
            Destroy(gameObject);
        }
    }

    //-
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            gm.playerDamaged(1);
        }
    }
    //-

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

            moveDirection = (target.position + transform.position).normalized * moveSpeed;
            rb.velocity = new Vector2(moveDirection.x, moveDirection.y);
            enemyAnimator.SetFloat("MoveY", target.position.y + transform.position.y);
            enemyAnimator.SetFloat("MoveX", target.position.x + transform.position.x);
        }
        if (collision.gameObject.CompareTag("Wall"))
        {
            enemyAnimator.SetFloat("MoveY", target.position.y - transform.position.y);
            enemyAnimator.SetFloat("MoveX", target.position.x - transform.position.x);
        }
        if (collision.gameObject.CompareTag("Explosion"))
        {
            health = health - 1;
        }
    }
}
