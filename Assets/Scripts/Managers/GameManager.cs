using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    static GameManager _instance = null;

    public static GameManager instance
    {
        get { return _instance; }
        set { _instance = value; }
    }

    public int maxHealth = 3;

    int _score = 0;
    public int score
    {
        get { return _score; }
        set 
        { 
            _score = value;
            Debug.Log("Current Score Is: " + _score);
        }
    }

    int _health = 3;

    public int health
    {
        get { return _health; }
        set
        {
            if(_health > value)
            {
                //respawn code goes here
            }
            _health = value;

            if (_health > maxHealth)
            {
                _health = maxHealth;
            }
            else if (_health <= 0)
            {
                PlayerDeath();
            }
            Debug.Log("Current Health Is: " + _health);
        }
    }

    public GameObject playerInstance;
    public GameObject playerPrefab;
    public LevelManager currentLevel;

    // Start is called before the first frame update
    void Start()
    {
        if (instance)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (SceneManager.GetActiveScene().name == "SampleScene")
            {
                SceneManager.LoadScene("TitleScene");
            }
            else if (SceneManager.GetActiveScene().name == "TitleScene")
            {
                SceneManager.LoadScene("SampleScene");
            }

            if (Input.GetKeyDown(KeyCode.Backspace))
            {
                QuitGame();
            }

            if (SceneManager.GetActiveScene().name == "GameOverScene")
            {
                SceneManager.LoadScene("TitleScene");
            }
        }
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void SpawnPlayer(Transform spawnLocation)
    {
        CameraFollow mainCamera = FindObjectOfType<CameraFollow>();
        if (mainCamera)
        {
            mainCamera.Player = Instantiate(playerPrefab, spawnLocation.position, spawnLocation.rotation);
            playerInstance = mainCamera.Player;
        }
        else
        {
            SpawnPlayer(spawnLocation);
        }
    }

    public void Respawn()
    {
        playerInstance.transform.position = currentLevel.spawnLocation.position;
    }

    public void PlayerDeath()
    {
        SceneManager.LoadScene("GameOverScene");
    }
}
