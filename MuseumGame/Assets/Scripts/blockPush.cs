using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blockPush : MonoBehaviour
{
    public bool canPush = false;

    private Rigidbody2D blockrb2d;
    private CircleCollider2D cc2d;

    public float pushPower = 2.0f;
    private float pushVar;

    // Start is called before the first frame update
    void Start()
    {
        cc2d = GetComponent<CircleCollider2D>();
        pushVar = pushPower;
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
                blockrb2d.MovePosition(new Vector2(blockrb2d.position.x + pushVar/100, blockrb2d.position.y));
            }
            else if (!GetComponent<movement>().faceRight)
            {
                blockrb2d.MovePosition(new Vector2(blockrb2d.position.x - pushVar/ 100, blockrb2d.position.y));
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Block")
        {
            pushVar = 0;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        pushVar = pushPower;
    }
}
