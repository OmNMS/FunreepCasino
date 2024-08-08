using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FunTarget_UiAnimation : MonoBehaviour
{
    public Sprite[] BgFrames;
    public Image BgImg;
    public Sprite[] TimerFrames;
    public Image TimerImg;
    public Sprite[] ScoreFrames;
    public Image ScoreImg;
    public Sprite[] WinnerFrames;
    public Image WinnerImg;
    public Sprite[] PreviousWin_Frames;
    public Image PreviousWin_Img;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Start_BgAnimation());
    }

    IEnumerator Start_BgAnimation()
    {
        BgImg.gameObject.SetActive(true);
        foreach (var item in BgFrames)
        {
            BgImg.sprite = item;
            yield return new WaitForSeconds(0.08f);
        }
        StartCoroutine(Start_BgAnimation());
    }

    IEnumerator Start_TimerAnimation()
    {
        TimerImg.gameObject.SetActive(true);
        foreach (var item in TimerFrames)
        {
            TimerImg.sprite = item;
            yield return new WaitForSeconds(0.08f);
        }
        StartCoroutine(Start_TimerAnimation());
    }

    IEnumerator Start_ScoreAnimation()
    {
        ScoreImg.gameObject.SetActive(true);
        foreach (var item in ScoreFrames)
        {
            ScoreImg.sprite = item;
            yield return new WaitForSeconds(0.08f);
        }
        StartCoroutine(Start_ScoreAnimation());
    }

    IEnumerator Start_WinnerAnimation()
    {
        WinnerImg.gameObject.SetActive(true);
        foreach (var item in WinnerFrames)
        {
            WinnerImg.sprite = item;
            yield return new WaitForSeconds(0.08f);
        }
        StartCoroutine(Start_WinnerAnimation());
    }

    IEnumerator Start_PreviousWin_Animation()
    {
        PreviousWin_Img.gameObject.SetActive(true);
        foreach (var item in PreviousWin_Frames)
        {
            PreviousWin_Img.sprite = item;
            yield return new WaitForSeconds(0.08f);
        }
        StartCoroutine(Start_PreviousWin_Animation());
    }

}
