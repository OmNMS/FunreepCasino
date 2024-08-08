using System.Collections.Generic;
using AndarBahar.Utility;
using Newtonsoft.Json;
using System;
using System.Collections;
using AndarBahar.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using AndarBahar.ServerStuff;

namespace AndarBahar.Gameplay
{
    public class AndarBahar_Timer : MonoBehaviour
    {
        public static AndarBahar_Timer instance;
        int bettingTime = 50;      //15;
        int timeUpTimer = 10;
        int waitTimer = 3;
        public Action onTimeUp;
        public Action onTimerStart;
        public Action onTimerEnd;
        public Action onWait;
        public Action startCountDown;
        public static gameState gamestate;
        [SerializeField] Text countdownTxt;
        [SerializeField] Text roundcount;
        [SerializeField] Button exitbtn;
        [SerializeField] GameObject countdowncircle;
        [SerializeField] GameObject rounds;

        IEnumerator countDown;
        IEnumerator onTimeUpcountDown;
        IEnumerator onWaitcountDown;
        public AndarBahar_UiHandler uiHandler;
        public Image yellowstripe;
        int countrounds;
        public void StartCoundown() => StartCoroutine(countDown);
        void Start()
        {
            instance = this;
            gamestate = gameState.cannotBet;
            countDown = Countdown();
            onTimeUpcountDown = TimpUpCountdown();
            // onWaitcountDown = WaitCountdown();
            onTimeUp?.Invoke();
        }

        //this will run once it connected to the server
        //it will carry the time and state of server
        float stripefill;

        public IEnumerator Countdown(int time = -1)
        {
            
            Debug.Log("Turn on timer circle");

            countdowncircle.SetActive(true);
            yellowstripe.color = new Color32(255,255,255,255);
            
            uiHandler.roundActive = true;
            onTimerStart?.Invoke();
            gamestate = gameState.canBet;
           
            uiHandler.ready = true;
            for (int i = time != -1 ? time : bettingTime; i >= 0; i--)
            {
                stripefill = yellowstripe.fillAmount;
                countdownTxt.text = "0:"+i.ToString();
                yellowstripe.fillAmount = stripefill-0.02f;
                if(i <10)
                {
                    yellowstripe.color = new Color32(255,0,0,255);
                }
                if (i == 2 ) uiHandler.roundActive = false;
                if(i == 5)
                {
                    Debug.Log("szexdcfvgbhnjmknjbhvgfcxzzxcvbhnjmkbvgcf");
                    AndarBahar_CardHandler.Instance._runbool = true;
                    AndarBahar_RoundWinnerHandlercs.Instance.nextrounddelete();
                    if (AndarBahar_RoundWinnerHandlercs.Instance.spinnow)
                    {
                        StartCoroutine(AndarBahar_CardHandler.Instance.Start_CardAnimation());
                    }
                    if(int.Parse(uiHandler.Total_Bets_Text.text) == 0 || uiHandler.Total_Bets_Text.text == null)
                    {
                        countrounds++;
                        if(countrounds >=3)
                        {
                            throwout();
                        }
                        
                    }
                    else
                    {
                        countrounds =0;
                    }
                    Debug.Log("throwout"+ countrounds);
                    //if(!uiHandler.isBetConfirmed)
                    //{
                    if(!uiHandler.isBetConfirmed)
                    {
                        uiHandler.OnBetsOk();
                    }
                    //uiHandler.SendBets();
                    //}
                    if (uiHandler.canPlaceBet)
                    {
                        uiHandler.WinText.text = "Bet Time Over";
                        
                    }
                }
                if(i == 2)
                {
                    
                    rounds.SetActive(false);
                }
                yield return new WaitForSecondsRealtime(1f);
            }
            onTimerEnd?.Invoke();

        }
        void throwout()
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.SetInt("athrownout",1);
            countrounds =0;
            AndarBahar.ServerStuff.AndarBahar_ServerRequest.instance.socket.Emit(Utility.Events.onleaveRoom);
            //SceneManager.LoadScene("Login");

        }
        IEnumerator TimpUpCountdown(int time = -1)
        {
            gamestate = gameState.cannotBet;
            onTimeUp?.Invoke();

            // for (int i = time != -1 ? time : timeUpTimer; i >= 0; i--)
            // {
            yield return new WaitForSecondsRealtime(1f);
            // }

        }
        // IEnumerator WaitCountdown(int time = -1)
        // {
        //     gamestate = gameState.wait;
        //     print("here");
            
        //     for (int i = time != -1 ? time : waitTimer; i >= 0; i--)
        //     {
        //         countdownTxt.text = i.ToString();
        //         yield return new WaitForSecondsRealtime(1f);
        //     }

        // }
        public int andarround;
        
        public void OnCurrentTime(object data)
        {
            Debug.Log("Please wait for next round");
            countdowncircle.SetActive(false);
            is_a_FirstRound = true;
            onTimeUp();
            // uiHandler.ShowMessage("please wait for next round...");

            JsonSerializerSettings settingsJson = new JsonSerializerSettings()
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore
            };
            var settings = new JsonSerializerSettings
            {
                Converters = new[] { new NullCharacterConverter() }
            };

            InitialData cr = new InitialData();
            try
            {
                cr = JsonConvert.DeserializeObject<InitialData>(data.ToString(), settings);
            }
            catch (Exception e)
            {
                Debug.Log(e);
            }
            Debug.Log("cccccccccccccccccccccccccccccccccccccccccccccccc"+cr.gametimer);
            yellowstripe.fillAmount = (cr.gametimer * 0.02f);
            andarround = cr.RoundCount;
            PlayerPrefs.SetInt("athrownout",0);  
            if (PlayerPrefs.GetString("andarpaused") == "true")
            {
                if(PlayerPrefs.GetInt("andarrounds") !=cr.RoundCount )
                {
                    PlayerPrefs.SetString("andarpaused","false");  
                    throwout();
                    //AndarBahar_UiHandler.Instance.throwout();
                }
            }
            Debug.Log("reached here");
            // AndarBahar.Gameplay.AndarBahar_RoundWinnerHandlercs.Instance.Start_Previouswin(cr.previous_Wins);
            if (cr.gametimer > 10)
            {
                uiHandler.HideMessage();
                //uiHandler.canPlaceBet = true;
                
                Debug.Log("Timer started" +uiHandler.canPlaceBet);
                // if (is_a_FirstRound) return;
                StartCoroutine(Countdown(cr.gametimer));
                
            }
            else if (cr.gametimer <= 10)
            {
                uiHandler.ShowMessage("Please Wait for next round");
                StartCoroutine(Countdown_before_round(cr.gametimer));
            }
            roundcount.text = cr.RoundCount.ToString();
            StartCoroutine(AndarBahar_RoundWinnerHandlercs.Instance.onwinresponse());
            AndarBahar_RoundWinnerHandlercs.Instance.started = true;

            
        }
        public void firsttimer(int value)
        {
            Debug.Log("Please wait for next round");
            countdowncircle.SetActive(false);
            is_a_FirstRound = true;
            //onTimeUp();
            uiHandler.ShowMessage("please wait for next round...");

            //InitialData cr = JsonConvert.DeserializeObject<InitialData>(data.ToString());
            //Debug.Log("cccccccccccccccccccccccccccccccccccccccccccccccc"+cr.gametimer);
            Debug.Log("reached here");
            // AndarBahar.Gameplay.AndarBahar_RoundWinnerHandlercs.Instance.Start_Previouswin(cr.previous_Wins);
            if (value > 10)
            {
                uiHandler.HideMessage();
                ///uiHandler.canPlaceBet = true;
                
                Debug.Log("Timer started" +uiHandler.canPlaceBet);
                // if (is_a_FirstRound) return;
                StartCoroutine(Countdown(value));
                
            }
            else if (value <= 10)
            {
                uiHandler.ShowMessage("Please Wait for next round");
                StartCoroutine(Countdown_before_round(value));
            }

            
        }

        IEnumerator Countdown_before_round(int time)
        {
            countdowncircle.SetActive(true);
            for (int i = time; i >= 0; i--)
            {
                countdownTxt.text = i.ToString();
                // Debug.Log("Countdown:"+i );
                yield return new WaitForSecondsRealtime(1f);
            }
        }

        public void OnTimerStart()
        {
            is_a_FirstRound = false;
            uiHandler.HideMessage();
            Debug.Log("Timer started");
            uiHandler.canPlaceBet = true;
            andarround++;
            uiHandler.isBetConfirmed = false;
            uiHandler.betOkBtn.interactable = true;
            uiHandler.TakeBet_Btn.interactable = true;
            if (is_a_FirstRound) return;
            // StopAllCoroutines();
            StartCoroutine(Countdown());
            yellowstripe.fillAmount =1;
            roundcount.text = andarround.ToString();
            exitbtn.interactable = true;
            rounds.SetActive(true);
        }

        public void OnTimeUp(object data)
        {
            if (is_a_FirstRound) return;
            // StopAllCoroutines();
            StartCoroutine(TimpUpCountdown());

        }

        // public void OnWait(object data)
        // {
        //     if (is_a_FirstRound) return;
        //     StopAllCoroutines();
        //     onWait?.Invoke();
        //     StartCoroutine(WaitCountdown());
        // }
        
        public bool is_a_FirstRound;
    }
}

public class InitialData
{
    public int gametimer;
    [JsonProperty(NullValueHandling =NullValueHandling.Include)]
    public List<int> previous_Wins;
    public int RoundCount;
}