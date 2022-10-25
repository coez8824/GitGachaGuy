using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadScript : MonoBehaviour
{
    public PlayerController pC;

    public bool skip;

    public GameObject spinG;
    public GameObject reloadVisual;
    public GameObject square;
    public GameObject triangle;
    public GameObject circle;

    public float reloadTime;

    private string b;

    // Start is called before the first frame update
    void Start()
    {
        b = "e";

        reloadVisual.SetActive(false);
        skip = false;
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKeyDown("joystick button 0"))&&(pC.canReload))
        {
            reloadVisual.SetActive(true);
            pC.Reload(reloadTime);
            StartCoroutine(spin(reloadTime));
            buttonPicker();
        }

        if ((Input.GetKeyDown(b)) && (pC.reloading))
        {
            skip = true;
            Reload();
        }
    }

    //Button QTE is proof of concept, will need a lot(?) changed when converting to touch screen buttons
    private void buttonPicker()
    {
        square.SetActive(false);
        triangle.SetActive(false);
        circle.SetActive(false);

        int i = Random.Range(1, 3);

        //Square button to also be part of reload QTE, but a bit iffy to get working. Will probably be easier to do with touch screen.
        if(i == 0)
        {
            b = "joystick button 0";
            square.SetActive(true);
        }
        else if (i == 1)
        {
            b = "joystick button 3";
            triangle.SetActive(true);
        }
        else if (i == 2)
        {
            b = "joystick button 2";
            circle.SetActive(true);
        }
    }

    private void Reload()
    {
        int i = pC.clipMax - pC.currClip;
        pC.ammoCount -= i;
        pC.currClip += i;
        Debug.Log("SKIP RELOADED");
        pC.canShoot = true;
        pC.reloading = false;
        b = "e";
    }

    //Credit ericbegue on Unity forum for code to make object spin 360 for duration of coroutine
    IEnumerator spin(float duration)
    {
        float startRotation = transform.eulerAngles.y;
        float endRotation = startRotation + 360.0f;
        float t = 0.0f;
        while (t < duration)
        {
            if (skip == true)
            {
                spinG.transform.eulerAngles = new Vector3(0, 0, 0);
                skip = false;
                reloadVisual.SetActive(false);
                yield break;
            }
            t += Time.deltaTime;
            float zRotation = Mathf.Lerp(startRotation, endRotation, t / duration) % 360.0f;
            spinG.transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, zRotation);
            yield return null;
        }
        reloadVisual.SetActive(false);
    }
}
