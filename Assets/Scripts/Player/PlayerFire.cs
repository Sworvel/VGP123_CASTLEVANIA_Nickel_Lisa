using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerFire : MonoBehaviour
{
    SpriteRenderer Simon;
    Animator anim;
    SpriteRenderer Dagger;
    AudioSource PlayerFireAudioSource;

    public AudioClip PlayerFireSFX;
    public AudioMixerGroup audioMixer;

    public Transform spawnPointLeft;
    public Transform spawnPointRight;

    public float projectileSpeed;
    public Projectile projectilePrefab;

    // Start is called before the first frame update
    void Start()
    {
        Simon = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        Dagger = GetComponent<SpriteRenderer>();

        if (projectileSpeed <= 0)
            projectileSpeed = 7.0f;

        if (!spawnPointLeft || !spawnPointRight || !projectilePrefab)
            Debug.Log("Unity Inspector Values Not Set");
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetButtonDown("Fire2"))
        {
            if (Time.timeScale == 1)
            {
                anim.SetBool("isShooting", true);
            }
        }
        else
        {
            anim.SetBool("isShooting", false);
        }
    }

    public void FireProjectile()
    {
        if (Simon.flipX)
        {
            Projectile projectileInstance = Instantiate(projectilePrefab, spawnPointLeft.position, spawnPointLeft.rotation);
            projectileInstance.speed = -projectileSpeed;
            Dagger.flipX = true;
        }
        else
        {
            Projectile projectileInstance = Instantiate(projectilePrefab, spawnPointRight.position, spawnPointRight.rotation);
            projectileInstance.speed = projectileSpeed;
        }
        if (!PlayerFireAudioSource)
        {
            PlayerFireAudioSource = gameObject.AddComponent<AudioSource>();
            PlayerFireAudioSource.clip = PlayerFireSFX;
            PlayerFireAudioSource.outputAudioMixerGroup = audioMixer;
            PlayerFireAudioSource.loop = false;
        }
        PlayerFireAudioSource.Play();
    }

    //void ResetFire()
    //{
    //    anim.SetBool("isShooting", false);
    //}
}