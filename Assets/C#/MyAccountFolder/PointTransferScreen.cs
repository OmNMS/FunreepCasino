    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using Newtonsoft.Json;
using System;
using UnityEngine.Networking;
using server;
using LobbyScripts;

namespace Accountscript
{
    public class PointTransferScreen : MonoBehaviour
    {
        public InputField toAccountInputField;
        public InputField pwdInputField;
        public InputField amountInputField;
        public Button okBtn;
        [SerializeField] public Button backBtn;
        public Text mainBalance;
        private bool isTransationFinished;
        bool isDataLoaded;

        // Start is called before the first frame update
        void Start()
        {
            isTransationFinished = true;
            //UpdateBalane();
            okBtn.onClick.AddListener(OnClickOKButton);
        }

        private void OnClickOKButton()
        {
            string _playername = "GK" + PlayerPrefs.GetString("email");
            if (!isTransationFinished)
            {
                Debug.Log("please wait");
                AndroidToastMsg.ShowAndroidToastMessage("please wait");
                return;
            }
            if (string.IsNullOrEmpty(amountInputField.text)|| int.Parse(amountInputField.text) > PlayerPrefs.GetFloat("points"))
            {
                Debug.Log("invalid amount");
                AndroidToastMsg.ShowAndroidToastMessage("invalid amount");
                return;
            }
            if (string.IsNullOrEmpty(pwdInputField.text))
            {
                Debug.Log("invalid password");
                AndroidToastMsg.ShowAndroidToastMessage("invalid amount");
                return;
            }
            if (!ValidateNumber(amountInputField.text, "account")) return;
            AndroidToastMsg.ShowAndroidToastMessage("Please wait");
            string amount = amountInputField.text.Trim();
            string accountNumber =  "GK"+toAccountInputField.text.Trim();
            string pwd = pwdInputField.text.Trim();
            isTransationFinished = false;
            int.TryParse( pwd, out int pass);
            int.TryParse( amount, out int amt);
            userData userData = new userData() 
            { 
                password = pass,
                points = amt, 
                receiverId = accountNumber,
                senderId = _playername 
            };

            // Debug.Log("user REQ   " + userData.password + "   " + userData.points + "    " + userData.receiverId + "   " +
            // userData.senderId  );

            Debug.Log(" Serialized data : " +  JsonConvert.SerializeObject(userData)  ); 
            Account_SocketRequest.instance.SendEvent(Constants.OnSendPoints, userData, (res) =>
            {
                BackEndData3<Status> filterResponse = JsonConvert.DeserializeObject<BackEndData3<Status>>(res);
                Debug.Log("OnSend pt RES   " + res);

                var status = filterResponse.status;
                Status status1 = filterResponse.data;
                if (status1.status == 401)//invalid user
                {
                    OnInvalidUser(status1.message); return;
                }
                if (status1.status == 404)//invalid user
                {
                    Debug.Log("Receiver Not Exist");
                    AndroidToastMsg.ShowAndroidToastMessage("Receiver Not Exist");
                    //OnInvalidUser(status1.message); return;
                }
                Debug.Log(status1.message);
                AndroidToastMsg.ShowAndroidToastMessage(status1.message);
                if (status1.status == 401)//invalid user//00522841//4686
                {
                    OnInvalidUser(status1.message);return;
                }if (status == 200)
                {
                    UpdateBalane();
                    ResetUi();
                }
                    if (Application.platform != RuntimePlatform.Android)
                    {
                        Debug.Log(filterResponse.message);
                    }
                    else
                    {
                        AndroidToastMsg.ShowAndroidToastMessage(filterResponse.message);
                    }
                isTransationFinished = true;
            });
        }

        void UpdateBalane()
        {
            string _playername = "GK" + PlayerPrefs.GetString("email");
            object user = new { playerId = _playername };
            Debug.LogError("_id   " + _playername);
            Account_SocketRequest.instance.SendEvent(Constants.OnUserProfile, user, (json) =>
            {

                Debug.LogError("OnUserPRofile   " + json);
                BackEndData3<PlayerProfile> profile = JsonUtility.FromJson<BackEndData3<PlayerProfile>>(json);
                mainBalance.text = profile.data.coins.ToString();
            });
        }
        private void OnInvalidUser(string msg)
        {
            Debug.Log("invalid user");
            // dialogue.OnDialogHide = () =>
            // {
                Account_SocketRequest.instance.SendEvent(Constants.OnLogout);
                LoginScript.Instance.LoginPanel.SetActive(true);
            // };
        }

        private bool ValidateNumber(string sample, string nam)
        {
            sample=sample.Trim();
            if (string.IsNullOrEmpty(sample))
            {
                Debug.Log(nam + " is empty");
                AndroidToastMsg.ShowAndroidToastMessage(nam + " is empty");
                return false;
            }

            Debug.Log(sample);
            if (!sample.All(char.IsDigit))
            {
                Debug.Log(nam + " is invalid");
                AndroidToastMsg.ShowAndroidToastMessage(nam + " is invalid");
                return false;
            }
            return true;
        }

        private void ResetUi()
        {
            toAccountInputField.text = string.Empty;
            pwdInputField.text = string.Empty;
            amountInputField.text = string.Empty;
        }

        public void OnClickBackButton()
        {
            Debug.Log("Close button clicked");
            this.gameObject.SetActive(false);
        }
        
    }

[Serializable]
public class userData
{
    public string senderId;
    public string receiverId;
    public int points;
    public int password;
}

[Serializable]
public class ProfileData
{
    public string status ;
    public string message;
    public string points ;
    public string device ;
    public string user_id;
}


public class Profile
{
    public ProfileData user_data;
}

}
