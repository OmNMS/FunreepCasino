using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Facebook.Unity;
using UnityEngine.UI;

public class FacebookAuth : MonoBehaviour
{
    // [SerializeField] UIManager uiManager;
  //  [SerializeField] HomeManager homeManager;
    public Image[] dpThatWillChange;
    public Text NameTXT;
    public Text IDText;
   // public GameObject homePanel;
    private void Awake()
    {
        if (!FB.IsInitialized)
        {
            FB.Init(() =>
            {
                if (FB.IsInitialized)
                    FB.ActivateApp();
                // else
                // Debug.LogError("Couldn't Initialize");
            },
            isGameShown =>
            {
                if (!isGameShown)
                    Time.timeScale = 0;
                else
                    Time.timeScale = 1;
            });
        }
        else
            FB.ActivateApp();
    }

    // #region Login / Logout
    public void FacebookLogin()
    {
        var permissions = new List<string>() { "public_profile", "email", "user_friends" };
        FB.LogInWithReadPermissions(permissions, AuthCallBack);
    }

    private void AuthCallBack(ILoginResult result)
    {
        if (FB.IsLoggedIn)
        {
            var aToken = Facebook.Unity.AccessToken.CurrentAccessToken;
            PlayerPrefs.SetString("UserLoginInfo", "FaceBook");
            PlayerPrefs.SetString("UserID", aToken.UserId);
            IDText.text = aToken.UserId;
            
            // Debug.Log(aToken.UserId);

            if (!PlayerPrefs.HasKey("UserName"))
            {
                FB.API("/me?fields=first_name", HttpMethod.GET, DisplayUserName);
                // FB.API("/me/picture?type=square&height=128&width=128", HttpMethod.GET, DisplayUserProfile);
                PlayerPrefs.SetString("UserCoin", "10");
                PlayerPrefs.SetString("UserProfilePicIndex", "2");
            }

            //  homePanel.SetActive(true);
            gameObject.SetActive(false);

       //     homeManager.LobbyDetails();
        }
        else
        {
            // Debug.Log("User Cancelled Login");
        }
    }

    public void FacebookLogout()
    {
        if (PlayerPrefs.HasKey("UserLoginInfo"))
        {
            if (PlayerPrefs.GetString("UserLoginInfo") == "FaceBook")
            {
                if (FB.IsLoggedIn)
                    FB.LogOut();
                // uiManager.DelSaveData();
            }
        }
    }

    void DisplayUserName(IResult result)
    {
        if (result.Error == null)
        {
            string name = result.ResultDictionary["first_name"].ToString();
            Debug.Log(name);
            PlayerPrefs.SetString("UserName", name);
            NameTXT.text = name;
        }
    }

    void DisplayUserProfile(IGraphResult result)
    {
        if (result.Texture != null)
        {
            foreach (var item in dpThatWillChange)
            {
                item.sprite = Sprite.Create(result.Texture, new Rect(0, 0, 128, 128), new Vector2());
            }
        }
    }
    // #endregion
}
