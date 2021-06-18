using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    static GameManager _instance = null;

    private float fixedDeltaTime = 1.0f;

    public static GameManager instance
    {
        get { return _instance; }
        set { _instance = value; }
    }

    public int maxHealth = 4;

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

    int _Health = 4;

    public int Health
    {
        get { return _Health; }
        set
        {
            _Health = value;

            if (_Health > maxHealth)
            {
                _Health = maxHealth;
            }
            else if (_Health <= 0)
            {
                PlayerDeath();
            }
            Debug.Log("Current Health Is: " + _Health);
        }
    }

    public GameObject playerInstance;
    public GameObject playerPrefab;
    public LevelManager currentLevel;

    // Start is called before the first frame update
    void Start()
    {
        this.fixedDeltaTime = Time.fixedDeltaTime;

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
            if (SceneManager.GetActiveScene().name == "TitleScene")
            {
                SceneManager.LoadScene("TitleScene2");
            }
            else if (SceneManager.GetActiveScene().name == "TitleScene2")
            {
                SceneManager.LoadScene("TitleScene3");
            }
            else if (SceneManager.GetActiveScene().name == "GameOverScene")
            {
                SceneManager.LoadScene("TitleScene");
            }
        }
        this.fixedDeltaTime = Time.fixedDeltaTime;
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

    public void StartGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("TitleScene");
    }
}
