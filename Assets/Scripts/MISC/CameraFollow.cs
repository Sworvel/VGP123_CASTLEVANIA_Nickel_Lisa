using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject Player;
    public float minXClamp = -200f;
    public float maxXClamp = 200f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Player)
        {
            Vector3 cameraTransform;

            cameraTransform = transform.position;

            cameraTransform.x = Player.transform.position.x - 0.5f;
            cameraTransform.x = Mathf.Clamp(cameraTransform.x, minXClamp, maxXClamp);
            cameraTransform.y = Player.transform.position.y;
            transform.position = cameraTransform;
        }
    }
}
