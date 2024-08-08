using Dragon.ServerStuff;
using Dragon.Utility;
using Newtonsoft.Json;
using Shared;
using SocketIO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyServerRequest : SocketHandler
{  
    private AndroidJavaObject androidActivity;
    private AndroidJavaObject thermalManager;
    private void Start()
    {
        // Screen.SetResolution(1920, 1080, true, 60);
        AndroidJavaClass unityPlayerClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        androidActivity = unityPlayerClass.GetStatic<AndroidJavaObject>("currentActivity");

        // Retrieve the thermal manager
        AndroidJavaClass thermalClass = new AndroidJavaClass("android.os.ThermalManager");
        thermalManager = androidActivity.Call<AndroidJavaObject>("getSystemService", thermalClass.GetStatic<string>("THERMAL_SERVICE"));
        // changeREsolutions();
        //socket = GameObject.Find("SocketIOComponents").GetComponent<SocketIOComponent>();
        socket.On("open", OnConnected);
        socket.On("disconnected", OnDisconnected);
        socket.On("OnForceExit", OnForceExist);
    }
    void OnConnected(SocketIOEvent e)
    {
        print("connected");

        isConnected = true;
        Player player = new Player()
        {
            playerId = UserDetail.UserId.ToString()
        };
        socket.Emit("onEnterLobby", new JSONObject(JsonUtility.ToJson(player)));

    }
    void OnDisconnected(SocketIOEvent e)
    {
        print("disconnected");
        AndroidToastMsg.ShowAndroidToastMessage("Disconnected From Server");
        Application.Quit();
        isConnected = false;
    }

    void OnForceExist(SocketIOEvent e)
    {
        checkforLogout responce = JsonConvert.DeserializeObject<checkforLogout>(e.data.ToString());

        if (responce.email == PlayerPrefs.GetString("email"))
        {
            if (responce.device_id ==  GenerateDeviceIdClass.GenerateUniqueIdentifier1())
            {
                PlayerPrefs.DeleteAll();
                SceneManager.LoadScene("Login");
            }
        }
        Debug.Log("someone connected from different location");
        Debug.Log("force exit");

        // SocketRequest.intance.Disconnect();
    }

    public void changeREsolutions()
    {
        // Screen scr = Screen.Res
        // Resolution[] resr = Screen.resolutions;
        // foreach (var item in resr)
        // {
        //     Debug.Log( " "  + item.ToString() );
        // }
    }

    private void Update() 
    {

        if (Application.platform == RuntimePlatform.Android)
        {
            string thermalStatus = thermalManager.Call<string>("getCurrentThermalStatus");
    
            //Do something based on the thermal status
            Debug.Log("Current thermal status: " + thermalStatus);
            if ( thermalStatus == "ATHERMAL_STATUS_MODERATE"  )
            {
                Screen.SetResolution(1920, 1080, true, 30);
            }    
            
            if ( thermalStatus == "ATHERMAL_STATUS_SEVERE")
            {
                Screen.SetResolution(1280, 720, true, 30);
            }
        }
    }
}

public class checkforLogout
{
    public string email;
    public string device_id;
}
