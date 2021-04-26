using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shrink : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private CapsuleCollider2D col;
    private bool canUnShrink = true;
    public bool isShrunk = false;
    private float scaleShrink = (6f/7f);
    private float scaleUnShrink = (7f/6f);
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        col = GetComponent<CapsuleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void startShrinking()
    {
        StartCoroutine(shrinkLogic());
    }

    IEnumerator shrinkLogic()
    {
        if (isShrunk)
        {
            for (int i = 0; i < 4; i++)
            {
                rb2d.transform.localScale = new Vector3(rb2d.transform.localScale.x * scaleUnShrink, rb2d.transform.localScale.y * scaleUnShrink, rb2d.transform.localScale.z);
                col.size = new Vector2(col.size.x * scaleUnShrink, col.size.y * scaleUnShrink);
                yield return new WaitForSeconds(0.1f);
            }
            isShrunk = false;
        }
        else
        {
            for (int i = 0; i < 4; i++)
            {
                rb2d.transform.localScale = new Vector3(rb2d.transform.localScale.x * scaleShrink, rb2d.transform.localScale.y * scaleShrink, rb2d.transform.localScale.z);
                col.size = new Vector2(col.size.x * scaleShrink, col.size.y * scaleShrink);
                yield return new WaitForSeconds(0.1f);
            }
            isShrunk = true;
        }
        StopCoroutine(shrinkLogic());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Can't Shrink");
        canUnShrink = false;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("Can Shrink");
        canUnShrink = true;
    }
}
