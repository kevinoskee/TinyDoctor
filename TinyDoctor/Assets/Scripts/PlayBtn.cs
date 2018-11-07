using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayBtn : MonoBehaviour, IPointerClickHandler
{
    public GameObject PauseUI;
    public AudioSource source;

    public void OnPointerClick(PointerEventData eventData)
    {
        PauseUI.SetActive(false);
        PauseBtn.isPause = false;
        Time.timeScale = 1f;
        source.Play();
    }
}
