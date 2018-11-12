using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

public class Card : MonoBehaviour {

    public GameObject LoadScreen;
    public Slider Loading;
    public GameObject Panel;
    float seconds;

    private void Start()
    {
        seconds = gameObject.GetComponent<Animation>().GetClip("CardAnim").length + 1;
        StartCoroutine(LoadAsync("Game Menu"));
    }

    IEnumerator LoadAsync(string scene)
    {
        yield return new WaitForSeconds(seconds);
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
