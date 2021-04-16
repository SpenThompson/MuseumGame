using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int currentHealth;
    public int maxHealth = 100;

    public HealthBarCustom healthBar;

    void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.gameObject.CompareTag("EnemyProjectile") || collider2D.gameObject.CompareTag("Enemy"))
        {
            DamagePlayer(10);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth <= 0)
        {
            GameManager.Instance.LoadLevel("Spawn", new Vector3(0,0,0));
            currentHealth = maxHealth;
            healthBar.SetHealth(currentHealth);
        }
    }

    public void DamagePlayer(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }
}
