    using Com.BigWin.Frontend.Data;
using SocketIO;
using System;
using UnityEngine;
using FunTarget.GamePlay;
using System.Collections.Generic;
using UnityEngine.UI;
using Newtonsoft.Json;
using UnityEngine.SceneManagement;

namespace FunTarget.ServerStuff
{
    public class FunTarget_ServerResponse : FunTarget_SocketHandler
    {
        public FunTarget_ServerRequest serverRequest;
        public static FunTarget_ServerResponse instance;
        public bool on_current = false;
        public bool fromcurrent;
        //public GameObject waitpanel;
        public Text roundCount;
        public int roundcounttillnow;

        [SerializeField] Button betOkBtn;
        [SerializeField] Button repeatBtn;

        private void Awake() {
            instance = this;
            socket = GameObject.Find("SocketIOComponents").GetComponent<SocketIOComponent>();
        }
        private void Start()
        {
             
            socket.On("open", OnConnected);
            socket.On("disconnected", OnDisconnected);
            // socket.On(Utility.Events.OnChipMove, OnChipMove);
            // socket.On(Utility.Events.OnGameStart, OnGameStart);
            // socket.On(Utility.Events.OnAddNewPlayer, OnAddNewPlayer);
            // socket.On(Utility.Events.OnPlayerExit, OnPlayerExit);
            socket.On(Utility.Events.OnTimerStart, OnTimerStart);
            // socket.On(Utility.Events.OnWait, OnWait);
            socket.On(Utility.Events.OnTimeUp, OnTimerUp);
            socket.On(Utility.Events.OnCurrentTimer, OnCurrentTimer);
            socket.On(Utility.Events.OnWinNo, OnWinNo);
            // socket.On(Utility.Events.OnBotsData, OnBotsData);
            socket.On(Utility.Events.OnPlayerWin, OnPlayerWin);
            socket.On(Utility.Events.OnBetsPlaced, OnBetsPlaced);
            socket.On(Utility.Events.OnWinAmount, OnWinAmount);
            socket.On(Utility.Events.onleaveRoom, OnLeaveRoom);
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
            socket.Off(Utility.Events.OnTimeUp, OnTimerUp);
            socket.Off(Utility.Events.OnCurrentTimer, OnCurrentTimer);
            socket.Off(Utility.Events.OnWinNo, OnWinNo);
            // socket.On(Utility.Events.OnBotsData, OnBotsData);
            socket.Off(Utility.Events.OnPlayerWin, OnPlayerWin);
            socket.Off(Utility.Events.OnBetsPlaced, OnBetsPlaced);
            socket.Off(Utility.Events.OnWinAmount, OnWinAmount);
            socket.Off(Utility.Events.onleaveRoom, OnLeaveRoom);
            socket.RemoveRouteAllListners(Utility.Events.OnWinNo);
            
            
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
        }
        void OnLeaveRoom(SocketIOEvent e)
        {
            Debug.Log("left the room");
            socketoff();
            if(PlayerPrefs.GetInt("reload") ==1)
            {
                PlayerPrefs.SetInt("reload",0);
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);

            }
            else
            {
                if (PlayerPrefs.GetInt("thrownout") == 0)
                {
                    SceneManager.LoadScene("MainScene");
                }
                
                else
                {
                    SceneManager.LoadScene("Login");
                }
            }
            // if (PlayerPrefs.GetInt("thrownout") == 0)
            // {
            //     SceneManager.LoadScene("MainScene");
            // }
            
            // else
            // {
            //     SceneManager.LoadScene("Login");
            // }
            
        }
        void OnWinAmount(SocketIOEvent e)
        {
            Debug.Log("OnWinAmount"+e.data);
        }
        void OnChipMove(SocketIOEvent e)
        {
            // PokerKing_ChipController.Instance.OnOtherPlayerMove((object)e.data);
        }

        void OnBotsData(SocketIOEvent e)
        {
            // PokerKing_BetsHandler.Instance.AddBotsData(e.data);
        }

        void OnWinNo(SocketIOEvent e)
        {
           StartCoroutine( FunTargetGamePlay.Instance.OnRoundEnd(e.data.ToString()));
           Debug.Log("OnWin no"+e.data);
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
        void OnBetsPlaced(SocketIOEvent e)
        {
            Debug.Log("OnBetsPlace" + e.data);
            FunTargetGamePlay.Instance.SendBets_Response((object)e.data);
        }


        void OnTimerStart(SocketIOEvent e)
        {
            //Debug.Log("on timer start " + e.data);
            //waitpanel.SetActive(false);
            FunTargetGamePlay.Instance. OnTimerStart(e.data.ToString());
            //FunTargetGamePlay.instance.waiting.SetActive(false);
            roundcounttillnow++;
            roundCount.text = roundcounttillnow.ToString();
            Debug.Log("on timer start " + e.data+"ROundCount: "+roundcounttillnow);
            // PokerKing_Timer.Instance.OnTimerStart((object)e.data);

            betOkBtn.gameObject.SetActive(false);
            repeatBtn.gameObject.SetActive(true);
        }

        void OnTimerUp(SocketIOEvent e)
        {
            Debug.Log("on timeUp " + e.data);
            FunTargetGamePlay.Instance.isTimeUp = true;
            // string playerName = "RL" + PlayerPrefs.GetString("email");
            // Info data = new Info{
            //     playerId = playerName
            // };
            // FunTarget_ServerRequest.instance.socket.Emit(Utility.Events.OnWinNo,new JSONObject(JsonConvert.SerializeObject(data)));
            // PokerKing_Timer.Instance.OnTimeUp((object)e.data);
        }
        void OnWait(SocketIOEvent e)
        {
            Debug.Log("on wait " + e.data);
            // PokerKing_Timer.Instance.OnWait((object)e.data);
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
            
                fromcurrent = true;
                Debug.Log("currunt data " + e);
                CurrentTimer val = JsonUtility.FromJson<CurrentTimer>(e.data.ToString());
                FunTargetGamePlay.Instance.setinitialarray(val.previousWins);
                StartCoroutine(FunTargetGamePlay.Instance.Timer(val.gametimer));
                SpinWheelWithoutPlugin.instane.initialimage(val.ImageGlobalArr[val.ImageGlobalArr.Length-1]);
                //StartCoroutine(FunTargetGamePlay.Instance.startprevious(val.previousWins));
                roundcounttillnow = val.RoundCount;
                
                // if(val.gametimer <2)
                // {
                //     //roundcounttillnow++;
                //     //waitpanel.SetActive(true);
                // }
                // else
                // {
                //     //waitpanel.SetActive(false);
                // }
                PlayerPrefs.SetInt("thrownout",0);  
                if (PlayerPrefs.GetString("funpaused") == "true")
                {
                    if(PlayerPrefs.GetInt("funtargetround") !=val.RoundCount )
                    {
                        Debug.Log("ROundcount"+PlayerPrefs.GetInt("funtargetround"));
                        //PlayerPrefs.SetString("funtargetround","false");
                        FunTargetGamePlay.Instance.throwout();
                        Debug.Log("thrownout because  roundcount did not match");
                    }
                    
                }
                if(PlayerPrefs.GetInt("funtargetround") !=val.RoundCount)
                {
                    Debug.Log("Get the win responce and val is not current round count ");
                    StartCoroutine(FunTargetGamePlay.Instance.funtargetwinresponse());
                }
                else if(PlayerPrefs.GetInt("funtargetround") ==val.RoundCount && PlayerPrefs.GetInt("funwin") >0)
                {
                    Debug.Log("Get the win responce val is this round count and funwiz is more then 0 ");
                    StartCoroutine(FunTargetGamePlay.Instance.funtargetwinresponse());
                    //FunTargetGamePlay.Instance.restorewithin();
                }
                
                FunTargetGamePlay.Instance.funrestoration();
                roundCount.text = val.RoundCount.ToString();

                FunTargetGamePlay.Instance.turnonDisplay.SetActive(false);
                FunTargetGamePlay.Instance.cam.SetActive(true); 
                FunTargetGamePlay.Instance.eventhandler.SetActive(true);

                SceneManager.SetActiveScene(SceneManager.GetSceneByName("Fun Target"));
                //SceneManager.UnloadSceneAsync( SceneManager.GetSceneByName("MainScene") );

                // FunTargetGamePlay.Instance.turnonDisplay.SetActive(false);
                // FunTargetGamePlay.Instance.cam.SetActive(true); 
                // FunTargetGamePlay.Instance.eventhandler.SetActive(true);

                Debug.Log("Setting Diaplay done ");
            }
            // for(int i = val.previousWins.Count - 1; i >= val.previousWins.Count - 10 ; i--)
            // {
            //     FunTargetGamePlay.Instance.previousWinsdata.RemoveAt(8);
            //     FunTargetGamePlay.Instance.previousWinsdata.Add(val.previousWins[i]);
            //     //SpinWheelWithoutPlugin.instane.SetWheelInitialAngle(val.previousWins[i], "1x");
            //     // if( FunTargetGamePlay.Instance.previousWinsdata.Count == 9)
            //     // {
                    
            //     //     FunTargetGamePlay.Instance.previousWinsdata.RemoveAt(8);
            //     //     FunTargetGamePlay.Instance.previousWinsdata.Add(val.previousWins[i]);
            //     //     Debug.Log("currunt data " + e.data);
            //     //     SpinWheelWithoutPlugin.instane.SetWheelInitialAngle(val.previousWins[i], "1x");
            //     //     break;
            //     // }
            //     // else
            //     // {
            //     //     FunTargetGamePlay.Instance.previousWinsdata.Add(val.previousWins[i]);
            //     // }
            // }


            for(int i = 0; i <= FunTargetGamePlay.Instance.previousWinsdata.Count - 1; i++)
            {
                FunTargetGamePlay.Instance.previousWinsText[i].GetComponent<Text>().text = FunTargetGamePlay.Instance.previousWinsdata[i].ToString();
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
        public int RoundCount;
        public int[] ImageGlobalArr;
        //public List<int> previousWins; 
    }
    public class Info{
        public string playerId;
        
    }
}
