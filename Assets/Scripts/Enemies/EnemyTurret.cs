using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class EnemyTurret : MonoBehaviour
{
    public Transform projectileSpawnPoint;
    public Projectile projectilePrefab;
    public GameObject Player;
    public SpriteRenderer EnemyProjectile;

    Animator anim;

    public float projectileForce;
    public float agroRange;
    public float projectileFireRate;
    public int Health;

    // float timeSinceLastFire = 2.0f;
    bool canFire;

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
            projectileFireRate = 4.0f;
        }

        if (Health <= 0)
        {
            Health = 6;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Player)
        {
            float distance = Vector2.Distance(transform.position, Player.transform.position);
            if (distance <= agroRange)
            {
                canFire = true;
            }
            else
            {
                canFire = false;
            }

            if (canFire)
            {
                anim.SetBool("isActive", true);
            }
            else if (!canFire)
            {
                anim.SetBool("isActive", false);
            }
        }
        else
        {
            Player = GameManager.instance.playerInstance;
        }
    }

    public void fire()
    {
        
        if(transform.position.x < Player.transform.position.x)
        {
            Projectile temp = Instantiate(projectilePrefab, projectileSpawnPoint.position, projectileSpawnPoint.rotation);
            temp.speed = projectileForce;
            //EnemyProjectile.flipX = true;
        }

        if (transform.position.x > Player.transform.position.x)
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
            Health--;
            Destroy(collision.gameObject);
            if (Health <= 0)
            {
                Destroy(gameObject.transform.parent.gameObject);
            }
        }
    } 
}
