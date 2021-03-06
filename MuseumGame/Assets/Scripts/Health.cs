using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int currentHealth;
    public int maxHealth = 100;
    public int lavaDamage = 10;

    public HealthBarCustom healthBar;

    private Rigidbody2D rb2d;

    void OnTriggerEnter2D(Collider2D collider2D)
    {
        Debug.Log("colliding");
        if (collider2D.gameObject.CompareTag("EnemyProjectile") || collider2D.gameObject.CompareTag("Enemy"))
        {
            DamagePlayer(10);
        }
      
    }
    void OnCollisionEnter2D(Collision2D collider2D)
    {
        if (collider2D.gameObject.CompareTag("EnemyPlatform"))
        {
            Debug.Log("lava");
            DamagePlayer(lavaDamage);
            rb2d.velocity = new Vector2(rb2d.velocity.x, 0);
            rb2d.AddForce(new Vector2(0, 200));
        }
        if (collider2D.gameObject.CompareTag("Dog")) 
        {
            DamagePlayer(50);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth <= 0)
        {
            GameManager.Instance.LoadLevel(GameManager.Instance.sceneToLoad, GameManager.Instance.positionToLoad);
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
