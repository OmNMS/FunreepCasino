
//using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using Newtonsoft.Json;
using UnityEngine.UI;
using Roulette.ServerStuff;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using System.IO;
using TMPro;

namespace Roulette.Gameplay
{
    public class RouletteScreen : MonoBehaviour
    {
        public static RouletteScreen Instance;
        [SerializeField] Toggle chipNo1Btn;
        [SerializeField] Toggle chipNo5Btn;
        [SerializeField] Toggle chipNo10Btn;
        [SerializeField] Toggle chipNo50Btn;
        [SerializeField] Toggle chipNo100Btn;
        [SerializeField] Toggle chipNo500Btn;
        [SerializeField] Toggle chipNo1000Btn;
        [SerializeField] Toggle chipNo5000Btn;
        [SerializeField] Toggle chipNo1Btn_Middle;
        [SerializeField] Toggle chipNo5Btn_Middle;
        [SerializeField] Toggle chipNo10Btn_Middle;
        [SerializeField] Toggle chipNo50Btn_Middle;
        [SerializeField] Toggle chipNo100Btn_Middle;
        [SerializeField] Toggle chipNo500Btn_Middle;
        [SerializeField] Toggle chipNo1000Btn_Middle;
        [SerializeField] Toggle chipNo5000Btn_Middle;
        [SerializeField] RectTransform[] coins;
        [SerializeField] RectTransform[] coins_middle;
        int currentTime = 0, timer_CountDown, totalBets, previousBets;
        int currentlySelectedChip = 1;
        float balance;
        bool canStopTimer, canPlaceBet, isBetConfirmed;
        public bool isTimeUp;
        [SerializeField] Button betOkBtn;
        [SerializeField] Button takeBtn;
        [SerializeField] Button exitbtn;
        [SerializeField] Button cancelbtn;
        public Button repeatbutton;
        public GameObject turnonDisplay, cam, eventhandler;

        int Winno;
        int winningAmount = 0;
        public TextMeshProUGUI timerTxt;
        [SerializeField] Text totalBetsTxt;
        [SerializeField] Text balanceTxt;
        [SerializeField] Text WinText;
        [SerializeField] Text MessagePopup;
        public List<int> StraightUpBets = new List<int>();
        public List<int> StraightUpValue = new List<int>();
        int[] straightBets_Array, straightValue_Array;
        public List<string> SplitBets = new List<string>();
        public List<int> SplitValue = new List<int>();
        int[] splitValue_Array;
        public int[,] SplitArray = new int[0, 0];
        public List<string> StreetBets = new List<string>();
        public List<int> StreetValue = new List<int>();
        int[] streetValue_Array;
        public int[,] StreetArray = new int[0, 0];
        public List<string> CornerBets = new List<string>();
        public List<int> CornerValue = new List<int>();
        int[] cornerValue_Array;
        public int[,] CornerArray = new int[0, 0];
        public List<string> SpecificBets = new List<string>();
        public List<int> SpecificValue = new List<int>();
        int[] SpecificValue_Array;
        public int[] SpecificArray;
        public List<string> LineBets = new List<string>();
        public List<int> LineValue = new List<int>();
        int[] LineValue_Array;
        public int[,] LineArray = new int[0, 0];
        //change from here
        public List<string> dozen01Bets = new List<string>();
        public List<int> dozen01Value = new List<int>();
        int[] dozen01Value_Array;
        string lastflag;
        public List<string> previous_straight, previous_split, previous_street, previous_corner, previous_line, previous_dozen01;
        public List<int> previous_straightvalues, previous_splitvalue, previous_streetvalue, previous_cornervalue, previous_linevalues, previous_dozen01values;
        public int previous_dozenvalue01, previous_dozenvalue02, previous_dozenvalue03, previous_columvalue01, previous_columnvalue02, previous_columnvalue03,
        previous_onetoEighteenValue, previous_nineteentoThirtysixValue, previous_evenValue, previous_oddValue, previous_blackValue, previous_redValue;
        public int[,] dozen01Array;


        public int dozenValue01, dozenValue02, dozenValue03, ColumnValue01, ColumnValue02, ColumnValue03,
            onetoEighteenValue, nineteentoThirtysixValue, evenValue, oddValue, blackValue, redValue;
        int flag_straightUp = 0, flag_Split = 0, flag_Street = 0, flag_Corner = 0, flag_Specific = 0, flag_Line = 0, flag_dozen01 = 0, flag_dozen02 = 0, flag_dozen03 = 0;
        int straightBets_modify, splitBets_modify, streetBets_modify, cornerBets_modify, specificBets_modify, lineBets_modify, dozen01Bets_modify, dozen02Bets_modify, dozen03Bets_modify;
        public List<Button> BetsNumber, SplitBets_Button, CornerBets_Button, StreetBets_Button, SpecificBets_Button,
        LineBets_Button, dozenBets_button, ColumnBets_Button;
        public Button evenBets_button, oddBets_button, redBets_button, blackBets_button, onetoEighteenBets_Button,
        nineteentoThirtysixBets_Button;
        bool _playblinkAnim;
        [SerializeField] List<Text> PreviousWin_Text;
        [SerializeField] List<Text> llocaltext;
        [SerializeField] List<int> PreviousWin_local;
        [SerializeField] List<string> PreviousWin_list;
        List<int> previouswinlocalstore;//only used when the take button is not used for clearing amount
        int count;
        public GameObject spin_AudioSource, timer_AudioSource, coinsaudio, betAudioSource;
        public GameObject _tableImage;
        public GameObject _chipObj_original, _chipObj_Middle, _chipImage, _transitionchipImage, chipImage_OriginalPos,
        _cancelSpecificBet_Left, _cancelSpecificBet_bottom, _cancelSPecificBet_transition,
        _cancelBet_Right, _cancelBet_bottom, _cancelBet_transition,
        _wheelZoom_Left, _wheelZoom_Right, _wheelZoom_transition, rouletteObj;
        bool _changePos_Objs, _originalPos_Objs;
        private bool OnZoom;
        private bool bet;
        private Button lastused;
        public SceneHandler sceneHandler;
        public GameObject selected;
        public GameObject d_selected;
        [SerializeField] GameObject[] eighteen;
        [SerializeField] GameObject[] nine;
        [SerializeField] GameObject[] even;
        [SerializeField] GameObject[] odd;
        [SerializeField] GameObject[] dozen1;
        [SerializeField] GameObject[] dozen2;
        [SerializeField] GameObject[] dozen3;
        [SerializeField] GameObject[] red;
        [SerializeField] GameObject[] black;
        float winround;
        bool continous;
        int currentwin;
        bool transfer;
        bool taken;
        public Animator wheelobj;
        string winamounturl = "http://139.59.92.165:5000/user/Winamount";
        string emptyurl = "http://139.59.92.165:5000/user/DeletePreviousWinamount";
        string previousurl = "http://139.59.92.165:5000/user/roulette";
        public GameObject rightnew;
        public GameObject leftnew;
        //bool spinnow = true;
        public bool spinnoww
        {
            get
            {
                if (int.Parse(WinText.text) > 0)//if(winnerAmount>0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
        //bool spinnoww;
        public GameObject betok;
        int[] valuesforlowest = new int[38];


        void Awake()
        {
            Instance = this;

        }

        // Start is called before the first frame update
        void Start()
        {

            betok = betOkBtn.gameObject;
            if (PlayerPrefs.GetFloat("points") < 0)
            {
                balance = 0;
            }
            else
            {
                balance = PlayerPrefs.GetFloat("points");
            }

            balanceTxt.text = balance.ToString("F2");
            //winningAmount = 0;
            totalBetsTxt.text = totalBets.ToString();
            if(spinnoww)
            {
                StartCoroutine(RepeatBlinkAnim());
            }
            else{
                repeatbtn.GetComponent<Button>().interactable = false;
            }
            
            //if(Roue)
            if (PlayerPrefs.GetString("roulettestarted") == "true")
            {
                savebinary.LoadPlayerroulette();
                PlayerPrefs.SetString("roulettestarted", "false");

            }
            //StartCoroutine(BetBlinkAnim());
            //canPlaceBet = true;
            AddListeners();

            //Debug.Log("initial value"+WinText.text);
            selector(0);
            // if(Roulettewithin.winnervalue >0)
            // {
            //     WinText.text = Roulettelast.winnervalue.ToString();

            // }

            //Debug.Log("Roullete Pause :" + PlayerPrefs.GetString("roulettepaused") );


            //restorewithin();

            //spinnoww = true;
        }
        bool started;
        public void restoration()
        {
            started = true;
            //StartCoroutine(roulettewinresponse());
            //Debug.Log("striaght"+Roulettelast.Straightbetlast.Count);
            //Debug.Log("Split"+Roulettelast.Splittbetlast.Count);
            //Debug.Log("Corner"+Roulettelast.CornerBetlast.Count);
            //Debug.Log("Street"+Roulettelast.Streetbetlast.Count);
            //Debug.Log("Line"+Roulettelast.Linebetlast.Count);

            if (int.Parse(WinText.text) > 0)
            {
                StartCoroutine(TakeBlink());
            }
            if (Roulettewithin.betconfirmed && PlayerPrefs.GetInt("rouletterounds") == Roulette_ServerResponse.instance.rouletterounds)//(PlayerPrefs.GetString("roulettepaused") == "true" || PlayerPrefs.GetInt("addedBets") == 1 || Roulettelast.addedinfile == 1 )
            {
                //Debug.Log("THIS IS THE ROUND COUNT"+Roulettewithin.Roundcount);
                //Debug.Log("VALUE FROM ROULETTEROUNDS" + Roulette_ServerResponse.instance.rouletterounds);
                if (Roulette_ServerResponse.instance.rouletterounds == Roulettewithin.Roundcount || currentwin > 0)
                {
                    //Debug.Log("restorerestorerestorerestorerestorerestorerestorerestore");

                    restore();
                    //StartCoroutine(previous)
                    //takeBtn.interactable = true;
                    cancelbtn.interactable = false;
                    //restorewithin();
                }
                // else
                // {

                // }
                isBetConfirmed = true;
                //takeBtn.interactable = false;
                canPlaceBet = false;
                repeatbutton.interactable = false;
                repeatbtn.SetActive(false);
                betOkBtn.gameObject.SetActive(true);
                betOkBtn.interactable = false;
                repeatbutton.interactable = false;
                cancelbtn.interactable = false;
                

            }
            else
            {
                isBetConfirmed = false;
                canPlaceBet = true;
                betOkBtn.interactable = true;
                repeatbutton.interactable = true;
                
                repeatbtn.SetActive(true);
                repeatbutton.interactable = true;
                cancelbtn.interactable = true;
                //takeBtn.interactable = false;
            }
            //
            // if(Roulettelast.addedinfile ==1 && Roulettelast.round == Roulette_ServerResponse.instance.rouletterounds)
            // {
            //     isBetConfirmed = true;
            //     //takeBtn.interactable = false;
            //     canPlaceBet = false;
            //     repeatbutton.interactable = false;
            //     repeatbtn.SetActive(false);
            //     betOkBtn.gameObject.SetActive(true);
            //     betOkBtn.interactable = false;
            //     repeatbutton.interactable = false;
            //     cancelbtn.interactable = false;
            // }
            
            //Invoke("UpdateUI",2f);
        }

        void Update()
        {
            if(Application.platform == RuntimePlatform.Android)
            {
                if(Input.GetKeyDown(KeyCode.Escape))
                {
                    close();
                }
            }
            if (_changePos_Objs == true)
            {
                ChipImageMove();
            }
            else if (_originalPos_Objs == true)
            {
                //ResetUi_Pos();
            }

        }

        bool isPause;

        void OnApplicationPause(bool hasFocus)
        {
            isPause = hasFocus;
            if (isPause)
            {
                if (isBetConfirmed)
                {
                    PlayerPrefs.SetString("roulettepaused", "true");
                    if (totalBets > 0)
                    {
                        store();
                        File.Delete(SaveFilePath);
                        savebinary.savefunctionroulette();
                    }


                    storewithin();
                    PlayerPrefs.SetInt("rouletterounds", Roulette_ServerResponse.instance.rouletterounds);
                    StopCoroutine(Timer());
                }
                else
                {
                    withingame.confirmed = false;
                }

            }
            if (!isPause)
            {
                //Debug.Log("back to the game");
                PlayerPrefs.SetInt("rreload", 1);
                Roulette_ServerRequest.intance.socket.Emit(Utility.Events.onleaveRoom);
                //Roulette_ServerResponse.instance.socketoff();
                //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            // isPause = hasFocus;

            // if (Application.isEditor) return;
            // // {

            // if(isPause)
            // {
            //     Debug.Log("Focus True:" +hasFocus);
            //     StopAllCoroutines();
            //     ServerResponse.instance.socketoff();
            //     FunTarget_ServerResponse.instance.socketoff();
            //     TripleChance_ServerResponse.Instantiate.socketoff();
            //     //7Up&DownServerResponse.Instance.socketoff();

            //     // AndarBahar_ServerResponse.Instance.socketoff();
            //     //SceneManager.LoadScene(1);
            //     //AndarBahar_ServerRequest.instance.socket.Emit(Utility.Events.onleaveRoom); //leave room;
            // }   
            // if (!isPause)
            // {

            //     Debug.Log("Focus False:" +hasFocus);

            //     // HomeScript.Instance.AndarBaharBtn();
            //     // AndarBahar_ServerResponse.Instance.socketOn();
            //     //SceneManager.LoadScene(SceneManager.GetActiveScene().name);                
            // }
            // }
        }

        string setplayeroffline = "http://139.59.92.165:5000/user/SetplayerOffline";
        public IEnumerator settingoffline()
        {
            WWWForm form = new WWWForm();
            string playername = "GK" + PlayerPrefs.GetString("email");
            form.AddField("email", playername);
            using (UnityWebRequest www = UnityWebRequest.Post(setplayeroffline, form))
            {
                yield return www.SendWebRequest();

                if (www.result != UnityWebRequest.Result.Success)
                {
                    Debug.Log(www.error);
                }
                else
                {
                    Debug.Log("Offline//Offline//Offline//Offline//Offline//Offline//Offline//Offline//Offline//Offline//");
                }
            }
        }
        private void OnApplicationQuit()
        {
            // if(totalBets > 0)
            // {
            //     store();
            // }
            Roulettelast.winnervalue = int.Parse(WinText.text);
            PlayerPrefs.SetString("funpaused", "false");
            //StartCoroutine(settingoffline());
        }

        void ChipImageMove()
        {
            if (Vector2.Distance(_chipImage.GetComponent<RectTransform>().anchoredPosition, _transitionchipImage.GetComponent<RectTransform>().anchoredPosition) <= 0)
            {
                //Debug.Log("stopped moving ");
                _changePos_Objs = false;
                _chipImage.SetActive(false);
                _chipObj_Middle.SetActive(true);
                _cancelSPecificBet_transition.SetActive(false);
                _cancelSpecificBet_bottom.SetActive(true);
                _cancelBet_transition.SetActive(false);
                _cancelBet_bottom.SetActive(true);
                _wheelZoom_transition.SetActive(false);
                _wheelZoom_Left.SetActive(true);
            }
            else
            {
                //Debug.Log("Moving ");
                _chipImage.GetComponent<RectTransform>().anchoredPosition = Vector2.MoveTowards(_chipImage.GetComponent<RectTransform>().anchoredPosition, _transitionchipImage.GetComponent<RectTransform>().anchoredPosition, 300.0f * Time.deltaTime);
                _cancelSPecificBet_transition.GetComponent<RectTransform>().anchoredPosition = Vector2.MoveTowards(_cancelSPecificBet_transition.GetComponent<RectTransform>().anchoredPosition, _cancelSpecificBet_bottom.GetComponent<RectTransform>().anchoredPosition, 300.0f * Time.deltaTime);
                _cancelBet_transition.GetComponent<RectTransform>().anchoredPosition = Vector2.MoveTowards(_cancelBet_transition.GetComponent<RectTransform>().anchoredPosition, _cancelBet_bottom.GetComponent<RectTransform>().anchoredPosition, 300.0f * Time.deltaTime);
                _wheelZoom_transition.GetComponent<RectTransform>().anchoredPosition = Vector2.MoveTowards(_wheelZoom_transition.GetComponent<RectTransform>().anchoredPosition, _wheelZoom_Left.GetComponent<RectTransform>().anchoredPosition, 350.0f * Time.deltaTime);
            }
        }
        public void test()
        {
            //Debug.Log("pressed");
        }

        void ResetUi_Pos()
        {
            if (Vector2.Distance(_chipImage.GetComponent<RectTransform>().anchoredPosition, chipImage_OriginalPos.GetComponent<RectTransform>().anchoredPosition) <= 0)
            {
                //Debug.Log("stopped moving ");
                _originalPos_Objs = false;
                _chipObj_original.SetActive(true);
                _chipObj_Middle.SetActive(false);
                _cancelSpecificBet_Left.SetActive(true);
                _cancelSpecificBet_bottom.SetActive(false);
                _cancelBet_Right.SetActive(true);
                _cancelBet_bottom.SetActive(false);
                _wheelZoom_Right.SetActive(true);
                _wheelZoom_Left.SetActive(false);
            }
            else
            {
                //Debug.Log("Moving ");
                _chipImage.GetComponent<RectTransform>().anchoredPosition = Vector2.MoveTowards(_chipImage.GetComponent<RectTransform>().anchoredPosition, chipImage_OriginalPos.GetComponent<RectTransform>().anchoredPosition, 300.0f * Time.deltaTime);
                _cancelSPecificBet_transition.GetComponent<RectTransform>().anchoredPosition = Vector2.MoveTowards(_cancelSPecificBet_transition.GetComponent<RectTransform>().anchoredPosition, _cancelSpecificBet_Left.GetComponent<RectTransform>().anchoredPosition, 300.0f * Time.deltaTime);
                _cancelBet_transition.GetComponent<RectTransform>().anchoredPosition = Vector2.MoveTowards(_cancelBet_transition.GetComponent<RectTransform>().anchoredPosition, _cancelBet_Right.GetComponent<RectTransform>().anchoredPosition, 300.0f * Time.deltaTime);
                _wheelZoom_transition.GetComponent<RectTransform>().anchoredPosition = Vector2.MoveTowards(_wheelZoom_transition.GetComponent<RectTransform>().anchoredPosition, _wheelZoom_Right.GetComponent<RectTransform>().anchoredPosition, 350.0f * Time.deltaTime);
            }
        }

        public void OnBetsPlaceResponse(string o)
        {
            BetResponce resp = JsonConvert.DeserializeObject<BetResponce>(o);
            if (resp.status == 200)
            {
                PlayerPrefs.SetString("points", resp.data.balance.ToString("F2"));
            }
        }

        private void AddListeners()
        {
            betOkBtn.onClick.AddListener(() =>
            {
                // if(totalBets > 0)
                // {
                Debug.Log("/////////////////////////////");
                OnBetsOk();
                // }
            });

            takeBtn.onClick.AddListener(() =>
            {
                SendTakeAmountRequest();
            });

            /*_cancelSpecificBet_Left.onClick.AddListener(() =>
            {
                SendTakeAmountRequest();
            });

            /*
            _cancelBet_Right.onClick.AddListener(()=>
            {
                ResetAllBets();
            });

            _cancelBet_bottom.onClick.AddListener(()=>
            {
                ResetAllBets();
            });*/

            chipNo1Btn.onValueChanged.AddListener((i) => { if (!i) return; currentlySelectedChip = 1; selector(0); });
            chipNo5Btn.onValueChanged.AddListener((i) => { if (!i) return; currentlySelectedChip = 5; selector(1); });
            chipNo10Btn.onValueChanged.AddListener((i) => { if (!i) return; currentlySelectedChip = 10; selector(2); });
            chipNo50Btn.onValueChanged.AddListener((i) => { if (!i) return; currentlySelectedChip = 50; selector(3); });
            chipNo100Btn.onValueChanged.AddListener((i) => { if (!i) return; currentlySelectedChip = 100; selector(4); });
            chipNo500Btn.onValueChanged.AddListener((i) => { if (!i) return; currentlySelectedChip = 500; selector(5); });
            chipNo1000Btn.onValueChanged.AddListener((i) => { if (!i) return; currentlySelectedChip = 1000; selector(6); });
            chipNo5000Btn.onValueChanged.AddListener((i) => { if (!i) return; currentlySelectedChip = 5000; selector(7); });

            chipNo1Btn_Middle.onValueChanged.AddListener((i) => { if (!i) return; currentlySelectedChip = 1; m_selector(0); });
            chipNo5Btn_Middle.onValueChanged.AddListener((i) => { if (!i) return; currentlySelectedChip = 5; m_selector(1); });
            chipNo10Btn_Middle.onValueChanged.AddListener((i) => { if (!i) return; currentlySelectedChip = 10; m_selector(2); });
            chipNo50Btn_Middle.onValueChanged.AddListener((i) => { if (!i) return; currentlySelectedChip = 50; m_selector(3); });
            chipNo100Btn_Middle.onValueChanged.AddListener((i) => { if (!i) return; currentlySelectedChip = 100; m_selector(4); });
            chipNo500Btn_Middle.onValueChanged.AddListener((i) => { if (!i) return; currentlySelectedChip = 500; m_selector(5); });
            chipNo1000Btn_Middle.onValueChanged.AddListener((i) => { if (!i) return; currentlySelectedChip = 1000; m_selector(6); });
            chipNo5000Btn_Middle.onValueChanged.AddListener((i) => { if (!i) return; currentlySelectedChip = 5000; m_selector(7); });

            AddSocketListners();
        }

        void AddSocketListners()
        {
            //Action onBadResponse = () => {  };

            // Roulette_ServerRequest.intance.ListenEvent(Utility.Events.OnWinNo, (json) => 
            // {
            //     Debug.Log("OnWinNo   " + json.ToString());
            //     StartCoroutine(OnRoundEnd(json));
            // });

            // Roulette_ServerRequest.intance.ListenEvent(Utility.Events.OnTimerStart, (json) =>
            // {
            //     OnTimerStart(json);
            // });

            // Roulette_ServerRequest.intance.ListenEvent(Utility.Events.OnTimeUp, (json) =>
            // {
            //     // BettingButtonInteractablity(false);
            //     isTimeUp = true;
            // });
        }
        public void currentcoin(int i)
        {
            currentlySelectedChip = i;
        }
        int oncecall = 0;
        bool called;
        bool zoomed;
        bool added;
        [SerializeField] Animator anime;

        public void AddBets(Button BetsPlaced)
        {

            if (isTimeUp)
            {
                return;
            }
            //Debug.Log("addbets");
            if (balance < currentlySelectedChip || winningAmount > 0)// || balance <  totalBets)
            {
                if (winningAmount > 0)
                {
                    //Debug.Log("idhar tak aaya tha" + "and Please Collect the Winning amount first");
                    //AndroidToastMsg.ShowAndroidToastMessage("Please Collect the Winning amount first");
                    MessagePopup.text = "Please Collect the Winning amount first";
                    return;
                }
                else
                {
                    //AndroidToastMsg.ShowAndroidToastMessage("not enough balance");
                    MessagePopup.text = "Not Enough Balance";
                    Debug.Log("not enough balance");
                    return;
                }

            }
            if (!canPlaceBet)
            {
                return;
            }


            //Debug.Log("///////////////////////////////////////");
            if (OnZoom)
            {
                // _chipObj_original.SetActive(false);
                // rouletteObj.SetActive(false);
                // _changePos_Objs = true;
                // _chipImage.SetActive(true);
                // _cancelSpecificBet_Left.SetActive(false);
                // _cancelSPecificBet_transition.SetActive(true);
                // _cancelBet_Right.SetActive(false);
                // _cancelBet_transition.SetActive(true);
                // _wheelZoom_transition.SetActive(true);
                // _wheelZoom_Right.SetActive(false);
            }
            flag_straightUp = 0;
            flag_Split = 0;
            flag_Street = 0;
            flag_Corner = 0;
            flag_Specific = 0;
            flag_Line = 0;
            if (!isBetConfirmed)
            {
                //totalBets = totalBets + currentlySelectedChip;
            }
            totalBetsTxt.text = totalBets.ToString();
            cancelbtn.interactable = true;
            betAudioSource.GetComponent<AudioSource>().Play();
            if (isBetConfirmed == false)
            {
                balance -= currentlySelectedChip;
            }
            lastused = BetsPlaced;
            if (BetsPlaced.gameObject.tag == "StraightUp" && isBetConfirmed == false)
            {
                // lastflag = "StraightUp";
                // BetsPlaced.transform.GetChild(1).GetComponent<Text>().enabled = true;
                // BetsPlaced.transform.GetChild(1).GetComponent<Text>().text = (int.Parse(BetsPlaced.transform.GetChild(1).GetComponent<Text>().text) + currentlySelectedChip).ToString();
                if (StraightUpBets.Count > 0)
                {
                    for (int i = 0; i < StraightUpBets.Count; i++)
                    {
                        if (int.Parse(BetsPlaced.name) == StraightUpBets[i])
                        {
                            flag_straightUp = 1;
                            straightBets_modify = i;
                            if (StraightUpValue[i] + currentlySelectedChip > 5000)
                            {
                                //Debug.Log("idhar nahi aana");
                                return;
                            }

                            break;
                        }
                        else
                        {
                            flag_straightUp = 0;
                            // StraightUpBets.Add(int.Parse(BetsPlaced.name));
                            // StraightUpValue.Add(int.Parse(BetsPlaced.transform.GetChild(1).GetComponent<Text>().text));
                        }
                    }
                    if (flag_straightUp == 1)
                    {
                        lastflag = "StraightUp";
                        BetsPlaced.transform.GetChild(1).GetComponent<Text>().enabled = true;
                        BetsPlaced.transform.GetChild(1).GetComponent<Text>().text = (int.Parse(BetsPlaced.transform.GetChild(1).GetComponent<Text>().text) + currentlySelectedChip).ToString();
                        totalBets = totalBets + currentlySelectedChip;
                        StraightUpValue[straightBets_modify] = int.Parse(BetsPlaced.transform.GetChild(1).GetComponent<Text>().text);

                        // BetsPlaced.transform.parent.GetChild(0).gameObject.SetActive(false);
                    }
                    else
                    {
                        lastflag = "StraightUp";
                        BetsPlaced.transform.GetChild(1).GetComponent<Text>().enabled = true;
                        BetsPlaced.transform.GetChild(1).GetComponent<Text>().text = (int.Parse(BetsPlaced.transform.GetChild(1).GetComponent<Text>().text) + currentlySelectedChip).ToString();
                        totalBets = totalBets + currentlySelectedChip;
                        StraightUpBets.Add(int.Parse(BetsPlaced.name));
                        StraightUpValue.Add(int.Parse(BetsPlaced.transform.GetChild(1).GetComponent<Text>().text));
                        BetsPlaced.transform.GetChild(0).GetComponent<Image>().enabled = true;
                        // for(int i =0;i<38;i++)
                        // {
                        //     if(int.Parse(BetsPlaced.name) == i)
                        //     {
                        //         valuesforlowest[i] += currentlySelectedChip; 
                        //     }
                        // }

                        // BetsPlaced.transform.parent.gameObject.transform.GetChild(0).GetComponent<Text>().enabled = false;
                    }
                }
                else
                {
                    lastflag = "StraightUp";
                    BetsPlaced.transform.GetChild(1).GetComponent<Text>().enabled = true;
                    BetsPlaced.transform.GetChild(1).GetComponent<Text>().text = (int.Parse(BetsPlaced.transform.GetChild(1).GetComponent<Text>().text) + currentlySelectedChip).ToString();
                    totalBets = totalBets + currentlySelectedChip;
                    //Debug.Log("idhartak");
                    StraightUpBets.Add(int.Parse(BetsPlaced.name));
                    StraightUpValue.Add(int.Parse(BetsPlaced.transform.GetChild(1).GetComponent<Text>().text));
                    BetsPlaced.transform.GetChild(0).GetComponent<Image>().enabled = true;
                    // for(int i =0;i<38;i++)
                    //     {
                    //         if(int.Parse(BetsPlaced.name) == i)
                    //         {
                    //             valuesforlowest[i] += currentlySelectedChip; 
                    //         }
                    //     }

                    // BetsPlaced.transform.parent.gameObject.transform.GetChild(0).GetComponent<Text>().enabled = false;
                }
                //Debug.Log("///////////////////idhar nahi aana");
            }

            else if (BetsPlaced.gameObject.tag == "SplitBets" && isBetConfirmed == false) /*&& (SplitValue[int.Parse(BetsPlaced.transform.GetChild(1).GetComponent<Text>().text)] + currentlySelectedChip <5000))*/   // n x 2
            {

                if (SplitBets.Count > 0)
                {

                    for (int i = 0; i < SplitBets.Count; i++)
                    {
                        if (BetsPlaced.name == SplitBets[i])
                        {
                            flag_Split = 1;
                            splitBets_modify = i;
                            if (SplitValue[i] + currentlySelectedChip > 5000)
                            {
                                return;
                            }
                            break;
                        }
                        else
                        {
                            flag_Split = 0;
                        }
                    }
                    if (flag_Split == 1)
                    {
                        lastflag = "SplitBets";
                        BetsPlaced.transform.GetChild(1).GetComponent<Text>().enabled = true;
                        BetsPlaced.transform.GetChild(1).GetComponent<Text>().text = (int.Parse(BetsPlaced.transform.GetChild(1).GetComponent<Text>().text) + currentlySelectedChip).ToString();
                        SplitValue[splitBets_modify] = int.Parse(BetsPlaced.transform.GetChild(1).GetComponent<Text>().text);
                        totalBets = totalBets + currentlySelectedChip;
                        // BetsPlaced.transform.parent.GetChild(0).gameObject.SetActive(false);
                    }
                    else
                    {
                        lastflag = "SplitBets";
                        BetsPlaced.transform.GetChild(1).GetComponent<Text>().enabled = true;
                        BetsPlaced.transform.GetChild(1).GetComponent<Text>().text = (int.Parse(BetsPlaced.transform.GetChild(1).GetComponent<Text>().text) + currentlySelectedChip).ToString();
                        SplitBets.Add(BetsPlaced.name);
                        SplitValue.Add(int.Parse(BetsPlaced.transform.GetChild(1).GetComponent<Text>().text));
                        BetsPlaced.transform.GetChild(0).GetComponent<Image>().enabled = true;
                        totalBets = totalBets + currentlySelectedChip;
                        // BetsPlaced.transform.parent.gameObject.transform.GetChild(0).GetComponent<Text>().enabled = false;
                    }
                    SplitArray = new int[0, 0];
                    //Array.Clear(SplitArray, 0, SplitArray.Length);
                    // for (int i = 0; i < SplitBets.Count; i++)
                    // {
                    //     SplitBets.RemoveAt(i);
                    // }
                    SplitArray = new int[SplitBets.Count, 2];

                    //SplitArray = new int[SplitBets.Count][];
                    for (int i = 0; i < SplitBets.Count; i++)
                    {
                        string[] splitedArray = SplitBets[i].Split(Char.Parse("_"));
                        // SplitArray[i][0] = int.Parse(splitedArray[0]);
                        // SplitArray[i][1] = int.Parse(splitedArray[1]);
                        SplitArray[i, 0] = int.Parse(splitedArray[0]);
                        SplitArray[i, 1] = int.Parse(splitedArray[1]);
                    }
                }
                //////////////////////////////////////////////////////////////////
                else
                {
                    lastflag = "SplitBets";
                    BetsPlaced.transform.GetChild(1).GetComponent<Text>().enabled = true;
                    BetsPlaced.transform.GetChild(1).GetComponent<Text>().text = (int.Parse(BetsPlaced.transform.GetChild(1).GetComponent<Text>().text) + currentlySelectedChip).ToString();
                    SplitBets.Add(BetsPlaced.name);
                    SplitValue.Add(int.Parse(BetsPlaced.transform.GetChild(1).GetComponent<Text>().text));
                    BetsPlaced.transform.GetChild(0).GetComponent<Image>().enabled = true;
                    totalBets = totalBets + currentlySelectedChip;
                    // BetsPlaced.transform.parent.gameObject.transform.GetChild(0).GetComponent<Text>().enabled = false;
                    SplitArray = new int[SplitBets.Count, 2];
                    //SplitArray = new int[SplitBets.Count][];
                    if (SplitArray.Length > 0)
                    {
                        SplitArray = new int[0, 0];
                        //Array.Clear(SplitArray, 0, SplitArray.Length);
                        //SplitBets.RemoveAll();
                        //Array.Empty(SplitArray.Length);
                    }

                    // for (int i = 0; i < SplitBets.Count; i++)
                    // {
                    //     SplitBets.RemoveAt(i);
                    // }
                    SplitArray = new int[SplitBets.Count, 2];
                    for (int i = 0; i < SplitBets.Count; i++)
                    {
                        string[] splitedArray = SplitBets[i].Split(Char.Parse("_"));
                        SplitArray[i, 0] = int.Parse(splitedArray[0]);
                        SplitArray[i, 1] = int.Parse(splitedArray[1]);
                        // SplitArray[i][0] = int.Parse(splitedArray[0]);
                        // SplitArray[i][1] = int.Parse(splitedArray[1]);
                    }

                }

            }

            else if (BetsPlaced.gameObject.tag == "StreetBets" && isBetConfirmed == false)
            {
                // lastflag = "StreetBets";
                // BetsPlaced.transform.GetChild(1).GetComponent<Text>().enabled = true;
                // BetsPlaced.transform.GetChild(1).GetComponent<Text>().text = (int.Parse(BetsPlaced.transform.GetChild(1).GetComponent<Text>().text) + currentlySelectedChip).ToString();
                if (StreetBets.Count > 0)
                {
                    for (int i = 0; i < StreetBets.Count; i++)
                    {
                        if (BetsPlaced.name == StreetBets[i])
                        {
                            flag_Street = 1;
                            streetBets_modify = i;
                            if (StreetValue[i] + currentlySelectedChip > 5000)
                            {
                                //Debug.Log("idhar nahi aana");
                                return;
                            }
                            break;
                        }
                        else
                        {
                            flag_Street = 0;
                        }
                    }
                    if (flag_Street == 1)
                    {
                        lastflag = "StreetBets";
                        BetsPlaced.transform.GetChild(1).GetComponent<Text>().enabled = true;
                        BetsPlaced.transform.GetChild(1).GetComponent<Text>().text = (int.Parse(BetsPlaced.transform.GetChild(1).GetComponent<Text>().text) + currentlySelectedChip).ToString();
                        StreetValue[streetBets_modify] = int.Parse(BetsPlaced.transform.GetChild(1).GetComponent<Text>().text);
                        totalBets = totalBets + currentlySelectedChip;
                        // BetsPlaced.transform.parent.GetChild(0).gameObject.SetActive(false);
                    }
                    else
                    {
                        lastflag = "StreetBets";
                        BetsPlaced.transform.GetChild(1).GetComponent<Text>().enabled = true;
                        BetsPlaced.transform.GetChild(1).GetComponent<Text>().text = (int.Parse(BetsPlaced.transform.GetChild(1).GetComponent<Text>().text) + currentlySelectedChip).ToString();
                        StreetBets.Add(BetsPlaced.name);
                        StreetValue.Add(int.Parse(BetsPlaced.transform.GetChild(1).GetComponent<Text>().text));
                        BetsPlaced.transform.GetChild(0).GetComponent<Image>().enabled = true;
                        totalBets = totalBets + currentlySelectedChip;

                        // BetsPlaced.transform.parent.gameObject.transform.GetChild(0).GetComponent<Text>().enabled = false;
                    }
                    //StreetArray = new int[0,0];
                    Array.Clear(StreetArray, 0, StreetArray.Length);
                    StreetArray = new int[StreetBets.Count, 3];
                    for (int i = 0; i < StreetBets.Count; i++)
                    {
                        string[] streetedArray = StreetBets[i].Split(Char.Parse("_"));
                        StreetArray[i, 0] = int.Parse(streetedArray[0]);
                        StreetArray[i, 1] = int.Parse(streetedArray[1]);
                        StreetArray[i, 2] = int.Parse(streetedArray[2]);
                    }
                }
                else
                {
                    lastflag = "StreetBets";
                    BetsPlaced.transform.GetChild(1).GetComponent<Text>().enabled = true;
                    BetsPlaced.transform.GetChild(1).GetComponent<Text>().text = (int.Parse(BetsPlaced.transform.GetChild(1).GetComponent<Text>().text) + currentlySelectedChip).ToString();
                    StreetBets.Add(BetsPlaced.name);
                    StreetValue.Add(int.Parse(BetsPlaced.transform.GetChild(1).GetComponent<Text>().text));
                    BetsPlaced.transform.GetChild(0).GetComponent<Image>().enabled = true;
                    totalBets = totalBets + currentlySelectedChip;
                    // BetsPlaced.transform.parent.gameObject.transform.GetChild(0).GetComponent<Text>().enabled = false;
                    StreetArray = new int[StreetBets.Count, 3];
                    if (StreetArray.Length > 0)
                    {
                        //StreetArray = new int[0,0];
                        Array.Clear(StreetArray, 0, StreetArray.Length);
                    }
                    for (int i = 0; i < StreetBets.Count; i++)
                    {
                        string[] streetedArray = StreetBets[i].Split(Char.Parse("_"));
                        StreetArray[i, 0] = int.Parse(streetedArray[0]);
                        StreetArray[i, 1] = int.Parse(streetedArray[1]);
                        StreetArray[i, 2] = int.Parse(streetedArray[2]);
                    }
                }
            }

            else if (BetsPlaced.gameObject.tag == "CornerBets" && isBetConfirmed == false)
            {
                // lastflag ="CornerBets";
                // BetsPlaced.transform.GetChild(1).GetComponent<Text>().enabled = true;
                // BetsPlaced.transform.GetChild(1).GetComponent<Text>().text = (int.Parse(BetsPlaced.transform.GetChild(1).GetComponent<Text>().text) + currentlySelectedChip).ToString();
                if (CornerBets.Count > 0)
                {
                    for (int i = 0; i < CornerBets.Count; i++)
                    {
                        if (BetsPlaced.name == CornerBets[i])
                        {
                            flag_Corner = 1;
                            cornerBets_modify = i;
                            if (CornerValue[i] + currentlySelectedChip > 5000)
                            {
                                //Debug.Log("idhar nahi aana");
                                return;
                            }
                            break;
                        }
                        else
                        {
                            flag_Corner = 0;
                        }
                    }
                    if (flag_Corner == 1)
                    {
                        lastflag = "CornerBets";
                        BetsPlaced.transform.GetChild(1).GetComponent<Text>().enabled = true;
                        BetsPlaced.transform.GetChild(1).GetComponent<Text>().text = (int.Parse(BetsPlaced.transform.GetChild(1).GetComponent<Text>().text) + currentlySelectedChip).ToString();
                        CornerValue[cornerBets_modify] = int.Parse(BetsPlaced.transform.GetChild(1).GetComponent<Text>().text);
                        totalBets = totalBets + currentlySelectedChip;
                        // BetsPlaced.transform.parent.GetChild(0).gameObject.SetActive(false);
                    }
                    else
                    {
                        lastflag = "CornerBets";
                        BetsPlaced.transform.GetChild(1).GetComponent<Text>().enabled = true;
                        BetsPlaced.transform.GetChild(1).GetComponent<Text>().text = (int.Parse(BetsPlaced.transform.GetChild(1).GetComponent<Text>().text) + currentlySelectedChip).ToString();
                        CornerBets.Add(BetsPlaced.name);
                        CornerValue.Add(int.Parse(BetsPlaced.transform.GetChild(1).GetComponent<Text>().text));
                        BetsPlaced.transform.GetChild(0).GetComponent<Image>().enabled = true;
                        totalBets = totalBets + currentlySelectedChip;
                        // BetsPlaced.transform.parent.gameObject.transform.GetChild(0).GetComponent<Text>().enabled = false;
                    }
                    //CornerArray = new int[0,0];
                    Array.Clear(CornerArray, 0, CornerArray.Length);
                    CornerArray = new int[CornerBets.Count, 4];
                    for (int i = 0; i < CornerBets.Count; i++)
                    {
                        string[] corneredArray = CornerBets[i].Split(Char.Parse("_"));
                        CornerArray[i, 0] = int.Parse(corneredArray[0]);
                        CornerArray[i, 1] = int.Parse(corneredArray[1]);
                        CornerArray[i, 2] = int.Parse(corneredArray[2]);
                        CornerArray[i, 3] = int.Parse(corneredArray[3]);
                    }
                }
                else
                {
                    lastflag = "CornerBets";
                    BetsPlaced.transform.GetChild(1).GetComponent<Text>().enabled = true;
                    BetsPlaced.transform.GetChild(1).GetComponent<Text>().text = (int.Parse(BetsPlaced.transform.GetChild(1).GetComponent<Text>().text) + currentlySelectedChip).ToString();
                    CornerBets.Add(BetsPlaced.name);
                    CornerValue.Add(int.Parse(BetsPlaced.transform.GetChild(1).GetComponent<Text>().text));
                    BetsPlaced.transform.GetChild(0).GetComponent<Image>().enabled = true;
                    totalBets = totalBets + currentlySelectedChip;
                    // BetsPlaced.transform.parent.gameObject.transform.GetChild(0).GetComponent<Text>().enabled = false;
                    CornerArray = new int[CornerBets.Count, 4];
                    //CornerArray = new int[0,0];
                    if (CornerArray.Length > 0)
                    {
                        //CornerArray = new int[0,0];
                        Array.Clear(CornerArray, 0, CornerArray.Length);
                        //Array.Empty(CornerArray);
                    }
                    for (int i = 0; i < CornerBets.Count; i++)
                    {

                        string[] corneredArray = CornerBets[i].Split(Char.Parse("_"));

                        CornerArray[i, 0] = int.Parse(corneredArray[0]);
                        //Debug.Log("kkkkkkkkkkkk"+corneredArray[0]);
                        CornerArray[i, 1] = int.Parse(corneredArray[1]);
                        CornerArray[i, 2] = int.Parse(corneredArray[2]);
                        CornerArray[i, 3] = int.Parse(corneredArray[3]);

                    }
                }
            }

            else if (BetsPlaced.gameObject.tag == "SpecificBets" && isBetConfirmed == false)
            {
                // lastflag ="SpecificBets";
                // BetsPlaced.transform.GetChild(1).GetComponent<Text>().enabled = true;
                // BetsPlaced.transform.GetChild(1).GetComponent<Text>().text = (int.Parse(BetsPlaced.transform.GetChild(1).GetComponent<Text>().text) + currentlySelectedChip).ToString();
                if (SpecificBets.Count > 0)
                {
                    for (int i = 0; i < SpecificBets.Count; i++)
                    {
                        if (BetsPlaced.name == SpecificBets[i])
                        {
                            flag_Specific = 1;
                            specificBets_modify = i;
                            if (SpecificValue[i] + currentlySelectedChip > 5000)
                            {
                                //Debug.Log("idhar nahi aana");
                                return;
                            }
                            break;
                        }
                        else
                        {
                            flag_Specific = 0;
                        }
                    }
                    if (flag_Specific == 1)
                    {
                        lastflag = "SpecificBets";
                        BetsPlaced.transform.GetChild(1).GetComponent<Text>().enabled = true;
                        BetsPlaced.transform.GetChild(1).GetComponent<Text>().text = (int.Parse(BetsPlaced.transform.GetChild(1).GetComponent<Text>().text) + currentlySelectedChip).ToString();
                        SpecificValue[specificBets_modify] = int.Parse(BetsPlaced.transform.GetChild(1).GetComponent<Text>().text);
                        totalBets = totalBets + currentlySelectedChip;
                        // BetsPlaced.transform.parent.GetChild(0).gameObject.SetActive(false);
                    }
                    else
                    {
                        lastflag = "SpecificBets";
                        BetsPlaced.transform.GetChild(1).GetComponent<Text>().enabled = true;
                        BetsPlaced.transform.GetChild(1).GetComponent<Text>().text = (int.Parse(BetsPlaced.transform.GetChild(1).GetComponent<Text>().text) + currentlySelectedChip).ToString();
                        SpecificBets.Add(BetsPlaced.name);
                        SpecificValue.Add(int.Parse(BetsPlaced.transform.GetChild(1).GetComponent<Text>().text));
                        BetsPlaced.transform.GetChild(0).GetComponent<Image>().enabled = true;
                        totalBets = totalBets + currentlySelectedChip;
                        // BetsPlaced.transform.parent.gameObject.transform.GetChild(0).GetComponent<Text>().enabled = false;
                    }
                    Array.Clear(SpecificArray, 0, SpecificArray.Length);
                    SpecificArray = new int[5];
                    for (int i = 0; i < SpecificBets.Count; i++)
                    {
                        string[] specifiedArray = SpecificBets[i].Split(Char.Parse("_"));
                        SpecificArray[0] = int.Parse(specifiedArray[0]);
                        SpecificArray[1] = int.Parse(specifiedArray[1]);
                        SpecificArray[2] = int.Parse(specifiedArray[2]);
                        SpecificArray[3] = int.Parse(specifiedArray[3]);
                        SpecificArray[4] = int.Parse(specifiedArray[4]);
                    }
                }
                else
                {
                    lastflag = "SpecificBets";
                    BetsPlaced.transform.GetChild(1).GetComponent<Text>().enabled = true;
                    BetsPlaced.transform.GetChild(1).GetComponent<Text>().text = (int.Parse(BetsPlaced.transform.GetChild(1).GetComponent<Text>().text) + currentlySelectedChip).ToString();
                    SpecificBets.Add(BetsPlaced.name);
                    SpecificValue.Add(int.Parse(BetsPlaced.transform.GetChild(1).GetComponent<Text>().text));
                    BetsPlaced.transform.GetChild(0).GetComponent<Image>().enabled = true;
                    totalBets = totalBets + currentlySelectedChip;
                    // BetsPlaced.transform.parent.gameObject.transform.GetChild(0).GetComponent<Text>().enabled = false;
                    SpecificArray = new int[5];
                    if (SpecificArray.Length > 0)
                    {
                        Array.Clear(SpecificArray, 0, SpecificArray.Length);
                    }
                    for (int i = 0; i < SpecificBets.Count; i++)
                    {
                        // string[] specifiedArray = SpecificBets[i].Split(Char.Parse("_"));
                        // SpecificArray[i , 0] = int.Parse(specifiedArray[0]);
                        // SpecificArray[i , 1] = int.Parse(specifiedArray[1]);
                        // SpecificArray[i , 2] = int.Parse(specifiedArray[2]);
                        // SpecificArray[i , 3] = int.Parse(specifiedArray[3]);
                        // SpecificArray[i , 4] = int.Parse(specifiedArray[4]);
                        string[] specifiedArray = SpecificBets[i].Split(Char.Parse("_"));
                        SpecificArray[0] = int.Parse(specifiedArray[0]);
                        SpecificArray[1] = int.Parse(specifiedArray[1]);
                        SpecificArray[2] = int.Parse(specifiedArray[2]);
                        SpecificArray[3] = int.Parse(specifiedArray[3]);
                        SpecificArray[4] = int.Parse(specifiedArray[4]);
                        //[[0,00,1,2,3]]//,[00],1,2,3];
                    }
                }
            }

            else if (BetsPlaced.gameObject.tag == "LineBets" && isBetConfirmed == false)
            {
                // lastflag = "LineBets";
                // BetsPlaced.transform.GetChild(1).GetComponent<Text>().enabled = true;
                // BetsPlaced.transform.GetChild(1).GetComponent<Text>().text = (int.Parse(BetsPlaced.transform.GetChild(1).GetComponent<Text>().text) + currentlySelectedChip).ToString();
                if (LineBets.Count > 0)
                {
                    for (int i = 0; i < LineBets.Count; i++)
                    {
                        if (BetsPlaced.name == LineBets[i])
                        {
                            flag_Line = 1;
                            lineBets_modify = i;
                            if (LineValue[i] + currentlySelectedChip > 5000)
                            {
                                //Debug.Log("idhar nahi aana");
                                return;
                            }
                            break;
                        }
                        else
                        {
                            flag_Line = 0;
                        }
                    }
                    if (flag_Line == 1)
                    {
                        lastflag = "LineBets";
                        BetsPlaced.transform.GetChild(1).GetComponent<Text>().enabled = true;
                        BetsPlaced.transform.GetChild(1).GetComponent<Text>().text = (int.Parse(BetsPlaced.transform.GetChild(1).GetComponent<Text>().text) + currentlySelectedChip).ToString();
                        LineValue[lineBets_modify] = int.Parse(BetsPlaced.transform.GetChild(1).GetComponent<Text>().text);
                        totalBets = totalBets + currentlySelectedChip;
                        // BetsPlaced.transform.parent.GetChild(0).gameObject.SetActive(false);
                    }
                    else
                    {
                        lastflag = "LineBets";
                        BetsPlaced.transform.GetChild(1).GetComponent<Text>().enabled = true;
                        BetsPlaced.transform.GetChild(1).GetComponent<Text>().text = (int.Parse(BetsPlaced.transform.GetChild(1).GetComponent<Text>().text) + currentlySelectedChip).ToString();
                        LineBets.Add(BetsPlaced.name);
                        LineValue.Add(int.Parse(BetsPlaced.transform.GetChild(1).GetComponent<Text>().text));
                        BetsPlaced.transform.GetChild(0).GetComponent<Image>().enabled = true;
                        totalBets = totalBets + currentlySelectedChip;
                        // BetsPlaced.transform.parent.gameObject.transform.GetChild(0).GetComponent<Text>().enabled = false;
                    }
                    //LineArray = new int[0,0];
                    Array.Clear(LineArray, 0, LineArray.Length);
                    LineArray = new int[LineBets.Count, 6];
                    for (int i = 0; i < LineBets.Count; i++)
                    {
                        string[] linedArray = LineBets[i].Split(Char.Parse("_"));
                        LineArray[i, 0] = int.Parse(linedArray[0]);
                        LineArray[i, 1] = int.Parse(linedArray[1]);
                        LineArray[i, 2] = int.Parse(linedArray[2]);
                        LineArray[i, 3] = int.Parse(linedArray[3]);
                        LineArray[i, 4] = int.Parse(linedArray[4]);
                        LineArray[i, 5] = int.Parse(linedArray[5]);
                    }
                }
                else
                {
                    lastflag = "LineBets";
                    BetsPlaced.transform.GetChild(1).GetComponent<Text>().enabled = true;
                    BetsPlaced.transform.GetChild(1).GetComponent<Text>().text = (int.Parse(BetsPlaced.transform.GetChild(1).GetComponent<Text>().text) + currentlySelectedChip).ToString();
                    LineBets.Add(BetsPlaced.name);
                    LineValue.Add(int.Parse(BetsPlaced.transform.GetChild(1).GetComponent<Text>().text));
                    BetsPlaced.transform.GetChild(0).GetComponent<Image>().enabled = true;
                    totalBets = totalBets + currentlySelectedChip;
                    // BetsPlaced.transform.parent.gameObject.transform.GetChild(0).GetComponent<Text>().enabled = false;
                    LineArray = new int[LineBets.Count, 6];
                    if (LineArray.Length > 0)
                    {
                        //LineArray = new int[0,0];
                        Array.Clear(LineArray, 0, LineArray.Length);
                    }
                    for (int i = 0; i < LineBets.Count; i++)
                    {
                        string[] linedArray = LineBets[i].Split(Char.Parse("_"));
                        LineArray[i, 0] = int.Parse(linedArray[0]);
                        LineArray[i, 1] = int.Parse(linedArray[1]);
                        LineArray[i, 2] = int.Parse(linedArray[2]);
                        LineArray[i, 3] = int.Parse(linedArray[3]);
                        LineArray[i, 4] = int.Parse(linedArray[4]);
                        LineArray[i, 5] = int.Parse(linedArray[5]);
                    }
                }
            }

            else if (BetsPlaced.gameObject.tag == "dozen01" && isBetConfirmed == false)
            {

                if (dozenValue01 + currentlySelectedChip > 5000)
                {
                    return;
                }
                lastflag = "dozen01";
                BetsPlaced.transform.GetChild(1).GetComponent<Image>().enabled = true;
                BetsPlaced.transform.GetChild(2).GetComponent<Text>().enabled = true;
                BetsPlaced.transform.GetChild(2).GetComponent<Text>().text = (int.Parse(BetsPlaced.transform.GetChild(2).GetComponent<Text>().text) + currentlySelectedChip).ToString();
                dozenValue01 = int.Parse(BetsPlaced.transform.GetChild(2).GetComponent<Text>().text);
                totalBets = totalBets + currentlySelectedChip;
                // lastflag = "dozen01";
                // BetsPlaced.transform.GetChild(1).GetComponent<Image>().enabled = true;
                // BetsPlaced.transform.GetChild(2).GetComponent<Text>().enabled = true;
                // BetsPlaced.transform.GetChild(2).GetComponent<Text>().text = (int.Parse(BetsPlaced.transform.GetChild(2).GetComponent<Text>().text) + currentlySelectedChip).ToString();
                // dozenValue01 = int.Parse(BetsPlaced.transform.GetChild(2).GetComponent<Text>().text);
                //fromhere
                StartCoroutine(dozen1b());
                // if(dozen01Bets.Count > 0)
                // {
                //     for(int i = 0; i < dozen01Bets.Count; i++)
                //     {
                //         if( BetsPlaced.name == dozen01Bets[i] )
                //         {
                //             flag_dozen01 = 1;
                //             dozen01Bets_modify = i;
                //             if(dozenValue01 + currentlySelectedChip >5000)
                //             {
                //                 //Debug.Log("idhar nahi aana");
                //                 return;
                //             }
                //             break;
                //         }
                //         else
                //         {
                //             flag_dozen01 = 0;
                //         }
                //     }
                //     if(flag_Line == 1)
                //     {
                //         lastflag = "dozen01";
                //         BetsPlaced.transform.GetChild(1).GetComponent<Image>().enabled = true;
                //         BetsPlaced.transform.GetChild(2).GetComponent<Text>().enabled = true;
                //         BetsPlaced.transform.GetChild(2).GetComponent<Text>().text = (int.Parse(BetsPlaced.transform.GetChild(2).GetComponent<Text>().text) + currentlySelectedChip).ToString();
                //         dozenValue01 = int.Parse(BetsPlaced.transform.GetChild(2).GetComponent<Text>().text);
                //         dozen01Value[dozen01Bets_modify] = int.Parse(BetsPlaced.transform.GetChild(2).GetComponent<Text>().text);
                //         totalBets = totalBets + currentlySelectedChip;
                //         // BetsPlaced.transform.parent.GetChild(0).gameObject.SetActive(false);
                //     }
                //     else
                //     {
                //         lastflag = "dozen01";
                //         BetsPlaced.transform.GetChild(1).GetComponent<Image>().enabled = true;
                //         BetsPlaced.transform.GetChild(2).GetComponent<Text>().enabled = true;
                //         BetsPlaced.transform.GetChild(2).GetComponent<Text>().text = (int.Parse(BetsPlaced.transform.GetChild(2).GetComponent<Text>().text) + currentlySelectedChip).ToString();
                //         dozenValue01 = int.Parse(BetsPlaced.transform.GetChild(2).GetComponent<Text>().text);
                //         dozen01Bets.Add(BetsPlaced.name);
                //         dozen01Value.Add(int.Parse(BetsPlaced.transform.GetChild(2).GetComponent<Text>().text));
                //         BetsPlaced.transform.GetChild(0).GetComponent<Image>().enabled = true;
                //         totalBets = totalBets + currentlySelectedChip;
                //         // BetsPlaced.transform.parent.gameObject.transform.GetChild(0).GetComponent<Text>().enabled = false;
                //     }
                //     Array.Clear(dozen01Array, 0, dozen01Array.Length);
                //     dozen01Array = new int[dozen01Bets.Count , 6];
                //     for(int i = 0; i < LineBets.Count; i++)
                //     {
                //         string[] dozened01Array = dozen01Bets[i].Split(Char.Parse("_"));
                //         dozen01Array[i , 0] = int.Parse(dozened01Array[0]);
                //         dozen01Array[i , 1] = int.Parse(dozened01Array[1]);
                //         dozen01Array[i , 2] = int.Parse(dozened01Array[2]);
                //         dozen01Array[i , 3] = int.Parse(dozened01Array[3]);
                //         dozen01Array[i , 4] = int.Parse(dozened01Array[4]);
                //         dozen01Array[i , 5] = int.Parse(dozened01Array[5]);
                //         dozen01Array[i , 6] = int.Parse(dozened01Array[6]);
                //         dozen01Array[i , 7] = int.Parse(dozened01Array[7]);
                //         dozen01Array[i , 8] = int.Parse(dozened01Array[8]);
                //         dozen01Array[i , 9] = int.Parse(dozened01Array[9]);
                //         dozen01Array[i , 10] = int.Parse(dozened01Array[10]);
                //         dozen01Array[i , 11] = int.Parse(dozened01Array[11]);
                //     }
                // }
                // else
                // {
                //     lastflag = "dozen01";
                //     BetsPlaced.transform.GetChild(1).GetComponent<Image>().enabled = true;
                //     BetsPlaced.transform.GetChild(2).GetComponent<Text>().enabled = true;
                //     BetsPlaced.transform.GetChild(2).GetComponent<Text>().text = (int.Parse(BetsPlaced.transform.GetChild(2).GetComponent<Text>().text) + currentlySelectedChip).ToString();
                //     dozenValue01 = int.Parse(BetsPlaced.transform.GetChild(2).GetComponent<Text>().text);
                //     dozen01Bets.Add(BetsPlaced.name);
                //     dozen01Value.Add(int.Parse(BetsPlaced.transform.GetChild(2).GetComponent<Text>().text));
                //     BetsPlaced.transform.GetChild(1).GetComponent<Image>().enabled = true;
                //     totalBets = totalBets + currentlySelectedChip;
                //     // BetsPlaced.transform.parent.gameObject.transform.GetChild(0).GetComponent<Text>().enabled = false;
                //     dozen01Array = new int[dozen01Bets.Count , 6];
                //     if(dozen01Array.Length > 0)
                //     {
                //         Array.Clear(dozen01Array, 0, dozen01Array.Length);
                //     }
                //     for(int i = 0; i < dozen01Bets.Count; i++)
                //     {
                //         string[] dozened01Array = dozen01Bets[i].Split(Char.Parse("_"));
                //         dozen01Array[i , 0] = int.Parse(dozened01Array[0]);
                //         dozen01Array[i , 1] = int.Parse(dozened01Array[1]);
                //         dozen01Array[i , 2] = int.Parse(dozened01Array[2]);
                //         dozen01Array[i , 3] = int.Parse(dozened01Array[3]);
                //         dozen01Array[i , 4] = int.Parse(dozened01Array[4]);
                //         dozen01Array[i , 5] = int.Parse(dozened01Array[5]);
                //         dozen01Array[i , 6] = int.Parse(dozened01Array[6]);
                //         dozen01Array[i , 7] = int.Parse(dozened01Array[7]);
                //         dozen01Array[i , 8] = int.Parse(dozened01Array[8]);
                //         dozen01Array[i , 9] = int.Parse(dozened01Array[9]);
                //         dozen01Array[i , 10] = int.Parse(dozened01Array[10]);
                //         dozen01Array[i , 11] = int.Parse(dozened01Array[11]);
                //     }

                // }
                //StartCoroutine(dozen1b());
                //till here
            }

            else if (BetsPlaced.gameObject.tag == "dozen02" && isBetConfirmed == false)
            {
                if (dozenValue02 + currentlySelectedChip > 5000)
                {
                    return;
                }
                lastflag = "dozen02";
                BetsPlaced.transform.GetChild(1).GetComponent<Image>().enabled = true;
                BetsPlaced.transform.GetChild(2).GetComponent<Text>().enabled = true;
                BetsPlaced.transform.GetChild(2).GetComponent<Text>().text = (int.Parse(BetsPlaced.transform.GetChild(2).GetComponent<Text>().text) + currentlySelectedChip).ToString();
                dozenValue02 = int.Parse(BetsPlaced.transform.GetChild(2).GetComponent<Text>().text);
                totalBets = totalBets + currentlySelectedChip;
                StartCoroutine(dozen2b());
            }

            else if (BetsPlaced.gameObject.tag == "dozen03" && isBetConfirmed == false)
            {
                if (dozenValue03 + currentlySelectedChip > 5000)
                {
                    return;
                }
                lastflag = "dozen03";
                BetsPlaced.transform.GetChild(1).GetComponent<Image>().enabled = true;
                BetsPlaced.transform.GetChild(2).GetComponent<Text>().enabled = true;
                BetsPlaced.transform.GetChild(2).GetComponent<Text>().text = (int.Parse(BetsPlaced.transform.GetChild(2).GetComponent<Text>().text) + currentlySelectedChip).ToString();
                dozenValue03 = int.Parse(BetsPlaced.transform.GetChild(2).GetComponent<Text>().text);
                totalBets = totalBets + currentlySelectedChip;
                StartCoroutine(dozen3b());
            }

            else if (BetsPlaced.gameObject.tag == "column01" && isBetConfirmed == false)
            {
                if (ColumnValue01 + currentlySelectedChip > 5000)
                {
                    return;
                }
                lastflag = "column01";
                BetsPlaced.transform.GetChild(1).GetComponent<Image>().enabled = true;
                BetsPlaced.transform.GetChild(2).GetComponent<Text>().enabled = true;
                BetsPlaced.transform.GetChild(2).GetComponent<Text>().text = (int.Parse(BetsPlaced.transform.GetChild(2).GetComponent<Text>().text) + currentlySelectedChip).ToString();
                ColumnValue01 = int.Parse(BetsPlaced.transform.GetChild(2).GetComponent<Text>().text);
                totalBets = totalBets + currentlySelectedChip;
            }

            else if (BetsPlaced.gameObject.tag == "column02" && isBetConfirmed == false)
            {
                if (ColumnValue02 + currentlySelectedChip > 5000)
                {
                    return;
                }
                lastflag = "column02";
                BetsPlaced.transform.GetChild(1).GetComponent<Image>().enabled = true;
                BetsPlaced.transform.GetChild(2).GetComponent<Text>().enabled = true;
                BetsPlaced.transform.GetChild(2).GetComponent<Text>().text = (int.Parse(BetsPlaced.transform.GetChild(2).GetComponent<Text>().text) + currentlySelectedChip).ToString();
                ColumnValue02 = int.Parse(BetsPlaced.transform.GetChild(2).GetComponent<Text>().text);
                totalBets = totalBets + currentlySelectedChip;
            }

            else if (BetsPlaced.gameObject.tag == "column03" && isBetConfirmed == false)
            {
                if (ColumnValue03 + currentlySelectedChip > 5000)
                {
                    return;
                }
                lastflag = "column03";
                BetsPlaced.transform.GetChild(1).GetComponent<Image>().enabled = true;
                BetsPlaced.transform.GetChild(2).GetComponent<Text>().enabled = true;
                BetsPlaced.transform.GetChild(2).GetComponent<Text>().text = (int.Parse(BetsPlaced.transform.GetChild(2).GetComponent<Text>().text) + currentlySelectedChip).ToString();
                ColumnValue03 = int.Parse(BetsPlaced.transform.GetChild(2).GetComponent<Text>().text);
                totalBets = totalBets + currentlySelectedChip;
            }

            else if (BetsPlaced.gameObject.tag == "OneToEighteen" && isBetConfirmed == false)
            {
                if (onetoEighteenValue + currentlySelectedChip > 5000)
                {
                    return;
                }
                lastflag = "OneToEighteen";
                BetsPlaced.transform.GetChild(1).GetComponent<Image>().enabled = true;
                BetsPlaced.transform.GetChild(2).GetComponent<Text>().enabled = true;
                BetsPlaced.transform.GetChild(2).GetComponent<Text>().text = (int.Parse(BetsPlaced.transform.GetChild(2).GetComponent<Text>().text) + currentlySelectedChip).ToString();
                onetoEighteenValue = int.Parse(BetsPlaced.transform.GetChild(2).GetComponent<Text>().text);
                totalBets = totalBets + currentlySelectedChip;
                StartCoroutine(onetoeighteenblink());
            }

            else if (BetsPlaced.gameObject.tag == "NineteenToThirtysix" && isBetConfirmed == false)
            {
                if (nineteentoThirtysixValue + currentlySelectedChip > 5000)
                {
                    return;
                }
                lastflag = "NineteenToThirtysix";
                BetsPlaced.transform.GetChild(1).GetComponent<Image>().enabled = true;
                BetsPlaced.transform.GetChild(2).GetComponent<Text>().enabled = true;
                BetsPlaced.transform.GetChild(2).GetComponent<Text>().text = (int.Parse(BetsPlaced.transform.GetChild(2).GetComponent<Text>().text) + currentlySelectedChip).ToString();
                nineteentoThirtysixValue = int.Parse(BetsPlaced.transform.GetChild(2).GetComponent<Text>().text);
                totalBets = totalBets + currentlySelectedChip;
                StartCoroutine(ninetothirty());
            }

            else if (BetsPlaced.gameObject.tag == "even" && isBetConfirmed == false)
            {
                if (evenValue + currentlySelectedChip > 5000)
                {
                    return;
                }
                lastflag = "even";
                BetsPlaced.transform.GetChild(1).GetComponent<Image>().enabled = true;
                BetsPlaced.transform.GetChild(2).GetComponent<Text>().enabled = true;
                BetsPlaced.transform.GetChild(2).GetComponent<Text>().text = (int.Parse(BetsPlaced.transform.GetChild(2).GetComponent<Text>().text) + currentlySelectedChip).ToString();
                evenValue = int.Parse(BetsPlaced.transform.GetChild(2).GetComponent<Text>().text);
                totalBets = totalBets + currentlySelectedChip;
                StartCoroutine(evenb());
            }

            else if (BetsPlaced.gameObject.tag == "odd" && isBetConfirmed == false)
            {
                if (oddValue + currentlySelectedChip > 5000)
                {
                    return;
                }
                lastflag = "odd";
                BetsPlaced.transform.GetChild(1).GetComponent<Image>().enabled = true;
                BetsPlaced.transform.GetChild(2).GetComponent<Text>().enabled = true;
                BetsPlaced.transform.GetChild(2).GetComponent<Text>().text = (int.Parse(BetsPlaced.transform.GetChild(2).GetComponent<Text>().text) + currentlySelectedChip).ToString();
                oddValue = int.Parse(BetsPlaced.transform.GetChild(2).GetComponent<Text>().text);
                totalBets = totalBets + currentlySelectedChip;
                StartCoroutine(oddb());
            }

            else if (BetsPlaced.gameObject.tag == "black" && isBetConfirmed == false)
            {
                if (blackValue + currentlySelectedChip > 5000)
                {
                    return;
                }
                lastflag = "black";
                BetsPlaced.transform.GetChild(0).GetComponent<Image>().enabled = true;
                BetsPlaced.transform.GetChild(1).GetComponent<Text>().enabled = true;
                BetsPlaced.transform.GetChild(1).GetComponent<Text>().text = (int.Parse(BetsPlaced.transform.GetChild(1).GetComponent<Text>().text) + currentlySelectedChip).ToString();
                blackValue = int.Parse(BetsPlaced.transform.GetChild(1).GetComponent<Text>().text);
                totalBets = totalBets + currentlySelectedChip;
                StartCoroutine(blackb());
            }

            else if (BetsPlaced.gameObject.tag == "red" && isBetConfirmed == false)
            {
                if (redValue + currentlySelectedChip > 5000)
                {
                    return;
                }
                lastflag = "red";
                BetsPlaced.transform.GetChild(0).GetComponent<Image>().enabled = true;
                BetsPlaced.transform.GetChild(1).GetComponent<Text>().enabled = true;
                BetsPlaced.transform.GetChild(1).GetComponent<Text>().text = (int.Parse(BetsPlaced.transform.GetChild(1).GetComponent<Text>().text) + currentlySelectedChip).ToString();
                redValue = int.Parse(BetsPlaced.transform.GetChild(1).GetComponent<Text>().text);
                totalBets = totalBets + currentlySelectedChip;
                StartCoroutine(redb());
            }
            //Debug.Log("the value of zoomed brfore calling the function"+zoomed);
; if (!zoomed)
            {
                zoomed = true;
                added = true;
                zoomineffect();
                repeatbtn.SetActive(false);
                betOkBtn.gameObject.SetActive(true);
                //anime.SetBool("zoomed",true);
            }
            if (isBetConfirmed == false)
            {
                oncecall = 1;
                betOkBtn.transform.GetChild(0).gameObject.SetActive(true);
            }
            if(!called)
            {
                called =true;
                StartCoroutine(BetBlinkAnim());
            }

            if (oncecall == 1 && isBetConfirmed == false)
            {
                //StartCoroutine(BetBlinkAnim());
                _wheelZoom_Right.SetActive(false);
                oncecall = 0;
            }
            UpdateUi();
            PlayerPrefs.SetInt("addedBets", 1);
            previousBets = totalBets;
        }
        IEnumerator onetoeighteenblink()
        {
            for (int i = 0; i < eighteen.Length; i++)
            {
                eighteen[i].GetComponent<Image>().enabled = true;
            }
            yield return new WaitForSeconds(0.5f);
            for (int i = 0; i < eighteen.Length; i++)
            {
                eighteen[i].GetComponent<Image>().enabled = false;
            }
            yield return new WaitForSeconds(0.5f);
            for (int i = 0; i < eighteen.Length; i++)
            {
                eighteen[i].GetComponent<Image>().enabled = true;
            }
            yield return new WaitForSeconds(0.5f);
            for (int i = 0; i < eighteen.Length; i++)
            {
                eighteen[i].GetComponent<Image>().enabled = false;
            }
        }
        IEnumerator ninetothirty()
        {
            for (int i = 0; i < nine.Length; i++)
            {
                nine[i].GetComponent<Image>().enabled = true;
            }
            yield return new WaitForSeconds(0.5f);
            for (int i = 0; i < nine.Length; i++)
            {
                nine[i].GetComponent<Image>().enabled = false;
            }
            yield return new WaitForSeconds(0.5f);
            for (int i = 0; i < nine.Length; i++)
            {
                nine[i].GetComponent<Image>().enabled = true;
            }
            yield return new WaitForSeconds(0.5f);
            for (int i = 0; i < nine.Length; i++)
            {
                nine[i].GetComponent<Image>().enabled = false;
            }
        }
        IEnumerator evenb()
        {
            for (int i = 0; i < even.Length; i++)
            {
                even[i].GetComponent<Image>().enabled = true;
            }
            yield return new WaitForSeconds(0.5f);
            for (int i = 0; i < even.Length; i++)
            {
                even[i].GetComponent<Image>().enabled = false;
            }
            yield return new WaitForSeconds(0.5f);
            for (int i = 0; i < even.Length; i++)
            {
                even[i].GetComponent<Image>().enabled = true;
            }
            yield return new WaitForSeconds(0.5f);
            for (int i = 0; i < even.Length; i++)
            {
                even[i].GetComponent<Image>().enabled = false;
            }
        }
        IEnumerator oddb()
        {
            for (int i = 0; i < odd.Length; i++)
            {
                odd[i].GetComponent<Image>().enabled = true;
            }
            yield return new WaitForSeconds(0.5f);
            for (int i = 0; i < odd.Length; i++)
            {
                odd[i].GetComponent<Image>().enabled = false;
            }
            yield return new WaitForSeconds(0.5f);
            for (int i = 0; i < odd.Length; i++)
            {
                odd[i].GetComponent<Image>().enabled = true;
            }
            yield return new WaitForSeconds(0.5f);
            for (int i = 0; i < odd.Length; i++)
            {
                odd[i].GetComponent<Image>().enabled = false;
            }
        }
        IEnumerator dozen1b()
        {
            for (int i = 0; i < dozen1.Length; i++)
            {
                dozen1[i].GetComponent<Image>().enabled = true;
            }
            yield return new WaitForSeconds(0.5f);
            for (int i = 0; i < dozen1.Length; i++)
            {
                dozen1[i].GetComponent<Image>().enabled = false;
            }
            yield return new WaitForSeconds(0.5f);
            for (int i = 0; i < dozen1.Length; i++)
            {
                dozen1[i].GetComponent<Image>().enabled = true;
            }
            yield return new WaitForSeconds(0.5f);
            for (int i = 0; i < dozen1.Length; i++)
            {
                dozen1[i].GetComponent<Image>().enabled = false;
            }
        }
        IEnumerator dozen2b()
        {
            for (int i = 0; i < dozen2.Length; i++)
            {
                dozen2[i].GetComponent<Image>().enabled = true;
            }
            yield return new WaitForSeconds(0.5f);
            for (int i = 0; i < dozen2.Length; i++)
            {
                dozen2[i].GetComponent<Image>().enabled = false;
            }
            yield return new WaitForSeconds(0.5f);
            for (int i = 0; i < dozen2.Length; i++)
            {
                dozen2[i].GetComponent<Image>().enabled = true;
            }
            yield return new WaitForSeconds(0.5f);
            for (int i = 0; i < dozen2.Length; i++)
            {
                dozen2[i].GetComponent<Image>().enabled = false;
            }
        }
        IEnumerator dozen3b()
        {
            for (int i = 0; i < dozen3.Length; i++)
            {
                dozen3[i].GetComponent<Image>().enabled = true;
            }
            yield return new WaitForSeconds(0.5f);
            for (int i = 0; i < dozen3.Length; i++)
            {
                dozen3[i].GetComponent<Image>().enabled = false;
            }
            yield return new WaitForSeconds(0.5f);
            for (int i = 0; i < dozen3.Length; i++)
            {
                dozen3[i].GetComponent<Image>().enabled = true;
            }
            yield return new WaitForSeconds(0.5f);
            for (int i = 0; i < dozen3.Length; i++)
            {
                dozen3[i].GetComponent<Image>().enabled = false;
            }
        }
        IEnumerator redb()
        {
            for (int i = 0; i < red.Length; i++)
            {
                red[i].GetComponent<Image>().enabled = true;
            }
            yield return new WaitForSeconds(0.5f);
            for (int i = 0; i < red.Length; i++)
            {
                red[i].GetComponent<Image>().enabled = false;
            }
            yield return new WaitForSeconds(0.5f);
            for (int i = 0; i < red.Length; i++)
            {
                red[i].GetComponent<Image>().enabled = true;
            }
            yield return new WaitForSeconds(0.5f);
            for (int i = 0; i < red.Length; i++)
            {
                red[i].GetComponent<Image>().enabled = false;
            }
        }
        IEnumerator blackb()
        {
            for (int i = 0; i < black.Length; i++)
            {
                black[i].GetComponent<Image>().enabled = true;
            }
            yield return new WaitForSeconds(0.5f);
            for (int i = 0; i < black.Length; i++)
            {
                black[i].GetComponent<Image>().enabled = false;
            }
            yield return new WaitForSeconds(0.5f);
            for (int i = 0; i < black.Length; i++)
            {
                black[i].GetComponent<Image>().enabled = true;
            }
            yield return new WaitForSeconds(0.5f);
            for (int i = 0; i < black.Length; i++)
            {
                black[i].GetComponent<Image>().enabled = false;
            }
        }
        bool alreadyadded;
        void betstesting()
        {
            // if(alreadyadded)
            // {
            //     return;
            // }
            Debug.Log("this things was called here");
            if (StraightUpValue.Count > 0)
            {
                for (int i = 0; i < StraightUpBets.Count; i++)
                {
                    //valuesforlowest[StraightUpBets[i]] += StraightUpValue[i];
                    valuesforlowest[StraightUpBets[i]] += (StraightUpValue[i] *36);
                }
            }
            if (SplitBets.Count > 0)
            {
                for (int i = 0; i < SplitBets.Count; i++)
                {
                    Debug.Log("the count of split bets" + SplitBets.Count +"the count of i"+i+"SPlitbets at i"+SplitBets[i]);
                    string[] splitedArray = SplitBets[i].Split(Char.Parse("_"));
                    Debug.Log("split here now"+int.Parse(splitedArray[0])+ "and the second value"+int.Parse(splitedArray[1]));
                    // SplitArray[i][0] = int.Parse(splitedArray[0]);
                    // SplitArray[i][1] = int.Parse(splitedArray[1]);
                    SplitArray[i, 0] = int.Parse(splitedArray[0]);
                    SplitArray[i, 1] = int.Parse(splitedArray[1]);

                    for (int k = 0; k < 38; k++)
                    {
                        if (int.Parse(splitedArray[0]) == k)
                        {
                            //valuesforlowest[k] += SplitValue[i] ;
                            valuesforlowest[k] += (SplitValue[i] *18);
                            Debug.Log("Bets logged in at count more than zero" + k);
                        }
                    }
                    for (int l = 0; l < 38; l++)
                    {
                        if (int.Parse(splitedArray[1]) == l)
                        {
                            //valuesforlowest[l]  += SplitValue[i];
                            valuesforlowest[l] += (SplitValue[i] *18 );
                            Debug.Log("Bets logged in at count more than zero" + l);
                        }
                    }

                }
            }
            if (StreetBets.Count > 0)
            {

                for (int i = 0; i < StreetBets.Count; i++)
                {
                    string[] streetedArray = StreetBets[i].Split(Char.Parse("_"));
                    StreetArray[i, 0] = int.Parse(streetedArray[0]);
                    StreetArray[i, 1] = int.Parse(streetedArray[1]);
                    StreetArray[i, 2] = int.Parse(streetedArray[2]);
                    for (int a = 0; a < 38; a++)
                    {
                        if (int.Parse(streetedArray[0]) == a)
                        {
                            //valuesforlowest[a] += StreetValue[i];
                            valuesforlowest[a] += (StreetValue[i]*12);
                        }
                        if (int.Parse(streetedArray[1]) == a)
                        {
                            //valuesforlowest[a] += StreetValue[i];
                            valuesforlowest[a] += (StreetValue[i]*12);
                        }
                        if (int.Parse(streetedArray[2]) == a)
                        {
                            //valuesforlowest[a] += StreetValue[i];
                            valuesforlowest[a] += (StreetValue[i]*12);
                        }
                    }
                }
            }
            if (CornerBets.Count > 0)
            {
                for (int i = 0; i < CornerBets.Count; i++)
                {

                    string[] corneredArray = CornerBets[i].Split(Char.Parse("_"));

                    CornerArray[i, 0] = int.Parse(corneredArray[0]);
                    CornerArray[i, 1] = int.Parse(corneredArray[1]);
                    CornerArray[i, 2] = int.Parse(corneredArray[2]);
                    CornerArray[i, 3] = int.Parse(corneredArray[3]);
                    for (int b = 0; b < 38; b++)
                    {
                        if (int.Parse(corneredArray[0]) == b)
                        {
                            //valuesforlowest[b] += CornerValue[i];
                            valuesforlowest[b] += (CornerValue[i] *9);
                        }
                        if (int.Parse(corneredArray[1]) == b)
                        {
                            //valuesforlowest[b] += CornerValue[i];
                            valuesforlowest[b] += (CornerValue[i] *9);
                        }
                        if (int.Parse(corneredArray[2]) == b)
                        {
                            //valuesforlowest[b] += CornerValue[i];
                            valuesforlowest[b] += (CornerValue[i] *9);
                        }
                        if (int.Parse(corneredArray[3]) == b)
                        {
                            //valuesforlowest[b] += CornerValue[i];
                            valuesforlowest[b] += (CornerValue[i] *9);
                        }
                    }

                }
            }
            if (LineBets.Count > 0)
            {
                for (int i = 0; i < LineBets.Count; i++)
                {
                    string[] linedArray = LineBets[i].Split(Char.Parse("_"));
                    LineArray[i, 0] = int.Parse(linedArray[0]);
                    LineArray[i, 1] = int.Parse(linedArray[1]);
                    LineArray[i, 2] = int.Parse(linedArray[2]);
                    LineArray[i, 3] = int.Parse(linedArray[3]);
                    LineArray[i, 4] = int.Parse(linedArray[4]);
                    LineArray[i, 5] = int.Parse(linedArray[5]);
                    for (int b = 0; b < 38; b++)
                    {
                        if (int.Parse(linedArray[0]) == b)
                        {
                            //valuesforlowest[b] += LineValue[i];
                            valuesforlowest[b] += (LineValue[i]*6);
                        }
                        if (int.Parse(linedArray[1]) == b)
                        {
                            //valuesforlowest[b] += LineValue[i];
                            valuesforlowest[b] += (LineValue[i]*6);
                        }
                        if (int.Parse(linedArray[2]) == b)
                        {
                            //valuesforlowest[b] += LineValue[i];
                            valuesforlowest[b] += (LineValue[i]*6);
                        }
                        if (int.Parse(linedArray[3]) == b)
                        {
                            //valuesforlowest[b] += LineValue[i];
                            valuesforlowest[b] += (LineValue[i]*6);
                        }
                        if (int.Parse(linedArray[4]) == b)
                        {
                            //valuesforlowest[b] += LineValue[i];
                            valuesforlowest[b] += (LineValue[i]*6);
                        }
                        if (int.Parse(linedArray[5]) == b)
                        {
                            //valuesforlowest[b] += LineValue[i];
                            valuesforlowest[b] += (LineValue[i]*6);
                        }
                    }
                }
            }
            if (redValue > 0)
            {
                for (int i = 0; i < redarrayv.Length; i++)
                {
                    int temp = redarrayv[i];
                    //valuesforlowest[temp] += redValue;
                    valuesforlowest[temp] += redValue *2;
                    Debug.LogError("the values were added at red" + temp);
                }
            }
            if (blackValue > 0)
            {
                for (int i = 0; i < blacarraykv.Length; i++)
                {
                    int temp = blacarraykv[i];
                    //valuesforlowest[temp] += blackValue;
                    valuesforlowest[temp] += blackValue *2;
                    Debug.LogError("the values were added at black" + temp);
                }
            }
            if (oddValue > 0)
            {
                for (int i = 0; i < oddarrayv.Length; i++)
                {
                    int temp = oddarrayv[i];
                    //valuesforlowest[temp] += oddValue;
                    valuesforlowest[temp] += oddValue *2;
                    
                }
                Debug.LogError("the values were added at odd");
            }
            if (evenValue > 0)
            {
                for (int i = 0; i < evenarrayv.Length; i++)
                {
                    int temp = evenarrayv[i];
                    //valuesforlowest[temp] += evenValue;
                    valuesforlowest[temp] += evenValue *2;
                }
                Debug.LogError("the values were added at even");
            }
            if (ColumnValue01 > 0)
            {
                for (int i = 0; i < columnn1v.Length; i++)
                {
                    int temp = columnn1v[i];
                    //valuesforlowest[temp] += ColumnValue01;
                    valuesforlowest[temp] += ColumnValue01*3;
                }
            }
            if (ColumnValue02 > 0)
            {
                for (int i = 0; i < columnn2v.Length; i++)
                {
                    int temp = columnn2v[i];
                    //valuesforlowest[temp] += ColumnValue02;
                    valuesforlowest[temp] += ColumnValue02*3;
                }
            }
            if (ColumnValue03 > 0)
            {
                for (int i = 0; i < columnn3v.Length; i++)
                {
                    int temp = columnn3v[i];
                    //valuesforlowest[temp] += ColumnValue03;
                    valuesforlowest[temp] += ColumnValue03*3;
                }
            }
            if (dozenValue01 > 0)
            {
                for (int i = 0; i < dozen1arrayv.Length; i++)
                {
                    int temp = dozen1arrayv[i];
                    //valuesforlowest[temp] += dozenValue01;
                    valuesforlowest[temp] += dozenValue01*3;
                }
            }
            if (dozenValue02 > 0)
            {
                for (int i = 0; i < dozen2arrayv.Length; i++)
                {
                    int temp = dozen2arrayv[i];
                    //valuesforlowest[temp] += dozenValue02;
                    valuesforlowest[temp] += dozenValue02*3;
                }
            }
            if (dozenValue03 > 0)
            {
                for (int i = 0; i < dozen3arrayv.Length; i++)
                {
                    int temp = dozen3arrayv[i];
                    //valuesforlowest[temp] += dozenValue03;
                    valuesforlowest[temp] += dozenValue03*3;
                }
            }
            if (onetoEighteenValue > 0)
            {
                for (int i = 0; i < onetoEighteenav.Length; i++)
                {
                    int temp = onetoEighteenav[i];
                    //valuesforlowest[temp] += onetoEighteenValue;
                    valuesforlowest[temp] += onetoEighteenValue*2;
                }
            }
            if (nineteentoThirtysixValue > 0)
            {
                for (int i = 0; i < nineteentoThirtysixav.Length; i++)
                {
                    int temp = nineteentoThirtysixav[i];
                    //valuesforlowest[temp] += nineteentoThirtysixValue;
                    valuesforlowest[temp] += nineteentoThirtysixValue *2;
                }
            }

            // for(int i=0;i<valuesforlowest.Length;i++)
            // {
            //     Debug.Log("Value At no "+i+" was "+valuesforlowest[i]);
            //     //Debug.Log("the size of spliarray was "+SplitArray.Length );
            // }
        }


        public void OnBetsOk()
        {

            //Debug.Log("bbbbbbbbbbbbblllllllllllajjjjjjjjjjjjjjj");
            if (totalBets == 0)
            {
                MessagePopup.text = "Place Bet to Start the game ";
                // commentTxt.text = "Bets Are Empty";
                return;
            }
            //SendBets();
            wheelright.SetActive(true);
            if (isTimeUp)
            {
                //AndroidToastMsg.ShowAndroidToastMessage("Time UP");
                MessagePopup.text = "Time UP";
                return;
            }
            Debug.Log("betsok called...  ");
            MessagePopup.text = "Bet has been accepted ";
            betstesting();
            alreadyadded = false;
            betOkBtn.interactable = false;
            repeatbtn.SetActive(false);
            betok.SetActive(true);
            cancelbtn.interactable = false;
            takeBtn.interactable = false;
            isBetConfirmed = true;
            SendBets();
            UpdateUi();
            betOkBtn.transform.GetChild(0).gameObject.SetActive(false);
            StopCoroutine(BetBlinkAnim());
            //StopBetBlinkAnim();
            oncecall = 0;

        }
        [SerializeField] GameObject middlewheel;
        public void zoomineffect()
        {
            rightnew.SetActive(false);
            leftnew.SetActive(true);
            // _wheelZoom_Right.SetActive(false);
            // _wheelZoom_Left.SetActive(true);
            anime.SetBool("zoomed", true);
            middlewheel.SetActive(false);
            Debug.Log("rightshould be off and left should be true ");

        }
        public void zoomouteffect()
        {
            rightnew.SetActive(true);
            leftnew.SetActive(false);
            // _wheelZoom_Right.SetActive(true);
            // _wheelZoom_Left.SetActive(false);
            Debug.Log("the wheel is zoomed off");
            anime.SetBool("zoomed", false);
            middlewheel.SetActive(true);

        }
        int countrounds;
        int straightsum, splitsum, cornersum, linesum, streetsum, specificsum;
        int[] dozen1array = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };// 
        int[] dozen2array = { 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24 };
        int[] dozen3array = { 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36 };
        int[] columnn1 = { 3, 6, 19, 12, 15, 18, 21, 24, 27, 30, 33, 36 };
        int[] columnn2 = { 2, 5, 8, 11, 14, 17, 20, 23, 26, 29, 32, 35 };
        int[] columnn3 = { 1, 4, 7, 10, 13, 16, 19, 22, 25, 28, 31, 34 };
        int[] onetoEighteena = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18 };
        int[] nineteentoThirtysixa = { 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36 };
        int[] evenarray = { 0, 2, 4, 6, 8, 10, 12, 14, 16, 18, 20, 22, 24, 26, 28, 30, 32, 34, 36 };
        int[] oddarray = { 1, 3, 5, 7, 9, 11, 13, 15, 17, 19, 21, 23, 25, 27, 29, 31, 33, 35 };
        int[] blacarrayk = { 2, 4, 6, 8, 10, 11, 13, 15, 17, 20, 22, 24, 26, 28, 29, 31, 33, 35 };
        int[] redarray = { 1, 3, 5, 7, 9, 12, 14, 16, 18, 19, 21, 23, 25, 27, 30, 32, 34, 36 };
        int[] dozen1arrayv = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };// 
        int[] dozen2arrayv = { 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24 };
        int[] dozen3arrayv = { 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36 };
        int[] columnn1v = { 3, 6, 19, 12, 15, 18, 21, 24, 27, 30, 33, 36 };
        int[] columnn2v = { 2, 5, 8, 11, 14, 17, 20, 23, 26, 29, 32, 35 };
        int[] columnn3v = { 1, 4, 7, 10, 13, 16, 19, 22, 25, 28, 31, 34 };
        int[] onetoEighteenav = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18 };
        int[] nineteentoThirtysixav = { 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36 };
        int[] evenarrayv = { 0, 2, 4, 6, 8, 10, 12, 14, 16, 18, 20, 22, 24, 26, 28, 30, 32, 34, 36 };
        int[] oddarrayv = { 1, 3, 5, 7, 9, 11, 13, 15, 17, 19, 21, 23, 25, 27, 29, 31, 33, 35 };
        int[] blacarraykv = { 2, 4, 6, 8, 10, 11, 13, 15, 17, 20, 22, 24, 26, 28, 29, 31, 33, 35 };
        int[] redarrayv = { 1, 3, 5, 7, 9, 12, 14, 16, 18, 19, 21, 23, 25, 27, 30, 32, 34, 36 };
        //int[] dozen1array = new int[1],dozen2array = new int[1],dozen3array = new int[1],columnn1= new int[1],columnn2= new int[1],columnn3= new int[1],onetoEighteena= new int[1],nineteentoThirtysixa= new int[1],evenarray= new int[1],oddarray= new int[1],blacarrayk= new int[1],redarray = new int[1];

        public void SendBets_Response(object data)
        {
            SendBet_Res res = JsonConvert.DeserializeObject<SendBet_Res>(data.ToString());//AndarBahar.Utility.Fuction.GetObjectOfType<SendBet_Res>(data);    
            if (res.status == 200)
            {
                //winnigText.text = res.message;
                balance = res.data.balance;

            }
            else if (res.status == 400)
            {
                //winnigText.text = res.message;
                balance = res.data.balance;
            }
            else if(res.status == 222)
            {
                CancelAllBets();
                MessagePopup.text ="Bets already accepted";
            }
            else
            {
                Debug.Log("none of the above");
            }
            balanceTxt.text = balance.ToString("F2");
        }
        
        private void SendBets()
        {
            if (!spinnoww)
            {
                MessagePopup.text = "Please Collect Winning amount First";
                return;
            }
            zoomouteffect();
            //anime.SetBool("zoomed",false);
            //exitbtn.interactable = false;
            string _playerName = "GK" + PlayerPrefs.GetString("email");
            straightsum = StraightUpValue.Sum();
            splitsum = SplitValue.Sum();
            cornersum = CornerValue.Sum();
            linesum = LineValue.Sum();
            streetsum = StreetValue.Sum();
            specificsum = SpecificValue.Sum();
            straightBets_Array = StraightUpBets.ToArray();
            straightValue_Array = StraightUpValue.ToArray();
            splitValue_Array = SplitValue.ToArray();
            streetValue_Array = StreetValue.ToArray();
            cornerValue_Array = CornerValue.ToArray();
            SpecificValue_Array = SpecificValue.ToArray();
            LineValue_Array = LineValue.ToArray();
            // if(splitValue_Array.Sum() ==0 )
            if (dozenValue01 == 0)
            {
                dozen1array = new int[0];
            }
            if (dozenValue02 == 0)
            {
                dozen2array = new int[0];
            }
            if (dozenValue03 == 0)
            {
                dozen3array = new int[0];
            }

            if (ColumnValue01 == 0)
            {
                columnn1 = new int[0];
            }
            if (ColumnValue02 == 0)
            {
                columnn2 = new int[0];
            }
            if (ColumnValue03 == 0)
            {
                columnn3 = new int[0];
            }
            if (onetoEighteenValue == 0)
            {
                onetoEighteena = new int[0];
            }
            if (nineteentoThirtysixValue == 0)
            {
                nineteentoThirtysixa = new int[0];
            }
            if (evenValue == 0)
            {
                evenarray = new int[0];
            }
            if (oddValue == 0)
            {
                oddarray = new int[0];
            }
            if (blackValue == 0)
            {
                blacarrayk = new int[0];
            }
            if (redValue == 0)
            {
                redarray = new int[0];
            }

            // {
            //     Array.Clear(splitValue_Array,0,splitValue_Array.Length);
            // }
            //column1 = ColumnValue01.ToArray();
            // dozen1array[0] = dozenValue01;//new int{1,2,3,4,5,6,7,8,9,10,11,12};
            // dozen2array[0] = dozenValue02;
            // dozen3array[0] = dozenValue03;
            // columnn1[0] = ColumnValue01;
            // columnn2[0] = ColumnValue02;
            // columnn3[0] = ColumnValue03;
            // onetoEighteena[0] = onetoEighteenValue;
            // nineteentoThirtysixa[0] = nineteentoThirtysixValue;
            // evenarray[0] = evenValue;
            // oddarray[0] = oddValue;
            // blacarrayk[0] = blackValue;
            // redarray[0] = redValue;


            //Debug.Log("Send bets called...  ");

            bets data = new bets
            {
                playerId = _playerName,
                straightUp = straightBets_Array,
                straightUpVal = straightValue_Array,
                Split = SplitArray,
                Street = StreetArray,
                SplitVal = splitValue_Array,
                StreetVal = streetValue_Array,
                Corner = CornerArray,
                CornerVal = cornerValue_Array,
                specificBet = SpecificArray,
                specificBetVal = SpecificValue_Array,
                line = LineArray,
                lineVal = LineValue_Array,
                dozen1 = dozen1array,
                dozen1Val = dozenValue01,
                dozen2 = dozen2array,
                dozen2Val = dozenValue02,
                dozen3 = dozen3array,
                dozen3Val = dozenValue03,
                column1 = columnn1,
                column1Val = ColumnValue01,
                column2 = columnn2,
                column2Val = ColumnValue02,
                column3 = columnn3,
                column3Val = ColumnValue03,
                onetoEighteen = onetoEighteena,
                onetoEighteenVal = onetoEighteenValue,
                nineteentoThirtysix = nineteentoThirtysixa,
                nineteentoThirtysixVal = nineteentoThirtysixValue,
                even = evenarray,
                evenVal = evenValue,
                odd = oddarray,
                oddVal = oddValue,
                black = blacarrayk,
                blackVal = blackValue,
                red = redarray,
                redVal = redValue,
                totalstraightUpVal = straightsum,
                totalSplitVal = splitsum,
                totalStreetVal = streetsum,
                totalCornerVal = cornersum,
                totalspecificBetVal = specificsum,
                totallineVal = linesum,
                totaldozen1Val = dozenValue01,
                totaldozen2Val = dozenValue02,
                totaldozen3Val = dozenValue03,
                totalcolumn1Val = ColumnValue01,
                totalcolumn2Val = ColumnValue02,
                totalcolumn3Val = ColumnValue03,
                totalonetoEighteen = onetoEighteenValue,
                totalnineteentoThirtysix = nineteentoThirtysixValue,
                totalevenVal = evenValue,
                totalodd = oddValue,
                totalblackVal = blackValue,
                totalredVal = redValue,
                Bet0 = valuesforlowest[0],
                Bet1 = valuesforlowest[1],
                Bet2 = valuesforlowest[2],
                Bet3 = valuesforlowest[3],
                Bet4 = valuesforlowest[4],
                Bet5 = valuesforlowest[5],
                Bet6 = valuesforlowest[6],
                Bet7 = valuesforlowest[7],
                Bet8 = valuesforlowest[8],
                Bet9 = valuesforlowest[9],
                Bet10 = valuesforlowest[10],
                Bet11 = valuesforlowest[11],
                Bet12 = valuesforlowest[12],
                Bet13 = valuesforlowest[13],
                Bet14 = valuesforlowest[14],
                Bet15 = valuesforlowest[15],
                Bet16 = valuesforlowest[16],
                Bet17 = valuesforlowest[17],
                Bet18 = valuesforlowest[18],
                Bet19 = valuesforlowest[19],
                Bet20 = valuesforlowest[20],
                Bet21 = valuesforlowest[21],
                Bet22 = valuesforlowest[22],
                Bet23 = valuesforlowest[23],
                Bet24 = valuesforlowest[24],
                Bet25 = valuesforlowest[25],
                Bet26 = valuesforlowest[26],
                Bet27 = valuesforlowest[27],
                Bet28 = valuesforlowest[28],
                Bet29 = valuesforlowest[29],
                Bet30 = valuesforlowest[30],
                Bet31 = valuesforlowest[31],
                Bet32 = valuesforlowest[32],
                Bet33 = valuesforlowest[33],
                Bet34 = valuesforlowest[34],
                Bet35 = valuesforlowest[35],
                Bet36 = valuesforlowest[36],
                Bet00 = valuesforlowest[37],


                points = totalBets
            };

            PostBet(data);
            //countrounds = 0;


            if (totalBets > 0)
            {
                store();
                //storewithin();
            }

            canPlaceBet = false;
            // if(totalBets == 0)
            // {
            //     Debug.Log("countrounds"+ countrounds);
            //     countrounds++;
            //     if(countrounds >=3)
            //     {
            //         close();
            //     }
            // }
            // else{
            //     countrounds = 0;
            // }
        }
        public void testing()
        {
            details data = new details
            {
                playerId = "GK" + PlayerPrefs.GetString("email")//playerName
            };
            Roulette_ServerRequest.intance.socket.Emit(Utility.Events.OnWinNo, new JSONObject(JsonConvert.SerializeObject(data)));
        }

        private void PostBet(bets data)
        {
            Debug.Log(new JSONObject(JsonConvert.SerializeObject(data).ToString()));
            Roulette_ServerRequest.intance.socket.Emit(Utility.Events.OnBetsPlaced, new JSONObject(JsonConvert.SerializeObject(data)));
            //Debug.Log("Post bets Called...  " + data.redVal.ToString() + "black" + data.blackVal.ToString());
            // Roulette_ServerRequest.intance.SendEvent(Utility.Events.OnBetsPlaced, data, (res) =>
            // {
            //     Debug.Log("is bet placed starus with statu - " + JsonUtility.FromJson<RouletteClasses.BetConfirmation>(res).status);
            // });

        }
        public void SendTakeAmountRequest()
        {
            if (spinnoww)
            {
                return;
            }
            repeatbutton.interactable = true;
            StopTakeBlink();
            StopCoroutine(TakeBlink());
            // balance = balance + winningAmount;
            // balanceTxt.text = balance.ToString();
            string _playername = "GK" + PlayerPrefs.GetString("email");
            //object o = new { playerId = _playername , win_points = winningAmount};
            Take_Bet data = new Take_Bet()
            {
                playerId = "GK" + PlayerPrefs.GetString("email"),
                winpoint = int.Parse(WinText.text)//winningAmount
            };
            taken = true;
            //spinnoww = true;
            //winningAmount = 0;
            //WinText.text = winningAmount.ToString();
            //coinanimation();
            StartCoroutine(coinanimation());
            StartCoroutine(emptyroulettewinresponse());
            canPlaceBet = true;
            //Roulette_ServerRequest.intance.socket.Emit(Utility.Events.OnWinAmount, new JSONObject (JsonConvert.SerializeObject(data) ));
            cancelbtn.interactable = true;

            for (int i = 0; i < PreviousWin_list.Count; i++)
            {
                if (int.Parse(PreviousWin_list[i]) == 37)
                {
                    PreviousWin_Text[i].GetComponent<Text>().text = "00";
                }
                else
                {
                    PreviousWin_Text[i].GetComponent<Text>().text = PreviousWin_list[i].ToString();
                }

            }
            takeBtn.transform.GetChild(1).gameObject.SetActive(false);
            textcolor();
            MessagePopup.text = "Place Bet to Start the game";
            ResetAllBets();
            takeBtn.interactable = false;
            // StartCoroutine(startprevious(PreviousWin_list.ToArray()));
            //(startprevious(PreviousWin_list.ToArray()));   
            betOkBtn.gameObject.SetActive(false);
           if (timernow >10)
           {
             repeatbtn.SetActive(true);
             StartCoroutine(RepeatBlinkAnim());
           }
           else
           {
                betOkBtn.gameObject.SetActive(true);
                betOkBtn.interactable =false;
           }
            StartCoroutine(startprevious(PreviousWin_list.ToArray()));
            //StartCoroutine(ballrotator.instant.values(int.Parse(PreviousWin_list[PreviousWin_list.Count -1])));
            //Roulette_ServerRequest.intance.socket.Emit(Utility.Events.OnWinAmount, new JSONObject (JsonConvert.SerializeObject(o) ));
            // Roulette_ServerRequest.intance.SendEvent( Utility.Events.OnWinAmount, o, (res) =>
            // {
            //     UpdateUi();
            // });

        }
        float partialwin;

        public void testing1()
        {
            // int i =1;
            winningAmount = 50000;
            // if(winningAmount > 50000)
            // {
            //     partialwin = 0.1f/winningAmount;
            // }


            //Debug.Log(partialwin + "dfgd" + i);

            //partialwin = winningAmount/500f;
            StartCoroutine(coinanimation());
        }
        IEnumerator coinanimation()
        {
            float localbalance = balance;
            float localwinpoint = winningAmount;
            float elapsedTime = 0f;
            float deductionPercentage = 0.1f;
            while (elapsedTime < 4f)
            {
                float deductionValue = (winningAmount * deductionPercentage);

                // Deduct the value from the current variable
                winningAmount -= Mathf.RoundToInt(deductionValue);
                localbalance += Mathf.RoundToInt(deductionValue);
                balanceTxt.text = localbalance.ToString("F2");
                WinText.text = winningAmount.ToString();
                if (winningAmount <= 5)
                {
                    balance += localwinpoint;//+= winPoint;
                    winningAmount = 0;
                    balanceTxt.text = balance.ToString("F2"); 
                    WinText.text = winningAmount.ToString();
                    break;
                }


                yield return null;
                elapsedTime += Time.deltaTime;

            }



            // Update the elapsed time
            elapsedTime += Time.deltaTime;
            // partialwin = winningAmount;
            // while (partialwin < 0)
            // {

            //     partialwin=-1;

            // int i =1;
            // if(winningAmount < 50)
            // {
            //     i = 500;
            // }
            // else if((winningAmount >= 50)&& (winningAmount < 500))
            // {
            //     i = 5000;
            // }
            // else if((winningAmount >= 500)&& (winningAmount < 5000))
            // {
            //     i = 50000;
            // }
            // else if((winningAmount >= 5000)&& (winningAmount < 50000))
            // {
            //     i = 500000;
            // }
            // else if(winningAmount>= 50000)
            // {
            //     i = 5000000;
            // }
            // partialwin = winningAmount/(i*i);

            // while (winningAmount >0)
            // {

            //     winningAmount= winningAmount-1; //- (int)partialwin;
            //     balance = balance +1;//+ partialwin;
            //     balanceTxt.text = balance.ToString();
            //     WinText.text = winningAmount.ToString();
            //     coinsaudio.GetComponent<AudioSource>().Play();
            //     //yield return new WaitForEndOfFrame();
            //     yield return new WaitForSeconds(partialwin);
            //     //StartCoroutine(coinanimation());
            // }
        }

        public GameObject repeatbtn;
        public void OnTimerStart(string o)
        {
            isTimeUp = false;
            zoomed = false;
            TimerClass _timedata = JsonConvert.DeserializeObject<TimerClass>(o);
            exitbtn.interactable = true;
            timer_CountDown = _timedata.result;
            RouletteScreen.Instance.canPlaceBet = true;
            RouletteScreen.Instance.isBetConfirmed = false;
            Debug.Log(" Timer Count down  :" + timer_CountDown);
            betOkBtn.gameObject.SetActive(false);
            repeatbtn.SetActive(true);
            StartCoroutine(RepeatBlinkAnim());
            for (int i = 0; i < valuesforlowest.Length; i++)
            {
                valuesforlowest[i] = 0;
            }
            //exitbtn.interactable = true;
            StartCoroutine(Timer(timer_CountDown));
            Shadow.SetActive(false);
            added = false;
            called =false;
            //StartCoroutine(GetCurrentTimer());
        }

        IEnumerator GetCurrentTimer()
        {
            yield return null;//new WaitUntil(() => currentTime <= 0);
            StartCoroutine(Timer(timer_CountDown));
            //betok.SetActive(false);
        }
        [SerializeField] GameObject wheelright;
        int timernow;
        public IEnumerator Timer(int counter = 95) //60
        {
            if (counter < 10)
            {

                repeatbutton.interactable = false;
                repeatbtn.SetActive(false);
                betOkBtn.gameObject.SetActive(true);
                betOkBtn.interactable = false;
                RouletteScreen.Instance.isTimeUp = true;
                RouletteScreen.Instance.canPlaceBet = false;

            }
            else
            {
                if (counter > 10)
                {
                    RouletteScreen.Instance.isTimeUp = false;

                }
            }
            //ResetAllBets();
            //Debug.Log("winnner"+ int.Parse(WinText.text)  );
            // isTimeUp = false;
            // canPlaceBet = true;
            //   repeatBtn.interactable = betOkBtn.interactable = clearBtn.interactable = doubleBtn.interactable = true;
            //isBetConfirmed = false;
            if (winningAmount == 0)
            {
                //repeatbutton.interactable = true;
            }
            if (counter == 0)
            {
                if (Roulette_ServerResponse.instance.fromroulettecurrent == true)
                {
                    //Roulette_ServerResponse.instance.waitpanel.SetActive(true);
                    Roulette_ServerResponse.instance.fromroulettecurrent = false;
                }
            }
            else
            {
                Roulette_ServerResponse.instance.fromroulettecurrent = false;
            }

            //canPlaceBet = true;
            canStopTimer = false;
            while (counter > 0)
            {
                // if (counter == 2)
                // {
                //     Roulette_ServerResponse.instance.sendtestofonwin();
                //     exitbtn.interactable = false;
                // }

                if (canStopTimer) yield break;
                // SecToMin(counter);
                timerTxt.text = "0 : " + counter.ToString();
                timernow = counter;
                if (counter == 20)
                {
                    // if(winningAmount > 0)
                    // {
                    //     //SendTakeAmountRequest();
                    // }
                }
                currentTime = counter;
                if (counter == 10)
                {


                    canPlaceBet = false;
                    if (!isBetConfirmed)
                    {
                        if (spinnoww)
                        {
                            OnBetsOk();
                            //SendBets();
                        }

                        if (totalBets > 0)
                        {

                            countrounds = 0;
                            //OnBetsOk();              //to confirm bets...
                        }
                        else
                        {
                            countrounds = countrounds + 1;
                            Debug.Log("countrounds" + countrounds);//GK00522841 /4686
                            if (countrounds >= 3)
                            {
                                //close();
                                //throwout();
                            }
                        }
                    }
                    repeatbutton.interactable = false;
                    _tableImage.SetActive(true);
                    betOkBtn.interactable = false;
                    repeatbtn.SetActive(false);
                    betok.SetActive(true);
                    // rouletteObj.SetActive(true);
                    // _chipImage.SetActive(true);
                    // _cancelSPecificBet_transition.SetActive(true);
                    // _cancelBet_transition.SetActive(true);
                    // _wheelZoom_transition.SetActive(true);
                    //_originalPos_Objs = true;
                }
                else
                {
                    //canPlaceBet = true;
                }
                yield return new WaitForSeconds(1f);
                counter--;
                timer_AudioSource.GetComponent<AudioSource>().Play();
            }

            currentTime = 0;
            timerTxt.text = 0.ToString();
        }
        public void throwout()
        {
            PlayerPrefs.DeleteAll();
            Roulette_ServerResponse.instance.socket.Emit(Utility.Events.onleaveRoom);
            PlayerPrefs.SetInt("rthrownout", 1);
            //SceneManager.LoadScene("Login");
        }
        int[][] array = new int[14][];
        void something()
        {
            for (int i = 0; i < array.Length; i++)
            {
                // for (int j = 0; j < Length; j++)
                // {
                //     array[i][j] =  StraightUpBets[];
                // }    
            }
            //array[0][0] = StraightUpBets[0];
        }
        public IEnumerator startprevious(string[] values)
        {
            StartCoroutine(roulettewinresponse());
            bool temp;
            if(values.Length > 5)
            {
                temp = true;
            }
            else
            {
                temp = false;
            }
            Debug.Log("here are the values" + values.ToString());
            if(temp)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    PreviousWin_list.Add(values[i]);
                }
                while (PreviousWin_list.Count > 5)
                {
                    PreviousWin_list.RemoveAt(0);

                }
                for (int i = 0; i < PreviousWin_list.Count; i++)
                {
                    if (int.Parse(PreviousWin_list[i]) == 37)
                    {
                        PreviousWin_Text[i].GetComponent<Text>().text = "00";
                    }
                    else
                    {
                        PreviousWin_Text[i].GetComponent<Text>().text = PreviousWin_list[i].ToString();
                    }
                }
                textcolor();
            }
            
            int lastvalue = int.Parse(PreviousWin_list[PreviousWin_list.Count - 1].ToString());
            SpinRouletteWheelWithoutPlugin.instane.setintialangle(lastvalue);
            StartCoroutine(ballrotator.instant.initialrun(lastvalue));
            yield return null;
            //ballrotator.instant.initialrun(lastvalue);
            Debug.Log("the initial number on the wheel is " + lastvalue);
            
        }
        void previousassignment()
        {
            if (StraightUpBets.Count > 0)
            {
                //Debug.Log("value of straightcount:" +StraightUpBets.Count);
                for (int i = 0; i < StraightUpBets.Count; i++)
                {
                    //Debug.Log("value of i inside for loop:" +i +"dfvgbhnjmk,"+StraightUpBets[i]);
                    previous_straight.Add(StraightUpBets[i].ToString());//int.Parse(StraightUpBets[i]);
                }
                for (int i = 0; i < StraightUpValue.Count; i++)
                {
                    previous_straightvalues.Add(StraightUpValue[i]);//int.Parse(StraightUpBets[i]);
                }
            }
            if (SplitBets.Count > 0)
            {
                for (int i = 0; i < SplitBets.Count; i++)
                {
                    previous_split.Add(SplitBets[i]);//int.Parse(StraightUpBets[i]);
                }
                for (int i = 0; i < SplitValue.Count; i++)
                {
                    previous_splitvalue.Add(SplitValue[i]);//int.Parse(StraightUpBets[i]);
                }
            }
            if (CornerBets.Count > 0)
            {
                for (int i = 0; i < CornerBets.Count; i++)
                {
                    previous_corner.Add(CornerBets[i]);//int.Parse(StraightUpBets[i]);
                }
                for (int i = 0; i < CornerValue.Count; i++)
                {
                    previous_cornervalue.Add(CornerValue[i]);//int.Parse(StraightUpBets[i]);
                }
            }
            if (LineBets.Count > 0)
            {
                for (int i = 0; i < LineBets.Count; i++)
                {
                    previous_line.Add(LineBets[i]);//int.Parse(StraightUpBets[i]);
                }
                for (int i = 0; i < LineValue.Count; i++)
                {
                    previous_linevalues.Add(LineValue[i]);//int.Parse(StraightUpBets[i]);
                }
            }
            if (StreetBets.Count > 0)
            {
                for (int i = 0; i < StreetBets.Count; i++)
                {
                    previous_street.Add(StreetBets[i]);//int.Parse(StraightUpBets[i]);
                }
                for (int i = 0; i < StreetValue.Count; i++)
                {
                    previous_streetvalue.Add(StreetValue[i]);//int.Parse(StraightUpBets[i]);
                }
            }
            previous_columvalue01 = ColumnValue01;
            previous_columnvalue02 = ColumnValue02;
            previous_columnvalue03 = ColumnValue03;
            previous_dozenvalue01 = dozenValue01;//dozen01Value[0];
            previous_dozenvalue02 = dozenValue02;
            previous_dozenvalue03 = dozenValue03;
            previous_onetoEighteenValue = onetoEighteenValue;
            previous_nineteentoThirtysixValue = nineteentoThirtysixValue;
            previous_blackValue = blackValue;
            previous_redValue = redValue;
            previous_evenValue = evenValue;
            previous_oddValue = oddValue;




        }
        public void clearstored()
        {
            Roulettelast.Straightbetlast.Clear();
            Roulettelast.StraightValuelast.Clear();
            Roulettelast.Splittbetlast.Clear();
            Roulettelast.SplitValuelast.Clear();
            Roulettelast.Streetbetlast.Clear();
            Roulettelast.StreetValuelast.Clear();
            Roulettelast.CornerBetlast.Clear();
            Roulettelast.CornerValuelast.Clear();
            Roulettelast.Linebetlast.Clear();
            Roulettelast.LineValuelast.Clear();
            Roulettelast.column01 = Roulettelast.column02 = Roulettelast.column03 = Roulettelast.dozen01betlast = Roulettelast.dozen02betlast = Roulettelast.dozen03betlast = Roulettelast.onetoeighteen = Roulettelast.nineteentothirtysix = Roulettelast.red = Roulettelast.black = Roulettelast.even = Roulettelast.odd = Roulettelast.totalbets = 0;


        }
        public void store()
        {
            clearstored();
            // Debug.Log("striaght "+StraightUpBets.Count+" inbinary "+Roulettelast.Straightbetlast.Count);
            // Debug.Log("Split "+SplitBets.Count+" inbinary "+Roulettelast.Splittbetlast.Count);
            // Debug.Log("Corner "+CornerBets.Count+" inbinary "+Roulettelast.CornerBetlast.Count);
            // Debug.Log("Street "+StreetBets.Count+" inbinary "+Roulettelast.Streetbetlast.Count);
            // Debug.Log("Line "+LineBets.Count+" inbinary "+Roulettelast.Straightbetlast.Count);
            //Roulettelast.winnervalue = int.Parse(WinText.text);
            Roulettelast.addedinfile = PlayerPrefs.GetInt("addedBets");
            Roulettelast.deviceid = PlayerPrefs.GetString("email");
            Roulettelast.round =Roulette_ServerResponse.instance.rouletterounds;

            for (int i = 0; i < StraightUpBets.Count; i++)
            {
                Roulettelast.Straightbetlast.Add(StraightUpBets[i]);
                Roulettelast.StraightValuelast.Add(StraightUpValue[i]);

            }
            for (int i = 0; i < SplitBets.Count; i++)
            {
                Roulettelast.Splittbetlast.Add(SplitBets[i]);
                Roulettelast.SplitValuelast.Add(SplitValue[i]);

            }
            for (int i = 0; i < StreetBets.Count; i++)
            {
                Roulettelast.Streetbetlast.Add(StreetBets[i]);
                Roulettelast.StreetValuelast.Add(StreetValue[i]);

            }
            for (int i = 0; i < CornerBets.Count; i++)
            {
                Roulettelast.CornerBetlast.Add(CornerBets[i]);
                Roulettelast.CornerValuelast.Add(CornerValue[i]);

            }
            for (int i = 0; i < LineBets.Count; i++)
            {
                Roulettelast.Linebetlast.Add(LineBets[i]);
                Roulettelast.LineValuelast.Add(LineValue[i]);
            }
            Roulettelast.column01 = ColumnValue01;
            Roulettelast.column02 = ColumnValue02;
            Roulettelast.column03 = ColumnValue03;
            Roulettelast.dozen01betlast = dozenValue01;
            Roulettelast.dozen02betlast = dozenValue02;
            Roulettelast.dozen03betlast = dozenValue03;
            Roulettelast.onetoeighteen = onetoEighteenValue;
            Roulettelast.nineteentothirtysix = nineteentoThirtysixValue;
            Roulettelast.red = redValue;
            Roulettelast.black = blackValue;
            Roulettelast.even = evenValue;
            Roulettelast.odd = oddValue;
            Roulettelast.totalbets = totalBets;
            //Debug.Log("OMG it works");
            File.Delete(SaveFilePath);
            savebinary.savefunctionroulette();

            //}
            //lastrecord.Roulettelas = true;
            //Debug.Log("data is stored");
            //savebinary.savefunction();

        }
        void clearstoredwithin()
        {
            Roulettewithin.Straightbetlast.Clear();
            Roulettewithin.StraightValuelast.Clear();
            Roulettewithin.Splittbetlast.Clear();
            Roulettewithin.SplitValuelast.Clear();
            Roulettewithin.Streetbetlast.Clear();
            Roulettewithin.StreetValuelast.Clear();
            Roulettewithin.CornerBetlast.Clear();
            Roulettewithin.CornerValuelast.Clear();
            Roulettewithin.Linebetlast.Clear();
            Roulettewithin.LineValuelast.Clear();
            Roulettewithin.column01 = 0;
            Roulettewithin.column02 = 0;
            Roulettewithin.column03 = 0;
            Roulettewithin.dozen01betlast = 0;
            Roulettewithin.dozen02betlast = 0;
            Roulettewithin.dozen03betlast = 0;
            Roulettewithin.onetoeighteen = 0;
            Roulettewithin.nineteentothirtysix = 0;
            Roulettewithin.red = 0;
            Roulettewithin.black = 0;
            Roulettewithin.even = 0;
            Roulettewithin.odd = 0;
            Roulettewithin.totalBets = 0;
        }
        void storewithin()
        {
            clearstoredwithin();
            Debug.Log("DDDDDDDDDDDDDDDDDDDDdata is stored");
            //Roulettewithin.winnervalue = int.Parse(WinText.text);
            Roulettewithin.betconfirmed = isBetConfirmed;
            Roulettewithin.Roundcount = Roulette_ServerResponse.instance.rouletterounds;
            for (int i = 0; i < StraightUpBets.Count; i++)
            {
                Roulettewithin.Straightbetlast.Add(StraightUpBets[i]);
                Roulettewithin.StraightValuelast.Add(StraightUpValue[i]);

            }
            for (int i = 0; i < SplitBets.Count; i++)
            {
                Roulettewithin.Splittbetlast.Add(SplitBets[i]);
                Roulettewithin.SplitValuelast.Add(SplitValue[i]);

            }
            for (int i = 0; i < StreetBets.Count; i++)
            {
                Roulettewithin.Streetbetlast.Add(StreetBets[i]);
                Roulettewithin.StreetValuelast.Add(StreetValue[i]);

            }
            for (int i = 0; i < CornerBets.Count; i++)
            {
                Roulettewithin.CornerBetlast.Add(CornerBets[i]);
                Roulettewithin.CornerValuelast.Add(CornerValue[i]);

            }
            for (int i = 0; i < LineBets.Count; i++)
            {
                Roulettewithin.Linebetlast.Add(LineBets[i]);
                Roulettewithin.LineValuelast.Add(LineValue[i]);
            }
            Roulettewithin.column01 = ColumnValue01;
            Roulettewithin.column02 = ColumnValue02;
            Roulettewithin.column03 = ColumnValue03;
            Roulettewithin.dozen01betlast = dozenValue01;
            Roulettewithin.dozen02betlast = dozenValue02;
            Roulettewithin.dozen03betlast = dozenValue03;
            Roulettewithin.onetoeighteen = onetoEighteenValue;
            Roulettewithin.nineteentothirtysix = nineteentoThirtysixValue;
            Roulettewithin.red = redValue;
            Roulettewithin.black = blackValue;
            Roulettewithin.even = evenValue;
            Roulettewithin.odd = oddValue;
            Roulettewithin.totalBets = totalBets;
        }
        void clearpreviouslocal()
        {
            previous_straight.Clear();
            previous_straightvalues.Clear();
            previous_split.Clear();
            previous_splitvalue.Clear();
            previous_street.Clear();
            previous_streetvalue.Clear();
            previous_corner.Clear();
            previous_cornervalue.Clear();
            previous_line.Clear();
            previous_linevalues.Clear();
            previous_columvalue01 = 0;
            previous_columnvalue02 = 0;
            previous_columnvalue03 = 0;
            previous_dozenvalue01 = 0;
            previous_dozenvalue02 = 0;
            previous_dozenvalue03 = 0;
            previous_onetoEighteenValue = 0;
            previous_nineteentoThirtysixValue = 0;
            previous_redValue = 0;
            previous_blackValue = 0;
            previous_evenValue = 0;
            previous_oddValue = 0;


        }
        void restore()
        {
            if(isBetConfirmed)
            {
                MessagePopup.text ="Bets have been accepted";
                return;
            }
            clearpreviouslocal();
            ResetAllBets();
            StartCoroutine(previouscall());
            betOkBtn.interactable=true;
            //savebinary.LoadPlayerroulette();
            //Debug.Log("the data from binary has been restored");  
            //WinText.text =Roulettelast.winnervalue.ToString();
            //winningAmount = Roulettelast.winnervalue;
            //winningAmount = int.Parse(WinText.text);
            //WinText.text = Roulettelast.winnervalue.ToString();
            // Debug.Log("striaght" + Roulettelast.Straightbetlast.Count);
            // Debug.Log("Split" + Roulettelast.Splittbetlast.Count);
            // Debug.Log("Corner" + Roulettelast.CornerBetlast.Count);
            // Debug.Log("Street" + Roulettelast.Streetbetlast.Count);
            // Debug.Log("Line" + Roulettelast.Linebetlast.Count);
            // for (int i = 0; i < Roulettelast.Straightbetlast.Count; i++)
            // {
            //     previous_straight.Add(Roulettelast.Straightbetlast[i].ToString());
            //     previous_straightvalues.Add(Roulettelast.StraightValuelast[i]);

            // }
            // for (int i = 0; i < Roulettelast.Splittbetlast.Count; i++)
            // {
            //     previous_split.Add(Roulettelast.Splittbetlast[i]);
            //     previous_splitvalue.Add(Roulettelast.SplitValuelast[i]);

            // }
            // for (int i = 0; i < Roulettelast.Streetbetlast.Count; i++)
            // {
            //     previous_street.Add(Roulettelast.Streetbetlast[i]);
            //     previous_streetvalue.Add(Roulettelast.StreetValuelast[i]);

            // }
            // for (int i = 0; i < Roulettelast.CornerBetlast.Count; i++)
            // {
            //     previous_corner.Add(Roulettelast.CornerBetlast[i]);
            //     previous_cornervalue.Add(Roulettelast.CornerValuelast[i]);

            // }
            // for (int i = 0; i < Roulettelast.Linebetlast.Count; i++)
            // {
            //     previous_line.Add(Roulettelast.Linebetlast[i]);
            //     previous_linevalues.Add(Roulettelast.LineValuelast[i]);
            // }

            // previous_columvalue01 = Roulettelast.column01;
            // previous_columnvalue02 = Roulettelast.column02;
            // previous_columnvalue03 = Roulettelast.column03;
            // previous_dozenvalue01 = Roulettelast.dozen01betlast;
            // previous_dozenvalue02 = Roulettelast.dozen02betlast;
            // previous_dozenvalue03 = Roulettelast.dozen03betlast;
            // previous_onetoEighteenValue = Roulettelast.onetoeighteen;
            // previous_nineteentoThirtysixValue = Roulettelast.nineteentothirtysix;
            // previous_redValue = Roulettelast.red;
            // previous_blackValue = Roulettelast.black;
            // previous_evenValue = Roulettelast.even;
            // previous_oddValue = Roulettelast.odd;
            // totalBets = Roulettelast.totalbets;
            // //totalBetsTxt.text = totalBets.ToString();
            // if (!repeated)
            // {
            //     showui();
            // }

            //lastrecord.Roulettelas = false;


        }
        void clearprevious()
        {
            previous_straight.Clear();
            previous_straightvalues.Clear();
            previous_split.Clear();
            previous_splitvalue.Clear();
            previous_corner.Clear();
            previous_cornervalue.Clear();
            previous_line.Clear();
            previous_linevalues.Clear();
            previous_columvalue01 = 0;
            previous_columnvalue02 = 0;
            previous_columnvalue03 = 0;
            previous_dozenvalue01 = 0;
            previous_dozenvalue02 = 0;
            previous_dozenvalue03 = 0;
            previous_onetoEighteenValue = 0;
            previous_nineteentoThirtysixValue = 0;
            previous_redValue = 0;
            previous_blackValue = 0;
            previous_evenValue = 0;
            previous_oddValue = 0;

        }
        void restorewithin()
        {
            Debug.Log("the data from withinclass has been restored");
            clearprevious();
            //winningAmount = Roulettewithin.winnervalue;
            if (Roulettewithin.StraightValuelast.Sum() + Roulettewithin.SplitValuelast.Sum() + Roulettewithin.StreetValuelast.Sum() + Roulettewithin.CornerValuelast.Sum() + Roulettewithin.LineValuelast.Sum() + Roulettewithin.column01 + Roulettewithin.column02 + Roulettewithin.column03 + Roulettewithin.dozen01betlast + Roulettewithin.dozen02betlast + Roulettewithin.dozen03betlast + Roulettewithin.onetoeighteen + Roulettewithin.nineteentothirtysix + Roulettewithin.red + Roulettewithin.black + Roulettewithin.even + Roulettewithin.odd > balance)
            {
                return;
            }

            //WinText.text =winningAmount.ToString();//Roulettewithin.winnervalue.ToString();
            //winningAmount = int.Parse(WinText.text);
            //WinText.text = Roulettelast.winnervalue.ToString();
            for (int i = 0; i < Roulettewithin.Straightbetlast.Count; i++)
            {
                previous_straight.Add(Roulettewithin.Straightbetlast[i].ToString());
                previous_straightvalues.Add(Roulettewithin.StraightValuelast[i]);

            }
            for (int i = 0; i < Roulettewithin.Splittbetlast.Count; i++)
            {
                previous_split.Add(Roulettewithin.Splittbetlast[i]);
                previous_splitvalue.Add(Roulettewithin.SplitValuelast[i]);

            }
            for (int i = 0; i < Roulettewithin.Streetbetlast.Count; i++)
            {
                previous_street.Add(Roulettewithin.Streetbetlast[i]);
                previous_streetvalue.Add(Roulettewithin.StreetValuelast[i]);

            }
            for (int i = 0; i < Roulettewithin.CornerBetlast.Count; i++)
            {
                previous_corner.Add(Roulettewithin.CornerBetlast[i]);
                previous_cornervalue.Add(Roulettewithin.CornerValuelast[i]);

            }
            for (int i = 0; i < Roulettewithin.Linebetlast.Count; i++)
            {
                previous_line.Add(Roulettewithin.Linebetlast[i]);
                previous_linevalues.Add(Roulettewithin.LineValuelast[i]);
            }

            previous_columvalue01 = Roulettewithin.column01;
            previous_columnvalue02 = Roulettewithin.column02;
            previous_columnvalue03 = Roulettewithin.column03;
            previous_dozenvalue01 = Roulettewithin.dozen01betlast;
            previous_dozenvalue02 = Roulettewithin.dozen02betlast;
            previous_dozenvalue03 = Roulettewithin.dozen03betlast;
            previous_onetoEighteenValue = Roulettewithin.onetoeighteen;
            previous_nineteentoThirtysixValue = Roulettewithin.nineteentothirtysix;
            previous_redValue = Roulettewithin.red;
            previous_blackValue = Roulettewithin.black;
            previous_evenValue = Roulettewithin.even;
            previous_oddValue = Roulettewithin.odd;
            showui();
        }
        //Button temp;
        int[] temp1;
        int[] temp2;
        int[] temp3;
        int[] temp4;
        int[] temp5;
        int[] temp6;
        public IEnumerator previouscall()
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
                    Debug.Log("previous"+www.downloadHandler.text);
                    RoulettePrevious response = JsonConvert.DeserializeObject<RoulettePrevious>(www.downloadHandler.text);
                    
                    if(response.status == 200)
                    {
                        clearpreviouslocal();
                        for (int i = 0; i <response.data.straightUp.Length; i++)
                        {
                            previous_straight.Add(response.data.straightUp[i].ToString());
                            previous_straightvalues.Add(response.data.straightUpVal[i]);

                        }
                        if (response.data.Split.Length > 0)
                        {
                            Debug.Log("split length"+response.data.Split.Length);
                            for (int i = 0; i < response.data.Split.Length/2; i++)
                            {
                                string value1;
                                string value2;
                                string last;
                                value1 = response.data.Split[i,0].ToString();
                                value2 = response.data.Split[i,1].ToString();
                                last = value1+"_"+value2;
                                Split_new.Add(last);
                                previous_split.Add(last);
                                Debug.Log("last"+last + "the value of i"+i);
                                // for (int j = 0; j < 2; j++)
                                // {
                                //     Debug.Log("split bets"+response.data.Split[i,j]);
                                    
                                // }
                               
                                //Split_server.Add(Roulettelast.Splittbetlast[i]);
                                //string[] formattedArray = response.data.Split.Select(row => string.Join("_", row)).ToArray();
    
    
    
                                // for (int j = 0; j < response.data.Split.Length; j++)
                                // {
                                //     string value1;
                                //     string value2;
                                //     string last;
                                //     value1 = response.data.Split[j,0].ToString();
                                //     value2 = response.data.Split[j,1].ToString();
                                //     last = value1+"_"+value2;
                                //     Split_new.Add(last);
                                //     previous_split.Add(last);
    
                                    
                                // }
                                previous_splitvalue.Add(response.data.SplitVal[i]);
    
                            }
                        }
                        if (response.data.Street.Length >0)
                        {
                            for (int i = 0; i < response.data.Street.Length/3; i++)
                            {
                                string value1;
                                string value2;
                                string value3;
                                string last;
                                value1 = response.data.Street[i,0].ToString();
                                value2 = response.data.Street[i,1].ToString();
                                value3 = response.data.Street[i,2].ToString();
                                last = value1+"_"+value2+"_"+value3;
                                Street_new.Add(last);
                                previous_street.Add(last);
                                //Split_server.Add(Roulettelast.Splittbetlast[i]);
                                // for (int j = 0; j < response.data.Street.Length; j++)
                                // {
                                //     string value1;
                                //     string value2;
                                //     string value3;
                                //     string last;
                                //     value1 = response.data.Street[i,0].ToString();
                                //     value2 = response.data.Street[i,1].ToString();
                                //     value3 = response.data.Street[i,2].ToString();
                                //     last = value1+"_"+value2+"_"+value3;
                                //     Street_new.Add(last);
                                //     previous_street.Add(last);
                                    
                                // }
                                previous_streetvalue.Add(response.data.StreetVal[i]);
    
                            }
                        }
                        if (response.data.Corner.Length >0)
                        {
                            for (int i = 0; i < response.data.Corner.Length/4; i++)
                            {
                                string value1;
                                string value2;
                                string value3;
                                string value4;
                                string last;
                                value1 = response.data.Corner[i,0].ToString();
                                value2 = response.data.Corner[i,1].ToString();
                                value3 = response.data.Corner[i,2].ToString();
                                value4 = response.data.Corner[i,3].ToString();
                                last = value1+"_"+value2+"_"+value3+"_"+value4;
                                Corner_new.Add(last);
                                previous_corner.Add(last);
                                //Split_server.Add(Roulettelast.Splittbetlast[i]);
                                // for (int j = 0; j < response.data.Corner.Length; j++)
                                // {
                                //     string value1;
                                //     string value2;
                                //     string value3;
                                //     string value4;
                                //     string last;
                                //     value1 = response.data.Corner[i,0].ToString();
                                //     value2 = response.data.Corner[i,1].ToString();
                                //     value3 = response.data.Corner[i,2].ToString();
                                //     value4 = response.data.Corner[i,3].ToString();
                                //     last = value1+"_"+value2+"_"+value3+"_"+value4;
                                //     Corner_new.Add(last);
                                //     previous_corner.Add(last);
                                // }
                                previous_cornervalue.Add(response.data.CornerVal[i]);
    
                            }
                        }
                        if (response.data.line.Length >0)
                        {
                            for (int i = 0; i < response.data.line.Length/6; i++)
                            {
                                string value1;
                                string value2;
                                string value3;
                                string value4;
                                string value5;
                                string value6;
                                string last;
                                value1 = response.data.line[i,0].ToString();
                                value2 = response.data.line[i,1].ToString();
                                value3 = response.data.line[i,2].ToString();
                                value4 = response.data.line[i,3].ToString();
                                value5 = response.data.line[i,4].ToString();
                                value6 = response.data.line[i,5].ToString();
                                last = value1+"_"+value2+"_"+value3+"_"+value4+"_"+value5+"_"+value6;
                                Line_new.Add(last);
                                previous_line.Add(last);
                                //Split_server.Add(Roulettelast.Splittbetlast[i]);
                                // for (int j = 0; j < response.data.line.Length; j++)
                                // {
                                //     string value1;
                                //     string value2;
                                //     string value3;
                                //     string value4;
                                //     string value5;
                                //     string value6;
                                //     string last;
                                //     value1 = response.data.line[i,0].ToString();
                                //     value2 = response.data.line[i,1].ToString();
                                //     value3 = response.data.line[i,2].ToString();
                                //     value4 = response.data.line[i,3].ToString();
                                //     value5 = response.data.line[i,4].ToString();
                                //     value6 = response.data.line[i,5].ToString();
                                //     last = value1+"_"+value2+"_"+value3+"_"+value4+"_"+value5+"_"+value6;
                                //     Line_new.Add(last);
                                //     previous_line.Add(last);
                                // }
                                previous_linevalues.Add(response.data.lineVal[i]);
    
                            }
                        }
                        previous_columvalue01 = response.data.column1Val;
                        previous_columnvalue02 = response.data.column2Val;
                        previous_columnvalue03 = response.data.column3Val;
                        previous_dozenvalue01 = response.data.dozen1Val;
                        previous_dozenvalue02 = response.data.dozen2Val;
                        previous_dozenvalue03 = response.data.dozen3Val;
                        previous_onetoEighteenValue = response.data.onetoEighteenVal;
                        previous_nineteentoThirtysixValue = response.data.nineteentoThirtysixVal;
                        previous_redValue = response.data.redVal;
                        previous_blackValue = response.data.blackVal;
                        previous_evenValue = response.data.evenVal;
                        previous_oddValue = response.data.oddVal;
                        letsdocalculation();
                        showui();
                        // if (!repeated)
                        // {
                            
                        // }
                        
                    }
                }
            }

        }

        bool repeated;
        public int[,] Split_server;
        //public List<int,int> Split_server;
        public int[,] Street_server, Corner_server, line_server;
        public List<string> Split_new,Street_new,Corner_new,Line_new;
        

        public void readfromserver()
        {
            for (int i = 0; i < Split_server.Length; i++)
            {
                string value1;
                string value2;
                string last;
                value1 = Split_server[i,0].ToString();
                value2 = Split_server[i,1].ToString();
                last = value1+"_"+value2;
                Split_new.Add(last);

                
            }
            for (int i = 0; i < Street_server.Length; i++)
            {
                string value1;
                string value2;
                string value3;
                string last;
                value1 = Street_server[i,0].ToString();
                value2 = Street_server[i,1].ToString();
                value3 = Street_server[i,2].ToString();
                last = value1+"_"+value2+"_"+value3;
                Street_new.Add(last);
            }
            for (int i = 0; i < Corner_server.Length; i++)
            {
                string value1;
                string value2;
                string value3;
                string value4;
                string last;
                value1 = Corner_server[i,0].ToString();
                value2 = Corner_server[i,1].ToString();
                value3 = Corner_server[i,2].ToString();
                value4 = Corner_server[i,3].ToString();
                last = value1+"_"+value2+"_"+value3+"_"+value4;
                Corner_new.Add(last);
            }
            for (int i = 0; i < line_server.Length; i++)
            {
                string value1;
                string value2;
                string value3;
                string value4;
                string value5;
                string value6;
                string last;
                value1 = line_server[i,0].ToString();
                value2 = line_server[i,1].ToString();
                value3 = line_server[i,2].ToString();
                value4 = line_server[i,3].ToString();
                value5 = line_server[i,4].ToString();
                value6 = line_server[i,5].ToString();
                last = value1+"_"+value2+"_"+value3+"_"+value4+"_"+value5+"_"+value6;
                Line_new.Add(last);
            }
        }
        public void repeat()
        {
            if(!spinnoww)
            {
                MessagePopup.text = "Please Collect Winning amount First";
                return;
            }
            repeated = true;
            repeatbtn.SetActive(false);
            StopCoroutine(RepeatBlinkAnim());
            betOkBtn.gameObject.SetActive(true);
            
            restore();
            
            //OnBetsOk();
            repeatbutton.interactable = false;
            PlayerPrefs.SetInt("addedBets", 1);
        }
        void letsdocalculation()
        {
            if (previous_straightvalues.Sum() + previous_splitvalue.Sum() + previous_cornervalue.Sum() + previous_streetvalue.Sum() + previous_linevalues.Sum() + previous_dozenvalue01 + previous_dozenvalue02 + previous_dozenvalue03 + previous_columvalue01 + previous_columnvalue02 + previous_columnvalue03 + previous_onetoEighteenValue + previous_nineteentoThirtysixValue + previous_blackValue + previous_redValue + previous_evenValue + previous_oddValue < balance)
            {
                if (previous_straight.Count > 0)
                {
                    for (int i = 0; i < previous_straight.Count; i++)
                    {
                        //StraightUpBets.Add(int.Parse(previous_straight[i]));//int.Parse(StraightUpBets[i]);
                        Button temp = GameObject.Find(previous_straight[i]).GetComponent<Button>();

                        StraightUpBets.Add(int.Parse(previous_straight[i]));
                        temp.transform.GetChild(1).GetComponent<Text>().enabled = true;
                        temp.transform.GetChild(1).GetComponent<Text>().text = previous_straightvalues[i].ToString();//(int.Parse(temp.transform.GetChild(1).GetComponent<Text>().text)).ToString();
                        temp.transform.GetChild(0).GetComponent<Image>().enabled = true;
                        // for(int j =0;j<38;j++)
                        // {
                        //     if(int.Parse(previous_straight[i]) == j)
                        //     {
                        //         valuesforlowest[j] += previous_straightvalues[i]; 
                        //     }
                        // }
                        //StraightUpBets[i] = int.Parse(previous_straight[i]); 
                        //AddBets(temp);
                    }
                    for (int i = 0; i < previous_straightvalues.Count; i++)
                    {
                        StraightUpValue.Add(previous_straightvalues[i]);//int.Parse(StraightUpBets[i]);
                    }
                }
                if (previous_split.Count > 0)
                {
                    for (int i = 0; i < previous_split.Count; i++)
                    {
                        //Debug.Log(i+"This is the value of i");
                        //SplitBets.Add(previous_split[i]);//int.Parse(StraightUpBets[i]);
                        Button temp = GameObject.Find(previous_split[i]).GetComponent<Button>();
                        //Button temp = GameObject.Find(previous_straight[i]).GetComponent<Button>();

                        SplitBets.Add(previous_split[i]);
                        temp.transform.GetChild(1).GetComponent<Text>().enabled = true;
                        temp.transform.GetChild(1).GetComponent<Text>().text = previous_splitvalue[i].ToString();//(int.Parse(temp.transform.GetChild(1).GetComponent<Text>().text)).ToString();
                        temp.transform.GetChild(0).GetComponent<Image>().enabled = true;

                        Array.Clear(SplitArray, 0, SplitArray.Length);
                        SplitArray = new int[SplitBets.Count, 2];
                        //SplitArray = new int[SplitBets.Count][];
                        for (int j = 0; j < SplitBets.Count; j++)
                        {
                            string[] splitedArray = SplitBets[j].Split(Char.Parse("_"));
                            // SplitArray[i][0] = int.Parse(splitedArray[0]);
                            // SplitArray[i][1] = int.Parse(splitedArray[1]);
                            SplitArray[j, 0] = int.Parse(splitedArray[0]);
                            SplitArray[j, 1] = int.Parse(splitedArray[1]);
                        }

                        //GameObject.transform.GetChild(0).GetComponent<Image>().enabled = true;
                    }
                    for (int i = 0; i < previous_splitvalue.Count; i++)
                    {
                        SplitValue.Add(previous_splitvalue[i]);//int.Parse(StraightUpBets[i]);
                    }
                }
                if (previous_corner.Count > 0)
                {
                    for (int i = 0; i < previous_corner.Count; i++)
                    {
                        //CornerBets.Add(previous_corner[i]);//int.Parse(StraightUpBets[i]);
                        Button temp = GameObject.Find(previous_corner[i]).GetComponent<Button>();
                        CornerBets.Add(previous_corner[i]);
                        temp.transform.GetChild(1).GetComponent<Text>().enabled = true;
                        temp.transform.GetChild(1).GetComponent<Text>().text = previous_cornervalue[i].ToString();//(int.Parse(temp.transform.GetChild(1).GetComponent<Text>().text)).ToString();
                        temp.transform.GetChild(0).GetComponent<Image>().enabled = true;
                        Array.Clear(CornerArray, 0, CornerArray.Length);
                        CornerArray = new int[CornerBets.Count, 4];
                        for (int j = 0; j < CornerBets.Count; j++)
                        {
                            string[] corneredArray = CornerBets[j].Split(Char.Parse("_"));
                            CornerArray[j, 0] = int.Parse(corneredArray[0]);
                            CornerArray[j, 1] = int.Parse(corneredArray[1]);
                            CornerArray[j, 2] = int.Parse(corneredArray[2]);
                            CornerArray[j, 3] = int.Parse(corneredArray[3]);
                        }
                    }
                    for (int i = 0; i < previous_cornervalue.Count; i++)
                    {
                        CornerValue.Add(previous_cornervalue[i]);//int.Parse(StraightUpBets[i]);
                    }
                }
                if (previous_line.Count > 0)
                {
                    for (int i = 0; i < previous_line.Count; i++)
                    {
                        //LineBets.Add(previous_line[i]);//int.Parse(StraightUpBets[i]);
                        Button temp = GameObject.Find(previous_line[i]).GetComponent<Button>();
                        //Button temp = GameObject.Find(previous_corner[i]).GetComponent<Button>();
                        LineBets.Add(previous_line[i]);
                        temp.transform.GetChild(1).GetComponent<Text>().enabled = true;
                        temp.transform.GetChild(1).GetComponent<Text>().text = previous_linevalues[i].ToString();//(int.Parse(temp.transform.GetChild(1).GetComponent<Text>().text)).ToString();
                        temp.transform.GetChild(0).GetComponent<Image>().enabled = true;
                        Array.Clear(LineArray, 0, LineArray.Length);
                        LineArray = new int[LineBets.Count, 6];
                        for (int j = 0; j < LineBets.Count; j++)
                        {
                            string[] linedArray = LineBets[j].Split(Char.Parse("_"));
                            LineArray[j, 0] = int.Parse(linedArray[0]);
                            LineArray[j, 1] = int.Parse(linedArray[1]);
                            LineArray[j, 2] = int.Parse(linedArray[2]);
                            LineArray[j, 3] = int.Parse(linedArray[3]);
                            LineArray[j, 4] = int.Parse(linedArray[4]);
                            LineArray[j, 5] = int.Parse(linedArray[5]);
                        }
                    }
                    for (int i = 0; i < previous_linevalues.Count; i++)
                    {
                        LineValue.Add(previous_linevalues[i]);//int.Parse(StraightUpBets[i]);
                    }
                }
                if (previous_street.Count > 0)
                {
                    for (int i = 0; i < previous_street.Count; i++)
                    {
                        //StreetBets.Add(previous_street[i]);//int.Parse(StraightUpBets[i]);
                        Button temp = GameObject.Find(previous_street[i]).GetComponent<Button>();
                        StreetBets.Add(previous_street[i]);
                        temp.transform.GetChild(1).GetComponent<Text>().enabled = true;
                        temp.transform.GetChild(1).GetComponent<Text>().text = previous_streetvalue[i].ToString();//(int.Parse(temp.transform.GetChild(1).GetComponent<Text>().text)).ToString();
                        temp.transform.GetChild(0).GetComponent<Image>().enabled = true;
                        Array.Clear(StreetArray, 0, StreetArray.Length);
                        StreetArray = new int[StreetBets.Count, 3];
                        for (int j = 0; j < StreetBets.Count; j++)
                        {
                            string[] streetedArray = StreetBets[j].Split(Char.Parse("_"));
                            StreetArray[j, 0] = int.Parse(streetedArray[0]);
                            StreetArray[j, 1] = int.Parse(streetedArray[1]);
                            StreetArray[j, 2] = int.Parse(streetedArray[2]);
                        }
                    }

                    for (int i = 0; i < previous_streetvalue.Count; i++)
                    {
                        StreetValue.Add(previous_streetvalue[i]);//int.Parse(StraightUpBets[i]);
                    }
                }

                if (previous_columvalue01 > 0)
                {
                    ColumnValue01 = previous_columvalue01;
                    Button temp = GameObject.Find("2:1(Col01)").GetComponent<Button>();
                    //StreetBets.Add(previous_straight[i]);
                    temp.transform.GetChild(2).GetComponent<Text>().enabled = true;
                    temp.transform.GetChild(2).GetComponent<Text>().text = ColumnValue01.ToString();//(int.Parse(temp.transform.GetChild(1).GetComponent<Text>().text)).ToString();
                    temp.transform.GetChild(1).GetComponent<Image>().enabled = true;
                }
                if (previous_columnvalue02 > 0)
                {
                    ColumnValue02 = previous_columnvalue02;
                    Button temp = GameObject.Find("2:1(Col02)").GetComponent<Button>();
                    temp.transform.GetChild(2).GetComponent<Text>().enabled = true;
                    temp.transform.GetChild(2).GetComponent<Text>().text = ColumnValue02.ToString();//(int.Parse(temp.transform.GetChild(1).GetComponent<Text>().text)).ToString();
                    temp.transform.GetChild(1).GetComponent<Image>().enabled = true;
                }
                if (previous_columnvalue03 > 0)
                {
                    ColumnValue03 = previous_columnvalue03;
                    Button temp = GameObject.Find("2:1(Col03)").GetComponent<Button>();
                    temp.transform.GetChild(2).GetComponent<Text>().enabled = true;
                    temp.transform.GetChild(2).GetComponent<Text>().text = ColumnValue03.ToString();//(int.Parse(temp.transform.GetChild(1).GetComponent<Text>().text)).ToString();
                    temp.transform.GetChild(1).GetComponent<Image>().enabled = true;
                }
                if (previous_dozenvalue01 > 0)
                {
                    dozenValue01 = previous_dozenvalue01;
                    Button temp = GameObject.Find("1st12").GetComponent<Button>();
                    temp.transform.GetChild(2).GetComponent<Text>().enabled = true;
                    temp.transform.GetChild(2).GetComponent<Text>().text = dozenValue01.ToString();//(int.Parse(temp.transform.GetChild(1).GetComponent<Text>().text)).ToString();
                    temp.transform.GetChild(1).GetComponent<Image>().enabled = true;
                }
                if (previous_dozenvalue02 > 0)
                {
                    dozenValue02 = previous_dozenvalue02;
                    Button temp = GameObject.Find("2nd12").GetComponent<Button>();
                    temp.transform.GetChild(2).GetComponent<Text>().enabled = true;
                    temp.transform.GetChild(2).GetComponent<Text>().text = dozenValue02.ToString();//(int.Parse(temp.transform.GetChild(1).GetComponent<Text>().text)).ToString();
                    temp.transform.GetChild(1).GetComponent<Image>().enabled = true;
                }
                if (previous_dozenvalue03 > 0)
                {
                    dozenValue03 = previous_dozenvalue03;
                    Button temp = GameObject.Find("3rd 12").GetComponent<Button>();
                    temp.transform.GetChild(2).GetComponent<Text>().enabled = true;
                    temp.transform.GetChild(2).GetComponent<Text>().text = dozenValue03.ToString();//(int.Parse(temp.transform.GetChild(1).GetComponent<Text>().text)).ToString();
                    temp.transform.GetChild(1).GetComponent<Image>().enabled = true;
                }
                if (previous_onetoEighteenValue > 0)
                {
                    onetoEighteenValue = previous_onetoEighteenValue;
                    Button temp = GameObject.Find("1st18").GetComponent<Button>();
                    temp.transform.GetChild(2).GetComponent<Text>().enabled = true;
                    temp.transform.GetChild(2).GetComponent<Text>().text = onetoEighteenValue.ToString();//(int.Parse(temp.transform.GetChild(1).GetComponent<Text>().text)).ToString();
                    temp.transform.GetChild(1).GetComponent<Image>().enabled = true;
                }
                if (previous_nineteentoThirtysixValue > 0)
                {
                    nineteentoThirtysixValue = previous_nineteentoThirtysixValue;
                    Button temp = GameObject.Find("19-36").GetComponent<Button>();
                    temp.transform.GetChild(2).GetComponent<Text>().enabled = true;
                    temp.transform.GetChild(2).GetComponent<Text>().text = nineteentoThirtysixValue.ToString();//(int.Parse(temp.transform.GetChild(1).GetComponent<Text>().text)).ToString();
                    temp.transform.GetChild(1).GetComponent<Image>().enabled = true;
                }
                if (previous_blackValue > 0)
                {
                    blackValue = previous_blackValue;
                    Button temp = GameObject.Find("Black").GetComponent<Button>();
                    temp.transform.GetChild(1).GetComponent<Text>().enabled = true;
                    temp.transform.GetChild(1).GetComponent<Text>().text = blackValue.ToString();//(int.Parse(temp.transform.GetChild(1).GetComponent<Text>().text)).ToString();
                    temp.transform.GetChild(0).GetComponent<Image>().enabled = true;
                }
                if (previous_redValue > 0)
                {
                    redValue = previous_redValue;
                    Button temp = GameObject.Find("Red").GetComponent<Button>();
                    temp.transform.GetChild(1).GetComponent<Text>().enabled = true;
                    temp.transform.GetChild(1).GetComponent<Text>().text = redValue.ToString();//(int.Parse(temp.transform.GetChild(1).GetComponent<Text>().text)).ToString();
                    temp.transform.GetChild(0).GetComponent<Image>().enabled = true;
                }
                if (previous_evenValue > 0)
                {

                    evenValue = previous_evenValue;
                    Button temp = GameObject.Find("Even").GetComponent<Button>();
                    temp.transform.GetChild(2).GetComponent<Text>().enabled = true;
                    temp.transform.GetChild(2).GetComponent<Text>().text = evenValue.ToString();//(int.Parse(temp.transform.GetChild(1).GetComponent<Text>().text)).ToString();
                    temp.transform.GetChild(1).GetComponent<Image>().enabled = true;

                }
                if (previous_oddValue > 0)
                {
                    oddValue = previous_oddValue;
                    Button temp = GameObject.Find("Odd").GetComponent<Button>();
                    temp.transform.GetChild(2).GetComponent<Text>().enabled = true;
                    temp.transform.GetChild(2).GetComponent<Text>().text = oddValue.ToString();//(int.Parse(temp.transform.GetChild(1).GetComponent<Text>().text)).ToString();
                    temp.transform.GetChild(1).GetComponent<Image>().enabled = true;
                }

                //totalrepeat();
                // totalBets += StraightUpValue.Sum() + SplitValue.Sum() + CornerValue.Sum() + StreetValue.Sum() + LineValue.Sum();
                // totalBets += dozenValue01 + dozenValue02 + dozenValue03 + ColumnValue01 + ColumnValue02 + ColumnValue03 + onetoEighteenValue + nineteentoThirtysixValue + blackValue + redValue + evenValue + oddValue;
                // //totalBets = Roulettelast.totalbets;
                // totalBetsTxt.text = totalBets.ToString();
                //Debug.Log("total bets"+ totalBets+"balance"+balance+"difference"+(balance - totalBets)); 
                //balance = balance - totalBets;


                Debug.Log("everything came till here");
                repeatclear();
                UpdateUi();

            }
        }
        void showui()
        {
            if (previous_straightvalues.Sum() + previous_splitvalue.Sum() + previous_cornervalue.Sum() + previous_streetvalue.Sum() + previous_linevalues.Sum() + previous_dozenvalue01 + previous_dozenvalue02 + previous_dozenvalue03 + previous_columvalue01 + previous_columnvalue02 + previous_columnvalue03 + previous_onetoEighteenValue + previous_nineteentoThirtysixValue + previous_blackValue + previous_redValue + previous_evenValue + previous_oddValue < balance)
            {
                if (previous_straight.Count > 0)
                {
                    for (int i = 0; i < previous_straight.Count; i++)
                    {
                        //StraightUpBets.Add(int.Parse(previous_straight[i]));//int.Parse(StraightUpBets[i]);
                        Button temp = GameObject.Find(previous_straight[i]).GetComponent<Button>();

                        StraightUpBets.Add(int.Parse(previous_straight[i]));
                        temp.transform.GetChild(1).GetComponent<Text>().enabled = true;
                        temp.transform.GetChild(1).GetComponent<Text>().text = previous_straightvalues[i].ToString();//(int.Parse(temp.transform.GetChild(1).GetComponent<Text>().text)).ToString();
                        temp.transform.GetChild(0).GetComponent<Image>().enabled = true;
                        //StraightUpBets[i] = int.Parse(previous_straight[i]); 
                        //AddBets(temp);
                    }
                    for (int i = 0; i < previous_straightvalues.Count; i++)
                    {
                        StraightUpValue.Add(previous_straightvalues[i]);//int.Parse(StraightUpBets[i]);
                    }
                }
                if (previous_split.Count > 0)
                {
                    for (int i = 0; i < previous_split.Count; i++)
                    {
                        //Debug.Log(i+"This is the value of i");
                        //SplitBets.Add(previous_split[i]);//int.Parse(StraightUpBets[i]);
                        Button temp = GameObject.Find(previous_split[i]).GetComponent<Button>();
                        //Button temp = GameObject.Find(previous_straight[i]).GetComponent<Button>();

                        SplitBets.Add(previous_split[i]);
                        temp.transform.GetChild(1).GetComponent<Text>().enabled = true;
                        temp.transform.GetChild(1).GetComponent<Text>().text = previous_splitvalue[i].ToString();//(int.Parse(temp.transform.GetChild(1).GetComponent<Text>().text)).ToString();
                        temp.transform.GetChild(0).GetComponent<Image>().enabled = true;
                        //GameObject.transform.GetChild(0).GetComponent<Image>().enabled = true;
                    }
                    for (int i = 0; i < previous_splitvalue.Count; i++)
                    {
                        SplitValue.Add(previous_splitvalue[i]);//int.Parse(StraightUpBets[i]);
                    }
                }
                if (previous_corner.Count > 0)
                {
                    for (int i = 0; i < previous_corner.Count; i++)
                    {
                        //CornerBets.Add(previous_corner[i]);//int.Parse(StraightUpBets[i]);
                        Button temp = GameObject.Find(previous_corner[i]).GetComponent<Button>();
                        CornerBets.Add(previous_corner[i]);
                        temp.transform.GetChild(1).GetComponent<Text>().enabled = true;
                        temp.transform.GetChild(1).GetComponent<Text>().text = previous_cornervalue[i].ToString();//(int.Parse(temp.transform.GetChild(1).GetComponent<Text>().text)).ToString();
                        temp.transform.GetChild(0).GetComponent<Image>().enabled = true;
                    }
                    for (int i = 0; i < previous_cornervalue.Count; i++)
                    {
                        CornerValue.Add(previous_cornervalue[i]);//int.Parse(StraightUpBets[i]);
                    }
                }
                if (previous_line.Count > 0)
                {
                    for (int i = 0; i < previous_line.Count; i++)
                    {
                        //LineBets.Add(previous_line[i]);//int.Parse(StraightUpBets[i]);
                        Button temp = GameObject.Find(previous_line[i]).GetComponent<Button>();
                        //Button temp = GameObject.Find(previous_corner[i]).GetComponent<Button>();
                        LineBets.Add(previous_line[i]);
                        temp.transform.GetChild(1).GetComponent<Text>().enabled = true;
                        temp.transform.GetChild(1).GetComponent<Text>().text = previous_linevalues[i].ToString();//(int.Parse(temp.transform.GetChild(1).GetComponent<Text>().text)).ToString();
                        temp.transform.GetChild(0).GetComponent<Image>().enabled = true;
                    }
                    for (int i = 0; i < previous_linevalues.Count; i++)
                    {
                        LineValue.Add(previous_linevalues[i]);//int.Parse(StraightUpBets[i]);
                    }
                }
                if (previous_street.Count > 0)
                {
                    for (int i = 0; i < previous_street.Count; i++)
                    {
                        //StreetBets.Add(previous_street[i]);//int.Parse(StraightUpBets[i]);
                        Button temp = GameObject.Find(previous_street[i]).GetComponent<Button>();
                        StreetBets.Add(previous_street[i]);
                        temp.transform.GetChild(1).GetComponent<Text>().enabled = true;
                        temp.transform.GetChild(1).GetComponent<Text>().text = previous_streetvalue[i].ToString();//(int.Parse(temp.transform.GetChild(1).GetComponent<Text>().text)).ToString();
                        temp.transform.GetChild(0).GetComponent<Image>().enabled = true;
                    }
                    for (int i = 0; i < previous_streetvalue.Count; i++)
                    {
                        StreetValue.Add(previous_streetvalue[i]);//int.Parse(StraightUpBets[i]);
                    }
                }

                if (previous_columvalue01 > 0)
                {
                    ColumnValue01 = previous_columvalue01;
                    Button temp = GameObject.Find("2:1(Col01)").GetComponent<Button>();
                    //StreetBets.Add(previous_straight[i]);
                    temp.transform.GetChild(2).GetComponent<Text>().enabled = true;
                    temp.transform.GetChild(2).GetComponent<Text>().text = ColumnValue01.ToString();//(int.Parse(temp.transform.GetChild(1).GetComponent<Text>().text)).ToString();
                    temp.transform.GetChild(1).GetComponent<Image>().enabled = true;
                }
                if (previous_columnvalue02 > 0)
                {
                    ColumnValue02 = previous_columnvalue02;
                    Button temp = GameObject.Find("2:1(Col02)").GetComponent<Button>();
                    temp.transform.GetChild(2).GetComponent<Text>().enabled = true;
                    temp.transform.GetChild(2).GetComponent<Text>().text = ColumnValue02.ToString();//(int.Parse(temp.transform.GetChild(1).GetComponent<Text>().text)).ToString();
                    temp.transform.GetChild(1).GetComponent<Image>().enabled = true;
                }
                if (previous_columnvalue03 > 0)
                {
                    ColumnValue03 = previous_columnvalue03;
                    Button temp = GameObject.Find("2:1(Col03)").GetComponent<Button>();
                    temp.transform.GetChild(2).GetComponent<Text>().enabled = true;
                    temp.transform.GetChild(2).GetComponent<Text>().text = ColumnValue03.ToString();//(int.Parse(temp.transform.GetChild(1).GetComponent<Text>().text)).ToString();
                    temp.transform.GetChild(1).GetComponent<Image>().enabled = true;
                }
                if (previous_dozenvalue01 > 0)
                {
                    dozenValue01 = previous_dozenvalue01;
                    Button temp = GameObject.Find("1st12").GetComponent<Button>();
                    temp.transform.GetChild(2).GetComponent<Text>().enabled = true;
                    temp.transform.GetChild(2).GetComponent<Text>().text = dozenValue01.ToString();//(int.Parse(temp.transform.GetChild(1).GetComponent<Text>().text)).ToString();
                    temp.transform.GetChild(1).GetComponent<Image>().enabled = true;
                }
                if (previous_dozenvalue02 > 0)
                {
                    dozenValue02 = previous_dozenvalue02;
                    Button temp = GameObject.Find("2nd12").GetComponent<Button>();
                    temp.transform.GetChild(2).GetComponent<Text>().enabled = true;
                    temp.transform.GetChild(2).GetComponent<Text>().text = dozenValue02.ToString();//(int.Parse(temp.transform.GetChild(1).GetComponent<Text>().text)).ToString();
                    temp.transform.GetChild(1).GetComponent<Image>().enabled = true;
                }
                if (previous_dozenvalue03 > 0)
                {
                    dozenValue03 = previous_dozenvalue03;
                    Button temp = GameObject.Find("3rd 12").GetComponent<Button>();
                    temp.transform.GetChild(2).GetComponent<Text>().enabled = true;
                    temp.transform.GetChild(2).GetComponent<Text>().text = dozenValue03.ToString();//(int.Parse(temp.transform.GetChild(1).GetComponent<Text>().text)).ToString();
                    temp.transform.GetChild(1).GetComponent<Image>().enabled = true;
                }
                if (previous_onetoEighteenValue > 0)
                {
                    onetoEighteenValue = previous_onetoEighteenValue;
                    Button temp = GameObject.Find("1st18").GetComponent<Button>();
                    temp.transform.GetChild(2).GetComponent<Text>().enabled = true;
                    temp.transform.GetChild(2).GetComponent<Text>().text = onetoEighteenValue.ToString();//(int.Parse(temp.transform.GetChild(1).GetComponent<Text>().text)).ToString();
                    temp.transform.GetChild(1).GetComponent<Image>().enabled = true;
                }
                if (previous_nineteentoThirtysixValue > 0)
                {
                    nineteentoThirtysixValue = previous_nineteentoThirtysixValue;
                    Button temp = GameObject.Find("19-36").GetComponent<Button>();
                    temp.transform.GetChild(2).GetComponent<Text>().enabled = true;
                    temp.transform.GetChild(2).GetComponent<Text>().text = nineteentoThirtysixValue.ToString();//(int.Parse(temp.transform.GetChild(1).GetComponent<Text>().text)).ToString();
                    temp.transform.GetChild(1).GetComponent<Image>().enabled = true;
                }
                if (previous_blackValue > 0)
                {
                    blackValue = previous_blackValue;
                    Button temp = GameObject.Find("Black").GetComponent<Button>();
                    temp.transform.GetChild(1).GetComponent<Text>().enabled = true;
                    temp.transform.GetChild(1).GetComponent<Text>().text = blackValue.ToString();//(int.Parse(temp.transform.GetChild(1).GetComponent<Text>().text)).ToString();
                    temp.transform.GetChild(0).GetComponent<Image>().enabled = true;
                }
                if (previous_redValue > 0)
                {
                    redValue = previous_redValue;
                    Button temp = GameObject.Find("Red").GetComponent<Button>();
                    temp.transform.GetChild(1).GetComponent<Text>().enabled = true;
                    temp.transform.GetChild(1).GetComponent<Text>().text = redValue.ToString();//(int.Parse(temp.transform.GetChild(1).GetComponent<Text>().text)).ToString();
                    temp.transform.GetChild(0).GetComponent<Image>().enabled = true;
                }
                if (previous_evenValue > 0)
                {

                    evenValue = previous_evenValue;
                    Button temp = GameObject.Find("Even").GetComponent<Button>();
                    temp.transform.GetChild(2).GetComponent<Text>().enabled = true;
                    temp.transform.GetChild(2).GetComponent<Text>().text = evenValue.ToString();//(int.Parse(temp.transform.GetChild(1).GetComponent<Text>().text)).ToString();
                    temp.transform.GetChild(1).GetComponent<Image>().enabled = true;

                }
                if (previous_oddValue > 0)
                {
                    oddValue = previous_oddValue;
                    Button temp = GameObject.Find("Odd").GetComponent<Button>();
                    temp.transform.GetChild(2).GetComponent<Text>().enabled = true;
                    temp.transform.GetChild(2).GetComponent<Text>().text = oddValue.ToString();//(int.Parse(temp.transform.GetChild(1).GetComponent<Text>().text)).ToString();
                    temp.transform.GetChild(1).GetComponent<Image>().enabled = true;
                }

                //totalrepeat();
                totalBets += StraightUpValue.Sum() + SplitValue.Sum() + CornerValue.Sum() + StreetValue.Sum() + LineValue.Sum() + dozen01Value.Sum();
                totalBets += dozenValue01+dozenValue02 + dozenValue03 + ColumnValue01 + ColumnValue02 + ColumnValue03 + onetoEighteenValue + nineteentoThirtysixValue + blackValue + redValue + evenValue + oddValue;
                //totalBets = Roulettelast.totalbets;
                totalBetsTxt.text = totalBets.ToString();
                //Debug.Log("total bets"+ totalBets+"balance"+balance+"difference"+(balance - totalBets)); 
                //balance = balance - totalBets;
                if (repeated)
                {
                    balance = balance - totalBets;
                    balanceTxt.text = balance.ToString("F2");
                    repeated = false;
                }
                if (balance > 0)
                {
                    balanceTxt.text = balance.ToString("F2");
                }
                //Debug.Log("//////////////////////////////////////////////////////////////////////////////////////////////////////");
            }
        }
        public void showlastui()
        {

            if (previous_straightvalues.Sum() + previous_splitvalue.Sum() + previous_cornervalue.Sum() + previous_streetvalue.Sum() + previous_linevalues.Sum() + previous_dozenvalue01 + previous_dozenvalue02 + previous_dozenvalue03 + previous_columvalue01 + previous_columnvalue02 + previous_columnvalue03 + previous_onetoEighteenValue + previous_nineteentoThirtysixValue + previous_blackValue + previous_redValue + previous_evenValue + previous_oddValue < balance)
            {
                if (previous_straight.Count > 0)
                {
                    for (int i = 0; i < previous_straight.Count; i++)
                    {
                        //StraightUpBets.Add(int.Parse(previous_straight[i]));//int.Parse(StraightUpBets[i]);
                        Button temp = GameObject.Find(previous_straight[i]).GetComponent<Button>();

                        StraightUpBets.Add(int.Parse(previous_straight[i]));
                        temp.transform.GetChild(1).GetComponent<Text>().enabled = true;
                        temp.transform.GetChild(1).GetComponent<Text>().text = previous_straightvalues[i].ToString();//(int.Parse(temp.transform.GetChild(1).GetComponent<Text>().text)).ToString();
                        temp.transform.GetChild(0).GetComponent<Image>().enabled = true;
                        //StraightUpBets[i] = int.Parse(previous_straight[i]); 
                        //AddBets(temp);
                    }
                    for (int i = 0; i < previous_straightvalues.Count; i++)
                    {
                        StraightUpValue.Add(previous_straightvalues[i]);//int.Parse(StraightUpBets[i]);
                    }
                }
                if (previous_split.Count > 0)
                {
                    for (int i = 0; i < previous_split.Count; i++)
                    {
                        //Debug.Log(i+"This is the value of i");
                        //SplitBets.Add(previous_split[i]);//int.Parse(StraightUpBets[i]);
                        Button temp = GameObject.Find(previous_split[i]).GetComponent<Button>();
                        //Button temp = GameObject.Find(previous_straight[i]).GetComponent<Button>();

                        SplitBets.Add(previous_split[i]);
                        temp.transform.GetChild(1).GetComponent<Text>().enabled = true;
                        temp.transform.GetChild(1).GetComponent<Text>().text = previous_splitvalue[i].ToString();//(int.Parse(temp.transform.GetChild(1).GetComponent<Text>().text)).ToString();
                        temp.transform.GetChild(0).GetComponent<Image>().enabled = true;
                        //GameObject.transform.GetChild(0).GetComponent<Image>().enabled = true;
                    }
                    for (int i = 0; i < previous_splitvalue.Count; i++)
                    {
                        SplitValue.Add(previous_splitvalue[i]);//int.Parse(StraightUpBets[i]);
                    }
                }
                if (previous_corner.Count > 0)
                {
                    for (int i = 0; i < previous_corner.Count; i++)
                    {
                        //CornerBets.Add(previous_corner[i]);//int.Parse(StraightUpBets[i]);
                        Button temp = GameObject.Find(previous_corner[i]).GetComponent<Button>();
                        CornerBets.Add(previous_corner[i]);
                        temp.transform.GetChild(1).GetComponent<Text>().enabled = true;
                        temp.transform.GetChild(1).GetComponent<Text>().text = previous_cornervalue[i].ToString();//(int.Parse(temp.transform.GetChild(1).GetComponent<Text>().text)).ToString();
                        temp.transform.GetChild(0).GetComponent<Image>().enabled = true;
                    }
                    for (int i = 0; i < previous_cornervalue.Count; i++)
                    {
                        CornerValue.Add(previous_cornervalue[i]);//int.Parse(StraightUpBets[i]);
                    }
                }
                if (previous_line.Count > 0)
                {
                    for (int i = 0; i < previous_line.Count; i++)
                    {
                        //LineBets.Add(previous_line[i]);//int.Parse(StraightUpBets[i]);
                        Button temp = GameObject.Find(previous_line[i]).GetComponent<Button>();
                        //Button temp = GameObject.Find(previous_corner[i]).GetComponent<Button>();
                        LineBets.Add(previous_line[i]);
                        temp.transform.GetChild(1).GetComponent<Text>().enabled = true;
                        temp.transform.GetChild(1).GetComponent<Text>().text = previous_linevalues[i].ToString();//(int.Parse(temp.transform.GetChild(1).GetComponent<Text>().text)).ToString();
                        temp.transform.GetChild(0).GetComponent<Image>().enabled = true;
                    }
                    for (int i = 0; i < previous_linevalues.Count; i++)
                    {
                        LineValue.Add(previous_linevalues[i]);//int.Parse(StraightUpBets[i]);
                    }
                }
                if (previous_street.Count > 0)
                {
                    for (int i = 0; i < previous_street.Count; i++)
                    {
                        //StreetBets.Add(previous_street[i]);//int.Parse(StraightUpBets[i]);
                        Button temp = GameObject.Find(previous_street[i]).GetComponent<Button>();
                        StreetBets.Add(previous_street[i]);
                        temp.transform.GetChild(1).GetComponent<Text>().enabled = true;
                        temp.transform.GetChild(1).GetComponent<Text>().text = previous_streetvalue[i].ToString();//(int.Parse(temp.transform.GetChild(1).GetComponent<Text>().text)).ToString();
                        temp.transform.GetChild(0).GetComponent<Image>().enabled = true;
                    }
                    for (int i = 0; i < previous_streetvalue.Count; i++)
                    {
                        StreetValue.Add(previous_streetvalue[i]);//int.Parse(StraightUpBets[i]);
                    }
                }

                if (previous_columvalue01 > 0)
                {
                    ColumnValue01 = previous_columvalue01;
                    Button temp = GameObject.Find("2:1(Col01)").GetComponent<Button>();
                    //StreetBets.Add(previous_straight[i]);
                    temp.transform.GetChild(2).GetComponent<Text>().enabled = true;
                    temp.transform.GetChild(2).GetComponent<Text>().text = ColumnValue01.ToString();//(int.Parse(temp.transform.GetChild(1).GetComponent<Text>().text)).ToString();
                    temp.transform.GetChild(1).GetComponent<Image>().enabled = true;
                }
                if (previous_columnvalue02 > 0)
                {
                    ColumnValue02 = previous_columnvalue02;
                    Button temp = GameObject.Find("2:1(Col02)").GetComponent<Button>();
                    temp.transform.GetChild(2).GetComponent<Text>().enabled = true;
                    temp.transform.GetChild(2).GetComponent<Text>().text = ColumnValue02.ToString();//(int.Parse(temp.transform.GetChild(1).GetComponent<Text>().text)).ToString();
                    temp.transform.GetChild(1).GetComponent<Image>().enabled = true;
                }
                if (previous_columnvalue03 > 0)
                {
                    ColumnValue03 = previous_columnvalue03;
                    Button temp = GameObject.Find("2:1(Col03)").GetComponent<Button>();
                    temp.transform.GetChild(2).GetComponent<Text>().enabled = true;
                    temp.transform.GetChild(2).GetComponent<Text>().text = ColumnValue03.ToString();//(int.Parse(temp.transform.GetChild(1).GetComponent<Text>().text)).ToString();
                    temp.transform.GetChild(1).GetComponent<Image>().enabled = true;
                }
                if (previous_dozenvalue01 > 0)
                {
                    dozenValue01 = previous_dozenvalue01;
                    Button temp = GameObject.Find("1st12").GetComponent<Button>();
                    temp.transform.GetChild(2).GetComponent<Text>().enabled = true;
                    temp.transform.GetChild(2).GetComponent<Text>().text = dozenValue01.ToString();//(int.Parse(temp.transform.GetChild(1).GetComponent<Text>().text)).ToString();
                    temp.transform.GetChild(1).GetComponent<Image>().enabled = true;
                }
                if (previous_dozenvalue02 > 0)
                {
                    dozenValue02 = previous_dozenvalue02;
                    Button temp = GameObject.Find("2nd12").GetComponent<Button>();
                    temp.transform.GetChild(2).GetComponent<Text>().enabled = true;
                    temp.transform.GetChild(2).GetComponent<Text>().text = dozenValue02.ToString();//(int.Parse(temp.transform.GetChild(1).GetComponent<Text>().text)).ToString();
                    temp.transform.GetChild(1).GetComponent<Image>().enabled = true;
                }
                if (previous_dozenvalue03 > 0)
                {
                    dozenValue03 = previous_dozenvalue03;
                    Button temp = GameObject.Find("3rd 12").GetComponent<Button>();
                    temp.transform.GetChild(2).GetComponent<Text>().enabled = true;
                    temp.transform.GetChild(2).GetComponent<Text>().text = dozenValue03.ToString();//(int.Parse(temp.transform.GetChild(1).GetComponent<Text>().text)).ToString();
                    temp.transform.GetChild(1).GetComponent<Image>().enabled = true;
                }
                if (previous_onetoEighteenValue > 0)
                {
                    onetoEighteenValue = previous_onetoEighteenValue;
                    Button temp = GameObject.Find("1st18").GetComponent<Button>();
                    temp.transform.GetChild(2).GetComponent<Text>().enabled = true;
                    temp.transform.GetChild(2).GetComponent<Text>().text = onetoEighteenValue.ToString();//(int.Parse(temp.transform.GetChild(1).GetComponent<Text>().text)).ToString();
                    temp.transform.GetChild(1).GetComponent<Image>().enabled = true;
                }
                if (previous_nineteentoThirtysixValue > 0)
                {
                    nineteentoThirtysixValue = previous_nineteentoThirtysixValue;
                    Button temp = GameObject.Find("19-36").GetComponent<Button>();
                    temp.transform.GetChild(2).GetComponent<Text>().enabled = true;
                    temp.transform.GetChild(2).GetComponent<Text>().text = nineteentoThirtysixValue.ToString();//(int.Parse(temp.transform.GetChild(1).GetComponent<Text>().text)).ToString();
                    temp.transform.GetChild(1).GetComponent<Image>().enabled = true;
                }
                if (previous_blackValue > 0)
                {
                    blackValue = previous_blackValue;
                    Button temp = GameObject.Find("Black").GetComponent<Button>();
                    temp.transform.GetChild(1).GetComponent<Text>().enabled = true;
                    temp.transform.GetChild(1).GetComponent<Text>().text = blackValue.ToString();//(int.Parse(temp.transform.GetChild(1).GetComponent<Text>().text)).ToString();
                    temp.transform.GetChild(0).GetComponent<Image>().enabled = true;
                }
                if (previous_redValue > 0)
                {
                    redValue = previous_redValue;
                    Button temp = GameObject.Find("Red").GetComponent<Button>();
                    temp.transform.GetChild(1).GetComponent<Text>().enabled = true;
                    temp.transform.GetChild(1).GetComponent<Text>().text = redValue.ToString();//(int.Parse(temp.transform.GetChild(1).GetComponent<Text>().text)).ToString();
                    temp.transform.GetChild(0).GetComponent<Image>().enabled = true;
                }
                if (previous_evenValue > 0)
                {

                    evenValue = previous_evenValue;
                    Button temp = GameObject.Find("Even").GetComponent<Button>();
                    temp.transform.GetChild(2).GetComponent<Text>().enabled = true;
                    temp.transform.GetChild(2).GetComponent<Text>().text = evenValue.ToString();//(int.Parse(temp.transform.GetChild(1).GetComponent<Text>().text)).ToString();
                    temp.transform.GetChild(1).GetComponent<Image>().enabled = true;

                }
                if (previous_oddValue > 0)
                {
                    oddValue = previous_oddValue;
                    Button temp = GameObject.Find("Odd").GetComponent<Button>();
                    temp.transform.GetChild(2).GetComponent<Text>().enabled = true;
                    temp.transform.GetChild(2).GetComponent<Text>().text = oddValue.ToString();//(int.Parse(temp.transform.GetChild(1).GetComponent<Text>().text)).ToString();
                    temp.transform.GetChild(1).GetComponent<Image>().enabled = true;
                }

                //totalrepeat();
                // totalBets += StraightUpValue.Sum() + SplitValue.Sum() + CornerValue.Sum() + StreetValue.Sum() + LineValue.Sum() + dozen01Value.Sum();
                // totalBets += dozenValue01 +dozenValue02 + dozenValue03 + ColumnValue01 + ColumnValue02 + ColumnValue03 + onetoEighteenValue + nineteentoThirtysixValue + blackValue + redValue + evenValue + oddValue;
                totalBets = Roulettelast.totalbets;
                totalBetsTxt.text = totalBets.ToString();
                //Debug.Log("total bets"+ totalBets+"balance"+balance+"difference"+(balance - totalBets)); 
                //balance = balance - totalBets;
            }
        }


        void repeatclear()
        {
            if (previous_straight.Count > 0)
            {
                previous_straight.Clear();
                previous_straightvalues.Clear();
            }
            if (previous_split.Count > 0)
            {
                previous_split.Clear();
                previous_splitvalue.Clear();
            }
            if (previous_corner.Count > 0)
            {
                previous_corner.Clear();
                previous_cornervalue.Clear();
            }
            if (previous_line.Count > 0)
            {
                previous_line.Clear();
                previous_linevalues.Clear();
            }
            if (previous_line.Count > 0)
            {
                previous_line.Clear();
                previous_linevalues.Clear();
            }
            if (previous_street.Count > 0)
            {
                previous_street.Clear();
                previous_streetvalue.Clear();
            }
            previous_dozenvalue01 = previous_dozenvalue02 = previous_dozenvalue03 = previous_columvalue01 = previous_columnvalue02 = previous_columnvalue03 = 0;
            previous_onetoEighteenValue = previous_nineteentoThirtysixValue = previous_evenValue = previous_oddValue = previous_blackValue = previous_redValue = 0;
            //Debug.Log("clearing values");

        }
        public GameObject Shadow;
        public IEnumerator OnRoundEnd(object o)
        {
            isTimeUp = true;
            //Roulette_ServerResponse.instance.rouletterounds++;
            // if (!spinnoww)
            // {
            //     yield break;
            // }
            yield return new WaitUntil(() => currentTime == 0);

            timer_AudioSource.GetComponent<AudioSource>().Stop();
            if (spinnoww)
            {
                wheelobj.SetBool("wheelzoom", true);
                Shadow.SetActive(true);
            }
           
            //Shadow.SetActive(true);
            //anime.SetBool("wheelzoom",true);
            //if(Onzoom)then only preform below
            //Camera.main.GetComponent<Animation>().Play("Camera_Expand");

            DiceWinNos windata = JsonConvert.DeserializeObject<DiceWinNos>(o.ToString());
            Debug.LogError("this is the type of the winno received" + windata.winNo.GetType());
            if (windata.winNo is string)
            {
                Winno = -1;
                Debug.Log("The variable is of an unknown type.");
            }
            else
            {
                
                int.TryParse(windata.winNo.ToString(), out Winno);
                Debug.Log("The variable is of type int." + Winno);
            }
            while (windata.previousWin_single.Count > 5)
            {
                windata.previousWin_single.RemoveAt(0);
            }

            for (int i = 0; i < PreviousWin_list.Count; i++)
            {
                PreviousWin_list[i] = windata.previousWin_single[i];
            }
            if (!spinnoww)
            {
                yield break;
            }
            // if(windata.winNo is int64)
            // {
            //     Winno = (int)windata.winNo;
            //     Debug.Log("The variable is of type int.");
            // }
            // if(windata.winNo is string)
            // {
            //     Winno = -1;
            //     Debug.Log("The variable is of an unknown type.");
            // }
            //Winno = windata.winNo;


            if (spinnoww)
            {
                //StartCoroutine(roulettewinresponse());
            }
            //PlayerPrefs.SetInt("Winno",int.Parse(WinText.text));
            //if (spinnoww)
            //{
            //SpinRouletteWheelWithoutPlugin.instane.Spin(Winno);
            //ballrotator.instant.rotatrnow(Winno);
            //}
            //spinnoww = false;
            repeatbutton.interactable = false;
            taken = false;
            takeBtn.interactable = true;
            // if (windata.winPoint > 0)
            // {

            //     TakeBlink();
            // }

            
            //ballrotator.instant.StartCoroutine(rotator());
            //RouletteWheel.instance.Spin(Winno);

            // RoulleteRotateDisplay.Instance.wheel_rotateSpeed = 3.0f;
            // RoulleteRotateDisplay.Instance.rotateSpeed = 80.0f;

            /*for(int i = windata.previousWin_single.Count - 1; i >= windata.previousWin_single.Count - 5 ; i--)
            {
                if(PreviousWin_local.Count > 5)//add equal here
                {
                    PreviousWin_local.RemoveAt(0);//4
                    PreviousWin_local.Add(windata.previousWin_single[i]);
                    break;
                }
                else
                {
                    PreviousWin_local.Add(windata.previousWin_single[i]);
                }
            }*/
            // PreviousWin_singleLocal.Reverse();
            //PreviousWin_list.Clear();
            // while(PreviousWin_list.Count > 4)
            // {
            //     PreviousWin_list.RemoveAt(0);
            // }
            // PreviousWin_list.Add(Winno);
            // for(int j = 0; j < PreviousWin_local.Count; j++)
            // {
            //     PreviousWin_list.Add(PreviousWin_local[j]);
            // }

            // if (count > 4)
            // {
            //     count = 4;
            // }
            // for(int i = 0; i < PreviousWin_list.Count; i++)
            // {
            //     PreviousWin_Text[i].GetComponent<Text>().text =  PreviousWin_list[i].ToString();
            // }

            yield return new WaitForSeconds(2.0f);
            //RoulleteRotateDisplay.Instance.rotateSpeed = 10.0f;//here
            repeatclear();
            //previousassignment();
            StartCoroutine(RoundEndIenums());
        }
        public void afterzoom()
        {
            SpinRouletteWheelWithoutPlugin.instane.Spin(Winno);
        
            ballrotator.instant.rotatrnow(Winno);
            //}
            //else
            //{
            //    ballrotator.instant.rotatrnow(37);
            //}

            spin_AudioSource.GetComponent<AudioSource>().Play();
        }
        public void tester(int value)
        {
            SpinRouletteWheelWithoutPlugin.instane.Spin(value);
        
            ballrotator.instant.rotatrnow(value);
        }
        public IEnumerator roulettewinresponse()
        {

            WWWForm form = new WWWForm();
            string playername = "GK" + PlayerPrefs.GetString("email");
            int gameid = 7;
            form.AddField("playerId", playername);
            form.AddField("game_id", gameid);
            using (UnityWebRequest www = UnityWebRequest.Post(winamounturl, form))
            {
                yield return www.SendWebRequest();//playername

                if (www.result != UnityWebRequest.Result.Success)
                {
                    Debug.LogError(www.error);
                }
                else
                {
                    Debug.Log("playerid"+playername+" "+www.downloadHandler.text);
                    winneramount response = JsonConvert.DeserializeObject<winneramount>(www.downloadHandler.text);
                    //winPoint = response.data.Winamount;
                    if (response.status == 200)
                    {
                        winningAmount = response.data.Winamount;
                        WinText.text = response.data.Winamount.ToString();
                        currentwin = response.data.Winamount;
                        winround = balance + winningAmount;
                        if (response.data.Winamount > 0)
                        {
                            takeBtn.transform.GetChild(1).gameObject.SetActive(true);
                            Debug.Log("start the animation");
                            StartCoroutine(TakeBlink());

                        }
                        if  (winningAmount == 0) //(int.Parse(WinText.text) == 0)
                        {
                            MessagePopup.text = "Please Collect Winning amount First";
                            displayprevious();
                            ResetAllBets();
                        }
                        // if (winningAmount>0)
                        // {
                        //     PlayerPrefs.SetInt("Winno",winningAmount);
                        // }
                        //Debug.Log("/////////////////////////////////////////" +response.data.Winamount );
                        if (started)
                        {
                            
                            WinText.text = response.data.Winamount.ToString();
                            if (response.data.Winamount > 0)
                            {
                                Debug.LogWarning("it came from started in funtargetwinresponmse");
                                restore();
                                //restorewithin();
                                //showlastui();
                            }
                            else
                            {
                                betOkBtn.gameObject.SetActive(false);
                                repeatbtn.SetActive(true);
                                StartCoroutine(RepeatBlinkAnim());
                            }
                            started = false;
                        }
                         
                            
                       
                    }
                    //winround = AUI.balance+winPoint;
                }
            }
        }
        public void displayprevious()
        {
            for (int i = 0; i < PreviousWin_list.Count; i++)
            {
                // Debug.Log("the data final is"+PreviousWin_list[i].GetType());
                if (PreviousWin_list[i] == "37")
                {
                    PreviousWin_Text[i].GetComponent<Text>().text = "00";
                }
                else
                {
                    PreviousWin_Text[i].GetComponent<Text>().text = PreviousWin_list[i].ToString();

                }
            }
        }

        public IEnumerator emptyroulettewinresponse()
        {

            WWWForm form = new WWWForm();
            string playername = "GK" + PlayerPrefs.GetString("email");
            int gameid = 7;
            form.AddField("playerId", playername);
            form.AddField("game_id", gameid);
            using (UnityWebRequest www = UnityWebRequest.Post(emptyurl, form))
            {
                yield return www.SendWebRequest();

                if (www.result != UnityWebRequest.Result.Success)
                {
                    Debug.LogError(www.error);
                }
                else
                {
                    Debug.Log("the winning amount has been cleared");
                    winningAmount = 0;
                    balanceTxt.text = balance.ToString("F2"); 
                    WinText.text = winningAmount.ToString();
                    // winneramount response = JsonConvert.DeserializeObject<winneramount>(www.downloadHandler.text);
                    // //winPoint = response.data.Winamount;
                    // winningAmount = response.data.Winamount;
                    // currentwin = response.data.Winamount;
                    // winround = balance + winningAmount;
                    // Debug.Log("/////////////////////////////////////////" +response.data.Winamount );
                    // if(started)
                    // {
                    //     WinText.text = response.data.Winamount.ToString();
                    //     started = false;
                    // }
                    //winround = AUI.balance+winPoint;
                }
            }
        }
        public void rotationbtn(int Winno)
        {
            // Winno= 25;
            SpinRouletteWheelWithoutPlugin.instane.Spin(Winno);
            ballrotator.instant.rotatrnow(Winno);
        }
        public void showwinamount()
        {
            StartCoroutine(roulettewinresponse());
            for (int i = 0; i < PreviousWin_list.Count; i++)
                {
                    // Debug.Log("the data final is"+PreviousWin_list[i].GetType());
                    if (PreviousWin_list[i] == "37")
                    {
                        PreviousWin_Text[i].GetComponent<Text>().text = "00";
                    }
                    else
                    {
                        PreviousWin_Text[i].GetComponent<Text>().text = PreviousWin_list[i].ToString();

                    }
                }
            WinText.text = winningAmount.ToString();
            winmusic.Play();
        }
        [SerializeField] AudioSource winmusic;

        public void playmusic()
        {
            winmusic.Play();
        }
        IEnumerator RoundEndIenums()
        {
            yield return new WaitForSeconds(5.0f);
            spin_AudioSource.GetComponent<AudioSource>().Stop();
            
            _tableImage.SetActive(false);
            Camera.main.GetComponent<Animation>().Stop();
            Camera.main.GetComponent<Animation>().Play("Camera_Shrink");
            StartCoroutine(BetsBlink(Winno));

            //if(Winno >= 0)
            //{
            //    StartCoroutine(BetsBlink(Winno));
            //}
            //else
            //{
            //    StartCoroutine(BetsBlink(37));
            //}
            PlayerPrefs.SetInt("Winno", int.Parse(WinText.text));
            // RoulleteRotateDisplay.Instance.wheel_rotateSpeed = 1.0f;//here
            // RoulleteRotateDisplay.Instance.rotateSpeed = 0f;//here
            // RoulleteRotateDisplay.Instance.SetBallInRoullete(Winno.ToString());//here
            //WinText.text = winningAmount.ToString();
            //Debug.Log("winning amount" +winningAmount + "current win" + currentwin);


            // while(PreviousWin_list.Count > 4)
            // {
            //     PreviousWin_list.RemoveAt(0);
            // }
            // PreviousWin_list.Add(Winno);

            if (taken || winningAmount == currentwin)
            {
                
                for (int i = 0; i < PreviousWin_list.Count; i++)
                {
                    // Debug.Log("the data final is"+PreviousWin_list[i].GetType());
                    if (PreviousWin_list[i] == "37")
                    {
                        PreviousWin_Text[i].GetComponent<Text>().text = "00";
                    }
                    else
                    {
                        PreviousWin_Text[i].GetComponent<Text>().text = PreviousWin_list[i].ToString();

                    }
                }
                
            }
            textcolor();

            // else if(!taken)
            // {
            //     if (transfer)
            //     {
            //         while(PreviousWin_list.Count > 4)
            //         {
            //             PreviousWin_list.RemoveAt(0);
            //         }
            //         PreviousWin_list.Add(Winno);
            //         for (int i = 0; i < PreviousWin_list.Count; i++)
            //         {
            //             PreviousWin_local.Add(PreviousWin_list[i]);
            //             llocaltext[i].GetComponent<Text>().text = PreviousWin_local[i].ToString();
            //         }
            //     }
            //     else if(transfer == false)
            //     {
            //         while(PreviousWin_local.Count > 4)
            //         {
            //             PreviousWin_local.RemoveAt(0);
            //         }
            //         PreviousWin_list.Add(Winno);
            //         for(int i = 0; i < PreviousWin_list.Count; i++)
            //     {
            //         llocaltext[i].GetComponent<Text>().text =  PreviousWin_local[i].ToString();
            //     }

            //     }
            // }


            // if (winningAmount > 0)
            // {
            //     //Debug.Log("reached");
            //     StartCoroutine(TakeBlink());
            //     StartCoroutine(TakeBlinkAnim());

            // }
            // if  (winningAmount == 0) //(int.Parse(WinText.text) == 0)
            // {
            //     MessagePopup.text = "Please Collect Winning amount First";
            //     ResetAllBets();
            // }
            repeatbutton.interactable = true;

            yield return new WaitForSeconds(3.0f);
            // RoulleteRotateDisplay.Instance.rotateSpeed = 30.0f;//here
            // RoulleteRotateDisplay.Instance.DisabledLastSphere();//here
        }

        private void UpdateUi()
        {
            balanceTxt.text = balance.ToString("F2");
            totalBetsTxt.text = totalBets.ToString();
        }

        public void ResetAllBets()
        {
            //betok.SetActive(false);
            betOkBtn.interactable = true;
            if (StraightUpBets.Count > 0)
            {
                StraightUpBets.Clear();
                StraightUpValue.Clear();
            }
            if (SplitBets.Count > 0)
            {
                SplitBets.Clear();
                SplitValue.Clear();
                if (SplitArray.Length > 0)
                {
                    SplitArray = new int[0, 0];
                    //Array.Clear(SplitArray, 0, SplitArray.Length);
                }
            }
            if (StreetBets.Count > 0)
            {
                StreetBets.Clear();
                StreetValue.Clear();
                if (StreetArray.Length > 0)
                {
                    StreetArray = new int[0, 0];
                    //Array.Clear(StreetArray, 0, StreetArray.Length);
                }
            }
            if (CornerBets.Count > 0)
            {
                CornerArray = new int[0, 0];
                Debug.Log("reached here");
                CornerBets.Clear();
                CornerValue.Clear();
                int[,] myarray = new int[0, 0];

                if (CornerArray.Length > 0)
                {
                    CornerArray = myarray;

                    //
                    //Array.Clear(CornerArray, 0, CornerArray.Length);
                }
            }

            if (SpecificBets.Count > 0)
            {
                SpecificBets.Clear();
                SpecificValue.Clear();
                if (SpecificArray.Length > 0)
                {
                    Array.Clear(SpecificArray, 0, SpecificArray.Length);
                }
            }

            if (LineBets.Count > 0)
            {
                LineBets.Clear();
                LineValue.Clear();
                if (LineArray.Length > 0)
                {
                    LineArray = new int[0, 0];
                    //Array.Clear(LineArray, 0, LineArray.Length);
                }
            }

            totalBets = 0;
            for (int i = 0; i < BetsNumber.Count; i++)
            {
                //Debug.Log("this is the value of i" + i);
                BetsNumber[i].gameObject.transform.GetChild(1).gameObject.transform.GetChild(0).GetComponent<Image>().enabled = false;
                BetsNumber[i].gameObject.transform.GetChild(1).gameObject.transform.GetChild(1).GetComponent<Text>().enabled = false;
                BetsNumber[i].gameObject.transform.GetChild(1).gameObject.transform.GetChild(1).GetComponent<Text>().text = "0";
            }

            for (int i = 0; i < SplitBets_Button.Count; i++)
            {
                SplitBets_Button[i].transform.GetChild(0).GetComponent<Image>().enabled = false;
                SplitBets_Button[i].transform.GetChild(1).GetComponent<Text>().enabled = false;
                SplitBets_Button[i].transform.GetChild(1).GetComponent<Text>().text = "0";
            }

            for (int i = 0; i < StreetBets_Button.Count; i++)
            {
                StreetBets_Button[i].transform.GetChild(0).GetComponent<Image>().enabled = false;
                StreetBets_Button[i].transform.GetChild(1).GetComponent<Text>().enabled = false;
                StreetBets_Button[i].transform.GetChild(1).GetComponent<Text>().text = "0";
            }

            for (int i = 0; i < CornerBets_Button.Count; i++)
            {
                CornerBets_Button[i].transform.GetChild(0).GetComponent<Image>().enabled = false;
                CornerBets_Button[i].transform.GetChild(1).GetComponent<Text>().enabled = false;

                CornerBets_Button[i].transform.GetChild(1).GetComponent<Text>().text = "0";
                // Debug.Log("name"+CornerBets_Button[i].transform.GetChild(1).name);
                // Debug.Log("name"+CornerBets_Button[i].name);
            }

            for (int i = 0; i < SpecificBets_Button.Count; i++)
            {
                SpecificBets_Button[i].transform.GetChild(0).GetComponent<Image>().enabled = false;
                SpecificBets_Button[i].transform.GetChild(1).GetComponent<Text>().enabled = false;
                SpecificBets_Button[i].transform.GetChild(1).GetComponent<Text>().text = "0";
            }

            for (int i = 0; i < LineBets_Button.Count; i++)
            {
                LineBets_Button[i].transform.GetChild(0).GetComponent<Image>().enabled = false;
                LineBets_Button[i].transform.GetChild(1).GetComponent<Text>().enabled = false;
                LineBets_Button[i].transform.GetChild(1).GetComponent<Text>().text = "0";
            }

            for (int i = 0; i < dozenBets_button.Count; i++)
            {
                dozenBets_button[i].transform.GetChild(1).GetComponent<Image>().enabled = false;

                dozenBets_button[i].transform.GetChild(2).GetComponent<Text>().text = "0";
                dozenBets_button[i].transform.GetChild(2).GetComponent<Text>().enabled = false;
            }

            for (int i = 0; i < ColumnBets_Button.Count; i++)
            {
                ColumnBets_Button[i].transform.GetChild(1).GetComponent<Image>().enabled = false;
                ColumnBets_Button[i].transform.GetChild(2).GetComponent<Text>().enabled = false;
                ColumnBets_Button[i].transform.GetChild(2).GetComponent<Text>().text = "0";
            }


            onetoEighteenBets_Button.transform.GetChild(1).GetComponent<Image>().enabled = false;
            onetoEighteenBets_Button.transform.GetChild(2).GetComponent<Text>().enabled = false;
            onetoEighteenBets_Button.transform.GetChild(2).GetComponent<Text>().text = "0";

            nineteentoThirtysixBets_Button.transform.GetChild(1).GetComponent<Image>().enabled = false;
            nineteentoThirtysixBets_Button.transform.GetChild(2).GetComponent<Text>().enabled = false;
            nineteentoThirtysixBets_Button.transform.GetChild(2).GetComponent<Text>().text = "0";

            evenBets_button.transform.GetChild(1).GetComponent<Image>().enabled = false;
            evenBets_button.transform.GetChild(2).GetComponent<Text>().enabled = false;
            evenBets_button.transform.GetChild(2).GetComponent<Text>().text = "0";

            oddBets_button.transform.GetChild(1).GetComponent<Image>().enabled = false;
            oddBets_button.transform.GetChild(2).GetComponent<Text>().enabled = false;
            oddBets_button.transform.GetChild(2).GetComponent<Text>().text = "0";

            redBets_button.transform.GetChild(0).GetComponent<Image>().enabled = false;
            redBets_button.transform.GetChild(1).GetComponent<Text>().enabled = false;
            redBets_button.transform.GetChild(1).GetComponent<Text>().text = "0";

            blackBets_button.transform.GetChild(0).GetComponent<Image>().enabled = false;
            blackBets_button.transform.GetChild(1).GetComponent<Text>().enabled = false;
            blackBets_button.transform.GetChild(1).GetComponent<Text>().text = "0";

            onetoEighteenValue = nineteentoThirtysixValue = oddValue = evenValue = dozenValue02 = dozenValue03 = ColumnValue01 = ColumnValue02 = ColumnValue03 = dozenValue01 = blackValue = redValue = 0;

            //canPlaceBet = true;
            // isTimeUp = false;
            PlayerPrefs.SetInt("addedBets", 0);
            UpdateUi();
        }
        public void CancelAllBets()///used in cancel bet button in unity
        {
            if ((totalBets > 0) && (!isBetConfirmed)) //(bet)
            {
                betOkBtn.interactable = true;
                balance += totalBets;
                if (StraightUpBets.Count > 0)
                {
                    StraightUpBets.Clear();
                    StraightUpValue.Clear();
                }
                if (SplitBets.Count > 0)
                {
                    SplitBets.Clear();
                    SplitValue.Clear();
                    if (SplitArray.Length > 0)
                    {
                        //Array.Clear(SplitArray, 0, SplitArray.Length);
                        SplitArray = new int[0, 0];
                    }
                }
                if (StreetBets.Count > 0)
                {
                    StreetBets.Clear();
                    StreetValue.Clear();
                    if (StreetArray.Length > 0)
                    {
                        StreetArray = new int[0, 0];
                        //Array.Clear(StreetArray, 0, StreetArray.Length);
                    }
                }
                if (CornerBets.Count > 0)
                {
                    CornerBets.Clear();
                    CornerValue.Clear();
                    if (CornerArray.Length > 0)
                    {
                        CornerArray = new int[0, 0];
                        //Array.Clear(CornerArray, 0, CornerArray.Length);
                    }
                }

                if (SpecificBets.Count > 0)
                {
                    SpecificBets.Clear();
                    SpecificValue.Clear();
                    if (SpecificArray.Length > 0)
                    {
                        Array.Clear(SpecificArray, 0, SpecificArray.Length);
                    }
                }

                if (LineBets.Count > 0)
                {
                    LineBets.Clear();
                    LineValue.Clear();
                    if (LineArray.Length > 0)
                    {
                        Debug.Log("reached here");
                        LineArray = new int[0, 0];
                        //Array.Clear(LineArray, 0, LineArray.Length);
                    }
                }

                totalBets = 0;
                for (int i = 0; i < BetsNumber.Count; i++)
                {
                    BetsNumber[i].transform.GetChild(1).gameObject.transform.GetChild(0).GetComponent<Image>().enabled = false;
                    BetsNumber[i].transform.GetChild(1).gameObject.transform.GetChild(1).GetComponent<Text>().enabled = false;
                    BetsNumber[i].transform.GetChild(1).gameObject.transform.GetChild(1).GetComponent<Text>().text = "0";
                }

                for (int i = 0; i < SplitBets_Button.Count; i++)
                {
                    SplitBets_Button[i].transform.GetChild(0).GetComponent<Image>().enabled = false;
                    SplitBets_Button[i].transform.GetChild(1).GetComponent<Text>().enabled = false;
                    SplitBets_Button[i].transform.GetChild(1).GetComponent<Text>().text = "0";
                }

                for (int i = 0; i < StreetBets_Button.Count; i++)
                {
                    StreetBets_Button[i].transform.GetChild(0).GetComponent<Image>().enabled = false;
                    StreetBets_Button[i].transform.GetChild(1).GetComponent<Text>().enabled = false;
                    StreetBets_Button[i].transform.GetChild(1).GetComponent<Text>().text = "0";
                }

                for (int i = 0; i < CornerBets_Button.Count; i++)
                {
                    CornerBets_Button[i].transform.GetChild(0).GetComponent<Image>().enabled = false;
                    CornerBets_Button[i].transform.GetChild(1).GetComponent<Text>().enabled = false;
                    CornerBets_Button[i].transform.GetChild(1).GetComponent<Text>().text = "0";
                }

                for (int i = 0; i < SpecificBets_Button.Count; i++)
                {
                    SpecificBets_Button[i].transform.GetChild(0).GetComponent<Image>().enabled = false;
                    SpecificBets_Button[i].transform.GetChild(1).GetComponent<Text>().enabled = false;
                    SpecificBets_Button[i].transform.GetChild(1).GetComponent<Text>().text = "0";
                }

                for (int i = 0; i < LineBets_Button.Count; i++)
                {
                    LineBets_Button[i].transform.GetChild(0).GetComponent<Image>().enabled = false;
                    LineBets_Button[i].transform.GetChild(1).GetComponent<Text>().enabled = false;
                    LineBets_Button[i].transform.GetChild(1).GetComponent<Text>().text = "0";
                }

                for (int i = 0; i < dozenBets_button.Count; i++)
                {
                    dozenBets_button[i].transform.GetChild(1).GetComponent<Image>().enabled = false;
                    dozenBets_button[i].transform.GetChild(2).GetComponent<Text>().enabled = false;
                    dozenBets_button[i].transform.GetChild(2).GetComponent<Text>().text = "0";
                }

                for (int i = 0; i < ColumnBets_Button.Count; i++)
                {
                    ColumnBets_Button[i].transform.GetChild(1).GetComponent<Image>().enabled = false;
                    ColumnBets_Button[i].transform.GetChild(2).GetComponent<Text>().enabled = false;
                    ColumnBets_Button[i].transform.GetChild(2).GetComponent<Text>().text = "0";
                }


                onetoEighteenBets_Button.transform.GetChild(1).GetComponent<Image>().enabled = false;
                onetoEighteenBets_Button.transform.GetChild(2).GetComponent<Text>().enabled = false;
                onetoEighteenBets_Button.transform.GetChild(2).GetComponent<Text>().text = "0";

                nineteentoThirtysixBets_Button.transform.GetChild(1).GetComponent<Image>().enabled = false;
                nineteentoThirtysixBets_Button.transform.GetChild(2).GetComponent<Text>().enabled = false;
                nineteentoThirtysixBets_Button.transform.GetChild(2).GetComponent<Text>().text = "0";

                evenBets_button.transform.GetChild(1).GetComponent<Image>().enabled = false;
                evenBets_button.transform.GetChild(2).GetComponent<Text>().enabled = false;
                evenBets_button.transform.GetChild(2).GetComponent<Text>().text = "0";

                oddBets_button.transform.GetChild(1).GetComponent<Image>().enabled = false;
                oddBets_button.transform.GetChild(2).GetComponent<Text>().enabled = false;
                oddBets_button.transform.GetChild(2).GetComponent<Text>().text = "0";

                redBets_button.transform.GetChild(0).GetComponent<Image>().enabled = false;
                redBets_button.transform.GetChild(1).GetComponent<Text>().enabled = false;
                redBets_button.transform.GetChild(1).GetComponent<Text>().text = "0";

                blackBets_button.transform.GetChild(0).GetComponent<Image>().enabled = false;
                blackBets_button.transform.GetChild(1).GetComponent<Text>().enabled = false;
                blackBets_button.transform.GetChild(1).GetComponent<Text>().text = "0";

                onetoEighteenValue = nineteentoThirtysixValue = oddValue = evenValue = dozenValue02 = dozenValue03 = ColumnValue01 = ColumnValue02 = ColumnValue03 = dozenValue01 = blackValue = redValue = 0;
                //canPlaceBet = true;
                repeatbutton.interactable = true;
                isTimeUp = false;
                UpdateUi();
            }
        }

        public void Specificclear()
        {
            int last;
            /*if(SpecificBets.Count > 0)
            {
                SpecificBets.Clear();
                SpecificValue.Clear();
                if(SpecificArray.Length > 0)
                {
                    Array.Clear(SpecificArray, 0, SpecificArray.Length);
                }
            }*/
            //Debug.Log("the Last placed bet is" +lastflag);
            switch (lastflag)
            {

                case "StraightUp":
                    //StraightUpBets[StraightUpBets.Count -1].GetChild(0)
                    lastused.transform.GetChild(0).GetComponent<Image>().enabled = false;
                    last = StraightUpValue[StraightUpValue.Count - 1];
                    balance += last;
                    StraightUpValue.RemoveAt(StraightUpValue.Count - 1);
                    lastused.transform.GetChild(1).GetComponent<Text>().text = "0";
                    StraightUpBets.RemoveAt(StraightUpBets.Count - 1);
                    lastflag = "";
                    break;
                case "SplitBets":
                    lastused.transform.GetChild(0).GetComponent<Image>().enabled = false;
                    last = SplitValue[SplitValue.Count - 1];
                    balance += last;
                    SplitValue.RemoveAt(SplitValue.Count - 1);
                    lastused.transform.GetChild(1).GetComponent<Text>().text = "0";
                    SplitBets.RemoveAt(SplitBets.Count - 1);
                    lastflag = "";
                    break;
                case "StreetBets":
                    lastused.transform.GetChild(0).GetComponent<Image>().enabled = false;
                    last = StreetValue[StreetValue.Count - 1];
                    balance += last;
                    StreetValue.RemoveAt(StreetValue.Count - 1);
                    lastused.transform.GetChild(1).GetComponent<Text>().text = "0";
                    StraightUpBets.RemoveAt(StraightUpBets.Count - 1);
                    lastflag = "";
                    break;
                case "CornerBets":
                    lastused.transform.GetChild(0).GetComponent<Image>().enabled = false;
                    last = CornerValue[CornerValue.Count - 1];
                    balance += last;
                    CornerValue.RemoveAt(CornerValue.Count - 1);
                    lastused.transform.GetChild(1).GetComponent<Text>().text = "0";
                    CornerBets.RemoveAt(CornerBets.Count - 1);
                    lastflag = "";
                    break;
                case "SpecificBets":
                    lastused.transform.GetChild(0).GetComponent<Image>().enabled = false;
                    last = SpecificValue[SpecificValue.Count - 1];
                    balance += last;
                    SpecificValue.RemoveAt(SpecificValue.Count - 1); lastused.transform.GetChild(1).GetComponent<Text>().text = "";
                    SpecificBets.RemoveAt(SpecificBets.Count - 1);
                    lastflag = "";
                    break;
                case "LineBets":
                    lastused.transform.GetChild(0).GetComponent<Image>().enabled = false;
                    last = LineValue[LineValue.Count - 1];
                    balance += last;
                    LineValue.RemoveAt(LineValue.Count - 1);
                    lastused.transform.GetChild(1).GetComponent<Text>().text = "0";
                    LineBets.RemoveAt(LineBets.Count - 1);
                    lastflag = "";
                    break;
                case "dozen01":
                    lastused.transform.GetChild(0).GetComponent<Image>().enabled = false;
                    last = dozen01Value[dozen01Value.Count - 1];
                    balance += last;
                    dozen01Value.RemoveAt(dozen01Value.Count - 1);
                    lastused.transform.GetChild(1).GetComponent<Text>().text = "0";
                    dozen01Bets.RemoveAt(dozen01Bets.Count - 1);
                    lastflag = "";
                    break;
                case "dozen02":
                    lastused.transform.GetChild(0).GetComponent<Image>().enabled = false;
                    balance += dozenValue02;
                    dozenValue02 = 0;
                    lastused.transform.GetChild(1).GetComponent<Text>().text = "0";
                    lastflag = "";
                    break;
                case "dozen03":
                    lastused.transform.GetChild(0).GetComponent<Image>().enabled = false;
                    balance += dozenValue03;
                    dozenValue03 = 0;
                    lastused.transform.GetChild(1).GetComponent<Text>().text = "0";
                    lastflag = "";
                    break;

                case "colum01":
                    lastused.transform.GetChild(0).GetComponent<Image>().enabled = false;
                    balance += ColumnValue01;
                    ColumnValue01 = 0;
                    lastused.transform.GetChild(1).GetComponent<Text>().text = "0";
                    lastflag = "";
                    break;
                case "colum02":
                    lastused.transform.GetChild(0).GetComponent<Image>().enabled = false;
                    balance += ColumnValue02;
                    ColumnValue02 = 0;
                    lastused.transform.GetChild(1).GetComponent<Text>().text = "0";
                    lastflag = "";
                    break;
                case "colum03":
                    lastused.transform.GetChild(0).GetComponent<Image>().enabled = false;
                    balance += ColumnValue03;
                    ColumnValue03 = 0;
                    lastused.transform.GetChild(1).GetComponent<Text>().text = "0";
                    lastflag = "";
                    break;
                case "OneToEighteen":
                    lastused.transform.GetChild(0).GetComponent<Image>().enabled = false;
                    balance += onetoEighteenValue;
                    onetoEighteenValue = 0;
                    lastused.transform.GetChild(1).GetComponent<Text>().text = "0";
                    lastflag = "";
                    break;
                case "NineteenToThirtySix":
                    lastused.transform.GetChild(0).GetComponent<Image>().enabled = false;
                    balance += nineteentoThirtysixValue;
                    nineteentoThirtysixValue = 0;
                    lastused.transform.GetChild(1).GetComponent<Text>().text = "0";
                    lastflag = "";
                    break;
                case "even":
                    lastused.transform.GetChild(0).GetComponent<Image>().enabled = false;
                    balance += evenValue;
                    evenValue = 0;
                    lastused.transform.GetChild(1).GetComponent<Text>().text = "0";
                    lastflag = "";
                    break;
                case "odd":
                    lastused.transform.GetChild(0).GetComponent<Image>().enabled = false;
                    balance += oddValue;
                    oddValue = 0;
                    lastused.transform.GetChild(1).GetComponent<Text>().text = "0";
                    lastflag = "";
                    break;
                case "black":
                    lastused.transform.GetChild(0).GetComponent<Image>().enabled = false;
                    balance += blackValue;
                    blackValue = 0;
                    lastused.transform.GetChild(1).GetComponent<Text>().text = "0";
                    lastflag = "";
                    break;
                case "red":
                    lastused.transform.GetChild(0).GetComponent<Image>().enabled = false;
                    balance += redValue;
                    redValue = 0;
                    lastused.transform.GetChild(1).GetComponent<Text>().text = "0";
                    lastflag = "";
                    break;
            }
            UpdateUi();
        }

        public IEnumerator TakeBlinkAnim()
        {
            // takeBtn.interactable = false;
            // yield return new WaitForSeconds(0.5f);
            // takeBtn.interactable = true;
            // yield return new WaitForSeconds(0.5f);
            // StartCoroutine(TakeBlinkAnim());
            if (_playblinkAnim == true)
            {
                takeBtn.interactable = false;
                yield return new WaitForSeconds(0.5f);
                takeBtn.interactable = true;
                yield return new WaitForSeconds(0.5f);
                StartCoroutine(TakeBlinkAnim());
            }
        }

        public void StopTakeBlink()
        {
            _playblinkAnim = false;
            StopCoroutine(TakeBlinkAnim());
        }

        public IEnumerator BetBlinkAnim()
        {
            //betOkBtn.transform.GetChild(0).gameObject.SetActive(true);
            if (!isBetConfirmed)
            {
                betOkBtn.transform.GetChild(0).GetComponent<Image>().enabled = true;
                yield return new WaitForSeconds(0.5f);
                betOkBtn.transform.GetChild(0).GetComponent<Image>().enabled = false;
                yield return new WaitForSeconds(0.5f);
                StartCoroutine(BetBlinkAnim());
            }

        }
        public void StopRepeatBlink()
        {
            //_playblinkAnim = false;
            StopCoroutine(RepeatBlinkAnim());
        }
        public IEnumerator RepeatBlinkAnim()
        {
            //betOkBtn.transform.GetChild(0).gameObject.SetActive(true);
            if (!isBetConfirmed && !zoomed)
            {
                repeatbtn.transform.GetChild(0).GetComponent<Image>().enabled = true;
                yield return new WaitForSeconds(0.5f);
                repeatbtn.transform.GetChild(0).GetComponent<Image>().enabled = false;
                yield return new WaitForSeconds(0.5f);
                StartCoroutine(RepeatBlinkAnim());
            }

        }
        public IEnumerator TakeBlink()
        {
            takeBtn.transform.GetChild(1).GetComponent<Image>().enabled = true;
            yield return new WaitForSeconds(1f);
            takeBtn.transform.GetChild(1).GetComponent<Image>().enabled = false;
            //takeBtn.transform.GetChild(1).gameObject.SetActive(false);
            yield return new WaitForSeconds(1f);
            StartCoroutine(TakeBlink());
            // if(!taken)
            // {
            //     //StartCoroutine(BetBlinkAnim());
            // }


        }
        public void StopBetBlinkAnim()
        {
            StopCoroutine(BetBlinkAnim());
        }

        public void StopBetBlink()
        {
            _playblinkAnim = false;
            StopCoroutine(BetBlinkAnim());
        }

        public void Anything()
        {
            StartCoroutine(BetsBlink(-1));
        }

        IEnumerator BetsBlink(int _winno)
        {
            Debug.LogError("Win No from Bet Blink: " + _winno);
            yield return new WaitForSeconds(1f);

            if (!added && !isBetConfirmed && timernow>10)
            {
                BetsNumber[_winno].transform.parent.GetComponent<Image>().enabled = true;
            }
            else
            {
                BetsNumber[_winno].transform.parent.GetComponent<Image>().enabled = false;
            }
            yield return new WaitForSeconds(1f);
            if (!added && !isBetConfirmed && timernow>10)
            {
                BetsNumber[_winno].transform.parent.GetComponent<Image>().enabled = false;
            }
            yield return new WaitForSeconds(1f);
            if (!added && !isBetConfirmed && timernow>10)
            {
                BetsNumber[_winno].transform.parent.GetComponent<Image>().enabled = true;
            }
            else
            {
                BetsNumber[_winno].transform.parent.GetComponent<Image>().enabled = false;
            }
            yield return new WaitForSeconds(1f);
            if (!added && !isBetConfirmed && timernow>10)
            {
                BetsNumber[_winno].transform.parent.GetComponent<Image>().enabled = false;
            }
            yield return new WaitForSeconds(1f);
            if (!added && !isBetConfirmed && timernow>10)
            {
                BetsNumber[_winno].transform.parent.GetComponent<Image>().enabled = false;
            }
            else
            {
                BetsNumber[_winno].transform.parent.GetComponent<Image>().enabled = false;
            }
            if (!added && !isBetConfirmed && timernow>10)
            {
                StartCoroutine(BetsBlink(_winno));

            }

        }
        private string SaveFilePath
        {
            get { return Application.persistentDataPath + "/roulette.nms"; }
        }

        public void close()
        {
            Debug.Log("Closing FunRoulette");
            //sceneHandler.unloadAddressableScene();
            //Roulette_ServerResponse.instance.socketoff();
            // if(totalBets>0)
            // {

            // }

            if (isBetConfirmed || int.Parse(WinText.text) > 0)
            {
                store();
                storewithin();
                File.Delete(SaveFilePath);
                savebinary.savefunctionroulette();

            }

            if (!isBetConfirmed)
            {
                if(totalBets == 0)
                {
                    SendBets();
                }
                Debug.Log("bet not confirmed");
                Roulettewithin.betconfirmed = false;
            }
            
            Roulettewithin.winnervalue = int.Parse(WinText.text);



            PlayerPrefs.SetString("roulettepaused", "false");
            Debug.Log("roundcount" + PlayerPrefs.GetInt("rouletterounds"));
            PlayerPrefs.SetInt("Winno", int.Parse(WinText.text));
            //StartCoroutine(settingoffline());
            PlayerPrefs.SetInt("rouletterounds", Roulette_ServerResponse.instance.rouletterounds);
            //savebinary.LoadPlayer();
            Roulette_ServerRequest.intance.socket.Emit(Utility.Events.onleaveRoom);
            //SceneManager.LoadScene("MainScene");
        }

        [SerializeField] List<GameObject> dSelecteds = new List<GameObject>();

        public void selector(int coinvalue)//used to enlarge the selected coins
        {
            for (int i = 0; i < dSelecteds.Count; i++)
            {
                if (i == coinvalue)
                {
                    //selected.transform.position = coins[i].transform.position;
                    //Debug.Log("HIGHLIGHT AT: "+selected.transform.position);

                    dSelecteds[i].SetActive(true);

                    //d_selected.transform.position = coins_middle[i].transform.position;
                    //m_selector(i);
                    //coins[i].gameObject.SetActive(false);
                    //coins[i].localScale = new Vector3(1.3f,1.3f,1);
                }
                else
                {
                    dSelecteds[i].SetActive(false);
                }
            }
        }
        public void m_selector(int coinvalue)//used to enlarge the selected coins
        {
            for (int i = 0; i < coins_middle.Length; i++)
            {
                if (i == coinvalue)
                {


                    selector(i);
                }
            }
        }
        public void textcolor()
        {
            for (int i = 0; i < PreviousWin_Text.Count; i++)
            {
                int number = int.Parse(PreviousWin_Text[i].text);
                if ((number == 1) || (number == 3) || (number == 7) || (number == 9) || (number == 5) || (number == 12) || (number == 14) || (number == 16) || (number == 18) || (number == 19) || (number == 21) || (number == 23) || (number == 25) || (number == 27) || (number == 30) || (number == 32) || (number == 34) || (number == 36))
                {
                    PreviousWin_Text[i].color = new Color32(255, 0, 0, 255);
                }
                else if ((number == 0) || (number == 00))
                {
                    PreviousWin_Text[i].color = new Color32(0, 255, 0, 255);
                }
                else
                {
                    PreviousWin_Text[i].color = new Color32(255, 255, 255, 255);
                }
            }
            //if((number ==1)|| (number ==3)||(number ==7)||(number ==9)||(number ==5)||(number ==12)||(number ==14)||(number ==16)||(number ==18)||(number ==19)||(number ==21)||(number ==23)||(number ==25)||(number ==27)||(number ==30)||(number ==32)||(number ==34)||(number ==36))

        }
        public class Take_Bet
        {
            public string playerId;
            public int winpoint;
        }

        public class DiceWinNos
        {
            public List<string> previousWin_single;
            public int RoundCount;
            public object winNo;
            public int winPoint;
        }

        public class TimerClass
        {
            public int result;//timer
        }

        [Serializable]
        public class bets
        {
            public string playerId;
            public int points;
            public int[] straightUp;
            public int[,] Split;
            public int[,] Street, Corner, line;
            public int[] straightUpVal, SplitVal, StreetVal, CornerVal, specificBetVal, specificBet, lineVal;
            public int[] dozen1, dozen2, dozen3, column1, column2, column3, onetoEighteen, nineteentoThirtysix, even, odd, black, red;
            public int totalstraightUpVal, totalSplitVal, totalCornerVal, totalStreetVal, totalspecificBetVal, totallineVal, totaldozen1Val, totaldozen2Val, totaldozen3Val, totalcolumn1Val, totalcolumn2Val, totalcolumn3Val, totalonetoEighteen, totalnineteentoThirtysix,
            totalevenVal, totalodd, totalblackVal, totalredVal;
            public int dozen1Val, dozen2Val, dozen3Val, column1Val, column2Val, column3Val, onetoEighteenVal, nineteentoThirtysixVal,
            evenVal, oddVal, blackVal, redVal;
            public int Bet0, Bet1, Bet2, Bet3, Bet4, Bet5, Bet6, Bet7, Bet8, Bet9, Bet10, Bet11, Bet12, Bet13, Bet14, Bet15, Bet16, Bet17,
            Bet18, Bet19, Bet20, Bet21, Bet22, Bet23, Bet24, Bet25, Bet26, Bet27, Bet28, Bet29, Bet30, Bet31, Bet32, Bet33, Bet34, Bet35, Bet36, Bet00;
        }

        public void zoom_function()
        {
            if (OnZoom) // this is true
            {
                OnZoom = false;
                _wheelZoom_Left.transform.GetChild(1).GetComponent<Text>().text = "OFF";
                _wheelZoom_Right.transform.GetChild(1).GetComponent<Text>().text = "OFF";
                _wheelZoom_transition.transform.GetChild(1).GetComponent<Text>().text = "OFF";
                Camera.main.GetComponent<Animation>().enabled = false;
            }
            else // this is false
            {
                OnZoom = true;
                _wheelZoom_Left.transform.GetChild(1).GetComponent<Text>().text = "ON";
                _wheelZoom_Right.transform.GetChild(1).GetComponent<Text>().text = "ON";
                _wheelZoom_transition.transform.GetChild(1).GetComponent<Text>().text = "ON";
                //_wheelZoom_transition.transform.GetChild(1).GetComponent<Text>()
                Camera.main.GetComponent<Animation>().enabled = true;
            }
        }
    }
}

namespace RouletteClasses
{
    public class BetConfirmation
    {
        public string status;
        public string message;
    }
}
public class BetResponce
{
    public int status;
    public string message;
    public DataforBet data;
}
public class DataforBet
{
    public string playerId;
    public long balance;
}
public class details
{
    public string playerId;
}
public class SendBet_Res
{
    public int status;
    public string message;
    public SendBet_Res_Data data;
}

public class SendBet_Res_Data
{
    public float balance;
}
public class RoulettePrevious
{
    public int status ;
    public string message;
    public RoulettePreviousBet data;

}
public class RoulettePreviousBet
{
     public int[] straightUp;
    public int[,] Split;
    public int[,] Street, Corner, line;
    public int[] straightUpVal, SplitVal, StreetVal, CornerVal, specificBetVal, specificBet, lineVal;
    public int[] dozen1, dozen2, dozen3, column1, column2, column3, onetoEighteen, nineteentoThirtysix, even, odd, black, red;
    public int totalstraightUpVal, totalSplitVal, totalCornerVal, totalStreetVal, totalspecificBetVal, totallineVal, totaldozen1Val, totaldozen2Val, totaldozen3Val, totalcolumn1Val, totalcolumn2Val, totalcolumn3Val, totalonetoEighteen, totalnineteentoThirtysix,
    totalevenVal, totalodd, totalblackVal, totalredVal;
    public int dozen1Val, dozen2Val, dozen3Val, column1Val, column2Val, column3Val, onetoEighteenVal, nineteentoThirtysixVal,
    evenVal, oddVal, blackVal, redVal;
}