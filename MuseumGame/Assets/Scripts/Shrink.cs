using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shrink : MonoBehaviour
{
    private GameObject player;
    private bool canShrink = false;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if (true)
        {

        }
    }

    public void startShrinking()
    {
        if (canShrink)
        {
            for(int i = 0; i < 2; i++)
            {
                player.transform.localScale = new Vector3(player.transform.localScale.x*0.75f, player.transform.localScale.y*0.75f, player.transform.localScale.z);
            } 
        }
    }

    public void startUnShrinking()
    {
        if (canShrink)
        {
            for (int i = 0; i < 2; i++)
            {
                player.transform.localScale = new Vector3(player.transform.localScale.x * (4/3), player.transform.localScale.y * (4 / 3), player.transform.localScale.z);
            }
        }
    }
}
