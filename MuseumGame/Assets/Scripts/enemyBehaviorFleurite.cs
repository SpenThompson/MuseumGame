using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyBehaviorFleurite : MonoBehaviour
{
    public int currentHealth;
    public int maxHP = 10;

    void Start()
    {
        currentHealth = maxHP;
    }

    void Update()
    {
        if (currentHealth <= 0)
        {
            Object.Destroy(this.gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Collider2D coll = collision.collider;

        if (coll.name == "Player")
        {
            Vector3 conPoint = collision.contacts[0].point;
            Vector3 center = coll.bounds.center;

            bool top = Mathf.Abs(conPoint.y) > Mathf.Abs(center.y);
            bool sideCheck = (conPoint.x > center.x - 0.1) && (conPoint.x < center.x + 0.1);
            if (top && sideCheck)
            {
                currentHealth = 0;
            }
        }
    }
}
