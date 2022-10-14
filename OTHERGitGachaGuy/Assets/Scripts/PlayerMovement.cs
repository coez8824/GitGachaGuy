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

    // Start is called before the first frame update
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        inputMove = playerInput.actions["Move"].ReadValue<Vector2>();
        moveVec = new Vector2(inputMove.x, inputMove.y);

        inputLook = playerInput.actions["Look"].ReadValue<Vector2>();
        //lookVec = new Vector2(inputLook.x, inputLook.y);

        Vector2 currMovement = moveVec * playerSpeed * Time.deltaTime;

        rb.MovePosition(rb.position + currMovement);

        if (inputLook.magnitude > 0f)
        {
            Vector3 currRotation = Vector3.left * inputLook.x + Vector3.up * inputLook.y;
            Quaternion playerRotation = Quaternion.LookRotation(currRotation, Vector3.forward);

            rb.SetRotation(playerRotation);
        }

    }
}
