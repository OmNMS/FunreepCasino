using UpDown7.Utility;
using Updown7.ServerStuff;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Shared;
using UnityEngine;
using UnityEngine.UI;
using Updown7.ServerStuff;
using Updown7.Gameplay;
using UnityEngine.SceneManagement;
using System;
using Newtonsoft.Json;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.Networking;
//using UnityEngine.ResourceManagement.ResourcProvider;


namespace Updown7.UI
{

    public class _7updown_UiHandler : MonoBehaviour
    {
        public static _7updown_UiHandler Instance;
        [SerializeField] Toggle chit10Btn;
        [SerializeField] Toggle chit50Btn;
        [SerializeField] Toggle chit100Btn;
        [SerializeField] Toggle chit500Btn;
        [SerializeField] Toggle chit1000Btn;
        [SerializeField] Toggle chit5000Btn;

        [SerializeField] TMP_Text leftBets;
        [SerializeField] TMP_Text middleBets;
        [SerializeField] TMP_Text rightBets;
        [SerializeField] TMP_Text usernameTxt;
        [SerializeField] TMP_Text totalbetsTxt;
        [SerializeField] public Text balanceTxt;

        [SerializeField] Button lobby;

        Dictionary<Spot, TMP_Text> betUiRefrence = new Dictionary<Spot, TMP_Text>();
        public TMP_Text PlayerBet;
        float playerbetvalue = 0;
        public Chip currentChip;
        _7updown_Timer timer;
        public GameObject mainUnite;
        public Image[] chip_select;
        public Button betOkBtn;
        public float balance;
        int amount_two_sixBets, amount_sevenBets, amount_eight_twelveBets;
        public  int totalBets;
        public bool isBetConfirmed, canPlaceBet;
        public static bool inGame = false;
        
        //public SceneHandler sceneHandler;

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            totalBets = 0;
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
            playerbetvalue = 0;
            PlayerBet.text = "0";
            currentChip = Chip.Chip10;
            betUiRefrence.Add(Spot.left, leftBets);
            betUiRefrence.Add(Spot.middle, middleBets);
            betUiRefrence.Add(Spot.right, rightBets);
            countdownPanel.gameObject.SetActive(false);
            placeBets.gameObject.SetActive(false);
            timer = mainUnite.GetComponent<_7updown_Timer>();
            //test.onClick.AddListener(() => StartCoroutine(StartCountDown()));
            SoundManager.instance.PlayBackgroundMusic();
            AddListeners();
            //ResetUi();
            // balance = 10000f;
            balance = PlayerPrefs.GetFloat("points");
            UpdateUi();
            if (sevenstorage.confirmed == true)
            {
                Debug.Log("reached here");
                //if(PlayerPrefs.GetInt("sevenrounds") !=c.RoundCount)
                //restore();
                isBetConfirmed = true;
                betOkBtn.interactable = false;
                canPlaceBet = false;
                
            }
            else
            {
                isBetConfirmed = false;
                canPlaceBet = true;
                betOkBtn.interactable = true;
            }
            //GameObject sceneHandler = GameObject.Find("SceneHandler").GetComponent<SceneHandler>();
            //StartCoroutine(Loading());
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
        // private void OnApplicationQuit() {
        //     StartCoroutine(settingoffline());
        // }

        IEnumerator StartBetting()
        {
            placeBets.gameObject.SetActive(true);
            placeBets.sprite = startBetting;
            yield return new WaitForSeconds(.5f);
            placeBets.gameObject.SetActive(false);
        }
        int leftTotalBets;
        int middleTotalBets;
        int rightTotalBets;
        public void AddBotsBets(Spot spot, Chip chip)
        {
            string betValue = string.Empty;
            switch (spot)
            {
                case Spot.left:
                    leftTotalBets += (int)chip;
                    betValue = leftTotalBets.ToString();
                    break;
                case Spot.middle:
                    middleTotalBets += (int)chip;
                    betValue = middleTotalBets.ToString();
                    break;
                case Spot.right:
                    rightTotalBets += (int)chip;
                    betValue = rightTotalBets.ToString();
                    break;
                default:
                    break;
            }
            UpdateUi();
        }

        public void BetValueSelect(int val)
        {
            if (val == 10)
            {
                currentChip = Chip.Chip10;
            }
            else if (val == 20)
            {
                currentChip = Chip.Chip20;
            }
            else if (val == 50)
            {
                currentChip = Chip.Chip50;
            }
            else if(val == 100)
            {
                currentChip = Chip.Chip100;
            }
            else if(val == 200)
            {
                currentChip = Chip.Chip200;
            }
            else if (val == 500)
            {
                currentChip = Chip.Chip500;
            }
            else if (val == 1000)
            {
                currentChip = Chip.Chip1000;
            }
            else if (val == 5000)
            {
                currentChip = Chip.Chip5000;
            }  
            else if (val == 10000)
            {
                //currentChip = Chip.Chip10000;
            }    
        }   


        public void AddPlayerBets()
        {
            //balance -= (float)currentChip;
            // balance -= 50;
            playerbetvalue += (float)currentChip;
            PlayerBet.text = playerbetvalue.ToString();
            UpdateUi();
        }
        public Text Left_total_text, Middle_total_text, Right_total_text;
        public void AddBets(Button BetsPlaced)
        {
            
           if (canPlaceBet == true &&(_7updown_RoundWinningHandler.Instance.winPoint == 0))
           {
                if (balance < (int)currentChip || balance <  totalBets)
                {
                 AndroidToastMsg.ShowAndroidToastMessage("not enough balance");
                 Debug.Log("not enough balance");
                 return;
                 
                }
                totalBets = totalBets + (int)currentChip;
                
 
                if(BetsPlaced.gameObject.tag == "amount_two_sixBets")
                {
                    Debug.Log("name  " + BetsPlaced.transform.GetChild(8).gameObject.name);
                    if((amount_two_sixBets + (int)currentChip) > 5000)
                    {
                        AndroidToastMsg.ShowAndroidToastMessage("Max bet limit reached");
                        return;
                    }
                    amount_two_sixBets += (int)currentChip;
                    BetsPlaced.transform.GetChild(9).GetComponent<Text>().text = amount_two_sixBets.ToString();
                    balance -= (int)currentChip;
                }
                else if(BetsPlaced.gameObject.tag == "amount_sevenBets")
                {
                    if((amount_sevenBets + (int)currentChip) > 5000)
                    {
                        AndroidToastMsg.ShowAndroidToastMessage("Max bet limit reached");
                        return;
                    }
                    amount_sevenBets += (int)currentChip;
                    BetsPlaced.transform.GetChild(8).GetComponent<Text>().text = amount_sevenBets.ToString();
                    balance -= (int)currentChip;
                }
                else if(BetsPlaced.gameObject.tag == "amount_eight_twelveBets")
                {
                    if((amount_eight_twelveBets + (int)currentChip) > 5000)
                    {
                        AndroidToastMsg.ShowAndroidToastMessage("Max bet limit reached");
                        return;
                    }
                    amount_eight_twelveBets += (int)currentChip;
                    BetsPlaced.transform.GetChild(9).GetComponent<Text>().text = amount_eight_twelveBets.ToString();
                    balance -= (int)currentChip;
                }
                UpdateUi();
            }
            else if(_7updown_RoundWinningHandler.Instance.winPoint != 0)
            {
                AndroidToastMsg.ShowAndroidToastMessage("Please collect the winnning amount first");
            }
        }

        public void OnBetsOk()
        {
            // if (totalBets == 0)
            // {
            //     // commentTxt.text = "Bets Are Empty";
            //     return;
            // }

            betOkBtn.interactable = false;

            isBetConfirmed = true;
            canPlaceBet= false;
            SendBets();
            UpdateUi();
        }

        public void SendBets()
        {
            
            string _playerName = "GK" + PlayerPrefs.GetString("email");
            //Debug.Log("Send bets called...  " + amount_two_sixBets + "   " + amount_sevenBets + "   " + amount_eight_twelveBets);

            bets data = new bets
            {
                playerId = _playerName,
                bet_amount_two_six = amount_two_sixBets,
                bet_amount_seven = amount_sevenBets,
                bet_amount_eight_twelve = amount_eight_twelveBets
            };
            if(totalBets >0)
            {
                store();
            }
            //Debug.Log(new JSONObject(JsonConvert.SerializeObject(data)));
            Debug.Log(new JSONObject(JsonConvert.SerializeObject(data).ToString()));
            ServerRequest.instance.socket.Emit(Events.OnBetsPlaced, new JSONObject(JsonConvert.SerializeObject(data)));
            // PostBet(data);
            canPlaceBet = false;
        }
        public void Resetbets()
        {

        }
        public void repeat()
        {
            restore();
        }

        public void bet_Response(object data)
        {
            bet_response res = UpDown7.Utility.Utility.GetObjectOfType<bet_response>(data);
            balance = res.data.balance;
            balanceTxt.text = balance.ToString();
        }

        public class bet_response
        {
           public int status;
            public string message;
            public Data data;
        }
        public class Data
        {
            public float balance;
            public string playerId;
        }


        public bool IsEnoughBalancePresent()
        {
            return balance - (float)currentChip > 0;
        }
        // public void UpDateBalance(float amount)
        // {
        //     StopLoading();
        //     isLoading = false;
        //     // balance = amount;
        //     balance = 10000f;
        //     UpdateUi();
        // }
        // public _7updown_LeftSideMenu sideMenu;
        private void AddListeners()
        {
            // chit10Btn.onValueChanged.AddListener((isOn) =>
            // {
            //     currentChip = Chip.Chip10;
            // });
            // chit50Btn.onValueChanged.AddListener((isOn) =>
            // {
            //     currentChip = Chip.Chip50;
            // });
            // chit100Btn.onValueChanged.AddListener((isOn) =>
            // {
            //     currentChip = Chip.Chip100;
            // });
            // chit500Btn.onValueChanged.AddListener((isOn) =>
            // {
            //     currentChip = Chip.Chip500;
            // });
            // chit1000Btn.onValueChanged.AddListener((isOn) =>
            // {
            //     currentChip = Chip.Chip1000;
            // });
            // chit5000Btn.onValueChanged.AddListener((isOn) =>
            // {
            //     currentChip = Chip.Chip5000;
            // });
            timer.onCountDownStart += () =>
            {
                if (timer.is_a_FirstRound) return;
                StartCoroutine(StartBetting());
            };
            timer.startCountDown += () =>
            {
                SoundManager.instance.PlayCountdown();
                StartCoroutine(StartCountDown());
            };
            // lobby.onClick.AddListener(() => sideMenu.ShowPopup());
        }


        public void GotoLobby()
        {
            //if(win)
            //inGame = true;
            _7updown_RoundWinningHandler.Instance.precheck();
            if(isBetConfirmed)
            {
                store();
            }
            if(!isBetConfirmed)
            {
                sevenstorage.confirmed = false;
            }
            PlayerPrefs.SetInt("sevenwin",int.Parse(_7updown_RoundWinningHandler.Instance.Winner_text.text));
            PlayerPrefs.SetInt("sevenrounds",_7updown_Timer.Instance.sevenround);
            //StartCoroutine(settingoffline());
            PlayerPrefs.SetString("sevenpaused", "false");
            PlayerPrefs.SetFloat("points",balance);
            ServerRequest.instance.socket.Emit(Events.onleaveRoom);
            //SceneManager.LoadScene("MainScene");
        }

       public void ResetUi()
        {
            canPlaceBet = true;
            playerbetvalue = 0;
            PlayerBet.text = "0";
            leftTotalBets = 0;
            middleTotalBets = 0;
            rightTotalBets = 0;
            amount_two_sixBets = amount_sevenBets = amount_eight_twelveBets = 0;
            leftBets.text = middleBets.text = rightBets.text = Left_total_text.text = Middle_total_text.text = Right_total_text.text = string.Empty;
        }
        // public void SetBets(int left,int middle,int right)
        // {
        //     leftTotalBets = left;
        //     middleTotalBets = middle;
        //     rightTotalBets = right;
        //     UpdateUi();
        // }
       public void UpdateUi()
        {
            leftBets.text = leftTotalBets.ToString();
            middleBets.text = middleTotalBets.ToString();
            rightBets.text = rightTotalBets.ToString();
            balanceTxt.text = balance.ToString();
            totalbetsTxt.text = totalBets.ToString();
            usernameTxt.text ="000"+ LocalPlayer.deviceId;
        }

        public void store()
        {
            //sevenstorage.winningAmount = int.Parse(winner)
            //sevenstorage.winningAmount = int.Parse(_7updown_RoundWinningHandler.Instance.Winner_text.text);
            sevenstorage.confirmed = isBetConfirmed;

            sevenstorage.left = amount_two_sixBets;
            sevenstorage.right = amount_eight_twelveBets;
            sevenstorage.center = amount_sevenBets;
        }
        public void restore()
        {
            if(sevenstorage.left+sevenstorage.right+sevenstorage.center > balance)
            {
                Debug.Log("Insufficent balance");
                AndroidToastMsg.ShowAndroidToastMessage("Insufficent balance");
                return;
            }
            if(sevenstorage.left > 0)
            {
                amount_two_sixBets = sevenstorage.left;
                Transform templ = GameObject.Find("T Left Part").GetComponent<Transform>();
                templ.transform.GetChild(9).GetComponent<Text>().text = amount_two_sixBets.ToString();
            }
            if(sevenstorage.right > 0)
            {
                amount_eight_twelveBets = sevenstorage.right;
                Transform tempr = GameObject.Find("T Right Part").GetComponent<Transform>();
                tempr.transform.GetChild(9).GetComponent<Text>().text = amount_eight_twelveBets.ToString();
            }
            if(sevenstorage.center > 0)
            {
                amount_sevenBets = sevenstorage.center;
                Transform tempc = GameObject.Find("T Middle").GetComponent<Transform>();
                tempc.transform.GetChild(8).GetComponent<Text>().text = amount_sevenBets.ToString();
            }
            totalBets = amount_eight_twelveBets+amount_sevenBets+amount_two_sixBets;
            // if (true)
            // {
            //     balance = balance - totalBets;
            //     balanceTxt.text = balance.ToString();
            // }
            //Transform templ = GameObject.Find("T Left Part").GetComponent<Transform>();
            
            
            //amount_eight_twelveBets = sevenstorage.right;
            

            //BetsPlaced.transform.GetChild(8).GetComponent<Text>().text = amount_two_sixBets.ToString();
            
            //BetsPlaced.transform.GetChild(8).GetComponent<Text>().text = amount_sevenBets.ToString();
        }


        [SerializeField] GameObject messagePopUP;
        [SerializeField] TMP_Text msgTxt;
        public void ShowMessage(string msg)
        {
            messagePopUP.SetActive(true);
            msgTxt.text = msg;
        }
        public void HideMessage()
        {
            messagePopUP.SetActive(false);
            msgTxt.text = string.Empty;
        }
        int rounds;
        int currentround;

       bool isPause;
        void OnApplicationPause(bool hasFocus)
        {
            isPause = hasFocus;
           
            //if (Application.isEditor) return;
            // {
                
            if(isPause)
            {
                Debug.Log("Focus True:" +hasFocus);
                StopAllCoroutines();
                ServerResponse.instance.socketoff();
                store();
                PlayerPrefs.SetString("sevenpaused", "true");
                PlayerPrefs.SetInt("sevenrounds",timer.sevenround);
                //rounds = PlayerPrefs.GetInt("roundcount");
                //7Up&DownServerResponse.Instance.socketoff();
                
                // AndarBahar_ServerResponse.Instance.socketoff();
                //SceneManager.LoadScene(1);
                //AndarBahar_ServerRequest.instance.socket.Emit(Utility.Events.onleaveRoom); //leave room;
            }
            if (!isPause)
            {
                //currentround = PlayerPrefs.GetInt("roundcount");
                Debug.Log("Focus False:" +hasFocus);
                
                PlayerPrefs.SetInt("sreload",1);
                //ServerRequest.instance.socket.Emit(Utility.Events.onleaveRoom); 
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                //Debug.Log("rrrrrrrrrrrrrrround" + currentround);
                //Addressables.LoadSceneAsync("7Up&Dwon1", UnityEngine.SceneManagement.LoadSceneMode.Single, true);
                //if(currentround != rounds )
                //{
                // Addressables.LoadSceneAsync(SceneManager.GetActiveScene().name, UnityEngine.SceneManagement.LoadSceneMode.Single, true);
                //}
                
                // HomeScript.Instance.AndarBaharBtn();
                // AndarBahar_ServerResponse.Instance.socketOn();
                //SceneManager.LoadScene(SceneManager.GetActiveScene().name);     
                //AsyncOperationHandle            
            }
            // }
        }

        private void OnApplicationQuit()
        {
            PlayerPrefs.SetString("sevenpaused","false");
            StartCoroutine(settingoffline());
            LocalPlayer.balance = balance.ToString();
            LocalPlayer.SaveGame();StopAllCoroutines();
        }
        public void AutoHidehowMessage(string msg, int time)
        {
            StartCoroutine(HidePanel(time));
            messagePopUP.SetActive(true);
            msgTxt.text = msg;
        }
        IEnumerator HidePanel(int time)
        {
            yield return new WaitForSeconds(time);
            messagePopUP.SetActive(false);
        }
        public void OnPlayerWin(object o)
        {
            Win win = UpDown7.Utility.Utility.GetObjectOfType<Win>(o);
            balance += win.winAmount;
            UpdateUi();
        }
        public void showchip(int position)
        {
            for (int i = 0; i < chip_select.Length; i++)
            {
                if (i!= position)
                {
                    chip_select[i].color = new Color32(255,255,255,130);
                }
                else if (i== position)
                {
                    chip_select[i].color = new Color32(255,255,255,255);
                }
            }
        }
        public Sprite[] frams;
        public Sprite stopBetting;
        public Sprite startBetting;
        public Image countdownPanel;
        public Image placeBets;
        public float countdownSpeed=.25f;
        public Button test;

        IEnumerator StartCountDown()
        {
            countdownPanel.gameObject.SetActive(true);
            foreach (var item in frams)
            {
                countdownPanel.sprite = item;
                yield return new WaitForSeconds(countdownSpeed);
            }
            countdownPanel.gameObject.SetActive(false);
            //placeBets.gameObject.SetActive(true);
            //placeBets.sprite = stopBetting;
            yield return new WaitForSeconds(0.5f);
            placeBets.gameObject.SetActive(false);
        }

        public Image loadingpnel;
        public Image loadingImag;
        public Sprite[] loadingFrames;
        bool isLoading = true;
        IEnumerator Loading()
        {
            loadingpnel.gameObject.SetActive(true);
            foreach (var item in loadingFrames)
            {
                if (!isLoading) yield break;
                loadingImag.sprite = item;
                yield return new WaitForEndOfFrame();
            }
            StartCoroutine(Loading());
        }
        public void StopLoading()
        {
            StopCoroutine(Loading());
            loadingpnel.gameObject.SetActive(false);

        }

        [Serializable]
        public class bets
        {
            public string playerId;
            public int bet_amount_two_six, bet_amount_seven, bet_amount_eight_twelve;
        }
    }
    
}

[Serializable]
public class Win
{
    public float winAmount;
}

namespace DoubleChanceClasses
{
   public class OnWinAmount
   {
       public long RoundCount;
       public int win_no;
       public int winPoints;
   }

   public class BetConfirmation
    {
        public string status;
        public string message;
    }
}