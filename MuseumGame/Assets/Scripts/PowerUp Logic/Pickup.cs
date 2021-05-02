using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public int artIndex;
    public int powerupIndex;

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
        
        GameManager.Instance.enablePowerup(powerupIndex);
        GameManager.Instance.ArtReceived(artIndex, powerupIndex);
        Destroy(gameObject);
    }
}
