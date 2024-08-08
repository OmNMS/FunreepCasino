using Com.BigWin.WebUtils;
using LobbyScripts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using KhushbuPlugin;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using LobbyScripts;
using Newtonsoft.Json;
using UnityEngine.Networking;

public class HomeScript : MonoBehaviour
{
    public static HomeScript Instance;
    public GameObject HomePanel;
    public GameObject LoginPanel;
    //Camra;
    public Text NameTxt;
    public Text IDTxt;
    public Image Profile;
    public Text Balance;
    public Button[] games;
    AsyncOperation DragonScene;
    AsyncOperation sevenupScene;
    [SerializeField] SceneHandler handler;
    // public Image LobbyAnimpnel;
    // public Sprite[] Lobbyframe;
    // public SceneHandler sceneHandler;
    string url = "http://139.59.92.165:5000/user/onbalance";//http://139.59.60.118:5000/user/onbalance";
    //string setplayeroffline = "http://139.59.60.118:5000/user/SetplayerOffline";


    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        // sceneHandler = GameObject.Find("SceneHandler").GetComponent<SceneHandler>();
        //sceneHandler = GameObject.Find("SceneHandler").GetComponent<SceneHandler>();
        //sceneHandler = GameObject.Find("SceneHandler").GetComponent<Transform>();
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        Balance.text = PlayerPrefs.GetFloat("points").ToString("F2");
        NameTxt.text = "FUN"+UserDetail.Name;
        IDTxt.text = "id:" + UserDetail.ID;
        //StartCoroutine(refresshing());
        // StartCoroutine(Loading());
        // refreshbutton();
        Debug.Log("balance at back end"+PlayerPrefs.GetFloat("points"));
        refreshbutton();
        foreach (Button item in games)
        {
        item.interactable = true;
        }
    }
    // IEnumerator Loading()
    // {
    //     foreach (var item in Lobbyframe)
    //     {
    //         LobbyAnimpnel.sprite = item;
    //         yield return new WaitForEndOfFrame();
    //     }
    //     StartCoroutine(Loading());
    // }
    IEnumerator refresshing()
    {
        refreshbutton();
        yield return new WaitForSeconds(5f);
        StartCoroutine(refresshing());
    }
    public void ShowHomeUI()
    {
        // Balance.text = UserDetail.Balance.ToString();
        Balance.text = PlayerPrefs.GetFloat("points").ToString("F2");
        NameTxt.text = UserDetail.Name;
        IDTxt.text = "id:" + UserDetail.ID;
        HomePanel.SetActive(true);
        
        //StartCoroutine(refresh());

        /*User user = new User() { 
        user_id = UserDetail.UserId, version_code = 1, language = "en" 
        };
        WebRequestHandler.instance.Post(Constants.ProfileURL,JsonUtility.ToJson(user), (data, status) => {
            if (!status) return;

            Profile profile = JsonConvert.DeserializeObject<Profile>(data);
            //print("balance is " + profile.response.data.chip_balance);
            Balance.text = profile.response.data.chip_balance.ToString();
            UserDetail.Balance = int.Parse(profile.response.data.chip_balance.ToString());
            PlayerPrefs.SetFloat("balance",profile.response.data.chip_balance);
            print("Profile Data " + data);
        });*/

    } 
    string setplayeroffline = "http://139.59.92.165:5000/user/SetplayerOffline";
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
    // void loaddata()
    // {
    //     savebinary
    // }
    public void refreshbutton()
    {
        
        StartCoroutine(refresh());
    }
    
    IEnumerator refresh()
    {
        WWWForm form = new WWWForm();
        form.AddField("email", "GK"+PlayerPrefs.GetString("email"));
        //Debug.Log("///////////////////////////////////"+PlayerPrefs.GetString("email"));

        using (UnityWebRequest www = UnityWebRequest.Post(url, form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                //Debug.Log(www.error);
            }
            else
            {
                Balanceupdate result = Newtonsoft.Json.JsonConvert.DeserializeObject<Balanceupdate>(www.downloadHandler.text);
                //Balance.text = "5000000";
                if(result.data.balance <0)
                {
                    Balance.text = "0";
                    PlayerPrefs.SetFloat("points",0);

                }
                else
                {
                    Balance.text = result.data.balance.ToString("F2");
                    PlayerPrefs.SetFloat("points",result.data.balance);
                }
                //Debug.Log("response for balance = "+www.downloadHandler.text);
                //PlayerPrefs.SetString("Points", result.data.balance.ToString());
                //int temp = int.P(PlayerPrefs.GetFloat("points"));
                
                //PlayerPrefs.SetString("Points", result.data.balance.ToString());
                //Debug.Log("temp:"+ temp+"//////////"+"balance"+ result.data.balance);
                
            }    
        }
        yield return new WaitForSeconds(5f);
        refreshbutton();

    }

    void Update() 
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void DragonBtn()
    {
        AndroidToastMsg.ShowAndroidToastMessage("Loading");
        
        AndroidToastMsg.ShowAndroidToastMessage("Loading");
        handler.loadAddressableScene("DragonTiger");
        // SceneManager.LoadScene("DragonScene");
    }
    public void Bingo()
    {
        //SceneManager.LoadScene("7Up&Down 1");
        //SceneManager.LoadScene("7Up&Download 1");
        //SceneManager.LoadScene("7Up&Download 1");    
        //sceneHandler.loadAddressableScene("7Up&Down1");
        AndroidToastMsg.ShowAndroidToastMessage("Loading");
        //DragonScene.allowSceneActivation = false;
        //sevenupScene.allowSceneActivation = true;
        SceneManager.LoadScene("Bingo");
    }
    public void JokerBonus()
    {
        //SceneManager.LoadScene("AndarBahar 1");
        
        //SceneManager.LoadScene("Andar_Baharload");
        //sceneHandler.loadAddressableScene("Andar_Bahar1");
        AndroidToastMsg.ShowAndroidToastMessage("Loading");
        handler.loadAddressableScene("FunCards");
        //SceneManager.LoadScene("JokerBonusGameScene");
    }
     public void AndarBahar()
    {
        //SceneManager.LoadScene("AndarBahar 1");
        
        //SceneManager.LoadScene("Andar_Baharload");
        //sceneHandler.loadAddressableScene("Andar_Bahar1");
        AndroidToastMsg.ShowAndroidToastMessage("Loading");
        SceneManager.LoadScene("AndarBahar 1");
    }

    public void TriplefunBtn()
    {
        //SceneManager.LoadScene("TripleFunload");
        //sceneHandler.loadAddressableScene("TripleFun");
        AndroidToastMsg.ShowAndroidToastMessage("Loading");
        handler.loadAddressableScene("FunMatka");
        //SceneManager.LoadScene("TripleFun2");
    }

    public void RouletteBtn()
    {
        //SceneManager.LoadScene("roulette_loading");
        handler.loadAddressableScene("Roulette");
        AndroidToastMsg.ShowAndroidToastMessage("Loading");
        // SceneManager.LoadScene("New_RouletteGame");
        //games[0].interactable = false;
        //SceneManager.LoadSceneAsync("New_RouletteGame", LoadSceneMode.Additive);
    }

    public void FunTargetBtn()
    {
        //SceneManager.LoadScene("Fun Target");
        //sceneHandler.loadAddressableScene("FunTarget");
        //SceneManager.LoadScene("FTloading 1");
        //games[1].interactable = false;
        AndroidToastMsg.ShowAndroidToastMessage("Loading");
        handler.loadAddressableScene("FunTarget");
        //SceneManager.LoadScene("Fun Target");

        //SceneManager.LoadSceneAsync("Fun Target", LoadSceneMode.Additive);
        // Camra.SetActive(false);
        // SceneManager.SetActiveScene(SceneManager.GetSceneByName("Fun Target"));
        // SceneManager.UnloadSceneAsync(SceneManager.GetSceneByName("MainScene"));
    }
    public void FunSorat()
    {
        
        AndroidToastMsg.ShowAndroidToastMessage("Loading");
        handler.loadAddressableScene("FunSorat");
        //SceneManager.LoadScene("Titli");
    }
    public void ShopBtn()
    {
        ShopScript.Instance.ShowShopUI();
    }
    public void ProfileBtn()
    {
        ProfileScript.Instance.ShowProfileUI();
    }
    public void settingBtn()
    {
        SettingScript.Instance.ShowSettingUI();
    }
    public void SupportBtn()
    {
        SupportScript.Instance.ShowSupportUI();
    }
    public void NoticeBtn()
    {
        NoticeScript.Instance.ShowNoticeUI();
    }
    public void MailBtn()
    {
        MailScript.Instance.ShowMailUI();
    }
    public void ShareBtn()
    {
        ReferAndEarnScript.Instance.ShowReferAndEarnUI();
    }
    public void SafeBtn()
    {
        SafeScript.Instance.ShowSafeUI();
    }
    public void RankBtn()
    {
        RankScript.Instance.ShowRankUI();
    }
    public void WithdrawBtn()
    {
        WithDrawScript.Instance.ShowWithDrawUI();
    }

    public void DiamondBtn()
    {
        //DiamondScript.Instance.ShowDiamondUI();
        DiamondOffer.Instance.ShowDiamondUI();
    }

    public void AgentBtn()
    {
        AgentScript.Instance.ShowDiamondUI();
    }
    public static string RandomString(int length)
    {
        string element = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        System.Random random = new System.Random();
        return new string((from s in Enumerable.Repeat(element, length)
                           select s[random.Next(s.Length)]).ToArray());
    }
    
    public void Logout()
    {
        
        Debug.Log("Logout called");
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene("Login");
        // LoginPanel.SetActive(true);
        // HomePanel.SetActive(false);
        
    }
}
public class LobbyData
{
    public string playerId;
}

public class ProfileData
{
    public string username;
    public int chip_balance ;
    public int safe_balance ;
    public int vip_level ;
    public string invite_friend_code ;
    public int team_name_updated ;
    public string image ;
    public List<object> refered_to_friend ;
    public string gender ;
    public string referal_bonus ;
    public bool account_verified ;
    public string Signup_bonus ;
    public string referral_bonus ;
    public int refered_by_status ;
    public string version_code ;
    public string apk_url ;
}

public class ProfileResponse
{
    public bool status;
    public string message;
    public ProfileData data;
}

[Serializable]
public class Profile
{
    public ProfileResponse response;
}

public class User
{
    public int user_id;
    public int version_code;
    public string language;
}

[Serializable]
public class PlayerProfile
{
    public int id;
    public string distributor_id;
    public string user_id;
    public string username;
    public object IMEI_no;
    public string device;
    public DateTime last_logged_in;
    public DateTime last_logged_out;
    public int IsBlocked;
    public string password;
    public DateTime created_at;
    public DateTime updated_at;
    public int active;
    public int coins;
}
public class balancechecker
{
    public int balance;
}
public class Balanceupdate
{
    public int status;
    public string message;
    public balancechecker data;
}
