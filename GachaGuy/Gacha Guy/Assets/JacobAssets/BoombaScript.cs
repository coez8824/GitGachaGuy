using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
//using static UnityEditor.FilePathAttribute;

//Code quaratined with "//-" is code added by Zoom

public class BoombaScript : MonoBehaviour
{
    private Animator enemyAnimator;
    public Transform target;
    public GameObject boom;
    public Collider2D explosion;
    public float deathDelay;
    public AIPath AIPathScript;

    //-
    private GameManager gm;
    private bool caution; //Created so explosion doesn't accidentally go off twice
    public AudioSource ping;
    //-

    // Start is called before the first frame update
    void Start()
    {
        //-
        gm = FindObjectOfType<GameManager>();
        caution = false;
        //-

        enemyAnimator = GetComponent<Animator>();
        target = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            enemyAnimator.SetBool("IsMoving", true);

            enemyAnimator.SetFloat("MoveY", target.position.y - transform.position.y);
            enemyAnimator.SetFloat("MoveX", target.position.x - transform.position.x);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((collision.gameObject.CompareTag("Player"))&&(!caution))
        {
            StartCoroutine(DelayedDeath());
        }
    }
    IEnumerator DelayedDeath()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        gameObject.GetComponent<Collider2D>().enabled = false;
        AIPathScript.canMove = false;
        boom.SetActive(true);
        explosion.enabled = true;


        yield return new WaitForSeconds(deathDelay);//Delay for set amoung of seconds
        ping.Play();
        Destroy(gameObject);

    }

    //-
    public void playerShot()
    {
        caution = true;
        gm.ps.WAL += (25 + (2 * Random.Range(0, gm.ps.LCK))); //Pays a base 25 cash + bonus based on luck
        StartCoroutine(DelayedDeath());
    }
    //-
}