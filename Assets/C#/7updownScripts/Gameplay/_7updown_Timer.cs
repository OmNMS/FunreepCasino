using System;
using System.Collections;
using UnityEngine;
using TMPro;
using UpDown7.Utility;
using Newtonsoft.Json;
using System.Collections.Generic;
using Updown7.UI;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Updown7.ServerStuff;

namespace Updown7.Gameplay
{
    public class _7updown_Timer : MonoBehaviour
    {
        public static _7updown_Timer Instance;
        int bettingTime = 40;       //30
        int timeUpTimer = 0;
        int waitTimer = 0;
        public Action onTimeUp;
        public Action onCountDownStart;
        public Action startCountDown;
        public static gameState gamestate;
        [SerializeField] TMP_Text countdownTxt;
        [SerializeField] Text stateTxt;
        
        [SerializeField] TMP_Text roundCount;
        
        [SerializeField] GameObject Canvas;
        [SerializeField] GameObject stopbetting;
        enum State
        {
            Betting,
            Drawing,
            Idle,

        }
        IEnumerator countDown;
        IEnumerator onTimeUpcountDown;
        IEnumerator onWaitcountDown;
        public Button exitbtn;
        public void StartCoundown() => StartCoroutine(countDown);

        private void Awake()
        {
            Instance = this;
        }

        void Start()
        {
            gamestate = gameState.cannotBet;
            countDown = Countdown();
            onTimeUpcountDown = TimpUpCountdown();
            onWaitcountDown = WaitCountdown();
            onTimeUp?.Invoke();
        }
        int countrounds;
        public int roundtimer;
        //this will run once it connected to the server
        //it will carry the time and state of server
        IEnumerator Countdown(int time = -1)
        {
            stopbetting.SetActive(false);
            onCountDownStart?.Invoke();
            Canvas.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceCamera;
            stateTxt.text = State.Betting.ToString();
            gamestate = gameState.canBet;
            if(time <2)
            {
                // if(ServerResponse.instance.fromsevencurrent)
                // {
                //     string playerName = "RL" + PlayerPrefs.GetString("email");
                // //  timer.((object)e.data);
                //     Info data = new Info{
                //         playerId = playerName
                //     };
                //     Debug.Log("//////////////////////////"+data.playerId);
                //     ServerRequest.instance.socket.Emit(Events.OnWinNo,new JSONObject(JsonConvert.SerializeObject(data)));
                //     exitbtn.interactable = false;
                //     ServerResponse.instance.fromsevencurrent = false;
                // }
            }
            else
            {
                ServerResponse.instance.fromsevencurrent = false;
            }
            for (int i = time != -1 ? time : bettingTime; i >= 0; i--)
            {
                roundtimer =i;
                // if(i == 2)
                // {
                //     string playerName = "RL" + PlayerPrefs.GetString("email");
                // //  timer.((object)e.data);
                //     Info data = new Info{
                //         playerId = playerName
                //     };
                //     Debug.Log("//////////////////////////"+data.playerId);
                //     ServerRequest.instance.socket.Emit(Events.OnWinNo,new JSONObject(JsonConvert.SerializeObject(data)));
                //     exitbtn.interactable = false;
                    
                //     //ServerResponse.instance.callonwin();
                // }
                if (i == 7)
                {
                    startCountDown?.Invoke();
                    stopbetting.SetActive(true);
                    gamestate = gameState.cannotBet;
                    _7updown_UiHandler.Instance.SendBets();
                }
                if (i == 10)
                {
                    _7updown_UiHandler.Instance.canPlaceBet = false;
                    if (!_7updown_UiHandler.Instance.isBetConfirmed)
                    {
                        if(_7updown_UiHandler.Instance.totalBets > 0)
                        {
                            Debug.Log("Bets ok");
                            //_7updown_UiHandler.Instance.OnBetsOk();              //to confirm bets...
                        }
                        else{
                            countrounds++;
                            Debug.Log("countrounds");
                            if(countrounds >=3)
                            {
                                _7updown_UiHandler.Instance.GotoLobby();
                                throwout();
                                //close();
                            }
                        }
                    }
                }
                
                
                countdownTxt.text = i.ToString();
                yield return new WaitForSecondsRealtime(1f);
            }
            onTimeUp?.Invoke();

        }
        void throwout()
        {
            PlayerPrefs.DeleteAll();
            ServerStuff.ServerRequest.instance.socket.Emit(Events.onleaveRoom);
            PlayerPrefs.SetInt("sthrownout",0);  
            
            //SceneManager.LoadScene("Login");
        }
        IEnumerator TimpUpCountdown(int time = -1)
        {
            stopbetting.SetActive(false);
            gamestate = gameState.cannotBet;
            Canvas.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;
            stateTxt.text = State.Drawing.ToString();
            onTimeUp?.Invoke();

            for (int i = time != -1 ? time : timeUpTimer; i >= 0; i--)
            {
                countdownTxt.text = i.ToString();
                yield return new WaitForSecondsRealtime(1f);
            }

        }
        IEnumerator WaitCountdown(int time = -1)
        {
            stateTxt.text = State.Idle.ToString();

            gamestate = gameState.wait;
            for (int i = time != -1 ? time : waitTimer; i >= 0; i--)
            {
                countdownTxt.text = i.ToString();
                yield return new WaitForSecondsRealtime(1f);
            }

        }


        public void OnTimerStart(object data)
        {
            _7updown_UiHandler.Instance.canPlaceBet = true;
            _7updown_UiHandler.Instance.isBetConfirmed = false;
            _7updown_UiHandler.Instance.betOkBtn.interactable = true;
            exitbtn.interactable = true;
            sevenround++;
            roundCount.text = sevenround.ToString();
            
            //Debug.Log(OnTimer)
            if (is_a_FirstRound)
            {
                // uiHandler.HideMessage();
                _7updown_UiHandler.Instance.HideMessage();
            }
            is_a_FirstRound = false;

            StopCoroutines();
            StartCoroutine(Countdown(50));
        }

        public void OnTimeUp(object data)
        {
            if (is_a_FirstRound) return;
            StopCoroutines();
            StartCoroutine(TimpUpCountdown());

        }

        public void OnWait(object data)
        {
            if (is_a_FirstRound) return;
            StopCoroutines();
            StartCoroutine(WaitCountdown());
        }

        public _7updown_UiHandler uiHandler;
        public bool is_a_FirstRound = true;
        int currnettimerr;
        public int sevenround;
        public bool started;
        public void OnCurrentTime(object data)
        {
            //Debug.Log("Please wait for next round");
            is_a_FirstRound = true;
            onTimeUp();
            // uiHandler.ShowMessage("please wait for next round...");
           // _7updown_UiHandler.Instance.ShowMessage("please wait for next round...");
            

            InitialData cr = JsonConvert.DeserializeObject<InitialData>(data.ToString());
            //var hiii = JsonConvert.DeserializeObject<InitialData>(data.ToString());
            //Debug.Log("hhhhhhhhhhhhhhhhhiiiiiiiiiiiiiiiiiiii" + cr.previousWins[0]);
            currnettimerr = cr.gametimer;
            sevenround = cr.RoundCount;
            started = true;
            PlayerPrefs.SetInt("sthrownout",0);  
            if (PlayerPrefs.GetString("sevenpaused") == "true")
            {
                if(PlayerPrefs.GetInt("sevenrounds") !=cr.RoundCount )
                {
                    PlayerPrefs.SetString("sevenpaused","false");  
                    throwout();
                }
            }

            if(cr.gametimer <5)
            {
                _7updown_UiHandler.Instance.ShowMessage("Please Wait For Next Round");
                Debug.Log("timerless");
            }
            else if(cr.gametimer > 5)
            {
                // _7updown_UiHandler.Instance.canPlaceBet = true;
                // _7updown_UiHandler.Instance.isBetConfirmed = false;
                //_7updown_UiHandler.Instance.betOkBtn.interactable = true;
                _7updown_UiHandler.Instance.HideMessage();
                  
            }
            if(PlayerPrefs.GetInt("sevenrounds") ==cr.RoundCount)
            {
                _7updown_UiHandler.Instance.restore();
                //StartCoroutine(FunTargetGamePlay.Instance.funtargetwinresponse());
                //StartCoroutine(_7updown_RoundWinningHandler.Instance.sevenonwinresponse());
            }
            if(PlayerPrefs.GetInt("sevenrounds") !=cr.RoundCount)
            {
                //StartCoroutine(FunTargetGamePlay.Instance.funtargetwinresponse());
                StartCoroutine(_7updown_RoundWinningHandler.Instance.sevenonwinresponse());
            }
            else if(PlayerPrefs.GetInt("sevenrounds") ==cr.RoundCount && PlayerPrefs.GetInt("sevenwin") >0)
            {
                //StartCoroutine(FunTargetGamePlay.Instance.funtargetwinresponse());
                StartCoroutine(_7updown_RoundWinningHandler.Instance.sevenonwinresponse());
            }
            StartCoroutine(Countdown(cr.gametimer));
            StartCoroutine(_7updown_RoundWinningHandler.Instance.sevenonwinresponse());
            roundCount.text = cr.RoundCount.ToString();
            //Debug.Log("tiiiiiiimmmmmmmmmmmmmeeeeeeeeeerrrrrrrrrrrr"+ cr.gametimer);
            // _7updown_UiHandler.Instance.UpDateBalance(float.Parse(cr.balance));
            
        }

        public void StopCoroutines()
        {
            StopCoroutine(Countdown());
            StopCoroutine(TimpUpCountdown());
            StopCoroutine(WaitCountdown());
        }
    }

    [Serializable]
    public class CurrentTimer
    {
        public gameState gameState;
        public int timer;
        public List<int> lastWins;
        public int LeftBets;
        public int MiddleBets;
        public int RightBets;
    }
    public enum gameState
    {
        canBet = 0,
        cannotBet = 1,
        wait = 2,
    }

    public class InitialData
    {
        public List<int> previousWins;
        public List<BotsBetsDetail> botsBetsDetails;
        public string balance;
        public int gametimer;
        public int RoundCount;
    }
    

}