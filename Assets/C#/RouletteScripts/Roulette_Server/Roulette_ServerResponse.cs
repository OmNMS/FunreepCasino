using UnityEngine;
using SocketIO;
using RouletteScripts.Utility;

namespace RouletteScripts.ServerStuff
{
    public class Roulette_ServerResponse : Roulette_SocketHandler
    {
        public Roulette_ServerRequest serverRequest;

        private void Start()
        {
            socket = GameObject.Find("SocketIOComponents").GetComponent<SocketIOComponent>();
            socket.On("open", OnConnected);
            socket.On("disconnected", OnDisconnected);
            socket.On(Utility.Events.OnTimerStart, OnTimerStart);
            socket.On(Utility.Events.OnCurrentTimer, OnCurrentTimer);
            socket.On(Utility.Events.OnWinNo, OnWinNo);
            serverRequest.JoinGame();
        }
        void OnConnected(SocketIOEvent e)
        {
            print("connected");
            isConnected = true;
            serverRequest.JoinGame();
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

        void OnWinNo(SocketIOEvent e)
        {
            // WOF_RoundWinningHandler.Instance.OnWin(e.data);         //call this function when api is integrated
            // PokerKing_RoundWinningHandler.Instance.OnWin(e.data);
        }

        void OnGameStart(SocketIOEvent e)
        {
            Debug.Log("OnGameStart " + e.data);
        }
        void OnAddNewPlayer(SocketIOEvent e)
        {
            Debug.Log("OnAddNewPlayer " + e.data);
        }
        void OnPlayerExit(SocketIOEvent e)
        {
            Debug.Log("OnPlayerExit " + e.data);
        }

        void OnTimerStart(SocketIOEvent e)
        {
            Debug.Log("on timer start " + e.data);
        }

        void OnTimerUp(SocketIOEvent e)
        {
            Debug.Log("on timeUp " + e.data);
        }
        void OnWait(SocketIOEvent e)
        {
            Debug.Log("on wait " + e.data);
        }
        void OnCurrentTimer(SocketIOEvent e)
        {
            Debug.LogError("currunt data " + e.data);
        }
        void OnPlayerWin(SocketIOEvent e)
        {
            Debug.Log("win something " + e.data);
        }
        void OnHistoryRecord(SocketIOEvent e)
        {
            Debug.Log("OnHistoryRecord " + e.data);
        }
    }

    public class CurrentTimer
    {
        public int gametimer;
    }
}
