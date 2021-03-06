using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    private float horizontal, vertical;
    public bool faceRight = true;
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
    public int maxJumps = 1;
    private int numJumps;
    private bool isGrounded = true;

    private Glider glider;

    public AudioClip[] clips;
    private AudioSource a;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        numJumps = maxJumps;
        animator = GetComponent<Animator>();
        a = GetComponent<AudioSource>();
        glider = GetComponent<Glider>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("isGliding", glider.IsGliding);

        if (Input.GetKeyDown("space") && numJumps > 0 && !GameManager.Instance.isPaused())
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, 0);
            rb2d.AddForce(new Vector2(0, jumpForce));
            isGrounded = false;
            numJumps -= 1;
        }
    }

    private void FixedUpdate()
    {

        if (!GameManager.Instance.isPaused())
        {
            horizontal = Input.GetAxisRaw("Horizontal");
            vertical = Input.GetAxisRaw("Vertical");
            animator.SetFloat("horizontal", horizontal);
        }
        else
        {
            horizontal = 0;
            vertical = 0;
        }
        

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
        JumpAnimation();
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

    /*
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.contacts.Length > 0)
        {
            //http://answers.unity.com/answers/130167/view.html
            //Check collisions from the bottom of the player, or from one way platforms to increase jumps.
            ContactPoint2D contact = collision.contacts[0];
            if ((Vector3.Dot(contact.normal, Vector3.up) > 0.5) || collision.gameObject.CompareTag("OneWay"))
            {
                isGrounded = true;
                numJumps = maxJumps;
            }
        }
    }
    */

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("grounded");
        isGrounded = true;
        numJumps = maxJumps;
    }

    private void JumpAnimation()
    {
        if (!isGrounded)
        {
            animator.SetBool("isGrounded", false);
            animator.SetFloat("velocityY", 1 * Mathf.Sign(rb2d.velocity.y));
        }
        if (isGrounded)
        {
            animator.SetBool("isGrounded", true);
            animator.SetFloat("velocityY", 0);
        }
    }

    private void landAnimation()
    {
        if (!isGrounded)
        {
            animator.SetBool("isGrounded", false);
            animator.SetFloat("velocityY", 1 * Mathf.Sign(rb2d.velocity.y));
        }
        if (isGrounded)
        {
            animator.SetBool("isGrounded", true);
            animator.SetFloat("velocityY", 0);
        }
    }
    private void Step()

    {

        a.PlayOneShot(clips[0]);

    }
    
}
