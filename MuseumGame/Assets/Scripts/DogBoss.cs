using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogBoss : MonoBehaviour
{
    private bool faceRight = false;
    private Rigidbody2D rb2d;
    private Vector2 v2;

    public float dogSpeed;
    public float runTime;
    public float waitTime;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        StartCoroutine(DogMove());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        rb2d.velocity = v2;
    }
    private void flip()
    {
        faceRight = !faceRight;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }
    IEnumerator DogMove()
    {
        for (; ; ) {
            if (faceRight)
            {
                v2 = new Vector2(dogSpeed, 0);
            }
            else
            {
                v2 = new Vector2(-dogSpeed, 0);
            }
            yield return new WaitForSeconds(runTime);

            v2 = new Vector2(0, 0);

            yield return new WaitForSeconds(waitTime);
            flip();
        }
    }
}
