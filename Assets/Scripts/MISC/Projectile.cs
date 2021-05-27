using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour
{
    SpriteRenderer rbSprite;

    public float speed;
    public float lifetime;

    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rbSprite = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        if (lifetime <= 0)
        {
            lifetime = 2.0f;
        }

        GetComponent<Rigidbody2D>().velocity = new Vector2(speed, 0);
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        if (rb.velocity.x < 0)
        {
            rbSprite.flipX = true;
        }
        else
        {
            rbSprite.flipX = false;
        }
        if (gameObject.tag == "EnemyProjectile")
        {
            if (rb.velocity.x < 0)
            {
                rbSprite.flipX = false;
            }
            else
            {
                rbSprite.flipX = true;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemies")
        {
            collision.gameObject.GetComponent<EnemyWalker>().IsDead();
            Destroy(this.gameObject);
        }

        else if (collision.gameObject.tag != "Player" && collision.gameObject.tag != "PickUp")
        {
            Destroy(this.gameObject);
        }
    }
}