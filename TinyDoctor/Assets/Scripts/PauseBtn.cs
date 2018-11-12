using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class PauseBtn : MonoBehaviour, IPointerClickHandler
{
    public GameObject PauseUI;
    public AudioSource source;

  //  public static bool isPause = false;
    public void OnPointerClick(PointerEventData eventData)
    {
        PauseUI.SetActive(true);
     //   isPause = true;
        Time.timeScale = 0f;
        source.Pause();

    }
}
