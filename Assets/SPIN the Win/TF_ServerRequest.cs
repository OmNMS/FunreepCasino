//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using SocketIO;
//using Newtonsoft.Json;
//using Com.BigWin.Frontend.Data;
//using System;

//public class TF_ServerRequest : MonoBehaviour
//{

//    public SocketIOComponent socket;
//    public static TF_ServerRequest intance;

//    public bool isConnected => socket.IsConnected;
//    private void Awake()
//    {
//        intance = this;
//    }

//    /// <summary>
//    /// use this socket method when the request and response route is different
//    /// </summary>
//    /// <param name="eventName"></param>
//    /// <param name="data"></param>
//    /// <param name="json"></param>
//    /// <param name="resposeEvent"></param>
//    public void SendEvent(string eventName, object data, Action<string> json, string resposeEvent = null)
//    {
//        if (isApplicationPaused) return;
//        var req = JsonConvert.SerializeObject(data);
//        print("sending event " + eventName + " req: " + req);
//        socket.Emit(eventName, new JSONObject(req));
//        eventName = resposeEvent == null ? eventName : resposeEvent;
//        print("event name " + eventName);
//        socket.On(eventName, (res) =>
//        {
//            Debug.Log("res  " + res.data);
//            print("received event " + eventName + " res: " + res.data); json(res.data.ToString());
//           // socket.RemoveRouteAllListners(eventName);
//        });
//    }

//    /// <summary>
//    /// Use this socket function when the request and response route are the same
//    /// </summary>
//    /// <param name="eventName"></param>
//    /// <param name="json"></param>
//    public void SendEvent(string eventName, Action<string> json)
//    {
//        if (isApplicationPaused) return;
//        print("sending event " + eventName);
//        socket.Emit(eventName);
//        socket.On(eventName, (res) =>
//        {
//            print("received event " + eventName + " res: " + res.data); json(res.data.ToString());
//           // socket.RemoveRouteAllListners(eventName);
//        });

//    }
//    /// <summary>
//    /// use this socket when there are only request
//    /// </summary>
//    /// <param name="eventName"></param>
//    public void SendEvent(string eventName)
//    {
//        //if (isApplicationPaused) return;

//        socket.Emit(eventName);

//    }

//    public void RemoveListners(string eventName)
//    {
//        Debug.Log(eventName + " event removed ");
//      //  socket.RemoveRouteAllListners(eventName);
//    }

//    public void ListenEvent(string eventName, Action<string> result)
//    {
//        Debug.Log("Listening evnet " + eventName);
//        socket.On(eventName, (res) =>
//        {

//            print("received event " + eventName + " res: " + res.data);
//            try
//            {

//                if (!string.IsNullOrEmpty(res.data.ToString()))
//                    result(res.data.ToString());
//            }
//            catch (Exception)
//            {

//                result(string.Empty);
//                throw;
//            }

//        });
//    }
//    public void ListenEvent<T>(string eventName, Action<T> result, Action onSomethingWentWrong) where T : class
//    {
//        Debug.Log("Listening evnet " + eventName);
//        socket.On(eventName, (res) =>
//        {
//            print("received event " + eventName + " res: " + res.data.ToString());
//            var o = JsonConvert.DeserializeObject<BackEndData3<T>>(res.data.ToString());
//            if (o.ValidateData(onSomethingWentWrong))
//            {
//                result(o.data);
//                return;
//            }
//        });
//    }

//    public void Disconnect()
//    {
//        socket.socket.Close();
//    }
//    public void Connect()
//    {
//        socket.socket.Connect();
//    }

//    public bool isApplicationPaused = false;


//}

