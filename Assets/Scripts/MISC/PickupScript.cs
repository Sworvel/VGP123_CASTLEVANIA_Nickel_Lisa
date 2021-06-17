using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PickupScript : MonoBehaviour
{
    public enum CollectibleType
    {
        POWERUP,
        COLLECTIBLE,
        Health,

    }

    public CollectibleType currentCollectible;
    public AudioClip GetHealthSFX;
    public AudioClip GetPowerUpSFX;
    public AudioClip GetScoreSFX;
    public AudioMixerGroup audioMixer;

    AudioSource GetHealthAudioSource;
    AudioSource GetPowerUpAudioSource;
    AudioSource GetScore;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            switch (currentCollectible)
            {
                case CollectibleType.COLLECTIBLE:
                    GameManager.instance.score++;
                    if (!GetScore)
                    {
                        GetScore = gameObject.AddComponent<AudioSource>();
                        GetScore.clip = GetScoreSFX;
                        GetScore.outputAudioMixerGroup = audioMixer;
                        GetScore.loop = false;
                    }
                    GetScore.Play();
                    break;

                case CollectibleType.POWERUP:
                    collision.gameObject.GetComponent<PlayerMovement>().StartSpeedChange();
                    if (!GetPowerUpAudioSource)
                    {
                        GetPowerUpAudioSource = gameObject.AddComponent<AudioSource>();
                        GetPowerUpAudioSource.clip = GetPowerUpSFX;
                        GetPowerUpAudioSource.outputAudioMixerGroup = audioMixer;
                        GetPowerUpAudioSource.loop = false;
                    }
                    GetPowerUpAudioSource.Play();
                    break;

                case CollectibleType.Health:
                    GameManager.instance.Health++;
                    if (!GetHealthAudioSource)
                    {
                        GetHealthAudioSource = gameObject.AddComponent<AudioSource>();
                        GetHealthAudioSource.clip = GetHealthSFX;
                        GetHealthAudioSource.outputAudioMixerGroup = audioMixer;
                        GetHealthAudioSource.loop = false;
                    }
                    GetHealthAudioSource.Play();
                    break;
            }
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            gameObject.GetComponent<Collider2D>().enabled = false;
            Destroy(gameObject, 3f);
        }
    }
}
