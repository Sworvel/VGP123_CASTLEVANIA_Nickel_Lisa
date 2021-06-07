using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer), typeof(Animator))]
public class EnemyWalker : MonoBehaviour
{
    Rigidbody2D rb;
    SpriteRenderer sr;
    Animator anim;
    BoxCollider2D enemyColl;
    BoxCollider2D playerColl;
    BoxCollider2D squishColl;

    public float speed;
    public int Health;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        enemyColl = this.transform.GetComponent<BoxCollider2D>();
        //playerColl = GameObject.Find("Player").GetComponent<BoxCollider2D>();

        if (speed <= 0)
        {
            speed = 5.0f;
        }

        if (Health <= 0)
        {
            Health = 3;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!anim.GetBool("Death") && !anim.GetBool("Squish"))
        {
            if (sr.flipX)
            {
                rb.velocity = new Vector2(speed, rb.velocity.y);
            }
            else
            {
                rb.velocity = new Vector2(-speed, rb.velocity.y);
            }
        }

        //Physics2D.IgnoreCollision(enemyColl, playerColl);
        // for some reason these caused a bunch of errors with the playerColl... still works without it tho
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Barrier")
        {
            sr.flipX = !sr.flipX;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "PlayerProjectile")
        {
            IsDead();
        }

        if (collision.gameObject.tag == "Player")
        {
            GameManager.instance.PlayerDeath();
        }
    }

    public void IsDead()
    {
        Health--;
        if (Health <= 0)
        {
            anim.SetBool("Death", true);
            rb.velocity = Vector2.zero;
        }
    }

    public void IsSquished()
    {
        anim.SetBool("Squish", true);
        rb.velocity = Vector2.zero;
    }

    public void FinishedDeath()
    {
        Destroy(gameObject.transform.parent.gameObject);
    }
}
