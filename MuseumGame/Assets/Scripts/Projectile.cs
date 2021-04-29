using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnCollisionEnter2D(Collision2D collision2D)
    {

        if (!collision2D.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }

    }
    void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (!collider2D.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
