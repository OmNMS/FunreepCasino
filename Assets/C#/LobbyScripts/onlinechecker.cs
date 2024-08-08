

using System.Net.Mime;
//using System.Diagnostics;
//using System.Diagnostics;
using Com.BigWin.WebUtils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using UnityEngine.Networking;


public class onlinechecker : MonoBehaviour
{
    public static onlinechecker instant;
    [HideInInspector]public string playerid;
    [HideInInspector]public string deviceid;
    string checkplayer ="http://139.59.92.165:5000/auth/checklogin";
    
    // Start is called before the first frame update
    void Start()
    {
        instant = this;
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    public void storedata(string player,string device)
    {
        playerid = player;
        deviceid = device;
        StartCoroutine(CheckLogin(playerid,deviceid));
    }
    public IEnumerator CheckLogin(string email_id, string deviceid)
    {
        WWWForm checkLoginForm = new WWWForm();
        checkLoginForm.AddField("email", email_id);
        checkLoginForm.AddField("device_id", deviceid);

        using (UnityWebRequest request = UnityWebRequest.Post(checkplayer, checkLoginForm))
        {
            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success )
            {
                Debug.Log(request.error);
                Debug.LogError("this was a faliure");
            }
            else
            {
                ///Debug.Log("Checking Logges in " + request.downloadHandler.text + "playerid" + playerid +"deviceid"+deviceid);
                
                checker result = JsonConvert.DeserializeObject<checker>(request.downloadHandler.text);
                if(result.status == 200)
                {
                    //Debug.Log("the player was logged in");
                }
                if(result.status == 204)
                {
                    Debug.Log("the player has left the game");
                    Application.Quit();
                }
                // if(!result.login)
                // {
                //     Debug.Log("my player was logged in ");
                // } 
                // else{
                //     Debug.Log("my player was not logged in ");
                // } 
                // if(result.status == 200)
                // {
                //     //callback(result);
                //     LoginScript.Instance.ForcedLoggedIn();
                // }
                //callback(result);

            }     
        }
        yield return new WaitForSeconds(10f);
        StartCoroutine(CheckLogin(playerid,deviceid));


    }
    public class checker
    {
        public int status;
        public bool login;
    }
}
