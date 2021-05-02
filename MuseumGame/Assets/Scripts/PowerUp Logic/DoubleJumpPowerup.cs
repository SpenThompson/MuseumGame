using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleJumpPowerup : MonoBehaviour
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
        Debug.Log("DoubleJump Powerup Obtained");
        GameManager.Instance.enablePowerup(Powerup.DoubleJump);
        Destroy(gameObject);
    }
}
