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
    public GameObject ItemInfo;
    public GameObject Posttest;

    public GameObject LoadScreen;
    public Slider Loading;
    public HelpControl helpControl;

    public GameObject Alert;

    string path;
    string jsonString;

    Doctor doctor = new Doctor();

    private void Start()
    {
        path = Path.Combine(Application.persistentDataPath, "Doctor.json");
        if (File.Exists(path))
        {
            jsonString = File.ReadAllText(path);
            doctor = JsonUtility.FromJson<Doctor>(jsonString);
            if (doctor.Cards[2] && doctor.Posttest < 0)
            {

                AlertUI.alert = "You can now take post test";
                Instantiate(Alert);
                Posttest.SetActive(true);

            }
        }
    }

    public void StartGame()
    {
            StartCoroutine(LoadAsync("Chapter Selection"));
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

    public void OnPosttest()
    {
        StartCoroutine(LoadAsync("Quiz"));
    }

    public void OnClose()
    {
        About.SetActive(false);
        Help.SetActive(false);
        Settings.SetActive(false);
        Almanac.SetActive(false);
        CardInfo.SetActive(false);
        Shop.SetActive(false);
        ItemInfo.SetActive(false);
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