using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Code quaratined with "//-" is code added by Zoom

public class GunbaMovement : MonoBehaviour
{
    //-
    private GameManager gm;
    public PlayerStats ps;
    public AudioSource ping;
    //-

    float moveSpeed = 10f;
    Rigidbody2D rb;
    private Transform target;
    private Transform wall;
    private Transform obstacle;
    public Vector3 moveDirection;
    private Animator enemyAnimator;

    //-
    //public GameObject enemy;
    //-

    public int health = 1;
    // Start is called before the first frame update
    void Start()
    {

        enemyAnimator = GetComponent<Animator>();
        //-
        gm = FindObjectOfType<GameManager>();
        ps = FindObjectOfType<GameManager>().GetComponent<PlayerStats>();
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

        rb.velocity = new Vector2(moveDirection.x, moveDirection.y);
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            enemyAnimator.SetBool("isMoving", true);
            enemyAnimator.SetFloat("MoveY", target.position.y - transform.position.y);
            enemyAnimator.SetFloat("MoveX", target.position.x - transform.position.x);
        }

        if (health <= 0)
        {
            ping.Play();
            ps.WAL += (5 + (2 * Random.Range(0, (ps.LCK + 1)))); //Pays a base 1 cash + bonus based on luck

            gm.track.robotsDestroyed++;

            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //-
            //gm.playerDamaged(1);
            //-

            moveDirection = (target.position + transform.position).normalized * moveSpeed;
            rb.velocity = new Vector2(moveDirection.x, moveDirection.y);
        }
        //if (collision.gameObject.CompareTag("Wall"))
        //{
        //    moveDirection = (target.position - transform.position).normalized * moveSpeed;
        //}
        //if (collision.gameObject.CompareTag("Obstacle"))
        //{
        //    moveDirection = (obstacle.position + transform.position).normalized * moveSpeed;
        //}
        if (collision.gameObject.CompareTag("Explosion"))
        {
            health = health - 1;
        }
    }
}
