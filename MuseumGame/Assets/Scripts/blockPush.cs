using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blockPush : MonoBehaviour
{
    public bool canPush = false;

    private Rigidbody2D rb2d;
    private Rigidbody2D blockrb2d;

    public float pushPower = 2.0f;
    public float weight = 6.0f;
    private Vector2 force;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "block" && canPush)
        {
            blockrb2d = collision.collider.GetComponent<Rigidbody2D>();
            force = blockrb2d.velocity * pushPower;
            rb2d.AddForceAtPosition(force, );
        }
    }
}
