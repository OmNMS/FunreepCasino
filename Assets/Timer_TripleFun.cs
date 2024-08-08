using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TripleFun.ServerStuff;
using UnityEngine.UI;
using TripleChance.GamePlay;

public class Timer_TripleFun : MonoBehaviour
{
   public static float time = 30;
    int minutes;
    int seconds;
  public  Text timerTxt;
    int currentTime;
    public GameObject WaitPanel;
  //  private bool isUserPlacedBets;
  //  private bool isBetConfirmed;
    private bool canPlaceBet;
    private bool isLastGameWinAmountReceived;
   // private bool canPlacedBet;
   // private bool isthisisAFirstRound;
  //  private bool isPreviousBetPlaced;
    private bool isdataLoaded = false;
    private bool isTimeUp = false;
    bool canStopTimer;


    public  TripleFunScreen TFS;
    public TripleFunWheel TFW;
    // Start is called before the first frame update
    void Start()
    {
       // Start_Timer();
        
    }

    // Update is called once per frame
 //private   void Start_Timer()
 //   {
 //      // string.Format("{0:0}", timerTxt.ToString());
 //       canPlaceBet = true;
 //       //  repeatBtn.interactable = betOkBtn.interactable = clearBtn.interactable = doubleBtn.interactable = true;
 //       //isUserPlacedBets = false;
 //       //isBetConfirmed = false;
 //       canPlaceBet = true;
 //       canStopTimer = false;
 //      // Debug.Log("timer started");
 //       //while (time <= 10)
 //       //{
 //       // //  if (canStopTimer) yield break;
 //       //    timerTxt.text = time.ToString();
 //       //   // currentTime = time;
 //       //    if (time < 6)
 //       //    {
 //       //        //  repeatBtn.interactable = betOkBtn.interactable = clearBtn.interactable = doubleBtn.interactable = false;
 //       //        //canPlaceBet = false;
 //       //        //if (!isBetConfirmed)
 //       //        //{
 //       //        //    //   OnBetsOk();
 //       //        //}
 //       //    }
 //       //    // yield return new WaitForSeconds(1f);
 //       //    time--;
 //       //}
 //       //currentTime = 0;
 //       //timerTxt.text = 0.ToString();
 //   }

    private void Update()
    {
        if (TripleFun_ServerResponse.TimerStart)
        {
            WaitPanel.SetActive(false);

            if (!isTimeUp)
            {
                time -= Time.deltaTime;

                minutes = Mathf.FloorToInt(time / 60);
                seconds = Mathf.FloorToInt(time % 60);
                timerTxt.text = string.Format("{0:00}:{1:00}", minutes, seconds);
                // if (time <= 10)
                // {
                //     for (int i = 0; i < TFS.singleGridBtns.Length; i++)
                //     { TFS.singleGridBtns[i].interactable = false; }

                //     for (int i = 0; i < TFS.doubleGridBtns.Length; i++)
                //     { TFS.doubleGridBtns[i].interactable = false; }

                //     for (int i = 0; i < TFS.tripleGridBtns.Length; i++)
                //     { TFS.tripleGridBtns[i].interactable = false; }
                // }


                //time.ToString();
            }
            if (time < 1)
            {
                TripleFun_ServerResponse.TimerStart = false;
                isTimeUp = true;
                timerTxt.text = " Time Up";
                int x = Random.Range(0, 9);
                int y = Random.Range(0, 9);
                int z = Random.Range(0, 9);
                TFW.Spin(x, y, z);
                Reset();
            }
        }
    }
    public void Reset()
    {
        time = 30;
        StartCoroutine(NextRound());
        TripleFun_ServerResponse.TimerStart = true;
    }

    IEnumerator NextRound()
    {
        yield return new WaitForSeconds(12f);
        isTimeUp = false;
        for (int i = 0; i < TFS.singleGridBtns.Length; i++)
        { TFS.singleGridBtns[i].interactable = true; }

        for (int i = 0; i < TFS.doubleGridBtns.Length; i++)
        { TFS.doubleGridBtns[i].interactable = true; }

        for (int i = 0; i < TFS.tripleGridBtns.Length; i++)
        { TFS.tripleGridBtns[i].interactable = true; }
    }
}
