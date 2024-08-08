//using System.Diagnostics;
using Com.BigWin.WebUtils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System.Security.Cryptography;
using UnityEngine.Networking;
using System.Text;

public class LoginScript : MonoBehaviour
{
    public static LoginScript Instance;
    public GameObject LoginPanel;
   // public GameObject LoginPanel1;
    public InputField Password;
    public InputField PlayerID;
    // public GameObject HomeCanvas, SignUpPanel;
    public GameObject Pop_uP_Force_Login;
    string LoginURL =  "http://139.59.92.165:5000/auth/login";//"http://139.59.92.165:5000/auth/login";//"http://16.171.166.176:5000/auth/login"; //"https://jeetogame.in/jeeto_game/WebServices/loginPassword";    
    string GuestURL = "https://jeetogame.in/jeeto_game/WebServices/Guestlogin";
    string checkplayer ="http://139.59.92.165:5000/user/CheckPlayer";
    //string setplayeronline ="http://139.59.60.118:5000/user/SetplayerOnline";
    string setplayeroffline = "http://139.59.92.165:5000/user/SetplayerOffline";
    const string checkloginstatusUrl = "http://139.59.92.165:5000/auth/checklogin";//beforeitwas check-login
    public Text ErrorMessage;
    public Text loginMessage;
    //public Text showerror;

    string device_Id;

    


    private void Start()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        // GenerateUniqueIdentifier();\
        device_Id = GenerateDeviceIdClass.GenerateUniqueIdentifier1();
        loginMessage.text = "Please Now Login";
        StartCoroutine(LoginMessageBlink());
    }
    private void Awake()
    {
        Instance = this;
    }
    public void ShowLoginUI()
    {
        if (PlayerPrefs.GetString("email", "") != "")
        {
            LoginPanel.SetActive(false);
        }
        else
        {
            LoginPanel.SetActive(true);
        }
      //  LoginPanel1.SetActive(false);
    }
    public string uniqueIdentifier ;
    public string GenerateUniqueIdentifier()
    {
        // Generate a unique identifier using device-specific information
        string deviceModel = SystemInfo.deviceModel;
        string manufacturer = SystemInfo.deviceName;
        //string androidID = UnityEngine.Android.AndroidDevice.uniqueIdentifier; // Requires "WRITE_SETTINGS" permission in AndroidManifest.xml

        // Generate a random value to ensure uniqueness
        //string randomValue = Guid.NewGuid().ToString();

        // Concatenate the device-specific information and random value to create the unique identifier
        uniqueIdentifier = string.Format("{0}_{1}", deviceModel, manufacturer);//, androidID, randomValue);
        Debug.Log("\a\a\a\a\a\a\a\a\a\a\a\a\a\a\a\a\a\a\a\a\a\a\a\a\a\a\a\a"+ deviceModel);
        //PlayerPrefs.SetString()

        return uniqueIdentifier;
    }
    
    public void ForcedLoggedIn()
    {
        string _playerID = "GK" + PlayerID.text;
        LoginForm form = new LoginForm(_playerID, Password.text, device_Id);
        StartCoroutine(WebRequestHandler.instance.LoginAPI(LoginURL, JsonUtility.ToJson(form), _playerID, Password.text, device_Id ));
    }

    public IEnumerator forcefullylogin()
    {
        WWWForm form = new WWWForm();
        string email = "GK" + PlayerID.text;
        string newDeviceId = device_Id;
        form.AddField("email", email);
        form.AddField("newDeviceId",newDeviceId);
        using (UnityWebRequest www = UnityWebRequest.Post("http://139.59.92.165:5000/auth/forcelogin", form))
            {
                yield return www.SendWebRequest();
                
                if (www.result != UnityWebRequest.Result.Success)
                {
                    Debug.LogError(www.error);
                }
                else
                {
                    Debug.Log("Responce for forcelogin  " +www.downloadHandler.text);
                    
                    
                    //winround = AUI.balance+winPoint;
                }
            }

    }
    public bool fromlogin;

    public void LoginBtn()
    {
        Debug.Log("Login Button");
        

        if ( !string.IsNullOrEmpty(PlayerID.text) && !string.IsNullOrEmpty(Password.text) )
        {
            string _playerID = "GK" + PlayerID.text;
            fromlogin = true;
            StartCoroutine(
                WebRequestHandler.instance.CheckLogin(checkloginstatusUrl, _playerID, device_Id, 
                (result) => 
                {
                    if (fromlogin)
                    {
                        if (result.isAlreadyLoggedIn)
                        {
                            forcefullylogin();
                            ForcedLoggedIn();
                            //Pop_uP_Force_Login.SetActive(true);
                        }
                        else
                        {
                            ForcedLoggedIn();
                        }
                    }
                    else
                    {
                        if(result.isAlreadyLoggedIn)
                        {
                            Debug.LogError("it should throwout now");
                        }
                    }
                } )
            );
        }   
        
    }
    public IEnumerator checking()
    {

        WWWForm form = new WWWForm();
        string playername = "GK"+PlayerID.text;//PlayerPrefs.GetString("email");
        form.AddField("email", playername);
        using (UnityWebRequest www = UnityWebRequest.Post(checkplayer, form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                checkplayer result = JsonUtility.FromJson<checkplayer>(www.downloadHandler.text);
                Debug.Log("result   " + result.status );
                if(result.status ==404)
                {
                    LoginForm formn = new LoginForm(playername, Password.text,GenerateDeviceIdClass.GenerateUniqueIdentifier1());
                    //StartCoroutine(WebRequestHandler.instance.LoginAPI(LoginURL, JsonUtility.ToJson(formn), playername, Password.text));
                }
                else if(result.status == 200)
                {
                    AndroidToastMsg.ShowAndroidToastMessage("Player logged in another device");
                }
            }
        }
    }

    public void OnLoginSuccess()
    {
        UserDetail.Name = PlayerPrefs.GetString("email");
    }

    private void OnLoginRequestProcessed(string json, bool success)
    {
        LoginFormRoot responce = JsonUtility.FromJson<LoginFormRoot>(json);
        // Debug.LogError("status  " + responce.response.status);

        // if (responce.response.status)
        // {
        //     PlayerPrefs.SetString("EmailId", PlayerID.text);
        //     PlayerPrefs.SetString("password", Password.text);
        //     PlayerPrefs.Save();

        //     // UserDetail.UserId = responce.response.data.user_id;
        //     // UserDetail.ID = responce.response.data.id.ToString();
        //     // UserDetail.Name = responce.response.data.name;
        //     // UserDetail.ProfileId = responce.response.data.profile_id;
        //     // UserDetail.MobileNo = responce.response.data.mobile_number;
        //     // UserDetail.Balance = responce.response.data.chip_balance;
        //     // UserDetail.refer_id = responce.response.data.refer_id;          
        //     LoginPanel.SetActive(false);
        //   //  LoginPanel1.SetActive(false);
        //     HomeScript.Instance.ShowHomeUI();
        // }
        // else
        // {
        //     Debug.Log("Unable to login");
        // }
    }

    public void ForgetPasswordBtn()
    {
        ForgetPasswordScript.Instance.ShowForgetPasswordUI();
    }

    public void LoginScreenBtn()
    {
        LoginPanel.SetActive(false);
       // LoginPanel1.SetActive(true);
    }

    public void PlayGuestBtn()
    {
        string device_id = SystemInfo.deviceUniqueIdentifier;
        GuestForm form = new GuestForm(device_id, "en");
        //   WebRequestHandler.instance.Post(GuestURL, JsonUtility.ToJson(form), OnGuestRequestProcessed); 
        gameObject.SetActive(false);
        // HomeCanvas.SetActive(true);
        AndroidToastMsg.ShowAndroidToastMessage("Guest Login");
    }

    public void CloseUI()
    {
        LoginPanel.SetActive(true);
     //   LoginPanel1.SetActive(false);
    }

    private void OnGuestRequestProcessed(string json, bool success)
    {
        GuestRes responce = JsonUtility.FromJson<GuestRes>(json);

        if (responce.response.status)
        {
            PlayerPrefs.SetString("GuestData", json);
            PlayerPrefs.Save();            
            UserDetail.UserId = responce.response.data.user_id;
            UserDetail.ID = responce.response.data.id.ToString();
            UserDetail .Name = responce.response.data.name;
            UserDetail.ProfileId = responce.response.data.profile_id;
            UserDetail.Balance = responce.response.data.chip_balance;
            UserDetail.refer_id = responce.response.data.refer_id;
            LoginPanel.SetActive(false);
         //   LoginPanel1.SetActive(false);
            HomeScript.Instance.ShowHomeUI();
        }
    }  

    public IEnumerator LoginMessageBlink()
    {
        for(int i = 0; i < 4; i++)
        {
            loginMessage.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.5f);
            loginMessage.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.5f);
        }
    }

    public void NewUser()
    {
        LoginPanel.SetActive(false);
        // SignUpPanel.SetActive(true);
    }
    public void closeapp()
    {
        Application.Quit();
    }


    
}
[Serializable]
public class LoginForm
{
    public string email;
    public string password;
    public string device_id;

    public LoginForm(string email, string password,string key)
    {
        this.email = email;
        this.password = password;
        this.device_id = key;
    }
}
[Serializable]
public class LoginFormData
{
    public int id;
    public int user_id;
    public string name;
    public int chip_balance;
    public int profile_id;
    public string refer_id;
    public string mobile_number;
}
[Serializable]
public class LoginFormResponse
{
    public int status;
    public string message;
    public LoginFormData data;
}
[Serializable]
public class LoginFormRoot
{
    public LoginFormResponse response;
}
[Serializable]
public class GuestForm
{
    public string device_id;
    public string language;

    public GuestForm(string device_id, string language)
    {
        this.device_id = device_id;
        this.language = language;
    }
}
[Serializable]
public class GuestData
{
    public string id;
    public int user_id;
    public string name;
    public int chip_balance;
    public int profile_id;
    public string refer_id;
}
[Serializable]
public class GuestResponse
{
    public bool status;
    public string message;
    public GuestData data;
}
[Serializable]
public class GuestRes
{
    public GuestResponse response;
}
public class details
{
    public string email;
}
public class checkplayer
{
    public int status;
    public string message;
    public bool player;
}
public class checker
{
    public int status;
    public string message;
}

// public static class GenerateDeviceIdClass
// {
//     public static string GenerateUniqueIdentifier1()
//     {
//         // Get device-specific information
//         string deviceModel = SystemInfo.deviceModel;
//         string deviceName = SystemInfo.deviceName;
//         string uniqueIdentifier = string.Format("{0}_{1}", deviceModel, deviceName);

//         // Create a SHA256 hash
//         using (SHA256 sha256Hash = SHA256.Create())
//         {
//             byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(uniqueIdentifier));
//             StringBuilder stringBuilder = new StringBuilder();

//             // Convert hash bytes to string
//             for (int i = 0; i < bytes.Length; i++)
//             {
//                 stringBuilder.Append(bytes[i].ToString("x2")); // Convert to hexadecimal string
//             }

//             return stringBuilder.ToString();
//         }
//     }
// }