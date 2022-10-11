using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector3 rightMovement;
    public Vector3 leftMovement;
    public Vector3 upMovement;
    public Vector3 downMovement;

    public Rigidbody2D pacmanPhysicsEngine;

    float speed = 10f;
    float currentDownForce = 0f;
    void Start()
    {
        rightMovement = new Vector3(speed, 0, 0);
        leftMovement = new Vector3(-speed, 0, 0);
        upMovement = new Vector3(0, speed, 0);
        downMovement = new Vector3(0, -speed, 0);

        //getting physics engine
        pacmanPhysicsEngine = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        /*
        if(Input.GetKey(KeyCode.RightArrow))
        {
            Debug.Log("Right arrow pressed");
            transform.Translate(rightMovement * Time.deltaTime * speed);
        }
        else if(Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(leftMovement * Time.deltaTime * speed);
        }
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.Translate(upMovement * Time.deltaTime * speed);
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            //transform.Translate(downMovement);
        }
        //Debug.Log("Update ran " + frameCount + " Times");
        //frameCount++;

    */

        if (Input.GetKey(KeyCode.RightArrow))
        {
            //  Debug.Log("Right arrow pressed");
            currentDownForce = pacmanPhysicsEngine.velocity.y;
            pacmanPhysicsEngine.velocity = new Vector3(rightMovement.x, 0, 0);


            //wont work. unity doesn't like it 
            //pacmanPhysicsEngine.velocity.y = currentDownForce;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {

            currentDownForce = pacmanPhysicsEngine.velocity.y;
            pacmanPhysicsEngine.velocity = new Vector3(leftMovement.x, 0, 0);
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            currentDownForce = pacmanPhysicsEngine.velocity.y;
            pacmanPhysicsEngine.velocity = new Vector3(0, upMovement.y, 0);
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            currentDownForce = pacmanPhysicsEngine.velocity.y;
            pacmanPhysicsEngine.velocity = new Vector3(0, downMovement.y, 0);
        }
        //Debug.Log("Update ran " + frameCount + " Times");
        //frameCount++;





    }
}