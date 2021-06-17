using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer), typeof(Animator))]
public class EnemyWalker : MonoBehaviour
{
    Rigidbody2D rb;
    SpriteRenderer sr;
    Animator anim;
    BoxCollider2D enemyColl;
    BoxCollider2D playerColl;
    BoxCollider2D squishColl;

    AudioSource TakeDaggarDamage;
    AudioSource EnemyDeathAudioSource;

    public AudioClip TakeDaggarDamageSFX;
    public AudioClip EnemyDeathSFX;
    public AudioMixerGroup audioMixer;

    public float speed;
    public int Health;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        enemyColl = this.transform.GetComponent<BoxCollider2D>();

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

        if (Health <= 0)
        {
            IsDead();
        }
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
            EnemyTakeDamage();
        }

        if (Health > 0 && collision.gameObject.tag == "Player")
        {
            GameManager.instance.PlayerDeath();
        }
    }

    public void EnemyTakeDamage()
    {     
        Health--;
        if (!TakeDaggarDamage)
        {
            TakeDaggarDamage = gameObject.AddComponent<AudioSource>();
            TakeDaggarDamage.clip = TakeDaggarDamageSFX;
            TakeDaggarDamage.outputAudioMixerGroup = audioMixer;
            TakeDaggarDamage.loop = false;
        }
        TakeDaggarDamage.Play();
    }

    public void IsDead()
    {
        anim.SetBool("Death", true);
        rb.velocity = Vector2.zero;
    }

    public void DeathSound()
    {
        if (!EnemyDeathAudioSource)
        {
            EnemyDeathAudioSource = gameObject.AddComponent<AudioSource>();
            EnemyDeathAudioSource.clip = EnemyDeathSFX;
            EnemyDeathAudioSource.outputAudioMixerGroup = audioMixer;
            EnemyDeathAudioSource.loop = false;
        }
        EnemyDeathAudioSource.Play();
    }

    public void IsSquished()
    {
        anim.SetBool("Squish", true);
        Health = 0;
        rb.velocity = Vector2.zero;
    }

    public void FinishedDeath()
    {
        Destroy(gameObject.transform.parent.gameObject);
        Destroy(this.gameObject);
    }
}
