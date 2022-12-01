using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using static UnityEngine.ParticleSystem;
using Pathfinding;

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
    }

    // Update is called once per frame
    void Update()
    {
        SuperDeathLaser = Random.Range(0, 500);
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
        if (SuperDeathLaser == key && SuperDeathLaserFiring == false)
        {
            Debug.Log("FIRE");
            StartCoroutine(FiringSuperDeathLaser());
        }
    }

    public IEnumerator StompAttack()
    {
        Debug.Log("STOMP!");
        if ((target.position.x - transform.position.x) <= range && (target.position.y - transform.position.y) <= range && (target.position.x - transform.position.x) >= -range && (target.position.y - transform.position.y) >= -range)
        {
            int randStomp = Random.Range(0, 10);
            if(randStomp == 3 && SuperDeathLaserFiring == false)
            {
                stomp.SetActive(true);
                yield return new WaitForSeconds(.1f);
                stomp.SetActive(false);
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

        if (first == true)
        {
            first = false;
            bossAnimator.SetFloat("MoveY", target.position.y - transform.position.y);
            bossAnimator.SetFloat("MoveX", target.position.x - transform.position.x);
        }

        yield return new WaitForSeconds(1f);//Delay for 1 seconds
        Destroy(gameObject);

    }
    
    IEnumerator FiringSuperDeathLaser()
    {
        
        AIPathScript.canMove = false;
        SuperDeathLaserFiring = true;
        bossAnimator.SetBool("isSetting", true);
        Quaternion firePoint = bossShooter.gameObject.transform.rotation;
        firePos = this.gameObject.transform.position;
        laserMoveY = new Vector3(0, (target.position.y - transform.position.y) * 2f, 0);
        laserMoveX = new Vector3((target.position.x - transform.position.x) * 1.9f, 0, 0);
        //shooting up
        if (target.position.y - transform.position.y > 0)
        {
            firePos = firePos + laserMoveY;
        }
        //shooting down
        else if (target.position.y - transform.position.y < 0)
        {
            firePos = firePos + laserMoveY;
        }
        //shooting right
        if (target.position.x - transform.position.x > 0)
        {
            firePos = firePos + laserMoveX;
        }
        //shooting left
        else if (target.position.x - transform.position.x < 0)
        {
            firePos = firePos + laserMoveX;
        }


        yield return new WaitForSeconds(2f);

       
        spawnedLaser = Instantiate(actualDeathLaser, firePos, firePoint);

        StartCoroutine(FinishedFiringSuperDeathLaser());
    }

    IEnumerator FinishedFiringSuperDeathLaser()
    {
        yield return new WaitForSeconds(2f);
        bossAnimator.SetBool("isSetting", false);
        AIPathScript.canMove = true;
        SuperDeathLaserFiring = false;
    }
}