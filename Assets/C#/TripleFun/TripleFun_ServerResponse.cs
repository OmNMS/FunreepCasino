using UnityEngine;
using SocketIO;
using System;
using Shared;
using Dragon.Utility;
using TFU;
//using AndarBahar.Server
//using Dragon.UI;
using Dragon.Gameplay;
using TripleChance.GamePlay;
using TripleChance.ServerStuff;
using Newtonsoft.Json;

namespace TripleFun.ServerStuff
{
    class TripleFun_ServerResponse : MonoBehaviour
    {

        public SocketIOComponent socket;
        private Timer_TripleFun timer;
     //   ITimer itimer;
        public static bool TimerStart = false;
        public static TripleFun_ServerResponse instance;
        private void Start()
        {
            instance = this;
            socket = SocketIOComponent.instance;
            timer = GetComponent<Timer_TripleFun>();
            socket.On("open", OnConnected);
            socket.On("disconnected", OnDisconnected);
            socket.On("startTimer", OnGameStart);
            socket.On("OnWinNo",OnWinNo);
            socket.On(Triple_Util.TF_Events.OnTimerStart, OnTimerStart);
            socket.On(Triple_Util.TF_Events.OnWait, OnWait);
            socket.On(Triple_Util.TF_Events.OnTimeUp, OnTimerUp);
            socket.On(Triple_Util.TF_Events.OnWinAmount,OnWinAmount);
            //socket.On(Utility.//Events.OnWinAmount,OnWinAmount);
            
        }
        public void onleaveRoom()
        {
            socket.Off("open", OnConnected);
            socket.Off("disconnected", OnDisconnected);
            socket.Off("startTimer", OnGameStart);
            socket.Off("OnWinNo",OnWinNo);
            socket.Off(Triple_Util.TF_Events.OnTimerStart, OnTimerStart);
            socket.Off(Triple_Util.TF_Events.OnWait, OnWait);
            socket.Off(Triple_Util.TF_Events.OnTimeUp, OnTimerUp);
        }
        void OnWinAmount(SocketIOEvent e)
        {
            Debug.Log("ONwinAmount"+e);
        }

        void OnConnected(SocketIOEvent e)
        {
            print("connected");
            // isConnected = true;
        }
        void OnDisconnected(SocketIOEvent e)
        {
            print("disconnected");
            //  isConnected = false;
        }
        //void OnChipMove(SocketIOEvent e)
        //{
        //    chipMovement.OnOtherPlayerMove((object)e.data);
        //}

        //void OnBotsData(SocketIOEvent e)
        //{
        //    botsController.AddBotsData(e.data);
        //}
        //void OnWinNo(SocketIOEvent e)
        //{
        //    if (timer.is_a_FirstRound) return;
        //    //Debug.Log(e.data);
        //    cardHandler.OnRoundDrawResult((object)e.data);
        //}

        void OnGameStart(SocketIOEvent e)
        {
            TimerStart = true;
            Debug.Log("GameStart");
        }
        //void OnAddNewPlayer(SocketIOEvent e)
        //{
        //    chipMovement.OnOtherPlayerMove((object)e.data);
        //}
        void OnPlayerExit(SocketIOEvent e)
        {

        }

        //void OnBotsData(SocketIOEvent e)
        //{

        //    botManager.AddBotsBets(e.data);
        //}
        void OnTimerStart(SocketIOEvent e)
        {
            Debug.Log("Timer Start");
            TimerStart = true;
            OnTimerStart(e);
            //WaitPanel.SetActive(False);
            TripleFunScreen.Instance.Enable_OverrideSorting();

        }

        void OnTimerUp(SocketIOEvent e)
        {
            Debug.Log("on timeUp ");
            //TripleFunScreen.Instance.BettingButtonInteractablity(false);
            string playerName = "GK" + PlayerPrefs.GetString("email");
            Info data = new Info{
                playerId = playerName
            };
            TripleFun_ServerRequest.instance.socket.Emit(Triple_Util.TF_Events.OnWinNo,new JSONObject(JsonConvert.SerializeObject(data)));
            //TripleFun_ServerResponse.instance.socket.Emit(Utility.Events.OnWinNo, new JSONObject(JsonConvert.SerializeObject(data)));
            //TripleChance_ServerResponse.instance.socket.Emit(Utility.Events.OnBetsPlaced, new JSONObject(JsonConvert.SerializeObject(data)));
            TripleFunScreen.Instance.isTimeUp = true;
            
            //   timer.OnTimeUp((object)e.data);
        }
        void OnWait(SocketIOEvent e)
        {
            Debug.Log("on wait ");
            //  timer.OnWait((object)e.data);
        }
        void OnWinNo(SocketIOEvent e)
        {
            
            Debug.Log("OnWIn ");
            StartCoroutine(TripleFunScreen.Instance.OnRoundEnd(e.ToString()));
        }
       
        /// <summary>
        /// the function contion the inital data that need to start the round
        /// like last 50 rounds wins
        /// bots name and balance
        /// </summary>
        ///// <param name="e"></param>
        //void OnCurrentTimer(SocketIOEvent e)
        //{
        //    timer.OnCurrentTime((object)e.data);
        //    botManager.UpdateBotData((object)e.data);
        //    uiHandler.UpdateDashboard((object)e.data);
        //    //roundWinningHandler.SetWinNumbers((object)e.data);
        //}

        //private bool isConnected;

        //void OnPlayerWin(SocketIOEvent e)
        //{
        //    uiHandler.OnPlayerWin(e.data);
        //}
    }
    public class Info{
        public string playerId;
    }


}