using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiAnimation : MonoBehaviour
{
    public Sprite[] ScoreFrames;
    public Image ScoreImg;
    public Sprite[] WinnerFrames;
    public Image WinnerImg;
    public Sprite[] TimerFrames;
    public Image TimerImg;
    public Sprite[] DoubleTitleFrames;
    public Image DoubleTitleImg;
    public Sprite[] TripleTitleFrames;
    public Image TripleTitleImg;
    public Sprite[] SingleTitleFrames;
    public Image SingleTitleImg;
    public Sprite[] SingleTableFrames;
    public Image SingleTableImg;
    public Sprite[] ChipsFrames;
    public Image ChipsImg;
    public Sprite[] ZoomOnOffFrames;
    public Image ZoomOnOffImg;
    public Sprite[] BetsOkFrames;
    public Image BetsOkImg;
    public Sprite[] TakeFrames;
    public Image TakeImg;
    public Sprite[] WheelRotation_Frames;
    public Image WheelImg;
    public Sprite Original_WheelImg;
    public bool _playWheel_rotation, _wheelAnim;
    public Sprite[] LeftStokes_Frames;
    public Image LeftStokesImg;
    public Sprite[] RightStokes_Frames;
    public Image RightStokesImg;
    public Sprite[] OuterWheelCut_Frames;
    public Image OuterWheelCut_Img;
    public Sprite[] MediumWheelCut_Frames;
    public Image MediumWheelCut_Img;
    public Sprite[] InnerWheelCut_Frames;
    public Image InnerWheelCut_Img;

    // Start is called before the first frame update
    void Start()
    {
        _playWheel_rotation = false;
        _wheelAnim = false;
        StartCoroutine(Start_ScoreAnimation());
        StartCoroutine(Start_WinnerAnimation());
        StartCoroutine(Start_TimerAnimation());
        StartCoroutine(Start_DoubleTitleAnimation());
        StartCoroutine(Start_TripleTitleAnimation());
        StartCoroutine(Start_SingleTitleAnimation());
        StartCoroutine(Start_SingleTableAnimation());
        StartCoroutine(Start_ChipsAnimation());
        StartCoroutine(Start_ZoomOnOffAnimation());
        StartCoroutine(Start_BetsOkAnimation());
        StartCoroutine(Start_TakeAnimation());
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

    IEnumerator Start_DoubleTitleAnimation()
    {
        DoubleTitleImg.gameObject.SetActive(true);
        foreach (var item in DoubleTitleFrames)
        {
            DoubleTitleImg.sprite = item;
            yield return new WaitForSeconds(0.08f);
        }
        StartCoroutine(Start_DoubleTitleAnimation());
    }

    IEnumerator Start_TripleTitleAnimation()
    {
        TripleTitleImg.gameObject.SetActive(true);
        foreach (var item in TripleTitleFrames)
        {
            TripleTitleImg.sprite = item;
            yield return new WaitForSeconds(0.08f);
        }
        StartCoroutine(Start_TripleTitleAnimation());
    }

    IEnumerator Start_SingleTitleAnimation()
    {
        SingleTitleImg.gameObject.SetActive(true);
        foreach (var item in SingleTitleFrames)
        {
            SingleTitleImg.sprite = item;
            yield return new WaitForSeconds(0.08f);
        }
        StartCoroutine(Start_SingleTitleAnimation());
    }

    IEnumerator Start_SingleTableAnimation()
    {
        SingleTableImg.gameObject.SetActive(true);
        foreach (var item in SingleTableFrames)
        {
            SingleTableImg.sprite = item;
            yield return new WaitForSeconds(0.08f);
        }
        StartCoroutine(Start_SingleTableAnimation());
    }

    IEnumerator Start_ChipsAnimation()
    {
        ChipsImg.gameObject.SetActive(true);
        foreach (var item in ChipsFrames)
        {
            ChipsImg.sprite = item;
            yield return new WaitForSeconds(0.08f);
        }
        StartCoroutine(Start_ChipsAnimation());
    }

    IEnumerator Start_ZoomOnOffAnimation()
    {
        ZoomOnOffImg.gameObject.SetActive(true);    
        foreach (var item in ZoomOnOffFrames)
        {
            ZoomOnOffImg.sprite = item;
            yield return new WaitForSeconds(0.08f);
        }
        StartCoroutine(Start_ZoomOnOffAnimation());
    }

    IEnumerator Start_BetsOkAnimation()
    {
        BetsOkImg.gameObject.SetActive(true);
        foreach (var item in BetsOkFrames)
        {
            BetsOkImg.sprite = item;
            yield return new WaitForSeconds(0.08f);
        }
        StartCoroutine(Start_BetsOkAnimation());
    }

    IEnumerator Start_TakeAnimation()
    {
        TakeImg.gameObject.SetActive(true);
        foreach (var item in TakeFrames)
        {
            TakeImg.sprite = item;
            yield return new WaitForSeconds(0.08f);
        }
        StartCoroutine(Start_TakeAnimation());
    }

    public IEnumerator Start_WheelRotation()
    {
        if(_playWheel_rotation)
        {
            WheelImg.gameObject.SetActive(true);
            foreach (var item in WheelRotation_Frames)
            {
                WheelImg.sprite = item;
                yield return new WaitForSeconds(0.03f);
            }
            StartCoroutine(Start_WheelRotation());
        }
    }
    public void Stop_WheelRotation()
    {
        _playWheel_rotation = false;
    }

    public IEnumerator Start_LeftStrokeAnimation()
    {
        if(_wheelAnim)
        {
            LeftStokesImg.gameObject.SetActive(true);
            foreach (var item in LeftStokes_Frames)
            {
                LeftStokesImg.sprite = item;
                yield return new WaitForSeconds(0.05f);
            }
            StartCoroutine(Start_LeftStrokeAnimation());
        }
    }

    public void Stop_LeftStrokeAnimation()
    {
        StopCoroutine(Start_LeftStrokeAnimation());
        _wheelAnim = false;
        LeftStokesImg.gameObject.SetActive(false);
    }

    public IEnumerator Start_RightStrokeAnimation()
    {
        if(_wheelAnim)
        {
            RightStokesImg.gameObject.SetActive(true);
            foreach (var item in RightStokes_Frames)
            {
                RightStokesImg.sprite = item;
                yield return new WaitForSeconds(0.05f);
            }
            StartCoroutine(Start_RightStrokeAnimation());
        }
    }

    public void Stop_RightStrokeAnimation()
    {
        StopCoroutine(Start_RightStrokeAnimation());
        _wheelAnim = false;
        RightStokesImg.gameObject.SetActive(false);
    }

    public IEnumerator Start_OuterWheelAnimation()
    {
        if(_wheelAnim)
        {
            OuterWheelCut_Img.gameObject.SetActive(true);
            foreach (var item in OuterWheelCut_Frames)
            {
                OuterWheelCut_Img.sprite = item;
                yield return new WaitForSeconds(0.05f);
            }
            StartCoroutine(Start_OuterWheelAnimation());
        }
    }

    public void Stop_OuterWheelAnimation()
    {
        StopCoroutine(Start_OuterWheelAnimation());
        _wheelAnim = false;
        OuterWheelCut_Img.gameObject.SetActive(false);
    }

    public IEnumerator Start_MediumWheelAnimation()
    {
        if(_wheelAnim)
        {
            MediumWheelCut_Img.gameObject.SetActive(true);
            foreach (var item in MediumWheelCut_Frames)
            {
                MediumWheelCut_Img.sprite = item;
                yield return new WaitForSeconds(0.05f);
            }
            StartCoroutine(Start_MediumWheelAnimation());
        }
    }

    public void Stop_MediumWheelAnimation()
    {
        StopCoroutine(Start_MediumWheelAnimation());
        _wheelAnim = false;
        MediumWheelCut_Img.gameObject.SetActive(false);
    }

    public IEnumerator Start_InnerWheelAnimation()
    {
        if(_wheelAnim)
        {
            InnerWheelCut_Img.gameObject.SetActive(true);
            foreach (var item in InnerWheelCut_Frames)
            {
                InnerWheelCut_Img.sprite = item;
                yield return new WaitForSeconds(0.05f);
            }
            StartCoroutine(Start_InnerWheelAnimation());
        }
    }

    public void Stop_InnerWheelAnimation()
    {
        StopCoroutine(Start_InnerWheelAnimation());
        _wheelAnim = false;
        InnerWheelCut_Img.gameObject.SetActive(false);
    }

}
