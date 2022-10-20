using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float playerSpeed;

    public MovementJoystick movementJoystick;

    public MovementJoystick lookJoystick;

    public GunScript gunScript;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if(movementJoystick.joystickVec.y != 0)
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

            if (gunScript.canShoot)
              gunScript.shoot();
        }
    }
}
