using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

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

    [Header("Audio")]
    public AudioClip PauseMenuMusic;
    public AudioClip LevelMusic;
    public AudioMixerGroup effectsAudioMixer;
    public AudioMixerGroup musicAudioMixer;

    AudioSource PauseMenuAudioSource;
    AudioSource LevelMusicAudioSource;

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
                PauseMenu.SetActive(!PauseMenu.activeSelf);
                if (!PauseMenuAudioSource)
                {
                    PauseMenuAudioSource = gameObject.AddComponent<AudioSource>();
                    PauseMenuAudioSource.clip = PauseMenuMusic;
                    PauseMenuAudioSource.outputAudioMixerGroup = effectsAudioMixer;
                    PauseMenuAudioSource.loop = false;
                }
                if (PauseMenu.activeSelf)
                {
                    PauseMenuAudioSource.Play();
                    Time.timeScale = 0;
                }
                else
                {
                    if (!LevelMusicAudioSource)
                    {
                        LevelMusicAudioSource = gameObject.AddComponent<AudioSource>();
                        LevelMusicAudioSource.clip = LevelMusic;
                        LevelMusicAudioSource.outputAudioMixerGroup = musicAudioMixer;
                        LevelMusicAudioSource.loop = true;
                    }
                    Time.timeScale = 1;
                    LevelMusicAudioSource.Play();
                }
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
        Destroy(PauseMenuAudioSource);
        PauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    void ReturnToMenu()
    {
        PauseMenu.SetActive(true);
        SettingsMenu.SetActive(false);
    }

    public void PlayLevelMusic()
    {

    }
}
