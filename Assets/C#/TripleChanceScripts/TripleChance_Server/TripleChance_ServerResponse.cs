using UnityEngine;
using SocketIO;
using TFU;
using TripleChance.Utility;
using TripleChance.GamePlay;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using Newtonsoft.Json;
using UnityEngine.UI;

namespace TripleChance.ServerStuff
{
    public class TripleChance_ServerResponse : TripleChance_SocketHandler
    {
        public TripleChance_ServerRequest serverRequest;
        public static TripleChance_ServerResponse instance;
        public bool on_current = false;
        public static bool TimerStart = false;
        public GameObject waitpanel;
        public Text roundcount;
        public bool fromtriplecurrent;
        private void Start()
        {
            instance = this;
            socket = GameObject.Find("SocketIOComponents").GetComponent<SocketIOComponent>();
            socket.On("open", OnConnected);
            socket.On("disconnected", OnDisconnected);
            // socket.On(Utility.Events.OnChipMove, OnChipMove);
            // socket.On(Utility.Events.OnGameStart, OnGameStart);
            // socket.On(Utility.Events.OnAddNewPlayer, OnAddNewPlayer);
            // socket.On(Utility.Events.OnPlayerExit, OnPlayerExit);
            socket.On(Utility.Events.OnTimerStart, OnTimerStart);
            // socket.On(Utility.Events.OnWait, OnWait);
            // socket.On(Utility.Events.OnTimeUp, OnTimerUp);
            socket.On(Utility.Events.OnCurrentTimer, OnCurrentTimer);
            socket.On(Utility.Events.OnWinNo, OnWinNo);
            socket.On(Utility.Events.OnWinAmount, OnWinAmount);
            socket.On(Triple_Util.TF_Events.OnTimeUp, OnTimerUp);
            socket.On(Triple_Util.TF_Events.OnBetsPlaced, OnBetsPlaced);
            socket.On(Triple_Util.TF_Events.onleaveRoom, OnLeaveRoom);
            // socket.On(Utility.Events.OnBotsData, OnBotsData);
            socket.On(Utility.Events.OnPlayerWin, OnPlayerWin);
            // socket.On(Utility.Events.OnHistoryRecord, OnHistoryRecord);
            serverRequest.JoinGame();
        }
        public void socketoff()
        {
            socket.Off("open", OnConnected);
            socket.Off("disconnected", OnDisconnected);
            // socket.On(Utility.Events.OnChipMove, OnChipMove);
            // socket.On(Utility.Events.OnGameStart, OnGameStart);
            // socket.On(Utility.Events.OnAddNewPlayer, OnAddNewPlayer);
            // socket.On(Utility.Events.OnPlayerExit, OnPlayerExit);
            socket.Off(Utility.Events.OnTimerStart, OnTimerStart);
            // socket.On(Utility.Events.OnWait, OnWait);
            // socket.On(Utility.Events.OnTimeUp, OnTimerUp);
            socket.Off(Utility.Events.OnCurrentTimer, OnCurrentTimer);
            socket.Off(Utility.Events.OnWinNo, OnWinNo);
            socket.Off(Utility.Events.OnWinAmount, OnWinAmount);
            socket.Off(Triple_Util.TF_Events.OnTimeUp, OnTimerUp);
            socket.Off(Triple_Util.TF_Events.OnBetsPlaced, OnBetsPlaced);
            socket.Off(Triple_Util.TF_Events.onleaveRoom, OnLeaveRoom);
            // socket.On(Utility.Events.OnBotsData, OnBotsData);
            socket.Off(Utility.Events.OnPlayerWin, OnPlayerWin);
        }
        void OnConnected(SocketIOEvent e)
        {
            print("connected");
            isConnected = true;
            // serverRequest.JoinGame();
        }
        void OnLeaveRoom(SocketIOEvent e)
        {
            Debug.Log("left the room");
            socketoff();
            // if (PlayerPrefs.GetInt("tthrownout",0) == 0)
            // {
            //     SceneManager.LoadScene("MainScene");
            // }
            // else{
            //     SceneManager.LoadScene("Login");
            // }
            //////
            if(PlayerPrefs.GetInt("treload") ==1)
            {
                PlayerPrefs.SetInt("treload",0);
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);

            }
            else
            {
                if (PlayerPrefs.GetInt("tthrownout") == 0)
                {
                    SceneManager.LoadScene("MainScene");
                }
                
                else
                {
                    SceneManager.LoadScene("Login");
                }
            }
            //SceneManager.LoadScene("MainScene");
        }
        void OnWinAmount(SocketIOEvent e)
        {
            Debug.Log("OnWinAmount Collected" + e.data);
        }
        void OnDisconnected(SocketIOEvent e)
        {
            print("disconnected");
            isConnected = false;
        }
        void OnChipMove(SocketIOEvent e)
        {
            // PokerKing_ChipController.Instance.OnOtherPlayerMove((object)e.data);
        }

        void OnBotsData(SocketIOEvent e)
        {
            // PokerKing_BetsHandler.Instance.AddBotsData(e.data);
        }
        void OnBetsPlaced(SocketIOEvent e)
        {
            Debug.Log("OnBetsPlaced" + e.data);
            TripleFunScreen.Instance.SendBets_Response(e.data.ToString());
            // PokerKing_BetsHandler.Instance.AddBotsData(e.data);
        }

        void OnWinNo(SocketIOEvent e)
        {
            Debug.Log("OnWIn " +e);
            StartCoroutine(TripleFunScreen.Instance.OnRoundEnd(e.data));
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
            Debug.Log("Timer Start" +e);
            TripleFunScreen.Instance.canPlaceBet=true;
            TripleFunScreen.Instance.OnTimerStart(e.data.ToString());
            TripleFunScreen.Instance.Enable_OverrideSorting();
            TimerStart = true;
            //waitpanel.SetActive(false);
            tripleround++;
            roundcount.text = tripleround.ToString();
            // Debug.LogError("on timer start " + e.data);
            // PokerKing_Timer.Instance.OnTimerStart((object)e.data);
        }

        void OnTimerUp(SocketIOEvent e)
        {
            Debug.Log("on timeUp " + e.data);
            Debug.Log("on timeUp ");
            // string playerName = "RL" + PlayerPrefs.GetString("email");
            // Info data = new Info{
            //     playerId = playerName
            // };
            // socket.Emit(Triple_Util.TF_Events.OnWinNo,new JSONObject(JsonConvert.SerializeObject(data)));
            //TripleFunScreen.Instance.BettingButtonInteractablity(false);
            TripleFunScreen.Instance.isTimeUp = true;
            
            // PokerKing_Timer.Instance.OnTimeUp((object)e.data);
        }
        void OnWait(SocketIOEvent e)
        {
            Debug.Log("on wait " + e.data);
            // PokerKing_Timer.Instance.OnWait((object)e.data);
        }
        public int tripleround;
        void OnCurrentTimer(SocketIOEvent e)
        {
            if (on_current)
            {
                return;
            }
            else
            {
                on_current = true;
                fromtriplecurrent = true;
                Debug.Log("currunt data " + e.data);
                CurrentTimer val = JsonUtility.FromJson<CurrentTimer>(e.data.ToString());
                // Debug.LogError("val  " + val.gametimer);
                TripleFunScreen.Instance.TripleBetting_Table.GetComponent<Canvas>().overrideSorting = true;
                TripleFunScreen.Instance.RighteShadow_panel.GetComponent<Canvas>().overrideSorting = true;
                tripleround = val.RoundCount;
                // if(val.gametimer < 2)
                // {
                //     waitpanel.SetActive(true);
                // }
                // else
                // {
                //     waitpanel.SetActive(false);
                // }
                PlayerPrefs.SetInt("tthrownout",0);  
                if (PlayerPrefs.GetString("triplepaused") == "true")
                {
                    Debug.Log("kkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkk");
                    if(PlayerPrefs.GetInt("tripletrounds") !=val.RoundCount )
                    {
                        PlayerPrefs.SetString("triplepaused","false");
                        TripleFunScreen.Instance.throwout();
                    }
                }
                TripleFunScreen.Instance.triplerestoration();
                TripleFunScreen.Instance.DoubleBetting_Table.GetComponent<Canvas>().overrideSorting = true;
                TripleFunScreen.Instance.LeftShadow_panel.GetComponent<Canvas>().overrideSorting = true;
                StartCoroutine(TripleFunScreen.Instance.Timer(val.gametimer));
                TripleFunScreen.Instance.setprevious(val.previousWins,val.previousWinsDouble,val.previousWinsTriple);
                roundcount.text = val.RoundCount.ToString();
            }
            // PokerKing_BotsManager.Instance.UpdateBotData(e.data);
            // PokerKing_RoundWinningHandler.Instance.SetWinNumbers(e.data);
            // PokerKing_Timer.Instance.OnCurrentTime((object)e.data);
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
        public List<int> previousWins;
        public List<int> previousWinsDouble;
        public List<int> previousWinsTriple;
        public int RoundCount;

    }
    public class Info{
        public string playerId;
    }
}
