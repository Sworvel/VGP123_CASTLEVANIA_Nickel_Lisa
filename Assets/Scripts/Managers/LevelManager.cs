using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public int startinglives;
    public Transform spawnLocation;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.Health = startinglives;
        GameManager.instance.SpawnPlayer(spawnLocation);
        GameManager.instance.currentLevel = GetComponent<LevelManager>();
    }
}
