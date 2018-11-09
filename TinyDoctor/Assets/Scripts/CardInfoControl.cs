using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;

public class CardInfoControl : MonoBehaviour {

    public static Sprite CardImage;
    public static int cardIndex;
    public Image Card;

    public Text Info;

    string path;
    string jsonString;

    Cards[] cards;

    private void Start()
    {

        TextAsset jsonTextFile = Resources.Load<TextAsset>("Cards") as TextAsset;
        cards = JsonHelper.GetJsonArray<Cards>(jsonTextFile.ToString());

        Card.sprite = CardImage;
        Info.text = cards[cardIndex].Description;
    }

    public void OnClose()
    {
        gameObject.SetActive(false);
        Info.text = cards[cardIndex].Description;
    }

    public void OnDescription()
    {
           Info.text = cards[cardIndex].Description;
    }

    public void OnSymptoms()
    {
           Info.text = cards[cardIndex].Symptoms;
    }

    public void OnMedications()
    {
          Info.text = cards[cardIndex].Medications;
    }

    public void OnPreventions()
    {
          Info.text = cards[cardIndex].Preventions;
    }

    [Serializable]
    public struct Cards
    {
            public string Description;
            public string Symptoms;
            public string Medications;
            public string Preventions;
        
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
}
