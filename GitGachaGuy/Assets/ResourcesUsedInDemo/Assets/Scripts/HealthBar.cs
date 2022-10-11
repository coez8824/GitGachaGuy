using Mono.Cecil.Cil;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Credit Weekly How on setting up health bar and health bar code

public class HealthBar : MonoBehaviour
{
    public Slider healthBar;
    public PlayerController playerHealth;

    private void Start()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        healthBar = GetComponent<Slider>();
        healthBar.maxValue = playerHealth.maxHealth;
        healthBar.value = playerHealth.maxHealth;
    }

    public void Update()
    {
        healthBar.value = playerHealth.playerHealth;
    }

    public void SetHealth(int hp)
    {
        healthBar.value = hp;
    }
}