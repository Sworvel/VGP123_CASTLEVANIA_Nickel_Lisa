using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class EnemyTurret : MonoBehaviour
{
    public Transform projectileSpawnPoint;
    public Projectile projectilePrefab;
    public Transform Player;
    public SpriteRenderer EnemyProjectile;

    Animator anim;

    public float projectileForce;
    public float agroRange;
    public float projectileFireRate;
    public int health;

    float timeSinceLastFire = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();

        if (projectileForce <= 0)
        {
            projectileForce = 3.0f;
        }

        if (projectileFireRate <= 0)
        {
            projectileFireRate = 2.0f;
        }

        if (health <= 0)
        {
            health = 6;
        }

        if (agroRange <= 0)
        {
            agroRange = 4.0f;
        }
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            anim.SetBool("isActive", true);
        }
    }

    public void fire()
    {
        if(transform.position.x < Player.position.x)
        {
            Projectile temp = Instantiate(projectilePrefab, projectileSpawnPoint.position, projectileSpawnPoint.rotation);
            temp.speed = projectileForce;
            //EnemyProjectile.flipX = true;
        }

        if (transform.position.x > Player.position.x)
        {
            Projectile temp = Instantiate(projectilePrefab, projectileSpawnPoint.position, projectileSpawnPoint.rotation);
            temp.speed = -projectileForce;
            //EnemyProjectile.flipX = false;
        }
    }

    public void ReturnToIdle()
    {
        anim.SetBool("isActive", false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "PlayerProjectile")
        {
            health--;
            Destroy(collision.gameObject);
            if (health <= 0)
            {
                Destroy(gameObject.transform.parent.gameObject);
            }
        }
    }
}
