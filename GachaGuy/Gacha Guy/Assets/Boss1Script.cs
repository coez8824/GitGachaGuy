using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using static UnityEngine.ParticleSystem;
using Pathfinding;
using System.Data;

public class Boss1Script : MonoBehaviour
{
    public GameObject stomp;
    public GameObject spawnedLaser;
    private Transform target;
    public float range = 10f;
    public float testX;
    public float testY;
    public Animator bossAnimator;
    public float health;
    public Collider2D collide;
    public AIPath AIPathScript;
    private bool first;
    public float SuperDeathLaser;
    public bool SuperDeathLaserFiring;
    float key;
    public GameObject actualDeathLaser;
    public BossShooter bossShooter;
    Vector3 laserMoveY;
    Vector3 laserMoveX;
    Vector3 firePos;
    Vector3 laserMove;
    public float moveX;
    public float moveY;
    //Vector3 laserMoveMaxX;
    //Vector3 laserMoveMaxY;
    public bool stomping;
    // Start is called before the first frame update
    void Start()
    {
        key = 69;
        SuperDeathLaserFiring = false;
        first = true;
        collide = GetComponent<Collider2D>();
        health = 100;
        target = GameObject.FindWithTag("Player").transform;
        StartCoroutine(StompAttack());
        stomping = false;
    }

    // Update is called once per frame
    void Update()
    {
        SuperDeathLaser = Random.Range(0, 1000);
        if (target != null && health > 0)
        {
            testX = target.position.x - transform.position.x;
            testY = target.position.y - transform.position.y;
            if (SuperDeathLaserFiring == false)
            {
                bossAnimator.SetFloat("MoveY", target.position.y - transform.position.y);
                bossAnimator.SetFloat("MoveX", target.position.x - transform.position.x);
            }
            if ((testX) <= 9.5f && (testY) <= 9.5f && (testX) >= -9.5f && (testY) >= -9.5f)
            {
                bossAnimator.SetBool("isMoving", false);
            }
            else
            {
                bossAnimator.SetBool("isMoving", true);
            }
        }
        if (SuperDeathLaser == key && SuperDeathLaserFiring == false && health > 0 && stomping == false)
        {
            Debug.Log("FIRE");
            StartCoroutine(FiringSuperDeathLaser());
        }
        if(health <= 0)
        {
            StartCoroutine(DelayedDeath());
        }
        if(transform.position.y <= 1)
        {
            bossAnimator.SetBool("hasFallen", true);
        }
    }

    IEnumerator Stomp()
    {
        stomp.SetActive(true);
        yield return new WaitForSeconds(.1f);
        stomp.SetActive(false);
    }
    public IEnumerator StompAttack()
    {
        if ((target.position.x - transform.position.x) <= range && (target.position.y - transform.position.y) <= range && (target.position.x - transform.position.x) >= -range && (target.position.y - transform.position.y) >= -range)
        {
            int randStomp = Random.Range(0, 20);
            if(randStomp == 3 && SuperDeathLaserFiring == false && health > 0 && stomping == false)
            {
                stomping = true;
                bossAnimator.SetBool("isStomping", true);
                yield return new WaitForSeconds(.4f);
                //StartCoroutine(Stomp());
                bossAnimator.SetBool("isStomping", false);
                stomping = false;
                
            }
            else{
                yield return new WaitForSeconds(1);
            }
        }
        else
        {
            yield return new WaitForSeconds(1);
        }
        StartCoroutine(StompAttack());
    }

    IEnumerator DelayedDeath()
    {
        collide.enabled = false;
        AIPathScript.canMove = false;
        bossAnimator.SetBool("isDead", true);
        bossShooter.StopAllCoroutines();

        if (first == true)
        {
            first = false;
            bossAnimator.SetFloat("MoveY", target.position.y - transform.position.y);
            bossAnimator.SetFloat("MoveX", target.position.x - transform.position.x);
        }

        yield return new WaitForSeconds(2f);//Delay for 1 seconds
        //Destroy(gameObject);

    }

    IEnumerator FiringSuperDeathLaser()
    {

        AIPathScript.canMove = false;
        SuperDeathLaserFiring = true;
        bossAnimator.SetBool("isSetting", true);
        Quaternion fireRotation = bossShooter.gameObject.transform.rotation;

        yield return new WaitForSeconds(2f);
        spawnedLaser = Instantiate(actualDeathLaser, this.gameObject.transform.position, fireRotation);

        StartCoroutine(FinishedFiringSuperDeathLaser());

        IEnumerator FinishedFiringSuperDeathLaser()
        {
            yield return new WaitForSeconds(2f);
            bossAnimator.SetBool("isSetting", false);
            AIPathScript.canMove = true;
            SuperDeathLaserFiring = false;
        }
    }
}