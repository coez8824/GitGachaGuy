using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using static UnityEngine.ParticleSystem;

public class meleeScript : MonoBehaviour
{

    private Animator enemyAnimator;
    public Transform target;
    bool canAttack;
    public bool first;
    public float range;
    public float testX;
    public float testY;
    // Start is called before the first frame update
    void Start()
    {
        enemyAnimator = GetComponent<Animator>();
        target = GameObject.FindWithTag("Player").transform;
        canAttack = true;
        StartCoroutine(Attack());
    }

    // Update is called once per frame
    void Update()
    {
        //StartCoroutine(Attack());
        testX = target.position.x - transform.position.x;
        testY = target.position.y - transform.position.y;
    }

    IEnumerator Attack()
    {
        first = true;

        if (target.position.y - transform.position.y < range && target.position.y - transform.position.y > -range && target.position.x - transform.position.x < range && target.position.x - transform.position.x > -range)
        {
            Debug.Log("ATTACK");
            if (first == true)
            {
                first = false;
                enemyAnimator.SetFloat("MoveY", target.position.y - transform.position.y);
                enemyAnimator.SetFloat("MoveX", target.position.x - transform.position.x);
            }

            
            if (canAttack == true)
            {
                canAttack = false;
                enemyAnimator.SetBool("isAttacking", true);
                yield return new WaitForSeconds(.21f);
                enemyAnimator.SetBool("isAttacking", false);
                StartCoroutine(AttackDelay());
            }
            else
            {
                yield return new WaitForSeconds(.1f);
            }
        }
        else
        {
            yield return new WaitForSeconds(.1f);
        }
        StartCoroutine(Attack());
    }

    IEnumerator AttackDelay()
    {
        canAttack = false;
        yield return new WaitForSeconds(.3f);
        canAttack = true;
    }
}
