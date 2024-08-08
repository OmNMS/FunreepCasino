using System;
using System.Collections;
using UnityEngine;
using Titli.Utility;
using Newtonsoft.Json;
using System.Collections.Generic;
using Titli.UI;
using Titli.ServerStuff;
using UnityEngine.UI;
using KhushbuPlugin;

namespace Titli.Gameplay
{
    public class Titli_Timer : MonoBehaviour
    {
        public static Titli_Timer Instance;
        int bettingTime = 25;
        int timeUpTimer = 7;
        int waitTimer = 3;
        public Action onTimeUp;
        public Action onCountDownStart;
        public Action startCountDown;
        public static gameState gamestate;
        [SerializeField] Text countdownTxt;
        [SerializeField] Text messageTxt;

        IEnumerator countDown;
        IEnumerator onTimeUpcountDown;
        IEnumerator onWaitcountDown;
        public void StartCoundown() => StartCoroutine(countDown);
        public IEnumerator Stop_CountDown;
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
            onTimeUp();
            if(is_a_FirstRound)
            {
                Titli_CardController.Instance._winNo = true;
                Titli_UiHandler.Instance.ShowMessage("please wait for next round...");
                for(int i = 0; i < Titli_CardController.Instance.TableObjs.Count; i++)
                {
                    Titli_CardController.Instance.TableObjs[i].GetComponent<BoxCollider2D>().enabled = false;
                }
            }
        }

        //this will run once it connected to the server
        //it will carry the time and state of server
        IEnumerator Countdown(int time = -1)
        {
            Titli_UiHandler.Instance.DoubleUp.interactable = true;
            Titli_UiHandler.Instance.repeatBtn.interactable = true;
            Titli_UiHandler.Instance.clearBtn.interactable = true;
            Titli_UiHandler.Instance.SpinBtn.interactable = true;
            onCountDownStart?.Invoke();
            gamestate = gameState.canBet;
            for (int i = time != -1 ? time : bettingTime; i >= 0; i--)
            {
                if ( i < 4)
                {
                    Titli_UiHandler.Instance.ExitBlock = true;
                }
                else
                {
                    Titli_UiHandler.Instance.ExitBlock = false;
                }
                if(i == 1)
                {
                    
                    Titli_UiHandler.Instance.sendbets();
                    Titli_UiHandler.Instance.DoubleUp.interactable = false;
                    Titli_UiHandler.Instance.repeatBtn.interactable = false;
                    Titli_UiHandler.Instance.clearBtn.interactable = false;
                    Titli_UiHandler.Instance.SpinBtn.interactable = false;
                    //call betsplaced here;
                }
                if (i == 1)
                {
                    startCountDown?.Invoke();
                }
                if(i == 0)
                {
                    // OnWait();
                }
                messageTxt.text = "Start Time";
                countdownTxt.text = i.ToString();
                yield return new WaitForSecondsRealtime(1f);
            }
            Titli_CardController.Instance._canPlaceBet = true;
            // Titli_ServerResponse.Instance.TimerUpFunction();
            onTimeUp?.Invoke();

        }

        //Local Timer Start countdown
        /*IEnumerator TimerStartCountDown(int timer = 25)
        {
            Debug.Log("timer count down start");
            gamestate = gameState.canBet;
            Titli_UiHandler.Instance.DoubleUp.interactable = true;
            Titli_UiHandler.Instance.repeatBtn.interactable = true;
            Titli_UiHandler.Instance.clearBtn.interactable = true;
            Titli_UiHandler.Instance.SpinBtn.interactable = true;
            // Titli_CardController.Instance._winNo = true;
            for(int i = timer; i >= 0; i--)
            {
                if (i == 1)
                {
                    startCountDown?.Invoke();
                }
                messageTxt.text = "Bettting Time";
                countdownTxt.text = i.ToString();
                yield return new WaitForSecondsRealtime(1f);
            }
            Titli_CardController.Instance._canPlaceBet = true;
            StartCoroutine(TimeUpCountDown());
            onTimeUp?.Invoke();
        }*/
        IEnumerator TimpUpCountdown(int time = -1)
        {
            gamestate = gameState.cannotBet;
            onTimeUp?.Invoke();
            Titli_CardController.Instance._startCardBlink = true;   
            Titli_CardController.Instance._canPlaceBet = false;
            /*Titli_UiHandler.Instance.DoubleUp.interactable = false;
            Titli_UiHandler.Instance.repeatBtn.interactable = false;
            Titli_UiHandler.Instance.clearBtn.interactable = false;
            Titli_UiHandler.Instance.SpinBtn.interactable = false;*/
            foreach(var item in Titli_CardController.Instance._cardsImage)
            {
                item.GetComponent<Button>().interactable = false;
            }
            //StartCoroutine(Titli_CardController.Instance.CardsBlink());

            for (int i = time != -1 ? time : timeUpTimer; i >= 0; i--)
            {
                messageTxt.text = "Result Time";
                countdownTxt.text = i.ToString();
                if(i == 0)
                {
                    Titli_UiHandler.Instance.ShowMessage("Starting Next round");
                }
                yield return new WaitForSecondsRealtime(1f);
            }

        }

        //Local Timer timeUp Countdown
        /*IEnumerator TimeUpCountDown(int timer = 5)
        {
            Debug.Log("Timeup countdown");
            gamestate = gameState.cannotBet;
            onTimeUp?.Invoke();
            Titli_UiHandler.Instance.DoubleUp.interactable = false;
            Titli_UiHandler.Instance.repeatBtn.interactable = false;
            Titli_UiHandler.Instance.clearBtn.interactable = false;
            Titli_UiHandler.Instance.SpinBtn.interactable = false;
            Titli_CardController.Instance._startCardBlink = true;
            Titli_CardController.Instance._canPlaceBet = false;
            foreach(var item in Titli_CardController.Instance._cardsImage)
            {
                item.GetComponent<Button>().interactable = false;
            }
            Titli_ServerResponse.Instance.OnWinFunction();
            Titli_CardController.Instance.CardBlink_coroutine = Titli_CardController.Instance.CardsBlink();
            StartCoroutine(Titli_CardController.Instance.CardsBlink());
            // Titli_CardController.Instance._winNo = true;
            for(int i = timer; i >= 0; i--)
            {
                if(i == 2)
                {
                    Titli_CardController.Instance._winNo = true;
                    // Titli_ServerResponse.Instance.OnWinFunction();
                }
                messageTxt.text = "Time Up";
                countdownTxt.text = i.ToString();
                yield return new WaitForSecondsRealtime(2f);
            }
        }*/

        IEnumerator WaitCountdown(int time = -1)
        {
            Debug.Log("WaitCountDown");
            gamestate = gameState.wait;
            Titli_CardController.Instance._canPlaceBet = false;
            for (int i = time != -1 ? time : waitTimer; i >= 0; i--)
            {
                messageTxt.text = "Wait Time";
                countdownTxt.text = i.ToString();
                if(i ==0)
                {
                    // OnTimeUp();
                    // winningid user = new winningid()
                    // {
                    //     playerId  = PlayerPrefs.GetString("email")
                    // };
                    // Titli_ServerRequest.instance.socket.Emit(Events.OnWinNo,new JSONObject(JsonConvert.SerializeObject(user)));
                    Debug.Log("onwin idhar call hoga");
                }
                yield return new WaitForSecondsRealtime(1f);
            }

        }


        public void OnTimerStart()//(object data)
        {
            if (is_a_FirstRound)
            {
                Titli_UiHandler.Instance.HideMessage();
            }
            is_a_FirstRound = false;
            Titli_CardController.Instance._startCardBlink = false;
            Titli_CardController.Instance._canPlaceBet = true;
            for(int i = 0; i < Titli_CardController.Instance.TableObjs.Count; i++)
            {
                Titli_CardController.Instance.TableObjs[i].GetComponent<BoxCollider2D>().enabled = true;
            }

            StopCoroutines();
            StartCoroutine(Countdown());
        }

        /*public void OnTimerStart()
        {
            if (is_a_FirstRound)
            {
                Titli_UiHandler.Instance.HideMessage();
            }
            is_a_FirstRound = false;
            Titli_CardController.Instance._startCardBlink = false;
            Titli_CardController.Instance._canPlaceBet = true;

            StopCoroutines();
            Stop_CountDown = TimerStartCountDown();
            Debug.Log("timer start");
            // StartCoroutine(TimerStartCountDown());
            StartCoroutine(Stop_CountDown);
            // StartCoroutine(Countdown());
        }*/

        public void OnTimeUp()//(object data)
        {
            StopCoroutines();
            // winningid user = new winningid()
            // {
            //     playerId  = PlayerPrefs.GetString("email")
            // };
            // Titli_ServerRequest.instance.socket.Emit(Events.OnWinNo,new JSONObject(JsonConvert.SerializeObject(user)));
            if (is_a_FirstRound) return;
            Titli_CardController.Instance._canPlaceBet = false;
            for(int i = 0; i < Titli_CardController.Instance.TableObjs.Count; i++)
            {
                Titli_CardController.Instance.TableObjs[i].GetComponent<BoxCollider2D>().enabled = false;
                if(i == 0)
                {
                    // OnTimerStart();
                }
            }
            StopCoroutines();
            StartCoroutine(TimpUpCountdown());
        }

        // public void OnTimeUp()
        // {
        //     if (is_a_FirstRound) return;
        //     Titli_CardController.Instance._canPlaceBet = false;
        //     StopCoroutines();
        //     StopCoroutine(Stop_CountDown);
        //     StartCoroutine(TimpUpCountdown());
        //     //StartCoroutine(TimeUpCountdown())
        // }

        public void OnWait()//(object data)
        {            
            StopCoroutines();
            // StartCoroutine(StartDragonAnim());           
            if (is_a_FirstRound) return;
            // StartCoroutine(WOF_UiHandler.Instance.StartImageAnimation());
            StopCoroutines();
            StartCoroutine(WaitCountdown());
        }
        public bool is_a_FirstRound = true;
        public void OnCurrentTime(object data)
        {
            int trial = 0;
            is_a_FirstRound = true;
            //onTimeUp();
            Titli_CardController.Instance._winNo = true;
            Debug.Log("please wait for next round...");
            Titli_UiHandler.Instance.ShowMessage("please wait for next round...");
            
            try
            {
                InitialData cr = JsonConvert.DeserializeObject<InitialData>(data.ToString());
                //Titli_UiHandler.Instance.UpDateBalance(float.Parse(cr.balance));
                trial = cr.gametimer;
                for (int i =trial; i >= 0; i--)
                {
                    countdownTxt.text = i.ToString();
                    messageTxt.text = "Please Wait";
                }
                // for (int i = trial != -1 ? tria : waitTimer; i >= 0; i--)
                // {
                    
                // }
                
            }
            catch (Exception e)
            {
                Debug.Log(e.Message);
            }
            // StartCoroutine(firsttime(trial));
        }
        IEnumerator firsttime(int number)
        {
            while(number>0)
            {
                Debug.Log("/////////////////////////////////////////////////////////");
                number--;
                yield return new WaitForSecondsRealtime(1f);
            }
            Debug.Log("HHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHH");
            OnTimerStart();
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
    }
    public class winningid  
    {
        public string playerId;
    }
}
