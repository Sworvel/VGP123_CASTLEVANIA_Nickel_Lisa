using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    Rigidbody2D rb;
    Animator anim;
    SpriteRenderer Simon;

    public bool isAttacking;
    private float timeBtwAttack;
    public float startTimeBtwAttack;
    public float attackBoxX;
    public float attackBoxY;

    public Transform attackPos;
    public float attackRange;
    public LayerMask whatIsEnemies;
    public int damage;



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
        if(timeBtwAttack <= 0)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                isAttacking = true;
                anim.SetBool("isAttacking", isAttacking);
                timeBtwAttack = startTimeBtwAttack;
                Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemies);
                for (int i = 0; i < enemiesToDamage.Length; i++)
                {
                    
                }
            }
        }
        else
        {
            isAttacking = false;
            anim.SetBool("isAttacking", isAttacking);
            timeBtwAttack -= Time.deltaTime;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(attackPos.position, new Vector2 (attackBoxX, attackBoxY));
    }

}