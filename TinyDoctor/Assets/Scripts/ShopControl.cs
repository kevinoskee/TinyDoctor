using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;

public class ShopControl : MonoBehaviour
{
    public Text coins;
    string path;
    string jsonString;
    Doctor doctor = new Doctor();

    private void Start()
    {
        GetCoin();
    }

    public void GetCoin()
    {
        path = Path.Combine(Application.persistentDataPath, "Doctor.json");
        jsonString = File.ReadAllText(path);
        doctor = JsonUtility.FromJson<Doctor>(jsonString);
        coins.text = doctor.Coins.ToString();
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
