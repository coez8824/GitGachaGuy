using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    private GameManager gm;

    public PlayerVisual pv;

    //public PlayerStats playerStats;

    private float playerSpeed;

    public MovementJoystick movementJoystick;

    public MovementJoystick lookJoystick;

    //public GunScript gunScript;

    private Rigidbody2D rb;

    public bool mouse;
    public bool controller;

    public Camera c;

    Vector2 move;

    public GameObject touchUI;
    public GameObject otherTouchUI;
    public GameObject panelUI;

    private bool colorReloads;

    //Vector2 newpos;
    //Vector2 oldpos;
    //Vector2 movement;

    Vector2 rightstick;

    [SerializeField]
    private bool mouseInCam;

    //Jacob's ice varible
    public bool ice;

    private void Start()
    {
        ice = false;
        gm = FindObjectOfType<GameManager>();
        rb = GetComponent<Rigidbody2D>();

        panelUI.SetActive(false);
    }

    public void mouseBoolOn()
    {
        mouseInCam = true;
    }
    public void mouseBoolOff()
    {
        mouseInCam = false;
    }

    private void FixedUpdate()
    {

        playerSpeed = gm.ps.SPD; //Player's speed = speed stat

        //newpos = rb.position;
        //movement = (newpos - oldpos);


        //if (Vector2.Dot(rb.transform.forward, movement) < 0 )
        //{
        //    Debug.Log("back");
        //}

        if((!mouse)&&(!controller))
        {
            if (movementJoystick.joystickVec.y != 0) //If move joystick is moved
            {
                rb.velocity = new Vector2(movementJoystick.joystickVec.x * playerSpeed, movementJoystick.joystickVec.y * playerSpeed);
                pv.moving = true;
            }
            else
            {
                rb.velocity = Vector2.zero;
                pv.moving = false;
            }

            if (lookJoystick.joystickVec.magnitude > 0f) //If look joystick is moved
            {
                Vector3 currRotation = Vector3.right * lookJoystick.joystickVec.x + Vector3.up * lookJoystick.joystickVec.y;
                Quaternion playerRotation = Quaternion.LookRotation(currRotation, Vector3.forward);

                rb.SetRotation(playerRotation); //Rotate player

                if (gm.gs.canShoot) //Shoot gun
                    gm.gs.shoot();
            }
        }
        else if(!controller)
        {
            panelUI.SetActive(true);

            touchUI.SetActive(false);
            otherTouchUI.SetActive(false);

            if(Input.GetKeyDown(KeyCode.R))
            {
                gm.gs.reload();
                colorReloads = true;
            }

            if (Input.GetKeyDown(KeyCode.T))
            {
                gm.tossGun();
            }

            if (Input.GetKeyDown(KeyCode.G))
            {
                gm.swapGun();
            }

            if(colorReloads)
            {
                if (Input.GetKeyDown(KeyCode.Alpha1))
                {
                    gm.gs.reloadRed();
                    colorReloads = false;
                }
                if (Input.GetKeyDown(KeyCode.Alpha2))
                {
                    gm.gs.reloadBlue();
                    colorReloads = false;
                }
                if (Input.GetKeyDown(KeyCode.Alpha3))
                {
                    gm.gs.reloadPink();
                    colorReloads = false;
                }
            }

            move.x = Input.GetAxisRaw("Horizontal");
            move.y = Input.GetAxisRaw("Vertical");

            rb.velocity = new Vector2(move.x * playerSpeed, move.y * playerSpeed);

            if(rb.velocity != Vector2.zero)
            {
                pv.moving = true;
            }
            else
            {
                pv.moving = false;
            }

            //Credit robertbu on Unity Answers for mouse look
            Vector3 pos = c.WorldToScreenPoint(transform.position);
            Vector3 dir = Input.mousePosition - pos;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg +90;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            if (Input.GetMouseButton(0))
                if ((gm.gs.canShoot)&&(mouseInCam)) //Shoot gun
                    gm.gs.shoot();
        }
        else if(controller)
        {
            touchUI.SetActive(false);
            otherTouchUI.SetActive(false);

            if (Input.GetButton("Jump"))
            {
                gm.gs.reload();
                colorReloads = true;
            }

            if (Input.GetButton("Debug Previous"))
            {
                Debug.Log("Controller Toss");
                gm.tossGun();
            }

            if (Input.GetButton("Debug Next"))
            {
                Debug.Log("Controller Swap");
                gm.swapGun();
            }

            if (colorReloads)
            {
                if (Input.GetButton("Fire3"))
                {
                    gm.gs.reloadRed();
                    colorReloads = false;
                }
                if (Input.GetButton("Fire2"))
                {
                    gm.gs.reloadBlue();
                    colorReloads = false;
                }
                if (Input.GetButton("Fire1"))
                {
                    gm.gs.reloadPink();
                    colorReloads = false;
                }
            }

            //Debug.Log("c");
            move.x = Input.GetAxisRaw("Horizontal");
            move.y = Input.GetAxisRaw("Vertical");

            rb.velocity = new Vector2(move.x * playerSpeed, move.y * playerSpeed);

            if (rb.velocity != Vector2.zero)
            {
                pv.moving = true;
            }
            else
            {
                pv.moving = false;
            }

            rightstick = new Vector2(Input.GetAxis("R_Horizontal"), Input.GetAxis("R_Vertical"));

            if (rightstick.magnitude >= 1f)
            {
                //Debug.Log(rightstick.magnitude);
                Vector3 currRotation = Vector3.left * rightstick.x + Vector3.up * rightstick.y;
                Quaternion playerRotation = Quaternion.LookRotation(currRotation, Vector3.forward);

                rb.SetRotation(playerRotation);

                if (gm.gs.canShoot) //Shoot gun
                    gm.gs.shoot();
            }
        }
    }

    //private void LateUpdate()
    //{
    //    oldpos = rb.position;
    //}

    private void OnCollisionEnter2D(Collision2D collision) //Use this for player entering hazards
    {
        if (collision.gameObject.CompareTag("Explosion"))
        {
            gm.playerDamaged(10);
        }
        if (gm.thornsActive == true && collision.gameObject.CompareTag("Shooter"))
        {
            collision.gameObject.GetComponent<Collision>().playerShot(gm.thornsLevel);
        }
        if (gm.thornsActive == true && collision.gameObject.CompareTag("Boomba"))
        {
            collision.gameObject.GetComponent<BoombaScript>().playerShot();
        }
        if (gm.thornsActive == true && collision.gameObject.CompareTag("Gunba"))
        {
            collision.gameObject.GetComponent<GunbaMovement>().health -= (gm.thornsLevel);
        }
        if (gm.thornsActive == true && collision.gameObject.CompareTag("Sawba"))
        {
            collision.gameObject.GetComponent<RoombaMovement>().health -= (gm.thornsLevel);
        }
        if(gm.thornsActive == true && collision.gameObject.CompareTag("Melee"))
        {
            collision.gameObject.GetComponent<Collision>().playerShot(gm.thornsLevel);
        }
    }

    public IEnumerator iced()
    {
        if (ice == false)
        {
            ice = true;
            gm.ps.SPD = gm.ps.SPD - 5;
            yield return new WaitForSeconds(3);
            gm.ps.SPD = gm.ps.SPD + 5;
            ice = false;
        }
        
    }

    public void StartIced()
    {
        StartCoroutine(iced());
    }
}
