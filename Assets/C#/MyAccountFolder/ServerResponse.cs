using SocketIO;
using System;
using UnityEngine;
using LobbyScripts;
using UnityEngine.SceneManagement;
using Newtonsoft.Json;

public class ServerResponse : MonoBehaviour
{
    public SocketIOComponent socket;
    public static ServerResponse  intance;
    // public Action OnForceExit;
    void Awake()
    {
        intance = this;
    }
    private void Start()
    {
        socket = GameObject.Find("SocketIOComponents").GetComponent<SocketIOComponent>();
        socket.On("open", OnConnected);
        socket.On(Constants.OnDissconnect, OnDissconnect);
        // socket.On("OnForceExit", OnForceExist);
    }
    void OnConnected(SocketIOEvent e)
    {
        print("connected");
    }  
    
    void OnDissconnect(SocketIOEvent e)
    {
        // OnForceExit?.Invoke();
        print("dissconnect" + e.data);
        AndroidToastMsg.ShowAndroidToastMessage("Disconnected " );
        Application.Quit();
        // SceneManager.LoadScene("Login");
    }

    // void CurrentRoundInfo(SocketIOEvent e)
    // {
    //     Debug.Log("current round info"+e.data);
    // }
    // void OnBetPlaced(SocketIOEvent e)
    // {
    //     Debug.Log("OnBetPlaced " + e.data);
    // } 
    // void OnWinAmount(SocketIOEvent e)
    // {
    //     Debug.Log("OnWinAmount " + e.data);
    // }
    // void OnForceExist(SocketIOEvent e)
    // {
    //     checkforLogout responce = JsonConvert.DeserializeObject<checkforLogout>(e.data.ToString());

    //     if (responce.email == PlayerPrefs.GetString("email"))
    //     {
            
    //     }
    //     Debug.Log("someone connected from different location");
    //     Debug.Log("force exit");

    //     SocketRequest.intance.Disconnect();
    //     OnForceExit?.Invoke();
    // }
}
// public class checkforLogout
// {
//     public string email;
//     public string device_id;
// }