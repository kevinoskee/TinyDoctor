using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;

public class CardInfoControl : MonoBehaviour {

    public static Sprite CardImage;
    public static string JSONFile;
    public Image Card;
  //  public Image Info;
    public Text Info;

    string path;
    string jsonString;
    CardInfo cardInfo;

    private void Start()
    {
        path = Application.streamingAssetsPath + JSONFile;
        jsonString = File.ReadAllText(path);
        cardInfo = JsonUtility.FromJson<CardInfo>(jsonString);

        Card.sprite = CardImage;
        Info.text = cardInfo.Description;
      //  Info.sprite = CardImage;
    }

    public void OnClose()
    {
        gameObject.SetActive(false);
        Info.text = cardInfo.Description;
    }

    public void OnDescription()
    {
        Info.text = cardInfo.Description;
    }

    public void OnSymptoms()
    {
        Info.text = cardInfo.Symptoms;
    }

    public void OnMedications()
    {
        Info.text = cardInfo.Medications;
    }

    public void OnPreventions()
    {
        Info.text = cardInfo.Preventions;
    }

    [Serializable]
    public class CardInfo
    {
        public string Description;
        public string Symptoms;
        public string Medications;
        public string Preventions;
    }
}
