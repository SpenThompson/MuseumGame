using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shrink : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private CapsuleCollider2D col;
    public bool canUnShrink = true;
    public bool canShrink = false;
    public bool isShrunk = true;
    private float scaleShrink = (6f/7f);
    private float scaleUnShrink = (7f/6f);

    public Vector3 minRbSize = new Vector3(0.0f, 0.0f, 0.0f);
    public Vector2 minColSize = new Vector2(0.0f, 0.0f);
    public Vector3 defaultRbSize = new Vector3(0.0f, 0.0f, 0.0f);
    public Vector2 defaultColSize = new Vector2(0.0f, 0.0f);


    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        col = GetComponent<CapsuleCollider2D>();

        minRbSize = new Vector3(rb2d.transform.localScale.x * scaleShrink, rb2d.transform.localScale.y * scaleShrink, rb2d.transform.localScale.z);
        minColSize = new Vector2(col.size.x * scaleShrink, col.size.y * scaleShrink);
        defaultRbSize = rb2d.transform.localScale;
        defaultColSize = col.size;

    }

    // Update is called once per frame
    void Update()
    {
        print(col.size + " " + rb2d.transform.localScale);
        startShrinking();
    }

    public void startShrinking()
    {
        RaycastHit2D ceiling = Physics2D.Raycast(rb2d.transform.position, transform.TransformDirection(Vector2.up), 2f);  

        if (ceiling && isShrunk)
        {
            Debug.Log("Unable to Shrink");
            cantShrinkCheck();
        }
        else
        {
            canShrinkCheck();
        }
        StartCoroutine(shrinkLogic());
    }

    IEnumerator shrinkLogic()
    {
            if (isShrunk && canShrink)
            {
                print("true");
                for (int i = 0; i < 3; i++) //unshrink
                {
                    if(((defaultColSize.x >= col.size.x) || (defaultRbSize.x >= rb2d.transform.localScale.x)))
                    {
                        rb2d.transform.localScale = new Vector3(rb2d.transform.localScale.x * scaleUnShrink, rb2d.transform.localScale.y * scaleUnShrink, rb2d.transform.localScale.z);
                        col.size = new Vector2(col.size.x * scaleUnShrink, col.size.y * scaleUnShrink);
                        yield return new WaitForSeconds(0.1f);
                    }
                }

                isShrunk = false;
            }
            else if (!isShrunk && canUnShrink)
            {
                for (int i = 0; i < 3; i++) //shrink
                {
                if (((minColSize.x <= col.size.x) || (minRbSize.x <= rb2d.transform.localScale.x)))
                {
                    rb2d.transform.localScale = new Vector3(rb2d.transform.localScale.x * scaleShrink, rb2d.transform.localScale.y * scaleShrink, rb2d.transform.localScale.z);
                    col.size = new Vector2(col.size.x * scaleShrink, col.size.y * scaleShrink);
                    yield return new WaitForSeconds(0.1f);
                }
                }
            isShrunk = true;
        }

    }

    public void cantShrinkCheck()
    {
        canShrink = false;
        canUnShrink = true;

    }
    public void canShrinkCheck()
    {
        canShrink = true;
        canUnShrink = false;
    }
}
