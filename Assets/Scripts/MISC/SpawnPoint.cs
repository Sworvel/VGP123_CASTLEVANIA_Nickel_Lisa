using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public GameObject[] spawnnedObject;

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(spawnnedObject[Random.Range(0, 4)], transform.position, transform.rotation); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
