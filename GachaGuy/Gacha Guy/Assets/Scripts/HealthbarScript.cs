using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthbarScript : MonoBehaviour
{
    private GameManager gm;
    private Slider slide;

    [SerializeField]
    private bool isShield;

    // Start is called before the first frame update
    void Start()
    {
        gm = FindObjectOfType<GameManager>();
        slide = GetComponent<Slider>();

        if (isShield)
            slide.maxValue = /*gm.ps.getSHD()*/50;
        else
            slide.maxValue = /*gm.ps.getHTH()*/100;
    }

    // Update is called once per frame
    void Update()
    {
        if (isShield)
            slide.value = gm.ps.currSHD;
        else
            slide.value = gm.ps.currHTH;
    }
}
