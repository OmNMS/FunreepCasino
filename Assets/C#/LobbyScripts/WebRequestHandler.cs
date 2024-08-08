//using System.Diagnostics;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Com.BigWin.WebUtils
{
    public class WebRequestHandler : MonoBehaviour
    {
        public static WebRequestHandler instance;
        string setplayeronline ="http://139.59.92.165:5000/user/SetplayerOnline";
        string setplayeroffline = "http://139.59.92.165:5000/user/SetplayerOffline";
        private void Awake()
        {
            instance = this;
        }
        public void Get(string url, Action<string, bool> OnRequestProcessed)
        {
            StartCoroutine(GetRequest(url, OnRequestProcessed));
        }
        private IEnumerator GetRequest(string url, Action<string, bool> OnRequestProcessed)
        {
            if (Application.internetReachability == NetworkReachability.NotReachable)
            {
                print("check internet connection");
                AndroidToastMsg.ShowAndroidToastMessage("check internet connection");
                yield break;
            }
           
            UnityWebRequest request = UnityWebRequest.Get(url);
            yield return request.SendWebRequest();
            if (request.isNetworkError || request.isHttpError)
            {
                Debug.Log("web request error in Get method with responce code : " + request.responseCode);
                OnRequestProcessed(request.error, false);
            }
            else
            {
                Debug.Log("sending get web request  : " + url + "got response:" + request.downloadHandler.text);
                OnRequestProcessed(request.downloadHandler.text, true);
            }
            request.Dispose();
        }
        public void Post(string url, string json, Action<string, bool> OnRequestProcessed)
        {
            Debug.Log("URL " + url);
            if (Application.internetReachability == NetworkReachability.NotReachable)
            {
                print("check internet connection");
                AndroidToastMsg.ShowAndroidToastMessage("check internet connection");
                return;
            }
            Debug.Log(url + " json request: " + json);
            StartCoroutine(PostRequest(url, json, OnRequestProcessed));
        }

        private IEnumerator PostRequest(string url, string json, Action<string, bool> OnRequestProcessed, int attemps = 2)
        {
            Debug.Log("url>>>>>  " + url);
            Debug.Log("PostRequest " + json);
            var request = new UnityWebRequest(url, "POST");
            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(json);
            request.uploadHandler = new UploadHandlerRaw(jsonToSend);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();
           
            if (request.result == UnityWebRequest.Result.ConnectionError)
            {

                if (attemps == 0)
                {
                    Debug.Log(">>>>>>>>" + request.error);
                    OnRequestProcessed(request.error, false);
                }
                else
                {
                    Debug.Log(">>>>>PostRequest>>>" );
                    StartCoroutine(PostRequest(url, json, OnRequestProcessed, --attemps));
                }
                    
            }
            else
            {
                // Debug.LogError(url + " json response: " + request.downloadHandler.text);
                LoginStatus n = JsonUtility.FromJson<LoginStatus>(request.downloadHandler.text);
                // Debug.LogError("status  " + n.status );
                //{"status":200,"message":"Login success","user":{"login":true,"profile":{"email":"GK00001"}},"errors":{},
                // "token":"eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VyX2lkIjoxLCJyb2xlIjoiU3VwZXIgQWRtaW4iLCJyb2xlX2lkIjoxLCJhZG1pbl9pZCI6MSwiaWF0IjoxNjUzMTMwNDMwLCJleHAiOjE2NTMxMzQwMzB9.hU41Zvx5uoaI7Nt46LaL8GFjTjAXUnet6GKhc5Ku4TA"}
                OnRequestProcessed(request.downloadHandler.text, true);
            }
            request.Dispose();
        }

        public IEnumerator CheckLogin(string url, string email_id, string deviceid, Action<checkstatus> callback)
        {
            WWWForm checkLoginForm = new WWWForm();
            checkLoginForm.AddField("email", email_id);
            checkLoginForm.AddField("device_id", deviceid);
            Debug.Log(deviceid+"ididididid");

            using (UnityWebRequest request = UnityWebRequest.Post(url, checkLoginForm))
            {
                yield return request.SendWebRequest();

                if (request.result != UnityWebRequest.Result.Success )
                {
                    Debug.Log(request.error);
                    Debug.LogError("this was a faliure");
                }
                else
                {
                    Debug.Log("Checking Logges in " + request.downloadHandler.text);
                    
                    checkstatus result = JsonConvert.DeserializeObject<checkstatus>(request.downloadHandler.text);  
                    // if(result.status == 200)
                    // {
                    //     //callback(result);
                    //     LoginScript.Instance.ForcedLoggedIn();
                    // }
                    callback(result);

                }     
            }


        }
        public Text showerror;
        // http://139.59.92.165:5000/auth/checklogin
        // {
        //     email ="GK00230010"
        //     device ="a28350efc555940a5f2e3728e4f9759accfcab953c0d232147161b457c3131b7";

        // }

        public IEnumerator LoginAPI(string url, string json, string email, string password,string id )
        {
            Debug.Log("url>>>>>  " + url);
            Debug.Log("PostRequest " + json);
            var request = new UnityWebRequest(url, "POST");
            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(json);
            request.uploadHandler = new UploadHandlerRaw(jsonToSend);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");
            // //
            // WWWForm form = new WWWForm();
            // string email = "GK" + PlayerID.text;
            // string password  = password;
            // string newDeviceId = device_Id;
            // form.AddField("email", email);
            // form.AddField("device_id",newDeviceId);


            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(request.error);
            }
            else
            {
                LoginStatus n = JsonUtility.FromJson<LoginStatus>(request.downloadHandler.text);
                LoginStatus nDeserialized = JsonConvert.DeserializeObject<LoginStatus>(request.downloadHandler.text);

                Debug.Log("========= DESERIALIZED N DATA =========");
                Debug.Log("ID: "+n.data.id);
                Debug.Log("Distributor ID "+n.data.distributor_id);
                Debug.Log("User ID "+n.data.user_id);
                Debug.Log("Username "+n.data.username);
                Debug.Log("IMEI N0: "+n.data.IMEI_no);
                Debug.Log("Device: "+n.data.device);
                Debug.Log("Last Logged In: "+n.data.last_logged_in);
                Debug.Log("Last Logged Out: "+n.data.last_logged_out);
                Debug.Log("IsBlocked: "+n.data.IsBlocked);
                Debug.Log("Password: "+n.data.password);
                Debug.Log("Created At: "+n.data.created_at);
                Debug.Log("Updated At: "+n.data.updated_at);
                Debug.Log("Active: "+n.data.active);
                Debug.Log("Coins: "+n.data.coins);
                Debug.Log("==================");


                //Debug.Log("data from login" + request.downloadHandler.text);
                // Debug.LogError("response  " + n.data.coins);
                // Debug.LogError("Coins  " + n.coins);
                Debug.Log("this is the response for force login" + n.status);
                Debug.LogError("Debugging Login Status " + JsonConvert.DeserializeObject<LoginStatus>(request.downloadHandler.text));
                showerror.text = n.status;
                Debug.Log("The value of 10 " + n);
                if(n.status == "200")
                {

                    //LoginScript.Instance.ErrorMessage.enabled = false;
                    // Debug.LogError(url + " json response: " + request.downloadHandler.text);
                    // Debug.LogError("status  " + n.status );
                    PlayerPrefs.SetString("email", LoginScript.Instance.PlayerID.text);
                    PlayerPrefs.SetString("password", password);
                    //
                    LoginScript.Instance.OnLoginSuccess();
                    LoginScript.Instance.LoginPanel.SetActive(false);
                    // LoginScript.Instance.ShowLoginUI();
                    if( n.data.coins < 0)
                    {
                        PlayerPrefs.SetFloat("points", 0);
                    }
                    else if(n.data.coins >= 0)
                    {
                        PlayerPrefs.SetFloat("points", n.data.coins);
                    }
                    LoginScript.Instance.fromlogin = false;
                    onlinechecker.instant.storedata(email,id);
                    //savebinary.LoadPlayerroulette();
                    SceneManager.LoadScene("MainScene");
                    PlayerPrefs.SetString("roulettepaused","false");
                    PlayerPrefs.SetString("roulettestarted","true");
                    PlayerPrefs.SetString("triplestarted","true");
                    PlayerPrefs.SetString("andarstarted","true");
                    PlayerPrefs.SetString("funstarted","true");
                    PlayerPrefs.SetString("funtargetround","false");
                    PlayerPrefs.SetString("triplepaused","false");
                    PlayerPrefs.SetString("sevenpaused","false");
                    PlayerPrefs.SetString("andarpaused","false"); 
                    //HomeScript.Instance.ShowHomeUI();
                    //StartCoroutine(settingonline());

                    //StartCoroutine(settingoffline());
                    StartCoroutine(LoginScript.Instance.forcefullylogin());
                    //LoginScript.Instance.for();
                }
                else
                {
                    //LoginScript.Instance.ErrorMessage.enabled = true;
                    AndroidToastMsg.ShowAndroidToastMessage("Invalid Credentials");
                    LoginScript.Instance.loginMessage.text = "Please Enter Ten Digit LoginID";
                    StartCoroutine(LoginScript.Instance.LoginMessageBlink());
                    LoginScript.Instance.PlayerID.text = "";
                    LoginScript.Instance.Password.text = "";
                    // Debug.LogError("login failed");
                }
            }
            request.Dispose();
        }
        public IEnumerator settingonline()
        {
            WWWForm form = new WWWForm();
            string playername = "GK"+PlayerPrefs.GetString("email");
            form.AddField("email", playername);
            using (UnityWebRequest www = UnityWebRequest.Post(setplayeronline, form))
            {
                yield return www.SendWebRequest();

                if (www.result != UnityWebRequest.Result.Success)
                {
                    Debug.Log(www.error);
                }
                else{
                    Debug.Log("Sucess//Sucess//Sucess//Sucess//Sucess//Sucess//Sucess//Sucess//Sucess//Sucess//");
                }
            }
        }
        public IEnumerator settingoffline()
        {
            WWWForm form = new WWWForm();
            string playername = "GK"+PlayerPrefs.GetString("email");
            form.AddField("email", playername);
            using (UnityWebRequest www = UnityWebRequest.Post(setplayeroffline, form))
            {
                yield return www.SendWebRequest();

                if (www.result != UnityWebRequest.Result.Success)
                {
                    Debug.Log(www.error);
                }
                else{
                    Debug.Log("Offline//Offline//Offline//Offline//Offline//Offline//Offline//Offline//Offline//Offline//");
                }
            }
        }
        private void OnApplicationQuit() {
            StartCoroutine(settingoffline());
        }

        public void DownloadSprite(string url, Action<Sprite> OnDownloadComplete)
        {
            StartCoroutine(LoadFromWeb(url, OnDownloadComplete));
        }

        IEnumerator LoadFromWeb(string url, Action<Sprite> OnDownloadComplete)
        {
            UnityWebRequest webRequest = new UnityWebRequest(url);
            DownloadHandlerTexture textureDownloader = new DownloadHandlerTexture(true);
            webRequest.downloadHandler = textureDownloader;
            yield return webRequest.SendWebRequest();
            if (!(webRequest.isNetworkError || webRequest.isHttpError))
            {
                Texture2D texture = textureDownloader.texture;
                Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero, 1f);
                OnDownloadComplete(sprite);
            }
            else
            {
                Debug.Log("failed to download image");
            }
        }
        public void DownloadTexture(string url, Action<Texture> OnDownloadComplete)
        {
            StartCoroutine(LoadFromWeb(url, OnDownloadComplete));
        }
        IEnumerator LoadFromWeb(string url, Action<Texture> OnDownloadComplete)
        {
            UnityWebRequest webRequest = new UnityWebRequest(url);
            DownloadHandlerTexture textureDownloader = new DownloadHandlerTexture(true);
            webRequest.downloadHandler = textureDownloader;
            yield return webRequest.SendWebRequest();
            if (!(webRequest.isNetworkError || webRequest.isHttpError))
            {
                OnDownloadComplete(textureDownloader.texture);
            }
            else
            {
                Debug.Log("failed to download image");
            }
        }
        public int GetVersionCode()
        {
            return FetchVersionCode();
        }
        public static int FetchVersionCode()
        {
            try
            {
                AndroidJavaClass contextCls = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
                AndroidJavaObject context = contextCls.GetStatic<AndroidJavaObject>("currentActivity");
                AndroidJavaObject packageMngr = context.Call<AndroidJavaObject>("getPackageManager");
                string packageName = context.Call<string>("getPackageName");
                AndroidJavaObject packageInfo = packageMngr.Call<AndroidJavaObject>("getPackageInfo", packageName, 0);
                return packageInfo.Get<int>("versionCode");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return 2;
            }
        }
    }

    [Serializable]
    public class LoginStatus
    {
       public string status;
       public string message;
       public int coins;
       public data data;
    }

    [Serializable]
    public class data
    {
        public int id;
        public string distributor_id;
        public string user_id;
        public string username;
        public string IMEI_no;
        public string device;
        public string last_logged_in;
        public string last_logged_out;
        public int IsBlocked;
        public string password;
        public string created_at;
        public string updated_at;
        public int active;
        public int coins;
    }

    public class checkstatus
    {
        // public int status;
        // public string message;
        public bool isAlreadyLoggedIn;
    }
}