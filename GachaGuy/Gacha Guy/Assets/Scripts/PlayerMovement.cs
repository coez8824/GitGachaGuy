using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private void Start()
    {
        gm = FindObjectOfType<GameManager>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        playerSpeed = gm.ps.SPD; //Player's speed = speed stat

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
}
