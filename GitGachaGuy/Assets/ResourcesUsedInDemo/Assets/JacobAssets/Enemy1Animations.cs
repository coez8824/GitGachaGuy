using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1Animations : MonoBehaviour 
{ 
    private Animator enemyAnimator;
    public Transform target;
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
            enemyAnimator.SetBool("isMoving", true);

            enemyAnimator.SetFloat("MoveY", target.position.y - transform.position.y);
            enemyAnimator.SetFloat("MoveX", target.position.x - transform.position.x);
        }
    }
}
