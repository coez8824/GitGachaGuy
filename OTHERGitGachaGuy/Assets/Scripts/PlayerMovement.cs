using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public int playerSpeed;

    private PlayerInput playerInput;
    private Vector2 inputMove;
    private Vector2 inputLook;
    private Vector2 moveVec;
    private Vector2 lookVec;
    private Rigidbody2D rb;

    private GunScript gunScript;

    // Start is called before the first frame update
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody2D>();

        gunScript = GetComponent<GunScript>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Gets input for left "Move" stick
        inputMove = playerInput.actions["Move"].ReadValue<Vector2>();
        moveVec = new Vector2(inputMove.x, inputMove.y); 

        //Gets input for right "Look: stick
        inputLook = playerInput.actions["Look"].ReadValue<Vector2>();

        //Moves player based on input
        Vector2 currMovement = moveVec * playerSpeed * Time.deltaTime;
        rb.MovePosition(rb.position + currMovement);

        //Activates if look stick is moved
        if (inputLook.magnitude > 0f)
        {
            Vector3 currRotation = Vector3.right * inputLook.x + Vector3.up * inputLook.y;
            Quaternion playerRotation = Quaternion.LookRotation(currRotation, Vector3.forward);

            rb.SetRotation(playerRotation);

            if(gunScript.canShoot)
                gunScript.shoot();
        }

    }
}
