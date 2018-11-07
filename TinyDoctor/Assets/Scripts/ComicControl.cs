using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ComicControl : MonoBehaviour
{


    private SpriteRenderer comicRender;
    public Sprite[] comics;
    public Button prevBtn;
    public Button nextBtn;
    public Text nextBtnText;


    private int navigate = 0;

    private void Start()
    {
        comicRender = GetComponent<SpriteRenderer>();
        comicRender.sprite = comics[navigate];
        prevBtn.enabled = false;
    }

    private void OnNavigate()
    {
        if(navigate == 0)
            prevBtn.enabled = false;
        else
            prevBtn.enabled = true;
        if(navigate+1 == comics.Length)
            nextBtnText.text = "Start Game";
        else
            nextBtnText.text = "Next";


        comicRender.sprite = comics[navigate];
    }
   
    public void OnNext()
    {
        if (nextBtnText.text == "Next")
        {
            navigate++;
            OnNavigate();
        }
        else
            SceneManager.LoadScene("Main Game");

    }

    public void OnPrev()
    {
        navigate--;
        OnNavigate();
    }

}
