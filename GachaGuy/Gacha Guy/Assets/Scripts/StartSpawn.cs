using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class StartSpawn : MonoBehaviour
{
    private Animator enemyAnimator;

    private bool b;

    // Start is called before the first frame update
    void Start()
    {
        enemyAnimator = this.GetComponent<Animator>();

        this.GetComponent<AIDestinationSetter>().enabled = !this.GetComponent<AIDestinationSetter>().enabled;
        this.GetComponent<EnemyShooter>().enabled = !this.GetComponent<EnemyShooter>().enabled;
        this.GetComponent<Collision>().enabled = !this.GetComponent<Collision>().enabled;

        enemyAnimator.SetFloat("MoveX", 1);
        enemyAnimator.SetBool("isMoving", true);

        b = true;

        StartCoroutine(time());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(b)
        {
            transform.Translate(Vector2.right * (Time.deltaTime * 2));
        }
    }

    IEnumerator time()
    {
        yield return new WaitForSeconds(3f);
        b = false;

        enemyAnimator.SetFloat("MoveX", 0);
        enemyAnimator.SetBool("isMoving", false);

        this.GetComponent<AIDestinationSetter>().enabled = !this.GetComponent<AIDestinationSetter>().enabled;
        this.GetComponent<EnemyShooter>().enabled = !this.GetComponent<EnemyShooter>().enabled;
        this.GetComponent<Collision>().enabled = !this.GetComponent<Collision>().enabled;
    }
}
