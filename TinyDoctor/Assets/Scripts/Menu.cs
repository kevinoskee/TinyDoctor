using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public GameObject About;
    public GameObject Help;
    public GameObject Settings;
    public GameObject Almanac;
    public GameObject CardInfo;

    public GameObject LoadScreen;
    public Slider Loading;
    public HelpControl helpControl;


    public void StartGame()
    {
        StartCoroutine(LoadAsync("Comic"));
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

    public void OnClose()
    {
        About.SetActive(false);
        Help.SetActive(false);
        Settings.SetActive(false);
        Almanac.SetActive(false);
        CardInfo.SetActive(false);
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
}