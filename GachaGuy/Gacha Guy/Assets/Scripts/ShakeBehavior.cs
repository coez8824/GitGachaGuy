using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Credit Matt Buckley on medium.com for shaking code

public class ShakeBehavior : MonoBehaviour
{
    // Transform of the GameObject you want to shake
    private Transform Transform;

    // Desired duration of the shake effect
    private float shakeDuration = 0f;

    // A measure of magnitude for the shake. Tweak based on your preference
    private float shakeMagnitude = 0.2f;

    // A measure of how quickly the shake effect should evaporate
    private float dampingSpeed = 1.0f;

    // The initial position of the GameObject
    Vector3 initialPosition;

    private GameObject pl;

    void Awake()
    {
        pl = GameObject.FindWithTag("Player");

        if (Transform == null)
        {
            Transform = GetComponent(typeof(Transform)) as Transform;
        }
    }

    void OnEnable()
    {
        initialPosition = Transform.localPosition;
    }

    void Update()
    {
        if (pl != null)
            initialPosition = pl.transform.position + new Vector3(0, 1, -5);

        if (shakeDuration > 0)
        {
            Transform.localPosition = initialPosition + Random.insideUnitSphere * shakeMagnitude;

            shakeDuration -= Time.deltaTime * dampingSpeed;
        }
        else
        {
            shakeDuration = 0f;
            Transform.localPosition = initialPosition;
        }
    }

    public void TriggerShake()
    {
        shakeDuration = 0.1f;
    }
}
