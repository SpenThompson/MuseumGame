using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    private GameObject player;
    public float boundaryPercent;

    private float lBound;
    private float rBound;
    private float uBound;
    private float dBound;

    public float easing;

    // Start is called before the first frame update
    void Start()
    {
        lBound = boundaryPercent * Camera.main.pixelWidth;
        rBound = Camera.main.pixelWidth - lBound;

        dBound = boundaryPercent * Camera.main.pixelHeight;
        uBound = Camera.main.pixelHeight - dBound;

        player = GameManager.Instance.GetPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        if (player)
        {
            Vector3 spriteLoc = Camera.main.WorldToScreenPoint(player.transform.position);
            Vector3 pos = transform.position;

            if (spriteLoc.x < lBound)
            {
                pos.x -= lBound - spriteLoc.x;
            }
            else if (spriteLoc.x > rBound)
            {
                pos.x += spriteLoc.x - rBound;
            }


            if (spriteLoc.y < dBound)
            {
                pos.y -= dBound - spriteLoc.y;
            }
            else if (spriteLoc.y > uBound)
            {
                pos.y += spriteLoc.y - uBound;
            }
            
            pos = Vector3.Lerp(transform.position, pos, easing);
            transform.position = pos;
        }

    }
}