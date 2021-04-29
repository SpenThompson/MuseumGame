using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrinkPowerup : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Shrink Powerup Obtained");
        GameManager.Instance.enablePowerup(3);
        Destroy(gameObject);
    }
}
