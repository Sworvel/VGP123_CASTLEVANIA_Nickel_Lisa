//What i Should do next time is separate all elements of the enemy into diferent scripts.
//eg. health is in one, sound is in another, all collisions in another, and so forth 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[RequireComponent(typeof(Animator))]
public class EnemyTurret : MonoBehaviour
{
    public Transform projectileSpawnPoint;
    public Projectile projectilePrefab;
    public GameObject Player;
    public SpriteRenderer EnemyProjectile;
    public GameObject Flame;

    Animator anim;

    AudioSource TurretTakeDamage;
    AudioSource TurretDeathAudioSource;

    public AudioClip TurretTakeDamageSFX;
    public AudioClip TurretDeathSFX;
    public AudioMixerGroup audioMixer;

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

        if (!Flame)
        {

        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Player)
        {
            float distance = Vector2.Distance(transform.position, Player.transform.position);
            if (distance <= agroRange && Health > 0)
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
        if (Health <= 0)
        {
            TurretDeath();
        }
    }

    public void fire()
    {
        
        if(transform.position.x < Player.transform.position.x)
        {
            Projectile temp = Instantiate(projectilePrefab, projectileSpawnPoint.position, projectileSpawnPoint.rotation);
            temp.speed = projectileForce;
        }

        else if (transform.position.x > Player.transform.position.x)
        {
            Projectile temp = Instantiate(projectilePrefab, projectileSpawnPoint.position, projectileSpawnPoint.rotation);
            temp.speed = -projectileForce;
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
            if (!TurretTakeDamage)
            {
                TurretTakeDamage = gameObject.AddComponent<AudioSource>();
                TurretTakeDamage.clip = TurretTakeDamageSFX;
                TurretTakeDamage.outputAudioMixerGroup = audioMixer;
                TurretTakeDamage.loop = false;
            }
            TurretTakeDamage.Play();
            Health--;
            Destroy(collision.gameObject);
        }
    }

    public void TurretDeath()
    {
        anim.SetBool("isDead", true);
    }

    public void TurretDeathSound()
    {
        if (!TurretDeathAudioSource)
        {
            TurretDeathAudioSource = gameObject.AddComponent<AudioSource>();
            TurretDeathAudioSource.clip = TurretDeathSFX;
            TurretDeathAudioSource.outputAudioMixerGroup = audioMixer;
            TurretDeathAudioSource.loop = false;
        }
        TurretDeathAudioSource.Play();
    }

    public void DestroyTurretFlame()
    {
        foreach (Transform child in transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }

    public void FinishedDeath()
    {
        Destroy(gameObject);
    }

}
