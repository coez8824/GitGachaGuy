using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR;

//Credit Hooson on YouTube for some starting code.

//Could this code be cleaned up? Probably. Do I fully understand it? Not really. But it works and I remember the phrase: "Don't fix what isn't broken." So I am not inclined to touch this code any more.
public class MovementJoystick : MonoBehaviour
{
    public GameObject joystick;
    public GameObject joystickBG;
    public Vector2 joystickVec;
    private Vector2 joystickTouchPos;
    private Vector2 joystickOriginalPos;
    private float joystickRadius;

    private bool point;

    // Start is called before the first frame update
    void Start()
    {
        point = false;
        joystickOriginalPos = joystickBG.transform.position;
        joystickRadius = joystickBG.GetComponent<RectTransform>().sizeDelta.y / 4;
    }

    public void PointerDown()
    {
        point = true;
        //joystick.transform.position = Input.mousePosition;
        //joystickBG.transform.position = Input.mousePosition;
        joystickTouchPos = joystickOriginalPos;
    }

    public void Drag(BaseEventData baseEventData)
    {
        PointerEventData pointerEventData = baseEventData as PointerEventData;
        Vector2 dragPos = pointerEventData.position;
        joystickVec = (dragPos - joystickTouchPos).normalized;

        float joystickDist = Vector2.Distance(dragPos, joystickTouchPos);

        if (joystickDist < joystickRadius)
        {
            joystick.transform.position = joystickTouchPos + joystickVec * joystickDist;
        }
        else
        {
            joystick.transform.position = joystickTouchPos + joystickVec * joystickRadius;
        }
    }

    public void PointerUp()
    {
        point = false;
        joystickVec = Vector2.zero;
        joystick.transform.position = joystickOriginalPos;
        //joystickBG.transform.position = joystickOriginalPos;
    }

    //Point at mouse code form BenZed on Unity Answers
    private void Update()
    {
        if (point)
        {
            //Get the Screen positions of the object
            Vector2 positionOnScreen = Camera.main.WorldToViewportPoint(transform.position);

            //Get the Screen position of the mouse
            Vector2 mouseOnScreen = (Vector2)Camera.main.ScreenToViewportPoint(Input.mousePosition);

            //Get the angle between the points
            float angle = AngleBetweenTwoPoints(positionOnScreen, mouseOnScreen);

            //Ta Daaa
            joystick.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
        }
    }

    float AngleBetweenTwoPoints(Vector3 a, Vector3 b)
    {
        return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }
}
