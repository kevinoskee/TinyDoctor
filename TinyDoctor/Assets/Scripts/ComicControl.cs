using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class ComicControl : MonoBehaviour
{

    private Camera Camera;
    public Button prevBtn;
    public Button nextBtn;
    public Text nextBtnText;
    public GameObject LoadScreen;
    public Slider Loading;

    public Sprite[] comics;

    public SpriteRenderer comicImage;
    public SpriteRenderer comicBG;



    public static int chapter = 0;
    private int navigate = 0;

    private void Start()
    {
        comicImage.sprite = comics[chapter];
        comicBG.sprite = comics[chapter];
        Camera = GetComponent<Camera>();
        Camera.transform.position = PositionList[chapter].pos[navigate];
        prevBtn.enabled = false;
    }

    private void Update()
    {
        Camera.transform.position = Vector3.Lerp(Camera.transform.position, PositionList[chapter].pos[navigate+1], Time.deltaTime * 5f);
        if (navigate == 0)
            prevBtn.enabled = false;
        else
            prevBtn.enabled = true;
        if (navigate + 2 == PositionList[chapter].pos.Length)
            nextBtnText.text = "Start Game";
        else
            nextBtnText.text = "Next";
    }

    public void OnNext()
    {
        if (nextBtnText.text == "Next")
        {
            navigate++;
        }
        else
        {
            switch (chapter)
            {
                case 0:
                    StartCoroutine(LoadAsync("Chapter 1"));
                    break;
                case 1:
                    StartCoroutine(LoadAsync("Chapter 2"));
                    break;
                case 2:
                    StartCoroutine(LoadAsync("Chapter 3"));
                    break;
            }
        }
            

    }

    public void OnPrev()
    {
        navigate--;
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
    public class Vector3List
    {
        public Vector3[] pos;
    }
    public Vector3List[] PositionList;
}
