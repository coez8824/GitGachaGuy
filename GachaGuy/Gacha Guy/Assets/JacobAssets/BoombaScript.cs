using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.FilePathAttribute;

public class BoombaScript : MonoBehaviour
{
    private Animator enemyAnimator;
    public Transform target;
    public GameObject boom;
    public Collider2D explosion;
    // Start is called before the first frame update
    void Start()
    {
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
        if (collision.gameObject.CompareTag("Player"))
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
}
