using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEngine.GraphicsBuffer;

public class SawbaAnimation : MonoBehaviour
{
    public RoombaMovement roombaMove;
    private Transform target;
    private Animator enemyAnimator;
    private GameManager gm;
    // Start is called before the first frame update
    void Start()
    {
        gm = FindObjectOfType<GameManager>();
        enemyAnimator = GetComponent<Animator>();
        target = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            enemyAnimator.SetBool("IsMoving", true);

            //enemyAnimator.SetFloat("MoveY", transform.position.y);
            //enemyAnimator.SetFloat("MoveX", transform.position.x);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            gm.playerDamaged(10);
        }
    }
}
