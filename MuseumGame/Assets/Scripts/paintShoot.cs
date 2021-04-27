using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class paintShoot : MonoBehaviour
{
    public Rigidbody2D paintBlob;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Instantiate(paintBlob, player.transform);
            if (player.GetComponent<movement>().faceRight)
            {
                paintBlob.velocity = transform.TransformDirection(Vector2.right);
            }
            else
            {
                paintBlob.velocity = transform.TransformDirection(Vector2.left);
            }
        }
    }
}
