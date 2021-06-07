using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    [Header("Buttons")]
    public Button StartButton;
    public Button QuitButton;
    public Button SettingsButton;
    public Button ReturnButton;
    public Button ReturnToMenuButton;
    public Button ReturnToGameButton;
    public Button ReturnToMainMenuButton;

    [Header("Menus")]
    public GameObject MainMenu;
    public GameObject SettingsMenu;
    public GameObject PauseMenu;

    [Header("Text")]
    public Text HealthText;
    public Text VolText;

    [Header("Slider")]
    public Slider VolSlider;

    // Start is called before the first frame update
    void Start()
    {
        if (StartButton)
        {
            StartButton.onClick.AddListener(() => GameManager.instance.StartGame());
        }

        if (SettingsButton)
        {
            SettingsButton.onClick.AddListener(() => ShowSettingsMenu());
        }

        if (ReturnButton)
        {
            ReturnButton.onClick.AddListener(() => ShowMainMenu());
        }

        if (QuitButton)
        {
            QuitButton.onClick.AddListener(() => GameManager.instance.QuitGame());
        }

        if (ReturnToGameButton)
        {
            ReturnToGameButton.onClick.AddListener(() => ReturnToGame());
            Time.timeScale = 1;
        }

        if (ReturnToMenuButton)
        {
            ReturnToMenuButton.onClick.AddListener(() => ReturnToMenu());
        }

        if (ReturnToMainMenuButton)
        {
            ReturnToMainMenuButton.onClick.AddListener(() => GameManager.instance.ReturnToMainMenu());
        }
    }

    private void Update()
    {
        if (HealthText)
        {
            HealthText.text = GameManager.instance.Health.ToString();
        }

        if (PauseMenu)
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                PauseMenu.SetActive(true);
                GameManager.instance.IsPaused();
            }
        }
    }

    void ShowMainMenu()
    {
        SettingsMenu.SetActive(false);
        MainMenu.SetActive(true);
    }

    void ShowSettingsMenu()
    {
        SettingsMenu.SetActive(true);
        MainMenu.SetActive(false);
        PauseMenu.SetActive(false);
    }

    void ReturnToGame()
    {
        PauseMenu.SetActive(false);
        GameManager.instance.ResumeGame();
    }

    void ReturnToMenu()
    {
        PauseMenu.SetActive(true);
        SettingsMenu.SetActive(false);
    }
}
