using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour
{
    SpriteRenderer DaggerSprite;

    public float speed;
    public float lifetime;

    private Rigidbody2D Dagger;

    void Awake()
    {
        Dagger = GetComponent<Rigidbody2D>();
        DaggerSprite = GetComponent<SpriteRenderer>();
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
        if (Dagger.velocity.x < 0)
        {
            DaggerSprite.flipX = true;
        }
        else
        {
            DaggerSprite.flipX = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "Player" && collision.gameObject.tag != "PickUp")
            Destroy(this.gameObject);
    }
}