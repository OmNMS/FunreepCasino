using AndarBahar.Utility;
using Shared;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using Newtonsoft.Json;
using DG.Tweening;
using AndarBahar.Gameplay;
using AndarBahar.UI;
using UnityEngine.SceneManagement;
using AndarBahar.ServerStuff;
using UnityEngine.AddressableAssets;
using UnityEngine.Networking;
using System.IO;

namespace AndarBahar.UI
{
    public class AndarBahar_UiHandler : MonoBehaviour
    {
        public bool roundActive;
        public static AndarBahar_UiHandler Instance;
        public TMP_Text Text_A_bet, Text_2_bet, Text_3_bet, Text_4_bet,Text_5_bet, Text_6_bet, Text_7_bet, Text_8_bet, Text_9_bet, Text_10_bet, Text_J_bet, Text_Q_bet, Text_K_bet, Text_Heart_bet, Text_Diamond_bet, Text_Spade_bet, Text_Club_bet, Text_Red_bet, Text_Black_bet, Text_A_6_bet, Text_Seven_bet, Text_8_K_bet, Text_Bahar_bet, Text_Andar_bet;
        public GameObject[] chipimg;
        public Button Left_10, Left_25, Left_50, Left_100, Right_500, Right_1000, Right_5000, Right_10000;
        public Button CancelBet_Btn, TakeBet_Btn;//BetBtn,
        public Button sidemenuBtn;
        public float balance;
        public  int totalBets;
        public Text andarBets_Txt;
        public Text baharBets_Txt;
        int oneToFiveBets;
        int sixToTenBets;
        int elevenToFifteenBets;
        int sixteenToTwentyFiveBets;
        int twentySixToThirtyBets;
        int thirtyOneToThirtyFiveBets;
        int thirtySixToFourtyBets;
        int fourtyAndMoreBets;
        public Text TotalBetsValueTxt;
        public Text balanceTxt;
        // public TMP_Text userNameTxt;
        public Text WinAmountTxt;
        // bool isSideMenuOpen;
        // public GameObject sideMenu;
        // string AndarTxt;
        public Chip currentChip;
        // public GameObject mainUnite;
        public AndarBahar_Timer timer;
        // public TMP_Text winnerCard;
        // public Sprite startBettingSprite;
        // public Sprite stopBettingSprite;
        public GameObject startStopImgHolder;
        public Transform startingPoint;
        public Transform middlePoint;
        public Transform finishingPoint;
        public iTween.EaseType easeType;
        public float transitionTime = 1, delay = .1f;
        public GameObject messagePanel;
        public TMP_Text messageTxt;
       
        public Text timerTxt, WinText;
        public TMP_Text Total_Bets_Text;
        public Button betOkBtn;
        int winningAmount, currentTime = 0;
        public bool isTimeUp, isBetConfirmed, canPlaceBet, canStopTimer;
        // public int andarBets, baharBets, HeartSymbolBets, DiamondSymbolBets, SpadeSymbolBets, ClubSymbolBets, 
        // RedCardBets, BlackCardBets, AtoSixBets, SeventhCardBets, EightToKBets, DirectCardBets;
        public GameObject spin_AudioSource, timer_AudioSource;
        public SceneHandler sceneHandler;
        public bool ready;
        public Image yellowstripe;
        public GameObject[] highlight;
        public bool limit;

        public bool IsEnoughBalanceAvailable()
        {
            return balance > (int)currentChip;
        }
        int Total_Bets, Andar_bets, Bahar_bets, A_bets, _2_bets,_3_bets,_4_bets,_5_bets,_6_bets,_7_bets,_8_bets,_9_bets,_10_bets,J_bets,Q_bets,K_bets,Diamond_bets,Spade_bets,Club_bets,Heart_bets,Red_bets,Black_bets,A_6_bets,Seven_bets,_8_k_bets;
        public int Andar_bet_Allowed = 0; //if 1 andar bet allowed and bahar bet not allowed and if 2 bahar bet allowerd and not andar...
        
        public void AddBets(Spots spots)//here bets will add and displayed and total will be created reall imp place 
        {
            if ((balance > 0 && balance >= (int)currentChip) && !isBetConfirmed && AndarBahar_RoundWinnerHandlercs.Instance.winPoint == 0)//AnwinningAmount == 0)
            {
                balance -= (int)currentChip;  
                Total_Bets += (int)currentChip;
                Debug.Log("Add bets Current Chip: " +currentChip.ToString()  );
                ready = true;
                Debug.Log("Balance: "+balance );
                switch (spots)
                {
                    case Spots.A:
                       if(A_bets+(int)currentChip > 1000)
                       {
                            limit =true;
                            break;
                        }
                        A_bets += (int)currentChip;
                        Text_A_bet.text = A_bets.ToString();
                        limit =false;
                        break;
                    case Spots._2:
                        if(_2_bets+(int)currentChip > 1000)
                       {
                            limit =true;
                            break;
                        }
                        _2_bets += (int)currentChip;
                        Text_2_bet.text = _2_bets.ToString();
                        limit =false;
                        break;
                    case Spots._3:
                        if(_3_bets+(int)currentChip > 1000)
                       {
                            limit =true;
                            break;
                        }
                        _3_bets += (int)currentChip;
                        Text_3_bet.text = _3_bets.ToString();
                        limit =false;
                        break;
                    case Spots._4:
                        if(_4_bets+(int)currentChip > 1000)
                       {
                            limit =true;
                            break;
                        }
                        _4_bets += (int)currentChip;
                        Text_4_bet.text = _4_bets.ToString();
                        limit =false;
                        break;
                    case Spots._5:
                        if(_5_bets+(int)currentChip > 1000)
                       {
                            limit =true;
                            break;
                        }
                        _5_bets += (int)currentChip;
                        Text_5_bet.text = _5_bets.ToString();
                        limit =false;
                        break;
                    case Spots._6:
                        if(_6_bets+(int)currentChip > 1000)
                       {
                            limit =true;
                            break;
                        }
                        _6_bets += (int)currentChip;
                        Text_6_bet.text = _6_bets.ToString();
                        break;
                    case Spots._7:
                        if(_7_bets+(int)currentChip > 1000)
                       {
                            limit =true;
                            break;
                        }
                        _7_bets += (int)currentChip;
                        Text_7_bet.text = _7_bets.ToString();
                        break;
                    case Spots._8:
                        if(_8_bets+(int)currentChip > 1000)
                       {
                            limit =true;
                            break;
                        }
                        _8_bets += (int)currentChip;
                        Text_8_bet.text = _8_bets.ToString();
                        break;
                    case Spots._9:
                        if(_9_bets+(int)currentChip > 1000)
                       {
                            limit =true;
                            break;
                        }
                        _9_bets += (int)currentChip;
                        Text_9_bet.text = _9_bets.ToString();
                        break;
                    case Spots._10:
                        if(_10_bets+(int)currentChip > 1000)
                       {
                            limit =true;
                            break;
                        }
                        _10_bets += (int)currentChip;
                        Text_10_bet.text = _10_bets.ToString();
                        break;
                    case Spots.J:
                        if(J_bets+(int)currentChip > 1000)
                       {
                            limit =true;
                            break;
                        }
                        J_bets += (int)currentChip;
                        Text_J_bet.text = J_bets.ToString();
                        limit =false;
                        break;
                    case Spots.Q:
                        if(Q_bets+(int)currentChip > 1000)
                       {
                            limit =true;
                            break;
                        }
                        Q_bets += (int)currentChip;
                        Text_Q_bet.text = Q_bets.ToString();
                        limit =false;
                        break;
                    case Spots.K:
                        if(K_bets+(int)currentChip > 1000)
                       {
                            limit =true;
                            break;
                        }
                        K_bets += (int)currentChip;
                        Text_K_bet.text = K_bets.ToString();
                        limit =false;
                        break;
                    case Spots.Diamond:
                        if(Diamond_bets+(int)currentChip > 1000)
                       {
                            limit =true;
                            break;
                        }
                        Diamond_bets += (int)currentChip;
                        Text_Diamond_bet.text = Diamond_bets.ToString();
                        limit =false;
                        break;
                    case Spots.Spade:
                        if(Spade_bets+(int)currentChip > 1000)
                       {
                            limit =true;
                            break;
                        }
                        Spade_bets += (int)currentChip;
                        Text_Spade_bet.text = Spade_bets.ToString();
                        limit =false;
                        break;
                    case Spots.Club:
                        if(Club_bets+(int)currentChip > 1000)
                       {
                            limit =true;
                            break;
                        }
                        Club_bets += (int)currentChip;
                        Text_Club_bet.text = Club_bets.ToString();
                        limit =false;
                        break;
                    case Spots.Heart:
                        if(Heart_bets+(int)currentChip > 1000)
                       {
                            limit =true;
                            break;
                        }
                        Heart_bets += (int)currentChip;
                        Text_Heart_bet.text = Heart_bets.ToString();
                        limit =false;
                        break;
                    case Spots.Red:
                        if(Red_bets+(int)currentChip > 1000)
                       {
                            limit =true;
                            break;
                        }
                        Red_bets += (int)currentChip;
                        Text_Red_bet.text = Red_bets.ToString();
                        limit =false;
                        break;
                    case Spots.Black:
                        if(Black_bets+(int)currentChip > 1000)
                       {
                            limit =true;
                            break;
                        }
                        Black_bets += (int)currentChip;
                        Text_Black_bet.text = Black_bets.ToString();
                        limit =false;
                        break;
                    case Spots.A_6:
                        if(A_6_bets+(int)currentChip > 1000)
                       {
                            limit =true;
                            break;
                        }
                        A_6_bets += (int)currentChip;
                        Text_A_6_bet.text = A_6_bets.ToString();
                        limit =false;
                        break;
                    case Spots.Seven:
                        if(Seven_bets+(int)currentChip > 1000)
                       {
                            limit =true;
                            break;
                        }
                        Seven_bets += (int)currentChip;
                        Text_Seven_bet.text = Seven_bets.ToString();
                        limit =false;
                        break;
                    case Spots._8_k:
                        if(_8_k_bets+(int)currentChip > 1000)
                       {
                            limit =true;
                            break;
                        }
                        _8_k_bets += (int)currentChip;
                        Text_8_K_bet.text = _8_k_bets.ToString();
                        limit =false;
                        break;
                    case Spots.Bahar:
                        if(Bahar_bets+(int)currentChip > 1000)
                       {
                            limit =true;
                            break;
                        }
                        Bahar_bets += (int)currentChip;
                        Andar_bet_Allowed = 2;
                        Text_Bahar_bet.text = Bahar_bets.ToString();
                        limit =false;
                        break;
                    case Spots.Andar:
                        if(Andar_bets+(int)currentChip > 1000)
                       {
                            limit =true;
                            break;
                        }
                        Andar_bets += (int)currentChip;
                        Andar_bet_Allowed = 1;
                        Text_Andar_bet.text = Andar_bets.ToString();
                        limit =false;
                        break;
                    default:
                        Debug.Log("Invalid Spot How did this occur !!!!! ???" +spots.ToString()); 
                        break;
                }

                UpdateUI();
                CancelBet_Btn.gameObject.SetActive(true);
                
            }
            // else if(AndarBahar_RoundWinnerHandlercs.Instance.winPoint != 0)
            // {
            //     AndroidToastMsg.ShowAndroidToastMessage("Please collect the winnning amount first");
                
            //}
            else
            {
                if(AndarBahar_RoundWinnerHandlercs.Instance.winPoint > 0)
                {
                    AndroidToastMsg.ShowAndroidToastMessage("Please collect the winnning amount first");
                }
                if((balance > 0 && balance >= (int)currentChip))
                {
                    AndroidToastMsg.ShowAndroidToastMessage("Not Enough balance");
                }
                canPlaceBet = false;
                ready = false;
            }
        }

        private void Start()
        {
            //Application.targetFrameRate = 60;
            WinAmountTxt.text = 0.ToString();
            //ChipImgSelect(0);
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
            balance = PlayerPrefs.GetFloat("points");
            balanceTxt.text = balance.ToString();
            Debug.Log("Balance:"+balance );
            totalBets = 0;
            //isSideMenuOpen = false;
            // sidemenuBtn.onClick.AddListener(() =>
            // {
            //     if (sideMenu.activeSelf)
            //     {
            //         sideMenu.gameObject.SetActive(false);
            //         return;
            //     }
            //     sideMenu.gameObject.SetActive(true);
            // });
            //messagePanel.SetActive(true);
            if(PlayerPrefs.GetString("andarstarted")=="true")
            {
                savebinary.LoadPlayerAndar();
                PlayerPrefs.SetString("andarstarted","false");
            }
            
            // if ((PlayerPrefs.GetString("andarpaused") == "true") ||Andarwithin.confirmed == true)
            // {
            //     Debug.Log("Bets were confirmed");
            //     restorewithn();
            //     isBetConfirmed = true;
            //     canPlaceBet =false;
            // }
            Debug.Log("////////////////////////////////////////////////"+Andarwithin.confirmed);
            if(Andarwithin.confirmed == true)
            {
                Debug.Log("Bets were confirmed");
                restorewithn();
                isBetConfirmed = true;
                canPlaceBet =false;
            }
            else
            {
                Debug.Log("Do Not reach here");
                isBetConfirmed = false;
                canPlaceBet =true;
            }
            currentChip = Chip.Chip10;
            
            //  Lchit10Btn.isOn = true;
            // Left_10.Select();
            SoundManager.instance.PlayBackgroundMusic();
            if(AndarBaharlast.winamount >0)
            {
                WinAmountTxt.text = AndarBaharlast.winamount.ToString();
                AndarBahar_RoundWinnerHandlercs.Instance.winPoint = int.Parse(WinAmountTxt.text);
                AndarBahar_RoundWinnerHandlercs.Instance.winround = AndarBahar_RoundWinnerHandlercs.Instance.winPoint + balance;
            }
            //restorewithn();
            //sceneHandler = GameObject.Find("SceneHandler").GetComponent<SceneHandler>();
            AddListeners();
        }

        bool isPause;
        void OnApplicationPause(bool hasFocus)
        {
            isPause = hasFocus;
           
            //if (Application.isEditor) return;
            // {
            if(isPause)
            {
                Debug.Log("Focus True:" +hasFocus);
                if (isBetConfirmed)
                {
                    StopAllCoroutines();
                    storewithin();
                    //AndarBahar_ServerResponse.Instance.socketoff();
                    PlayerPrefs.SetString("andarpaused","true");
                    PlayerPrefs.SetInt("andarrounds",timer.andarround);
                    StopCoroutine(timer.Countdown());
                }
                else{
                    Andarwithin.confirmed = false;
                }
                // AndarBahar_ServerResponse.Instance.socketoff();
                //SceneManager.LoadScene(1);
                //AndarBahar_ServerRequest.instance.socket.Emit(Utility.Events.onleaveRoom); //leave room;
            }
            if (!isPause)
            {
                
                Debug.Log("Focus False:" +hasFocus);
                PlayerPrefs.SetInt("areload",1);
                AndarBahar_ServerRequest.instance.socket.Emit(Utility.Events.onleaveRoom);
                // HomeScript.Instance.AndarBaharBtn();
                // AndarBahar_ServerResponse.Instance.socketOn();
                // AndarBahar_ServerResponse.Instance.socketoff();
                // SceneManager.LoadScene(SceneManager.GetActiveScene().name); 
                // sceneHandler.loadAddressableScene(SceneManager.GetActiveScene().name);   
                //Addressables.LoadSceneAsync("Anadar_Bahar1", UnityEngine.SceneManagement.LoadSceneMode.Single, true);         
            }
            // }
        }
        string setplayeroffline = "http://139.59.92.165:5000/user/SetplayerOffline";
        public IEnumerator settingoffline()
        {
            WWWForm form = new WWWForm();
            string playername = "GK"+PlayerPrefs.GetString("email");
            form.AddField("email", playername);
            using (UnityWebRequest www = UnityWebRequest.Post(setplayeroffline, form))
            {
                yield return www.SendWebRequest();

                if (www.result != UnityWebRequest.Result.Success)
                {
                    Debug.Log(www.error);
                }
                else{
                    Debug.Log("Offline//Offline//Offline//Offline//Offline//Offline//Offline//Offline//Offline//Offline//");
                }
            }
        }
        private void OnApplicationQuit() {
            PlayerPrefs.SetString("andarpaused","false");
            //StartCoroutine(settingoffline());
        }

        public void storewithin()
        {
            Andarwithin.confirmed = isBetConfirmed;
            Debug.Log("boolboolboolbool"+Andarwithin.confirmed);
            Andarwithin.deviceid = PlayerPrefs.GetString("email");
            Andarwithin.winamount = int.Parse(WinAmountTxt.text);
            Andarwithin.A = A_bets;
            Andarwithin.twob =_2_bets ;
            Andarwithin.threeb = _3_bets;
            Andarwithin.fourb =_4_bets ;
            Andarwithin.fiveb = _5_bets;
            Andarwithin.sixb =_6_bets ;
            Andarwithin.sevenb = _7_bets;
            Andarwithin.eightb =_8_bets ;
            Andarwithin.nineb = _9_bets;
            Andarwithin.tenb =_10_bets ;
            Andarwithin.jb = J_bets;
            Andarwithin.qb =Q_bets ;
            Andarwithin.kb = K_bets;
            Andarwithin.diamondb = Diamond_bets;
            Andarwithin.spadeb = Spade_bets;
            Andarwithin.cloverb = Club_bets;
            Andarwithin.heartb = Heart_bets;
            Andarwithin.redb = Red_bets;
            Andarwithin.blackb = Black_bets;
            Andarwithin.atosix = A_6_bets;
            Andarwithin.seven = Seven_bets;
            Andarwithin.eighttok = _8_k_bets;
            Andarwithin.andar = Andar_bets;
            Andarwithin.bahar = Bahar_bets;


        }
        public GameObject chipimage;
        public List<GameObject> cloned;
        public void restorewithn()
        {
            Debug.Log("Data is restored");
            if(Andarwithin.A+Andarwithin.twob+Andarwithin.threeb+Andarwithin.fourb+Andarwithin.fiveb+Andarwithin.sixb+Andarwithin.sevenb+Andarwithin.eightb+Andarwithin.nineb+Andarwithin.tenb+Andarwithin.jb+Andarwithin.qb+Andarwithin.kb+Andarwithin.diamondb+Andarwithin.spadeb+Andarwithin.cloverb+Andarwithin.heartb+Andarwithin.blackb+Andarwithin.redb+Andarwithin.atosix+Andarwithin.seven+Andarwithin.eighttok+Andarwithin.andar+Andarwithin.bahar >balance)
            {
                Debug.Log("Insufficent balance");
                WinText.text ="Insufficent balance";
                return;
            }
            //AndarBahar_RoundWinnerHandlercs.Instance.winPoint = Andarwithin.winamount;
            A_bets =Andarwithin.A;
            _2_bets = Andarwithin.twob ;
            _3_bets = Andarwithin.threeb;
            _4_bets = Andarwithin.fourb ;
            _5_bets = Andarwithin.fiveb;
            _6_bets = Andarwithin.sixb ;
            _7_bets = Andarwithin.sevenb;
            _8_bets = Andarwithin.eightb ;
            _9_bets = Andarwithin.nineb;
            _10_bets = Andarwithin.tenb ;
            J_bets  = Andarwithin.jb;
            Q_bets = Andarwithin.qb ;
            K_bets = Andarwithin.kb;
            Diamond_bets = Andarwithin.diamondb;
            Spade_bets = Andarwithin.spadeb;
            Club_bets = Andarwithin.cloverb;
            Heart_bets = Andarwithin.heartb;
            Black_bets = Andarwithin.blackb;
            Red_bets = Andarwithin.redb;
            A_6_bets = Andarwithin.atosix;
            Seven_bets = Andarwithin.seven;
            _8_k_bets = Andarwithin.eighttok;
            Andar_bets = Andarwithin.andar;
            Bahar_bets = Andarwithin.bahar;
           //Transform chipcontent = GameObject.Find("ChipContainer").GetComponent<Transform>();
           //Transform poistion = 
           //WinAmountTxt.text = AndarBahar_RoundWinnerHandlercs.Instance.winPoint.ToString();
            if(A_bets > 0)
            {
                Text_A_bet.text = A_bets.ToString();
                GameObject clone = Instantiate(chipimage,AndarBahar_ChipController.Instance.GetChipParent(Spots.A).position,Quaternion.identity,AndarBahar_ChipController.Instance.GetChipParent(Spots.A));
                cloned.Add(clone);
                Total_Bets += A_bets;
                //AndarBahar_ChipController.Instance.CreateChip(AndarBahar_ChipController.Instance.GetChipParent(Spots.A).gameObject.transform.position,Chip.Chip10)
                
            }
            if(_2_bets > 0)
            {
                Text_2_bet.text = _2_bets.ToString();
                GameObject clone = Instantiate(chipimage,AndarBahar_ChipController.Instance.GetChipParent(Spots._2).position,Quaternion.identity,AndarBahar_ChipController.Instance.GetChipParent(Spots._2));
                cloned.Add(clone);
                Total_Bets += _2_bets;
            }
            if(_3_bets > 0)
            {
                GameObject clone = Instantiate(chipimage,AndarBahar_ChipController.Instance.GetChipParent(Spots._3).position,Quaternion.identity,AndarBahar_ChipController.Instance.GetChipParent(Spots._3));
                cloned.Add(clone);
                Text_3_bet.text = _3_bets.ToString();
                Total_Bets +=_3_bets;
            }
            if(_4_bets > 0)
            {
                GameObject clone = Instantiate(chipimage,AndarBahar_ChipController.Instance.GetChipParent(Spots._4).position,Quaternion.identity,AndarBahar_ChipController.Instance.GetChipParent(Spots._4));
                cloned.Add(clone);
                Text_4_bet.text = _4_bets.ToString();
                Total_Bets +=_4_bets;
            }
            if(_5_bets > 0)
            {
                GameObject clone = Instantiate(chipimage,AndarBahar_ChipController.Instance.GetChipParent(Spots._5).position,Quaternion.identity,AndarBahar_ChipController.Instance.GetChipParent(Spots._5));
                cloned.Add(clone);
                Text_5_bet.text = _5_bets.ToString();
                Total_Bets +=_5_bets;
            }
            if(_6_bets > 0)
            {
                GameObject clone = Instantiate(chipimage,AndarBahar_ChipController.Instance.GetChipParent(Spots._6).position,Quaternion.identity,AndarBahar_ChipController.Instance.GetChipParent(Spots._6));
                cloned.Add(clone);
                Text_6_bet.text = _6_bets.ToString();
                Total_Bets +=_6_bets;
            }
            if(_7_bets > 0)
            {
                GameObject clone = Instantiate(chipimage,AndarBahar_ChipController.Instance.GetChipParent(Spots._7).position,Quaternion.identity,AndarBahar_ChipController.Instance.GetChipParent(Spots._7));
                cloned.Add(clone);
                Text_7_bet.text = _7_bets.ToString();
                Total_Bets +=_7_bets;
            }
            if(_8_bets > 0)
            {
                GameObject clone = Instantiate(chipimage,AndarBahar_ChipController.Instance.GetChipParent(Spots._8).position,Quaternion.identity,AndarBahar_ChipController.Instance.GetChipParent(Spots._8));
                cloned.Add(clone);
                Text_8_bet.text = _8_bets.ToString();
                Total_Bets +=_8_bets;
            }
            
            if(_9_bets > 0)
            {
                GameObject clone = Instantiate(chipimage,AndarBahar_ChipController.Instance.GetChipParent(Spots._9).position,Quaternion.identity,AndarBahar_ChipController.Instance.GetChipParent(Spots._9));
                cloned.Add(clone);
                Text_9_bet.text = _9_bets.ToString();
                Total_Bets +=_9_bets;
            }
            if(_10_bets > 0)
            {
                GameObject clone = Instantiate(chipimage,AndarBahar_ChipController.Instance.GetChipParent(Spots._10).position,Quaternion.identity,AndarBahar_ChipController.Instance.GetChipParent(Spots._10));
                cloned.Add(clone);
                Text_10_bet.text = _10_bets.ToString();
                Total_Bets += _10_bets;
            }
            if(J_bets > 0)
            {
                GameObject clone = Instantiate(chipimage,AndarBahar_ChipController.Instance.GetChipParent(Spots.J).position,Quaternion.identity,AndarBahar_ChipController.Instance.GetChipParent(Spots.J));
                cloned.Add(clone);
                Text_J_bet.text = J_bets.ToString();
                Total_Bets +=J_bets;
            }
            if(Q_bets > 0)
            {
                GameObject clone = Instantiate(chipimage,AndarBahar_ChipController.Instance.GetChipParent(Spots.Q).position,Quaternion.identity,AndarBahar_ChipController.Instance.GetChipParent(Spots.Q));
                cloned.Add(clone);
                Text_Q_bet.text = Q_bets.ToString();
                Total_Bets +=Q_bets;
            }
            
            if(K_bets > 0)
            {
                GameObject clone = Instantiate(chipimage,AndarBahar_ChipController.Instance.GetChipParent(Spots.K).position,Quaternion.identity,AndarBahar_ChipController.Instance.GetChipParent(Spots.K));
                cloned.Add(clone);
                Text_K_bet.text = K_bets.ToString();
                Total_Bets +=K_bets;
            }

            if(Diamond_bets> 0)
            {
                GameObject clone = Instantiate(chipimage,AndarBahar_ChipController.Instance.GetChipParent(Spots.Diamond).position,Quaternion.identity,AndarBahar_ChipController.Instance.GetChipParent(Spots.Diamond));
                cloned.Add(clone);
                Text_Diamond_bet.text = Diamond_bets.ToString();
                Total_Bets +=Diamond_bets;
            }
            
            if(Spade_bets > 0)
            {
                GameObject clone = Instantiate(chipimage,AndarBahar_ChipController.Instance.GetChipParent(Spots.Spade).position,Quaternion.identity,AndarBahar_ChipController.Instance.GetChipParent(Spots.Spade));
                cloned.Add(clone);
                Text_Spade_bet.text = Spade_bets.ToString();
                Total_Bets +=Spade_bets ;
            }
            
            if(Club_bets > 0)
            {
                GameObject clone = Instantiate(chipimage,AndarBahar_ChipController.Instance.GetChipParent(Spots.Club).position,Quaternion.identity,AndarBahar_ChipController.Instance.GetChipParent(Spots.Club));
                cloned.Add(clone);
                Text_Club_bet.text = Club_bets.ToString();
                Total_Bets+=Club_bets;
            }
            
            if(Heart_bets > 0)
            {
                GameObject clone = Instantiate(chipimage,AndarBahar_ChipController.Instance.GetChipParent(Spots.Heart).position,Quaternion.identity,AndarBahar_ChipController.Instance.GetChipParent(Spots.Heart));
                cloned.Add(clone);
                Text_Heart_bet.text = Heart_bets.ToString();
                Total_Bets +=Heart_bets;
            }
            
            if(Red_bets > 0)
            {
                GameObject clone = Instantiate(chipimage,AndarBahar_ChipController.Instance.GetChipParent(Spots.Red).position,Quaternion.identity,AndarBahar_ChipController.Instance.GetChipParent(Spots.Red));
                cloned.Add(clone);
                Text_Red_bet.text = Red_bets.ToString();
                Total_Bets +=Red_bets;
            }
            
            if(Black_bets > 0)
            {
                GameObject clone = Instantiate(chipimage,AndarBahar_ChipController.Instance.GetChipParent(Spots.Black).position,Quaternion.identity,AndarBahar_ChipController.Instance.GetChipParent(Spots.Black));
                cloned.Add(clone);
                Text_Black_bet.text = Black_bets.ToString();
                Total_Bets +=Black_bets;
            }
            if(A_6_bets > 0)
            {
                GameObject clone = Instantiate(chipimage,AndarBahar_ChipController.Instance.GetChipParent(Spots.A_6).position,Quaternion.identity,AndarBahar_ChipController.Instance.GetChipParent(Spots.A_6));
                cloned.Add(clone);
                Text_A_6_bet.text = A_6_bets.ToString();
                Total_Bets +=A_6_bets;
            }
            
            
            if(Seven_bets > 0)
            {
                GameObject clone = Instantiate(chipimage,AndarBahar_ChipController.Instance.GetChipParent(Spots.Seven).position,Quaternion.identity,AndarBahar_ChipController.Instance.GetChipParent(Spots.Seven));
                cloned.Add(clone);
                Text_Seven_bet.text = Seven_bets.ToString();
                Total_Bets +=Seven_bets;
            }
            
            if(_8_k_bets > 0)
            {
                GameObject clone = Instantiate(chipimage,AndarBahar_ChipController.Instance.GetChipParent(Spots._8_k).position,Quaternion.identity,AndarBahar_ChipController.Instance.GetChipParent(Spots._8_k));
                cloned.Add(clone);
                Text_8_K_bet.text = _8_k_bets.ToString();
                Total_Bets +=_8_k_bets;
            }
            
            if(Bahar_bets > 0)
            {
                GameObject clone = Instantiate(chipimage,AndarBahar_ChipController.Instance.GetChipParent(Spots.Bahar).position,Quaternion.identity,AndarBahar_ChipController.Instance.GetChipParent(Spots.Bahar));
                cloned.Add(clone);
                Text_Bahar_bet.text = Bahar_bets.ToString();
                Total_Bets +=Bahar_bets;
            }
            
            if(Andar_bets > 0)
            {
                GameObject clone = Instantiate(chipimage,AndarBahar_ChipController.Instance.GetChipParent(Spots.Andar).position,Quaternion.identity,AndarBahar_ChipController.Instance.GetChipParent(Spots.Andar));
                cloned.Add(clone);
                Text_Andar_bet.text = Andar_bets.ToString();
                Total_Bets +=Andar_bets;
            }
            if(!AndarBahar_RoundWinnerHandlercs.Instance.started)
            {
                balance = balance - Total_Bets;
            }   
            
            UpdateUI();
            
        }
        public void restore()
        {
    
            Debug.Log("Data is restored");
            //AndarBahar_RoundWinnerHandlercs.Instance.winPoint = Andarwithin.winamount;
            A_bets =AndarBaharlast.A;
            _2_bets = AndarBaharlast.twob ;
            _3_bets = AndarBaharlast.threeb;
            _4_bets = AndarBaharlast.fourb ;
            _5_bets = AndarBaharlast.fiveb;
            _6_bets = AndarBaharlast.sixb ;
            _7_bets = AndarBaharlast.sevenb;
            _8_bets = AndarBaharlast.eightb ;
            _9_bets = AndarBaharlast.nineb;
            _10_bets = AndarBaharlast.tenb ;
            J_bets  = AndarBaharlast.jb;
            Q_bets = AndarBaharlast.qb ;
            K_bets = AndarBaharlast.kb;
            Diamond_bets = AndarBaharlast.diamondb;
            Spade_bets = AndarBaharlast.spadeb;
            Club_bets = AndarBaharlast.cloverb;
            Heart_bets = AndarBaharlast.heartb;
            Black_bets = AndarBaharlast.blackb;
            Red_bets = AndarBaharlast.redb;
            A_6_bets = AndarBaharlast.atosix;
            Seven_bets = AndarBaharlast.seven;
            _8_k_bets = AndarBaharlast.eighttok;
            Andar_bets = AndarBaharlast.andar;
            Bahar_bets = AndarBaharlast.bahar;
           //Transform chipcontent = GameObject.Find("ChipContainer").GetComponent<Transform>();
           //Transform poistion = 
           Debug.Log("AA"+AndarBaharlast.A);
           //WinAmountTxt.text = AndarBahar_RoundWinnerHandlercs.Instance.winPoint.ToString();
            if(A_bets > 0)
            {
                Text_A_bet.text = A_bets.ToString();
                GameObject clone = Instantiate(chipimage,AndarBahar_ChipController.Instance.GetChipParent(Spots.A).position,Quaternion.identity,AndarBahar_ChipController.Instance.GetChipParent(Spots.A));
                cloned.Add(clone);
                Total_Bets += A_bets;
                //AndarBahar_ChipController.Instance.CreateChip(AndarBahar_ChipController.Instance.GetChipParent(Spots.A).gameObject.transform.position,Chip.Chip10)
                
            }
            if(_2_bets > 0)
            {
                Text_2_bet.text = _2_bets.ToString();
                GameObject clone = Instantiate(chipimage,AndarBahar_ChipController.Instance.GetChipParent(Spots._2).position,Quaternion.identity,AndarBahar_ChipController.Instance.GetChipParent(Spots._2));
                cloned.Add(clone);
                Total_Bets += _2_bets;
            }
            if(_3_bets > 0)
            {
                GameObject clone = Instantiate(chipimage,AndarBahar_ChipController.Instance.GetChipParent(Spots._3).position,Quaternion.identity,AndarBahar_ChipController.Instance.GetChipParent(Spots._3));
                cloned.Add(clone);
                Text_3_bet.text = _3_bets.ToString();
                Total_Bets +=_3_bets;
            }
            if(_4_bets > 0)
            {
                GameObject clone = Instantiate(chipimage,AndarBahar_ChipController.Instance.GetChipParent(Spots._4).position,Quaternion.identity,AndarBahar_ChipController.Instance.GetChipParent(Spots._4));
                cloned.Add(clone);
                Text_4_bet.text = _4_bets.ToString();
                Total_Bets +=_4_bets;
            }
            if(_5_bets > 0)
            {
                GameObject clone = Instantiate(chipimage,AndarBahar_ChipController.Instance.GetChipParent(Spots._5).position,Quaternion.identity,AndarBahar_ChipController.Instance.GetChipParent(Spots._5));
                cloned.Add(clone);
                Text_5_bet.text = _5_bets.ToString();
                Total_Bets +=_5_bets;
            }
            if(_6_bets > 0)
            {
                GameObject clone = Instantiate(chipimage,AndarBahar_ChipController.Instance.GetChipParent(Spots._6).position,Quaternion.identity,AndarBahar_ChipController.Instance.GetChipParent(Spots._6));
                cloned.Add(clone);
                Text_6_bet.text = _6_bets.ToString();
                Total_Bets +=_6_bets;
            }
            if(_7_bets > 0)
            {
                GameObject clone = Instantiate(chipimage,AndarBahar_ChipController.Instance.GetChipParent(Spots._7).position,Quaternion.identity,AndarBahar_ChipController.Instance.GetChipParent(Spots._7));
                cloned.Add(clone);
                Text_7_bet.text = _7_bets.ToString();
                Total_Bets +=_7_bets;
            }
            if(_8_bets > 0)
            {
                GameObject clone = Instantiate(chipimage,AndarBahar_ChipController.Instance.GetChipParent(Spots._8).position,Quaternion.identity,AndarBahar_ChipController.Instance.GetChipParent(Spots._8));
                cloned.Add(clone);
                Text_8_bet.text = _8_bets.ToString();
                Total_Bets +=_8_bets;
            }
            
            if(_9_bets > 0)
            {
                GameObject clone = Instantiate(chipimage,AndarBahar_ChipController.Instance.GetChipParent(Spots._9).position,Quaternion.identity,AndarBahar_ChipController.Instance.GetChipParent(Spots._9));
                cloned.Add(clone);
                Text_9_bet.text = _9_bets.ToString();
                Total_Bets +=_9_bets;
            }
            if(_10_bets > 0)
            {
                GameObject clone = Instantiate(chipimage,AndarBahar_ChipController.Instance.GetChipParent(Spots._10).position,Quaternion.identity,AndarBahar_ChipController.Instance.GetChipParent(Spots._10));
                cloned.Add(clone);
                Text_10_bet.text = _10_bets.ToString();
                Total_Bets += _10_bets;
            }
            if(J_bets > 0)
            {
                GameObject clone = Instantiate(chipimage,AndarBahar_ChipController.Instance.GetChipParent(Spots.J).position,Quaternion.identity,AndarBahar_ChipController.Instance.GetChipParent(Spots.J));
                cloned.Add(clone);
                Text_J_bet.text = J_bets.ToString();
                Total_Bets +=J_bets;
            }
            if(Q_bets > 0)
            {
                GameObject clone = Instantiate(chipimage,AndarBahar_ChipController.Instance.GetChipParent(Spots.Q).position,Quaternion.identity,AndarBahar_ChipController.Instance.GetChipParent(Spots.Q));
                cloned.Add(clone);
                Text_Q_bet.text = Q_bets.ToString();
                Total_Bets +=Q_bets;
            }
            
            if(K_bets > 0)
            {
                GameObject clone = Instantiate(chipimage,AndarBahar_ChipController.Instance.GetChipParent(Spots.K).position,Quaternion.identity,AndarBahar_ChipController.Instance.GetChipParent(Spots.K));
                cloned.Add(clone);
                Text_K_bet.text = K_bets.ToString();
                Total_Bets +=K_bets;
            }

            if(Diamond_bets> 0)
            {
                GameObject clone = Instantiate(chipimage,AndarBahar_ChipController.Instance.GetChipParent(Spots.Diamond).position,Quaternion.identity,AndarBahar_ChipController.Instance.GetChipParent(Spots.Diamond));
                cloned.Add(clone);
                Text_Diamond_bet.text = Diamond_bets.ToString();
                Total_Bets +=Diamond_bets;
            }
            
            if(Spade_bets > 0)
            {
                GameObject clone = Instantiate(chipimage,AndarBahar_ChipController.Instance.GetChipParent(Spots.Spade).position,Quaternion.identity,AndarBahar_ChipController.Instance.GetChipParent(Spots.Spade));
                cloned.Add(clone);
                Text_Spade_bet.text = Spade_bets.ToString();
                Total_Bets +=Spade_bets ;
            }
            
            if(Club_bets > 0)
            {
                GameObject clone = Instantiate(chipimage,AndarBahar_ChipController.Instance.GetChipParent(Spots.Club).position,Quaternion.identity,AndarBahar_ChipController.Instance.GetChipParent(Spots.Club));
                cloned.Add(clone);
                Text_Club_bet.text = Club_bets.ToString();
                Total_Bets+=Club_bets;
            }
            
            if(Heart_bets > 0)
            {
                GameObject clone = Instantiate(chipimage,AndarBahar_ChipController.Instance.GetChipParent(Spots.Heart).position,Quaternion.identity,AndarBahar_ChipController.Instance.GetChipParent(Spots.Heart));
                cloned.Add(clone);
                Text_Heart_bet.text = Heart_bets.ToString();
                Total_Bets +=Heart_bets;
            }
            
            if(Red_bets > 0)
            {
                GameObject clone = Instantiate(chipimage,AndarBahar_ChipController.Instance.GetChipParent(Spots.Red).position,Quaternion.identity,AndarBahar_ChipController.Instance.GetChipParent(Spots.Red));
                cloned.Add(clone);
                Text_Red_bet.text = Red_bets.ToString();
                Total_Bets +=Red_bets;
            }
            
            if(Black_bets > 0)
            {
                GameObject clone = Instantiate(chipimage,AndarBahar_ChipController.Instance.GetChipParent(Spots.Black).position,Quaternion.identity,AndarBahar_ChipController.Instance.GetChipParent(Spots.Black));
                cloned.Add(clone);
                Text_Black_bet.text = Black_bets.ToString();
                Total_Bets +=Black_bets;
            }
            if(A_6_bets > 0)
            {
                GameObject clone = Instantiate(chipimage,AndarBahar_ChipController.Instance.GetChipParent(Spots.A_6).position,Quaternion.identity,AndarBahar_ChipController.Instance.GetChipParent(Spots.A_6));
                cloned.Add(clone);
                Text_A_6_bet.text = A_6_bets.ToString();
                Total_Bets +=A_6_bets;
            }
            
            
            if(Seven_bets > 0)
            {
                GameObject clone = Instantiate(chipimage,AndarBahar_ChipController.Instance.GetChipParent(Spots.Seven).position,Quaternion.identity,AndarBahar_ChipController.Instance.GetChipParent(Spots.Seven));
                cloned.Add(clone);
                Text_Seven_bet.text = Seven_bets.ToString();
                Total_Bets +=Seven_bets;
            }
            
            if(_8_k_bets > 0)
            {
                GameObject clone = Instantiate(chipimage,AndarBahar_ChipController.Instance.GetChipParent(Spots._8_k).position,Quaternion.identity,AndarBahar_ChipController.Instance.GetChipParent(Spots._8_k));
                cloned.Add(clone);
                Text_8_K_bet.text = _8_k_bets.ToString();
                Total_Bets +=_8_k_bets;
            }
            
            if(Bahar_bets > 0)
            {
                GameObject clone = Instantiate(chipimage,AndarBahar_ChipController.Instance.GetChipParent(Spots.Bahar).position,Quaternion.identity,AndarBahar_ChipController.Instance.GetChipParent(Spots.Bahar));
                cloned.Add(clone);
                Text_Bahar_bet.text = Bahar_bets.ToString();
                Total_Bets +=Bahar_bets;
            }
            
            if(Andar_bets > 0)
            {
                GameObject clone = Instantiate(chipimage,AndarBahar_ChipController.Instance.GetChipParent(Spots.Andar).position,Quaternion.identity,AndarBahar_ChipController.Instance.GetChipParent(Spots.Andar));
                cloned.Add(clone);
                Text_Andar_bet.text = Andar_bets.ToString();
                Total_Bets +=Andar_bets;
            }
            if(!AndarBahar_RoundWinnerHandlercs.Instance.started)
            {
                balance = balance - Total_Bets;
            }
        }
        public void repeatbtn()//assigned on the repeat button
        {
            //restore();
            restorewithn();
        }
        void destroyspawn()
        {
            foreach (var item in cloned)
            {
                Destroy(item);
            }
        }

        void Update()
        {

        }
        void AddSocketListners()
        {
        //    Action onBadResponse = () => {  };

            
            // AndarBahar_SeverRequest.instance.ListenEvent<weelNumbers>( Utility.Events.OnWinNo, (json) =>
            // {
            //     StartCoroutine(OnRoundEnd(json.ToString()));
            // }, onBadResponse);

            // AndarBahar_ServerRequest.instance.ListenEvent(Utility.Events.OnWinNo, (json) => 
            // {
            //     // Debug.LogError("onwin  " + json.ToString());
            //     // StartCoroutine(OnRoundEnd(json));
            // });

            // AndarBahar_ServerRequest.instance.ListenEvent(Utility.Events.OnTimerStart, (json) =>
            // {
            //     // Debug.LogError("timer  " + json.ToStrin
            //     // OnTimerStart(json);
            // });

            // AndarBahar_ServerRequest.instance.ListenEvent<DoubleChanceClasses.OnWinAmount>(Utility.Events.OnWinNo, (json) =>
            // {
            //     // OnWinAmount(json.ToString());
            // }, onBadResponse);


            // AndarBahar_ServerRequest.instance.ListenEvent(Utility.Events.OnTimeUp, (json) =>
            // {
            //     // BettingButtonInteractablity(false);
            //     isTimeUp = true;
            // });

        }
        public void ChipImgSelect(int ind)
        {
            for (int i = 0; i < chipimg.Length; i++)
            {
                chipimg[i].SetActive(false);
            }
            chipimg[ind].SetActive(true);
        }
        private void AddListeners()
        {
            TakeBet_Btn.onClick.AddListener(()=>
            {
                //WinAmountTxt.text = " ";
            }
            );
            Left_10.onClick.AddListener(() =>
            {
                currentChip = Chip.Chip10;
                //ChipImgSelect(0);
                changehighlight(0);
                Debug.Log("xyz");
            });
            Left_25.onClick.AddListener(() =>
            {
                Debug.Log("2525255252525252525252525");
                currentChip = Chip.Chip25;
                changehighlight(1);
                //ChipImgSelect(1);
            });
            Left_50.onClick.AddListener(() =>
            {
                currentChip = Chip.Chip50;
                changehighlight(2);
                //ChipImgSelect(2);
            });
            Left_100.onClick.AddListener(() =>
            {
                currentChip = Chip.Chip100;
                changehighlight(3);
                //ChipImgSelect(3);
            });

            Right_500.onClick.AddListener(() =>
            {
                currentChip = Chip.Chip500;
                changehighlight(4);
               // ChipImgSelect(4);
            });
            
            Right_1000.onClick.AddListener(() =>
            {
                currentChip = Chip.Chip1000;
                changehighlight(5);
                //ChipImgSelect(5);
            });
            
            Right_5000.onClick.AddListener(() =>
            {
                currentChip = Chip.Chip5000;
                changehighlight(6);
                //ChipImgSelect(6);
            });
            
            Right_10000.onClick.AddListener(() =>
            {
                currentChip = Chip.Chip10000;
                Debug.Log("till here");
                changehighlight(7);
                //ChipImgSelect(7);
            });

            Debug.Log(timer == null);
            timer.onTimerEnd += () =>
            {
                if (timer.is_a_FirstRound) return;
                // startStopImgHolder.SetActive(true);
                // StartCoroutine(TransitionAnimation(stopBettingSprite));
            };
            timer.onTimerStart += () =>
            {
                if (timer.is_a_FirstRound) return;
                // startStopImgHolder.SetActive(true);
                // StartCoroutine(TransitionAnimation(startBettingSprite));
            };
            timer.onWait += () =>
            {
                ResetUI();
            };
            //lobby.onClick.AddListener(() => sideMenu.ShowPopup());
            // AddBets();
        }

        // public void AndharBetting()
        // {
        //     Debug.Log("Bet");
        //     if (balance >= (int)currentChip && baharBets == 0)
        //     {
        //         andarBets = andarBets + (int)currentChip;
        //         andarBets_Txt.text = andarBets.ToString();
        //         totalBets = andarBets + baharBets;
        //         Debug.Log("Total Bets = " + totalBets);
        //         TotalBetsValueTxt.text = totalBets.ToString();
        //         Debug.Log("Andar bets: "+andarBets);
        //     }
        //   //  AddBets();

        //     //balance = balance - (int)currentChip;
        //     //UpdateUI();
        // }

        // public void BaharBetting()
        // {
        //     Debug.Log("Bet");
        //     if (balance >= (int)currentChip && andarBets == 0)
        //     {
        //         baharBets = baharBets + (int)currentChip;
        //         andarBets_Txt.text = baharBets.ToString();
        //         totalBets = baharBets + andarBets;
        //         TotalBetsValueTxt.text = totalBets.ToString();
        //         Debug.Log("Bahar bets: " +baharBets);
        //     }
        //     //  AddBets();
        //     //balance = balance - (int)currentChip;
        //     //UpdateUI();
        // }

        // public void AddBets(Button BetsPlaced)
        // {
        //     if (balance < (int)currentChip || balance <  totalBets)
        //     {
        //         AndroidToastMsg.ShowAndroidToastMessage("not enough balance");
        //         Debug.Log("not enough balance");
        //         return;
        //     }
        //     totalBets = totalBets + (int)currentChip;
        //     balance -= (int)currentChip;
        //     if(BetsPlaced.gameObject.tag == "AndarBets" && baharBets == 0)
        //     {
        //         BetsPlaced.transform.GetChild(1).GetComponent<Text>().text = (int.Parse(BetsPlaced.transform.GetChild(1).GetComponent<Text>().text) + (int)currentChip).ToString();
        //         andarBets = int.Parse(BetsPlaced.transform.GetChild(1).GetComponent<Text>().text) + (int)currentChip;
        //     }
        //     else if(BetsPlaced.gameObject.tag == "BaharBets" && andarBets == 0)
        //     {
        //         BetsPlaced.transform.GetChild(1).GetComponent<Text>().text = (int.Parse(BetsPlaced.transform.GetChild(1).GetComponent<Text>().text) + (int)currentChip).ToString();
        //         baharBets = int.Parse(BetsPlaced.transform.GetChild(1).GetComponent<Text>().text) + (int)currentChip;
        //     }
        //     else if(BetsPlaced.gameObject.tag == "HeartSymbolBets")
        //     {
        //         BetsPlaced.transform.GetChild(1).GetComponent<Text>().text = (int.Parse(BetsPlaced.transform.GetChild(1).GetComponent<Text>().text) + (int)currentChip).ToString();
        //     }
        //     else if(BetsPlaced.gameObject.tag == "DiamondSymbolBets")
        //     {
        //         BetsPlaced.transform.GetChild(1).GetComponent<Text>().text = (int.Parse(BetsPlaced.transform.GetChild(1).GetComponent<Text>().text) + (int)currentChip).ToString();
        //     }
        //     else if(BetsPlaced.gameObject.tag == "SpadeSymbolBets")
        //     {
        //         BetsPlaced.transform.GetChild(1).GetComponent<Text>().text = (int.Parse(BetsPlaced.transform.GetChild(1).GetComponent<Text>().text) + (int)currentChip).ToString();
        //     }
        //     else if(BetsPlaced.gameObject.tag == "ClubSymbolBets")
        //     {
        //         BetsPlaced.transform.GetChild(1).GetComponent<Text>().text = (int.Parse(BetsPlaced.transform.GetChild(1).GetComponent<Text>().text) + (int)currentChip).ToString();
        //     }
        //     else if(BetsPlaced.gameObject.tag == "RedCardBets")
        //     {
        //         BetsPlaced.transform.GetChild(1).GetComponent<Text>().text = (int.Parse(BetsPlaced.transform.GetChild(1).GetComponent<Text>().text) + (int)currentChip).ToString();
        //     }
        //     else if(BetsPlaced.gameObject.tag == "BlackCardBets")
        //     {
        //         BetsPlaced.transform.GetChild(1).GetComponent<Text>().text = (int.Parse(BetsPlaced.transform.GetChild(1).GetComponent<Text>().text) + (int)currentChip).ToString();
        //     }
        //     else if(BetsPlaced.gameObject.tag == "AtoSixBets")
        //     {
        //         BetsPlaced.transform.GetChild(1).GetComponent<Text>().text = (int.Parse(BetsPlaced.transform.GetChild(1).GetComponent<Text>().text) + (int)currentChip).ToString();
        //     }
        //     else if(BetsPlaced.gameObject.tag == "SeventhCardBets")
        //     {
        //         BetsPlaced.transform.GetChild(1).GetComponent<Text>().text = (int.Parse(BetsPlaced.transform.GetChild(1).GetComponent<Text>().text) + (int)currentChip).ToString();
        //     }
        //     else if(BetsPlaced.gameObject.tag == "EightToKBets")
        //     {
        //         BetsPlaced.transform.GetChild(1).GetComponent<Text>().text = (int.Parse(BetsPlaced.transform.GetChild(1).GetComponent<Text>().text) + (int)currentChip).ToString();
        //     }
        //     else if(BetsPlaced.gameObject.tag == "DirectCardBets")
        //     {
        //         BetsPlaced.transform.GetChild(1).GetComponent<Text>().text = (int.Parse(BetsPlaced.transform.GetChild(1).GetComponent<Text>().text) + (int)currentChip).ToString();
        //     }
        //     UpdateUI();
        // }

        public void OnBetsOk()
        {
            //Debug.Log("betsok called...  ");
            
            // if (totalBets == 0)
            // {
            //     // commentTxt.text = "Bets Are Empty";
            //     return;
            // }
            // if (isTimeUp)
            // {
            //     AndroidToastMsg.ShowAndroidToastMessage("Time UP");
            //     return;
            // }
            //betOkBtn.interactable = false;
            //canPlaceBet = false;
            isBetConfirmed = true;
            canPlaceBet = false;
            betOkBtn.interactable = false;
            SendBets();
            UpdateUI();
        }
        public IEnumerator betsokblink()
        {
            betOkBtn.GetComponent<Image>().color = new Color32(200,200,200,128);
            yield return new WaitForSeconds(0.5f);
            betOkBtn.GetComponent<Image>().color = new Color32(255,255,255,255);
            yield return new WaitForSeconds(0.5f);
            if(!isBetConfirmed)
            {
                StartCoroutine(betsokblink());
            }
            

        }
        public IEnumerator takeblink()
        {
            TakeBet_Btn.GetComponent<Image>().color = new Color32(200,200,200,128);
            yield return new WaitForSeconds(0.5f);
            TakeBet_Btn.GetComponent<Image>().color = new Color32(255,255,255,255);
            yield return new WaitForSeconds(0.5f);
            if(!AndarBahar_RoundWinnerHandlercs.Instance.taken)
            {
                StartCoroutine(takeblink());
            }
            

        }
        int countrounds;
        
        private string SaveFilePath
        {
        get { return Application.persistentDataPath + "/andar.nms";}
        }

        int? nullvalue = null;
        public void SendBets()
        {
            if(!AndarBahar_RoundWinnerHandlercs.Instance.spinnow)
            {
                return;
            }
            //canPlaceBet = false;
            string _playerName = "GK" + PlayerPrefs.GetString("email");
            Debug.Log("///////////////////////////////////"+AndarBahar_ServerRequest.instance.username);
            //Debug.Log("Send bets called...  "+ canPlaceBet);
            //Debug.Log( "Bets Andhar: " +Andar_bets+"   Bets Bahar  "+  Bahar_bets+"   Bets A "+  A_bets+"   Bets 2 "+  _2_bets+"   Bets 3 "+ _3_bets+"   Bets 4 "+ _4_bets+"   Bets 5 "+ _5_bets+"   Bets 6 "+ _6_bets+"   Bets 7"+ _7_bets+"   Bets 8 "+ _8_bets+"   Bets 9 "+ _9_bets+"   Bets 10 "+ _10_bets+"   Bets J "+ J_bets+"   Bets Q "+ Q_bets+"   Bets K "+ K_bets+"   Bets Diamond "+ Diamond_bets+"   Bets Spade "+ Spade_bets+"   Bets Club "+ Club_bets+"   Bets Heart "+ Heart_bets+"   Bets Red "+ Red_bets+"   Bets Black "+ Black_bets+"   Bets A-6 "+ A_6_bets+"   Bets Seven "+ Seven_bets+"   Bets 8-K "+ _8_k_bets );

            //bets data = new bets
            sendingdata data = new sendingdata
            {
                playerId = _playerName,//"RL"+PlayerPrefs.GetString("email"),//AndarBahar_ServerRequest.instance.username,//player,
                //playerId= "RL00005",
                Card_A_amount = A_bets,
                Card_2_amount = _2_bets,
                Card_3_amount = _3_bets,
                Card_4_amount = _4_bets,
                Card_5_amount = _5_bets,
                Card_6_amount = _6_bets,
                Card_7_amount = _7_bets,
                Card_8_amount = _8_bets,
                Card_9_amount = _9_bets,
                Card_10_amount = _10_bets,
                Card_J_amount = J_bets, 
                Card_Q_amount = Q_bets,
                Card_K_amount = K_bets,
                Card_Diamond_amount = Diamond_bets,
                Card_Spade_amount = Spade_bets,
                Card_Club_amount = Club_bets,
                Card_Heart_amount = Heart_bets,
                Card_Red_amount = Red_bets,
                Card_Black_amount = Black_bets,
                Card_A_6_amount = A_6_bets,
                Card_seven_amount = Seven_bets,
                Card_8_K_amount = _8_k_bets,
                Card_Andhar_amount = Andar_bets,
                Card_Bahar_amount = Bahar_bets
                //"RL"+PlayerPrefs.GetString("email")
                
            };
            try
            {
                AndarBahar_ServerRequest.instance.socket.Emit(Utility.Events.OnBetsPlaced, new JSONObject (JsonConvert.SerializeObject(data) ) );
            }
            catch (Exception e)
            {
                Debug.Log(e);
                //throw;
            }
            
            
            if(Total_Bets >0)//(int.Parse(WinAmountTxt.text) >0)
            {
                store();
                File.Delete(SaveFilePath);
                savebinary.savefunctionandar();
                storewithin();
            }
            Debug.Log(new JSONObject (JsonConvert.SerializeObject(data)));
            CancelBet_Btn.gameObject.SetActive(false);
            canPlaceBet = false;
            
        }

        public void SendBets_Response(object data)
        {
            SendBet_Res res = AndarBahar.Utility.Fuction.GetObjectOfType<SendBet_Res>(data);    
            if (res.status == 200)
            {
                WinText.text = res.message;
                balance = res.data.balance;
                balanceTxt.text = balance.ToString();
            }
            else if (res.status == 400)
            {
                WinText.text = res.message;
                balance = res.data.balance;
            }
            else{
                Debug.Log("none of the above");
            }
        }

        public class SendBet_Res
        {
            public int status;
            public string message;
            public Data data;
        }
        public class Data
        {
            public float balance;
        }   
        public class TakeBet
        {
            public int win_point;
            public float balance;
        }

        public void Take_Bet_response(object data)
        {
            TakeBet res = AndarBahar.Utility.Fuction.GetObjectOfType<TakeBet>(data);
            // balance = res.Balance;
            balance = res.balance;
            balanceTxt.text = balance.ToString();
            WinAmountTxt.text = " ";//string.Empty;
        }

        public void CancelBet()
        {
            balance += Total_Bets;
            balanceTxt.text = balance.ToString();
            //destroyspawn();
            ResetUI();
            CancelBet_Btn.gameObject.SetActive(false);
            AndarBahar_ChipController.Instance.DestroyChips();
        }

        // private void PostBet(bets data)
        // {
        //     AndarBahar_ServerRequest.instance.SendEvent(Utility.Events.OnBetsPlaced, data, (res) =>
        //     {
        //         Debug.Log("bets res   " + res );
        //         var response = JsonConvert.DeserializeObject< DoubleChanceClasses.BetConfirmation>(res);
        //         Debug.Log("is bet placed starus with statu - " + JsonUtility.FromJson<DoubleChanceClasses.BetConfirmation>(res).status);

        //     });

        // }

        // void SendTakeAmountRequest() //Take bet function
        // {
        //     balance = balance + winningAmount;
        //     balanceTxt.text = balance.ToString();
        //     string _playername = "RL" + PlayerPrefs.GetString("email");
        //     object o = new { playerId = _playername , win_points = winningAmount};
        //     winningAmount = 0;
        //     WinText.text = winningAmount.ToString();
        //     AndarBahar_ServerRequest.instance.SendEvent( Utility.Events.OnWinAmount, o, (res) =>
        //     {
        //         UpdateUI();
        //     });
        // }

        // public IEnumerator Timer(int counter = 95) //60
        // {
        //     isTimeUp = false;
        //     canPlaceBet = true;
        // //   repeatBtn.interactable = betOkBtn.interactable = clearBtn.interactable = doubleBtn.interactable = true;
        //     isBetConfirmed = false;
        //     canStopTimer = false;
        //     while (counter > 0)
        //     {
        //         if (canStopTimer) yield break;
        //         // SecToMin(counter);
        //         timerTxt.text = counter.ToString();
        //         if(counter == 20)
        //         {
        //             if(winningAmount > 0)
        //             {
        //                 SendTakeAmountRequest();
        //             }
        //         }
        //         currentTime = counter;
        //         if (counter < 10)
        //         {
        //             canPlaceBet = false;
        //             if (!isBetConfirmed)
        //             {
        //                 if(totalBets > 0)
        //                 {
        //                     OnBetsOk();              //to confirm bets...
        //                 }
        //             }
        //         }
        //         yield return new WaitForSeconds(1f);
        //         counter--;
        //         timer_AudioSource.GetComponent<AudioSource>().Play();
        //     }
        //     currentTime = 0;
        //     timerTxt.text = 0.ToString();
        // }

        public void BackButton()
        {
            store();
            if (isBetConfirmed || int.Parse(WinAmountTxt.text)>0 )
            {
                storewithin();
            }
            if(Total_Bets == 0 || !isBetConfirmed)
            {
                Andarwithin.confirmed = false;
            }
            savebinary.savefunctionandar();
            PlayerPrefs.SetString("andarpaused","false");
            AndarBahar_ServerRequest.instance.socket.Emit(Utility.Events.onleaveRoom);
            //StartCoroutine(settingoffline());
            //sceneHandler.unloadAddressableScene();
            //SceneManager.LoadScene("MainScene");
        }

        // public void UpdateBets(Spots spot, Chip c)
        // {

            // int bet = (int)c;
            // switch (spot)
            // {
            //     case Spots.Andar:
            //         andarBets += bet;
            //         break;
            //     case Spots.Bahar:
            //         baharBets += bet;
            //         break;
                //case Spots.oneToFive:
                //    oneToFiveBets += bet;
                //    break;
                //case Spots.sixToTen:
                //    sixToTenBets += bet;
                //    break;
                //case Spots.elevenToFifteen:
                //    elevenToFifteenBets += bet;
                //    break;
                //case Spots.sixteenToTwentyFive:
                //    sixteenToTwentyFiveBets += bet;
                //    break;
                //case Spots.twentySixToThirty:
                //    twentySixToThirtyBets += bet;
                //    break;
                //case Spots.thirtyOneToThirtyFive:
                //    thirtyOneToThirtyFiveBets += bet;
                //    break;
                //case Spots.thirtySixToFouty:
                //    thirtySixToFourtyBets += bet;
                //    break;
                //case Spots.fortyOneAndMore:
                //    fourtyAndMoreBets += bet;
                //    break;
        //         default:
        //             break;
        //     }
        //     UpdateUI();
        // }

        // public void DisplayWinnerCardOnDashBoard(Card c)
        // {
            // int cardNo = c.cardNo;
            // if (cardNo == 10)
            // {
            //     winnerCard.text = "J";
            // }
            // else if (cardNo == 11)
            // {
            //     winnerCard.text = "Q";
            // }
            // else if (cardNo == 12)
            // {
            //     winnerCard.text = "K";
            // }
            // else
            // {
            //     winnerCard.text = cardNo.ToString();
            // }
        // }

        IEnumerator TransitionAnimation(Sprite img)
        {
            startStopImgHolder.SetActive(true);
            startStopImgHolder.GetComponent<Image>().sprite = img;
            startStopImgHolder.transform.position = startingPoint.position;
            startStopImgHolder.transform.DOLocalMoveX(middlePoint.position.x, transitionTime, false);
            iTween.MoveTo(startStopImgHolder, iTween.Hash("position", middlePoint.position, "time", transitionTime, "easetype", easeType));
            yield return new WaitForSeconds(transitionTime + delay);
            startStopImgHolder.GetComponent<RectTransform>().DOBlendableMoveBy(finishingPoint.position, transitionTime, true);
            iTween.MoveTo(startStopImgHolder, iTween.Hash("position", finishingPoint.position, "time", transitionTime, "easetype", easeType));
            startStopImgHolder.SetActive(false);
        }
        public void HideMessage()
        {

            messagePanel.SetActive(false);
        }

        public void ShowMessage(string msg)
        {
            messagePanel.SetActive(true);
            messageTxt.text = msg;
        }

        // public void OnPlayerWin(object o)
        // {
        //     Win win = JsonConvert.DeserializeObject<Win>(o.ToString());
        //     Debug.Log("OnPlayerWin");
        //     StartCoroutine(waitFor(win.winAmount));
        // }
        // IEnumerator waitFor(float amount)
        // {
        //     yield return new WaitForSeconds(10);
        //     balance += amount;
        //     UpdateUI();
        // }

        public void ResetUI()
        {
            print("reset ui");
            destroyspawn();
            Andar_bet_Allowed = 0;

            Total_Bets = Andar_bets = Bahar_bets = A_bets= _2_bets=_3_bets=_4_bets=_5_bets=_6_bets=_7_bets=_8_bets=_9_bets=_10_bets=J_bets=Q_bets=K_bets=Diamond_bets=Spade_bets=Club_bets=Heart_bets=Red_bets=Black_bets=A_6_bets=Seven_bets=_8_k_bets = 0;
            Text_A_bet.text = Text_2_bet.text = Text_3_bet.text = Text_4_bet.text =Text_5_bet.text = Text_6_bet.text = Text_7_bet.text = Text_8_bet.text = Text_9_bet.text = Text_10_bet.text = Text_J_bet.text = Text_Q_bet.text = Text_K_bet.text = Text_Heart_bet.text = Text_Diamond_bet.text = Text_Spade_bet.text = Text_Club_bet.text = Text_Red_bet.text = Text_Black_bet.text = Text_A_6_bet.text = Text_Seven_bet.text = Text_8_K_bet.text = string.Empty;
            Text_Bahar_bet.text = Text_Andar_bet.text = string.Empty;
            canPlaceBet = true;
            ready =true;
            WinText.text = "PLEASE BET TO START GAME, MINIMUM BET = 10";
           
            UpdateUI();
        }
        void UpdateUI()
        {
            balanceTxt.text = balance.ToString();
            Total_Bets_Text.text = Total_Bets.ToString();
            // TotalBetsValueTxt.text = totalBets.ToString();

        }

        public void UpdateDashboard(object data)
        {
            // Dashboard dashboard = JsonConvert.DeserializeObject<Dashboard>(data.ToString());
            // andarHirtoryPC.text = dashboard.historyPercent[0].ToString() + "%";
            // baharHirtoryPC.text = dashboard.historyPercent[1].ToString() + "%";
            // andarPridictionPC.text = dashboard.pridictionPercent[0].ToString() + "%";
            // baharPridictionPC.text = dashboard.pridictionPercent[1].ToString() + "%";
            // if (dashboard.balance != null)
            //     balance = float.Parse(dashboard.balance);

            // for (int i = 0; i < dashboard.previousWins.Count; i++)
            // {
            //     if (dashboard.previousWins[i] == 0)
            //     {
            //         andarBaharGrid[i].sprite = andarSprite;
            //     }
            //     else
            //     {
            //         andarBaharGrid[i].sprite = baharSprite;
            //     }
            // }

            // for (int i = 0; i < dashboard.historyCards.Count; i++)
            // {
            //     if (i >= last8roundsWins.Length) break;
            //     if (dashboard.historyCards[i].joker_card_no == 10)
            //     {
            //         last8roundsWins[i].text = "J";
            //     }
            //     else if (dashboard.historyCards[i].joker_card_no == 11)
            //     {
            //         last8roundsWins[i].text = "Q";
            //     }
            //     else if (dashboard.historyCards[i].joker_card_no == 12)
            //     {
            //         last8roundsWins[i].text = "K";
            //     }
            //     else
            //     {
            //         try
            //         {
            //             if (i < last8roundsWins.Length)
            //                 last8roundsWins[i].text = dashboard.historyCards[i].joker_card_no.ToString();

            //         }
            //         catch (Exception)
            //         {

            //             throw;
            //         }
            //     }

            //     if (i < last8roundsWins.Length)
            //         if (dashboard.historyCards[i].winSpot == (int)Spots.Andar)
            //         {
            //             last8roundsWins[i].color = Color.blue;
            //         }
            //         else
            //         {
            //             last8roundsWins[i].color = Color.red;
            //         }
            // }
            // UpdateUI();
        }
        public void clearstore()
        {
            AndarBaharlast.winamount = 0;
        }
        public void store()
        {
            AndarBaharlast.winamount =int.Parse(WinAmountTxt.text);
            AndarBaharlast.A = A_bets;
            AndarBaharlast.twob =_2_bets ;
            AndarBaharlast.threeb = _3_bets;
            AndarBaharlast.fourb =_4_bets ;
            AndarBaharlast.fiveb = _5_bets;
            AndarBaharlast.sixb =_6_bets ;
            AndarBaharlast.sevenb = _7_bets;
            AndarBaharlast.eightb =_8_bets ;
            AndarBaharlast.nineb = _9_bets;
            AndarBaharlast.tenb =_10_bets ;
            AndarBaharlast.jb = J_bets;
            AndarBaharlast.qb =Q_bets ;
            AndarBaharlast.kb = K_bets;
            AndarBaharlast.diamondb = Diamond_bets;
            AndarBaharlast.spadeb = Spade_bets;
            AndarBaharlast.cloverb = Club_bets;
            AndarBaharlast.heartb = Heart_bets;
            AndarBaharlast.redb = Red_bets;
            AndarBaharlast.blackb = Black_bets;
            AndarBaharlast.atosix = A_6_bets;
            AndarBaharlast.seven = Seven_bets;
            AndarBaharlast.eighttok = _8_k_bets;
            AndarBaharlast.andar = Andar_bets;
            AndarBaharlast.bahar = Bahar_bets;
            //lastrecord.andarlas = true;
        }
        
        public void changehighlight(int number)
        {
            
            //StopCoroutine(blinkcoin(number));
            for (int i = 0; i < highlight.Length; i++)
            {
                Debug.Log("reached till here" + number);
                if(i == number)
                {
                    highlight[i].SetActive(true);
                    //highlight[i].SetActive(true);
                    //StartCoroutine(blinkcoin(number));
                }
                else
                {
                    highlight[i].SetActive(false);
                }
            }
        }
        IEnumerator blinkcoin(int value)
        {
            highlight[value].SetActive(true);
            yield return new WaitForSeconds(1f);
            //highlight[value].SetActive(false);
            //yield return new WaitForSeconds(1f);
            
            //StartCoroutine(blinkcoin(value));

            // Debug.Log("reached till here");
            // highlight[value].GetComponent<Image>().color = new Color32(0,0,0,255);
            // yield return new WaitForSeconds(1f);
            // highlight[value].GetComponent<Image>().color = new Color32(0,0,0,100);
            // yield return new WaitForSeconds(1f);
            // StartCoroutine(blinkcoin(value));

        }

        public void ShowHistory(object data) //not needed now...!!!
        {
            
        }

        [Serializable]
        public class bets
        {
            public string playerId;
            public int? Card_A_amount; 
            public int Card_2_amount,Card_3_amount, Card_4_amount, Card_5_amount, Card_6_amount, Card_7_amount, Card_8_amount, Card_9_amount, Card_10_amount, Card_J_amount, Card_Q_amount, Card_K_amount, Card_Heart_amount, Card_Diamond_amount, Card_Spade_amount, Card_Club_amount, Card_Red_amount, Card_Black_amount, Card_A_6_amount, Card_seven_amount, Card_8_K_amount, Card_Andhar_amount, Card_Bahar_amount;
            
           //public string totalbets;
        }
    }
}

public class HistoryCard
{
    public int joker_card_no;
    public int winSpot;
}
public class sendingdata
{
    public string playerId;
    public int? Card_A_amount; 
    public int Card_2_amount,Card_3_amount, Card_4_amount, Card_5_amount, Card_6_amount, Card_7_amount, Card_8_amount, Card_9_amount, Card_10_amount, Card_J_amount, Card_Q_amount, Card_K_amount, Card_Heart_amount, Card_Diamond_amount, Card_Spade_amount, Card_Club_amount, Card_Red_amount, Card_Black_amount, Card_A_6_amount, Card_seven_amount, Card_8_K_amount, Card_Andhar_amount, Card_Bahar_amount;
}

// public class Dashboard
// {
//     public List<int> previousWins;
//     public List<HistoryCard> historyCards;
//     public List<int> historyPercent;
//     public List<int> pridictionPercent;
//     public string balance;
// }
