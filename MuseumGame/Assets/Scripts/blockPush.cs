using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blockPush : MonoBehaviour
{
    public bool canPush = false;

    private Rigidbody2D blockrb2d;

    public float pushPower = 2.0f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.collider.tag == "Block" && canPush)
        {
            Debug.Log("pushing block");
            blockrb2d = collision.collider.GetComponent<Rigidbody2D>();
            if (GetComponent<movement>().faceRight)
            {
                blockrb2d.MovePosition(new Vector2(blockrb2d.position.x + pushPower/100, blockrb2d.position.y));
            }
            else if (!GetComponent<movement>().faceRight)
            {
                blockrb2d.MovePosition(new Vector2(blockrb2d.position.x - pushPower/100, blockrb2d.position.y));
            }
        }
    }
}
