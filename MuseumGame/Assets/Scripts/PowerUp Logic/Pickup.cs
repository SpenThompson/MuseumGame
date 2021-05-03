using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public int artIndex;
    public Powerup powerup;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<SpriteRenderer>().enabled = !GameManager.Instance.IsArtActivated(artIndex);
        GetComponent<Collider2D>().enabled = !GameManager.Instance.IsArtActivated(artIndex);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        GameManager.Instance.enablePowerup(powerup);
        GameManager.Instance.ArtReceived(artIndex,powerup);
        Destroy(gameObject);
    }
}
