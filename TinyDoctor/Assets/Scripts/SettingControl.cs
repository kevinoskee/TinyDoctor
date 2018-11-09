using System;
using System.IO;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingControl : MonoBehaviour {

    public AudioMixer AudioMixer;
    public Toggle MusicToggle;
    public Slider MusicSlider;
    public Toggle SoundToggle;
    public Slider SoundSlider;
    public Dropdown GraphicsQuality;
    public GameObject SettingsUI;

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
        MusicToggle.isOn = settings.Music;
        SoundToggle.isOn = settings.Sound;
        MusicSlider.value = settings.MusicVol;
        SoundSlider.value = settings.SoundVol;
        GraphicsQuality.value = settings.Graphics;

        if (!settings.Music)
        {
         
            MusicSlider.interactable = false;
            SetMusicVolume(-80f);
        }
        else
        {
            MusicSlider.interactable = true;
            SetMusicVolume(settings.MusicVol);
        }
        if (!settings.Sound)
        {
            SoundSlider.interactable = false;
            SetSoundVolume(-80f);
        }
        else
        {
            SoundSlider.interactable = true;
            SetSoundVolume(settings.SoundVol);
        }

        SetQuality(settings.Graphics);

    }

    public void SetMusicVolume (float musicVol)
    {
        AudioMixer.SetFloat("MusicVol", musicVol);
    }
    public void SetSoundVolume(float soundVol)
    {
        AudioMixer.SetFloat("SoundVol", soundVol);
    }

    public void OnMusicToggle(bool status)
    {
        if (!status)
        {
            MusicSlider.interactable = false;
            SetMusicVolume(-80f);
        }
        else
        {
            MusicSlider.interactable = true;
            SetMusicVolume(MusicSlider.value);
        }
    }
    public void OnSoundToggle(bool status)
    {
        if (!status)
        {
            SoundSlider.interactable = false;
            SetSoundVolume(-80f);
        }
        else
        {
            SoundSlider.interactable = true;
            SetSoundVolume(SoundSlider.value);
        }
    }

    public void SetQuality(int quality)
    {
        QualitySettings.SetQualityLevel(quality);
    }

    public void OnClose()
    {
        SettingsUI.SetActive(false);
        UpdateSettings();
    }

    void UpdateSettings()
    {
        path = Path.Combine(Application.persistentDataPath, "Settings.json");
        settings.Graphics = QualitySettings.GetQualityLevel();
        settings.Music = MusicToggle.isOn;
        settings.Sound = SoundToggle.isOn;
        settings.MusicVol = MusicSlider.value;
        settings.SoundVol = SoundSlider.value;


        string newSettings = JsonUtility.ToJson(settings,true);
        File.WriteAllText(path, newSettings);
    }

    [Serializable]
    public class Settings
    {
        public int Graphics;
        public bool Music;
        public bool Sound;
        public float MusicVol;
        public float SoundVol;
    }

}
