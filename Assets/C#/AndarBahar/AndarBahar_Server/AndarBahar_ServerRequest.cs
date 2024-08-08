using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using SocketIO;
using Shared;
using AndarBahar.Utility;
using Newtonsoft.Json;
using Com.BigWin.Frontend.Data;
using AndarBahar.ServerStuff;

namespace AndarBahar.ServerStuff
{

    public class AndarBahar_ServerRequest : AndarBahar_SocketHandler
    {
        // public SocketIOComponent socket;
        public static AndarBahar_ServerRequest instance;
        public string username;
        void Awake()
        {
            socket = GameObject.Find("SocketIOComponents").GetComponent<SocketIOComponent>();
            instance = this;
        }

        void Start()
        {
            socket = GameObject.Find("SocketIOComponents").GetComponent<SocketIOComponent>();
        }

        public void JoinGame()
        {
            Debug.Log($"player { UserDetail.playerId_Local} Join game");
            Player player = new Player()
            {
                balance = UserDetail.player_Local_Balance,
                playerId = UserDetail.playerId_Local,//UserDetail.UserId.ToString(),
                // profilePic = LocalPlayer.profilePic,
                gameId = "3"        //2
            };
            socket.Emit(Utility.Events.RegisterPlayer, new JSONObject(JsonUtility.ToJson(player)));
            username = UserDetail.playerId_Local;
        }

        // public void OnHistoryRecordGame()
        // {
        //     socket.Emit(Utility.Events.OnHistoryRecord);
        // }

        /// <summary>
        /// use this socket method when the request and response route is different
        /// </summary>
        /// <param name="eventName"></param>
        /// <param name="data"></param>
        /// <param name="json"></param>
        /// <param name="resposeEvent"></param>
        public void SendEvent(string eventName, object data, Action<string> json, string resposeEvent = null)
        {
            if (isApplicationPaused) return;
            var req = JsonConvert.SerializeObject(data);
            Debug.Log("sending event " + eventName + " req: " + req);
            socket.Emit(eventName, new JSONObject(req));
            eventName = resposeEvent == null ? eventName : resposeEvent;
            Debug.Log("event name " + eventName);
            socket.On(eventName, (res) =>
            {
                Debug.Log("res  " + res.data);
                Debug.Log("received event " + eventName + " res: " + res.data); json(res.data.ToString());
                socket.RemoveRouteAllListners(eventName);
            });
        }

        /// <summary>
        /// Use this socket function when the request and response route are the same
        /// </summary>
        /// <param name="eventName"></param>
        /// <param name="json"></param>
        public void SendEvent(string eventName, Action<string> json)
        {
            if (isApplicationPaused) return;
            Debug.Log("sending event " + eventName);
            socket.Emit(eventName);
            socket.On(eventName, (res) =>
            {
                Debug.Log("received event " + eventName + " res: " + res.data); json(res.data.ToString());
                socket.RemoveRouteAllListners(eventName);
            });

        }
        /// <summary>
        /// use this socket when there are only request
        /// </summary>
        /// <param name="eventName"></param>
        public void SendEvent(string eventName)
        {
            //if (isApplicationPaused) return;

            socket.Emit(eventName);

        } 
        
        public void RemoveListners(string eventName)
        {
            Debug.Log(eventName +" event removed ");
            socket.RemoveRouteAllListners(eventName);
        }

        public void ListenEvent(string eventName, Action<string> result)
        {
            Debug.Log("Listening evnet " + eventName);
            socket.On(eventName, (res) =>
            {

                Debug.Log("received event " + eventName + " res: " + res.data);
                try
                {

                    if (!string.IsNullOrEmpty(res.data.ToString()))
                        result(res.data.ToString());
                }
                catch (Exception)
                {

                    result(string.Empty);
                    throw;
                }

            });
        }
        public void ListenEvent<T>(string eventName, Action<T> result, Action onSomethingWentWrong) where T : class
        {
            Debug.Log("Listening evnet " + eventName);
            socket.On(eventName, (res) =>
            {
                Debug.Log("received event " + eventName + " res: " + res.data.ToString());
                var o = JsonConvert.DeserializeObject<BackEndData3<T>>(res.data.ToString());
                if (o.ValidateData(onSomethingWentWrong))
                {
                    result(o.data);
                    return;
                }
            });
        }

        // public void OnChipMove(Vector3 position, Chip chip, Spot spot)
        // {
        //     OnChipMove Obj = new OnChipMove()
        //     {
        //         position = position,
        //         playerId = UserDetail.UserId.ToString(),
        //         chip = chip,
        //         spot = spot
        //     };
        //     socket.Emit(Events.OnChipMove, new JSONObject(JsonUtility.ToJson(Obj)));
        // }

        public void Disconnect()
        {
            socket.socket.Close();
        }
        public void Connect()
        {
            socket.socket.Connect();
        }

        public  bool isApplicationPaused = false;
    }
}
