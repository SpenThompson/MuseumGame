using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUpPowerup : MonoBehaviour
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
        Debug.Log("HealthUp Powerup Obtained");
        GameManager.Instance.obtainedPowerups.Add(Powerup.HealthUp);
        GameManager.Instance.powerupStatus.Add(false);
        Destroy(gameObject);
    }
}
