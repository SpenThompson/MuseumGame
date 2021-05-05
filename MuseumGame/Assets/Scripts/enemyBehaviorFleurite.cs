using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyBehaviorFleurite : MonoBehaviour
{
    private Rigidbody2D RB2D;
    private Animator animation;

    public bool faceRight = false;
    public int currentHealth;
    public int maxHP = 10;
    // -1 for left, 0 for stationary, 1 for right
    public int goDirection = 0;

    void Start()
    {
        currentHealth = maxHP;
        RB2D = GetComponent<Rigidbody2D>();
        animation = GetComponent<Animator>();
        animation.SetInteger("hp", currentHealth);
    }

    void Update()
    {
        if (currentHealth <= 0)
        {
            animation.SetInteger("hp", currentHealth);
            Object.Destroy(this.gameObject);
        }

        if (goDirection == -1)
        {
            RB2D.velocity = new Vector2(0, RB2D.velocity.y);
            RB2D.AddForce(new Vector2(10, 0));
            if (!faceRight)
            {
                flip();
            }
        } else if (goDirection == 1)
        {
            RB2D.velocity = new Vector2(0, RB2D.velocity.y);
            RB2D.AddForce(new Vector2(-10, 0));
            if (faceRight)
            {
                flip();
            }
        } else
        {
            RB2D.velocity = new Vector2(0, RB2D.velocity.y);
        }
        animation.SetInteger("direction", goDirection);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Collider2D coll = collision.collider;

        //Bouncing on Fleurite
        if (coll.name == "Player")
        {
            Vector3 conPoint = collision.contacts[0].point;
            Vector3 center = coll.bounds.center;

            bool top = Mathf.Abs(conPoint.y) > Mathf.Abs(center.y);
            bool sideCheck = (conPoint.x > center.x - 0.1) && (conPoint.x < center.x + 0.1);
            if (top && sideCheck)
            {
                currentHealth -= 10;
            }
        }
        //Getting shot
        if (coll.name == "PaintProjectile" || coll.name == "Projectile")
        {
            currentHealth -= 5;
        }
    }
    private void flip()
    {
        faceRight = !faceRight;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }
}
