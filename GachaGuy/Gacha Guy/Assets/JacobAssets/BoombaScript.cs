using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.FilePathAttribute;

//Code quaratined with "//-" is code added by Zoom

public class BoombaScript : MonoBehaviour
{
    private Animator enemyAnimator;
    public Transform target;
    public GameObject boom;
    public Collider2D explosion;

    //-
    private GameManager gm;
    private bool caution; //Created so explosion doesn't accidentally go off twice
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
        explosion.enabled = true;
        boom.SetActive(true);

        yield return new WaitForSeconds(.05f);//Delay for 5 seconds

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
