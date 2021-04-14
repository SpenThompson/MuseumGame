using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarCustom : MonoBehaviour
{
    public Slider healthBar;
    public Health playerHealth;

    // Start is called before the first frame update
    void Start()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
        healthBar = GetComponent<Slider>();
        healthBar.maxValue = playerHealth.maxHealth;
        healthBar.value = playerHealth.maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetHealth(int hp)
    {
        healthBar.value = hp;
    }
}
