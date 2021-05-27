using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupScript : MonoBehaviour
{
    public enum CollectibleType
    {
        POWERUP,
        COLLECTIBLE,
        health,

    }

    public CollectibleType currentCollectible;


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
                    PlayerMovement pmScript = collision.gameObject.GetComponent<PlayerMovement>();
                    pmScript.score++;
                    Debug.Log(pmScript.score);
                    break;

                case CollectibleType.POWERUP:
                    collision.gameObject.GetComponent<PlayerMovement>().StartSpeedChange();
                    break;

                case CollectibleType.health:
                    pmScript = collision.gameObject.GetComponent<PlayerMovement>();
                    pmScript.health++;
                    Debug.Log(pmScript.health);
                    break;
            }
            Destroy(gameObject);
        }
    }
}
