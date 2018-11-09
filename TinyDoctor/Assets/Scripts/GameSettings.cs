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
    Settings settings = new Settings();

    private void Start()
    {
        LoadData();
        InitData();
    }

    void LoadData()
    {
        path = Path.Combine(Application.persistentDataPath, "Settings.json");
        if (!File.Exists(path))
            LoadFromResource();
        else
            LoadFromSave();
    }

    void LoadFromSave()
    {
        jsonString = File.ReadAllText(path);
        settings = JsonUtility.FromJson<Settings>(jsonString);
    }

    void LoadFromResource()
    {
        TextAsset jsonTextFile = Resources.Load<TextAsset>("Settings") as TextAsset;
        settings = JsonUtility.FromJson<Settings>(jsonTextFile.ToString());
    }

    void InitData()
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
