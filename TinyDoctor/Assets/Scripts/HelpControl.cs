using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HelpControl : MonoBehaviour
{


    private Image help;
    public Sprite[] helps;
    public Button prevBtn;
    public Button nextBtn;

    [HideInInspector]
    public int navigate = 0;

    private void OnEnable()
    {
        help = GetComponent<Image>();
        help.sprite = helps[navigate];
        prevBtn.interactable = false;
    }

    private void OnNavigate()
    {
        if (navigate == 0)
            prevBtn.interactable = false;
        else
            prevBtn.interactable = true;
        if (navigate + 1 == helps.Length)
            nextBtn.interactable = false;
        else
            nextBtn.interactable = true;


        help.sprite = helps[navigate];
    }

    public void OnNext()
    {
         navigate++;
         OnNavigate();
    }

    public void OnPrev()
    {
        navigate--;
        OnNavigate();
    }

}
