using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

//Code quaratined with "//-" is code added by Zoom

public class Collision : MonoBehaviour
{
    //-
    private Color orig;
    private SpriteRenderer sr;
    //-

    public Transform target;
    private Animator enemyAnimator;
    public int health;
    public Enemy1Animations enemyAnimations;
    public Collider2D collide;
    // Start is called before the first frame update
    void Start()
    {
        //-
        sr = GetComponent<SpriteRenderer>();
        orig = sr.color;
        //-

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

        enemyAnimator.SetFloat("MoveY", target.position.y - transform.position.y);
        enemyAnimator.SetFloat("MoveX", target.position.x - transform.position.x);

        yield return new WaitForSeconds(1f);//Delay for 1 seconds

        Destroy(gameObject);

    }

    //-
    public void playerShot(int i)
    {
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
