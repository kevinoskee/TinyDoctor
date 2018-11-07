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
    Doctor doctor;
    public Image[] cards;
    Color unlocked = Color.white;
    Color locked = Color.gray;

    private void Start()
    {
        path = Application.streamingAssetsPath + "/Doctor.json";
        jsonString = File.ReadAllText(path);
        doctor = JsonUtility.FromJson<Doctor>(jsonString);
        CheckCards();
        
    }

    void CheckCards()
    {
        for(int i = 0; i < doctor.Cards.Length; i++)
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
    }
}