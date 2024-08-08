using System.Net.Sockets;
using SocketIO;
using UnityEngine;
using AndarBahar.Gameplay;
using AndarBahar.Utility;
using AndarBahar.UI;
using Shared;
using UnityEngine.SceneManagement;
using Newtonsoft.Json;


namespace AndarBahar.ServerStuff
{
    public class AndarBahar_ServerResponse : AndarBahar_SocketHandler
    {
        public static AndarBahar_ServerResponse Instance;
        public GameObject mainUnit;
        // public SocketIOComponent socket;
        AndarBahar_Timer timer;
        public AndarBahar_UiHandler uiHandler;
        AndarBahar_ChipController chipController;
        AndarBahar_BotsManager botManager;
        AndarBahar_CardHandler cardHandler;
        public bool on_current = false;
        public bool fromandarcurrent;
        public  AndarBahar_ServerRequest ABserverRequest;
        private void Start()
        {
            Instance  =this;
            // socket = SocketIOComponent.instance;
            socket = GameObject.Find("SocketIOComponents").GetComponent<SocketIOComponent>();
            timer = mainUnit.GetComponent<AndarBahar_Timer>();
            chipController = mainUnit.GetComponent<AndarBahar_ChipController>();
            botManager = mainUnit.GetComponent<AndarBahar_BotsManager>();
            cardHandler = mainUnit.GetComponent<AndarBahar_CardHandler>();
            socketOn();
            socket.On("open", OnConnected);
            socket.On("disconnected", OnDisconnected);            
            // socket.On(Utility.Events.OnPlayerWin, OnPlayerWin);
            
            ABserverRequest.JoinGame();
            
        }

        void OnLeaveRoom(SocketIOEvent e)
        {
            Debug.Log("OnLevaeRoom:"+e.data );
            socketoff();
            // if (PlayerPrefs.GetInt("athrownout",0) == 0)
            // {
            //     SceneManager.LoadScene("MainScene");
            // }
            // else{
            //     SceneManager.LoadScene("Login");
            // }
            ////////////////
            if(PlayerPrefs.GetInt("areload") ==1)
            {
                PlayerPrefs.SetInt("areload",0);
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);

            }
            else
            {
                if (PlayerPrefs.GetInt("athrownout") == 0)
                {
                    SceneManager.LoadScene("MainScene");
                }
                
                else
                {
                    SceneManager.LoadScene("Login");
                }
            }
            //SceneManager.LoadScene("MainScene");
            //socketoff();
        }

        public void socketOn()
        {
            socket.On(Utility.Events.OnWait, OnWait);
            socket.On(Utility.Events.onleaveRoom, OnLeaveRoom);
            socket.On(Utility.Events.OnTimeUp, OnTimerUp);
            socket.On(Utility.Events.OnCurrentTimer, OnCurrentTimer);
            socket.On(Utility.Events.OnBotsData, OnBotsData);
            socket.On(Utility.Events.OnTimerStart, OnTimerStart);
            socket.On(Utility.Events.OnWinAmount, OnWinAmount);
            socket.On(Utility.Events.OnWinNo, OnWinNo);
            socket.On(Utility.Events.OnBetsPlaced, OnBetsPlaced );
            socket.On(Utility.Events.OnHistoryRecord, OnHistoryRecord);
        }
        public void socketoff()
        {
            socket.Off(Utility.Events.OnWait, OnWait);
            socket.Off(Utility.Events.OnTimeUp, OnTimerUp);
            socket.Off(Utility.Events.OnCurrentTimer, OnCurrentTimer);
            socket.Off(Utility.Events.OnBotsData, OnBotsData);
            socket.Off(Utility.Events.OnTimerStart, OnTimerStart);
            socket.Off(Utility.Events.OnWinAmount, OnWinAmount);
            socket.Off(Utility.Events.OnWinNo, OnWinNo);
            socket.Off(Utility.Events.OnBetsPlaced, OnBetsPlaced );
            socket.Off(Utility.Events.OnHistoryRecord, OnHistoryRecord);
            socket.Off(Utility.Events.onleaveRoom, OnLeaveRoom);
        }

        void OnBetsPlaced(SocketIOEvent e)
        {
            Debug.Log("OnBetsPlaced:" +e.data );
            AndarBahar_UiHandler.Instance.SendBets_Response((object)e.data);
        }
        void OnWinAmount(SocketIOEvent e)
        {
            Debug.Log("OnWinAmount:" +e.data );
            AndarBahar_UiHandler.Instance.Take_Bet_response((object)e.data);
        }

        void OnConnected(SocketIOEvent e)
        {
            print("connected");
            isConnected = true;
            
        }
        void OnDisconnected(SocketIOEvent e)
        {
            print("disconnected Andar bahar game");
            isConnected = false;
            uiHandler.BackButton();
        }
        //void OnChipMove(SocketIOEvent e)
        //{
        //    chipMovement.OnOtherPlayerMove((object)e.data);
        //}

        //void OnBotsData(SocketIOEvent e)
        //{
        //    botsController.AddBotsData(e.data);
        //}
        void OnWinNo(SocketIOEvent e)
        {
            Debug.Log("OnWin:" +e.data );
            AndarBahar.Gameplay.AndarBahar_RoundWinnerHandlercs.Instance.OnWin((object)e.data);
            // if (timer.is_a_FirstRound) return;
            // cardHandler.OnRoundDrawResult((object)e.data);
            //new WaitWhile( () => AndarBahar_UiHandler.Instance.roundActive);
            
        }

        //void OnGameStart(SocketIOEvent e)
        //{
        //    chipMovement.OnOtherPlayerMove((object)e.data);
        //}
        //void OnAddNewPlayer(SocketIOEvent e)
        //{
        //    chipMovement.OnOtherPlayerMove((object)e.data);
        //}
        // void OnPlayerExit(SocketIOEvent e)
        // {
        // }

        void OnBotsData(SocketIOEvent e)
        {
            // Debug.Log("OnBotsData:" +e.data );
            botManager.AddBotsBets(e.data);
        }
        void OnTimerStart(SocketIOEvent e)
        {
            Debug.Log("OnTimerStart:" +e.data );
            new WaitWhile(() => AndarBahar_UiHandler.Instance.roundActive);
            timer.OnTimerStart();
            // cardHandler.SetWinnerCard((object)e.data);
        }
        

        void OnTimerUp(SocketIOEvent e)
        {
            Debug.Log("OnTimerUp:" +e.data );
            // string playerName = "RL" + PlayerPrefs.GetString("email");
            // Info data = new Info{
            //     playerId = playerName
            // };
            // socket.Emit(Utility.Events.OnWinNo, new JSONObject(JsonConvert.SerializeObject(data)));
            //uiHandler.roundActive = false;
            timer.OnTimeUp((object)e.data);
        }
        void OnWait(SocketIOEvent e)
        {
            Debug.Log("on wait " +e.data );
            // AndarBahar_CardHandler.Instance.Stop_CharacterAnimation();
            // timer.OnWait((object)e.data);
        }
        /// <summary>
        /// the function contion the inital data that need to start the round
        /// like last 50 rounds wins
        /// bots name and balance
        /// </summary>
        /// <param name="e"></param>
        void OnCurrentTimer(SocketIOEvent e)
        {
            if (on_current)
            {
                return;
            }
            else
            {
                on_current = true;
                fromandarcurrent = true;
                Debug.Log("OnCurrentTImer:" +e.data );
                timer.OnCurrentTime(e.data);
                AndarBahar.Gameplay.AndarBahar_RoundWinnerHandlercs.Instance.Start_Previouswin(e.data);
            }
             //timer.OnCurrentTime((object)e.data);
            
            //timer.OnCurrentTime(e.data);
            
            // botManager.UpdateBotData((object)e.data);
            // uiHandler.UpdateDashboard((object)e.data);
            //roundWinningHandler.SetWinNumbers((object)e.data);
        }

        void OnHistoryRecord(SocketIOEvent e)
        {
            Debug.Log("OnHistoryRecord:"+e.data );
            // uiHandler.ShowHistory((object)e.data);
        }
        // private bool isConnected;

        // void OnPlayerWin(SocketIOEvent e)
        // {
        //     uiHandler.OnPlayerWin(e.data);
        // }
    }
    public class Info{
        public string playerId;
    }
}
