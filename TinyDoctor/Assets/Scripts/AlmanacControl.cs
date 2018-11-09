using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;

public class AlmanacControl : MonoBehaviour
{

    string path;
    string jsonString;
    Doctor doctor = new Doctor();
    public Image[] cards;
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
        if (!File.Exists(path))
            LoadFromResource();
        else
            LoadFromSave();
    }

    void LoadFromSave()
    {
        jsonString = File.ReadAllText(path);
        doctor = JsonUtility.FromJson<Doctor>(jsonString);
    }

    void LoadFromResource()
    {
        TextAsset jsonTextFile = Resources.Load<TextAsset>("Doctor") as TextAsset;
        doctor = JsonUtility.FromJson<Doctor>(jsonTextFile.ToString());
    }

    void InitData()
    {
        for (int i = 0; i < doctor.Cards.Length; i++)
        {
            if (doctor.Cards[i])
                cards[i].color = unlocked;
            else
                cards[i].color = locked;
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