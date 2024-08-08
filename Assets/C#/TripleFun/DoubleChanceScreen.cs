//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using System;
//using System.Linq;
//using m = UnityEngine.MonoBehaviour;
////using Com.BigWin.Frontend.Data;
//using UnityEngine.UI;
//using TMPro;
//using Newtonsoft.Json;
//using SocketIO;
////using BetNameSpace;
////using LastBetNameSpace;
////using Com.BigWin.Frontend.JeetoJokerSocketClasses;
////using DoubleChance;

//public class TripleFun : MonoBehaviour
//{
//    [SerializeField] Toggle chipNo10Btn;
//    [SerializeField] Toggle chipNo50Btn;
//    [SerializeField] Toggle chipNo100Btn;
//    [SerializeField] Toggle chipNo200Btn;
//    [SerializeField] Toggle chipNo500Btn;
//    [SerializeField] Toggle chipNo1000Btn;

//    #region BETTING RETATED GRID
//    //---------All Type Cards------------
//    [SerializeField] Button[] doubleGridBtns;
//    [SerializeField] Button[] singleGridBtns;

//    #endregion
//    [SerializeField] Button exitBtn;
//    [SerializeField] Button betOkBtn;
//    [SerializeField] Button clearBtn;
//    [SerializeField] Button doubleBtn;
//    [SerializeField] Button repeatBtn;
//    [SerializeField] Button RandomPickBtn;
//    [SerializeField] GameObject RandomPickObj;

//    [SerializeField] Text timerTxt;
//    [SerializeField] Text balanceTxt;
//    [SerializeField] Text totalBetsTxt;
//    [SerializeField] Text commentTxt;

//    const int SINGLE_BETS_LIMIT = 5000;
//    const int OVERALL_BETS_LIMIT = 50000;

//    private bool isUserPlacedBets;
//    private bool isBetConfirmed;
//    private bool canPlaceBet;
//    private bool isLastGameWinAmountReceived;
//    private bool canPlacedBet;
//    private bool isthisisAFirstRound;
//    private bool isPreviousBetPlaced;
//    private bool isdataLoaded;
//    private bool isTimeUp;


//    private int lastWinNumber;
//    int[] ALL_HEARTS_SPADES_DIAMONDS_CLUBS_BET_CONTINER = new int[4];//All
//    int[] Double_Bets_Container = new int[12];
//    int[] Previous_Double_Bets_Container = new int[12];
//    int[] Single_Bets_Container = new int[3];
//    int[] Previous_Single_Bets_Container = new int[3];
//    int currentlySelectedChip = 10;

//    [SerializeField] float balance;
//    int totalBets;
//    string roundcount;
//    string lastroundcount;
//    string lastWinRoundcount;
//    string isPreviousWinsRecivied;
//    string winningAmount;
//    string currentComment;
//    string userId;
//    string[] PrizeName;
//    string[] commenstArray = { "Bets are Empty!!", "For Amusement Only", "Bet Accepted!! your bet amount is :", "Please click on Take", "Bets Confirmed" };

//    //[SerializeField] GameObject DoubleChancebtn;
//    //public override ScreenID ScreenID => ScreenID.DOUBLE_CHANCE_GAME_SCREEN;
//    //protected override string ScreenName =>  DoubleChancebtn.name;

//    int outer_win_wheelValue, inner_wheelValue;

//    public List<string> singleWins_list = new List<string>();
//    public List<string> DoubleWins_List = new List<string>();
//    public List<Text> PreviousSingleWinList = new List<Text>();
//    public List<Text> PreviousDoubleWinList = new List<Text>();

//    //public override void Initialize(Transform screenContainer, ScreenController screenController)
//    //{
//    //    base.Initialize(screenContainer, screenController);
//    //    AddListners();
//    //    Double_Bets_Container = new int[doubleGridBtns.Length];
//    //    Previous_Double_Bets_Container = new int[doubleGridBtns.Length];
//    //    Single_Bets_Container = new int[singleGridBtns.Length];
//    //    Previous_Single_Bets_Container = new int[singleGridBtns.Length];
//    //}

//    private void AddListners()
//    {
//        exitBtn.onClick.AddListener(() =>
//        {
//            //SocketRequest.intance.SendEvent(Constant.onleaveRoom);
//            //sc.OnClickHome();
//        });

//        betOkBtn.onClick.AddListener(() =>
//        {
//            OnBetsOk();
//        });

//        clearBtn.onClick.AddListener(() =>
//        {
//            if (!canPlaceBet) return;
//            ResetAllBets();
//        });

//        doubleBtn.onClick.AddListener(() =>
//        {
//            OnClickOnDoubleBetBtn();
//        });

//        repeatBtn.onClick.AddListener(() =>
//        {
//            RepeatBets();
//        });

//        RandomPickBtn.onClick.AddListener(() =>
//        {
//            RandomPickObj.SetActive(true);
//        });

//        AddCardBetListners();

//        chipNo10Btn.onValueChanged.AddListener((i) => { if (!i) return; currentlySelectedChip = 10; });
//        chipNo50Btn.onValueChanged.AddListener((i) => { if (!i) return; currentlySelectedChip = 50; });
//        chipNo100Btn.onValueChanged.AddListener((i) => { if (!i) return; currentlySelectedChip = 100; });
//        chipNo200Btn.onValueChanged.AddListener((i) => { if (!i) return; currentlySelectedChip = 200; });
//        chipNo500Btn.onValueChanged.AddListener((i) => { if (!i) return; currentlySelectedChip = 500; });
//        chipNo1000Btn.onValueChanged.AddListener((i) => { if (!i) return; currentlySelectedChip = 1000; });
//    }

//    private void AddCardBetListners()
//    {
//        for (int i = 0; i < doubleGridBtns.Length; i++)
//        {
//            int index = i;
//            doubleGridBtns[i].onClick.AddListener(() =>
//            {
//                AddBets(ref Double_Bets_Container, index);
//            });
//        }


//        for (int i = 0; i < singleGridBtns.Length; i++)
//        {
//            int index = i;
//            singleGridBtns[i].onClick.AddListener(() =>
//            {
//                AddBets(ref Single_Bets_Container, index);
//            });
//        }
//    }

//    private void AddBets(ref int[] continer, int containerIndex)
//    {
//        Debug.Log("isdataLoaded " + isdataLoaded);
//        if (!isdataLoaded)
//        {
//            AndroidToastMsg.ShowAndroidToastMessage("please wait");
//            return;
//        }
//        if (!canPlaceBet || isTimeUp) return;
//        if (currentlySelectedChip == 0)
//        {
//            AndroidToastMsg.ShowAndroidToastMessage("please select a chip first");
//            return;
//        }

//        if (balance < currentlySelectedChip || balance < currentlySelectedChip + totalBets)
//        {
//            AndroidToastMsg.ShowAndroidToastMessage("not enough balance");
//            return;
//        }
//        if (continer[containerIndex] + currentlySelectedChip > SINGLE_BETS_LIMIT)
//        {
//            AndroidToastMsg.ShowAndroidToastMessage("reached the limit");
//            return;
//        }
//        continer[containerIndex] += currentlySelectedChip;
//        UpdateUi();

//        SoundManager.instance.PlayClip("addbet");
//    }

//    void RepeatBets()
//    {
//        for (int i = 0; i < Double_Bets_Container.Length; i++)
//        {
//            Double_Bets_Container[i] += Previous_Double_Bets_Container[i];
//        }

//        for (int i = 0; i < Single_Bets_Container.Length; i++)
//        {
//            Single_Bets_Container[i] += Previous_Single_Bets_Container[i];
//        }
//        UpdateUi();
//    }

//    void OnClickOnDoubleBetBtn()
//    {
//        int _totalBets_value = Double_Bets_Container.Sum() + Single_Bets_Container.Sum();
//        if (_totalBets_value == 0)
//        {

//            m.print("no bet placed yet");
//            AndroidToastMsg.ShowAndroidToastMessage("no bet placed yet");
//            return;
//        }
//        bool isEnoughBalance = balance > _totalBets_value * 2;

//        if (!isEnoughBalance)
//        {
//            m.print("not enough balance");
//            AndroidToastMsg.ShowAndroidToastMessage("not enough balance");
//            return;
//        }

//        bool isRichedTheLimit = _totalBets_value * 2 > SINGLE_BETS_LIMIT;

//        if (isRichedTheLimit)
//        {
//            m.print("reached the limit");
//            AndroidToastMsg.ShowAndroidToastMessage("reached the limit");
//            return;
//        }

//        for (int i = 0; i < Double_Bets_Container.Length; i++)
//        {
//            Double_Bets_Container[i] *= 2;
//        }

//        for (int i = 0; i < Single_Bets_Container.Length; i++)
//        {
//            Single_Bets_Container[i] *= 2;
//        }

//        for (int i = 0; i < doubleGridBtns.Length; i++)
//        {
//            doubleGridBtns[i].transform.GetChild(0).GetComponent<TMP_Text>().text = Double_Bets_Container[i].ToString() == "0" ? string.Empty : Double_Bets_Container[i].ToString();
//        }
//        for (int i = 0; i < singleGridBtns.Length; i++)
//        {
//            singleGridBtns[i].transform.GetChild(0).GetComponent<TMP_Text>().text = Single_Bets_Container[i].ToString() == "0" ? string.Empty : Single_Bets_Container[i].ToString();
//        }

//        totalBets = _totalBets_value;
//        SoundManager.instance.PlayClip("addbet");
//        UpdateUi();
//    }

//    void OnBetsOk()
//    {
//        if (totalBets == 0)
//        {
//            commentTxt.text = "Bets Are Empty";
//            return;
//        }
//        if (isTimeUp)
//        {
//            AndroidToastMsg.ShowAndroidToastMessage("Time UP");
//            return;
//        }
//        BettingButtonInteractablity(false);
//        commentTxt.text = "Bets Confirmed";
//        clearBtn.interactable = false;
//        betOkBtn.interactable = false;
//        doubleBtn.interactable = false;
//        repeatBtn.interactable = false;
//        balance -= totalBets;
//        isBetConfirmed = true;
//       // SendBets();
//        UpdateUi();
//    }

//    //private void SendBets()
//    //{
//    //    if (totalBets == 0)
//    //    {
//    //        currentComment = commenstArray[0];
//    //        UpdateUi();
//    //        return;
//    //    }

//    //    var c = new JeetoJokerTimerScreen12Cards.Allcard
//    //    {

//    //        no_0 = Double_Bets_Container[0],
//    //        no_1 = Double_Bets_Container[1],
//    //        no_2 = Double_Bets_Container[2],
//    //        no_3 = Double_Bets_Container[3],
//    //        no_4 = Double_Bets_Container[4],
//    //        no_5 = Double_Bets_Container[5],
//    //        no_6 = Double_Bets_Container[6],
//    //        no_7 = Double_Bets_Container[7],
//    //        no_8 = Double_Bets_Container[8],
//    //        no_9 = Double_Bets_Container[9],
//    //        no10 = Double_Bets_Container[10],
//    //        no11 = Double_Bets_Container[11],

//    //    };
//    //    var hsdc = new JeetoJokerTimerScreen12Cards.AllFaceCard
//    //    {
//    //        no_0 = ALL_HEARTS_SPADES_DIAMONDS_CLUBS_BET_CONTINER[0],
//    //        no_1 = ALL_HEARTS_SPADES_DIAMONDS_CLUBS_BET_CONTINER[1],
//    //        no_2 = ALL_HEARTS_SPADES_DIAMONDS_CLUBS_BET_CONTINER[2],
//    //        no_3 = ALL_HEARTS_SPADES_DIAMONDS_CLUBS_BET_CONTINER[3],
//    //    };

//    //    var jqk = new JeetoJokerTimerScreen12Cards.FaceCard()
//    //    {
//    //        no_0 = Single_Bets_Container[0],
//    //        no_1 = Single_Bets_Container[1],
//    //        no_2 = Single_Bets_Container[2],
//    //    };


//    //    bets data = new bets
//    //    {

//    //        gameId = (int)Games.DoubleChance,
//    //        // playerId = LocalDatabase.data.email,
//    //        playerId = sc.data.Email,
//    //        @double = Double_Bets_Container,
//    //        single = Single_Bets_Container
//    //    };
//    //    PostBet(data);
//    //    canPlaceBet = false;
//    //}

//    //private void PostBet(bets data)
//    //{
//    //    SocketRequest.intance.SendEvent(Constant.OnPlaceBet, data, (res) =>
//    //    {
//    //        var response = JsonConvert.DeserializeObject<BetConfirmation>(res);
//    //        if (response == null)
//    //        {
//    //            return;
//    //        }
//    //        if (Constant.IS_INVALID_USER == response.status)
//    //        {
//    //            return;
//    //        }
//    //        if (response.status == "200") { balance -= totalBets; isBetConfirmed = true; }
//    //        currentComment = response.message;
//    //        UpdateUi();
//    //        Debug.Log("is bet placed starus with statu - " + JsonUtility.FromJson<BetConfirmation>(res).status);

//    //    });
//    //}

//    //public override void Show(object data = null)
//    //{
//    //    base.Show(data);
//    //    if (data == null)
//    //    {
//    //        Debug.Log("someting went wrong");
//    //        return;
//    //    }
//    //    var o = JsonConvert.DeserializeObject<roundInfo>(data.ToString());
//    //    balance = o.balance;
//    //    // balance = 10000f;
//    //    isdataLoaded = true;

//    //    foreach (var w in o.previousWinData)
//    //    {

//    //        if (singleWins_list.Count > 6)
//    //        {
//    //            singleWins_list.RemoveAt(0);
//    //            singleWins_list.Add(w.outer_win_no.ToString());
//    //        }
//    //        else
//    //        {
//    //            singleWins_list.Add(w.outer_win_no.ToString());
//    //        }

//    //        if (DoubleWins_List.Count > 6)
//    //        {
//    //            DoubleWins_List.RemoveAt(0);
//    //            DoubleWins_List.Add(w.outer_win_no.ToString() + w.inner_win_no.ToString());
//    //        }
//    //        else
//    //        {
//    //            DoubleWins_List.Add(w.outer_win_no.ToString() + w.inner_win_no.ToString());
//    //        }
//    //    }

//    //    for (int i = 0; i < PreviousSingleWinList.Count; i++)
//    //    {
//    //        PreviousSingleWinList[i].text = singleWins_list[i];
//    //    }

//    //    for (int i = 0; i < PreviousDoubleWinList.Count; i++)
//    //    {
//    //        PreviousDoubleWinList[i].text = DoubleWins_List[i];
//    //    }

//    //    StartCoroutine(Timer(o.gametimer));
//    //    UpdateUi();
//    //    AddSocketListners();
//    //}

//    void AddSocketListners()
//    {

//    }



//    //void AddSocketListners()
//    //{
//    //    Action onBadResponse = () => { sc.Back(); };

//    //    SocketRequest.intance.ListenEvent<weelNumbers>(Constant.OnWinNo, (json) =>
//    //    {
//    //        if (!isActive) return;
//    //        StartCoroutine(OnRoundEnd(json));
//    //    }, onBadResponse);
//    //    SocketRequest.intance.ListenEvent(Constant.OnTimerStart, (json) =>
//    //    {
//    //        if (!isActive) return;
//    //        OnTimerStart();
//    //    });
//    //    SocketRequest.intance.ListenEvent(Constant.OnDissconnect, (json) =>
//    //    {
//    //        if (!isActive) return;
//    //        print("dissconnected");
//    //    });
//    //    SocketRequest.intance.ListenEvent<DoubleChanceClasses.OnWinAmount>(Constant.OnWinAmount, (json) =>
//    //    {
//    //        if (!isActive) return;
//    //        OnWinAmount(json.ToString());
//    //    }, onBadResponse);


//    //    SocketRequest.intance.ListenEvent(Constant.OnTimeUp, (json) =>
//    //    {
//    //        if (!isActive) return;
//    //        BettingButtonInteractablity(false);
//    //        isTimeUp = true;
//    //    });

//    //}

//    void OnWinAmount(string res)
//    {
//        DoubleChanceClasses.OnWinAmount o = JsonConvert.DeserializeObject<DoubleChanceClasses.OnWinAmount>(res.ToString());
//    }

//    #region GAME_FLOW
//    private void OnTimerStart()
//    {
//      //  StartCoroutine(GetCurrentTimer());
//    }
//    //IEnumerator GetCurrentTimer()
//    //{
//    //    yield return new WaitUntil(() => currentTime <= 0);
//    //    SocketRequest.intance.SendEvent(Constant.OnTimer, (json) =>
//    //    {
//    //        print("current timer " + json);
//    //        Timer time = JsonConvert.DeserializeObject<Timer>(json);
//    //        if (time.result < 10)
//    //        {
//    //            isTimeUp = true;
//    //            BettingButtonInteractablity(false);
//    //        };
//    //        isdataLoaded = true;
//    //        StopCoroutine(Timer());
//    //        StartCoroutine(Timer(time.result));
//    //    });
//    //}    //IEnumerator GetCurrentTimer()
//    //{
//    //    yield return new WaitUntil(() => currentTime <= 0);
//    //    SocketRequest.intance.SendEvent(Constant.OnTimer, (json) =>
//    //    {
//    //        print("current timer " + json);
//    //        Timer time = JsonConvert.DeserializeObject<Timer>(json);
//    //        if (time.result < 10)
//    //        {
//    //            isTimeUp = true;
//    //            BettingButtonInteractablity(false);
//    //        };
//    //        isdataLoaded = true;
//    //        StopCoroutine(Timer());
//    //        StartCoroutine(Timer(time.result));
//    //    });
//    //}

//    int currentTime = 0;
//    bool canStopTimer;
//    /// <summary>
//    /// This is the 60 sec timer 
//    /// </summary>
//    /// <param name="counter"></param>
//    /// <returns></returns>
//    private IEnumerator Timer(int counter = 60) //60
//    {
//        isTimeUp = false;
//        canPlaceBet = true;
//        repeatBtn.interactable = betOkBtn.interactable = clearBtn.interactable = doubleBtn.interactable = true;
//        isUserPlacedBets = false;
//        isBetConfirmed = false;
//        canPlaceBet = true;
//        commentTxt.text = commenstArray[1];
//        canStopTimer = false;
//        Debug.Log("timer started");
//        while (counter > 0)
//        {
//            if (canStopTimer) yield break;
//            timerTxt.text = counter.ToString();
//            currentTime = counter;
//            if (counter < 6)
//            {
//                repeatBtn.interactable = betOkBtn.interactable = clearBtn.interactable = doubleBtn.interactable = false;
//                canPlaceBet = false;
//                if (!isBetConfirmed)
//                {
//                    OnBetsOk();
//                }
//            }
//            yield return new WaitForSeconds(1f);
//            counter--;
//        }
//        currentTime = 0;
//        timerTxt.text = 0.ToString();
//    }

//    IEnumerator OnRoundEnd(weelNumbers o)
//    {
//        yield return new WaitUntil(() => currentTime == 0);
//        var DCW = FindObjectOfType<TripleFunWheel>();
//      //  DCW.Spin(o.inner_win_no, o.outer_win_no);
//        inner_wheelValue = o.inner_win_no;
//        outer_win_wheelValue = o.outer_win_no;

//        if (singleWins_list.Count > 6)
//        {
//            singleWins_list.RemoveAt(0);
//            singleWins_list.Add(o.outer_win_no.ToString());
//        }
//        else
//        {
//            singleWins_list.Add(o.outer_win_no.ToString());
//        }
//        if (DoubleWins_List.Count > 6)
//        {
//            DoubleWins_List.RemoveAt(0);
//            DoubleWins_List.Add(o.outer_win_no.ToString() + o.inner_win_no.ToString());
//        }
//        else
//        {
//            DoubleWins_List.Add(o.outer_win_no.ToString() + o.inner_win_no.ToString());
//        }

//        for (int i = 0; i < PreviousSingleWinList.Count; i++)
//        {
//            PreviousSingleWinList[i].text = singleWins_list[i];
//        }

//        for (int i = 0; i < PreviousDoubleWinList.Count; i++)
//        {
//            PreviousDoubleWinList[i].text = DoubleWins_List[i];
//        }

//        // DCW.OnSpinComplete += () =>
//        // {
//        //     // int doubleWin = int.Parse(o.outer_win_no.ToString() + o.inner_win_no.ToString());
//        //     // StartCoroutine(WinAnimation(doubleWin,o.inner_win_no));
//        //     DCW.OnSpinComplete += mySpinComplete;
//        // };
//        DCW.OnSpinComplete += mySpinComplete;
//    }

//    void mySpinComplete()
//    {
//        int doubleWin = int.Parse(outer_win_wheelValue.ToString() + inner_wheelValue.ToString());
//        // StartCoroutine(WinAnimation(doubleWin,inner_wheelValue));
//        StartCoroutine(WinAnimation(doubleWin, outer_win_wheelValue));
//        var DCW = FindObjectOfType<TripleFunWheel>();
//        DCW.OnSpinComplete -= mySpinComplete;
//    }
//    #endregion

//    IEnumerator WinAnimation(int doubleWinNumber, int singleWinNumber)
//    {

//        Debug.Log("Win Animation2");
//        BettingButtonInteractablity(false);
//        for (int i = 0; i < 5; i++)
//        {
//            doubleGridBtns[doubleWinNumber].interactable = true;
//            singleGridBtns[singleWinNumber].interactable = true;
//            yield return new WaitForSeconds(0.5f);
//            doubleGridBtns[doubleWinNumber].interactable = false;
//            singleGridBtns[singleWinNumber].interactable = false;
//            yield return new WaitForSeconds(0.5f);
//        }
//        singleGridBtns[singleWinNumber].interactable = true;
//        doubleGridBtns[doubleWinNumber].interactable = false;
//        ResetAllBets();
//        UpdateUi();
//    }
//    void BettingButtonInteractablity(bool status)
//    {
//        foreach (var card in doubleGridBtns)
//        {
//            card.interactable = status;
//        }
//        foreach (var card in singleGridBtns)
//        {
//            card.interactable = status;
//        }

//    }

//    private void UpdateUi()
//    {
//        #region BETS
//        for (int i = 0; i < doubleGridBtns.Length; i++)
//        {
//            string v = Double_Bets_Container[i] == 0 ? $"{i}" : Double_Bets_Container[i].ToString();
//            if (Double_Bets_Container[i] != 0)
//            {
//                doubleGridBtns[i].GetComponentInChildren<TextMeshProUGUI>().color = Color.white;

//            }
//            else
//            {
//                doubleGridBtns[i].GetComponentInChildren<TextMeshProUGUI>().color = Color.black;

//            }
//            doubleGridBtns[i].GetComponentInChildren<TextMeshProUGUI>().text = v;

//        }
//        for (int i = 0; i < singleGridBtns.Length; i++)
//        {
//            string v = Single_Bets_Container[i] == 0 ? $"{i}" : Single_Bets_Container[i].ToString();
//            if (Single_Bets_Container[i] != 0)
//            {
//                singleGridBtns[i].GetComponentInChildren<TextMeshProUGUI>().color = Color.white;

//            }
//            else
//            {
//                singleGridBtns[i].GetComponentInChildren<TextMeshProUGUI>().color = Color.black;


//            }
//            singleGridBtns[i].GetComponentInChildren<TextMeshProUGUI>().text = v;
//        }


//        totalBets = Double_Bets_Container.Sum() + Single_Bets_Container.Sum();
//        #endregion
//        balanceTxt.text = balance.ToString();
//        totalBetsTxt.text = totalBets.ToString();
//       // LocalDatabase.data.balance = balance;
//        LocalDatabase.SaveGame();
//    }

//    private void ResetAllBets()
//    {
//        for (int i = 0; i < Double_Bets_Container.Length; i++)
//        {
//            Previous_Double_Bets_Container[i] = Double_Bets_Container[i];
//        }
//        for (int i = 0; i < Single_Bets_Container.Length; i++)
//        {
//            Previous_Single_Bets_Container[i] = Single_Bets_Container[i];
//        }

//        Double_Bets_Container = new int[100];
//        Single_Bets_Container = new int[10];
//        totalBets = 0;
//        isUserPlacedBets = false;
//        canPlaceBet = true;
//        isTimeUp = false;

//        //UI
//        BettingButtonInteractablity(true);
//        clearBtn.interactable = true;
//        betOkBtn.interactable = true;
//        doubleBtn.interactable = true;
//        repeatBtn.interactable = true;
//        UpdateUi();
//    }

//    //public override void Hide()
//    //{
//    //    base.Hide();
//    //    RemoveSocketListners();
//    //    ResetAllBets();

//    //}
//    //void RemoveSocketListners()
//    //{
//    //    SocketRequest.intance.RemoveListners(Constant.OnWinNo);
//    //    SocketRequest.intance.RemoveListners(Constant.OnTimerStart);
//    //    SocketRequest.intance.RemoveListners(Constant.OnDissconnect);
//    //    SocketRequest.intance.RemoveListners(Constant.OnWinAmount);
//    //    SocketRequest.intance.RemoveListners(Constant.OnTimeUp);
//    //}

//    class roundInfo
//    {
//        public int balance;
//        public int gametimer;
//        public List<weelNumbers> previousWinData;
//    }
//    class weelNumbers

//    {
//        // public int single_winno;
//        // public int double_winno;
//        public int outer_win_no;
//        public int inner_win_no;
//    }
//    public class bets
//    {
//        public string playerId;
//        public int gameId;
//        public int points;
//        public int[] single, @double;
//    }

//}

//namespace DoubleChanceClasses
//{
//    public class OnWinAmount
//    {
//        public long RoundCount;
//        public int win_no;
//        public int winPoints;
//    }
//}
