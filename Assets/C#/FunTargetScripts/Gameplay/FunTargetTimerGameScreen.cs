using UnityEngine.UI;
using UnityEngine;
using TMPro;
using Com.BigWin.Frontend.Data;
using System.Collections;
using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
//using Assets.Scripts.Games.FunTarget.ApiData;
//using CurrentRoundNameSpace;
//using BetNameSpace;
//using LastBetNameSpace;
using m = UnityEngine.MonoBehaviour;
using UnityEngine.Events;
//using WinNampSpcae;
using FunTargetSocketClasses;
using Newtonsoft.Json;
using SocketIO;
using FunTarget.ServerStuff;
using FunTarget.Utility;

namespace FunTarget.GamePlay
{
    public class FunTargetTimerGameScreen : MonoBehaviour
    {
        #region Idenfiers
        // [SerializeField] Image headerBulbs;
        //[SerializeField] Image timerRingImg;
        //[SerializeField] Image baseWheelBulbs;
        //[SerializeField] Image LastWinImg;
        //[SerializeField] Sprite[] winImages;
        //[SerializeField] GameObject Lastbets;

        [SerializeField] Button exitBtn;
        [SerializeField] Button betOkBtn;
        [SerializeField] Button clearBtn;
        [SerializeField] Button doubleBtn;
        [SerializeField] Button repeatBtn;
        //--------------------------------------------
        // [SerializeField] Toggle chipNo1Btn;
        // [SerializeField] Toggle chipNo5Btn;
        [SerializeField] Toggle chipNo1Btn;
        [SerializeField] Toggle chipNo5Btn;
        [SerializeField] Toggle chipNo10Btn;
        [SerializeField] Toggle chipNo50Btn;
        [SerializeField] Toggle chipNo100Btn;
        [SerializeField] Toggle chipNo500Btn;
        [SerializeField] Toggle chipNo1000Btn;
        [SerializeField] Toggle chipNo5000Btn;
      //  [SerializeField] Toggle chipDeleteBtn;
        //-------------------------------------------------
        [SerializeField] Button zeroBetBtn;
        [SerializeField] Button oneBetBtn;
        [SerializeField] Button twoBetBtn;
        [SerializeField] Button threeBetBtn;
        [SerializeField] Button fourBetBtn;
        [SerializeField] Button fiveBetBtn;
        [SerializeField] Button sixBetBtn;
        [SerializeField] Button sevenBetBtn;
        [SerializeField] Button eightBetBtn;
        [SerializeField] Button nineBetBtn;
        //---------------------------------------------------
        [SerializeField] Text timerText;
      //  [SerializeField] TextMeshProUGUI comments;
       // [SerializeField] TextMeshProUGUI gameIDText;
        [SerializeField]public Text balanceText;
        [SerializeField] Text GameIdTxt;
        [SerializeField]public  Text totalBetText;
        [SerializeField]public Text winnigText;
        [SerializeField]public TextMeshProUGUI[] previousWinNOsTxt;//this represent the red boxes
   //     [SerializeField] TextMeshProUGUI[] previousWinXTxt;//this represent the red boxes
        [SerializeField] TextMeshProUGUI count;

        public float balance;
        private float totalBet;
        private int currentlySectedChip = 1;
        private int previousWinAmount;
        private int section;
        private int lastWinNo;

        private int[] previousBet = new int[10];
        public int[] betHolder = new int[10];
        public int[] PreviousbetHolder = new int[10];
        private int[] previousWins = new int[10];


        private string roundcount;
        private string lastroundcount;
        private string lastWinRoundcount;
        private string[] PrizeName;
        private string isPreviousWinsRecivied;
        private string winningAmount;
        private string currentComment;
        private string userId;
        private string[] commenstArray = {"Bets are Empty!!" ,"For Amusement Only","Bet Accepted!! your bet amount is :"
        ,"Please click on Take","Bets Confirmed"};

        private bool isUserPlacedBets;
        private bool isBetConfirmed;
        private bool canPlaceBet;
        private bool isLastGameWinAmountReceived;
        private bool canPlacedBet;
        private bool isthisisAFirstRound;
        private bool isPreviousBetPlaced;
        private bool isdataLoaded;
        private bool isTimeUp;
        private bool isActive;
        private Color timerRingColor;
        private User user;
        LastRoundWins[] lastRoundWins;
        public GameObject WaitPanel;


        const int SINGLE_BET_LIMIT = 5000;
        #endregion
        //public override void Initialize(Transform screenContainer, ScreenController screenController)
        //{
        //    base.Initialize(screenContainer, screenController);
        //    roundcount = string.Empty;
        //    isthisisAFirstRound = true;
        //    lastRoundWins = new LastRoundWins[10];
        //    //FindUIReferences();
        //    AddListners();
        //    timerRingColor = timerRingImg.color;

        //    // AddSocketListners();
        //}

        private void Start()
        {
            // socket = SocketIOComponent.instance;
            // StartCoroutine(Timer(25));
            AddListners();
            balance = 10000;
            balanceText.text = balance.ToString();
            Debug.Log(balance);
        }

        private void AddListners()
        {
            exitBtn.onClick.AddListener(() =>
            {
                SceneManager.LoadScene("MainScene");
              //  SocketRequest.intance.SendEvent(Constant.onleaveRoom);
                //  sc.OnClickHome();
            });
            exitBtn.onClick.AddListener(() => ResetAllBets());

            betOkBtn.onClick.AddListener(() =>
            {
               // if()
                OnBetCalculation();
                DisableButtons();
            });
            clearBtn.onClick.AddListener(() =>
            {
                ResetAllBets();
            });
            doubleBtn.onClick.AddListener(() => {

                OnClickOnDoubleBetBtn();

            });

            repeatBtn.onClick.AddListener(() =>
            {
                RepeatBets();
            });

            AddSocketListners();

            //------------betting no---------------
            oneBetBtn.onClick.AddListener(() =>  { AddBet(1, oneBetBtn.gameObject); });
            twoBetBtn.onClick.AddListener(() =>  { AddBet(2, twoBetBtn.gameObject); });
            threeBetBtn.onClick.AddListener(() =>{ AddBet(3, threeBetBtn.gameObject); });
            fourBetBtn.onClick.AddListener(() => { AddBet(4, fourBetBtn.gameObject); });
            fiveBetBtn.onClick.AddListener(() => { AddBet(5, fiveBetBtn.gameObject); });
            sixBetBtn.onClick.AddListener(() =>  { AddBet(6, sixBetBtn.gameObject); });
            sevenBetBtn.onClick.AddListener(() =>{ AddBet(7, sevenBetBtn.gameObject); });
            eightBetBtn.onClick.AddListener(() =>{ AddBet(8, eightBetBtn.gameObject); });
            nineBetBtn.onClick.AddListener(() => { AddBet(9, nineBetBtn.gameObject); });
            zeroBetBtn.onClick.AddListener(() => { AddBet(0, zeroBetBtn.gameObject); });

            //------------current betting cheap---------------
            chipNo1Btn.onValueChanged.AddListener((i) => { if (!i) return; currentlySectedChip = 1;  });
            chipNo5Btn.onValueChanged.AddListener((i) => { if (!i) return; currentlySectedChip = 5;  });
            chipNo1Btn.onValueChanged.AddListener((i) => { if (!i) return; currentlySectedChip = 1;  });
            chipNo5Btn.onValueChanged.AddListener((i) => { if (!i) return; currentlySectedChip = 5;  });
            chipNo10Btn.onValueChanged.AddListener((i) => { if (!i) return; currentlySectedChip = 10;  });
            chipNo50Btn.onValueChanged.AddListener((i) => { if (!i) return; currentlySectedChip = 50;  });
            chipNo100Btn.onValueChanged.AddListener((i) => { if (!i) return; currentlySectedChip = 100; } );
            chipNo500Btn.onValueChanged.AddListener((i) => { if (!i) return; currentlySectedChip = 500;  });
            chipNo1000Btn.onValueChanged.AddListener((i) => { if (!i) return; currentlySectedChip = 1000; });
            chipNo5000Btn.onValueChanged.AddListener((i) => { if (!i) return; currentlySectedChip = 5000;  });

        }

        private void DisableButtons()
        {
            zeroBetBtn.interactable = false;
            oneBetBtn.interactable = false;
            twoBetBtn.interactable = false;
            threeBetBtn.interactable = false;
            fourBetBtn.interactable = false;
            fiveBetBtn.interactable = false;
            sixBetBtn.interactable = false;
            sevenBetBtn.interactable = false;
            eightBetBtn.interactable = false;
            nineBetBtn.interactable = false;
            betOkBtn.interactable = false;
            clearBtn.interactable = false;
            doubleBtn.interactable = false;
            repeatBtn.interactable = false;
        }


        public IEnumerator ResetAll()
        {
            yield return new WaitForSeconds(2f);
                zeroBetBtn.interactable = true;
                oneBetBtn.interactable = true;
                twoBetBtn.interactable = true;
                threeBetBtn.interactable = true;
                fourBetBtn.interactable = true;
                fiveBetBtn.interactable = true;
                sixBetBtn.interactable = true;
                sevenBetBtn.interactable = true;
                eightBetBtn.interactable = true;
                nineBetBtn.interactable = true;
                betOkBtn.interactable = true;
                clearBtn.interactable = true;
                doubleBtn.interactable = true;
                repeatBtn.interactable = true;
            totalBetText.text = "0";
            balanceText.text = balance.ToString();
            zeroBetBtn.transform.GetChild(0).GetComponent<Text>().text = "0";
            oneBetBtn.transform.GetChild(0).GetComponent<Text>().text = "0";
            twoBetBtn.transform.GetChild(0).GetComponent<Text>().text = "0";
            threeBetBtn.transform.GetChild(0).GetComponent<Text>().text = "0";
            fourBetBtn.transform.GetChild(0).GetComponent<Text>().text = "0";
            fiveBetBtn.transform.GetChild(0).GetComponent<Text>().text = "0";
            sixBetBtn.transform.GetChild(0).GetComponent<Text>().text = "0";
            sevenBetBtn.transform.GetChild(0).GetComponent<Text>().text = "0";
            eightBetBtn.transform.GetChild(0).GetComponent<Text>().text = "0";
            nineBetBtn.transform.GetChild(0).GetComponent<Text>().text = "0";
            StartCoroutine(Timer()) ;
        }

        void AddSocketListners()
        {

            FunTarget_ServerRequest.instance.ListenEvent(Utility.Events.OnWinNo, (json) =>
            {
                // Debug.LogError("OnRound Ended...  " + json.ToString());
                StartCoroutine(OnRoundEnd(json));
            });
            FunTarget_ServerRequest.instance.ListenEvent(Utility.Events.OnTimerStart, (json) =>
            {
                OnTimerStart();
            }); 
            // FunTarget_ServerRequest.instance.ListenEvent(Constant.OnDissconnect, (json) =>
            // {
            //     if (!isActive) return;
            //     print("dissconnected");
            // }); 
            FunTarget_ServerRequest.instance.ListenEvent(Utility.Events.OnPlayerWin, (json) =>
            {
                OnWinAmount(json);
            });
            FunTarget_ServerRequest.instance.ListenEvent(Utility.Events.OnTimeUp, (json) =>
            {
                isTimeUp = true;
            });

        }

        void OnWinAmount(string res)
        {
            OnWinAmount o = JsonConvert.DeserializeObject<OnWinAmount>(res);
            winnigText.text = o.data.win_points.ToString();
            winningAmount = o.data.win_points.ToString();
        }
        void OnTimerStart()
        {
            WaitPanel.SetActive(false);
            Debug.Log("timer started");
            //this will get the current timer from the sever unless the timer is 0
            //it not it will wait for it
            StartCoroutine(GetCurrentTimer());
        }
        IEnumerator GetCurrentTimer()
        {
            yield return new WaitUntil(() => currentTime <= 0);
            StartCoroutine(Timer(60));
            // SocketRequest.intance.SendEvent(Constant.OnTimer, (json) =>
            // {
            //     print("current timer " + json);
            //     Timer time = JsonConvert.DeserializeObject<Timer>(json);
            //     StopCoroutine(Timer());
            //     if (time.result == 0) StartCoroutine(Timer());
            //     //else StartCoroutine(Timer(time.result));
            // });
        }

        private void OnClickOnDoubleBetBtn()
        {

            for (int i = 0; i < betHolder.Length; i++)
            {
                betHolder[i] *= 2;
                Debug.Log(i+" "+betHolder[i]);
            }
            zeroBetBtn.transform.GetChild(0).GetComponent<Text>().text = betHolder[0].ToString() == "0" ? string.Empty : betHolder[0].ToString();
            oneBetBtn.transform.GetChild(0).GetComponent<Text>().text = betHolder[1].ToString() == "0" ? string.Empty : betHolder[1].ToString();
            twoBetBtn.transform.GetChild(0).GetComponent<Text>().text = betHolder[2].ToString() == "0" ? string.Empty : betHolder[2].ToString();
            threeBetBtn.transform.GetChild(0).GetComponent<Text>().text = betHolder[3].ToString() == "0" ? string.Empty : betHolder[3].ToString();
            fourBetBtn.transform.GetChild(0).GetComponent<Text>().text = betHolder[4].ToString() == "0" ? string.Empty : betHolder[4].ToString();
            fiveBetBtn.transform.GetChild(0).GetComponent<Text>().text = betHolder[5].ToString() == "0" ? string.Empty : betHolder[5].ToString();
            sixBetBtn.transform.GetChild(0).GetComponent<Text>().text = betHolder[6].ToString() == "0" ? string.Empty : betHolder[6].ToString();
            sevenBetBtn.transform.GetChild(0).GetComponent<Text>().text = betHolder[7].ToString() == "0" ? string.Empty : betHolder[7].ToString();
            eightBetBtn.transform.GetChild(0).GetComponent<Text>().text = betHolder[8].ToString() == "0" ? string.Empty : betHolder[8].ToString();
            nineBetBtn.transform.GetChild(0).GetComponent<Text>().text = betHolder[9].ToString() == "0" ? string.Empty : betHolder[9].ToString();
            totalBet = betHolder.Sum();
            // SoundManager.instance.PlayClip("addbet");
            UpdateUi();

        }

        void RepeatBets()
        {
            for (int i = 0; i < betHolder.Length; i++)
            {
                betHolder[i] += PreviousbetHolder[i];
            }
            PlacePreviousBets();
            UpdateUi();
        }

        public void ResetAllBets()
        {
            zeroBetBtn.interactable = true;
            oneBetBtn.interactable = true;
            twoBetBtn.interactable = true;
            threeBetBtn.interactable = true;
            fourBetBtn.interactable = true;
            fiveBetBtn.interactable = true;
            sixBetBtn.interactable = true;
            sevenBetBtn.interactable = true;
            eightBetBtn.interactable = true;
            nineBetBtn.interactable = true;
            betOkBtn.interactable = true;
            clearBtn.interactable = true;
            doubleBtn.interactable = true;
            repeatBtn.interactable = true;
            totalBetText.text = "0";
            for (int i = 0; i < betHolder.Length; i++)
            {
                PreviousbetHolder[i] = betHolder[i];
            }
            for (int i = 0; i < betHolder.Length; i++)
            {
                betHolder[i] = 0;
            }
            oneBetBtn.transform.GetChild(0).GetComponent<Text>().text = string.Empty;
            twoBetBtn.transform.GetChild(0).GetComponent<Text>().text = string.Empty;
            threeBetBtn.transform.GetChild(0).GetComponent<Text>().text = string.Empty;
            fourBetBtn.transform.GetChild(0).GetComponent<Text>().text = string.Empty;
            fiveBetBtn.transform.GetChild(0).GetComponent<Text>().text = string.Empty;
            sixBetBtn.transform.GetChild(0).GetComponent<Text>().text = string.Empty;
            sevenBetBtn.transform.GetChild(0).GetComponent<Text>().text = string.Empty;
            eightBetBtn.transform.GetChild(0).GetComponent<Text>().text = string.Empty;
            nineBetBtn.transform.GetChild(0).GetComponent<Text>().text = string.Empty;
            zeroBetBtn.transform.GetChild(0).GetComponent<Text>().text = string.Empty;
            totalBet = 0;
            isUserPlacedBets = false;
            UpdateUi();
        }

        private void AddBet(int betIndex, GameObject btnREf)
        {
            Debug.Log("isdataLoaded " + isdataLoaded);
            if (!isdataLoaded)
            {
                m.print("please wait data to load");
                AndroidToastMsg.ShowAndroidToastMessage("please wait");
                return;
            }
            Debug.Log("canPlaceBet " + canPlaceBet);
            if (!canPlaceBet || isTimeUp) return;
            if (currentlySectedChip == 0)
            {
                m.print("please select a chip first");
                AndroidToastMsg.ShowAndroidToastMessage("please select a chip first");
                return;
            }

            if (balance < currentlySectedChip || balance < currentlySectedChip + betHolder.Sum())
            {
                m.print("not enough balanc");
                AndroidToastMsg.ShowAndroidToastMessage("not enough balance");
                Debug.Log("return here ");
                return;
            }
            if (betHolder[betIndex] + currentlySectedChip > SINGLE_BET_LIMIT)
            {
                m.print("reached the limit");
                AndroidToastMsg.ShowAndroidToastMessage("reached the limit");
                return;
            }
            if (currentlySectedChip == -1)//this is for delete chip btn
            {
                if (betHolder[betIndex] == 0) return;
                betHolder[betIndex] = 0;
            }
            else
            {
                betHolder[betIndex] += currentlySectedChip;
            }
            betOkBtn.gameObject.transform.GetChild(0).GetComponent<Text>().text = "BET OK";
            clearBtn.interactable = true;
            isPreviousBetPlaced = true;
            totalBet = betHolder.Sum();
            btnREf.transform.GetChild(0).GetComponent<Text>().text = betHolder[betIndex].ToString() == "0" ? string.Empty : betHolder[betIndex].ToString();
            currentComment = commenstArray[1];
            // SoundManager.instance.PlayClip("addbet");
            UpdateUi();
        }

        private void LoadDefaultData()
        {
            StartCoroutine(AnimateBulbs());
            currentComment = commenstArray[1];
            lastwinNo = -1;
            canPlaceBet = false;
         //   LastWinImg.gameObject.SetActive(false);
            isthisisAFirstRound = true;
            isLastGameWinAmountReceived = true;
            isTimerStarted = false;
            isdataLoaded = false;
            isSomethingWentWrong = false;
            isCurrentBetPlace = false;
            winningAmount = string.Empty;
        }
        bool isTimerStarted;
        private bool isSomethingWentWrong;
        private bool isCurrentBetPlace;
        int lastwinNo;
        int minmumTimeAllowed = 6;
        string tempRoundCount;
        public List<int> PreviousInfo;
        void UpdateRoundData(CurrenRoundInfo curretRoundInfo, bool isFirstARound = false)
        {

            Debug.Log("Update round");
            if (curretRoundInfo.gametimer < minmumTimeAllowed)
            {
                AndroidToastMsg.ShowAndroidToastMessage("Please wait");
                Debug.Log("Please wait");
                currentTime = 0;
                UpdateUi();
                return;
            }
            isdataLoaded = true;
            isTimeUp = false;
            isPreviousBetPlaced = false;
            canPlaceBet = true;
            roundcount = curretRoundInfo.RoundCount.ToString();
            balance = curretRoundInfo.balance;
            // balance = 10000;
            totalBet = 0;
            int arrayLimit = curretRoundInfo.previousWinData.Count - 1;
            for (int j = 0; j < curretRoundInfo.previousWinData.Count; j++)
            {
                previousWinNOsTxt[j].text = curretRoundInfo.previousWinData[j].winNo.ToString();
            }
            curretRoundInfo.previousWinData.Reverse();
            // for (int i = 0; i <= arrayLimit; i++)
            // {
            //     int number = arrayLimit - i;
            //     previousWinNOsTxt[number].text = curretRoundInfo.previousWinData[i].winNo.ToString();
            //     previousWinXTxt[number].text = curretRoundInfo.previousWinData[i].winx.ToString();
            // }
            Debug.Log("is a first round " + isFirstARound);
            // winnigText.text = curretRoundInfo.previousWinData[0].winNo.ToString();
            if (isFirstARound)
            {
                if (tempRoundCount == roundcount)
                {

                }
                int lastroundNo = curretRoundInfo.previousWinData[0].winNo;
                string winx = curretRoundInfo.previousWinData[0].winx;
             //   StartCoroutine(Timer(curretRoundInfo.gametimer));

#if UNITY_ANDROID
                //   SpinWheel.instane.SetWheelInitialAngle(lastroundNo, winx);
#else
                SpinWheelWithoutPlugin.instane.SetWheelInitialAngle(lastroundNo, winx);
#endif
            }
            else
            {
                if (previousBet.Sum() > 0)
                    betOkBtn.gameObject.transform.GetChild(0).GetComponent<Text>().text = "Pre";
            }

            currentComment = commenstArray[1];

            // if(curretRoundInfo.pendingWinningData != null)
            // {
            //     Debug.Log("won some amount");
            //     OnWinTheBet(curretRoundInfo.pendingWinningData.win_no);
            //     winningAmount = curretRoundInfo.pendingWinningData.winPoints.ToString();

            // }
            // else
            // {
            //     winningAmount = string.Empty;
            //     Debug.Log("didnot won anything");
            // };

            tempRoundCount = roundcount;
            UpdateUi();
        }
        public void SomeThingWentWrong()
        {
            StopAllCoroutines();
            isSomethingWentWrong = true;
            LoadDefaultData();
            balance = 0;
            totalBet = 0;
            previousWinAmount = 0;
            UpdateUi();
            try
            {

              //  StartNextOrNewRound(true);
            }
            catch (Exception e)
            {
                Debug.Log(e.Message);
                Debug.Log(e.StackTrace);
                throw;
            }
            m.print("something went wrong called");
        }
        private void UpdateUi()
        {
            balanceText.text = balance.ToString();
            //   GameIdTxt.text = sc.data.Email;
            totalBetText.text = totalBet.ToString();
            winnigText.text = winningAmount;
        }

        private void OnBetCalculation()
        {
            if (!isdataLoaded)
            {
                m.print("please wait data to load");
                AndroidToastMsg.ShowAndroidToastMessage("please wait data to load");
                return;
            }
            // if (!isLastGameWinAmountReceived)
            // {

            //     currentComment = commenstArray[1];
            //     SendTakeAmountRequest();
            //     UpdateUi();
            //     previousWinAmount = 0;
            //     return;
            // }
            if (previousBet.Sum() != 0 && !isPreviousBetPlaced
                && betOkBtn.gameObject.transform.GetChild(0).GetComponent<Text>().text == "Pre")
            {
                PlacePreviousBets();
                betOkBtn.gameObject.transform.GetChild(0).GetComponent<Text>().text = "BET OK";
                UpdateUi();
                return;
            }
            if (betHolder.Sum() == 0)
            {
                currentComment = commenstArray[0];
                UpdateUi();
                return;
            }
            if (!canPlaceBet) return;
            canPlaceBet = false;
            isUserPlacedBets = true;
            currentComment = commenstArray[2] + betHolder.Sum().ToString();
            betOkBtn.interactable = clearBtn.interactable = doubleBtn.interactable = repeatBtn.interactable = false;
            Array.Copy(betHolder, previousBet, betHolder.Length);
            // SendBets();
            UpdateUi();
        }

        /// <summary>
        /// This function just copy the PreviousBetArray into currenBetArray
        /// and updates the UI
        /// </summary>
        private void PlacePreviousBets()
        {
            bool isEnoughBalance = previousBet.Sum() < balance;
            if (!isEnoughBalance)
            {
                m.print("not enough balance");
                AndroidToastMsg.ShowAndroidToastMessage("not enough balance");
                return;
            }
            Array.Copy(previousBet, betHolder, previousBet.Length);

            oneBetBtn.transform.GetChild(0).GetComponent<Text>().text = betHolder[1] == 0 ? string.Empty : betHolder[1].ToString();
            twoBetBtn.transform.GetChild(0).GetComponent<Text>().text = betHolder[2] == 0 ? string.Empty : betHolder[2].ToString();
            threeBetBtn.transform.GetChild(0).GetComponent<Text>().text = betHolder[3] == 0 ? string.Empty : betHolder[3].ToString();
            fourBetBtn.transform.GetChild(0).GetComponent<Text>().text = betHolder[4] == 0 ? string.Empty : betHolder[4].ToString();
            fiveBetBtn.transform.GetChild(0).GetComponent<Text>().text = betHolder[5] == 0 ? string.Empty : betHolder[5].ToString();
            sixBetBtn.transform.GetChild(0).GetComponent<Text>().text = betHolder[6] == 0 ? string.Empty : betHolder[6].ToString();
            sevenBetBtn.transform.GetChild(0).GetComponent<Text>().text = betHolder[7] == 0 ? string.Empty : betHolder[7].ToString();
            eightBetBtn.transform.GetChild(0).GetComponent<Text>().text = betHolder[8] == 0 ? string.Empty : betHolder[8].ToString();
            nineBetBtn.transform.GetChild(0).GetComponent<Text>().text = betHolder[9] == 0 ? string.Empty : betHolder[9].ToString();
            zeroBetBtn.transform.GetChild(0).GetComponent<Text>().text = betHolder[0] == 0 ? string.Empty : betHolder[0].ToString();
            isPreviousBetPlaced = true;
            totalBet = betHolder.Sum();
            UpdateUi();
        }

        int currentTime = 0;
        bool canStopTimer;
        /// <summary>
        /// This is the 60 sec timer 
        /// </summary>
        /// <param name="counter"></param>
        /// <returns></returns>
        private IEnumerator Timer(int counter = 60) //60
        {
            isTimeUp = false;
            isdataLoaded = true;
            canPlaceBet = true;
            betOkBtn.interactable = clearBtn.interactable = doubleBtn.interactable = repeatBtn.interactable = true;
            isUserPlacedBets = false;
            canStopTimer = false;
            Debug.Log("timer started");
          
            while (counter > 0)
            {
                if (canStopTimer) yield break;
                timerText.text = counter.ToString();
                currentTime = counter;
                if (counter < 6)
                {
                    isTimeUp = true;
                    zeroBetBtn.interactable = oneBetBtn.interactable = twoBetBtn.interactable = 
                    threeBetBtn.interactable = fourBetBtn.interactable = fiveBetBtn.interactable = 
                    sixBetBtn.interactable = sevenBetBtn.interactable = eightBetBtn.interactable = nineBetBtn.interactable = false;
                    MonitorBets();
                    canPlaceBet = false;
                    if (!isBetConfirmed)
                    {
                        OnBetCalculation();
                    }
                }
                yield return new WaitForSeconds(1f);
                counter--;
            }
            currentTime = 0;
            timerText.text = 0.ToString();
        }

        void MonitorBets()
        {
            bool canPostBetsToServer = isTimeUp || isUserPlacedBets;
            if (canPostBetsToServer)
            {
                if (canPlaceBet)
                {
                    canPlaceBet = false;
                    if (!isUserPlacedBets)
                    {
                        // SendBets();
                    }
                }
              //  timerRingImg.color = timerRingColor;
                betOkBtn.interactable
                    = clearBtn.interactable
                    = doubleBtn.interactable
                    = repeatBtn.interactable = false;
            }

        }

        /// <summary>
        /// this will call before 6 sec from the server
        /// </summary>
        /// <param name="res"></param>
        IEnumerator OnRoundEnd(object res)
        {
            yield return new WaitUntil(() => currentTime == 0);

            Weel o = JsonConvert.DeserializeObject<Weel>(res.ToString());
            // int no = /*o.data.win_no;*/ 1;
            int no = UnityEngine.Random.Range(0,9);
            lastwinNo = no;
            Debug.Log("no "+no);
            Spin(no);
           // StartCoroutine(Spin(5, "c"));
        }
        void Spin(int winNo)
        {
            // SpinWheelWithoutPlugin.instane.Spin(winNo);
            SpinWheelWithoutPlugin.instane.onSpinComplete = () =>
            {
               //  winNumber.text = winNo.ToString();
              // StartNextOrNewRound();
            };

            #if UNITY_ANDROID

                        // SpinWheelWithoutPlugin.instane.Spin(winNo);
                    // SpinWheelWithoutPlugin.instane.onSpinComplete = () =>
                        //SpinWheel.instane.SpinTheWheel(winNo, xFactorImage);
                        //SpinWheel.instane.onSpinComplete = () =>
                    //  { StartNextOrNewRound(); };
            #else
                            //  SpinWheelWithoutPlugin.instane.Spin(winNo, xFactorImage);
                            //  SpinWheelWithoutPlugin.instane.onSpinComplete = () =>
                            //  { StartNextOrNewRound(); };
            #endif
        }

        private void SendBets()
        {
            if (betHolder.Sum() == 0)
            {
                currentComment = commenstArray[0];
                UpdateUi();
                return;
            }

            FunTargetSocketClasses.Bet data = new FunTargetSocketClasses.Bet
            {
                // no_0 = betHolder[0],
                // no_1 = betHolder[1],
                // no_2 = betHolder[2],
                // no_3 = betHolder[3],
                // no_4 = betHolder[4],
                // no_5 = betHolder[5],
                // no_6 = betHolder[6],
                // no_7 = betHolder[7],
                // no_8 = betHolder[8],
                // no_9 = betHolder[9],
                gameId =   6,         //(int)Games.funWheel,
                round_count = long.Parse(roundcount),
                device = SystemInfo.deviceUniqueIdentifier,
                playerId = "Test1",
                points = betHolder.Sum().ToString(),
                betArray = betHolder
            };
            Debug.Log("data  " + data);
            PostBet(data);
            canPlaceBet = false;
        }
        private void PostBet(FunTargetSocketClasses.Bet data)
        {

            //SocketRequest.intance.SendEvent(Constant.OnPlaceBet, data, (res) =>
            //{
            //    var response = JsonConvert.DeserializeObject<BetConfirmation>(res);
            //    if (response == null)
            //    {
            //        SomeThingWentWrong();
            //        return;
            //    }
            //    if (Constant.IS_INVALID_USER == response.status)
            //    {
            //        OnInvalidUser(response.message);
            //        return;
            //    }
            //    if (response.status == "200") { balance -= betHolder.Sum(); isBetConfirmed = true; }
            //    currentComment = response.message;
            //    UpdateUi();
            //    Debug.Log("is bet placed starus with statu - " + JsonUtility.FromJson<BetConfirmation>(res).status);

            //});

        }
        private void OnInvalidUser(string msg)
        {
            m.print("invalid user");
            //dialogue.Show(msg, okButtonMsg: "Logout");
            //dialogue.OnDialogHide = () =>
            //{
            //    sc.OnLoginScreen();
            //};
        }
        private void OnWinTheBet(int winnigNO)
        {
            isLastGameWinAmountReceived = false;
            canPlaceBet = false;
            betOkBtn.gameObject.transform.GetChild(0).GetComponent<Text>().text = "Take";
            currentComment = commenstArray[3];
            m.print("last round won and win no is" + winnigNO);
          //  LastWinImg.sprite = winImages[winnigNO];
           // LastWinImg.gameObject.SetActive(true);
            currentComment = commenstArray[3];
            UpdateUi();
        }

        private void OnApplicationPause(bool pause)
        {
            //when appliction resume
            if (!pause)
            {
                SomeThingWentWrong();
            }
        }
        // public override ScreenID ScreenID => ScreenID.FUN_TARGET_TIMER_GAME_SCREEN;
        // protected override string ScreenName => "FunTargetTimerGameScreen";
        // Animation Functions
        private IEnumerator AnimateBulbs()
        {
            bool switchOn = false;
            while (isActive)
            {
                yield return new WaitForSeconds(0.25f);
                if (switchOn)
                {
                    // headerBulbs.color = Color.white;
                  //  baseWheelBulbs.enabled = true;
                }
                else
                {
                    //   headerBulbs.color = Color.red;
                  //  baseWheelBulbs.enabled = false;
                }
                switchOn = !switchOn;
            }
        }
    }
}
[Serializable]
public class User
{
    public string user_id;
    public string device;

    public int game;//it the game id
}
[Serializable]
public class LastWinAmountAdder
{
    public string user_id;
    public string device;
    public int game;//it the game id
    public int round_count;//it the game id
}

[Serializable]
public class AddWinAmountResponse
{
    public string message;
    public string status;
    public string coins;
    public string sec;
    public int win_amount;
    public int is_winning_amount_add;
}

[Serializable]
public class LastRoundWins
{
    public int winNo;
    public string winX;
}

[Serializable]
public class RoundCount
{
    public string round_count;
}


namespace FunTargetSocketClasses
{
    public class PreviousWinData
    {
        public double RoundCount;
        public int winNo;
        public string winx;
    }
    [SerializeField]
    public class CurrenRoundInfo
    {
        public int gametimer;
        public float balance;
        public double RoundCount;
        public List<PreviousWinData> previousWinData;
        // public OnWinAmount pendingWinningData;
    }


    public class WeelData
    {
        public double RoundCount;
        public int win_no;
        public string winX;
    }

    public class Weel
    {
        public bool status;
        public string message;
        public WeelData data;
    }

    public class Timer
    {
        public int result;//timer
    }

    public class OnWinAmount
    {
        public bool status;
        public string message;
        public OnWinAmountData data;
    }
    public class OnWinAmountData
    {
        public long RoundCount;
        public int win_no;
        // public int winPoints;
        public int win_points;
        public string player_id;
        public string balance;
    }

    public class WinAmountConfirmationData
    {
        public string playerId;
        public int balance;
    }

    public class WinAmountConfirmation
    {
        public bool status;
        public string message;
        public WinAmountConfirmationData data;
    }

    public class PreviousRoundBets
    {
        public int isCurrRoundBet;
        public int no_0;
        public int no_1;
        public int no_2;
        public int no_3;
        public int no_4;
        public int no_5;
        public int no_6;
        public int no_7;
        public int no_8;
        public int no_9;
    }

    public class Bet
    {
        public long round_count;
        public int gameId;
        public string playerId;
        public string device;
        public string points;
        public int[] betArray;
    }
}