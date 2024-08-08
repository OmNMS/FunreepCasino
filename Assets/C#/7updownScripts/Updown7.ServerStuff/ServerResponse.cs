using UnityEngine;
using SocketIO;
using Updown7.Gameplay;
using UpDown7.Utility;
using Updown7.UI;
using UnityEngine.SceneManagement;
using Newtonsoft.Json;
using UnityEngine.UI;

namespace Updown7.ServerStuff
{
    class ServerResponse : SocketHandler
    {
        public GameObject mainUnit;
        IChipMovement chipMovement;
        public  ServerRequest serverRequest;
        public static ServerResponse instance;
        public bool on_current = false;
        public bool fromsevencurrent;
        
        private void Awake() 
        {
            instance = this;
        }
        private void Start()
        {
            socket = SocketIOComponent.instance;
           // socket = GameObject.Find("SocketIOComponents").GetComponent<SocketIOComponent>();
            chipMovement = mainUnit.GetComponent<IChipMovement>();
            socket.On("open", OnConnected);
            socket.On("disconnected", OnDisconnected);
            socket.On(Events.OnChipMove, OnChipMove);
            socket.On(Events.OnGameStart, OnGameStart);
            socket.On(Events.OnAddNewPlayer, OnAddNewPlayer);
            socket.On(Events.OnPlayerExit, OnPlayerExit);
            socket.On(Events.OnTimerStart, OnTimerStart);
            socket.On(Events.OnWait, OnWait);
            socket.On(Events.OnTimeUp, OnTimerUp);
            socket.On(Events.OnCurrentTimer, OnCurrentTimer);
            socket.On(Events.OnWinNo, OnWinNo);
            socket.On(Events.OnBotsData, OnBotsData);
            socket.On(Events.OnPlayerWin, OnPlayerWin);
            socket.On(Events.onleaveRoom, onLeaveRoom);
            socket.On(Events.OnBetsPlaced, OnBetsPlaced);
            socket.On(Events.OnWinAmount, OnWinAmount);
            serverRequest.JoinGame();
        }
        void onLeaveRoom(SocketIOEvent e)
        {
            Debug.Log("data" +e.data);
            socketoff();
            // if (PlayerPrefs.GetInt("sthrownout",0) == 0)
            // {
            //     SceneManager.LoadScene("MainScene");
            // }
            // else{
            //     SceneManager.LoadScene("Login");
            // }
            //////////////////
            if(PlayerPrefs.GetInt("sreload") ==1)
            {
                PlayerPrefs.SetInt("sreload",0);
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);

            }
            else
            {
                if (PlayerPrefs.GetInt("sthrownout") == 0)
                {
                    SceneManager.LoadScene("MainScene");
                }
                
                else
                {
                    SceneManager.LoadScene("Login");
                }
            }
            
        }

        public void socketoff()
        {
            socket.Off("open", OnConnected);
            socket.Off("disconnected", OnDisconnected);
            socket.Off(Events.OnChipMove, OnChipMove);
            socket.Off(Events.OnGameStart, OnGameStart);
            socket.Off(Events.OnAddNewPlayer, OnAddNewPlayer);
            socket.Off(Events.OnPlayerExit, OnPlayerExit);
            socket.Off(Events.OnTimerStart, OnTimerStart);
            socket.Off(Events.OnWait, OnWait);
            socket.Off(Events.OnTimeUp, OnTimerUp);
            socket.Off(Events.OnCurrentTimer, OnCurrentTimer);
            socket.Off(Events.OnWinNo, OnWinNo);
            socket.Off(Events.OnBotsData, OnBotsData);
            socket.Off(Events.OnPlayerWin, OnPlayerWin);
            socket.Off(Events.onleaveRoom, onLeaveRoom);
            socket.Off(Events.OnWinAmount, OnWinAmount);
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
        }
        void OnChipMove(SocketIOEvent e)
        {
            chipMovement.OnOtherPlayerMove((object)e.data);
        }
        void OnBetsPlaced(SocketIOEvent e)
        {
            Debug.Log("OnBetsPlace:"+e);
        }
        void OnBotsData(SocketIOEvent e)
        {
            // Debug.Log("bots data incoming" +e.data);
            //Debug.Log("bbbbbbboooooooooooooooooooootttttttttttttttttttsssssssss" +e.data);
            _7updown_BetsHandler.Instance.AddBotsData(e.data);
        }
        void OnWinNo(SocketIOEvent e)
        {
            Debug.Log("OnWin"+e.data);
            _7updown_RoundWinningHandler.Instance.OnWin(e.data);
        }
        void OnWinAmount(SocketIOEvent e)
        {
            Debug.Log("OnWinAmount"+e);
        }

        void OnGameStart(SocketIOEvent e)
        {
            chipMovement.OnOtherPlayerMove((object)e.data);
        }
        void OnAddNewPlayer(SocketIOEvent e)
        {
            chipMovement.OnOtherPlayerMove((object)e.data);
        }
        void OnPlayerExit(SocketIOEvent e)
        {
            chipMovement.OnOtherPlayerMove((object)e.data);
        }


        void OnTimerStart(SocketIOEvent e)
        {
            Debug.Log("on timer start " + e.data);
          //  timer.OnTimerStart((object)e.data);
            _7updown_Timer.Instance.OnTimerStart(e.data);
        }

        void OnTimerUp(SocketIOEvent e)
        {
            Debug.Log("on timeUp ");
            //callonwin();
            //socket.Emit(Events.OnWinNo, new JSONObject(JsonConvert.SerializeObject(data)));
            //serverRequest.instance.socket.Emit(Utility.Events.OnWinNo,new JSONObject(JsonConvert.SerializeObject(data)));
            _7updown_Timer.Instance.OnTimeUp((object)e.data);

        }
        public void callonwin()
        {
            string playerName = "GK" + PlayerPrefs.GetString("email");
          //  timer.((object)e.data);
            Info data = new Info{
                playerId = playerName
            };
            Debug.Log("//////////////////////////"+data.playerId);
            socket.Emit(Events.OnWinNo,new JSONObject(JsonConvert.SerializeObject(data)));
            //socket.Emit(Events.OnWinNo,new JSONObject(JsonConvert.SerializeObject(data)));
        }
        void OnWait(SocketIOEvent e)
        {
            Debug.Log("on wait ");
          //  timer.OnWait((object)e.data);
            _7updown_Timer.Instance.OnWait((object)e.data);

        }
        void OnCurrentTimer(SocketIOEvent e)
        {
            if (on_current)
            {
                return;
            }
            else
            {
                on_current = true;
                fromsevencurrent = true;
 
                Debug.Log("currunt data " + e.data);
                
                _7updown_Timer.Instance.OnCurrentTime((object)e.data);

                _7updown_RoundWinningHandler.Instance.SetWinNumbers((object)e.data);
                _7updown_BotsManager.Instance.UpdateBotData((object)e.data);
            }
        }
        void OnPlayerWin(SocketIOEvent e)
        {
            Debug.Log("win something " + e.data);
            // uiHandler.OnPlayerWin(e.data);
            _7updown_UiHandler.Instance.OnPlayerWin(e.data);
        } 
    }
    public class Info{
        public string playerId;
    }
}
