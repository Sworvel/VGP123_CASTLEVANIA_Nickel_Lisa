using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator), typeof(SpriteRenderer))]
public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;
    Animator anim;
    SpriteRenderer Simon;

    public float speed;
    public int jumpForce;
    public bool isGrounded;
    public LayerMask isGroundLayer;
    public Transform groundCheck;
    public float groundChekcRadius;
    public bool isCrouched;
    public int BounceForce;

    public int Score = 0;
    public int Health = 3;

    bool coroutineRunning = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        Simon = GetComponent<SpriteRenderer>();

        if (speed <= 0)
        {
            speed = 5.0f;
        }

        if (jumpForce <= 0)
        {
            jumpForce = 300;
        }

        if (groundChekcRadius <= 0)
        {
            groundChekcRadius = 0.2f;
        }

        if (!groundCheck)
        {
            Debug.Log("Groundcheck does not exist, please assign a ground check object.");
        }

        if (BounceForce <= 0)
        {
            BounceForce = 10;
        }
    }

    void Update()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundChekcRadius, isGroundLayer);
        isCrouched = false;

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.velocity = Vector2.zero;
            rb.AddForce(Vector2.up * jumpForce);
        }

        Vector2 moveDirection = new Vector2(horizontalInput * speed, rb.velocity.y);
        rb.velocity = moveDirection;

        float verticalInput = Input.GetAxisRaw("Vertical");

        if (Input.GetAxisRaw("Vertical") < 0)
        {
            isCrouched = true;
        }

        anim.SetFloat("speed", Mathf.Abs(horizontalInput));
        anim.SetBool("isGrounded", isGrounded);
        anim.SetBool("isCrouched", isCrouched);

        if (Simon.flipX && horizontalInput > 0 || !Simon.flipX && horizontalInput < 0)
        {
            Simon.flipX = !Simon.flipX;
        }
    }

    public void StartSpeedChange()
    {
        if (!coroutineRunning)
        {
            StartCoroutine(SpeedChange());
        }
        else
        {
            StopCoroutine(SpeedChange());
            StartCoroutine(SpeedChange());
        }
    }

    IEnumerator SpeedChange()
    {
        coroutineRunning = true;
        speed = 4;
        yield return new WaitForSeconds(5.0f);
        speed = 2;
        coroutineRunning = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "SquishCollider")
        {
            if (!isGrounded)
            {
                collision.gameObject.GetComponentInParent<EnemyWalker>().IsSquished();
                rb.velocity = Vector2.zero;
                rb.AddForce(Vector2.up * BounceForce);
            }
        }
    }
}