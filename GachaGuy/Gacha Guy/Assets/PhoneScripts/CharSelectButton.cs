using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharSelectButton : MonoBehaviour
{
    [SerializeField] public GameObject phone;

    void OnMouseDown()
    {
        phone.GetComponent<PhoneHandler>().charSelectButtonPressed();
    }
}
