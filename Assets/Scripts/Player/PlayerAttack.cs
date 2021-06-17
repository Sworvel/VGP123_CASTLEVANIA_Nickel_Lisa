using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PlayerAttack : MonoBehaviour
{
    Rigidbody2D rb;
    Animator anim;
    SpriteRenderer Simon;
    AudioSource WhipAudioSource;

    public bool isAttacking;
    private float timeBtwAttack;
    public float startTimeBtwAttack;
    public float attackBoxX;
    public float attackBoxY;

    public Transform attackPos;
    public float attackRange;
    public LayerMask whatIsEnemies;
    public int damage;

    public AudioClip WhipSFX;
    public AudioMixerGroup audioMixer;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        Simon = GetComponent<SpriteRenderer>();

        if (!rb)
        {
            Debug.Log("Rigidbody2D does not exist");
        }
        if (!anim)
        {
            Debug.Log("Animation does not exist");
        }
    }

    void Update()
    {
        isAttacking = false;
        anim.SetBool("isAttacking", isAttacking);
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        if (horizontalInput == 0)
        {
            if (timeBtwAttack <= 0)
            {
                if (Input.GetButtonDown("Fire1"))
                {
                    isAttacking = true;
                    anim.SetBool("isAttacking", isAttacking);
                    timeBtwAttack = startTimeBtwAttack;
                    Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemies);
                    //for (int i = 0; i < enemiesToDamage.Length; i++) 
                    if (!WhipAudioSource)
                    {
                        WhipAudioSource = gameObject.AddComponent<AudioSource>();
                        WhipAudioSource.clip = WhipSFX;
                        WhipAudioSource.outputAudioMixerGroup = audioMixer;
                        WhipAudioSource.loop = false;
                    }
                    WhipAudioSource.Play();
                }
            }
            else
            {
                timeBtwAttack -= Time.deltaTime;
            }
        }
    }

    // add another child object, and when player attacks and something on
    // enemy layer is colliding, that enemy will take damage

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(attackPos.position, new Vector2 (attackBoxX, attackBoxY));
    }

}