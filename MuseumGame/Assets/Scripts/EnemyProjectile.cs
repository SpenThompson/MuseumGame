using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        
        if (collider.gameObject.CompareTag("Player"))
        {
            //player takes damage
            Destroy(gameObject);
        }
        else if (collider.gameObject.CompareTag("Platform"))
        {
            Destroy(gameObject);
        }
    }
}
