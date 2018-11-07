using System;
using System.IO;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class GameSettings : MonoBehaviour
{

    public AudioMixer AudioMixer;

    string path;
    string jsonString;
    Settings settings;

    private void Start()
    {
        path = Application.streamingAssetsPath + "/Settings.json";
        jsonString = File.ReadAllText(path);
        settings = JsonUtility.FromJson<Settings>(jsonString);

        InitSettings();
    }

    void InitSettings()
    {

        if (!settings.Music)
        {
            SetMusicVolume(-80f);
        }
        else
        {
            SetMusicVolume((float)settings.MusicVol);
        }
        if (!settings.Sound)
        {
            SetSoundVolume(-80f);
        }
        else
        {
            SetSoundVolume((float)settings.SoundVol);
        }

        SetQuality(settings.Graphics);

    }

    public void SetMusicVolume(float musicVol)
    {
        AudioMixer.SetFloat("MusicVol", musicVol);
    }
    public void SetSoundVolume(float soundVol)
    {
        AudioMixer.SetFloat("SoundVol", soundVol);
    }

  
    public void SetQuality(int quality)
    {
        QualitySettings.SetQualityLevel(quality);
    }

    [Serializable]
    public class Settings
    {
        public int Graphics;
        public bool Music;
        public bool Sound;
        public double MusicVol;
        public double SoundVol;
    }

}
