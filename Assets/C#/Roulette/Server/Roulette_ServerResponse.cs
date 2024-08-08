using UnityEngine;
using SocketIO;
using Roulette.Utility;
using Roulette.Gameplay;
using System;
using UnityEngine.SceneManagement;
using Newtonsoft.Json;
using UnityEngine.UI;

namespace Roulette.ServerStuff
{
    public class Roulette_ServerResponse : Roulette_SocketHandler
    {
        public Roulette_ServerRequest serverRequest;
        public static Roulette_ServerResponse instance;
        public bool on_current = false;
        //public GameObject waitpanel;
        [SerializeField] GameObject wheel;
        public Text RoundCount;
        public bool fromroulettecurrent;
        private void Awake() 
        {
            socket = GameObject.Find("SocketIOComponents").GetComponent<SocketIOComponent>();
        }
        private void Start()
        {
            instance = this;
            socket.On("open", OnConnected);
            socket.On("disconnected", OnDisconnected);
            // socket.On(Utility.Events.OnChipMove, OnChipMove);
            // socket.On(Utility.Events.OnGameStart, OnGameStart);
            // socket.On(Utility.Events.OnAddNewPlayer, OnAddNewPlayer);
            // socket.On(Utility.Events.OnPlayerExit, OnPlayerExit);
            socket.On(Utility.Events.OnTimerStart, OnTimerStart);
            socket.On(Utility.Events.OnWait, OnWait);
            socket.On(Utility.Events.OnTimeUp, OnTimeUp);
            socket.On(Utility.Events.OnCurrentTimer, OnCurrentTimer);
            socket.On(Utility.Events.OnWinNo, OnWinNo);
            socket.On(Utility.Events.OnBetsPlaced, OnBetsPlaced);
            socket.On(Utility.Events.OnWinAmount, OnWinAmount);

            socket.On(Utility.Events.onleaveRoom, onleaveRoom);
            // socket.On(Utility.Events.OnBotsData, OnBotsData);
            // socket.On(Utility.Events.OnPlayerWin, OnPlayerWin);
            // socket.On(Utility.Events.OnHistoryRecord, OnHistoryRecord);
            serverRequest.JoinGame();
        }

        

        public void socketoff()
        {
            socket = GameObject.Find("SocketIOComponents").GetComponent<SocketIOComponent>();
            socket.Off("open", OnConnected);
            socket.Off("disconnected", OnDisconnected);
            // socket.On(Utility.Events.OnChipMove, OnChipMove);
            // socket.On(Utility.Events.OnGameStart, OnGameStart);
            // socket.On(Utility.Events.OnAddNewPlayer, OnAddNewPlayer);
            // socket.On(Utility.Events.OnPlayerExit, OnPlayerExit);
            socket.Off(Utility.Events.OnTimerStart, OnTimerStart);
            socket.Off(Utility.Events.OnWait, OnWait);
            socket.Off(Utility.Events.OnTimeUp, OnTimeUp);
            socket.Off(Utility.Events.OnCurrentTimer, OnCurrentTimer);
            socket.Off(Utility.Events.OnWinNo, OnWinNo);
            socket.Off(Utility.Events.OnWinAmount, OnWinAmount);
            socket.Off(Utility.Events.onleaveRoom, onleaveRoom);
            socket.Off(Utility.Events.OnBetsPlaced, OnBetsPlaced);
            // socket.On(Utility.Events.OnBotsData, OnBotsData);
            // socket.On(Utility.Events.OnPlayerWin, OnPlayerWin);
            // socket.On(Utility.Events.OnHistoryRecord, OnHistoryRecord);
        }
        void OnConnected(SocketIOEvent e)
        {
            print("connected");
            isConnected = true;
            // serverRequest.JoinGame();
        }
        void OnDisconnected(SocketIOEvent e)
        {
            print("disconnected");
            isConnected = false;
            SceneManager.LoadScene("MainScene");
        }
        void OnChipMove(SocketIOEvent e)
        {
            // PokerKing_ChipController.Instance.OnOtherPlayerMove((object)e.data);
        }
        void OnWinAmount(SocketIOEvent e)
        {
            Debug.Log("OnWinAmount"+e);
        }
        void OnBetsPlaced(SocketIOEvent e)
        {
            Debug.Log("OnBetsPlaced:"+e);
            RouletteScreen.Instance.SendBets_Response(e.data.ToString());
        }
        void onleaveRoom(SocketIOEvent e)
        {
            Debug.Log("leaving room Roullete"+e);
            socketoff();
            // if (PlayerPrefs.GetInt("rthrownout",0) == 0)
            // {
            //     SceneManager.LoadScene("MainScene");
            // }
            // else{
            //     SceneManager.LoadScene("Login");
            // }
            ///////////////////////////////////
            if(PlayerPrefs.GetInt("rreload") ==1)
            {
                PlayerPrefs.SetInt("rreload",0);
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);

            }
            else
            {
                if (PlayerPrefs.GetInt("rthrownout") == 0)
                {
                    SceneManager.LoadScene("MainScene");
                }
                
                else
                {
                    SceneManager.LoadScene("Login");
                }
            }
            // PokerKing_ChipController.Instance.OnOtherPlayerMove((object)e.data);
        }

        void OnBotsData(SocketIOEvent e)
        {
            // PokerKing_BetsHandler.Instance.AddBotsData(e.data);
        }

        void OnWinNo(SocketIOEvent e)
        {
            Debug.Log("OnWinNo   "+e.data);
            StartCoroutine(RouletteScreen.Instance.OnRoundEnd(e.data));
            // Debug.LogError("Onwin data  " + e.data);
            // WOF_RoundWinningHandler.Instance.OnWin(e.data);         //call this function when api is integrated
            // PokerKing_RoundWinningHandler.Instance.OnWin(e.data);
        }

        void OnGameStart(SocketIOEvent e)
        {
            Debug.Log("OnGameStart " + e.data);
            // PokerKing_ChipController.Instance.OnOtherPlayerMove((object)e.data);
        }
        void OnAddNewPlayer(SocketIOEvent e)
        {
            Debug.Log("OnAddNewPlayer " + e.data);
            // PokerKing_ChipController.Instance.OnOtherPlayerMove((object)e.data);
        }
        void OnPlayerExit(SocketIOEvent e)
        {
            Debug.Log("OnPlayerExit " + e.data);
            // PokerKing_ChipController.Instance.OnOtherPlayerMove((object)e.data);
        }


        void OnTimerStart(SocketIOEvent e)
        {
            Debug.Log("On Timer Start :" + e.data );
            RouletteScreen.Instance.OnTimerStart(e.data.ToString());
            //waitpanel.SetActive(false);
            rouletterounds++;
            RoundCount.text = rouletterounds.ToString();
            //wheel.SetActive(true);
            // Debug.LogError("on timer start " + e.data);
            // PokerKing_Timer.Instance.OnTimerStart((object)e.data);
        }

        
        void OnTimeUp(SocketIOEvent e)
        {
            Debug.Log("on timeUp " + e.data);
            RouletteScreen.Instance.isTimeUp = true;
            // string playerName = "RL" + PlayerPrefs.GetString("email");
            // Info data = new Info{
            //     playerId = playerName
            // };
            // Debug.Log("//////////////////////////// "+ data.playerId);
            // Roulette_ServerResponse.instance.socket.Emit(Utility.Events.OnWinNo, new JSONObject(JsonConvert.SerializeObject(data)));
            
            // PokerKing_Timer.Instance.OnTimeUp((object)e.data);
        }
        void OnWait(SocketIOEvent e)
        {
            Debug.Log("on wait " + e.data);
            // PokerKing_Timer.Instance.OnWait((object)e.data);
        }
        public int rouletterounds;
        void OnCurrentTimer(SocketIOEvent e)
        {
            if (on_current)
            {
                return;
            }
            else
            {
                on_current = true;
                fromroulettecurrent = true;
                Debug.Log("currunt data " + e.data);
                CurrentTimer val = JsonUtility.FromJson<CurrentTimer>(e.data.ToString());
                Debug.Log("val  " + val.gametimer);
                rouletterounds = val.RoundCount;
                PlayerPrefs.SetInt("rthrownout",0);  
                

                if(val.gametimer <2)
                {
                    //rouletterounds = val.RoundCount+1;
                    //waitpanel.SetActive(true);
                    //wheel.SetActive(false);
                }
                else 
                {
                    //waitpanel.SetActive(false);
                    //wheel.SetActive(true);
                    if ((PlayerPrefs.GetString("roulettepaused") == "true"))//||Roulettewithin.betconfirmed)
                    {
                        
                        if(PlayerPrefs.GetInt("rouletterounds") !=val.RoundCount )
                        {
                            PlayerPrefs.SetString("roulettepaused","false");  
                            RouletteScreen.Instance.throwout();
                        }
                    }
                }
                if(PlayerPrefs.GetInt("rouletterounds") !=val.RoundCount)
                {
                    StartCoroutine(RouletteScreen.Instance.roulettewinresponse());
                }
                else if(PlayerPrefs.GetInt("rouletterounds") ==val.RoundCount && PlayerPrefs.GetInt("Winno") >0)
                {
                    StartCoroutine(RouletteScreen.Instance.roulettewinresponse());
                }
                RouletteScreen.Instance.restoration();
                StartCoroutine(RouletteScreen.Instance.Timer(val.gametimer));
                if(val.gametimer <10)
                {
                    RouletteScreen.Instance.repeatbutton.interactable = false;
                }
                //RouletteScreen.Instance.ResetAllBets();
                StartCoroutine(RouletteScreen.Instance.startprevious(val.previousWins));
                RoundCount.text = val.RoundCount.ToString();

                RouletteScreen.Instance.turnonDisplay.SetActive(false);
                RouletteScreen.Instance.cam.SetActive(true); 
                RouletteScreen.Instance.eventhandler.SetActive(true);

                SceneManager.SetActiveScene(SceneManager.GetSceneByName("New_RouletteGame") );
                SceneManager.UnloadSceneAsync( SceneManager.GetSceneByName("MainScene") );

            }
        }
        void OnPlayerWin(SocketIOEvent e)
        {
            Debug.Log("win something " + e.data);
            // PokerKing_UiHandler.Instance.OnPlayerWin(e.data);
        }
        void OnHistoryRecord(SocketIOEvent e)
        {
            Debug.Log("OnHistoryRecord " + e.data); 
            // PokerKing_UiHandler.Instance.ShowHistoryGame(e.data);
        }
    }

    public class CurrentTimer
    {
        public int gametimer;
        public string[] previousWins;
        public int RoundCount;
    }
    public class Info{
        public string playerId;
    }
    
}
