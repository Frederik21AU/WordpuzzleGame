using System;
using UnityEditor;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    [SerializeField] Slider volumeslider;

    void Start()
    {
        if(!PlayerPrefs.HasKey("musicVolume"))
        {
            PlayerPrefs.SetFloat("musicVolume", 1);
            load();
        }
    else
        {
            load();
        }
    }
    public void ChangeVolume()
    {
        AudioListener.volume = volumeslider.value;
        save();
    }
    void Update()
{
    volumeslider.SetValueWithoutNotify(AudioListener.volume);
}

    private void load()
    {
        volumeslider.value = PlayerPrefs.GetFloat("musicVolume");
    }

    private void save()
    {
        PlayerPrefs.SetFloat("musicVolume", volumeslider.value);
    }
}