using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shrink : MonoBehaviour
{
    private GameObject player;
    private bool canUnShrink = true;
    public bool isShrunk = true;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void startShrinking()
    {
        if (canUnShrink)
        {
            for(int i = 0; i < 3; i++)
            {
                player.transform.localScale = new Vector3(player.transform.localScale.x * (3 / 4), player.transform.localScale.y * (3 / 4), player.transform.localScale.z);
                player.GetComponent<CapsuleCollider2D>().size = new Vector2(player.GetComponent<CapsuleCollider2D>().size.x * (3 / 4), player.GetComponent<CapsuleCollider2D>().size.y * (3 / 4));
                new WaitForSeconds(0.25f);
            } 
        }
    }

    public void startUnShrinking()
    {
        if (canUnShrink)
        {
            for(int i = 0; i<3; i++)
            {
                player.transform.localScale = new Vector3(player.transform.localScale.x * (4 / 3), player.transform.localScale.y * (4 / 3), player.transform.localScale.z);
                player.GetComponent<CapsuleCollider2D>().size = new Vector2(player.GetComponent<CapsuleCollider2D>().size.x * (4 / 3), player.GetComponent<CapsuleCollider2D>().size.y * (4 / 3));
                new WaitForSeconds(0.25f);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        canUnShrink = false;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        canUnShrink = true;
    }
}
