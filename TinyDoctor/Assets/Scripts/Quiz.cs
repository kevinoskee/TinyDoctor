using System;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;
using System.Collections;

public class Quiz : MonoBehaviour
{

    int counter = 0;

    public Text Question;
    public Text OptA;
    public Text OptB;
    public Text OptC;
    public Text OptD;
    public Text Number;

    public GameObject LoadScreen;
    public Slider Loading;

    public GameObject GainScoreUI;
    public Text GainScore;

    int point;

    string path;
    string jsonString;

    Doctor doctor = new Doctor();
    Test[] test;

    private void Start()
    {
        path = Path.Combine(Application.persistentDataPath, "Doctor.json");
        jsonString = File.ReadAllText(path);
        doctor = JsonUtility.FromJson<Doctor>(jsonString);

        GetQuestion();
    }

    public void GetQuestion()
    {
        TextAsset jsonTextFile = Resources.Load<TextAsset>("Test") as TextAsset;
        test = JsonHelper.GetJsonArray<Test>(jsonTextFile.ToString());
        Number.text = (counter + 1).ToString() + " / " + test.Length.ToString();
        Question.text = test[counter].Question;
        OptA.text = test[counter].Choices[0];
        OptB.text = test[counter].Choices[1];
        OptC.text = test[counter].Choices[2];
        OptD.text = test[counter].Choices[3];

    }

    public void CheckAnswer(int opt)
    {

        if (test[counter].Answer == opt)
            point++;

        counter++;
        if (counter == test.Length)
            WriteScore();
        else
            GetQuestion();

    }

    void WriteScore()
    {
        if (doctor.Pretest < 0)
        {
            doctor.Pretest = point;
            string newScore = JsonUtility.ToJson(doctor, true);
            File.WriteAllText(path, newScore);
            ComicControl.chapter = 0;
            StartCoroutine(LoadAsync("Game Menu"));
        }
        else
        {
            doctor.Posttest = point;
            string newScore = JsonUtility.ToJson(doctor, true);
            File.WriteAllText(path, newScore);
            doctor.GainScore = doctor.Posttest - doctor.Pretest;
            GainScoreUI.SetActive(true);
            GainScore.text = doctor.GainScore.ToString();
         
        }
    }

    [Serializable]
    public struct Test
    {
        public string Question;
        public string[] Choices;
        public int Answer;
    }

    public class JsonHelper
    {
        public static T[] GetJsonArray<T>(string json)
        {
            string newJson = "{ \"array\": " + json + "}";
            Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(newJson);
            return wrapper.array;
        }

        [Serializable]
        private class Wrapper<T>
        {
            public T[] array;
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
