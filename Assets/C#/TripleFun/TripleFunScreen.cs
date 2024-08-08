using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Shared;
using System.Linq;
using m = UnityEngine.MonoBehaviour;
//using Com.BigWin.Frontend.Data;
using UnityEngine.UI;
using TMPro;
using Newtonsoft.Json;
using TripleChance.Utility;
using TripleChance.ServerStuff;
using TFU;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using System.IO;
//using BetNameSpace;
//using LastBetNameSpace;
//using Com.BigWin.Frontend.JeetoJokerSocketClasses;
//using DoubleChance;

namespace TripleChance.GamePlay
{
    public class TripleFunScreen : MonoBehaviour
    {
        public static TripleFunScreen Instance;
        [SerializeField] Toggle chipNo1Btn;
        [SerializeField] Toggle chipNo5Btn;
        [SerializeField] Toggle chipNo10Btn;
        [SerializeField] Toggle chipNo50Btn;
        [SerializeField] Toggle chipNo100Btn;
        [SerializeField] Toggle chipNo500Btn;
        [SerializeField] Toggle chipNo1000Btn;
        [SerializeField] Toggle chipNo5000Btn;
        [SerializeField] GameObject[] chipimages;

        #region BETTING RETATED GRID
        //---------All Type Cards------------
        public Button[] doubleGridBtns;
        public Button[] singleGridBtns;
        public Button[] tripleGridBtns;
        public GameObject TripleBetting_Table, RighteShadow_panel;
        public GameObject DoubleBetting_Table, LeftShadow_panel;
        bool _tripletable_zoomout, _playanim;
        bool _doubletable_zoomInOut, _playdouble;

        #endregion
        //[SerializeField] Button exitBtn;
        [SerializeField] Button betOkBtn;
        [SerializeField] Button TakeBtn;
        [SerializeField] Button closebtn;
        [SerializeField] Button clearBtn_Left;
        [SerializeField] Button ClearSpecificBets_Left;
        [SerializeField] Button clearBtn_Right;
        [SerializeField] Button ClearSpecificBets_Right;
        [SerializeField] Button wheelZoomON;
        [SerializeField] Button WheelZoomOff;
        [SerializeField] Button Repeatbtn;
        //[SerializeField] Button doubleBtn;
        //[SerializeField] Button repeatBtn;
        //[SerializeField] Button RandomPickBtn;

        [SerializeField] Button Mulitple100;
        [SerializeField] Button Mulitple200;
        [SerializeField] Button Mulitple300;
        [SerializeField] Button Mulitple400;
        [SerializeField] Button Mulitple500;
        [SerializeField] Button Mulitple600;
        [SerializeField] Button Mulitple700;
        [SerializeField] Button Mulitple800;
        [SerializeField] Button Mulitple900;


        [SerializeField] GameObject RandomPickObj;

        [SerializeField] Text timerTxt;
        [SerializeField] Text balanceTxt;
        [SerializeField] Text totalBetsTxt;
        [SerializeField] Text commentTxt;

        const int SINGLE_BETS_LIMIT = 5000;
        const int OVERALL_BETS_LIMIT = 50000;

        private bool isUserPlacedBets;
        public bool isBetConfirmed;
        public bool canPlaceBet;
        private bool isLastGameWinAmountReceived;
        private bool canPlacedBet;
        private bool isthisisAFirstRound;
        private bool isPreviousBetPlaced;
        private bool isdataLoaded;
        public  bool isTimeUp;
        public int[] previous_round_single = new int[10];
        public int[] previous_round_double= new int[210];
        public List<int> previous_round_triplekey;
        //public int[] previous_round_triplekey= new int[1000];
        public List<int> previous_round_triplevalue;
        //public int[] previous_round_triplevalue= new int[1000];


        private int lastWinNumber;
        int[] ALL_HEARTS_SPADES_DIAMONDS_CLUBS_BET_CONTINER = new int[4];//All
        public int[] Double_Bets_Container = new int[100];
        int[] CopyArray_Double_Bets_Container = new int[100];
        int[] Previous_Double_Bets_Container = new int[100];
        public int[] Single_Bets_Container = new int[10];
        int[] CopyArray_Single_Bets_Container = new int[10];
        int[] Previous_Single_Bets_Container = new int[10];
        int[] Triple_Bets_Container = new int[1000];
        int[] CopyArray_Triple_Bets_Container = new int[100];
        int[] Previous_Triple_Bets_Container = new int[100];
        
        public Dictionary<int, int> TripleBets = new Dictionary<int,int>();
        int currentlySelectedChip = 10;

        float balance;
        public int totalBets;
        string roundcount;
        string lastroundcount;
        string lastWinRoundcount;
        string isPreviousWinsRecivied;
        string winningAmount;
        string currentComment;
        string userId;
        string[] PrizeName;
        string[] commenstArray = { "Bets are Empty!!", "For Amusement Only", "Bet Accepted!! your bet amount is :", "Please click on Take", "Bets Confirmed" };

        //[SerializeField] GameObject DoubleChancebtn;
        //public override ScreenID ScreenID => ScreenID.DOUBLE_CHANCE_GAME_SCREEN;
        //protected override string ScreenName =>  DoubleChancebtn.name;

        int outer_win_wheelValue, inner_wheelValue;

        public List<string> singleWins_list = new List<string>();
        public List<string> DoubleWins_List = new List<string>();
        public List<Text> PreviousSingleWinList = new List<Text>();
        public List<Text> PreviousDoubleWinList = new List<Text>();
        public Dictionary<int, int> _betsData = new Dictionary<int, int>();
        public Text WinNo_txt;
        public GameObject Wheel, Wheel_shadowPanel;
        //public GameObject WaitPanel;
        public List<GameObject> FillerList;
        public Text Minutes, Seconds;
        int SelectedBets_section, OnClickTripleBet;
        public GameObject CancelBets_Left, CancelSpecificBets_Left,
        CancelBets_Right, CancelSpecificBets_Right;
        public Text winTxt;
        public GameObject UiAnimation_Obj;
        int winnerAmount;
        [SerializeField] List<int> PreviousWin_singleLocal, PreviousWin_doubleLocal, PreviousWin_tripleLocal;
        int single, @double, triple;
        public Text OuterWheel_txt, MediumWheel_txt, InnerWheel_txt;
        public GameObject timer_AudioSource, spin_AudioSource, bets_AudioSource;
        public int currentmultiple;
        //public int[] triplekeys = new int[1000];
        public List<int> triplekeys;
        public List<int> triplevalue;
        public AudioClip coinsaudio;
        public Text message;
        //public int[] triplevalue = new int[1000];

        public SceneHandler sceneHandler;

        private bool OnZoom;
        float winround;
        [SerializeField] GameObject[] arrowsticks;
        string winamounturl ="http://139.59.92.165:5000/user/Winamount";
        string emptyurl ="http://139.59.92.165:5000/user/DeletePreviousWinamount";
        string previousurl ="http://139.59.92.165:5000/user/triplechance";
        //bool spinnow;
        public bool spinnow
        {
            get{
                if(int.Parse(winTxt.text)>0)
                {
                    return false;
                }
                else{
                    return true;
                }
            }
        }
        bool betadded = false;

        //public override void Initialize(Transform screenContainer, ScreenController screenController)
        //{
        //    base.Initialize(screenContainer, screenController);
        //    AddListners();
        //    Double_Bets_Container = new int[doubleGridBtns.Length];
        //    Previous_Double_Bets_Container = new int[doubleGridBtns.Length];
        //    Single_Bets_Container = new int[singleGridBtns.Length];
        //    Previous_Single_Bets_Container = new int[singleGridBtns.Length];
        //}

        void Awake()
        {
            Instance = this;
        }


        private void Start()
        {
            //sceneHandler = GameObject.Find("SceneHandler").GetComponent<SceneHandler>();
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
            _tripletable_zoomout = false;
            _playanim = true;

            _doubletable_zoomInOut = false;
            _playdouble = true;
            TripleBetting_Table.GetComponent<Canvas>().overrideSorting = false;
            RighteShadow_panel.GetComponent<Canvas>().overrideSorting = false;
            DoubleBetting_Table.GetComponent<Canvas>().overrideSorting = false;
            LeftShadow_panel.GetComponent<Canvas>().overrideSorting = false;
            //spinnow = true;
            
            //int colourchanger = 0;
            
           
            // balance = int.Parse(PlayerPrefs.GetString("_balance", "10000"));
            if(PlayerPrefs.GetFloat("points") <0)
            {
                balance = 0;
            }
            else{
                balance = PlayerPrefs.GetFloat("points");
            }
            
            isthisisAFirstRound = true;
            AddListners();
            Debug.Log(balance);
            balanceTxt.text = balance.ToString("F2");
            totalBetsTxt.text = totalBets.ToString();
            if(PlayerPrefs.GetString("triplestarted") =="true")
            {
                savebinary.LoadPlayertriple();
                PlayerPrefs.SetString("triplestarted","false");
            }
            
            
            SelectedBets_section = 0;
            for(int i = 0; i < 1000; i++)
            {
                TripleBets.Add(i, 0);
            }
            PlayerPrefs.Save();

            for(int j = 0; j < PreviousWin_singleLocal.Count; j++)
            {
                TripleFunWheel.instance.PreviousWin_Single.Add(PreviousWin_singleLocal[j]);
            }

            for(int j = 0; j < PreviousWin_doubleLocal.Count; j++)
            {
                TripleFunWheel.instance.PreviousWin_Double.Add(PreviousWin_doubleLocal[j]);
            }

            for(int j = 0; j < PreviousWin_tripleLocal.Count; j++)
            {
                TripleFunWheel.instance.PreviousWin_Triple.Add(PreviousWin_tripleLocal[j]);
            }
            if(Triplelast.winamount >0)
            {
                winTxt.text = Triplelast.winamount.ToString();
                StartCoroutine(TakeBlinkAnim());
            }

            TripleFunWheel.instance.SetLastFive();  
            // if(PlayerPrefs.GetString("triplepaused") == "true" || Triplewithin.tripleconfirmed == true)
            // {
            //     restorewithin();
            //     isBetConfirmed = true;
            //     betOkBtn.interactable = false;
            //     canPlaceBet = false;
            // }
            // else
            // {
            //     isBetConfirmed = false;
            //     betOkBtn.interactable = true;
            //     canPlaceBet=true;
            // }
            if(Triplelast.playerid == PlayerPrefs.GetString("email"))
            {
                Debug.Log("device matched");
                //restorewithin();
            }
            else{
                Debug.Log("deviecid incorrect");
            }
            
            //currentmultiple = 0;
            
            // UpdateUi();
        }
        bool started;
        public void triplerestoration()
        {
            started = true;
            StartCoroutine(triplewinresponse());
            TripleFunWheel.instance.SetLastFive();
            if(PlayerPrefs.GetString("triplepaused") == "true" || Triplewithin.tripleconfirmed == true)
            {
                if(TripleChance_ServerResponse.instance.tripleround == Triplewithin.Roundcount)
                {
                    restorewithin();
                }
                
                isBetConfirmed = true;
                canPlaceBet = false;
                betOkBtn.interactable = false;
            }
            else
            {
                isBetConfirmed = false;
                canPlaceBet=true;
                betOkBtn.interactable = true;
            }
        }
        
        private string SaveFilePath
        {
        get { return Application.persistentDataPath + "/Triplefun.nms"; }
        }

        bool isPause;
        void OnApplicationPause(bool hasFocus)
        {
            isPause = hasFocus;
                
            if(isPause)
            {
                if (isBetConfirmed)
                {
                    PlayerPrefs.SetString("triplepaused","true");
                    if(totalBets > 0)
                    {
                        store();
                    }
                    
                    Triplelast.winamount = int.Parse(winTxt.text);
                    File.Delete(SaveFilePath);
                    savebinary.savefunctiontriple();
                    storewithin();
                    PlayerPrefs.SetInt("tripletrounds",TripleChance_ServerResponse.instance.tripleround);
                }
                Triplewithin.tripleconfirmed = false;
                // Debug.Log("Focus True:" +hasFocus);
                // StopAllCoroutines();
                // ServerResponse.instance.socketoff();
                // FunTarget_ServerResponse.instance.socketoff();
                // TripleChance_ServerResponse.Instantiate.socketoff();
                //7Up&DownServerResponse.Instance.socketoff();
                
                // AndarBahar_ServerResponse.Instance.socketoff();
                //SceneManager.LoadScene(1);
                //AndarBahar_ServerRequest.instance.socket.Emit(Utility.Events.onleaveRoom); //leave room;
            }   
            if (!isPause)
            {
                
                // Debug.Log("Focus False:" +hasFocus);
                //TripleChance_ServerResponse.instance.socketoff();
                PlayerPrefs.SetInt("treload",1);
                TripleChance_ServerRequest.intance.socket.Emit(Utility.Events.onleaveRoom);
                //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                // HomeScript.Instance.AndarBaharBtn();
                // AndarBahar_ServerResponse.Instance.socketOn();
                //SceneManager.LoadScene(SceneManager.GetActiveScene().name);                
            }
            // }
        }
        
        // string setplayeroffline = "http://139.59.60.118:5000/user/SetplayerOffline";
        // public IEnumerator settingoffline()
        // {
        //     WWWForm form = new WWWForm();
        //     string playername = "RL"+PlayerPrefs.GetString("email");
        //     form.AddField("email", playername);
        //     using (UnityWebRequest www = UnityWebRequest.Post(setplayeroffline, form))
        //     {
        //         yield return www.SendWebRequest();

        //         if (www.result != UnityWebRequest.Result.Success)
        //         {
        //             Debug.Log(www.error);
        //         }
        //         else{
        //             //Debug.Log("Offline//Offline//Offline//Offline//Offline//Offline//Offline//Offline//Offline//Offline//");
        //         }
        //     }
        // }
        private void OnApplicationQuit() {
            PlayerPrefs.SetString("triplepaused","false");
            if(totalBets>0)
            {
                store();
            }
            //StartCoroutine(settingoffline());
        }

        private void AddListners()
        {
            //exitBtn.onClick.AddListener(() =>
            //{
            //    //SocketRequest.intance.SendEvent(Constant.onleaveRoom);
            //    //sc.OnClickHome();
            //});

            betOkBtn.onClick.AddListener(() =>
            {
                if(totalBets > 0)
                {
                    OnBetsOk();
                }
            });

            TakeBtn.onClick.AddListener(() =>
            {
                SendTakeAmountRequest();
                
            });

            clearBtn_Left.onClick.AddListener(() =>
            {
                clearbets();
               //ResetAllBets();
            });

            clearBtn_Right.onClick.AddListener(() =>
            {
                clearbets();
               //ResetAllBets();
            });

            ClearSpecificBets_Left.onClick.AddListener(() =>
            {
                CancelSpecificBets();
            });

            ClearSpecificBets_Right.onClick.AddListener(() =>
            {
                CancelSpecificBets();
            });

            //doubleBtn.onClick.AddListener(() =>
            //{
            //    OnClickOnDoubleBetBtn();
            //});

            //repeatBtn.onClick.AddListener(() =>
            //{
            //    RepeatBets();
            //});

            //RandomPickBtn.onClick.AddListener(() =>
            //{
            //    RandomPickObj.SetActive(true);
            //});

            AddCardBetListners();
            AddSocketListners();
            chipNo1Btn.onValueChanged.AddListener((i) => { if (!i) return; currentlySelectedChip = 1;coinanimation(0); });
            chipNo5Btn.onValueChanged.AddListener((i) => { if (!i) return; currentlySelectedChip = 5;coinanimation(1);  });
            chipNo10Btn.onValueChanged.AddListener((i) => { if (!i) return; currentlySelectedChip = 10;coinanimation(2);  });
            chipNo50Btn.onValueChanged.AddListener((i) => { if (!i) return; currentlySelectedChip = 50;coinanimation(3);  });
            chipNo100Btn.onValueChanged.AddListener((i) => { if (!i) return; currentlySelectedChip = 100;coinanimation(4);  });
            chipNo500Btn.onValueChanged.AddListener((i) => { if (!i) return; currentlySelectedChip = 500;coinanimation(5);  });
            chipNo1000Btn.onValueChanged.AddListener((i) => { if (!i) return; currentlySelectedChip = 1000;coinanimation(6);  });
            chipNo5000Btn.onValueChanged.AddListener((i) => { if (!i) return; currentlySelectedChip = 5000;coinanimation(7);  });
        }

        int CurrentBetvalue = 0;
        private void AddCardBetListners()
        {
            for (int i = 0; i < doubleGridBtns.Length; i++)
            {
                int index = i;
                doubleGridBtns[i].onClick.AddListener(() =>
                {
                // doubleGridBtns[index].transform.GetChild(0).gameObject.GetComponent<Text>().text = ( currentlySelectedChip).ToString();
                    //CurrentBetvalue = CurrentBetvalue + currentlySelectedChip;
                    //totalBets = totalBets + currentlySelectedChip;
                    //totalBetsTxt.text = totalBets.ToString();

                    if(_doubletable_zoomInOut == false)
                    {
                        _doubletable_zoomInOut = true;
                        _playdouble = true;
                    }
                    if(_playdouble == true)
                    {
                        //DoubleZoomIn_table();
                    }
                    if(Double_Bets_Container[index] < 1000)
                    {
                        AddBets(ref Double_Bets_Container, index,2);
                    }
                    
                });
            }


            for (int i = 0; i < singleGridBtns.Length ; i++)
            {
                int index = i;
                singleGridBtns[i].onClick.AddListener(() =>
                {
                    Debug.Log("reached here reached here reached here reached here");
                    // singleGridBtns[index].transform.GetChild(0).gameObject.GetComponent<Text>().text = currentlySelectedChip.ToString();
                    //totalBets = totalBets + currentlySelectedChip;
                    //totalBetsTxt.text = totalBets.ToString();
                    if(Single_Bets_Container[index] < 1000)
                    {
                        AddBets(ref Single_Bets_Container, index,1);
                        zoomallout();
                    }
                    
                

                });
            }

            for (int i = 0; i < tripleGridBtns.Length; i++)
            {
                int index = i;
                tripleGridBtns[i].onClick.AddListener(() =>
                {
                    if(_tripletable_zoomout == false)
                    {
                        _tripletable_zoomout = true;
                        _playanim = true;
                    }
                    if(_playanim == true)
                    {
                        //TripleZoomIn_table();
                    }
                    //OnClickTripleBet = int.Parse(tripleGridBtns[index].transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text);
                    // Debug.LogError("index  " + tripleGridBtns[index].transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text );
                    Debug.Log("Trip[le container]" + Triple_Bets_Container[index] + "index " +index);
                    AddBets(ref Triple_Bets_Container, index,3);
                    zoomallout();
                    // if(Triple_Bets_Container[index] < 100)
                    // {
                        
                    //     AddBets(ref Triple_Bets_Container, index);
                    // }
                   
                });
            }

        // UpdateUi();
        }
        public void addsinglebets()
        {
            Debug.Log("clickclickclickclickclickclickclick");
        }
        
        public bool added;
        private void AddBets(ref int[] continer, int containerIndex,int assign)
        {
            if (balance < currentlySelectedChip || winnerAmount!=0)// || balance <  totalBets)
            {
                if(winnerAmount!=0)
                {
                    //AndroidToastMsg.ShowAndroidToastMessage("Please Collect the Winning Amount first");
                    message.text ="Please Collect the Winning Amount first";
                    return;
                }
                else
                {
                    //AndroidToastMsg.ShowAndroidToastMessage("not enough balance");
                    message.text ="not enough balance";
                    Debug.Log("not enough balance");
                    return;
                }
            }
            if(!canPlaceBet)
            {
                return;
            }
            added = true;
            // if (continer[containerIndex] + currentlySelectedChip > SINGLE_BETS_LIMIT)
            // {
            //     AndroidToastMsg.ShowAndroidToastMessage("reached the limit");
            //     Debug.Log("reached the limit");
            //     // return;
            // }
            if(assign ==3)
            {
                if (continer[containerIndex+currentmultiple] + currentlySelectedChip > 100)
                {
                    //AndroidToastMsg.ShowAndroidToastMessage("reached the limit");
                    message.text ="reached the limit";
                    Debug.Log("reached the limit");
                    //continer[containerIndex].transform
                    return;
                }
            }
            else if(assign ==2)
            {
                if (continer[containerIndex] + currentlySelectedChip > 1000)
                {
                    //AndroidToastMsg.ShowAndroidToastMessage("reached the limit");
                    message.text ="reached the limit";
                    Debug.Log("reached the limit");
                    
                    return;
                }
                else if(continer[containerIndex] == 1000)
                {
                    doubleGridBtns[containerIndex].interactable = false;
                }
            }
            else if(assign ==1)
            {
                if (continer[containerIndex] + currentlySelectedChip > 5000)
                {
                    //AndroidToastMsg.ShowAndroidToastMessage("reached the limit");
                    message.text ="Reached the limit";
                    Debug.Log("reached the limit");
                    
                    return;
                }
                
                else if(continer[containerIndex] == 5000)
                {
                    singleGridBtns[containerIndex].interactable = false;
                }   
            }
            totalBets = totalBets + currentlySelectedChip;
            totalBetsTxt.text = totalBets.ToString();
            balance -= currentlySelectedChip;
            bets_AudioSource.GetComponent<AudioSource>().Play();
            if(!betadded)
            {
                Debug.Log("Repeat button disabling");
                betadded = true;
                StartCoroutine(StopPrevAnim());
                StartCoroutine(BetBlinkAnim());
            }
            
            int _betValue = 0;
            if (assign!=3)
            {
                continer[containerIndex] += currentlySelectedChip;
                _betValue= continer[containerIndex];
            }
            //continer[currentmultiple+containerIndex] += currentlySelectedChip;
            //Debug.Log("container values"+ continer[containerIndex]);

            //int _betValue = continer[containerIndex];
            //int _betValue = continer[currentmultiple+containerIndex];
            //Debug.Log("Triple bets "+ OnClickTripleBet+" betsvalue "+_betValue);
            //TripleBets[continer[containerIndex]] = _betValue;
            if (assign == 3)
            {
                bool contains = false;
                continer[containerIndex] += currentlySelectedChip;
                _betValue = continer[containerIndex];
                //SelectedBets_section = currentmultiple;
                TripleBets[containerIndex] = _betValue;
                for (int i = 0; i < triplekeys.Count; i++)
                {
                    if(triplekeys[i] == containerIndex)
                    {
                        triplevalue[i]+=currentlySelectedChip;
                        contains = true;
                        break;
                    }
                }
                if (!contains)
                {
                    triplekeys.Add(containerIndex);
                    triplevalue.Add(_betValue);
                }
                //Debug.Log("the number"+triplekeys[])
                
                // foreach (var item in TripleBets)
                // {
                //     //Debug.Log("key:"+item.Key+ "Value"+ item.Value);
                // }
                //Debug.Log("finalllllllllllll"+ currentmultiple+containerIndex);
                //TripleBets[OnClickTripleBet] = _betValue;
            }
            
            if(_betsData.ContainsKey(containerIndex) )
            {
                _betsData[containerIndex] = _betValue;//_betValue;
            }
            else
            {
                _betsData.Add(containerIndex, _betValue);
            }
            UpdateUi();

            //  SoundManager.instance.PlayClip("addbet");
        }

        public void TripleZoomIn_table()
        {
            if(_tripletable_zoomout == true)
            {
                RighteShadow_panel.SetActive(true);
                TripleBetting_Table.GetComponent<Animation>().Play("TripleFun_tableZoomIn");
                TripleBetting_Table.GetComponent<Canvas>().overrideSorting = true;
                RighteShadow_panel.GetComponent<Canvas>().overrideSorting = true;
                DoubleBetting_Table.GetComponent<Canvas>().overrideSorting = false;
                LeftShadow_panel.GetComponent<Canvas>().overrideSorting = false;
                CancelBets_Right.SetActive(true);
                CancelSpecificBets_Right.SetActive(true);
                _playanim = false;
            }
            else if(_doubletable_zoomInOut == true)
            {
                DoubleBetting_Table.GetComponent<Canvas>().overrideSorting = false;
                LeftShadow_panel.GetComponent<Canvas>().overrideSorting = false;
                DoubleZoomOut_table();
            }
        }
        [SerializeField] Transform lefttop,rightop,leftbottom,rightbottom;

        public void setzoom(int zone)
        {
            if(isBetConfirmed || !spinnow)
            {
                return;
            }
            switch (zone)
            {
                
                case 1 :lefttop.GetComponent<Animator>().Play("matkatopleft");
                        if(rightop.localScale.x >1)
                        {
                            rightop.GetComponent<Animator>().Play("matkatoprightout");
                        }
                        if(leftbottom.localScale.x >1)
                        {
                            leftbottom.GetComponent<Animator>().Play("matkaleftout");
                        }
                        if(rightbottom.localScale.x >1)
                        {
                            rightbottom.GetComponent<Animator>().Play("matkarightlowout");
                        }
                
                        lefttop.parent.transform.SetAsLastSibling();
                        break;
                case 2 :rightop.GetComponent<Animator>().Play("matkatoprightin");
                        if(lefttop.localScale.x >1)
                        {
                            lefttop.GetComponent<Animator>().Play("matkatopleftout");
                        }
                        if(leftbottom.localScale.x >1)
                        {
                            leftbottom.GetComponent<Animator>().Play("matkaleftout");
                        }
                        if(rightbottom.localScale.x >1)
                        {
                            rightbottom.GetComponent<Animator>().Play("matkarightlowout");
                        }


                        rightop.parent.parent.transform.SetAsLastSibling();
                        break;
                case 3: leftbottom.GetComponent<Animator>().Play("matkalowleftin");
                        if(lefttop.localScale.x >1)
                        {
                            lefttop.GetComponent<Animator>().Play("matkatopleftout");
                        }
                        if(rightop.localScale.x >1)
                        {
                            rightop.GetComponent<Animator>().Play("matkatoprightout");
                        }
                        if(rightbottom.localScale.x >1)
                        {
                            rightbottom.GetComponent<Animator>().Play("matkarightlowout");
                        }
                        leftbottom.parent.transform.SetAsLastSibling();
                        break;
                case 4 :rightbottom.GetComponent<Animator>().Play("matkarightin");
                        if(lefttop.localScale.x >1)
                        {
                            lefttop.GetComponent<Animator>().Play("matkatopleftout");
                        }
                        if(rightop.localScale.x >1)
                        {
                            rightop.GetComponent<Animator>().Play("matkatoprightout");
                        }
                        if(leftbottom.localScale.x >1)
                        {
                            leftbottom.GetComponent<Animator>().Play("matkaleftout");
                        }
                        
                        leftbottom.parent.parent.transform.SetAsLastSibling();
                        break;
                //case default : break;
                
            }
        }

        public void TripleZoomOut_table()
        {
            _tripletable_zoomout = false;
            if(_tripletable_zoomout == false)
            {
                RighteShadow_panel.SetActive(false);
                TripleBetting_Table.GetComponent<Animation>().Play("TripleFun_tableZoomOut");
                TripleBetting_Table.GetComponent<Canvas>().overrideSorting = false;
                RighteShadow_panel.GetComponent<Canvas>().overrideSorting = false;
                CancelBets_Right.SetActive(false);
                CancelSpecificBets_Right.SetActive(false);
            }
        }

        public void DoubleZoomIn_table()
        {
            if(_doubletable_zoomInOut == true)
            {
                LeftShadow_panel.SetActive(true);
                DoubleBetting_Table.GetComponent<Animation>().Play("DoubleBet_tableZoomIn");
                TripleBetting_Table.GetComponent<Canvas>().overrideSorting = false;
                RighteShadow_panel.GetComponent<Canvas>().overrideSorting = false;
                DoubleBetting_Table.GetComponent<Canvas>().overrideSorting = true;
                LeftShadow_panel.GetComponent<Canvas>().overrideSorting = true;
                CancelBets_Left.SetActive(true);
                CancelSpecificBets_Left.SetActive(true);
                _playdouble = false;
            }
            if(_tripletable_zoomout == true)
            {
                TripleBetting_Table.GetComponent<Canvas>().overrideSorting = false;
                RighteShadow_panel.GetComponent<Canvas>().overrideSorting = false;
                TripleZoomOut_table();
            }
        }

        public void DoubleZoomOut_table()
        {
            _doubletable_zoomInOut = false;
            if(_doubletable_zoomInOut == false)
            {
                CancelBets_Left.SetActive(false);
                CancelSpecificBets_Left.SetActive(false);
                LeftShadow_panel.SetActive(false);
                DoubleBetting_Table.GetComponent<Animation>().Play("DoubleBet_tableZoomOut");
                DoubleBetting_Table.GetComponent<Canvas>().overrideSorting = false;
                LeftShadow_panel.GetComponent<Canvas>().overrideSorting = false;
            }
        }

        public void RepeatBets()
        {
            int __totalBets =
                CopyArray_Single_Bets_Container.Sum() +
                CopyArray_Double_Bets_Container.Sum();
            if (!isdataLoaded)
            {
                //AndroidToastMsg.ShowAndroidToastMessage("please wait");
                message.text ="Please wait";
                return;
            }
            if (!canPlaceBet || isTimeUp) return;
            if (currentlySelectedChip == 0)
            {
                //AndroidToastMsg.ShowAndroidToastMessage("please select a chip first");
                message.text = "Please select a chip first";
                return;
            }

            if (balance < __totalBets)
            {
                //AndroidToastMsg.ShowAndroidToastMessage("not enough balance");
                message.text = "not enough balance";
                return;
            }
            // Single_Bets_Container = CopyArray_Single_Bets_Container;
            // Double_Bets_Container = CopyArray_Double_Bets_Container;
            for (int i = 0; i < Double_Bets_Container.Length; i++)
            {
                Double_Bets_Container[i] += Previous_Double_Bets_Container[i];
            }

            for (int i = 0; i < Single_Bets_Container.Length; i++)
            {
                Single_Bets_Container[i] += Previous_Single_Bets_Container[i];
            }

            for (int i = 0; i < Triple_Bets_Container.Length; i++)
            {
                Triple_Bets_Container[i] += Triple_Bets_Container[i];
            }   
            UpdateUi();
        }

        
        List<int> tripleBets_keys = new List<int>();
        List<int> doubleBets_keys = new List<int>();
        List<int> singleBets_keys = new List<int>();
        List<int> tripleValues_keys = new List<int>();
        [SerializeField]List<int> doubleValues_keys = new List<int>();
        List<int> singleValues_keys = new List<int>();
        [SerializeField]int[] tripleBets_Array, doubleBets_Array, singleBets_Array;
        [SerializeField]int[] tripleValues_Array, doubleValues_Array, singleValues_Array;

        private void SendBets()
        {
            Debug.Log("the data has been stored and the bets ahev been placed");
            
            //closebtn.interactable = false;
            if(totalBets > 0)
            {
                store();
            }
            
           //Debug.Log("bets send");
            for(int i = 0; i < Double_Bets_Container.Length; i++)
            {
                if(  Double_Bets_Container[i] > 0 )
                {
                    //doubleBets_keys.Add(i);
                    doubleBets_keys.Add(int.Parse(doubleGridBtns[i].gameObject.name));
                    //Debug.Log("The value is"+Double_Bets_Container[i]);
                }
            }
            doubleBets_Array = doubleBets_keys.ToArray();

            for(int i = 0; i < Double_Bets_Container.Length; i++)
            {
                if(  Double_Bets_Container[i] > 0 )
                {
                    doubleValues_keys.Add(Double_Bets_Container[i]);
                    //doubleValues_keys.Add(int.Parse(doubleGridBtns[i].gameObject.name));
                }
            }
            doubleValues_Array = doubleValues_keys.ToArray();

            for(int i = 0; i < Single_Bets_Container.Length; i++)
            {
                if(  Single_Bets_Container[i] > 0 )
                {
                    singleBets_keys.Add(i);
                }
            }
            singleBets_Array = singleBets_keys.ToArray();

            for(int i = 0; i < Single_Bets_Container.Length; i++)
            {
                if( Single_Bets_Container[i] > 0 )
                {
                    singleValues_keys.Add(Single_Bets_Container[i]);
                }
                /*if(  Single_Bets_Container[i] > 0 )
                {
                    singleValues_keys.Add(Single_Bets_Container[i]);
                }*/
            }
            
            singleValues_Array = singleValues_keys.ToArray();
            for(int i = 0;i < singleValues_Array.Count<int>();i++)
            {
                //Debug.Log("valuuuueeeeeeeee" + singleValues_Array[i]);
            }
            
        
            var keycollection = TripleBets.Keys;
            var valueCollection = TripleBets.Values;
            foreach(var key in keycollection)
            {
                if(TripleBets[key] > 0)
                {
                    tripleBets_keys.Add(int.Parse(tripleGridBtns[key].gameObject.name));
                    tripleValues_keys.Add(TripleBets[key]);
                }
            }
            tripleBets_Array = tripleBets_keys.ToArray();
            tripleValues_Array = tripleValues_keys.ToArray();

            if (totalBets == 0)
            {
                currentComment = commenstArray[0];
                UpdateUi();
                return;
            }
            string _playerName = "GK" + PlayerPrefs.GetString("email"); 
            //Debug.Log(singleValues_Array);

            bets data = new bets
            {

                playerId = _playerName,
                single = singleBets_Array,
                doubleNo = doubleBets_Array,
                triple = tripleBets_Array,
                singleVal = singleValues_Array,//Single_Bets_Container.ToArray<int>(), //ingleValues_Arrays,
                doubleVal = doubleValues_Array,
                tripleVal = tripleValues_Array,
                totalsingleVal = Single_Bets_Container.Sum(),
                totaldoubleVal = Double_Bets_Container.Sum(),
                totaltripleVal = triplevalue.Sum(),
                // points = Single_Bets_Container.Sum() + Double_Bets_Container.Sum() + TripleBets.Values.Sum()
                points = totalBets

            };
            Debug.Log("THE BETS ARE"+new JSONObject(JsonConvert.SerializeObject(data)).ToString());
            TripleChance_ServerResponse.instance.socket.Emit(Utility.Events.OnBetsPlaced, new JSONObject(JsonConvert.SerializeObject(data)));
            //PostBet(data);
            for (int i = 0; i < doubleValues_Array.Length; i++)
            {  
                Debug.Log("value at" +i+":"+doubleValues_Array[i]);
                
            }
            
            //Debug.Log("single"+singleBets_Array+" double"+doubleBets_Array+" triple"+tripleBets_Array+" singleval"+Single_Bets_Container.ToArray<int>()+" doubleval"+doubleValues_Array+" tripleval"+tripleValues_Array);
            //singleBets_Array = new int[0];
            //singleValues_Array = new int[0];
            singleBets_Array = singleValues_Array = doubleValues_Array = tripleValues_Array = doubleBets_Array  = Array.Empty<int>();
            doubleValues_keys.Clear(); 
            doubleBets_keys.Clear();
            singleValues_keys.Clear();
            singleBets_keys.Clear();
            canPlaceBet = false;
        }
        public void leavetheroom()
        {
            TripleChance_ServerResponse.instance.socket.Emit(Utility.Events.onleaveRoom);
        }

        private void PostBet(bets data)
        {
            // for(int i = 0; i < data.single.Length; i++)
            // {
            //     Debug.LogError("data single...   " + data.single[i]);
            // }

            // for(int i = 0; i < data.doubleNo.Length; i++)
            // {
            //     Debug.LogError("data double...   " + data.doubleNo[i]);
            // }

            // for(int i = 0; i < data.triple.Length; i++)
            // {
            //     Debug.LogError("data triple...   " + data.triple[i]);
            // }

            // for(int i = 0; i < data.singleVal.Length; i++)
            // {
            //     Debug.LogError("data single val...   " + data.singleVal[i]);
            // }

            // for(int i = 0; i < data.doubleVal.Length; i++)
            // {
            //     Debug.LogError("data double. val  ..   " + data.doubleVal[i]);
            // }

            // for(int i = 0; i < data.tripleVal.Length; i++)
            // {
            //     Debug.LogError("data triple Valye...   " + data.tripleVal[i]);
            // }

            /*TripleChance_ServerRequest.intance.SendEvent(Utility.Events.OnBetsPlaced, data, (res) =>
            {
                Debug.Log("bets res   " + res );
                var response = JsonConvert.DeserializeObject< DoubleChanceClasses.BetConfirmation>(res);
                // if (response == null)
                // {
                //     return;
                // }
                // if (Utility.Events.IS_INVALID_USER == response.status)
                // {
                //     return;
                // }
                // if (response.status == "200") 
                // {
                //     // balance -= totalBets; 
                //     isBetConfirmed = true;
                //     Debug.LogError("balance   " + balance + "  totalbets  " + totalBets); 
                // }
                // else
                // {
                //     Debug.LogError("error in upload... ");
                // }
                // // currentComment = response.message;
                // CopyArray_Single_Bets_Container = Single_Bets_Container;
                // CopyArray_Double_Bets_Container = Double_Bets_Container;
                // UpdateUi();
                Debug.Log("is bet placed starus with statu - " + JsonUtility.FromJson<DoubleChanceClasses.BetConfirmation>(res).status);

            });*/

        }

        //void OnClickOnDoubleBetBtn()
        //{
        //    int _totalBets_value = Double_Bets_Container.Sum() + Single_Bets_Container.Sum();
        //    if (_totalBets_value == 0)
        //    {

        //        m.print("no bet placed yet");
        //        AndroidToastMsg.ShowAndroidToastMessage("no bet placed yet");
        //        return;
        //    }
        //    bool isEnoughBalance = balance > _totalBets_value * 2;

        //    if (!isEnoughBalance)
        //    {
        //        m.print("not enough balance");
        //        AndroidToastMsg.ShowAndroidToastMessage("not enough balance");
        //        return;
        //    }

        //    bool isRichedTheLimit = _totalBets_value * 2 > SINGLE_BETS_LIMIT;

        //    if (isRichedTheLimit)
        //    {
        //        m.print("reached the limit");
        //        AndroidToastMsg.ShowAndroidToastMessage("reached the limit");
        //        return;
        //    }

        //    for (int i = 0; i < Double_Bets_Container.Length; i++)
        //    {
        //        Double_Bets_Container[i] *= 2;
        //    }

        //    for (int i = 0; i < Single_Bets_Container.Length; i++)
        //    {
        //        Single_Bets_Container[i] *= 2;
        //    }

        //    for (int i = 0; i < doubleGridBtns.Length; i++)
        //    {
        //        doubleGridBtns[i].transform.GetChild(0).GetComponent<TMP_Text>().text = Double_Bets_Container[i].ToString() == "0" ? string.Empty : Double_Bets_Container[i].ToString();
        //    }
        //    for (int i = 0; i < singleGridBtns.Length; i++)
        //    {
        //        singleGridBtns[i].transform.GetChild(0).GetComponent<TMP_Text>().text = Single_Bets_Container[i].ToString() == "0" ? string.Empty : Single_Bets_Container[i].ToString();
        //    }

        //    totalBets = _totalBets_value;
        //    SoundManager.instance.PlayClip("addbet");
        //    UpdateUi();
        //}

        void OnBetsOk()
        {
            // if (totalBets == 0)
            // {
            //     // commentTxt.text = "Bets Are Empty";
            //     return;
            // }
            if (isTimeUp)
            {
                //AndroidToastMsg.ShowAndroidToastMessage("Time UP");
                message.text = "Time UP";
                return;
            }
            //BettingButtonInteractablity(false);
            // commentTxt.text = "Bets Confirmed";
            //clearBtn.interactable = false;
            message.text = "Bets have been placed";
            betOkBtn.interactable = false;
            //doubleBtn.interactable = false;
            //repeatBtn.interactable = false;
            //  balance -= totalBets;
            isBetConfirmed = true;
            if(spinnow)
            {
                SendBets();
                StopBetokBlink();
            }
            if(lefttop.localScale.x >1)
            {
                lefttop.GetComponent<Animator>().Play("matkatopleftout");
            }
            if(rightop.localScale.x >1)
            {
                rightop.GetComponent<Animator>().Play("matkatoprightout");
            }
            if(leftbottom.localScale.x >1)
            {
                leftbottom.GetComponent<Animator>().Play("matkaleftout");
            }
            if(rightbottom.localScale.x >1)
            {
                rightbottom.GetComponent<Animator>().Play("matkarightlowout");
            }
            
            UpdateUi();
        }

        public void BetsBtn(int _betsVal)
        {
            currentmultiple = _betsVal;
            //Debug.Log("cisdfvgbhnm" + currentmultiple);
            SelectedBets_section = _betsVal;
            for (int i = 0; i < tripleGridBtns.Length; i++)
            {
                if(_betsVal == 0)
                {
                    if(i <10)
                    {
                        //tripleGridBtns[i].transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = (("00"+ i).ToString());
                    }
                    else{
                        //tripleGridBtns[i].transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = (("0"+ i).ToString());
                    }
                    
                }
                else
                {
                    //tripleGridBtns[i].transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = ((_betsVal + i).ToString());
                }
            }
            UpdateUi();
        }

        public GameObject betokpink;
        public GameObject prevbet;
        public GameObject prevbetpink;
        private void Update()
        {
            
           
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                if(Application.platform == RuntimePlatform.Android)
                {
                    //StartCoroutine(settingoffline());
                    TripleFunGameManager.instance.close();
                    PlayerPrefs.SetString("triplepaused", "false");
                    
                    //SceneManager.LoadScene(1);
                }
            }
         
            /*Mulitple100.onClick.AddListener(() =>
            {
                Debug.Log("CUrrent" + currentmultiple);
                currentmultiple = 100;
                for (int i = 0; i < tripleGridBtns.Length; i++)
                {
                    tripleGridBtns[i].transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = ((100 + i).ToString());
                }
            });

            Mulitple200.onClick.AddListener(() =>
            {
                currentmultiple = 200;
                Debug.Log("CUrrent" + currentmultiple);
                for (int i = 0; i < tripleGridBtns.Length; i++)
                {
                    tripleGridBtns[i].transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = ((200 + i).ToString());
                }
            });
            Mulitple300.onClick.AddListener(() =>
            {
                currentmultiple = 300;
                Debug.Log("CUrrent" + currentmultiple);
                for (int i = 0; i < tripleGridBtns.Length; i++)
                {
                    tripleGridBtns[i].transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = ((300 + i).ToString());
                }
            });
            Mulitple400.onClick.AddListener(() =>
            {
                currentmultiple = 400;
                Debug.Log("CUrrent" + currentmultiple);
                for (int i = 0; i < tripleGridBtns.Length; i++)
                {
                    tripleGridBtns[i].transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = ((400 + i).ToString());
                }
            });
            Mulitple500.onClick.AddListener(() =>
            {
                currentmultiple = 500;
                Debug.Log("CUrrent" + currentmultiple);
                for (int i = 0; i < tripleGridBtns.Length; i++)
                {
                    tripleGridBtns[i].transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = ((500 + i).ToString());
                }
            });
            Mulitple600.onClick.AddListener(() =>
            {
                currentmultiple = 600;
                Debug.Log("CUrrent" + currentmultiple);
                for (int i = 0; i < tripleGridBtns.Length; i++)
                {
                    tripleGridBtns[i].transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = ((600 + i).ToString());
                }
            });

            Mulitple700.onClick.AddListener(() =>
            {
                currentmultiple = 700;
                Debug.Log("CUrrent" + currentmultiple);
                for (int i = 0; i < tripleGridBtns.Length; i++)
                {
                    tripleGridBtns[i].transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = ((700 + i).ToString());
                }
            }); Mulitple800.onClick.AddListener(() =>
            {
                currentmultiple = 800;
                Debug.Log("CUrrent" + currentmultiple);
                for (int i = 0; i < tripleGridBtns.Length; i++)
                {
                    tripleGridBtns[i].transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = ((800 + i).ToString());
                }
            }); Mulitple900.onClick.AddListener(() =>
            {
                currentmultiple = 900;
                Debug.Log("CUrrent" + currentmultiple);
                for (int i = 0; i < tripleGridBtns.Length; i++)
                {
                    tripleGridBtns[i].transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = ((900 + i).ToString());
                }
            });*/
        }
        public void setprevious(List<int> single,List<int> doubble,List<int> tripple)
        {
            Debug.LogError("initial value assigned");
            while(single.Count>5)
            {
                single.RemoveAt(0);
            }
            while(doubble.Count>5)
            {
                doubble.RemoveAt(0);
            }
            while(tripple.Count>10)
            {
                tripple.RemoveAt(0);
            }
            //Debug.Log("initial value assigned" + single.Count+"///"+doubble.Count+"///"+tripple.Count);
            for (int i = 0; i < doubble.Count; i++) 
            {
                PreviousWin_doubleLocal.Add(doubble[i]);
            }
            for (int i = 0; i < single.Count; i++)
            {
                PreviousWin_singleLocal.Add(single[i]);
            }
            for (int i = 0; i < tripple.Count; i++)
            {
                PreviousWin_tripleLocal.Add(tripple[i]);
            }
            for(int j = 0; j < PreviousWin_singleLocal.Count; j++)
            {
                TripleFunWheel.instance.PreviousWin_Single.Add(PreviousWin_singleLocal[j]);
            }

            for(int j = 0; j < PreviousWin_doubleLocal.Count; j++)
            {
                TripleFunWheel.instance.PreviousWin_Double.Add(PreviousWin_doubleLocal[j]);
                
            }

            for(int j = 0; j < PreviousWin_tripleLocal.Count; j++)
            {
                //TripleFunWheel.instance.PreviousWin_Triple.Add(PreviousWin_tripleLocal[j]);
                TripleFunWheel.instance.PreviousWin_Triple.Add(PreviousWin_tripleLocal[j]);
            }
            int temp = PreviousWin_tripleLocal[PreviousWin_tripleLocal.Count-1];
            int onesPlace = temp % 10;
            int tensPlace = (temp / 10) % 10;
            int hundredsPlace = (temp / 100) % 10;
            //int thousandsPlace = temp / 1000;
            Debug.Log("1:" +onesPlace + "10"+tensPlace+"onehundred"+hundredsPlace);

            TripleFunWheel.instance.SetLastFive();
            TripleFunWheel.instance.setinitialrotation(onesPlace,tensPlace,hundredsPlace);
            WinNo_txt.text = temp.ToString();
        }

        void AddSocketListners()
        {
           Action onBadResponse = () => {  };

            
            // TripleChance_ServerRequest.intance.ListenEvent<weelNumbers>( Utility.Events.OnWinNo, (json) =>
            // {
            //     StartCoroutine(OnRoundEnd(json.ToString()));
            // }, onBadResponse);

            // TripleChance_ServerRequest.intance.ListenEvent(Utility.Events.OnWinNo, (json) => 
            // {
            //     // Debug.LogError("onwin  " + json.ToString());
            //     StartCoroutine(OnRoundEnd(json));
            // });

            // TripleChance_ServerRequest.intance.ListenEvent(Utility.Events.OnTimerStart, (json) =>
            // {
            //     Enable_OverrideSorting();
            //     // Debug.LogError("timer  " + json.ToString());
            //     OnTimerStart(json);
            // });

            // TripleChance_ServerRequest.intance.ListenEvent<winAmount>(Utility.Events.OnWinAmount, (json) =>
            // {
            //     Debug.Log("Recieved win amount " + json);
            //     StartCoroutine(ShowWinAmount(json.win_points));

            // }, onBadResponse);

            // SocketRequest.intance.ListenEvent(Utility.Events.OnDissconnect, (json) =>
            // {
            //     print("dissconnected");
            // });
            // TripleChance_ServerRequest.intance.ListenEvent<DoubleChanceClasses.OnWinAmount>(Utility.Events.OnWinNo, (json) =>
            // {
            //     OnWinAmount(json.ToString());
            // }, onBadResponse);


            TripleChance_ServerRequest.intance.ListenEvent(Utility.Events.OnTimeUp, (json) =>
            {
                //BettingButtonInteractablity(false);
                isTimeUp = true;
            });

        }

        public void addrow(int row)
        {
            Debug.Log("Row selected");
            if (isBetConfirmed == false)
            {
                // _tripletable_zoomout = true;
                // TripleZoomIn_table();
                if(row == 0)
                {
                    for (int i = 0; i < 10; i++)
                    {
                        OnClickTripleBet = int.Parse(tripleGridBtns[i].transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text);
                        AddBets(ref Triple_Bets_Container,i,3);
                    }
    
                }
                if(row == 1)
                {
                    for (int i = 10; i < 20; i++)
                    {
                        OnClickTripleBet = int.Parse(tripleGridBtns[i].transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text);
                        AddBets(ref Triple_Bets_Container,i,3);
                    }
                }
                if(row == 2)
                {
                    for (int i = 20; i < 30; i++)
                    {
                        OnClickTripleBet = int.Parse(tripleGridBtns[i].transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text);
                        AddBets(ref Triple_Bets_Container,i,3);
                    }
                }
                if(row == 3)
                {
                    for (int i = 30; i < 40; i++)
                    {
                        OnClickTripleBet = int.Parse(tripleGridBtns[i].transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text);
                        AddBets(ref Triple_Bets_Container,i,3);
                    }
                }
                if(row == 4)
                {
                    for (int i = 40; i < 50; i++)
                    {
                        OnClickTripleBet = int.Parse(tripleGridBtns[i].transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text);
                        AddBets(ref Triple_Bets_Container,i,3);
                    }
                }
                if(row == 5)
                {
                    for (int i = 50; i < 60; i++)
                    {
                        OnClickTripleBet = int.Parse(tripleGridBtns[i].transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text);
                        AddBets(ref Triple_Bets_Container,i,3);
                    }
                }
                if(row == 6)
                {
                    for (int i = 60; i < 70; i++)
                    {
                        OnClickTripleBet = int.Parse(tripleGridBtns[i].transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text);
                        AddBets(ref Triple_Bets_Container,i,3);
                    }
                }
                if(row == 7)
                {
                    for (int i = 70; i < 80; i++)
                    {
                        OnClickTripleBet = int.Parse(tripleGridBtns[i].transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text);
                        AddBets(ref Triple_Bets_Container,i,3);
                    }
                }
                if(row == 8)
                {
                    for (int i = 80; i < 90; i++)
                    {
                        OnClickTripleBet = int.Parse(tripleGridBtns[i].transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text);
                        AddBets(ref Triple_Bets_Container,i,3);
                    }
                }
                if(row == 9)
                {
                    for (int i = 90; i < 100; i++)
                    {
                        OnClickTripleBet = int.Parse(tripleGridBtns[i].transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text);
                        AddBets(ref Triple_Bets_Container,i,3);
                    }
                }
            }

        }

        public void double_addrow(int row)
        {
            Debug.Log("Row selected");
            if (isBetConfirmed == false)
            {
                if(row == 10)
                {
                    for (int i = 0; i < 10; i++)
                    {
                        OnClickTripleBet = int.Parse(doubleGridBtns[i].transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text);
                        AddBets(ref Double_Bets_Container,i,2);
                    }
    
                }
                if(row == 11)
                {
                    for (int i = 10; i < 20; i++)
                    {
                        OnClickTripleBet = int.Parse(doubleGridBtns[i].transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text);
                        AddBets(ref Double_Bets_Container,i,2);
                    }
                }
                if(row == 12)
                {
                    for (int i = 20; i < 30; i++)
                    {
                        OnClickTripleBet = int.Parse(doubleGridBtns[i].transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text);
                        AddBets(ref Double_Bets_Container,i,2);
                    }
                }
                if(row == 13)
                {
                    for (int i = 30; i < 40; i++)
                    {
                        OnClickTripleBet = int.Parse(doubleGridBtns[i].transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text);
                        AddBets(ref Double_Bets_Container,i,2);
                    }
                }
                if(row == 14)
                {
                    for (int i = 40; i < 50; i++)
                    {
                        OnClickTripleBet = int.Parse(doubleGridBtns[i].transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text);
                        AddBets(ref Double_Bets_Container,i,2);
                    }
                }
                if(row == 15)
                {
                    for (int i = 50; i < 60; i++)
                    {
                        OnClickTripleBet = int.Parse(doubleGridBtns[i].transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text);
                        AddBets(ref Double_Bets_Container,i,2);
                    }
                }
                if(row == 16)
                {
                    for (int i = 60; i < 70; i++)
                    {
                        OnClickTripleBet = int.Parse(doubleGridBtns[i].transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text);
                        AddBets(ref Double_Bets_Container,i,2);
                    }
                }
                if(row == 17)
                {
                    for (int i = 70; i < 80; i++)
                    {
                        OnClickTripleBet = int.Parse(doubleGridBtns[i].transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text);
                        AddBets(ref Double_Bets_Container,i,2);
                    }
                }
                if(row == 18)
                {
                    for (int i = 80; i < 90; i++)
                    {
                        OnClickTripleBet = int.Parse(doubleGridBtns[i].transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text);
                        AddBets(ref Double_Bets_Container,i,2);
                    }
                }
                if(row == 19)
                {
                    for (int i = 90; i < 100; i++)
                    {
                        OnClickTripleBet = int.Parse(doubleGridBtns[i].transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text);
                        AddBets(ref Double_Bets_Container,i,2);
                    }
                }
            }




        }
        //void columbets()
        public void columnbets(int column)
        {
           if (!isBetConfirmed)
           {
                Debug.Log("Selecting Column");

                if (column == 0)
                {
                    int i=0;
                    while (i<100)
                    {
                        OnClickTripleBet = int.Parse(tripleGridBtns[i].transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text);
                        AddBets(ref Triple_Bets_Container,i,3);
                        i = i+ 10;
                    }
                    Debug.Log("Column " + i + " selected");


                }
                if (column == 1)
                {
                    int i = 1;
                    while (i<100)
                    {
                        OnClickTripleBet = int.Parse(tripleGridBtns[i].transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text);
                        AddBets(ref Triple_Bets_Container,i,3);
                        i = i+ 10;
                    }
                    Debug.Log("Column " + i + " selected");

                }
                if (column == 2)
                {
                    int i = 2;
                    while (i<100)
                    {
                     OnClickTripleBet = int.Parse(tripleGridBtns[i].transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text);
                     AddBets(ref Triple_Bets_Container,i,3);
                     i = i+ 10;
                    }
                    Debug.Log("Column "+i+" selected");

                }
                if (column == 3)
                {
                 int i = 3;
                 while (i<100)
                 {
                     OnClickTripleBet = int.Parse(tripleGridBtns[i].transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text);
                     AddBets(ref Triple_Bets_Container,i,3);
                     i = i+ 10;
                 }
                    Debug.Log("Column "+i+" selected");

                }
                if (column == 4)
                {
                    int i = 4;
                    while (i<100)
                    {
                        OnClickTripleBet = int.Parse(tripleGridBtns[i].transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text);
                        AddBets(ref Triple_Bets_Container,i,3);
                        i = i+ 10;
                    }
                    Debug.Log("Column "+i+" selected");

                }
                if (column == 5)
                {
                    int i = 5;
                    while (i<100)
                    {
                        OnClickTripleBet = int.Parse(tripleGridBtns[i].transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text);
                        AddBets(ref Triple_Bets_Container,i,3);
                        i = i+ 10;
                     }
                    Debug.Log("Column "+i+" selected");

                }
                if (column == 6)
                {
                    int i = 6;
                    while (i<100)
                    {
                        OnClickTripleBet = int.Parse(tripleGridBtns[i].transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text);
                        AddBets(ref Triple_Bets_Container,i,3);
                        i = i+ 10;
                    }
                    Debug.Log("Column "+i+" selected");

                }
                if (column == 7)
                {
                    int i = 7;
                    while (i<100)
                    {
                        OnClickTripleBet = int.Parse(tripleGridBtns[i].transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text);
                        AddBets(ref Triple_Bets_Container,i,3);
                        i = i+ 10;
                    }
                    Debug.Log("Column "+i+" selected");

                }
                if (column == 8)
                {   
                    int i = 8;
                    while (i<100)
                    {
                        OnClickTripleBet = int.Parse(tripleGridBtns[i].transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text);
                        AddBets(ref Triple_Bets_Container,i,3);
                         i = i+ 10;
                    }
                    Debug.Log("Column "+i+" selected");

                }
                if (column == 9)
                {   
                    int i = 9;
                    while (i<100)
                    {
                        OnClickTripleBet = int.Parse(tripleGridBtns[i].transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text);
                        AddBets(ref Triple_Bets_Container,i,3);
                        i = i+ 10;
                    }
                    Debug.Log("Column "+i+" selected");

                }
            }

        }

        public void LoremIpsum()
        {
            Debug.Log("Lorem Ipsum");
        }

        public void double_columnbets(int column)
        {
            Debug.Log("Selecting column before confirming no bet");
            if (!isBetConfirmed)
            {
                Debug.Log("Selecting Column");
                if(column == 0)
                {
                    int i=0;
                    while (i<100)
                    {
                        OnClickTripleBet = int.Parse(doubleGridBtns[i].transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text);
                        AddBets(ref Double_Bets_Container,i,2);
                        i = i+ 10;
                    }
                    Debug.Log("Column "+i+" selected");
                }
                if(column == 1)
                {
                    int i = 1;
                    while (i<100)
                    {
                        OnClickTripleBet = int.Parse(doubleGridBtns[i].transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text);
                        AddBets(ref Double_Bets_Container,i,2);
                        i = i+ 10;
                    }
                    Debug.Log("Column " + i + " selected");

                }
                if (column == 2)
                {
                    int i = 2;
                    while (i<100)
                    {
                        OnClickTripleBet = int.Parse(doubleGridBtns[i].transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text);
                        AddBets(ref Double_Bets_Container,i,2);
                        i = i+ 10;
                    }
                    Debug.Log("Column "+i+" selected");

                }
                if (column == 3)
                {
                    int i = 3;
                    while (i<100)
                    {
                        OnClickTripleBet = int.Parse(doubleGridBtns[i].transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text);
                        AddBets(ref Double_Bets_Container,i,2);
                        i = i+ 10;
                    }
                    Debug.Log("Column "+i+" selected");

                }
                if (column == 4)
                {
                    int i = 4;
                    while (i<100)
                    {
                        OnClickTripleBet = int.Parse(doubleGridBtns[i].transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text);
                        AddBets(ref Double_Bets_Container,i,2);
                        i = i+ 10;
                    }
                    Debug.Log("Column "+i+" selected");

                }
                if (column == 5)
                {
                    int i = 5;
                    while (i<100)
                    {
                        OnClickTripleBet = int.Parse(doubleGridBtns[i].transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text);
                        AddBets(ref Double_Bets_Container,i,2);
                        i = i+ 10;
                    }
                    Debug.Log("Column "+i+" selected");

                }
                if (column == 6)
                {
                    int i = 6;
                    while (i<100)
                    {
                        OnClickTripleBet = int.Parse(doubleGridBtns[i].transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text);
                        AddBets(ref Double_Bets_Container,i,2);
                        i = i+ 10;
                    }
                    Debug.Log("Column "+i+" selected");

                }
                if (column == 7)
                {
                    int i = 7;
                    while (i<100)
                    {
                        OnClickTripleBet = int.Parse(doubleGridBtns[i].transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text);
                        AddBets(ref Double_Bets_Container,i,2);
                        i = i+ 10;
                    }
                    Debug.Log("Column "+i+" selected");

                }
                if (column == 8)
                {   
                    int i = 8;
                    while (i<100)
                    {
                        OnClickTripleBet = int.Parse(doubleGridBtns[i].transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text);
                        AddBets(ref Double_Bets_Container,i,2);
                        i = i+ 10;
                    }
                    Debug.Log("Column "+i+" selected");

                }
                if (column == 9)
                {   
                    int i = 9;
                    while (i<100)
                    {
                        OnClickTripleBet = int.Parse(doubleGridBtns[i].transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text);
                        AddBets(ref Double_Bets_Container,i,2);
                        i = i+ 10;
                    }
                    Debug.Log("Column "+i+" selected");

                }
            }

        }

        
        public IEnumerator Enable_OverrideSorting()
        {
            yield return new WaitForSeconds(2.0f);
            TripleBetting_Table.GetComponent<Canvas>().overrideSorting = true;
            RighteShadow_panel.GetComponent<Canvas>().overrideSorting = true;

            DoubleBetting_Table.GetComponent<Canvas>().overrideSorting = true;
            LeftShadow_panel.GetComponent<Canvas>().overrideSorting = true;
        }

        void OnWinAmount(string res)
        {
           DoubleChanceClasses.OnWinAmount o = JsonConvert.DeserializeObject<DoubleChanceClasses.OnWinAmount>(res.ToString());
        }

        #region GAME_FLOW

        IEnumerator ShowWinAmount(int winAmount)
        {
            Debug.Log("timer started");
            yield return new WaitUntil(() => currentTime < 56 && currentTime > 50);
            if (winAmount == 0)
            {
                commentTxt.text = "No Wins";
                yield break;
            }
            commentTxt.text = "You Won:" + winAmount;
            winTxt.text = "Wins:" + winAmount;
        }

        void SendTakeAmountRequest()
        {
            if(spinnow)
            {
                return;
            }
            StopTakeBlink();
            Repeatbtn.gameObject.SetActive(true);
            Repeatbtn.interactable = true;
            balance = balance + winnerAmount;
            balanceTxt.text = balance.ToString("F2");
            string _playername = "GK" + PlayerPrefs.GetString("email");
            //object o = new { playerId = _playername , win_points = winnerAmount};
            StartCoroutine(coinparcel());
            Take_Bet data = new Take_Bet()
            {
                playerId = "GK"+PlayerPrefs.GetString("email"),
                winpoint = winnerAmount
            };
            //winnerAmount = 0; 
            //winTxt.text = winnerAmount.ToString();
            //TripleChance_ServerRequest.intance.socket.Emit(Utility.Events.OnWinAmount, new JSONObject (JsonConvert.SerializeObject(o) ));
            //TripleChance_ServerRequest.intance.socket.Emit(Triple_Util.TF_Events.OnWinAmount, new JSONObject (JsonConvert.SerializeObject(data) ));
            ResetAllBets();
            //changetriple(triple);
            StartCoroutine(emptytriplewinresponse());
            TakeBtn.interactable = false;
            //spinnow = true;
            
            // TripleChance_ServerRequest.intance.SendEvent( Utility.Events.OnWinAmount, o, (res) =>
            // {
                // WinAmountConfirmation winAmountConfirmation = JsonConvert.DeserializeObject<WinAmountConfirmation>(res);
                // if (winAmountConfirmation.status)
                // {
                //     // LastWinImg.gameObject.SetActive(false);
                //     betOkBtn.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "BET OK";
                //     canPlaceBet = true;
                //     isLastGameWinAmountReceived = true;
                //     Debug.Log("Amount Successfully Added");
                //     // currentComment = "Amount Successfully Added";
                //     balance = winAmountConfirmation.data.win_points;
                //     winningAmount = string.Empty;

                //     // if (previousBet.Sum() > 0)
                //     //     betOkBtn.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Pre";
                // }
                // else
                // {
                //     // currentComment = winAmountConfirmation.message;
                //     Debug.Log("error msg:   " + winAmountConfirmation.message);
                // }

            //     UpdateUi();
            // });
        }

        float partialwin;
        
        public void testing()
        {
            int i =1;
            winnerAmount = 50000;
            // if(winningAmount > 50000)
            // {
            //     partialwin = 0.1f/winningAmount;
            // }
            
            
            //Debug.Log(partialwin + "dfgd" + i);
            
            //partialwin = winningAmount/500f;
            //StartCoroutine(coinanimation());
        }
        // IEnumerator coinanimation()
        // {
        //     float elapsedTime = 0f;
        //     float deductionPercentage = 0.1f;
        //     while (elapsedTime <4f)
        //     {
        //         float deductionValue = (winnerAmount * deductionPercentage);
        //         //_7updown_UiHandler.instance
        //             // Deduct the value from the current variable
        //         winnerAmount -= Mathf.RoundToInt(deductionValue);
        //         balance += deductionValue;
        //         balanceTxt.text = balance.ToString();
        //         Winner_text.text = winnerAmount.ToString();
                    
        //             // Make sure we don't go below zero
        //         //winPoint = Mathf.Max(winPoint, 0);
    
        //         yield return null;
        //         elapsedTime += Time.deltaTime;
                
        //     }
        //     balance = winround;//+= winPoint;
        //     winnerAmount = 0;
        //     balanceTxt.text = balance.ToString();
        //     Winner_text.text = winnerAmount.ToString();
            

            
        //     elapsedTime += Time.deltaTime;
        //     // partialwin = winningAmount;
        //     // while (partialwin < 0)
        //     // {

        //     //     partialwin=-1;
               
        //     // int i =1;
        //     // if(winnerAmount < 50)
        //     // {
        //     //     i = 500;
        //     // }
        //     // else if((winnerAmount >= 50)&& (winnerAmount < 500))
        //     // {
        //     //     i = 5000;
        //     // }
        //     // else if((winnerAmount >= 500)&& (winnerAmount < 5000))
        //     // {
        //     //     i = 50000;
        //     // }
        //     // else if((winnerAmount >= 5000)&& (winnerAmount < 50000))
        //     // {
        //     //     i = 500000;
        //     // }
        //     // else if(winnerAmount>= 50000)
        //     // {
        //     //     i = 5000000;
        //     // }
        //     // partialwin = winnerAmount/(i*i);
            
        //     // while (winnerAmount >0)
        //     // {
                
        //     //     winnerAmount= winnerAmount-1; //- (int)partialwin;
        //     //     balance = balance +1;//+ partialwin;
        //     //     balanceTxt.text = balance.ToString();
        //     //     winTxt.text = winnerAmount.ToString();
        //     //     //coinsaudio.Play();
        //     //     //yield return new WaitForEndOfFrame();
        //     //     yield return new WaitForSeconds(partialwin);
        //     //     //StartCoroutine(coinanimation());
        //     // }
        // }

        int timer_CountDown;
        bool repeated;
        public void OnTimerStart(string o)
        {
            Debug.LogError("/////////"+"reached here");
            TimerClass _timedata = JsonConvert.DeserializeObject<TimerClass>(o);
            timer_CountDown = _timedata.result;
            Debug.LogError("/////////"+timer_CountDown);
            StartCoroutine(Timer(timer_CountDown));
            canPlaceBet = true;
            isBetConfirmed = false;
            //WaitPanel.SetActive(false);
            closebtn.interactable = true;
            PrevBet();
            //closebtn.interactable = true;
            
            //StartCoroutine(GetCurrentTimer());//GetCurrentTimer
            
        }
        public void PrevBet()
        {
            betOkBtn.gameObject.SetActive(false);
            repeated = true;
            StartCoroutine(PrevBetAnim());

        }
        public IEnumerator PrevBetAnim()
        {
            if(spinnow)
            {
                prevbet.SetActive(true);
            }
            // if(repeated)
            // {
            //     //Debug.Log("Executing PrevBetAnim Coroutine...");
            //     prevbet.SetActive(true);
            //     prevbetpink.SetActive(false);
            //     yield return new WaitForSeconds(0.5f);
            //     prevbet.SetActive(false);
            //     prevbetpink.SetActive(true);
            //     yield return new WaitForSeconds(0.5f);
            //     StartCoroutine(PrevBetAnim());
            // }
            yield return null;
        }
        public IEnumerator StopPrevAnim()
        {
            repeated = false;
            StopCoroutine(PrevBetAnim());
            prevbet.SetActive(false);
            prevbetpink.SetActive(false);
            betOkBtn.gameObject.SetActive(true);

            yield return new WaitForSeconds(0.5f);

            prevbetpink.SetActive(false);
        }
        public void SendBets_Response(object data)
        {
            SendBet_Res res = JsonConvert.DeserializeObject<SendBet_Res>(data.ToString());//AndarBahar.Utility.Fuction.GetObjectOfType<SendBet_Res>(data);    
            if (res.status == 200)
            {
                //winnigText.text = res.message;
                balance = res.data.balance;
                balanceTxt.text = balance.ToString("F2");
            }
            else if (res.status == 400)
            {
                //winnigText.text = res.message;
                balance = res.data.balance;
            }
            else{
                Debug.Log("none of the above");
            }
        }

        IEnumerator GetCurrentTimer()
        {
           yield return new WaitUntil(() => currentTime <= 0);
            //StartCoroutine(Timer(timer_CountDown));
        //    TripleChance_ServerRequest.intance.SendEvent(Utility.Events.OnTimerStart, (json) =>
        //    {
        //        print("current timer " + json);
        //        Timer time = JsonConvert.DeserializeObject<Timer>(json);
        //        if (time.result < 10)
        //        {
        //            isTimeUp = true;
        //            BettingButtonInteractablity(false);
        //        };
        //        isdataLoaded = true;
        //        StopCoroutine(Timer());
        //        StartCoroutine(Timer(time.result));
        //    });
        }
        public IEnumerator previousserverdata()
        {
            WWWForm form = new WWWForm();
            string playerId = "GK"+PlayerPrefs.GetString("email");
            form.AddField("playerId",playerId);
            using (UnityWebRequest www = UnityWebRequest.Post(previousurl, form))
            {
                yield return www.SendWebRequest();
                
                if (www.result != UnityWebRequest.Result.Success)
                {
                    Debug.LogError(www.error);
                }
                else
                {
                    Debug.Log(www.downloadHandler.text);
                    PreviousTriple response = JsonConvert.DeserializeObject<PreviousTriple>(www.downloadHandler.text);
                    
                    if(response.status == 200)
                    {
                        repeatclear();
                        for (int i = 0; i < response.data.single.Length; i++)
                        {
                            previous_round_single[i] = response.data.singleVal[i];
                        }
                        for (int i = 0; i < response.data.doubleNo.Length; i++)
                        {
                            previous_round_double[i] = response.data.doubleVal[i];
                        }
                        Debug.Log("the count for triples"+Triplelast.triplebet.Count);
                        for (int i = 0; i < response.data.triple.Length; i++)
                        {
                            if (Triplelast.tripleval[i]>0)
                            {
                                previous_round_triplekey.Add(response.data.triple[i]);
                                previous_round_triplevalue.Add(response.data.tripleVal[i]);
                            }
                        }
                        showui();

                    }
                }
            }
        }
        int currentTime = 0;
        bool canStopTimer;
        int countrounds;
        int minutes;
        int seconds;
        /// <summary>
        /// This is the 60 sec timer 
        /// </summary>
        /// <param name="counter"></param>
        /// <returns></returns>
        public IEnumerator Timer(int counter = 120) //60
        {
            isTimeUp = false;
            //canPlaceBet = true;
        //   repeatBtn.interactable = betOkBtn.interactable = clearBtn.interactable = doubleBtn.interactable = true;
            if(int.Parse(winTxt.text) ==0)
            {
                Repeatbtn.interactable = true;
            }
            else if(int.Parse(winTxt.text) >0)
            {
                TakeBtn.interactable = true;
                Repeatbtn.interactable = false;
            }
            if(counter <10)
            {
                Repeatbtn.interactable = false;
            }
            isUserPlacedBets = false;
            //isBetConfirmed = false;
            //canPlaceBet = true;
            // commentTxt.text = commenstArray[1];
            canStopTimer = false;
            //Debug.Log("timer started");
            //Repeatbtn.interactable =true;
            //betOkBtn.interactable = true;
            // if(counter ==0)
            // {
            //     if(TripleChance_ServerResponse.instance.fromtriplecurrent ==true)
            //     {
            //         //TripleChance_ServerResponse.instance.waitpanel.SetActive(true);
            //         TripleChance_ServerResponse.instance.fromtriplecurrent = false;
            //     }
            // }
            // else{
            //     TripleChance_ServerResponse.instance.fromtriplecurrent = false;
            // }
            while (counter > 0)
            {
                if (canStopTimer) yield break;
                // SecToMin(counter);
                minutes = Mathf.FloorToInt(counter / 60);
                seconds = Mathf.FloorToInt(counter % 60);
                timerTxt.text = counter.ToString();//"0"+ minutes.ToString() + ":" + seconds.ToString();  
                //timerTxt.text = "00:"+counter.ToString();
                timer_AudioSource.GetComponent<AudioSource>().Play();
                // if(counter == 20)
                // {
                //     if(winnerAmount > 0)
                //     {
                //         SendTakeAmountRequest();
                //     }
                // }
                currentTime = counter;
                if(counter == 10)
                {
                    TripleBetting_Table.GetComponent<Canvas>().overrideSorting = false;
                    RighteShadow_panel.GetComponent<Canvas>().overrideSorting = false;
                    DoubleBetting_Table.GetComponent<Canvas>().overrideSorting = false;
                    LeftShadow_panel.GetComponent<Canvas>().overrideSorting = false;
                    // if(_tripletable_zoomout == true)
                    // {
                    //     TripleZoomOut_table();
                    // }
                    // else if(_doubletable_zoomInOut == true)
                    // {
                    //     DoubleZoomOut_table();
                    // }
                    if(lefttop.localScale.x >1)
                    {
                        lefttop.GetComponent<Animator>().Play("matkatopleftout");
                    }
                    if(rightop.localScale.x >1)
                    {
                        rightop.GetComponent<Animator>().Play("matkatoprightout");
                    }
                    if(leftbottom.localScale.x >1)
                    {
                        leftbottom.GetComponent<Animator>().Play("matkaleftout");
                    }
                    if(rightbottom.localScale.x >1)
                    {
                        rightbottom.GetComponent<Animator>().Play("matkarightlowout");
                    }
                }
                if(counter == 2)
                {
                    TripleBetting_Table.GetComponent<Canvas>().overrideSorting = false;
                    RighteShadow_panel.GetComponent<Canvas>().overrideSorting = false;
                    DoubleBetting_Table.GetComponent<Canvas>().overrideSorting = false;
                    LeftShadow_panel.GetComponent<Canvas>().overrideSorting = false;
                    if(_tripletable_zoomout == true)
                    {
                        TripleZoomOut_table();
                    }
                    else if(_doubletable_zoomInOut == true)
                    {
                        DoubleZoomOut_table();
                    }
                    
                    closebtn.interactable = false;
                   
                }
                if (counter == 10)
                {
                //   repeatBtn.interactable = betOkBtn.interactable = clearBtn.interactable = doubleBtn.interactable = false;
                    added = true;
                    //Debug.Log("addded"+added);
                    canPlaceBet = false;
                   
                    if (!isBetConfirmed)
                    {
                        if(spinnow)
                        {
                            SendBets();
                            StopBetokBlink();
                            StartCoroutine(StopPrevAnim());
                        }
                        if(totalBets > 0)
                        {
                            Debug.Log("nothing");
                            //OnBetsOk();
                        }
                        else{
                            countrounds++;
                            //Debug.Log("countrounds");
                            if(countrounds >=3)
                            {
                                throwout();
                                //TripleFunGameManager.instance.close();
                                //close();
                            }
                        }
                    }
                    for(int i = 0; i < FillerList.Count; i++)
                    {
                        TripleFunScreen.Instance.FillerList[i].SetActive(false);
                    }
                }
                yield return new WaitForSeconds(1f);
                counter--;
                // timer_AudioSource.GetComponent<AudioSource>().Play();
            }
            currentTime = 0;
            timerTxt.text = 0.ToString();
        }

        public void zoomallout()
        {
            if(lefttop.localScale.x >1)
            {
                lefttop.GetComponent<Animator>().Play("matkatopleftout");
            }
            if(rightop.localScale.x >1)
            {
                rightop.GetComponent<Animator>().Play("matkatoprightout");
            }
            if(leftbottom.localScale.x >1)
            {
                leftbottom.GetComponent<Animator>().Play("matkaleftout");
            }
            if(rightbottom.localScale.x >1)
            {
                rightbottom.GetComponent<Animator>().Play("matkarightlowout");
            }
        }
        public void throwout()
        {
            PlayerPrefs.DeleteAll();
            TripleChance_ServerRequest.intance.socket.Emit(Utility.Events.onleaveRoom);
            PlayerPrefs.SetInt("tthrownout",1);
            //SceneManager.LoadScene("Login");
        }

        private void SecToMin(int timer)
        {
            float minutes = Mathf.Floor(timer / 60);
            float seconds = Mathf.RoundToInt(timer%60);
            
            if(minutes < 10) {
                Minutes.text = "0" + minutes.ToString();
            }
            if(seconds < 10) {
                Seconds.text = "0" + Mathf.RoundToInt(seconds).ToString();
            }
        }

        public IEnumerator OnRoundEnd(object o)
        {
            //added =true;
            Debug.Log(spinnow);
            //TripleChance_ServerResponse.instance.tripleround++;
            if(!spinnow)
            {
                yield break;
                
            }
            //Debug.Log("bbbbbbbbbbbbbbbbbbbbbbbbbbbbbbb");
            yield return new WaitUntil(() => currentTime == 0);

            timer_AudioSource.GetComponent<AudioSource>().Stop();
            TripleBetting_Table.GetComponent<Canvas>().overrideSorting = false;
            RighteShadow_panel.GetComponent<Canvas>().overrideSorting = false;
            DoubleBetting_Table.GetComponent<Canvas>().overrideSorting = false;
            LeftShadow_panel.GetComponent<Canvas>().overrideSorting = false;

            _playblinkAnim = true;

            var DCW = FindObjectOfType<TripleFunWheel>();
            DiceWinNos windata = JsonConvert.DeserializeObject<DiceWinNos>(o.ToString());

            // int x = UnityEngine.Random.Range(0, 9);
            // int y = UnityEngine.Random.Range(0, 9);
            // int z = UnityEngine.Random.Range(0, 9);
            single = windata.winSingleNo;
            @double = windata.winDoubleNo;
            triple = windata.winTripleNo;
            Debug.Log("the values that are transfered are: "+single+ " double:"+@double+" triple:"+triple);
            StartCoroutine(triplewinresponse());
            // if(triple ==000 || triple ==111 || triple ==222||triple ==333||triple == 444||triple == 555||triple == 666 || triple == 777|| triple == 888 || triple == 999)
            // {
            //     for (int i = 0; i < triplekeys.Count; i++)
            //     {
            //         if(triplekeys[i] == triple)
            //         {
            //             winnerAmount = triplevalue[i] *900;
            //         }
            //     }
            // }
            // winnerAmount += windata.winPoint;
            // winround = balance+winnerAmount;
            if(winnerAmount > 0)
            {
                StartCoroutine(TakeBlinkAnim());
            }
            Repeatbtn.interactable = false;
            
            // PreviousWin_singleLocal.Reverse();
            TripleFunWheel.instance.PreviousWin_Single.Clear();
            added = false;
            while(windata.previousWins.Count > 5)
            {
                windata.previousWins.RemoveAt(0);
            }
            // PreviousWin_singleLocal.Add(single);
            TripleFunWheel.instance.PreviousWin_Single.Clear();
            //TripleFunWheel.instance.PreviousWin_Single.Add(single);
            for(int j = 0; j < windata.previousWins.Count; j++)
            {
                TripleFunWheel.instance.PreviousWin_Single.Add(windata.previousWins[j]);
            }
            //StartCoroutine(coinparcel());

            /*for(int i = windata.previousWin_double.Count - 1; i >= windata.previousWin_double.Count - 5 ; i--)
            {
                if(PreviousWin_doubleLocal.Count >= 5)
                {
                    PreviousWin_doubleLocal.RemoveAt(4);
                    PreviousWin_doubleLocal.Add(windata.previousWin_double[i]);
                    break;
                }
                else
                {
                    PreviousWin_doubleLocal.Add(windata.previousWin_double[i]);
                }
            }*/
            // PreviousWin_doubleLocal.Reverse();
            while(windata.previousWinsDouble.Count > 5)
            {
                windata.previousWinsDouble.RemoveAt(0);
               
            }
            // PreviousWin_doubleLocal.Add(windata.previousWin_double[windata.previousWin_double.Count -1]);//(windata.doubleNo);
            TripleFunWheel.instance.PreviousWin_Double.Clear();
            for(int j = 0; j < windata.previousWinsDouble.Count; j++)
            {
                TripleFunWheel.instance.PreviousWin_Double.Add(windata.previousWinsDouble[j]);
            }

            
            while(windata.previousWinsTriple.Count > 10)
            {
                windata.previousWinsTriple.RemoveAt(0);
               
            }
            // PreviousWin_tripleLocal.Add(windata.previousWin_triple[windata.previousWin_triple.Count -1]);//(windata.doubleNo);
            // TripleFunWheel.instance.PreviousWin_Triple.Clear();
            /*for(int i = windata.previousWin_triple.Count - 1; i >= windata.previousWin_triple.Count - 5 ; i--)
            {
                if(PreviousWin_tripleLocal.Count >= 5)
                {
                    PreviousWin_tripleLocal.RemoveAt(4);
                    PreviousWin_tripleLocal.Add(windata.previousWin_triple[i]);
                    break;
                }
                else
                {
                    PreviousWin_tripleLocal.Add(windata.previousWin_triple[i]);
                }
            }*/
            // PreviousWin_tripleLocal.Reverse();*/
            //TripleFunWheel.instance.PreviousWin_Triple.Clear();
            TripleFunWheel.instance.PreviousWin_Triple.Clear();
            for(int j = 0; j < windata.previousWinsTriple.Count; j++)
            {
                TripleFunWheel.instance.PreviousWin_Triple.Add(windata.previousWinsTriple[j]);
            }
            repeatclear();

            WinNo_txt.gameObject.SetActive(false);
            Wheel_shadowPanel.SetActive(true);
            // Wheel.GetComponent<Animation>().Play("TripleFun_ZoomIn");
            // UiAnimation_Obj.GetComponent<UiAnimation>()._playWheel_rotation = true;
            // UiAnimation_Obj.GetComponent<UiAnimation>()._wheelAnim = true;
            // StartCoroutine(UiAnimation_Obj.GetComponent<UiAnimation>().Start_WheelRotation());
            // Wheel.GetComponent<Animation>().Stop();
            for(int i = 0; i < FillerList.Count; i++)
            {
                FillerList[i].GetComponent<Animation>().Stop();
                TripleFunScreen.Instance.FillerList[i].SetActive(false);
            }
            //if(spinnow)
            //{
            Wheel.GetComponent<Animation>().Play("TripleFun_ZoomIn");
            UiAnimation_Obj.GetComponent<UiAnimation>()._playWheel_rotation = true;
            UiAnimation_Obj.GetComponent<UiAnimation>()._wheelAnim = true;
            StartCoroutine(UiAnimation_Obj.GetComponent<UiAnimation>().Start_WheelRotation());
            //spin_AudioSource.GetComponent<AudioSource>().Play();
            StartCoroutine(SPinStart());    
            //}
            //spinnow = false;
    
            //StartCoroutine(SPinStart());
            //Debug.Log("conttainnerrrrrr"+ previous_round_single.Length);
            // TripleFunWheel.instance.Spin(single, @double, triple);
            for (int i = 0; i <  Single_Bets_Container.Length; i++)
            {
                previous_round_single[i] = Single_Bets_Container[i];
            }
            for (int i = 0; i <  Double_Bets_Container.Length; i++)
            {
                //previous_round_double[i] = Double_Bets_Container[i];
            }
            for (int i = 0; i <  triplekeys.Count; i++)
            {
                //previous_round_triplekey.Add(TripleBets.Keys[i]);
                previous_round_triplekey.Add(triplekeys[i]);
                previous_round_triplevalue.Add(triplevalue[i]);
            }
            Debug.Log("allassigned");

            

            DCW.OnSpinComplete += mySpinComplete;
        }
        public IEnumerator triplewinresponse()
        {
            
            WWWForm form = new WWWForm();
            string playername = "GK"+PlayerPrefs.GetString("email");
            int gameid  = 6;
            form.AddField("playerId", playername);
            form.AddField("game_id",gameid);
            using (UnityWebRequest www = UnityWebRequest.Post(winamounturl, form))
            {
                yield return www.SendWebRequest();
                
                if (www.result != UnityWebRequest.Result.Success)
                {
                    Debug.LogError(www.error);
                }
                else
                {
                    Debug.Log("Responce for triple win  " +www.downloadHandler.text);
                    winneramount response = JsonConvert.DeserializeObject<winneramount>(www.downloadHandler.text);
                    if (response.status == 200)
                    {
                        //winPoint = response.data.Winamount;
                        winnerAmount = response.data.Winamount;
                        winround = balance+winnerAmount;
                        if(winnerAmount > 0)
                        {
                            TakeBtn.gameObject.SetActive(true);
                            betOkBtn.gameObject.SetActive(false);
                            prevbet.SetActive(false);
                        }
                        //winround = balance + winningAmount;
                        Debug.Log("/////////////////////////////////////////" +response.data.Winamount );
                        if(started)
                        {
                            winTxt.text = response.data.Winamount.ToString();
                            if(response.data.Winamount > 0)
                            {
                                restore();
                                //restorewithin();
                            }
                            started = false;
                        }
                    }
                    else
                    {
                        Debug.Log("User not found Error " + response.message);
                    }
                    //winround = AUI.balance+winPoint;
                }
            }
        }

        public IEnumerator emptytriplewinresponse()
        {
            
            WWWForm form = new WWWForm();
            string playername = "GK"+PlayerPrefs.GetString("email");
            int gameid  = 6;
            form.AddField("playerId", playername);
            form.AddField("game_id",gameid);
            using (UnityWebRequest www = UnityWebRequest.Post(emptyurl, form))
            {
                yield return www.SendWebRequest();
                
                if (www.result != UnityWebRequest.Result.Success)
                {
                    Debug.LogError(www.error);
                }
                else
                {
                    Debug.Log("The data has been deleted from the database");
                }
            }
        }
        

        public void repeatassign() // used for testing repeat
        {
            //Debug.Log("Triplebetscount"+TripleBets.Count);
            for (int i = 0; i <  Single_Bets_Container.Length; i++)
            {
                previous_round_single[i] = Single_Bets_Container[i];
            }
            for (int i = 0; i <  Double_Bets_Container.Length; i++)
            {
                previous_round_double[i] = Double_Bets_Container[i];
            }
            for (int i = 0; i <  triplekeys.Count; i++)
            {
                //previous_round_triplekey.Add(TripleBets.Keys[i]);
                previous_round_triplekey.Add(triplekeys[i]);
                previous_round_triplevalue.Add(triplevalue[i]);
            }
        }
        IEnumerator coinparcel()
        {
            float localbalance = balance;
            float localwinpoint = winnerAmount;
            float elapsedTime = 0f;
            float deductionPercentage = 0.1f;
            while (elapsedTime <4f)
            {
                float deductionValue = (winnerAmount * deductionPercentage);
                    
                    // Deduct the value from the current variable
                winnerAmount -= Mathf.RoundToInt(deductionValue);
                localbalance += Mathf.RoundToInt(deductionValue);
                balanceTxt.text = localbalance.ToString("F2");
                winTxt.text = winnerAmount.ToString();
                if(winnerAmount == 5)
                {
                    balance = winround;//+= winPoint;
                    winnerAmount = 0;
                    balanceTxt.text = balance.ToString("F2");
                    winTxt.text = winnerAmount.ToString();
                    break;
                }    
                // Make sure we don't go below zero
                //winPoint = Mathf.Max(winPoint, 0);
    
                yield return null;
                elapsedTime += Time.deltaTime;
                
            }
            // balance += localwinpoint;//+= winPoint;
            // winnerAmount = 0;
            // balanceTxt.text = balance.ToString();
            // winTxt.text = winnerAmount.ToString();
            

            // Update the elapsed time
            elapsedTime += Time.deltaTime;
        
        
        }

        IEnumerator SPinStart()
        {
            Debug.Log("Play the music now");
            ///spin_AudioSource.GetComponent<AudioSource>().Play();
            yield return new WaitForSeconds(0.3f);
            spin_AudioSource.GetComponent<AudioSource>().Play();
            TripleFunWheel.instance.Spin(single, @double, triple);
            //spin_AudioSource.GetComponent<AudioSource>().Play();
            yield return new WaitForSeconds(0.5f);
            
        }

        void mySpinComplete()
        {
            winTxt.text = winnerAmount.ToString();
            int doubleWin = int.Parse(outer_win_wheelValue.ToString() + inner_wheelValue.ToString());
            // StartCoroutine(WinAnimation(doubleWin,inner_wheelValue));
            // StartCoroutine(WinAnimation(doubleWin, outer_win_wheelValue));
            var DCW = FindObjectOfType<TripleFunWheel>();
            DCW.OnSpinComplete -= mySpinComplete;
            
        }

        bool _playblinkAnim;
        public GameObject Takepink;

        public IEnumerator TakeBlinkAnim()
        {
            if(_playblinkAnim == true)
            {
                //TakeBtn.GetComponent<Image>().color = new Color32(255,255,255,180);
                // TakeBtn.gameObject.SetActive(true);
                // Takepink.SetActive(false);
                // //TakeBtn.interactable = false;
                // yield return new WaitForSeconds(0.5f);
                // //TakeBtn.GetComponent<Image>().color = new Color32(255,255,255,255);
                // TakeBtn.gameObject.SetActive(false);
                // Takepink.SetActive(true);
                // //TakeBtn.interactable = true;
                // yield return new WaitForSeconds(0.5f);
                // StartCoroutine(TakeBlinkAnim());
            }
            yield return null;
        }
        public IEnumerator BetBlinkAnim()
        {
            if(betadded == true)
            {
                // //TakeBtn.GetComponent<Image>().color = new Color32(255,255,255,180);
                // betOkBtn.gameObject.SetActive(true);
                // betokpink.SetActive(false);
                // //TakeBtn.interactable = false;
                // yield return new WaitForSeconds(0.5f);
                // //TakeBtn.GetComponent<Image>().color = new Color32(255,255,255,255);
                // betOkBtn.gameObject.SetActive(false);
                // betokpink.SetActive(true);
                // //TakeBtn.interactable = true;
                // yield return new WaitForSeconds(0.5f);
                // StartCoroutine(BetBlinkAnim());
            }
            yield return null;
        }
        public void StopBetokBlink()
        {
            betadded = false;
            StopCoroutine(BetBlinkAnim());
        }

        public void StopTakeBlink()
        {
            _playblinkAnim = false;
            
            StopCoroutine(TakeBlinkAnim());
            TakeBtn.gameObject.SetActive(true);
            Takepink.SetActive(false);
        }
        #endregion

    public  IEnumerator WinAnimation(int doubleWinNumber, int singleWinNumber, int tripleWinNumber)
    {
        for (int i = 0; i < 5; i++)
        {
            //doubleGridBtns[doubleWinNumber].interactable = true;
            //doubleGridBtns[doubleWinNumber].gameObject.GetComponent<Image>().enabled = true;
            //singleGridBtns[singleWinNumber].interactable = true;
            //singleGridBtns[singleWinNumber].gameObject.GetComponent<Image>().enabled = true;
            //changetriple(tripleWinNumber);
            //Debug.Log("ttttttttttttttttttttttttttttttttttttttttttttttttttttt" + tripleWinNumber);
            // for(int j = 0; j < tripleGridBtns.Length; j++)
            // {
            //     //Debug.Log("this is the current version" + tripleGridBtns[i].transform.GetChild(0).GetComponent<TMP_Text>().text);
            //     if(tripleGridBtns[j].transform.GetChild(0).GetComponent<TMP_Text>().text == tripleWinNumber.ToString())
            //     {
            //         //Debug.Log("the value of i"+i +"the final number in grid"+tripleGridBtns[i].transform.GetChild(0).GetComponent<TMP_Text>().text);
            //         //tripleGridBtns[j].interactable = true;
            //         tripleGridBtns[j].gameObject.GetComponent<Image>().enabled = true;
            //         //tripleGridBtns[i].transform.GetChild(0).parent.GetComponent<Button>().interactable = true;
            //     }
            // }
            yield return new WaitForSeconds(0.5f);
            // doubleGridBtns[doubleWinNumber].interactable = false;
            // singleGridBtns[singleWinNumber].interactable = false;
            // doubleGridBtns[doubleWinNumber].gameObject.GetComponent<Image>().enabled = false;
            // singleGridBtns[singleWinNumber].gameObject.GetComponent<Image>().enabled = false;
            // for(int j = 0; j < tripleGridBtns.Length; j++)
            // {
            //     if(tripleGridBtns[j].transform.GetChild(0).GetComponent<TMP_Text>().text == tripleWinNumber.ToString())
            //     {
            //         //tripleGridBtns[i].transform.GetChild(0).parent.GetComponent<Button>().interactable = false;
            //         //tripleGridBtns[j].interactable = false;
            //         tripleGridBtns[j].gameObject.GetComponent<Image>().enabled = false;
            //     }
            // }
            
        
            yield return new WaitForSeconds(0.5f);
        }
        // singleGridBtns[singleWinNumber].interactable = true;
        // doubleGridBtns[doubleWinNumber].interactable = false;
        // doubleGridBtns[doubleWinNumber].gameObject.GetComponent<Image>().enabled = false;
        // singleGridBtns[singleWinNumber].gameObject.GetComponent<Image>().enabled = true;
        // winTxt.text = winnerAmount.ToString();
        //ResetAllBets();
        Debug.Log("Win Animation2");
        //BettingButtonInteractablity(false);
        // Debug.LogError("tripleWinNumber  " + tripleWinNumber);
        // if(tripleWinNumber > 0 && tripleWinNumber < 100)
        // {
        //     // Debug.LogError("zero panel");
        //     if(int.Parse(winTxt.text) == 0)
        //     {
        //         ResetAllBets();
        //     }
            
        //     BetsBtn(00);
            
            
        // }
        // else if(tripleWinNumber > 100 && tripleWinNumber < 200)
        // {
        //     // Debug.LogError("hundred panel");
        //     if(int.Parse(winTxt.text) == 0)
        //     {
        //         ResetAllBets();
        //     }
        //     BetsBtn(100);
            
           
        // }
        // else if(tripleWinNumber > 200 && tripleWinNumber < 300)
        // {
        //     // Debug.LogError("2 hundred  panel");
        //     BetsBtn(200);
        //     if(int.Parse(winTxt.text) == 0)
        //     {
        //         ResetAllBets();
        //     }
            
        // }
        // else if(tripleWinNumber > 300 && tripleWinNumber < 400)
        // {
        //     // Debug.LogError("3 hundred  panel");
        //     if(int.Parse(winTxt.text) == 0)
        //     {
        //         ResetAllBets();
        //     }
        //     BetsBtn(300);
            
            
        // }
        // else if(tripleWinNumber > 400 && tripleWinNumber < 500)
        // {
        //     // Debug.LogError("4 hundred  panel");
        //     BetsBtn(400);
        //     if(int.Parse(winTxt.text) == 0)
        //     {
        //         ResetAllBets();
        //     }
            
        // }
        // else if(tripleWinNumber > 500 && tripleWinNumber < 600)
        // {
        //     // Debug.LogError("5 hundred  panel");
        //     if(int.Parse(winTxt.text) == 0)
        //     {
        //         ResetAllBets();
        //     }
        //     BetsBtn(500);
            
            
        // }
        // else if(tripleWinNumber > 600 && tripleWinNumber < 700)
        // {
        //     // Debug.LogError("6 hundred  panel");
        //     if(int.Parse(winTxt.text) == 0)
        //     {
        //         ResetAllBets();
        //     }
        //     BetsBtn(600);
            
            
        // }
        // else if(tripleWinNumber > 700 && tripleWinNumber < 800)
        // {
        //     // Debug.LogError("7 hundred  panel");
        //     if(int.Parse(winTxt.text) == 0)
        //     {
        //         ResetAllBets();
        //     }
        //     BetsBtn(700);
            
            
        // }
        // else if(tripleWinNumber > 800 && tripleWinNumber < 900)
        // {
        //     // Debug.LogError("8 hundred  panel");
        //     if(int.Parse(winTxt.text) == 0)
        //     {
        //         ResetAllBets();
        //     }
        //     BetsBtn(800);
            
            
        // }
        // else if(tripleWinNumber > 900 && tripleWinNumber < 1000)
        // {
        //     // Debug.LogError("9 hundred  panel");
        //     if(int.Parse(winTxt.text) == 0)
        //     {
        //         ResetAllBets();
        //     }
        //     BetsBtn(900);
            
            
        // }
        StartCoroutine(justblink(doubleWinNumber, singleWinNumber, tripleWinNumber));
        // if(!spinnow || !added)
        // {
        //     //StartCoroutine(WinAnimation(doubleWinNumber, singleWinNumber, tripleWinNumber));
        //     StartCoroutine(justblink(doubleWinNumber, singleWinNumber, tripleWinNumber));
        // }
        // for (int i = 0; i < 5; i++)
        // {
        //     doubleGridBtns[doubleWinNumber].interactable = true;
        //     singleGridBtns[singleWinNumber].interactable = true;
        //     for(int j = 0; j < tripleGridBtns.Length; j++)
        //     {
        //         if(tripleGridBtns[i].transform.GetChild(0).GetComponent<TMP_Text>().text == tripleWinNumber.ToString())
        //         {
        //             tripleGridBtns[i].transform.GetChild(0).parent.GetComponent<Button>().interactable = true;
        //         }
        //     }
        //     yield return new WaitForSeconds(0.5f);
        //     doubleGridBtns[doubleWinNumber].interactable = false;
        //     singleGridBtns[singleWinNumber].interactable = false;
        //     for(int j = 0; j < tripleGridBtns.Length; j++)
        //     {
        //         if(tripleGridBtns[i].transform.GetChild(0).GetComponent<TMP_Text>().text == tripleWinNumber.ToString())
        //         {
        //             tripleGridBtns[i].transform.GetChild(0).parent.GetComponent<Button>().interactable = false;
        //         }
        //     }
        //     yield return new WaitForSeconds(0.5f);
        // }
        // singleGridBtns[singleWinNumber].interactable = true;
        // doubleGridBtns[doubleWinNumber].interactable = false;
        //ResetAllBets();
        UpdateUi();
    }
    public IEnumerator justblink(int doubleWinNumber,int singleWinNumber,int tripleWinNumber)
    {
        // for (int i = 0; i < 5; i++)
        // {
            // doubleGridBtns[doubleWinNumber].interactable = true;
            // singleGridBtns[singleWinNumber].interactable = true;
        //     doubleGridBtns[doubleWinNumber].gameObject.GetComponent<Image>().enabled = true;
        //     singleGridBtns[singleWinNumber].gameObject.GetComponent<Image>().enabled = true;
        //     //changetriple(tripleWinNumber);
        //     //Debug.Log("ttttttttttttttttttttttttttttttttttttttttttttttttttttt" + tripleWinNumber);
        //     for(int j = 0; j < tripleGridBtns.Length; j++)
        //     {
        //         //Debug.Log("this is the current version" + tripleGridBtns[i].transform.GetChild(0).GetComponent<TMP_Text>().text);
        //         if(tripleGridBtns[j].transform.GetChild(0).GetComponent<TMP_Text>().text == tripleWinNumber.ToString())
        //         {
        //             //Debug.Log("the value of i"+i +"the final number in grid"+tripleGridBtns[i].transform.GetChild(0).GetComponent<TMP_Text>().text);
        //             //tripleGridBtns[j].interactable = true;
        //             tripleGridBtns[j].gameObject.GetComponent<Image>().enabled = true;
        //             //tripleGridBtns[i].transform.GetChild(0).parent.GetComponent<Button>().interactable = true;
        //         }
        //     }
        //     yield return new WaitForSeconds(0.5f);
        //     // doubleGridBtns[doubleWinNumber].interactable = false;
        //     // singleGridBtns[singleWinNumber].interactable = false;
        //     doubleGridBtns[doubleWinNumber].gameObject.GetComponent<Image>().enabled = false;
        //     singleGridBtns[singleWinNumber].gameObject.GetComponent<Image>().enabled = false;
        //     for(int j = 0; j < tripleGridBtns.Length; j++)
        //     {
        //         if(tripleGridBtns[j].transform.GetChild(0).GetComponent<TMP_Text>().text == tripleWinNumber.ToString())
        //         {
        //             //tripleGridBtns[i].transform.GetChild(0).parent.GetComponent<Button>().interactable = false;
        //             //tripleGridBtns[j].interactable = false;
        //             tripleGridBtns[j].gameObject.GetComponent<Image>().enabled = false;
        //         }
        //     }
            
        
        //     yield return new WaitForSeconds(0.5f);
        // }
        // // singleGridBtns[singleWinNumber].interactable = true;
        // // doubleGridBtns[doubleWinNumber].interactable = true;
        // doubleGridBtns[doubleWinNumber].gameObject.GetComponent<Image>().enabled = true;
        // singleGridBtns[singleWinNumber].gameObject.GetComponent<Image>().enabled = true;
        // for(int j = 0; j < tripleGridBtns.Length; j++)
        // {
        //     if(tripleGridBtns[j].transform.GetChild(0).GetComponent<TMP_Text>().text == tripleWinNumber.ToString())
        //     {
        //         //tripleGridBtns[i].transform.GetChild(0).parent.GetComponent<Button>().interactable = false;
        //         tripleGridBtns[j].interactable = true;
        //         tripleGridBtns[j].gameObject.GetComponent<Image>().enabled = true;
        //     }
        // }
        // if(!spinnow || !added)
        // {
        //     //StartCoroutine(WinAnimation(doubleWinNumber, singleWinNumber, tripleWinNumber));
            
        //     StartCoroutine(justblink(doubleWinNumber, singleWinNumber, tripleWinNumber));
        // }
        // else{
        //     StopCoroutine(justblink(doubleWinNumber, singleWinNumber, tripleWinNumber));
        // }
        yield return null;
    }
    public void changetriple(int tripleWinNumber)
    {
        if(tripleWinNumber > 0 && tripleWinNumber < 100)
        {
            // Debug.LogError("zero panel");
            if(int.Parse(winTxt.text) == 0)
            {
                ResetAllBets();
            }
            BetsBtn(00);
            //
            
        }
        else if(tripleWinNumber > 100 && tripleWinNumber < 200)
        {
            // Debug.LogError("hundred panel");
            if(int.Parse(winTxt.text) == 0)
            {
                ResetAllBets();
            }
            BetsBtn(100);
            //
           
        }
        else if(tripleWinNumber > 200 && tripleWinNumber < 300)
        {
            // Debug.LogError("2 hundred  panel");
            if(int.Parse(winTxt.text) == 0)
            {
                ResetAllBets();
            }
            BetsBtn(200);
            //
            
        }
        else if(tripleWinNumber > 300 && tripleWinNumber < 400)
        {
            // Debug.LogError("3 hundred  panel");
            if(int.Parse(winTxt.text) == 0)
            {
                ResetAllBets();
            }
            BetsBtn(300);
            //
            
        }
        else if(tripleWinNumber > 400 && tripleWinNumber < 500)
        {
            // Debug.LogError("4 hundred  panel");
            BetsBtn(400);
            if(int.Parse(winTxt.text) == 0)
            {
                ResetAllBets();
            }
            //ResetAllBets();
            
        }
        else if(tripleWinNumber > 500 && tripleWinNumber < 600)
        {
            // Debug.LogError("5 hundred  panel");
            if(int.Parse(winTxt.text) == 0)
            {
                ResetAllBets();
            }
            BetsBtn(500);
            //
            
        }
        else if(tripleWinNumber > 600 && tripleWinNumber < 700)
        {
            // Debug.LogError("6 hundred  panel");
            if(int.Parse(winTxt.text) == 0)
            {
                ResetAllBets();
            }
            BetsBtn(600);
            //
            
        }
        else if(tripleWinNumber > 700 && tripleWinNumber < 800)
        {
            // Debug.LogError("7 hundred  panel");
            if(int.Parse(winTxt.text) == 0)
            {
                ResetAllBets();
            }
            BetsBtn(700);
            //
            
        }
        else if(tripleWinNumber > 800 && tripleWinNumber < 900)
        {
            // Debug.LogError("8 hundred  panel");
            if(int.Parse(winTxt.text) == 0)
            {
                ResetAllBets();
            }
            BetsBtn(800);
            //
            
        }
        else if(tripleWinNumber > 900 && tripleWinNumber < 1000)
        {
            // Debug.LogError("9 hundred  panel");
            if(int.Parse(winTxt.text) == 0)
            {
                ResetAllBets();
            }
            BetsBtn(900);
            //
            
        }
    }
    
        public void BettingButtonInteractablity(bool status)
        {
            foreach (var card in doubleGridBtns)
            {
                card.interactable = status;
            }
            foreach (var card in singleGridBtns)
            {
                card.interactable = status;
            }
            foreach(var card in tripleGridBtns)
            {
                card.interactable = status;
            }

        }

        private void UpdateUi()
        {
            balanceTxt.text = balance.ToString("F2");
            totalBetsTxt.text = totalBets.ToString();

            #region BETS
            for (int i = 0; i < doubleGridBtns.Length; i++)
            {
                //string v = Double_Bets_Container[i] == 0 ? $"{i}" : Double_Bets_Container[i].ToString();
                //Debug.Log("THIS IS THE VALUE OF I USED IN BUTTON " +i);
                //Debug.Log("LENGTH OF DOUBLE BUTTON ARRAY" + doubleGridBtns.Length +  "LENGTH OF CONTAINER" + Double_Bets_Container.Length);
                if (Double_Bets_Container[i] != 0)
                {
                    doubleGridBtns[i].transform.GetChild(0).gameObject.GetComponent<Text>().text = Double_Bets_Container[i].ToString();
                    //doubleGridBtns[i].transform.GetChild(1).gameObject.SetActive(true);
                    //doubleGridBtns[i].transform.GetChild(1).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = v;//TripleBets[SelectedBets_section + i].ToString();

                }
                else
                {
                    doubleGridBtns[i].transform.GetChild(0).gameObject.GetComponent<Text>().text = "";
                    //doubleGridBtns[i].transform.GetChild(0).gameObject.GetComponent<Text>().text = "";
                    //doubleGridBtns[i].transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().color = Color.white;

                }
                //doubleGridBtns[i].transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = v;

            }
            for (int i = 0; i < singleGridBtns.Length; i++)
            {
                string v = Single_Bets_Container[i] == 0 ? $"{i}" : Single_Bets_Container[i].ToString();
                if (Single_Bets_Container[i] != 0)
                {
                    singleGridBtns[i].transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = v;
                    //singleGridBtns[i].transform.GetChild(1).gameObject.SetActive(true);
                    //singleGridBtns[i].transform.GetChild(1).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = v;

                }
                else
                {
                    singleGridBtns[i].transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "";
                    // singleGridBtns[i].transform.GetChild(1).gameObject.SetActive(false);
                    // singleGridBtns[i].transform.GetChild(1).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text= "";


                }

                //singleGridBtns[i].transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = v;
            }
            for (int i = 0; i < tripleGridBtns.Length; i++)
            {
                if( TripleBets[i] != 0 )
                {
                    //tripleGridBtns[i].transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().color = Color.white;
                    //tripleGridBtns[i].transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = TripleBets[SelectedBets_section + i].ToString();
                    tripleGridBtns[i].transform.GetChild(0).GetComponent<Text>().text = TripleBets[i].ToString();
                    // tripleGridBtns[i].transform.GetChild(1).gameObject.SetActive(true);
                    // tripleGridBtns[i].transform.GetChild(1).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = TripleBets[SelectedBets_section + i].ToString();
                    
                    
                }
                else
                {
                    //tripleGridBtns[i].transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().color = Color.white;
                    tripleGridBtns[i].transform.GetChild(0).GetComponent<Text>().text = "";
                    // tripleGridBtns[i].transform.GetChild(1).gameObject.SetActive(false);
                    // tripleGridBtns[i].transform.GetChild(1).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "";//TripleBets[SelectedBets_section + i].ToString();
                }

                // string v = Triple_Bets_Container[i] == 0 ? $"{i+100}" : Triple_Bets_Container[i].ToString();
                // if (Triple_Bets_Container[i] != 0)
                // {
                //     tripleGridBtns[i].transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().color = Color.white;

                // }
                // else
                // {
                //     tripleGridBtns[i].transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().color = Color.black;


                // }

                // tripleGridBtns[i].transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = v;
            }

            //totalBets = Double_Bets_Container.Sum() + Single_Bets_Container.Sum();
            #endregion

            // LocalDatabase.data.balance = balance;
            LocalDatabase.SaveGame();
        }

        private void CancelSpecificBets()
        {
            // balance = balance + _betsData.Values.Last();
            // totalBets = totalBets - _betsData.Values.Last();
            // _betsData.Remove(_betsData.Keys.Last());

            if (totalBets >0)
            {
                balance = balance + _betsData.Values.Last();
                totalBets = totalBets - _betsData.Values.Last();
                _betsData.Remove(_betsData.Keys.Last());
               if( _betsData.Keys.Last() >= 0)
               {
                   if(_betsData.Keys.Last() > 0 && _betsData.Keys.Last() <= 9)
                   {
                       if(Single_Bets_Container[_betsData.Keys.Last()] != 0)
                       {
                           Single_Bets_Container[_betsData.Keys.Last()] = 0;
                       }
                   }
   
                   if(_betsData.Keys.Last() >= 0 && _betsData.Keys.Last() < 100)
                   {
                       if(Double_Bets_Container[_betsData.Keys.Last()] != 0)
                       {
                           Double_Bets_Container[_betsData.Keys.Last()] = 0;
                       }
                   }
   
                   if(_betsData.Keys.Last() >= 0 && _betsData.Keys.Last() < 1000)
                   {
                       if(TripleBets[_betsData.Keys.Last()]  != 0)
                       {
                           TripleBets[_betsData.Keys.Last()] = 0;
                       }
                   }
               }
               else
               {
                   if(_betsData.Keys.First() >= 0 && _betsData.Keys.First() <= 9)
                   {
                       if(Single_Bets_Container[_betsData.Keys.First()] != 0)
                       {
                           Single_Bets_Container[_betsData.Keys.First()] = 0;
                       }
                   }
   
                   if(_betsData.Keys.First() >= 0 && _betsData.Keys.First() < 100)
                   {
                       if(Double_Bets_Container[_betsData.Keys.First()] != 0)
                       {
                           Double_Bets_Container[_betsData.Keys.First()] = 0;
                       }
                   }
   
                   if(_betsData.Keys.First() >= 0 && _betsData.Keys.First() < 1000)
                   {
                       if(TripleBets[_betsData.Keys.First()]  != 0)
                       {
                           TripleBets[_betsData.Keys.First()] = 0;
                       }
                   }
                }
            }

            UpdateUi();
        }
        void clearbets()
        {
            balance += totalBets;
            for(int i = 0; i < 1000; i++)
            {
                TripleBets[i] = 0;
            }
            for(int i = 0; i < Single_Bets_Container.Length; i++)
            {
                Single_Bets_Container[i] = 0;
            }
            for (int i = 0; i < Double_Bets_Container.Length; i++)
            {
                Double_Bets_Container[i] = 0;
            }
            for (int i = 0; i < Triple_Bets_Container.Length; i++)
            {
                Triple_Bets_Container[i] = 0;
            }
            // for (int i = 0; i < triplekeys.Count; i++)
            // {
            //     triplekeys.RemoveAt(i);
            //     triplevalue.RemoveAt(i);
            // }
            // for (int i = 0; i < doubleValues_Array.Length; i++)
            // {
            //     doubleValues_Array[i] = 0;
            // }
            tripleBets_keys.Clear();
            triplevalue.Clear();
            doubleValues_Array = Array.Empty<int>();
            string temp = doubleValues_Array.ToString();
            //Debug.Log("/////////////////////////////////////////"+TripleBets[].);
            //BetsBtn(currentmultiple);
            totalBets = 0;
            zoomallout();
            UpdateUi();
        }
        void clearstore()
        {
            for (int i = 0; i < Triplelast.singlebet.Length; i++)
            {
                Triplelast.singlebet[i] = 0;
            }
            for (int i = 0; i < Triplelast.doublebet.Length; i++)
            {
                Triplelast.doublebet[i] = 0;
            }
            Triplelast.triplebet.Clear();
            Triplelast.tripleval.Clear();
            Debug.Log("///////////////////////////"+Triplelast.triplebet.Count);
        }
        public void store()
        {
            //Triplelast.winamount = int.Parse(winTxt.text);
            clearstore();
            Triplelast.playerid = PlayerPrefs.GetString("email");
            bool singleb,doubleb,triplb;
            singleb=doubleb=triplb = false;
            int tempsingle,tempdouble;
            tempsingle = tempdouble = 0;
            Debug.Log("Data is stored");

            for (int i = 0; i < Single_Bets_Container.Length; i++)
            {
                if(Single_Bets_Container[i] == 0)
                {
                    tempsingle++;
                }
            }
            if(tempsingle == Single_Bets_Container.Length)
            {
                singleb = true;
                
            }
            for (int i = 0; i < Double_Bets_Container.Length; i++)
            {
                if(Double_Bets_Container[i] == 0)
                {
                    tempdouble++;
                }
            }
            if(tempdouble == Double_Bets_Container.Length)
            {
                doubleb = true;
            }
            if(triplekeys.Count == 0)
            {
                triplb = true;
            }
            // if(singleb == doubleb == triplb == true)
            // {
            //     Debug.Log("full");
            //     // for (int i = 0; i < Single_Bets_Container.Length; i++)
            //     // {
            //     //     Triplelast.singlebet[i] = previous_round_single[i];
            //     // }
            //     // for (int i = 0; i < Double_Bets_Container.Length; i++)
            //     // {
            //     //     Triplelast.doublebet[i] = previous_round_double[i];
            //     // }
            //     // for (int i = 0; i < triplekeys.Count; i++)
            //     // {
            //     //     Triplelast.triplebet.Add(previous_round_triplekey[i]);
            //     //     Triplelast.tripleval.Add(previous_round_triplevalue[i]);
            //     // }
            // }
            // else
            // {
            for (int i = 0; i < Single_Bets_Container.Length; i++)
            {
                Triplelast.singlebet[i] = Single_Bets_Container[i];
            }
            for (int i = 0; i < Double_Bets_Container.Length; i++)
            {
                Triplelast.doublebet[i] = Double_Bets_Container[i];
            }
            for (int i = 0; i < triplekeys.Count; i++)
            {
                Triplelast.triplebet.Add(triplekeys[i]);
                Triplelast.tripleval.Add(triplevalue[i]);
                Debug.Log("///////"+i);
            }
            File.Delete(SaveFilePath);
            savebinary.savefunctiontriple();
            
            
            //lastrecord.Triplelas = true;
            
        }
        public void storewithin()
        {
            Triplewithin.winamount = int.Parse(winTxt.text);
            Triplewithin.tripleconfirmed = isBetConfirmed;
            Triplewithin.Roundcount = TripleChance_ServerResponse.instance.tripleround;
            for (int i = 0; i < Single_Bets_Container.Length; i++)
            {
                Triplewithin.singlebet[i] = Single_Bets_Container[i];
            }
            for (int i = 0; i < Double_Bets_Container.Length; i++)
            {
                Triplewithin.doublebet[i] = Double_Bets_Container[i];
            }
            for (int i = 0; i < triplekeys.Count; i++)
            {
                Triplewithin.triplebet.Add(triplekeys[i]);
                Triplewithin.tripleval.Add(triplevalue[i]);
            }
        }
        public void restore()
        {
            //winTxt.text = Triplelast.winamount.ToString();
            //winnerAmount = int.Parse(winTxt.text);
            //winround = balance+winnerAmount;
            repeatclear();
            for (int i = 0; i < Triplelast.singlebet.Length; i++)
            {
                previous_round_single[i] = Triplelast.singlebet[i];
            }
            for (int i = 0; i < Triplelast.doublebet.Length; i++)
            {
                Debug.Log("the count of triplelast"+ Triplelast.doublebet.Length+" LEcngth og previous"+previous_round_double.Length );
                previous_round_double[i] = Triplelast.doublebet[i];
            }
            Debug.Log("the count for triples"+Triplelast.triplebet.Count);
            for (int i = 0; i < Triplelast.triplebet.Count; i++)
            {
                if (Triplelast.tripleval[i]>0)
                {
                    previous_round_triplekey.Add(Triplelast.triplebet[i]);
                    previous_round_triplevalue.Add(Triplelast.tripleval[i]);
                }
            }
            showui();
            //lastrecord.Triplelas = false;
            Debug.Log("Data has been restored");
        }
        void restorewithin()
        {
            //winTxt.text = Triplewithin.winamount.ToString();
            //winnerAmount = int.Parse(winTxt.text);
            //winround = balance+winnerAmount;
            for (int i = 0; i < Triplewithin.singlebet.Length; i++)
            {
                previous_round_single[i] = Triplewithin.singlebet[i];
            }
            for (int i = 0; i < Triplewithin.doublebet.Length; i++)
            {
                previous_round_double[i] = Triplewithin.doublebet[i];
            }
            for (int i = 0; i < Triplewithin.triplebet.Count; i++)
            {
                if (Triplewithin.tripleval[i] >0)
                {
                    previous_round_triplekey.Add(Triplewithin.triplebet[i]);
                    previous_round_triplevalue.Add(Triplewithin.tripleval[i]);
                }
            }
            showui();
        }

        void showui()
        {
            
            if (previous_round_single.Sum() + previous_round_double.Sum() + previous_round_triplevalue.Sum() < balance)
            {
                if (previous_round_single.Length >0)
                {
                    for (int i = 0; i < previous_round_single.Length; i++)
                    {
                        //Debug.Log()
                        Single_Bets_Container[i] = previous_round_single[i];//add plus here
                        //AddBets(ref Single_Bets_Container,Single_Bets_Container[i],1);
                    }
                }
                if (previous_round_double.Length > 0)
                {
                    for (int i = 0; i < previous_round_double.Length; i++)
                    {
                        
                        Double_Bets_Container[i] =  previous_round_double[i];//add plus here
                        //AddBets(ref Double_Bets_Container,Double_Bets_Container[i],2);
                    }
                }
                Debug.Log("previoustriple"+previous_round_triplekey.Count);
                if (previous_round_triplekey.Count>0)
                {
                    for (int i = 0; i < previous_round_triplekey.Count; i++)
                    {
                        //Debug.Log("KeyCode"+previous_round_triplekey[i]+"Value"+previous_round_triplevalue[i]);
                        TripleBets[previous_round_triplekey[i]] = previous_round_triplevalue[i];//add plus here
                        triplekeys.Add(previous_round_triplekey[i]);
                        triplevalue.Add(previous_round_triplevalue[i]);
                        //Triple_Bets_Container[i] = previous_round_triple[i];
                        //AddBets(ref Triple_Bets_Container,Triple_Bets_Container[i],3);
                    }
                }
                Debug.Log("triplevalue"+triplevalue.Sum());
                totalBets = Single_Bets_Container.Sum() + Double_Bets_Container.Sum() + triplevalue.Sum();
                totalBetsTxt.text = totalBets.ToString();
                if (!started)
                {
                    balance = balance-totalBets;
                }
                if (balance >0)
                {
                    balanceTxt.text = balance.ToString("F2");
                }
                //Repeatbtn.interactable=false;
                //repeatclear();

            }
            UpdateUi();
        }
        public void Repeatbetts()
        {
            // if(lastrecord.Triplelas == true)
            // {
            restore();
            Debug.Log("lot of data were being called");
            //previousserverdata();
            Repeatbtn.interactable =false;
            StartCoroutine(StopPrevAnim());
            // }
            // if (Single_Bets_Container.Sum() + Double_Bets_Container.Sum() + TripleBets.Values.Sum() < balance)
            // {
            //     if (previous_round_single.Length >0)
            //     {
            //         for (int i = 0; i < previous_round_single.Length; i++)
            //         {
                        
            //             Single_Bets_Container[i] += previous_round_single[i];
            //             //AddBets(ref Single_Bets_Container,Single_Bets_Container[i],1);
            //         }
            //     }
            //     if (previous_round_double.Length > 0)
            //     {
            //         for (int i = 0; i < previous_round_double.Length; i++)
            //         {
                        
            //             Double_Bets_Container[i] +=  previous_round_double[i];
            //             //AddBets(ref Double_Bets_Container,Double_Bets_Container[i],2);
            //         }
            //     }
            //     if (previous_round_triplekey.Count>0)
            //     {
            //         for (int i = 0; i < previous_round_triplekey.Count; i++)
            //         {
            //             Debug.Log("KeyCode"+previous_round_triplekey[i]+"Value"+previous_round_triplevalue[i]);
            //             TripleBets[previous_round_triplekey[i]] += previous_round_triplevalue[i];
                        
            //             //Triple_Bets_Container[i] = previous_round_triple[i];
            //             //AddBets(ref Triple_Bets_Container,Triple_Bets_Container[i],3);
            //         }
            //     }
            //     totalBets = Single_Bets_Container.Sum() + Double_Bets_Container.Sum() + TripleBets.Values.Sum();
            //     totalBetsTxt.text = totalBets.ToString();
            //     balance = balance-totalBets;
            //     balanceTxt.text = balance.ToString();
            //     Repeatbtn.interactable=false;
            //     repeatclear();

            // }
           
        }
        void repeatclear()
        {
            
            for (int i = 0; i < previous_round_single.Length; i++)
            {
                        
                previous_round_single[i] = 0;
            }
            for (int i = 0; i < previous_round_double.Length; i++)
            {
                        
                previous_round_double[i] = 0;
            }
            previous_round_triplekey.Clear();
            previous_round_triplevalue.Clear();
            // for (int i = 0; i < previous_round_triplekey.Count; i++)
            // {
                        
            //     previous_round_triplekey[i] = 0;
            //     previous_round_triplevalue[i] = 0;
            // }
        }

        public void ResetAllBets()
        {
            //Debug.Log("Reset All");
            for (int i = 0; i < Double_Bets_Container.Length; i++)
            {
                //Previous_Double_Bets_Container[i] = Double_Bets_Container[i];
            }
            for (int i = 0; i < Single_Bets_Container.Length; i++)
            {
                //Debug.Log("i value :" + i);
                Previous_Single_Bets_Container[i] = Single_Bets_Container[i];
            }

            //Double_Bets_Container =  Single_Bets_Container = Triple_Bets_Container = singleValues_Array = doubleValues_Array = tripleValues_Array = Array.Empty<int>();
            //int[] tripleValues_Array, doubleValues_Array, singleValues_Array;
            for(int i = 0; i < 1000; i++)
            {
                TripleBets[i] = 0;
            }
            for(int i = 0; i < Single_Bets_Container.Length; i++)
            {
                Single_Bets_Container[i] = 0;
            }
            for (int i = 0; i <Double_Bets_Container.Length; i++)
            {
                Double_Bets_Container[i] = 0;
            }
            for (int i = 0; i < Triple_Bets_Container.Length; i++)
            {
                Triple_Bets_Container[i] = 0;
            }
            foreach (Button item in tripleGridBtns)
            {
                item.transform.GetChild(0).GetComponent<Text>().text ="";
                //item.transform.GetChild(1).gameObject.SetActive(false);
                //    tripleGridBtns[i].transform.GetChild(1).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = TripleBets[SelectedBets_section + i].ToString();
            }
            foreach (Button item in doubleGridBtns)
            {
                item.transform.GetChild(0).GetComponent<Text>().text ="";
                //item.transform.GetChild(1).gameObject.SetActive(false);
                //    tripleGridBtns[i].transform.GetChild(1).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = TripleBets[SelectedBets_section + i].ToString();
            }
            foreach (Button item in singleGridBtns)
            {
                item.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text ="";
                //item.transform.GetChild(1).gameObject.SetActive(false);
                //    tripleGridBtns[i].transform.GetChild(1).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = TripleBets[SelectedBets_section + i].ToString();
            }

            // balance = balance + totalBets;
            totalBets = 0;
            isUserPlacedBets = false;
            canPlaceBet = true;
            isTimeUp = false;

            //UI
            BettingButtonInteractablity(true);
            //clearBtn.interactable = true;
            betOkBtn.interactable = true;
            triplekeys.Clear();
            triplevalue.Clear();
            //doubleBtn.interactable = true;
            //repeatBtn.interactable = true;
            UpdateUi();
        }

        public void gotolobby()
        {
            //TripleFun_ServerResponse.instance        
        }
        //
        public void coinanimation(int value)
        {
            Color selected = new Color(255f,255f,255f,255f);
            Color unselected = new Color32(255,255,255,110);
            Debug.Log("called");
            for(int i =0;i<chipimages.Length;i++)
            {
                if(i == value)
                {
                    chipimages[i].GetComponent<Image>().color = new Color32(255,255,255,255);
                }
                else if(i != value)
                {
                    chipimages[i].GetComponent<Image>().color = unselected;//new Color(255f,255f,255f,110f);
                    //Debug.Log("Unsellected" + i);
                }
            }
            /*foreach (var item in chipimages)
            {
                if()
            }*/
            //for(int i=0 ; i < chipimg.Length; i++)
            /*switch(value)
            {
                case 1: chipNo1Btn.image.color =new Color(255,255,255,255);
                        chipNo5Btn.image.color =new Color(255,255,255,110);
                        chipNo10Btn.image.color =new Color(255,255,255,110);
                        chipNo50Btn.image.color =new Color(255,255,255,110);
                        chipNo100Btn.image.color =new Color(255,255,255,110);
                        chipNo500Btn.image.color =new Color(255,255,255,110);
                        chipNo1000Btn.image.color =new Color(255,255,255,110);
                        chipNo5000Btn.image.color =new Color(255,255,255,110);
                        break;

                case 5: chipNo1Btn.image.color =new Color(255,255,255,110);
                        chipNo5Btn.image.color =new Color(255,255,255,255);
                        chipNo10Btn.image.color =new Color(255,255,255,110);
                        chipNo50Btn.image.color =new Color(255,255,255,110);
                        chipNo100Btn.image.color =new Color(255,255,255,110);
                        chipNo500Btn.image.color =new Color(255,255,255,110);
                        chipNo1000Btn.image.color =new Color(255,255,255,110);
                        chipNo5000Btn.image.color =new Color(255,255,255,110);
                        break;
                case 10:chipNo1Btn.image.color =new Color(255,255,255,110);
                        chipNo5Btn.image.color =new Color(255,255,255,110);
                        chipNo10Btn.image.color =new Color(255,255,255,255);
                        chipNo50Btn.image.color =new Color(255,255,255,110);
                        chipNo100Btn.image.color =new Color(255,255,255,110);
                        chipNo500Btn.image.color =new Color(255,255,255,110);
                        chipNo1000Btn.image.color =new Color(255,255,255,110);
                        chipNo5000Btn.image.color =new Color(255,255,255,110);
                        break;
                case 50:chipNo1Btn.image.color =new Color(255,255,255,110);
                        chipNo5Btn.image.color =new Color(255,255,255,110);
                        chipNo10Btn.image.color =new Color(255,255,255,110);
                        chipNo50Btn.image.color =new Color(255,255,255,255);
                        chipNo100Btn.image.color =new Color(255,255,255,110);
                        chipNo500Btn.image.color =new Color(255,255,255,110);
                        chipNo1000Btn.image.color =new Color(255,255,255,110);
                        chipNo5000Btn.image.color =new Color(255,255,255,110);
                        break;
                case 100:chipNo1Btn.image.color =new Color(255,255,255,110);
                        chipNo5Btn.image.color =new Color(255,255,255,110);
                        chipNo10Btn.image.color =new Color(255,255,255,110);
                        chipNo50Btn.image.color =new Color(255,255,255,110);
                        chipNo100Btn.image.color =new Color(255,255,255,255);
                        chipNo500Btn.image.color =new Color(255,255,255,110);
                        chipNo1000Btn.image.color =new Color(255,255,255,110);
                        chipNo5000Btn.image.color =new Color(255,255,255,110);
                        break;
                case 500:chipNo1Btn.image.color =new Color(255,255,255,110);
                        chipNo5Btn.image.color =new Color(255,255,255,110);
                        chipNo10Btn.image.color =new Color(255,255,255,110);
                        chipNo50Btn.image.color =new Color(255,255,255,110);
                        chipNo100Btn.image.color =new Color(255,255,255,110);
                        chipNo500Btn.image.color =new Color(255,255,255,255);
                        chipNo1000Btn.image.color =new Color(255,255,255,110);
                        chipNo5000Btn.image.color =new Color(255,255,255,110);
                        break;
                case 1000:chipNo1Btn.image.color =new Color(255,255,255,110);
                        chipNo5Btn.image.color =new Color(255,255,255,110);
                        chipNo10Btn.image.color =new Color(255,255,255,110);
                        chipNo50Btn.image.color =new Color(255,255,255,110);
                        chipNo100Btn.image.color =new Color(255,255,255,110);
                        chipNo500Btn.image.color =new Color(255,255,255,110);
                        chipNo1000Btn.image.color =new Color(255,255,255,255);
                        chipNo5000Btn.image.color =new Color(255,255,255,110);
                        break;
                case 5000:chipNo1Btn.image.color =new Color(255,255,255,110);
                        chipNo5Btn.image.color =new Color(255,255,255,110);
                        chipNo10Btn.image.color =new Color(255,255,255,110);
                        chipNo50Btn.image.color =new Color(255,255,255,110);
                        chipNo100Btn.image.color =new Color(255,255,255,110);
                        chipNo500Btn.image.color =new Color(255,255,255,110);
                        chipNo1000Btn.image.color =new Color(255,255,255,110);
                        chipNo5000Btn.image.color =new Color(255,255,255,255);
                        break;

            }*/
        }
        //

        //public override void Hide()
        //{
        //    base.Hide();
        //    RemoveSocketListners();
        //    ResetAllBets();

        //}
        //void RemoveSocketListners()
        //{
        //    SocketRequest.intance.RemoveListners(Constant.OnWinNo);
        //    SocketRequest.intance.RemoveListners(Constant.OnTimerStart);
        //    SocketRequest.intance.RemoveListners(Constant.OnDissconnect);
        //    SocketRequest.intance.RemoveListners(Constant.OnWinAmount);
        //    SocketRequest.intance.RemoveListners(Constant.OnTimeUp);
        //}

        class roundInfo
        {
            public int balance;
            public int gametimer;
            public List<weelNumbers> previousWinData;
        }
        class weelNumbers

        {
            // public int single_winno;
            // public int double_winno;
            public int outer_win_no;
            public int mid_win_no;
            public int inner_win_no;
        }

        [Serializable]
        public class bets
        {
            public string playerId;
            // public int gameId;
            public int points;
            public int[] single, doubleNo, triple;
            public int[] singleVal, doubleVal, tripleVal;
            public int totalsingleVal,totaldoubleVal,totaltripleVal;
        }
        public class PreviousTriple
        {
            public int status;
            public string message;
            public PreviousTripleData data;
        }

        public class PreviousTripleData
        {
            public string playerId;
            // public int gameId;
            public int points;
            public int[] single, doubleNo, triple;
            public int[] singleVal, doubleVal, tripleVal;
            public int totalsingleVal,totaldoubleVal,totaltripleVal;
        }

        public class winAmount
        {
            public int outer_win;
            public int inner_win;
            public int win_points;
            public string player_id;
            public int balance;
        }

        public class WinAmountConfirmation
        {
            public bool status;
            public string message;
            public WinAmountConfirmationData data;
        }

        public class WinAmountConfirmationData
        {
            public string playerId;
            public int win_points;
        }

        public class DiceWinNos
        {
            public int winSingleNo;
            public int winDoubleNo;
            public int winTripleNo;
            public List<int> previousWins;
            public List<int> previousWinsDouble;
            public List<int> previousWinsTriple;
            public int RoundCount;
            public string playerId;
            public int winningSpot, winPoint;
        }

        public void zoomon()
        {
            wheelZoomON.interactable = false;
            WheelZoomOff.interactable= true;
            Wheel.GetComponent<Animation>().enabled = true;
        }

        public void zoomoff()
        {
            wheelZoomON.interactable = true;
            WheelZoomOff.interactable= false;
            Wheel.GetComponent<Animation>().enabled = false;
            
        }

        public class TimerClass
        {
            public int result;//timer
        }
        public class Take_Bet
        {
            public string playerId;
            public int winpoint;
        }
    }
}

namespace DoubleChanceClasses
{
   public class OnWinAmount
   {
       public long RoundCount;
       public int win_no;
       public int winPoints;
   }

   public class BetConfirmation
    {
        public string status;
        public string message;
    }
}
