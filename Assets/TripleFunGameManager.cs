using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Components;
using TripleFun.ServerStuff;
using TripleChance.GamePlay;
using TripleChance.ServerStuff;
using TripleChance.Utility;
using System.IO;

public class TripleFunGameManager : MonoBehaviour
{
    int click = 0;
    public static TripleFunGameManager instance;
    SceneHandler sceneHandler;
    private void Start() 
    {
        instance =this;
        //sceneHandler = GameObject.Find("SceneHandler").GetComponent<SceneHandler>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            click++;
            AndroidToastMsg.ShowAndroidToastMessage("Press again to exit");
            Debug.Log("Prss again to exit");
            StartCoroutine(ClickAgain());
            if (click > 1)
            {
                Application.Quit();
                // SceneManager.LoadScene(0);
            }
        }
    }



    IEnumerator ClickAgain()
    {
        yield return new WaitForSeconds(0.5f);
        click = 0;
    }


//     public static void ShowAndroidToastMessage(string message = "something went wrong")
//     {
//         if (Application.platform != RuntimePlatform.Android) return;

// #if UNITY_ANDROID
//         AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
//         AndroidJavaObject unityActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");

//         if (unityActivity != null)
//         {
//             AndroidJavaClass toastClass = new AndroidJavaClass("android.widget.Toast");
//             unityActivity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
//             {
//                 AndroidJavaObject toastObject = toastClass.CallStatic<AndroidJavaObject>("makeText", unityActivity, message, 0);
//                 toastObject.Call("show");
//             }));
//         }
// #endif
//     }

    
    private string SaveFilePath
    {
    get { return Application.persistentDataPath + "/Triplefun.nms"; }
    }
    public void close()
    {
        
        //sceneHandler = GameObject.Find(SceneHandler);
        Debug.Log("clsoe btton has been pressed");
        // if(TripleFunScreen.Instance.totalBets >0)
        // {
            
        // }
        Triplelast.winamount = int.Parse(TripleFunScreen.Instance.winTxt.text);
        if( TripleFunScreen.Instance.isBetConfirmed || int.Parse(TripleFunScreen.Instance.winTxt.text) > 0)
        {
            TripleFunScreen.Instance.storewithin();
            if(PlayerPrefs.GetInt("tripletrounds") != TripleChance_ServerResponse.instance.tripleround)
            {
                
                TripleFunScreen.Instance.store();
            }
            
        }
        if(!TripleFunScreen.Instance.isBetConfirmed)
        {
            Triplewithin.tripleconfirmed = false;
        }

        PlayerPrefs.SetInt("tripletrounds",TripleChance_ServerResponse.instance.tripleround);
        File.Delete(SaveFilePath);
        savebinary.savefunctiontriple();
        //savebinary.savefunctiontriple();
        //savebinary.LoadPlayer();
        TripleFunScreen.Instance.leavetheroom();
        
        //TripleChance_ServerResponse.instance.socket.Emit(Utility.Events.OnBetsPlaced, new JSONObject(JsonConvert.SerializeObject(data)));
        //sceneHandler.unloadAddressableScene();
        InterfaceDisplay.ingame = transform;
        //TripleFUn
        //TripleFun_ServerResponse.instance.socket.Emit(Utility)//(Events.onleaveRoom);//onleaveRoom();
        //SceneManager.LoadScene(1);
    }

}
