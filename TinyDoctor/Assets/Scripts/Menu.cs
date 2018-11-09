using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;
using System.IO;
using System;

public class Menu : MonoBehaviour
{
    public GameObject About;
    public GameObject Help;
    public GameObject Settings;
    public GameObject Almanac;
    public GameObject CardInfo;
    public GameObject Shop;

    public GameObject LoadScreen;
    public Slider Loading;
    public HelpControl helpControl;

    string path;

    Doctor doctor = new Doctor();

    public void StartGame()
    {
        path = Path.Combine(Application.persistentDataPath, "Doctor.json");
        if (!File.Exists(path))
        {
            CreateFile();
            StartCoroutine(LoadAsync("Quiz"));
        }
        else
            StartCoroutine(LoadAsync("Comic"));
    }

    void CreateFile()
    {
        doctor.Cards = new bool[] { false, false, false };
        doctor.Pretest = -1;
        doctor.Posttest = -1;
        doctor.GainScore = -1;
        string newDoctor = JsonUtility.ToJson(doctor, true);
        File.WriteAllText(path, newDoctor);
    }

    public void OnAbout()
    {
        About.SetActive(true);
    }

    public void OnSettings()
    {
        Settings.SetActive(true);
    }

    public void OnHelp()
    {
        Help.SetActive(true);
        helpControl.navigate = 0;
       
    }

    public void OnAlmanac()
    {
        Almanac.SetActive(true);
    }

    public void OnShop()
    {
        Shop.SetActive(true);
    }

    public void OnClose()
    {
        About.SetActive(false);
        Help.SetActive(false);
        Settings.SetActive(false);
        Almanac.SetActive(false);
        CardInfo.SetActive(false);
        Shop.SetActive(false);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    IEnumerator LoadAsync(string scene)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(scene);
        LoadScreen.SetActive(true);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            Loading.value = progress;
            yield return null;
        }
    }

    [Serializable]
    public class Doctor
    {
        public int Coins;
        public bool[] Cards;
        public int Pretest;
        public int Posttest;
        public int GainScore;
    }
}