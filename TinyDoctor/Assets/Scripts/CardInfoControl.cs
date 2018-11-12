using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;
using System.Collections;

public class CardInfoControl : MonoBehaviour {

    public static Sprite CardImage;
    public static string cardName;

    public Image Card;
    public ScrollRect scrollRect;

    public Text Info;

    string path;
    string jsonString;
    string section;


    private void Start()
    {
        Card.sprite = CardImage;
        scrollRect.normalizedPosition = new Vector2(0, 1);
        section = "Description";
        GetFromTxt();
    }

    public void OnClose()
    {
        gameObject.SetActive(false);
        scrollRect.normalizedPosition = new Vector2(0, 1);
        section = "Description";
        GetFromTxt();
    }

    public void OnDescription()
    {
        scrollRect.normalizedPosition = new Vector2(0, 1);
        section = "Description";
        GetFromTxt();
    }

    public void OnSymptoms()
    {
        scrollRect.normalizedPosition = new Vector2(0, 1);
        section = "Symptoms";
        GetFromTxt();
    }

    public void OnMedications()
    {
        scrollRect.normalizedPosition = new Vector2(0, 1);
        section = "Medications";
        GetFromTxt();
    }

    public void OnPreventions()
    {
        scrollRect.normalizedPosition = new Vector2(0, 1);
        section = "Preventions";
        GetFromTxt();
    }

    public void GetFromTxt()
    {
        string newPath = "Assets/Resources/Card.txt";
        string[] lines = File.ReadAllLines(newPath);
        string data = "";
        int start = Array.FindIndex(lines, row => row.Contains(">" + cardName + " " + section)) + 1;
        int end = Array.FindIndex(lines, row => row.Contains("<" + cardName + " " + section)) - 1;
       for(int ctr = start; ctr <= end; ctr++)
        {
            if (lines[ctr].StartsWith("\\n"))
            {
                data += ("\n");
            }
            else
                data += lines[ctr];
        }
        Info.text = data;
    }
}
