using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;

public class ChaptersControl : MonoBehaviour
{

    string path;
    string jsonString;
    Doctor doctor = new Doctor();
    public Image[] chapters;
    Color unlocked = Color.white;
    Color locked = Color.gray;

    private void Start()
    {
        LoadData();
        InitData();
    }

    void LoadData()
    {
        path = Path.Combine(Application.persistentDataPath, "Doctor.json");
        jsonString = File.ReadAllText(path);
        doctor = JsonUtility.FromJson<Doctor>(jsonString);
    }
  
    void InitData()
    {
        for (int i = 1; i < doctor.Cards.Length; i++)
        {
            if (doctor.Cards[i-1])
                chapters[i].color = unlocked;
            else
                chapters[i].color = locked;
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