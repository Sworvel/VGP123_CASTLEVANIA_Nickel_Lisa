using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeManager : MonoBehaviour
{
    public AudioMixer mixer;
    public Slider MasterVolume;
    public Slider MusicVolume;
    public Slider EffectsVolume;

    string MasterVolParamater = "MasterVolume";
    string MusicVolParamater = "MusicVolume";
    string EffectsVolParamater = "EffectsVolume";
    
    void Start()
    {
        MasterVolume.onValueChanged.AddListener(HandleMasterSliderChange);
        MusicVolume.onValueChanged.AddListener(HandleMusicSliderChange);
        EffectsVolume.onValueChanged.AddListener(HandleEffectsSliderChange);
    }

    void HandleMasterSliderChange(float value)
    {
          mixer.SetFloat(MasterVolParamater, value);
    }

    void HandleMusicSliderChange(float value)
    {
        mixer.SetFloat(MusicVolParamater, value);
    }

    void HandleEffectsSliderChange(float value)
    {
        mixer.SetFloat(EffectsVolParamater, value);
    }
}
