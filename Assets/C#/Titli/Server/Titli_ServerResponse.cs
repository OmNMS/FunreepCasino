using System.Collections;
using UnityEngine;
using SocketIO;
using Titli.Utility;
using Titli.UI;
using Titli.Gameplay;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using Newtonsoft.Json;
using System.Threading;

namespace Titli.ServerStuff
{
    
    public class Titli_ServerResponse : Titli_SocketHandler
    {
        private string updateScoreAPI = "http://139.59.92.165:5000/user/updateScore";
        public static Titli_ServerResponse Instance;
        public Titli_ServerRequest serverRequest;
        public bool on_current = false;

        private void Awake()
        {
            Instance = this;
            socket = GameObject.Find("SocketIOComponents").GetComponent<SocketIOComponent>();
            
        }
        private void Start()
        {
         
            
            //socket.Emit("open");
            // socket = GameObject.Find("SocketIOComponents").GetComponent<SocketIOComponent>();
            socket.On("open", OnConnected);
            
            socket.On("disconnected", OnDisconnected);
              Debug.Log("game started");
            socket.On(Utility.Events.onleaveRoom, OnleaveRoom);
            //socket.On(Utility.Events.OnChipMove, OnChipMove);
            //socket.On(Utility.Events.OnGameStart, OnGameStart);
            //socket.On(Utility.Events.OnAddNewPlayer, OnAddNewPlayer);
            //socket.On(Utility.Events.OnPlayerExit, OnPlayerExit);
            
            socket.On(Utility.Events.OnTimerStart, OnTimerStart);
            
            //socket.On(Utility.Events.OnWait, OnWait);
            
            socket.On(Utility.Events.OnTimeUp, OnTimerUp);
             
            //socket.On(Events.SendCurrentRoundInfo, OnCurrentTimer);
             
            socket.On(Utility.Events.OnCurrentTimer, OnCurrentTimer);
             
            socket.On(Utility.Events.OnWinNo, OnWinNo);
           
            //socket.On(Utility.Events.OnBotsData, OnBotsData);
             
            socket.On(Utility.Events.OnPlayerWin, OnPlayerWin);
             
            //socket.On(Utility.Events.OnHistoryRecord, OnHistoryRecord);
            
            socket.On(Utility.Events.OnBetsPlaced, OnBetsPlaced);

            serverRequest.JoinGame();
            
        }

        
        
        void OnConnected(SocketIOEvent e)
        {

            print("connected");
            Debug.Log("1111");
            isConnected = true;
            Debug.Log("2222");
            //serverRequest.JoinGame();
        }

        void OnleaveRoom(SocketIOEvent e)
        {
            socketsoff();
            socket.Emit("disconnected");
            Debug.Log("onleave room socket details:"+e.data);

            SceneManager.LoadScene("MainScene");
            
            //socket.Off("open", OnConnected);
            
            //socket.Off("disconnected", OnDisconnected);
            //Debug.Log("onleave room socket details:"+e.data);
            //socketsoff();
            //SceneManager.LoadScene("MainScene");
            // socket.Off(Events.OnChipMove, OnChipMove);
            // socket.Off(Events.OnGameStart, OnGameStart);
            // socket.Off(Events.OnAddNewPlayer, OnAddNewPlayer);
            // socket.Off(Events.OnPlayerExit, OnPlayerExit);
            // socket.Off(Events.OnTimerStart, OnTimerStart);
            // socket.Off(Events.OnWait, OnWait);
            // socket.Off(Events.OnTimeUp, OnTimerUp);
            // socket.Off(Events.SendCurrentRoundInfo, OnCurrentTimer);
            // socket.Off(Events.OnWinNo, OnWinNo);
            // socket.Off(Events.OnBotsData, OnBotsData);
            // socket.Off(Events.OnPlayerWin, OnPlayerWin);
            // socket.Off(Events.OnHistoryRecord, OnHistoryRecord);
            // socket.Off(Events.OnBetsPlaced, OnBetsPlaced);
            // socket.Off(Events.onleaveRoom, OnLeaveRoom);
            
            
        } 
        public void socketsoff()
        {
            socket = GameObject.Find("SocketIOComponents").GetComponent<SocketIOComponent>();
            //socket.Off(Utility.Events.OnChipMove, OnChipMove);
            //socket.Off(Utility.Events.OnGameStart, OnGameStart);
            //socket.Off(Utility.Events.OnAddNewPlayer, OnAddNewPlayer);
            //socket.Off(Utility.Events.OnPlayerExit, OnPlayerExit);
            socket.Off(Utility.Events.OnTimerStart, OnTimerStart);
            //socket.Off(Utility.Events.OnWait, OnWait);
            socket.Off(Utility.Events.OnTimeUp, OnTimerUp);
            socket.Off(Utility.Events.SendCurrentRoundInfo, OnCurrentTimer);
            socket.Off(Utility.Events.OnWinNo, OnWinNo);
            //socket.Off(Utility.Events.OnBotsData, OnBotsData);
            socket.Off(Utility.Events.OnPlayerWin, OnPlayerWin);
            //socket.Off(Utility.Events.OnHistoryRecord, OnHistoryRecord);
            socket.Off(Utility.Events.OnBetsPlaced, OnBetsPlaced);
            socket.Off(Utility.Events.onleaveRoom, OnleaveRoom);
            Debug.Log("traversed all");
        }
        void OnDisconnected(SocketIOEvent e)
        {
            print("disconnected");
            isConnected = false;
        }
        void OnChipMove(SocketIOEvent e)
        {
            // Titli_CardController.Instance.OnOtherPlayerMove((object)e.data);
        }

        void OnBotsData(SocketIOEvent e)
        {
            // WOF_BetsHandler.Instance.AddBotsData(e.data);
        }

        public void OnWinFunction()
        {
            Debug.Log("WIn function called ");
            // socket.On(Events.OnWinNo, OnWinNo);
        }

        void OnWinNo(SocketIOEvent e)
        {
            Debug.Log("On win :" +e.data);
            // if(Titli_CardController.Instance._winNo == true)
            // {
                // WOF_RoundWinningHandler.Instance.OnWin(e.data);         //call this function when api is integrated
            Titli_RoundWinningHandler.Instance.OnWin(e.data);
                // Titli_CardController.Instance._winNo = false;
            // }
        }

        void OnGameStart(SocketIOEvent e)
        {
            Debug.Log("OnGameStart " + e.data);
            // WOF_ChipController.Instance.OnOtherPlayerMove((object)e.data);
            // StartCoroutine(Titli_CardController.Instance.CardsBlink());
        }
        void OnAddNewPlayer(SocketIOEvent e)
        {
            Debug.Log("OnAddNewPlayer " + e.data);
            // WOF_ChipController.Instance.OnOtherPlayerMove((object)e.data);
        }
        void OnPlayerExit(SocketIOEvent e)
        {
            Debug.Log("OnPlayerExit " + e.data);
            // WOF_ChipController.Instance.OnOtherPlayerMove((object)e.data);
        }

        public void TimerFunction()
        {
            //socket.On(Events.OnTimerStart, OnTimerStart);
        }

        void OnTimerStart(SocketIOEvent e)
        {
            Debug.Log("on timer start " + e.data);
            Titli_UiHandler.Instance.HideMessage();
            Titli_Timer.Instance.OnTimerStart();//((object)e.data);
        }

        public void TimerUpFunction()
        {
            //socket.On(Events.OnTimeUp, OnTimerUp);
        }

        void OnTimerUp(SocketIOEvent e)
        {
            Debug.Log("on timeUp " + e.data);
            Titli_Timer.Instance.OnTimeUp();//((object)e.data);
            winningid user = new winningid()
            {
                playerId  = PlayerPrefs.GetString("email")
            };
            //Titli_ServerRequest.instance.socket.Emit(Events.OnWinNo,new JSONObject(JsonConvert.SerializeObject(user)));
            //Titli_ServerRequest.instance.socket.Emit(Events.OnHistoryRecord,new JSONObject(JsonConvert.SerializeObject(user)));
            // Debug.Log("onwin idhar call hoga");
            // updategamescore(Titli_UiHandler.Instance.getBalance());
        }
        
        public void updategamescore(int score)
        {
            PlayerPrefs.SetString("Points",score.ToString());
            
            // WebRequests.instance.UpdateScore(UiHandler.Instance.getBalance().ToString());
            StartCoroutine(UpdateScore(score));
        }
        
        public IEnumerator UpdateScore(int score)
        {
            // Debug.Log("333333  email"+ PlayerPrefs.GetString("email") +  "score "+score);
            WWWForm form = new WWWForm();
            form.AddField("useremail", PlayerPrefs.GetString("email"));
            form.AddField("score", score);

            using (UnityWebRequest www = UnityWebRequest.Post(updateScoreAPI, form))
            {
                yield return www.SendWebRequest();

                if (www.result != UnityWebRequest.Result.Success)
                {
                    Debug.Log(www.error);
                }
                else
                {
                    Debug.Log("response is = "+www.downloadHandler.text);
                }
            }
        }
        void OnWait(SocketIOEvent e)
        {
            Debug.Log("on wait " + e.data);
            Titli_Timer.Instance.OnWait();//((object)e.data);
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
                Debug.Log("Current data " + e.data);
                Titli_Timer.Instance.OnCurrentTime((object)e.data);
                Titli_RoundWinningHandler.Instance.SetWinNumbers(e.data);
                Titli_BotsManager.Instance.UpdateBotData(e.data);
                //Titli_RoundWinningHandler.Instance.SetWinNumbers(e.data);
            }
        }
        void OnPlayerWin(SocketIOEvent e)
        {
            Debug.Log("win something " + e.data);
            // Titli_UiHandler.Instance.OnPlayerWin(e.data);
        }
        void OnHistoryRecord(SocketIOEvent e)
        {
            Debug.Log("OnHistoryRecord " + e.data);
            //Titli_RoundWinningHandler.Instance.storage(e.data);
        }

        void OnBetsPlaced(SocketIOEvent e)
        {
            Debug.Log("OnBetsPlaced" + e.data);
        }
    }
}

