using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private GameManager gm;

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
        playerSpeed = gm.ps.SPD;

        if (movementJoystick.joystickVec.y != 0)
        {
            rb.velocity = new Vector2(movementJoystick.joystickVec.x * playerSpeed, movementJoystick.joystickVec.y * playerSpeed);
        }
        else
        {
            rb.velocity = Vector2.zero;
        }

        if (lookJoystick.joystickVec.magnitude > 0f)
        {
            Vector3 currRotation = Vector3.right * lookJoystick.joystickVec.x + Vector3.up * lookJoystick.joystickVec.y;
            Quaternion playerRotation = Quaternion.LookRotation(currRotation, Vector3.forward);

            rb.SetRotation(playerRotation);

            if (gm.gs.canShoot)
              gm.gs.shoot();
        }
    }
}
