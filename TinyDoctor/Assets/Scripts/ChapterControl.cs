using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class ChapterControl : MonoBehaviour, IPointerClickHandler
{
    Color unlocked = Color.white;
    public GameObject Alert;
    public GameObject LoadScreen;
    public Slider Loading;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (gameObject.GetComponent<Image>().color == unlocked)
        {
            switch (gameObject.name)
            {
                case "Chapter1":
                    ComicControl.chapter = 0;
                    StartCoroutine(LoadAsync("Comic"));
                    break;
                case "Chapter2":
                    ComicControl.chapter = 1;
                    StartCoroutine(LoadAsync("Comic"));
                    break;
                case "Chapter3":
                    ComicControl.chapter = 2;
                    StartCoroutine(LoadAsync("Comic"));
                    break;

            }
        }
        else
        {
            AlertUI.alert = "Finish previous chapters first";
            Instantiate(Alert);
        }

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
