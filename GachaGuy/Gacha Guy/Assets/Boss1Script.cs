using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using static UnityEngine.ParticleSystem;
using Pathfinding;
using System.Data;

//Code quaratined with "//-" is code added by Zoom
public class Boss1Script : MonoBehaviour
{
   public Quaternion fireRotation;
    //-
    private GameManager gm;
    private Color orig;
    private SpriteRenderer sr;
    public AudioSource ting;//firing sound
    //public AudioSource ping;//death sound
    public GameObject door;
    //-

    public GameObject stomp;
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
    public int randStomp;
    // Start is called before the first frame update
    void Start()
    {
        //-
        gm = FindObjectOfType<GameManager>();
        sr = GetComponent<SpriteRenderer>();
        orig = sr.color;
        //-

        key = 69;
        SuperDeathLaserFiring = false;
        first = true;
        collide = GetComponent<Collider2D>();
        health = 100;
        target = GameObject.FindWithTag("Player").transform;
       
        stomping = false;
    }

    // Update is called once per frame
    void Update()
    {
        SuperDeathLaser = Random.Range(0, 1000);
        randStomp = Random.Range(0, 200);
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
            SuperDeathLaserFiring = true;
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
        if (testX <= range && testY <= range && testX >= -range && testY >= -range  && randStomp == 3 && SuperDeathLaserFiring == false && health > 0 && stomping == false)
        {
            stomping = true;
            StartCoroutine(StompFrame());
        }
    }
    IEnumerator StompAttack()
    {
        bossAnimator.SetBool("isStomping", true);
        yield return new WaitForSeconds(2f);
        bossAnimator.SetBool("isStomping", false);
        stomping = false;
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

        //-
        door.GetComponent<DoorScript>().open = true;
        Destroy(door.GetComponent<Collider2D>());
        //-

        yield return new WaitForSeconds(2f);//Delay for seconds
        //Destroy(gameObject);

    }

    IEnumerator FiringSuperDeathLaser()
    {

        AIPathScript.canMove = false;
        bossAnimator.SetBool("isSetting", true);
        fireRotation = bossShooter.gameObject.transform.rotation;

        yield return new WaitForSeconds(3f);
        bossShooter.SpawnLaser();

        StartCoroutine(FinishedFiringSuperDeathLaser());
    }


    IEnumerator FinishedFiringSuperDeathLaser()
    {
        yield return new WaitForSeconds(2f);
        bossAnimator.SetBool("isSetting", false);
        AIPathScript.canMove = true;
        SuperDeathLaserFiring = false;
    }

    IEnumerator StompFrame()
    {
        yield return new WaitForEndOfFrame();
        StartCoroutine(StompAttack());

    }

    //-
    //Credit webcam on Unity Answers for making enemies flash red when hit
    public void playerShot(int i)
    {


        ting.Play();
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