using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RestartBtn : MonoBehaviour, IPointerClickHandler
{
    public GameObject GameOverUI;
    public AudioSource source;
    public GameObject LoadScreen;
    public Slider Loading;

    public void OnPointerClick(PointerEventData eventData)
    {
        PauseBtn.isPause = false;
        Time.timeScale = 1f;
        StartCoroutine(LoadAsync("Main Game"));
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
