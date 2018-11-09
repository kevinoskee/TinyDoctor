using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CamControl : MonoBehaviour
{

    private Camera Camera;
    public Vector3[] pos;
    public Button prevBtn;
    public Button nextBtn;
    public Text nextBtnText;
    public GameObject LoadScreen;
    public Slider Loading;

    private int navigate = 0;

    private void Start()
    {
        Camera = GetComponent<Camera>();
        Camera.transform.position = pos[navigate];
        prevBtn.enabled = false;
    }

    private void Update()
    {
        Camera.transform.position = Vector3.Lerp(Camera.transform.position, pos[navigate+1], Time.deltaTime * 5f);
        if (navigate == 0)
            prevBtn.enabled = false;
        else
            prevBtn.enabled = true;
        if (navigate + 2 == pos.Length)
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
            StartCoroutine(LoadAsync("Main Game"));

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

}
