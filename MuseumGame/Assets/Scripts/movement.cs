using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    private float horizontal, vertical;
    private bool jumping = false;
    private bool faceRight = true;
    private Rigidbody2D rb2d;
    private Animator animator;

    public float _Velocity = 0.0f;
    public float _MaxVelocity = 1.0f;
    public float _Acc = 0.0f;
    public float _AccSpeed = 0.1f;
    public float _MaxAcc = 1.0f;
    public float _MinAcc = -1.0f;
    public float _Deceleration = 2f;
    public float jumpForce = 350;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        animator.SetFloat("horizontal", horizontal);

        if (horizontal < 0)
        {
            if (faceRight)
            {
                flip();
            }
            _Acc = 0;
            _Acc -= _AccSpeed;
            acceleration();
        }

        else if (horizontal > 0)
        {
            if (!faceRight)
            {
                flip();
            }
            _Acc = 0;
            _Acc += _AccSpeed;
            acceleration();
        }

        if (horizontal == 0)
        {
            deceleration();
        }

        rb2d.velocity = new Vector2(_Velocity, rb2d.velocity.y);

        if (Input.GetKeyDown("space") && !jumping)
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, 0);
            rb2d.AddForce(new Vector2(0, jumpForce));
            jumping = true;
        }
    }

    private void acceleration()
    {
        if (_Acc > _MaxAcc)
            _Acc = _MaxAcc;

        else if (_Acc < _MinAcc)
            _Acc = _MinAcc;

        _Velocity += _Acc;

        if (_Velocity > _MaxVelocity)
            _Velocity = _MaxVelocity;
        else if (_Velocity < -_MaxVelocity)
            _Velocity = -_MaxVelocity;
    }

    private void deceleration()
    {
        if (_Velocity > 0)
        {
            _Velocity -= _Deceleration;
        }
        else if (_Velocity < 0)
        {
            _Velocity += _Deceleration;
        }
        if (_Velocity < _Deceleration && _Velocity > -_Deceleration)
        {
            _Velocity = 0;
        }
    }

    private void flip()
    {
        faceRight = !faceRight;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        jumping = false;
    }
}
