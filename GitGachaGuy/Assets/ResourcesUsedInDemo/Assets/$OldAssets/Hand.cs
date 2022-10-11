using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    public GameObject hand;
    public GameObject body;
    public GameObject firePoint;
    public MovementJoystick aimJoystick;
    public PlayerShootProjectiles bang;
    public Vector2 handOrgPos;

    public float playerSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        handOrgPos = body.transform.position;
        if (aimJoystick.joystickVec.y != 0)
        {
            hand.transform.position = handOrgPos + aimJoystick.joystickVec;
            
            //bang.shoot(firePoint.transform.position);
        }
        else
        {
            hand.transform.position = handOrgPos;
        }
    }

    /*private void Update()
    {
        //Get the Screen positions of the object
        Vector2 positionOnScreen = Camera.main.WorldToViewportPoint(transform.position);

        //Get the Screen position of the mouse
        Vector2 mouseOnScreen = (Vector2)Camera.main.ScreenToViewportPoint(Input.mousePosition);

        //Get the angle between the points
        float angle = AngleBetweenTwoPoints(positionOnScreen, mouseOnScreen);

        //Ta Daaa
        hand.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
    }

    float AngleBetweenTwoPoints(Vector3 a, Vector3 b)
    {
        return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }*/
}
