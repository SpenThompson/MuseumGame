using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyBehFleurite : MonoBehaviour
{
    public int currentHealth;
    public int maxHP = 10;
    void Start()
    {
        currentHealth = maxHP;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth <= 0)
        {
            
        }
    }
}
