using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;
using System.IO;
using System;

public class LoadScene : MonoBehaviour
{
    public GameObject LoadScreen;
    public Slider Loading;
    string path;
    string jsonString;
    Doctor doctor = new Doctor();

    private void Start ()
    {
        path = Path.Combine(Application.persistentDataPath, "Doctor.json");
        if (!File.Exists(path))
        {
            CreateFile();
            StartCoroutine(LoadAsync("Quiz"));
        }
        else
            StartCoroutine(LoadAsync("Game Menu"));
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
