using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;

public class ItemInfoControl : MonoBehaviour {

    public static Sprite ItemImage;
    public static int itemIndex;
    public ShopControl shopControl;
    public Text price;
    public Text info;
    public Image item;
    public GameObject Alert;
    Items[] items;

    string path;
    string jsonString;
    Doctor doctor = new Doctor();

    private void Start()
    {
        GetInfo();
    }

    public void GetInfo()
    {
        TextAsset jsonTextFile = Resources.Load<TextAsset>("Items") as TextAsset;
        items = JsonHelper.GetJsonArray<Items>(jsonTextFile.ToString());
        item.sprite = ItemImage;
        price.text = items[itemIndex].Price.ToString();
        info.text = items[itemIndex].Info;
        path = Path.Combine(Application.persistentDataPath, "Doctor.json");
        jsonString = File.ReadAllText(path);
        doctor = JsonUtility.FromJson<Doctor>(jsonString);
    }

    public void OnBuy()
    {
        if (items[itemIndex].Price <= doctor.Coins)
        {
            doctor.Coins -= items[itemIndex].Price;
            string newDoctor = JsonUtility.ToJson(doctor, true);
            File.WriteAllText(path, newDoctor);
            shopControl.GetCoin();
        }
        else
        {
            AlertUI.alert = "Not Enough Coins";
            Instantiate(Alert);
        }
    }

    public void OnCancel()
    {
        gameObject.SetActive(false);
    }

    [Serializable]
    public struct Items
    {
        public string Info;
        public int Price;
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
}
