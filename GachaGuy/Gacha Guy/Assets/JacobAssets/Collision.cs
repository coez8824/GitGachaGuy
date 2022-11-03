using System.Collections;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

//Code quaratined with "//-" is code added by Zoom

public class Collision : MonoBehaviour
{
    //-
    private GameManager gm;
    private Color orig;
    private SpriteRenderer sr;
    public AudioSource ting;//firing sound
    public AudioSource ping;//death sound
    //-

    public AudioSource[] deathSoundArray;
    public Transform target;
    private Animator enemyAnimator;
    public int health;
    public Enemy1Animations enemyAnimations;
    public Collider2D collide;
    public int randomSound;
    EnemyShooter shooter;
    public float testX;
    public float testY;
    public bool first;
    // Start is called before the first frame update
    void Start()
    {
        //-
        gm = FindObjectOfType<GameManager>();
        sr = GetComponent<SpriteRenderer>();
        orig = sr.color;
        //-

        first = true;
        target = GameObject.FindWithTag("Player").transform;
        health = 5;
        enemyAnimator = GetComponent<Animator>();
        collide = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(health <= 0)
        {
            StartCoroutine(DelayedDeath());
        }

        if (target != null && health > 0)
        {
            testX = target.position.x - transform.position.x;
            testY = target.position.y - transform.position.y;
            enemyAnimator.SetFloat("MoveY", target.position.y - transform.position.y);
            enemyAnimator.SetFloat("MoveX", target.position.x - transform.position.x);
            if ((testX) <= 9.5f && (testY) <= 9.5f && (testX) >= -9.5f && (testY) >= -9.5f)
            {
                enemyAnimator.SetBool("isMoving", false);
            }
            else
            {
                enemyAnimator.SetBool("isMoving", true);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Explosion"))
        {
            health = health - 3;
        }
    }

    IEnumerator DelayedDeath()
    {
        collide.enabled = false;
        enemyAnimator.SetBool("isDead", true);

        if(first == true)
        {
            first = false;
            enemyAnimator.SetFloat("MoveY", target.position.y - transform.position.y);
            enemyAnimator.SetFloat("MoveX", target.position.x - transform.position.x);
        }

        randomSound = Random.Range(0, 13);
        deathSoundArray[randomSound].Play();

        yield return new WaitForSeconds(1f);//Delay for 1 seconds

        //-
        //ping.Play();
        gm.ps.WAL += (25 + (2 * Random.Range(0, gm.ps.LCK))); //Pays a base 25 cash + bonus based on luck
        //-
        Destroy(gameObject);

    }

    //-
    //Credit webcam on Unity Answers for making enemies flash red when hit
    public void playerShot(int i)
    {


        ting.Play();
        health -= i;

        sr.color = Color.red;
        Invoke("ResetColor", .1f);
    }

    private void ResetColor()
    {
        sr.color = orig;
    }
    //-
}
