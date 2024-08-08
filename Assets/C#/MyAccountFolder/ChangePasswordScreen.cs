using UnityEngine.UI;
using UnityEngine;
using Com.BigWin.Frontend.Data;
using Newtonsoft.Json;
using Accountscript;

public class ChangePasswordScreen : MonoBehaviour
{
    public InputField passwordInputField;
    public InputField newPasswordInputField;
    public InputField confirmPasswordInputField;
    public Button okBtn;

    // Start is called before the first frame update
    void Start()
    {
        AddListners();
    }

    private void AddListners()
    {
        okBtn.onClick.AddListener(OnClickOKButton);
    }

    private void OnClickOKButton()
    {
        string _playername = "GK" + PlayerPrefs.GetString("email");
        if (newPasswordInputField.text != confirmPasswordInputField.text)
        {

            if (Application.platform != RuntimePlatform.Android)
            {
                // dialogue.Show("password does not matched");
                Debug.Log("password does not matched");
            }
            else
            {
                AndroidToastMsg.ShowAndroidToastMessage("password does not matched");
            }
            return;
        }
        if (newPasswordInputField.text == confirmPasswordInputField.text)
        {
            object o = new { user_id = _playername, old_password = passwordInputField.text.ToString(), new_password = newPasswordInputField.text };
            SocketRequest.intance.SendEvent(Constant.OnChangePassword, o, (res) =>
                {
                    BackEndData3<ChangePasswordResponce> filterResponse = JsonConvert.DeserializeObject<BackEndData3<ChangePasswordResponce>>(res);
                    if (Application.platform != RuntimePlatform.Android)
                    {
                        // dialogue.Show(filterResponse.message);
                        Debug.Log(filterResponse.message);
                    }
                    else
                    {
                        AndroidToastMsg.ShowAndroidToastMessage(filterResponse.message);
                    }
                    ResetUi();
                });
            //webRequestHandler.Post(Constant.CHANGE_PASSWORD_URL, JsonUtility.ToJson(form), OnChangePasswordRequestProcessed);
        }
        else
        {
            // dialogue.Show(Constant.PASSWORD_NOT_MATCHED);
            Debug.Log(Constant.PASSWORD_NOT_MATCHED);
        }
    }

    void ResetUi()
    {
        passwordInputField.text = string.Empty;
        newPasswordInputField.text = string.Empty;
        confirmPasswordInputField.text = string.Empty;
    }

}
