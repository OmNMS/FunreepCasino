using UnityEngine.UI;
using UnityEngine;
using TMPro;
using Com.BigWin.Frontend.Data;
using System.Collections;
using System;
using System.Linq;
using System.Collections.Generic;
using m = UnityEngine.MonoBehaviour;
using UnityEngine.Events;
using FunTargetClasses;
using Newtonsoft.Json;
using SocketIO;
using UnityEngine.SceneManagement;
using FunTarget.ServerStuff;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using System.IO;
using UnityEngine.Networking;

namespace FunTarget.GamePlay
{
    public class FunTargetGamePlay : MonoBehaviour
    {
        
        public static FunTargetGamePlay Instance;
        #region Idenfiers
        [SerializeField] Button exitBtn;
        [SerializeField] Button betOkBtn;
        [SerializeField] Button clearBtn;
        [SerializeField] Button doubleBtn;
        [SerializeField] Button cancelSpecificBtn;
        [SerializeField] Animator center_anime;
        public Button takeBtn;
        [SerializeField] Button repeatBtn;
        //--------------------------------------------
        //[SerializeField] Toggle chipNo1Btn;
        //[SerializeField] Toggle chipNo5Btn;
        //[SerializeField] Toggle chipNo10Btn;
        //[SerializeField] Toggle chipNo50Btn;
        //[SerializeField] Toggle chipNo100Btn;
        //[SerializeField] Toggle chipNo500Btn;
        //[SerializeField] Toggle chipNo1000Btn;
        //[SerializeField] Toggle chipNo5000Btn;
        //-------------------------------------------------
        public GameObject turnonDisplay, cam, eventhandler;
        [SerializeField] GameObject arrow;
        [SerializeField] GameObject betButtons;
        [SerializeField] Button zeroBetBtn;
        [SerializeField] Button oneBetBtn;
        [SerializeField] Button twoBetBtn;
        [SerializeField] Button threeBetBtn;
        [SerializeField] Button fourBetBtn;
        [SerializeField] Button fiveBetBtn;
        [SerializeField] Button sixBetBtn;
        [SerializeField] Button sevenBetBtn;
        [SerializeField] Button eightBetBtn;
        [SerializeField] Button nineBetBtn;
        [SerializeField] Button closebtn;
        //---------------------------------------------------
        [SerializeField] TextMeshProUGUI timerText;
        [SerializeField] TextMeshProUGUI balanceText;
        [SerializeField] TextMeshProUGUI totalBetText;
        public TextMeshProUGUI winnigText;
        [SerializeField] TextMeshProUGUI[] previousWinNOsTxt;//this represent the red boxes
        [SerializeField] TextMeshProUGUI messageText;
        [SerializeField] AudioSource wheelAudio;
        [SerializeField] AudioSource timerAudio;
        [SerializeField] AudioSource winnerAudio;
        [SerializeField]private float balance;
        public int currentlySectedChip = 1;
        private float totalBet;
        private int previousWinAmount;
        private int section;
        private int lastWinNo;
        private int[] previousBet = new int[10];
        public int[] betHolder = new int[10];
        public int[] PreviousbetHolder = new int[10];
        private int[] previousWins = new int[10];
        //private int[] 
        private string roundcount;
        private string lastroundcount;
        private string lastWinRoundcount;
        private string[] PrizeName;
        private string isPreviousWinsRecivied;
        private string winningAmount;
        private string currentComment;
        private string userId;
        private string[] commentsArray = {"Bets are Empty!!" ,"For Amusement Only","Bet Accepted!! your bet amount is :"
        ,"Please click on Take","Bets Confirmed"};

        private bool isUserPlacedBets;
        private bool isBetConfirmed;
        private bool canPlaceBet;
        private bool isLastGameWinAmountReceived;
        private bool canPlacedBet;
        private bool isthisisAFirstRound;
        private bool isPreviousBetPlaced;
        private bool isdataLoaded;
        public bool isTimeUp;
        LastRoundWins[] lastRoundWins;
        bool isTimerStarted;
        private bool isSomethingWentWrong;
        private bool isCurrentBetPlace;
        int lastwinNo;
        int minmumTimeAllowed = 6;
        string tempRoundCount;
        public List<int> PreviousInfo;
        private bool isActive;
        const int SINGLE_BET_LIMIT = 5000;
        int timer_CountDown;
        public List<int> previousWinsdata = new List<int>();
        public List<TextMeshProUGUI> previousWinsText = new List<TextMeshProUGUI>();
        int winnerAmount;
        SceneHandler sceneHandler;
        List<int> Bets_Container = new List<int>();
        public GameObject[] coinimages;
        float winround;
        string winamounturl ="http://139.59.92.165:5000/user/Winamount";
        string emptyurl ="http://139.59.92.165:5000/user/DeletePreviousWinamount";
        string  previousurl = "http://139.59.92.165:5000/user/funtarget";
        public GameObject waiting;
        public Text[] betvaluetxt;
        [SerializeField] Button[] buttonimages;
        [SerializeField] GameObject namespot,wheelspot;
        [SerializeField] Text userid;
        public bool spinnow
        {
            get{
                if (winnigText.text == string.Empty)
                {
                    return true;
                }
                else if ( int.Parse(winnigText.text) > 0 )
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
        //bool spinnow;
        //public List<int> lastrecord;//int[] lastrecord;
        public int omegaCounter;
        #endregion

        void Awake()
        {
            Instance = this;
        }

        
        private void Start()
        {
            userid.text = "FUN"+PlayerPrefs.GetString("email");
            omegaCounter = 0;
            roundcount = string.Empty;  
            isthisisAFirstRound = true;
            isdataLoaded = true;
            //canPlaceBet = true;
            //spinnow = true;
            lastRoundWins = new LastRoundWins[10];
            //FindUIReferences();
            if(PlayerPrefs.GetFloat("points") <0)
            {
                balance =0;
            }
            else
            {
                balance = PlayerPrefs.GetFloat("points");
            }
            
            balanceText.text = balance.ToString("F2");
            totalBetText.text = totalBet.ToString();
            AddListners();
            //sceneHandler = GameObject.Find("SceneHandler").GetComponent<SceneHandler>();
            imageselect(0);
            StartCoroutine(spotblink());
            
            
            if(PlayerPrefs.GetString("funpaused") == "true")
            {
                //Debug.Log("winwinwinwinwinwinwinwinwinwinwinwinwin"+int.Parse(winnigText.text));
                //if(PlayerPrefs.GetFloat("totalbet") >0)
                //{
                Debug.Log("this is value sotred in playerprefs " +PlayerPrefs.GetInt("funtargetround")+" this is the value in stored variable"+ FunTarget_ServerResponse.instance.roundcounttillnow);
                // if (PlayerPrefs.GetInt("funtargetround") ==FunTarget_ServerResponse.instance.roundcounttillnow )
                // {
                RepeatBets();
                //}
                
                // winnerAmount = Funtargetlast.winningAmount;
                // winnigText.text = winnerAmount.ToString();
                // }
                // else
                // {
                    //throwout();
                //}
                //winnigText.text = PlayerPrefs.GetString("funtargetwinner");
                
                //winnerAmount = int.Parse(winnigText.text);
                
                
            }
            else
            {
                //Debug.Log("reached here");
                
                
            }
            
            // Debug.Log("funfunfunfunfunfunfunfunfun"+PlayerPrefs.GetInt("funclosed"));
            // if(PlayerPrefs.GetInt("funclosed") == 1)
            // {
            //     RepeatBets();
            // }
            if ( winnigText.text != string.Empty)
            {
                if(int.Parse(winnigText.text) > 0)
                {
                    //StartCoroutine(takeBtn.GetComponent<ButtonBlinker>().TriggerBlinking());
                    betOkBtn.interactable = false;
                    //betOkBtn.GetComponent<ButtonBlinker>().StopBlinking();
                }
            }
            // winnerAmount = int.Parse(winnigText.text);//int.Parse(PlayerPrefs.GetString("funtargetwinner"));
            // winround = balance+winnerAmount;
            // PlayerPrefs.SetInt("funclosed",0);
            
            
            //
            
            
            // AddSocketListners();
        }
        bool started;
        IEnumerator spotblink()
        {
            namespot.SetActive(false);
            wheelspot.SetActive(false);
            yield return new WaitForSeconds(0.5f);
            namespot.SetActive(true);
            wheelspot.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            StartCoroutine(spotblink());
        }
        public void funrestoration()
        {
            started = true;
            //StartCoroutine(funtargetwinresponse());
            if(PlayerPrefs.GetString("funstarted") =="true")
            {
                savebinary.LoadPlayerfuntarget();
                PlayerPrefs.SetString("funstarted","false");
                
            }
            
            if(withingame.funtargetwinning >0)
            {
                winnigText.text = Funtargetlast.winningAmount.ToString();
                
            }
            else{
                if(withingame.confirmed == true)
                {
                    if (FunTarget_ServerResponse.instance.roundcounttillnow == withingame.RoundCount)
                    {
                        Debug.LogError("error poped out here");
                        StartCoroutine(previousdata());
                        //restorewithin();
                    }
                    isBetConfirmed = true;
                    repeatBtn.interactable = false;
                    //repeatBtn.GetComponent<ButtonBlinker>().StopBlinking();
                    if (betOkBtn.gameObject.activeInHierarchy)
                    {
                        betOkBtn.interactable = false;
                        //betOkBtn.GetComponent<ButtonBlinker>().StopBlinking();
                    }
                    canPlaceBet = false;
                }
                else
                {
                    isBetConfirmed = false;
                    betOkBtn.interactable = true;
                    canPlaceBet = true;
                }
                winnerAmount = int.Parse(winnigText.text);//int.Parse(PlayerPrefs.GetString("funtargetwinner"));
                winround = balance+winnerAmount;
                
            }
        }
        
        bool isPause;
        void OnApplicationPause(bool hasFocus)
        {
            isPause = hasFocus;
           
            //if (Application.isEditor) return;
            // {
                
            if(isPause)
            {
                Debug.Log("Application pause:" +hasFocus);
                if (isBetConfirmed)
                {
                    PlayerPrefs.SetString("funpaused","true");
                    if(totalBet >0)
                    {
                        storedata();
                    }
                    
                    //Funtargetlast.winningAmount = int.Parse(winnigText.text);
                    File.Delete(SaveFilePath);
                    savebinary.savefunctionfuntarget(); 
                    StopCoroutine(Timer());
                    //PlayerPrefs.SetString("funpaused","true");
                    //PlayerPrefs.SetString("funtargetwinner",winnigText.text);
                    
                    PlayerPrefs.SetFloat("totalbet",totalBet);
                    //Debug.Log("wwwwwwwwwwwwwiiiiiiiiiiiiiiiinnnnnnnnnnn"+PlayerPrefs.GetString("funtargetwinner"));
                    PlayerPrefs.SetInt("funtargetround",FunTarget_ServerResponse.instance.roundcounttillnow);
                }
                PlayerPrefs.SetString("lastplayer","funtarget");
                
                //StopAllCoroutines();
                //ServerResponse.instance.socketoff();
                //FunTarget_ServerResponse.instance.socketoff();
                //7Up&DownServerResponse.Instance.socketoff();
                
                // AndarBahar_ServerResponse.Instance.socketoff();
                //SceneManager.LoadScene(1);
                //AndarBahar_ServerRequest.instance.socket.Emit(Utility.Events.onleaveRoom); //leave room;
            }
            if (!isPause)
            {
                //StopCoroutine(Timer());
                //FunTarget_ServerResponse.instance.socketoff();
                Debug.Log("Application pause:" +hasFocus);
                PlayerPrefs.SetInt("reload",1);
                FunTarget_ServerRequest.instance.socket.Emit(Utility.Events.onleaveRoom);
                //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                //Addressables.LoadSceneAsync(SceneManager.GetActiveScene().name);
                // restoredata();
                // PlacePreviousBets();
                //Debug.Log("bets have been replaced");
                //UpdateUi();
                //string _playerName = "RL" + PlayerPrefs.GetString("email");
                // details data = new details
                // {
                //     playerid = _playerName
                // };
                // FunTarget_ServerRequest.instance.socket.Emit(Utility.Events.OnBetsPlaced,new JSONObject(JsonConvert.SerializeObject(data)));
                //Addressables.LoadSceneAsync(SceneManager.GetActiveScene().name, UnityEngine.SceneManagement.LoadSceneMode.Single, true);
                // HomeScript.Instance.AndarBaharBtn();
                // AndarBahar_ServerResponse.Instance.socketOn();
                //SceneManager.LoadScene(SceneManager.GetActiveScene().name);                
            }
            // }
        }
        string setplayeroffline = "http://139.59.92.165:5000/user/SetplayerOffline";
        // public IEnumerator emptytab()
        // {
        //     WWWForm form =  
        // }
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
                    //Debug.Log(www.error);
                }
                else{
                    //Debug.Log("Offline//Offline//Offline//Offline//Offline//Offline//Offline//Offline//Offline//Offline//");
                }
            }
        }
        private void OnApplicationQuit() {
            PlayerPrefs.SetString("funtargetwinner","0");
            PlayerPrefs.SetString("funpaused","false");
            PlayerPrefs.SetFloat("totalbet",0);
            PlayerPrefs.SetInt("funclosed",0);
            if(totalBet > 0)
            {
                storedata();
            }
            File.Delete(SaveFilePath);
            savebinary.savefunctionfuntarget();
            StartCoroutine(settingoffline());
        }
        public void setinitialarray(List<int> values)
        {
            Debug.Log("sxdcfvgkjhbgvfxcdfvghhnbvgfdcvbgjhgbvfxcfvgbhnj" + values.Count);
            // bool certain;
            if(values.Count >10)
            {
                while(values.Count >10)
                {
                    values.RemoveAt(0);
                }
                previousWinsdata.Clear();
                // certain = true;
            }
            else
            {
                values.Reverse();
                while ( values.Count < 10 )
                {
                    values.Add(0);
                }
                values.Reverse();
            }

            

            Debug.Log("Length of values is : " + values.Count + " And Previous win text length is : " + previousWinsText.Count );

            for (int i = 0; i < previousWinsText.Count; i++)
            {
                // if(certain)
                // {
                   
                // }
                Debug.Log("brrrrruhhh added into list " + i );
                previousWinsdata.Add(values[i]);
                Debug.Log("brrrrruhhh added into text diaplay " + i );
                previousWinsText[i].text = values[i].ToString();//100.ToString();
                //PreviousWin_list.Add(values[i]);
            }
            // while(previousWinsdata.Count >9)
            // {
            //     previousWinsdata.RemoveAt(0);
            //     //PreviousWin_list.RemoveAt(0);
                
            // }
            // for(int i = 0; i < previousWinsdata.Count; i++)
            // {
            //     //Debug.Log("brrrrruhhh");
            //     //previousWinsText[i].text = 100.ToString();//previousWinsdata[i].ToString();
            //     //PreviousWin_Text[i].GetComponent<Text>().text =  PreviousWin_list[i].ToString();
            // }
            
            int temp = previousWinsdata[previousWinsdata.Count-1];
            //Debug.LogError("the values length was "+values.Count +" " +temp);
            //Debug.Log("value of temp" + temp);
            SpinWheelWithoutPlugin.instane.setfirst(temp);
            StartCoroutine(funtargetwinresponse());            
        }
        private string SaveFilePath
        {
        get { return Application.persistentDataPath + "/funtarget.nms"; }
        }
        private void AddListners()
        {
            exitBtn.onClick.AddListener(() =>
            {
                close();
            });
            //exitBtn.onClick.AddListener(() => ResetAllBets());

            betOkBtn.onClick.AddListener(() =>
            {
                //betOkBtn.GetComponent<ButtonBlinker>().StopBlinking();
                //messageText.text = commentsArray[3];
                if(totalBet == 0)
                {
                    messageText.text = "Please Bet to Start Game . Minimum Bet =1 ";
                    return;
                }
                OnBetCalculation();
            });
            clearBtn.onClick.AddListener(() =>
            {
                Button[] betBtns = betButtons.GetComponentsInChildren<Button>();
                foreach (Button button in betBtns)
                {
                    if (button.name != "BetValue")
                    {
                        //button.GetComponent<ButtonBlinker>().StopBlinking();
                        //Debug.Log("Resetting " + button.gameObject.name);
                    }
                }
                clearb();
                //ResetAllBets();
            });
            doubleBtn.onClick.AddListener(OnClickOnDoubleBetBtn());

            repeatBtn.onClick.AddListener(() =>
            {
                Button[] betBtns = betButtons.GetComponentsInChildren<Button>();
                foreach (Button button in betBtns)
                {
                    if (button.name != "BetValue")
                    {
                        // if (button.GetComponent<ButtonBlinker>().IsBlinking)
                        // {
                        //     //button.GetComponent<ButtonBlinker>().StopBlinking();
                        //     //Debug.Log("Resetting " + button.gameObject.name);
                        // // }
                    }
                }
                //arrow.GetComponent<ButtonBlinker>().StopBlinking();
                RepeatBets();
            });

            takeBtn.onClick.AddListener(() =>
            {
                //Debug.Log("amountto be received");
                //takeBtn.GetComponent<ButtonBlinker>().StopBlinking();
                // Button[] betBtns = betButtons.GetComponentsInChildren<Button>();
                // foreach (Button button in betBtns)
                // {
                //     if (button.name != "BetValue")
                //     {
                //         button.GetComponent<ButtonBlinker>().StopBlinking();
                //         //Debug.Log("Resetting " + button.gameObject.name);
                //     }
                // }
                SendTakeAmountRequest();
                
            });

            cancelSpecificBtn.onClick.AddListener(() =>
            {
                betHolder[latestBetIndex] = 0;
                betvaluetxt[latestBetIndex].text = betHolder[latestBetIndex].ToString();

                Button[] betBtns = betButtons.GetComponentsInChildren<Button>();
                foreach (Button button in betBtns)
                {
                    if (button.gameObject.name == latestBetIndex.ToString())
                    {
                        //button.image.sprite = button.GetComponent<ButtonBlinker>().sprite1;
                    }
                }
            });

            // AddSocketListners();

            //------------betting no---------------
            oneBetBtn.onClick.AddListener(() => { AddBet(1, oneBetBtn.gameObject); });
            twoBetBtn.onClick.AddListener(() => { AddBet(2, twoBetBtn.gameObject); });
            threeBetBtn.onClick.AddListener(() => { AddBet(3, threeBetBtn.gameObject); });
            fourBetBtn.onClick.AddListener(() => { AddBet(4, fourBetBtn.gameObject); });
            fiveBetBtn.onClick.AddListener(() => { AddBet(5, fiveBetBtn.gameObject); });
            sixBetBtn.onClick.AddListener(() => { AddBet(6, sixBetBtn.gameObject); });
            sevenBetBtn.onClick.AddListener(() => { AddBet(7, sevenBetBtn.gameObject); });
            eightBetBtn.onClick.AddListener(() => { AddBet(8, eightBetBtn.gameObject); });
            nineBetBtn.onClick.AddListener(() => { AddBet(9, nineBetBtn.gameObject); });
            zeroBetBtn.onClick.AddListener(() => { AddBet(0, zeroBetBtn.gameObject); });

            //------------current betting cheap---------------
            //chipNo1Btn.onValueChanged.AddListener((i) => { if (!i) return; currentlySectedChip = 1;Debug.Log(currentlySectedChip+"currentchip"); DisableToggleBgImage(chipNo1Btn.gameObject);});//imageselect(0); });
            //chipNo5Btn.onValueChanged.AddListener((i) => { if (!i) return; currentlySectedChip = 5;Debug.Log(currentlySectedChip+"currentchip"); DisableToggleBgImage(chipNo5Btn.gameObject);});//imageselect(0); });
            //chipNo10Btn.onValueChanged.AddListener((i) => { if (!i) return; currentlySectedChip = 10;Debug.Log(currentlySectedChip+"currentchip");  DisableToggleBgImage(chipNo10Btn.gameObject);});//imageselect(0); });
            //chipNo50Btn.onValueChanged.AddListener((i) => { if (!i) return; currentlySectedChip = 50;Debug.Log(currentlySectedChip+"currentchip"); DisableToggleBgImage(chipNo50Btn.gameObject);});//imageselect(0); });
            //chipNo100Btn.onValueChanged.AddListener((i) => { if (!i) return; currentlySectedChip = 100;Debug.Log(currentlySectedChip+"currentchip"); DisableToggleBgImage(chipNo100Btn.gameObject);});//imageselect(0); });
            //chipNo500Btn.onValueChanged.AddListener((i) => { if (!i) return; currentlySectedChip = 500;Debug.Log(currentlySectedChip+"currentchip"); DisableToggleBgImage(chipNo500Btn.gameObject);});//imageselect(0); });
            //chipNo1000Btn.onValueChanged.AddListener((i) => { if (!i) return; currentlySectedChip = 1000;Debug.Log(currentlySectedChip+"currentchip"); DisableToggleBgImage(chipNo1000Btn.gameObject);});//imageselect(0); });
            //chipNo5000Btn.onValueChanged.AddListener((i) => { if (!i) return; currentlySectedChip = 5000;Debug.Log(currentlySectedChip+"currentchip"); DisableToggleBgImage(chipNo1000Btn.gameObject);});//imageselect(0); });
        }
        void close()
        {
            //storedata();    
            //Application.persistentDataPath.Delete("funtarget.nms");
            
            File.Delete(SaveFilePath);
            if (isBetConfirmed || int.Parse(winnigText.text) >0)
            {
                currentround();
                storedata();
            }
            if(!isBetConfirmed)
            {
                Debug.Log("the value for betconfirm"+isBetConfirmed);
                withingame.confirmed = false;
                if(totalBet == 0)
                {
                    SendBets();
                }
                
                
            }
            withingame.funtargetwinning = int.Parse(winnigText.text);
            savebinary.savefunctionfuntarget();
            PlayerPrefs.SetString("funpaused","false");
            PlayerPrefs.SetInt("funtargetround",FunTarget_ServerResponse.instance.roundcounttillnow);
            //PlayerPrefs.SetInt("funclosed",1);
            //savebinary.LoadPlayer();
            //Invoke(nameof(something), 2f);
            PlayerPrefs.SetFloat("points",balance);
            PlayerPrefs.SetInt("thrownout",0);
            PlayerPrefs.SetInt("funwin", int.Parse(winnigText.text));
            //StartCoroutine(settingoffline());
            FunTarget_ServerRequest.instance.socket.Emit(Utility.Events.onleaveRoom);
            //SendTakeAmountRequest();
            //sceneHandler.unloadAddressableScene();
            // FunTarget_ServerRequest.instance.SendEvent(Utility.Events.onleaveRoom);
            //FunTarget_ServerResponse.instance.socketoff();
            //SceneManager.LoadScene("MainScene");
            //SceneManager.LoadScene(0);
            //savebinary.LoadPlayer();
        }

        void currentround()
        {
            withingame.confirmed = isBetConfirmed;
            withingame.funtargetwinning = int.Parse(winnigText.text);
            withingame.RoundCount = FunTarget_ServerResponse.instance.roundcounttillnow;
            for (int i = 0; i < withingame.funtarget_previous.Length; i++)
            {
                withingame.funtarget_previous[i] = 0;
            }
            
            for (int i = 0; i < betvaluetxt.Length; i++)
            {
                if(betvaluetxt[i].text == "")
                {
                    withingame.funtarget_previous[i] = 0;
                }
                else
                {
                    withingame.funtarget_previous[i] = int.Parse(betvaluetxt[i].text);
                }
               
            }
        }
        public void showui()
        {
            // 
            for (int i = 0; i < withingame.funtarget_previous.Length; i++)
            {
                //betHolder[i] = withingame.funtarget_previous[i];
                betHolder[i] = Funtargetlast.funtargetvalue[i];
                if(betHolder[i]>0)
                {
                    //buttonimages[i].image.sprite = buttonimages[i].GetComponent<ButtonBlinker>().sprite2;
                    betvaluetxt[i].text = betHolder[i].ToString();
                }
            }
            totalBet = betHolder.Sum();
            totalBetText.text =totalBet.ToString();
            
            // oneBetBtn.transform.GetChild(0).GetComponent<Text>().text = betHolder[1] == 0 ? string.Empty : betHolder[1].ToString();
            // twoBetBtn.transform.GetChild(0).GetComponent<Text>().text = betHolder[2] == 0 ? string.Empty : betHolder[2].ToString();
            // threeBetBtn.transform.GetChild(0).GetComponent<Text>().text = betHolder[3] == 0 ? string.Empty : betHolder[3].ToString();
            // fourBetBtn.transform.GetChild(0).GetComponent<Text>().text = betHolder[4] == 0 ? string.Empty : betHolder[4].ToString();
            // fiveBetBtn.transform.GetChild(0).GetComponent<Text>().text = betHolder[5] == 0 ? string.Empty : betHolder[5].ToString();
            // sixBetBtn.transform.GetChild(0).GetComponent<Text>().text = betHolder[6] == 0 ? string.Empty : betHolder[6].ToString();
            // sevenBetBtn.transform.GetChild(0).GetComponent<Text>().text = betHolder[7] == 0 ? string.Empty : betHolder[7].ToString();
            // eightBetBtn.transform.GetChild(0).GetComponent<Text>().text = betHolder[8] == 0 ? string.Empty : betHolder[8].ToString();
            // nineBetBtn.transform.GetChild(0).GetComponent<Text>().text = betHolder[9] == 0 ? string.Empty : betHolder[9].ToString();
            // zeroBetBtn.transform.GetChild(0).GetComponent<Text>().text = betHolder[0] == 0 ? string.Empty : betHolder[0].ToString();
            // isPreviousBetPlaced = true;
            // totalBet = betHolder.Sum();
        }

        public void restorewithin()
        {
            bool isEnoughBalance = previousBet.Sum() < balance;
            if (!isEnoughBalance || int.Parse(winnigText.text) >0)
            {
                m.print("not enough balance");

                messageText.text = "Not enough balance";
                //AndroidToastMsg.ShowAndroidToastMessage("not enough balance");
                return;
            }
            //winnigText.text = withingame.funtargetwinning.ToString();
            for (int i = 0; i < withingame.funtarget_previous.Length; i++)
            {
                betHolder[i] = withingame.funtarget_previous[i];
                if(betHolder[i]>0)
                {
                    //buttonimages[i].image.sprite = buttonimages[i].GetComponent<ButtonBlinker>().sprite2;
                    betvaluetxt[i].text = betHolder[i].ToString();
                }
                
            }
            
            // oneBetBtn.transform.GetChild(0).GetComponent<Text>().text = betHolder[1] == 0 ? string.Empty : betHolder[1].ToString();
            // twoBetBtn.transform.GetChild(0).GetComponent<Text>().text = betHolder[2] == 0 ? string.Empty : betHolder[2].ToString();
            // threeBetBtn.transform.GetChild(0).GetComponent<Text>().text = betHolder[3] == 0 ? string.Empty : betHolder[3].ToString();
            // fourBetBtn.transform.GetChild(0).GetComponent<Text>().text = betHolder[4] == 0 ? string.Empty : betHolder[4].ToString();
            // fiveBetBtn.transform.GetChild(0).GetComponent<Text>().text = betHolder[5] == 0 ? string.Empty : betHolder[5].ToString();
            // sixBetBtn.transform.GetChild(0).GetComponent<Text>().text = betHolder[6] == 0 ? string.Empty : betHolder[6].ToString();
            // sevenBetBtn.transform.GetChild(0).GetComponent<Text>().text = betHolder[7] == 0 ? string.Empty : betHolder[7].ToString();
            // eightBetBtn.transform.GetChild(0).GetComponent<Text>().text = betHolder[8] == 0 ? string.Empty : betHolder[8].ToString();
            // nineBetBtn.transform.GetChild(0).GetComponent<Text>().text = betHolder[9] == 0 ? string.Empty : betHolder[9].ToString();
            // zeroBetBtn.transform.GetChild(0).GetComponent<Text>().text = betHolder[0] == 0 ? string.Empty : betHolder[0].ToString();
            isPreviousBetPlaced = true;
            totalBet = betHolder.Sum();
            
            //Debug.Log("loop has been breached");
            if (PlayerPrefs.GetString("OnBetConfirmed") == "false")
            {
                balance -= totalBet;
            }
            if(withingame.confirmed == true)
            {
                canPlaceBet = false;
                isBetConfirmed = true;
            }
            if (balance >0)
            {
                balanceText.text = balance.ToString("F2");
            }
            
            totalBetText.text = totalBet.ToString();
        }

        void AddSocketListners()
        {
            // FunTarget_ServerRequest.instance.ListenEvent(Utility.Events.OnWinNo, (json) =>
            // {
            //     StartCoroutine(OnRoundEnd(json));
            // });
            // FunTarget_ServerRequest.instance.ListenEvent(Utility.Events.OnTimerStart, (json) =>
            // {
            //     OnTimerStart(json);
            // }); 
            // FunTarget_ServerRequest.instance.ListenEvent(Utility.Events.OnDissconnect, (json) =>
            // {
            //     print("dissconnected");
            // }); 
            // FunTarget_ServerRequest.instance.ListenEvent(Utility.Events.OnWinAmount, (json) =>
            // {
            //     OnWinAmount(json);
            // });
            // FunTarget_ServerRequest.instance.ListenEvent(Utility.Events.OnTimeUp, (json) =>
            // {
            //     isTimeUp = true;
            // });

        }
        public void stopcenteranim()
        {
            center_anime.SetBool("rotate",false);
        }

        void OnWinAmount(string res)
        {
            OnWinAmount o = JsonConvert.DeserializeObject<OnWinAmount>(res);
            //winnigText.text = o.data.win_points.ToString();
            winningAmount = o.data.win_points.ToString();
        }
        public void OnTimerStart(string res)
        {
            //Debug.Log("timer started");
            //PlayerPrefs.SetString("OnBetConfirmed","false");


            ///commented here
            if(betOkBtn.gameObject.activeInHierarchy)
            {
                betOkBtn.interactable = false;
                betOkBtn.GetComponent<ButtonBlinker>().StopBlinking();
            }
           
            closebtn.interactable = true;
            repeatBtn.interactable = true;
            //StartCoroutine(repeatBtn.GetComponent<ButtonBlinker>().TriggerBlinking());
            isdataLoaded = true;
            isBetConfirmed = false;
            canPlaceBet = true;
            isTimeUp = false;
            TimerClass _timedata = JsonConvert.DeserializeObject<TimerClass>(res);
            // timer_CountDown = 40;//_timedata.result;
            timer_CountDown = _timedata.result;
            omegaCounter = 0;
            //this will get the current timer from the sever unless the timer is 0
            //it not it will wait for it
            StartCoroutine(GetCurrentTimer());
        }
        IEnumerator GetCurrentTimer()
        {
            yield return new WaitUntil(() => currentTime <= 0);
            StartCoroutine(Timer(timer_CountDown +1));
            
            // FunTarget_ServerRequest.instance.SendEvent(Constant.OnTimer, (json) =>
            // {
            //     print("current timer " + json);
            //     Timer time = JsonConvert.DeserializeObject<Timer>(json);
            //     StopCoroutine(Timer());
            //     if (time.result == 0) StartCoroutine(Timer());
            //     else StartCoroutine(Timer(time.result));
            // });
        }
        private void DisableToggleBgImage(GameObject target)
        {
            //Debug.Log(">>>>>>>>>> Current Selected Chip >>>> " + currentlySectedChip);
            // chipNo1Btn.transform.GetChild(0).GetComponent<Image>().enabled = true;
            // chipNo5Btn.transform.GetChild(0).GetComponent<Image>().enabled = true;
            // chipNo10Btn.transform.GetChild(0).GetComponent<Image>().enabled = true;
            // chipNo50Btn.transform.GetChild(0).GetComponent<Image>().enabled = true;
            // chipNo100Btn.transform.GetChild(0).GetComponent<Image>().enabled = true;
            // chipNo500Btn.transform.GetChild(0).GetComponent<Image>().enabled = true;
            // chipNo1000Btn.transform.GetChild(0).GetComponent<Image>().enabled = true;
            // chipNo5000Btn.transform.GetChild(0).GetComponent<Image>().enabled = true;
            // target.transform.GetChild(0).GetComponent<Image>().enabled = false;
        }
        private UnityAction OnClickOnDoubleBetBtn()
        {
            return () =>
            {
                if (!isdataLoaded)
                {
                    m.print("please wait data to load");
                    messageText.text = "Please wait for data to load";
                    //AndroidToastMsg.ShowAndroidToastMessage("please wait data to load");
                    return;
                }
                if (betHolder.Sum() == 0)
                {

                    m.print("no bet placed yet");
                    messageText.text = "No bets placed yet";
                    //AndroidToastMsg.ShowAndroidToastMessage("no bet placed yet");
                    return;
                }
                bool isEnoughBalance = balance > betHolder.Sum() * 2;

                if (!isEnoughBalance)
                {
                    m.print("not enough balance");
                    messageText.text = "Not enough balance";
                    //AndroidToastMsg.ShowAndroidToastMessage("not enough balance");
                    return;
                }

                bool isRichedTheLimit = betHolder.Sum() * 2 > SINGLE_BET_LIMIT;

                if (isRichedTheLimit)
                {
                    m.print("reached the limit");
                    messageText.text = "Reached bet limit";
                    //AndroidToastMsg.ShowAndroidToastMessage("reached the limit");
                    return;
                }

                for (int i = 0; i < betHolder.Length; i++)
                {
                    betHolder[i] *= 2;
                }
                // zeroBetBtn.transform.GetChild(0).GetComponent<Text>().text = betHolder[0].ToString() == "0" ? string.Empty : betHolder[0].ToString();
                // oneBetBtn.transform.GetChild(0).GetComponent<Text>().text = betHolder[1].ToString() == "0" ? string.Empty : betHolder[1].ToString();
                // twoBetBtn.transform.GetChild(0).GetComponent<Text>().text = betHolder[2].ToString() == "0" ? string.Empty : betHolder[2].ToString();
                // threeBetBtn.transform.GetChild(0).GetComponent<Text>().text = betHolder[3].ToString() == "0" ? string.Empty : betHolder[3].ToString();
                // fourBetBtn.transform.GetChild(0).GetComponent<Text>().text = betHolder[4].ToString() == "0" ? string.Empty : betHolder[4].ToString();
                // fiveBetBtn.transform.GetChild(0).GetComponent<Text>().text = betHolder[5].ToString() == "0" ? string.Empty : betHolder[5].ToString();
                // sixBetBtn.transform.GetChild(0).GetComponent<Text>().text = betHolder[6].ToString() == "0" ? string.Empty : betHolder[6].ToString();
                // sevenBetBtn.transform.GetChild(0).GetComponent<Text>().text = betHolder[7].ToString() == "0" ? string.Empty : betHolder[7].ToString();
                // eightBetBtn.transform.GetChild(0).GetComponent<Text>().text = betHolder[8].ToString() == "0" ? string.Empty : betHolder[8].ToString();
                // nineBetBtn.transform.GetChild(0).GetComponent<Text>().text = betHolder[9].ToString() == "0" ? string.Empty : betHolder[9].ToString();
                // totalBet = betHolder.Sum();
                // SoundManager.instance.PlayClip("addbet");
                //UpdateUi();//clear
            };
        }
        private void Update() 
        {
            if(Application.platform == RuntimePlatform.Android)
            {
                if(Input.GetKeyDown(KeyCode.Escape))
                {
                    close();
                }
            }
            if(Input.GetKeyDown(KeyCode.T))
            {
                Debug.Log("showthis");
                GetComponent<ObjectScreenshot>().TakeScreenshot();
            }
        }
        void storedata()
        {
            
            Funtargetlast.funtargetvalue[0] = withingame.funtarget_previous[0];
            Funtargetlast.funtargetvalue[1] = withingame.funtarget_previous[1];
            Funtargetlast.funtargetvalue[2] = withingame.funtarget_previous[2];
            Funtargetlast.funtargetvalue[3] = withingame.funtarget_previous[3];
            Funtargetlast.funtargetvalue[4] = withingame.funtarget_previous[4];
            Funtargetlast.funtargetvalue[5] = withingame.funtarget_previous[5];
            Funtargetlast.funtargetvalue[6] = withingame.funtarget_previous[6];
            Funtargetlast.funtargetvalue[7] = withingame.funtarget_previous[7];
            Funtargetlast.funtargetvalue[8] = withingame.funtarget_previous[8];
            Funtargetlast.funtargetvalue[9] = withingame.funtarget_previous[9];
            File.Delete(SaveFilePath);
            savebinary.savefunctionfuntarget(); 
            //lastrecord.funtargetlas = true;
            //}
            //Debug.Log("binarybinarybinarybinarybonary");
            // for (int i = 0; i < Funtargetlast.funtargetvalue.Length; i++)
            // {
            //     Debug.Log("the value of i: "+i+" value: "+Funtargetlast.funtargetvalue[i]);
            // }
            
        }
        void justshowbets()
        {
            for (int i = 0; i < Funtargetlast.funtargetvalue.Length; i++)
            {
                betHolder[i] = Funtargetlast.funtargetvalue[i];
                if(betHolder[i]>0)
                {
                    betvaluetxt[i].text = betHolder[i].ToString();
                    //buttonimages[i].image.sprite = buttonimages[i].GetComponent<ButtonBlinker>().sprite2;
                    //buttonimages[i].image.color = Color.green;
                }
                
                //Debug.Log("/////////"+betHolder[i]);
                //Debug.Log(i+i+i+i+i+i+i+i+i+i+i+":"+Funtargetlast.funtargetvalue[i] + "//"+betHolder[i] );
            }
        }
        public IEnumerator previousdata()
        {
            WWWForm form = new WWWForm();
            string playerId = "GK"+PlayerPrefs.GetString("email");
            form.AddField("playerId",playerId);
            using (UnityWebRequest www = UnityWebRequest.Post(previousurl, form))
            {
                yield return www.SendWebRequest();
                
                if (www.result != UnityWebRequest.Result.Success)
                {
                    Debug.LogError(www.error);
                }
                else
                {
                    PreveiousBet response = JsonConvert.DeserializeObject<PreveiousBet>(www.downloadHandler.text);
                    Debug.Log(JsonConvert.DeserializeObject<PreveiousBet>(www.downloadHandler.text));
                    if(response.status == 200)
                    {
                        Debug.Log("the data should be printed by now");
                        betHolder[0] = response.data.zero;
                        betHolder[2] = response.data.two;
                        betHolder[3] = response.data.three;
                        betHolder[1] = response.data.one;
                        betHolder[4] = response.data.four;
                        betHolder[5] = response.data.five;
                        betHolder[6] = response.data.Six;
                        betHolder[7] = response.data.Seven;
                        betHolder[8] = response.data.Eight;
                        betHolder[9] = response.data.Nine;
                        restoredata();
                        
                    }

                }
                
            }
        }
        void restoredata()
        {
            Debug.Log("the values retreived was zero:"+betHolder[0]+" "+"one:" +betHolder[1]+"two:"+betHolder[2]+" "+"three:" +betHolder[3]+"four:"+betHolder[4]+" "+"five:" +betHolder[5]+"six:"+betHolder[6]+" "+"seven:" +betHolder[7]+"eight:"+betHolder[8]+" "+"nine:" +betHolder[9]);
            
            if (repeated)
            {
                if(betHolder.Sum() > balance)
                {
                    messageText.text = "Insufficent balance";
                    canPlaceBet = true;
                    return;
                }
            }
            for (int i = 0; i < betHolder.Length; i++)
            {
                if(betHolder[i]>0)
                {
                    betvaluetxt[i].text = betHolder[i].ToString();  
                    //buttonimages[i].image.sprite = buttonimages[i].GetComponent<ButtonBlinker>().sprite2;
                    //buttonimages[i].image.color = Color.green;
                }
            }
            Debug.Log("Bet holder sum is : " + (int) betHolder.Sum()); 
            totalBet = betHolder.Sum();//Funtargetlast.funtargetvalue.Sum();//betHolder.Sum();
            totalBetText.text = totalBet.ToString();
            if(repeated)
            {
                balance = balance - totalBet;
                balanceText.text = balance.ToString("F2");
                repeated = false;

                repeatBtn.gameObject.SetActive(false);
                betOkBtn.gameObject.SetActive(true);
                betOkBtn.interactable = true;
                canPlaceBet = true;
                // if (totalBet >0)
                // {
                //     OnBetCalculation();
                // }
            }
            else
            {
                if (totalBet >0)
                {
                    Debug.Log("Is total bet is more than  0 : " + totalBet );
                    isBetConfirmed =true;
                    //OnBetCalculation();
                    betOkBtn.interactable = false;
                    repeatBtn.interactable = false;
                    canPlaceBet = false;
                }
                else
                {
                    repeatBtn.gameObject.SetActive(false);
                    betOkBtn.gameObject.SetActive(true);
                    betOkBtn.interactable = true;
                    canPlaceBet = true;
                }
            }

        }
        void localsave()
        {
            for (int i = 0; i < Funtargetlast.funtargetvalue.Length; i++)
            {
                //kuchnahi
            }
        }
        bool repeated;
        void RepeatBets()
        {
            if(int.Parse(winnigText.text) > 0)
            {
                messageText.text = "Please collect winning amount first";
                //AndroidToastMsg.ShowAndroidToastMessage("Please collect winning amount first");
                return;
            }
            repeated = true;
            //Debug.Log("////////////////////// "+lastrecord.funtargetlas);
            //PlayerPrefs.SetInt("funclosed",1);
            // if(lastrecord.funtargetlas == true)
            // {
            //     restoredata();
            //     //Debug.Log("xdfcvgbhnjbhvgfdcvfbnjmh");
            //     //PlacePreviousBets();
            // }
            ResetAllBets();
            StartCoroutine(previousdata());
            // if ( totalBet > 0f)
            // {
            //     OnBetCalculation();
            //     // SendBets();
            // }
            //restoredata();
            
            //PlacePreviousBets();
            //restorewithin();
            //restoredata();
            
            betOkBtn.interactable = false;
            repeatBtn.interactable = false;
            canPlaceBet = false;
            //UpdateUi();//clear

            //repeatBtn.GetComponent<ButtonBlinker>().StopBlinking();

            // Button[] betBtns = betButtons.GetComponentsInChildren<Button>();
            // foreach (Button button in betBtns)
            // {
            //     if(button.GetComponentInChildren<Text>().text != "0" && button.GetComponentInChildren<Text>().text != null)
            //     {
            //         button.image.color = Color.green;
            //     }
            // }
        }

        public void ResetAllBets()
        {
            PlayerPrefs.SetInt("funclosed",0);
            for(int i = 0; i < betHolder.Length; i++ )
            {
                PreviousbetHolder[i] = betHolder[i]; 
            }
            for (int i = 0; i < betHolder.Length; i++)
            {
                betHolder[i] = 0;
                betvaluetxt[i].text = "";
            }
            // oneBetBtn.transform.GetChild(0).GetComponent<Text>().text = string.Empty;
            // twoBetBtn.transform.GetChild(0).GetComponent<Text>().text = string.Empty;
            // threeBetBtn.transform.GetChild(0).GetComponent<Text>().text = string.Empty;
            // fourBetBtn.transform.GetChild(0).GetComponent<Text>().text = string.Empty;
            // fiveBetBtn.transform.GetChild(0).GetComponent<Text>().text = string.Empty;
            // sixBetBtn.transform.GetChild(0).GetComponent<Text>().text = string.Empty;
            // sevenBetBtn.transform.GetChild(0).GetComponent<Text>().text = string.Empty;
            // eightBetBtn.transform.GetChild(0).GetComponent<Text>().text = string.Empty;
            // nineBetBtn.transform.GetChild(0).GetComponent<Text>().text = string.Empty;
            // zeroBetBtn.transform.GetChild(0).GetComponent<Text>().text = string.Empty;
            //balance += totalBet;
            totalBet = 0;
            isUserPlacedBets = false;
        //    if (betOkBtn.gameObject.activeInHierarchy)
        //    {
        //      betOkBtn.interactable = false;
        //      betOkBtn.GetComponent<ButtonBlinker>().StopBlinking();
        //    }
            canPlaceBet = true;
            isTimeUp = false;
            balanceText.text = balance.ToString("F2");
            UpdateUi();

            Button[] betBtns = betButtons.GetComponentsInChildren<Button>();
            // foreach (Button button in betBtns)
            // {
            //     //if (button.name != "BetValue")
            //     //{
            //     //    button.image.color = Color.white;
            //     //}

            //     button.image.sprite = button.GetComponent<ButtonBlinker>().sprite1;
            // }
        }
        public void clearb()
        {
            balance += totalBet;
            // oneBetBtn.transform.GetChild(0).GetComponent<Text>().text = string.Empty;
            // twoBetBtn.transform.GetChild(0).GetComponent<Text>().text = string.Empty;
            // threeBetBtn.transform.GetChild(0).GetComponent<Text>().text = string.Empty;
            // fourBetBtn.transform.GetChild(0).GetComponent<Text>().text = string.Empty;
            // fiveBetBtn.transform.GetChild(0).GetComponent<Text>().text = string.Empty;
            // sixBetBtn.transform.GetChild(0).GetComponent<Text>().text = string.Empty;
            // sevenBetBtn.transform.GetChild(0).GetComponent<Text>().text = string.Empty;
            // eightBetBtn.transform.GetChild(0).GetComponent<Text>().text = string.Empty;
            // nineBetBtn.transform.GetChild(0).GetComponent<Text>().text = string.Empty;
            // zeroBetBtn.transform.GetChild(0).GetComponent<Text>().text = string.Empty;
            for (int i = 0; i < betHolder.Length; i++)
            {
                betHolder[i] = 0;
            }
            for (int i = 0; i < betvaluetxt.Length; i++)
            {
                betvaluetxt[i].text = "";
            }
            //balance += totalBet;
            totalBet = 0;
            isUserPlacedBets = false;
            betOkBtn.interactable = false;
            //betOkBtn.GetComponent<ButtonBlinker>().StopBlinking();
            repeatBtn.interactable = true;
            //StartCoroutine(repeatBtn.GetComponent<ButtonBlinker>().TriggerBlinking());
            canPlaceBet = true;
            balanceText.text = balance.ToString("F2");
            UpdateUi();
        }

        int latestBetIndex;
        private void AddBet(int betIndex, GameObject btnREf)
        {
            //Debug.Log("isdataLoaded " + isdataLoaded);
            if (!isdataLoaded)
            {
                m.print("please wait data to load");
                messageText.text = "Please wait for data to load";
                //AndroidToastMsg.ShowAndroidToastMessage("please wait");
                return;
            }
            //Debug.Log("canPlaceBet " + canPlaceBet);
            if (!canPlaceBet || isTimeUp) return;
            if (currentlySectedChip == 0)
            {
                m.print("please select a chip first");
                messageText.text = "Please select a chip first";
                //AndroidToastMsg.ShowAndroidToastMessage("please select a chip first");
                return;
            }
            
            if (balance < currentlySectedChip || int.Parse(winnigText.text) != 0 )
            {
                if(int.Parse(winnigText.text) >0)
                {
                    messageText.text = "Please collect winning amount";
                    //AndroidToastMsg.ShowAndroidToastMessage("Please Collect Winning Amount");
                }
                else
                {
                    m.print("not enough balanc");
                    messageText.text = "Not enough balance";
                    //AndroidToastMsg.ShowAndroidToastMessage("not enough balance");
                    //Debug.Log("return here ");
                }
                return;
                
            }
            if (betHolder[betIndex] + currentlySectedChip > SINGLE_BET_LIMIT)
            {
                m.print("reached the limit");
                messageText.text = "Only 5000 bet amount allowed per number";
                //AndroidToastMsg.ShowAndroidToastMessage("Only 5000 betallowed per number");
                return;
            }
            if (currentlySectedChip == -1)//this is for delete chip btn
            {
                if (betHolder[betIndex] == 0) return;
                betHolder[betIndex] = 0;
            }
            else
            {
                if(betHolder[betIndex] <=5000)
                {
                    
                    betHolder[betIndex] += currentlySectedChip;
                    latestBetIndex = betIndex;
                    messageText.text = "You can either make bet or press Bet Ok button";
                }
                else
                {
                    //Debug.Log("Bets are not allllllllllllllloooooooooowwweeeeeeeeeeeeedddddddddd");
                }
                
            }

            Button[] betBtns = betButtons.GetComponentsInChildren<Button>();
            foreach (Button button in betBtns)
            {
                if (button.name != "BetValue")
                {
                    // if (button.GetComponent<ButtonBlinker>().IsBlinking)
                    // {
                    //     button.GetComponent<ButtonBlinker>().StopBlinking();
                    //     //Debug.Log("Resetting " + button.gameObject.name);
                    // }
                }
            }
            //btnREf.GetComponent<Button>().image.sprite = btnREf.GetComponent<ButtonBlinker>().sprite2;
            betOkBtn.gameObject.transform.GetChild(0).GetComponent<Text>().text = "BET OK";
            PlayerPrefs.SetInt("funclosed",1);
            betOkBtn.interactable = true;
            betOkBtn.gameObject.SetActive(true);
            repeatBtn.gameObject.SetActive(false);
            //StartCoroutine(betOkBtn.GetComponent<ButtonBlinker>().TriggerBlinking());
            //arrow.GetComponent<ButtonBlinker>().StopBlinking();
            clearBtn.interactable = true;
            cancelSpecificBtn.interactable = true;
            isPreviousBetPlaced = true;
            totalBet = betHolder.Sum();
            balance -= currentlySectedChip;
            //Debug.Log("Betholder  " + betHolder.Sum());
            //btnREf.transform.GetChild(0).GetComponent<Text>().text = betHolder[betIndex].ToString() == "0" ? string.Empty : betHolder[betIndex].ToString();
            //Debug.Log("currently selcted"+currentlySectedChip);
            betvaluetxt[betIndex].text = betHolder[betIndex].ToString();
            // SoundManager.instance.PlayClip("addbet");
            
            balanceText.text = balance.ToString("F2");
            UpdateUi();//clear
        }
        private void LoadDefaultData()
        {
            lastwinNo = -1;
            canPlaceBet = false;
            isthisisAFirstRound = true;
            isLastGameWinAmountReceived = true;
            isTimerStarted = false;
            isdataLoaded = false;
            isSomethingWentWrong = false;
            isCurrentBetPlace = false;
            // winningAmount = string.Empty;
        }

        void UpdateRoundData(CurrenRoundInfo curretRoundInfo, bool isFirstARound = false)
        {

            //Debug.Log("Update round");
            if (curretRoundInfo.gametimer < minmumTimeAllowed)
            {
                messageText.text = "Please wait";
                //AndroidToastMsg.ShowAndroidToastMessage("Please wait");
                //Debug.Log("Please wait");
                currentTime = 0;
                UpdateUi();//function not called
                return;
            }
            isdataLoaded = true;
            isTimeUp = false;
            isPreviousBetPlaced = false;
            //canPlaceBet = true;
            roundcount = curretRoundInfo.RoundCount.ToString();
            //balance = curretRoundInfo.balance;//////////////////////////////////////////////////////////////////////////////////////////////////////////
           //Debug.Log( curretRoundInfo.balance + "/////////////////////////////////////////////////////////////");
            // balance = 10000;
            totalBet = 0;
            int arrayLimit = curretRoundInfo.previousWinData.Count - 1;
            /*
            while()
            */
            for(int j = 0; j < curretRoundInfo.previousWinData.Count; j++)
            {
                previousWinNOsTxt[j].text = curretRoundInfo.previousWinData[j].winNo.ToString();
            }
            curretRoundInfo.previousWinData.Reverse();
            //Debug.Log("is a first round " + isFirstARound );
            if (isFirstARound)
            {
                if (tempRoundCount == roundcount)
                {

                }
                int lastroundNo = curretRoundInfo.previousWinData[0].winNo;
                string winx = curretRoundInfo.previousWinData[0].winx;
                StartCoroutine(Timer(curretRoundInfo.gametimer));

                SpinWheelWithoutPlugin.instane.SetWheelInitialAngle(lastroundNo, winx);
            }
            else
            {
                if (previousBet.Sum() > 0)
                    betOkBtn.gameObject.transform.GetChild(0).GetComponent<Text>().text = "Pre";
            }


            tempRoundCount = roundcount;
            UpdateUi();//function not called
        }
        public void SomeThingWentWrong()
        {
            StopAllCoroutines();
            isSomethingWentWrong = true;
            LoadDefaultData();
            balance = 0;
            totalBet = 0;
            previousWinAmount = 0;
            UpdateUi();//function not called
            try
            {

                StartNextOrNewRound(true);
            }
            catch (Exception e)
            {
                Debug.Log(e.Message);
                Debug.Log(e.StackTrace);
                throw;
            }
            m.print("something went wrong called");
        }
        private void UpdateUi()
        {
            //balanceText.text = balance.ToString();
            // winnigText.text = winningAmount;
            winnigText.text = winnerAmount.ToString();//
            totalBetText.text = totalBet.ToString();
        }
        void RemoveSocketListners(){
            FunTarget_ServerRequest.instance.RemoveListners(Utility.Events.OnWinNo);
            FunTarget_ServerRequest.instance.RemoveListners(Utility.Events.OnTimerStart);
            // FunTarget_ServerRequest.instance.RemoveListners(Utility.Events.OnDissconnect);
            FunTarget_ServerRequest.instance.RemoveListners(Utility.Events.OnWinAmount);
            FunTarget_ServerRequest.instance.RemoveListners(Utility.Events.OnTimeUp);

        }
        private void OnBetCalculation()
        {
            if (!isdataLoaded)
            {
                m.print("please wait data to load");
                messageText.text = "Please wait for data to load";
                //AndroidToastMsg.ShowAndroidToastMessage("please wait data to load");
                return;
            }
            if (previousBet.Sum() != 0 && !isPreviousBetPlaced
                && betOkBtn.gameObject.transform.GetChild(0).GetComponent<Text>().text == "Pre")
            {
                PlacePreviousBets();
                betOkBtn.gameObject.transform.GetChild(0).GetComponent<Text>().text = "BET OK";
                UpdateUi();
                return;
            }
            if (betHolder.Sum() == 0)
            {
                countrounds++;
                Debug.Log("countrounds"+countrounds);
                if(countrounds >=3)
                {
                    //close();
                    Debug.Log("thrownout because countrounds was greater than 3");
                    //throwout();
                }
                UpdateUi();
                //return;
            }
            isBetConfirmed = true;
            // if (!canPlaceBet)
            // {winnerAmount
            //     return;
            // }
            canPlaceBet = false;
            isUserPlacedBets = true;
            betOkBtn.interactable = clearBtn.interactable = doubleBtn.interactable = repeatBtn.interactable = 
                 cancelSpecificBtn.interactable = false;
            Array.Copy(betHolder, previousBet, betHolder.Length);
            PlayerPrefs.SetString("OnBetConfirmed","true");
            SendBets();
            UpdateUi();
           if (spinnow)
            {
                messageText.text = "Bets have been accepted";
            }
           else
           {
                messageText.text = "Please Collect the winning amount first ";
           }
            //AndroidToastMsg.ShowAndroidToastMessage("Bets Confirmed");
            //closebtn.interactable = false;
            //repeatBtn.GetComponent<ButtonBlinker>().StopBlinking();
            // if(repeatBtn.GetComponent<ButtonBlinker>() != null)
            // {
            //     repeatBtn.GetComponent<ButtonBlinker>().StopBlinking();
            // }
        }

        void SendTakeAmountRequest()
        {
            //Debug.Log("the winner amount is greater than zero" + winnerAmount);
            if (!spinnow)
            {
                
                balanceText.text = balance.ToString("F2");
                
                
                Take_Bet data = new Take_Bet()
                {
                    playerId = "GK"+PlayerPrefs.GetString("email"),
                    winpoint = int.Parse(winnigText.text)//winnerAmount
                };
                
                messageText.text = "Please Bet to Start Game . Minimum Bet =1 ";
                //FunTarget_ServerRequest.instance.socket.Emit(Utility.Events.OnWinAmount, new JSONObject (JsonConvert.SerializeObject(data) ));
                Debug.Log("the values are taken now");
                StartCoroutine(coinanimation());
                StartCoroutine(emptywinresponse());
                FunTargetGamePlay.Instance.ResetAllBets();
                //takeBtn.interactable = false;
                isBetConfirmed = false;
                repeatBtn.interactable = true;
                // StartCoroutine(repeatBtn.GetComponent<ButtonBlinker>().TriggerBlinking());
                // Debug.Log("Previous win data Size :" + previousWinsdata.Count );
                setinitialarray(previousWinsdata);
                repeatBtn.gameObject.SetActive(true);
                takeBtn.gameObject.SetActive(false);
                betOkBtn.gameObject.SetActive(false);
                
               
                //startprevious();
                //
                //spinnow = true;
                //winnerAmount = 0;
                //winnigText.text = winnerAmount.ToString();
                // FunTarget_ServerRequest.instance.SendEvent( Utility.Events.OnWinAmount, o, (res) =>
                // {/*
                //     WinAmountConfirmation winAmountConfirmation = JsonConvert.DeserializeObject<WinAmountConfirmation>(res);
                //         if (winAmountConfirmation.status)
                //         {
                //             // LastWinImg.gameObject.SetActive(false);
                //             betOkBtn.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "BET OK";
                //             canPlaceBet = true;
                //             isLastGameWinAmountReceived = true;
                //             Debug.Log("Amount Successfully Added");
                //             // currentComment = "Amount Successfully Added";
                //             balance = winAmountConfirmation.data.win_points;
                //             winningAmount = string.Empty;
    
                //             // if (previousBet.Sum() > 0)
                //             //     betOkBtn.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Pre";
                //         }
                //         else
                //         {
                //             // currentComment = winAmountConfirmation.message;
                //             Debug.Log("error msg:   " + winAmountConfirmation.message);
                //         }
                //     */
    
                //     UpdateUi();//clear
                // });
            }
        }
        

        /// <summary>
        /// This function just copy the PreviousBetArray into currenBetArray
        /// and updates the UI
        /// </summary>
        private void PlacePreviousBets()
        {
            bool isEnoughBalance = previousBet.Sum() < balance;
            if (!isEnoughBalance)
            {
                m.print("not enough balance");

                messageText.text = "Not enough balance";
                //AndroidToastMsg.ShowAndroidToastMessage("not enough balance");
                return;
            }   
            //Array.Copy(previousBet, betHolder, previousBet.Length);
            for (int i = 0; i < previousBet.Length; i++)
            {
                betHolder[i]+=previousBet[i];
            }
            

            oneBetBtn.transform.GetChild(0).GetComponent<Text>().text = betHolder[1] == 0 ? string.Empty : betHolder[1].ToString();
            twoBetBtn.transform.GetChild(0).GetComponent<Text>().text = betHolder[2] == 0 ? string.Empty : betHolder[2].ToString();
            threeBetBtn.transform.GetChild(0).GetComponent<Text>().text = betHolder[3] == 0 ? string.Empty : betHolder[3].ToString();
            fourBetBtn.transform.GetChild(0).GetComponent<Text>().text = betHolder[4] == 0 ? string.Empty : betHolder[4].ToString();
            fiveBetBtn.transform.GetChild(0).GetComponent<Text>().text = betHolder[5] == 0 ? string.Empty : betHolder[5].ToString();
            sixBetBtn.transform.GetChild(0).GetComponent<Text>().text = betHolder[6] == 0 ? string.Empty : betHolder[6].ToString();
            sevenBetBtn.transform.GetChild(0).GetComponent<Text>().text = betHolder[7] == 0 ? string.Empty : betHolder[7].ToString();
            eightBetBtn.transform.GetChild(0).GetComponent<Text>().text = betHolder[8] == 0 ? string.Empty : betHolder[8].ToString();
            nineBetBtn.transform.GetChild(0).GetComponent<Text>().text = betHolder[9] == 0 ? string.Empty : betHolder[9].ToString();
            zeroBetBtn.transform.GetChild(0).GetComponent<Text>().text = betHolder[0] == 0 ? string.Empty : betHolder[0].ToString();
            isPreviousBetPlaced = true;
            totalBet = betHolder.Sum();
            balance -= totalBet;
            balanceText.text = balance.ToString("F2");
            totalBetText.text = totalBet.ToString();
            //UpdateUi();
            for (int i = 0; i < previousBet.Length; i++)
            {
                previousBet[i] = 0;
            }

        }
        public IEnumerator startprevious(int[] values)
        {
            for (int i = 0; i < values.Length; i++)
            {
                previousWinsdata.Add(values[i]);
                //PreviousWin_list.Add(values[i]);
            }
            while(previousWinsdata.Count >9)
            {
                previousWinsdata.RemoveAt(0);
                //PreviousWin_list.RemoveAt(0);
                
            }
            for(int i = 0; i < previousWinsdata.Count; i++)
            {
                previousWinsText[i].GetComponent<Text>().text = previousWinsdata[i].ToString();
                //PreviousWin_Text[i].GetComponent<Text>().text =  PreviousWin_list[i].ToString();
            }
            yield return null;
        }
        public void SendBets_Response(object data)
        {
            SendBet_Res res = JsonConvert.DeserializeObject<SendBet_Res>(data.ToString());//AndarBahar.Utility.Fuction.GetObjectOfType<SendBet_Res>(data);    
            if (res.status == 200)
            {
                //winnigText.text = res.message;
                balance = res.data.balance;
                balanceText.text = balance.ToString("F2");
            }
            else if(res.status == 222)
            {
                clearb();
                messageText.text = "Bets already accepted";
                
            }
            else if (res.status == 400)
            {
                //winnigText.text = res.message;
                balance = res.data.balance;
            }
            else{
                Debug.Log("none of the above");
            }
        }
        

        int currentTime = 0;
        int countrounds;
        /// <summary>
        /// This is the 60 sec timer 
        /// </summary>
        /// <param name="counter"></param>
        /// <returns></returns>
        public IEnumerator Timer(int counter = 40) //60 
        {
            isTimeUp = false;
            /*betOkBtn.interactable =  repeatBtn.interactable  */clearBtn.interactable = doubleBtn.interactable = takeBtn.interactable = cancelSpecificBtn.interactable = true;
            if(winnerAmount ==0)
            {
                if (repeatBtn.gameObject.activeInHierarchy)
                {
                    repeatBtn.interactable = true;
                    //StartCoroutine(repeatBtn.GetComponent<ButtonBlinker>().TriggerBlinking());
                }
            }
            isUserPlacedBets = false;
            if(counter ==0)//10
            {
                if(FunTarget_ServerResponse.instance.fromcurrent == true)
                {
                    //waiting.SetActive(true);
                    //FunTarget_ServerResponse.instance.waitpanel.SetActive(true);
                    
                }
                
            }
            // if(counter >6)
            // {
            //     isBetConfirmed = false;
            // }
            // else
            // {
            //     isBetConfirmed = true;
            // }
            //isBetConfirmed = false;
            //canPlaceBet = true;
            while (counter > 0)
            {
                //if (isSomethingWentWrong) yield break;
                //closebtn.interactable = true;
                if(FunTarget_ServerResponse.instance.fromcurrent)
                {
                   
                    yield return null;
                }
                else
                {
                    yield return new WaitForSeconds(1f);
                }
                //yield return null;//new WaitForSeconds(1f);
                FunTarget_ServerResponse.instance.fromcurrent = false;
                counter--;
                timerAudio.Play();
                // if(counter == 20)
                // {
                //     // if(winnerAmount > 0)
                //     // {
                //     //     SendTakeAmountRequest();
                //     // }
                // }
                if(counter == 15)
                {
                   // StartCoroutine(timerText.GetComponentInParent<ButtonBlinker>().TriggerBlinking());
                }
                if (counter == 10)
                {
                    //timerText.GetComponentInParent<ButtonBlinker>().StopBlinking();
                    
                    //OnBetCalculation();
                //   repeatBtn.interactable = betOkBtn.interactable = clearBtn.interactable = doubleBtn.interactable = false;
                    canPlaceBet = false;
                    //Debug.Log("/////////////////////////////////////////////"+spinnow);
                    if (!isBetConfirmed)
                    {
                        if(totalBet > 0)
                        {
                            countrounds =0;
                            //OnBetCalculation();
                        }
                        else
                        {
                            countrounds++;
                            Debug.Log("countrounds");
                            if(countrounds >=3)
                            {
                                //close();
                                Debug.Log("thrownout because countrounds was bigger than 3");
                                //throwout();
                            }
                        }
                        
                        if(spinnow)
                        {
                            
                            SendBets();
                            messageText.text = "Bet Time Over";
                            // if(totalBet > 0)
                            // {

                            // }
                            //AndroidToastMsg.ShowAndroidToastMessage("Bets Confirmed");
                            //closebtn.interactable = false;
                        }
                        // else if(totalBet > 0)
                        // {
                        //     countrounds =0;
                        //     //OnBetCalculation();
                        // }
                        // else
                        // {
                        //     countrounds++;
                        //     Debug.Log("countrounds");
                        //     if(countrounds >=3)
                        //     {
                        //         //close();
                        //         Debug.Log("thrownout because countrounds was bigger than 3");
                        //         throwout();
                        //     }
                        // }
                    }
                }
                if (counter == 0 )
                {

                    //isBetConfirmed = true;
                }
                currentTime = counter;
                timerText.text = counter.ToString();
                MonitorBets();
                bool canShowRingAnimation = counter < 20 && counter > 10;
                if (canShowRingAnimation)
                {
                    //Debug.Log("countdownmusic");
                    //SoundManager.instance?.PlayClip("countdown");
                }
                if (counter < 12) isTimeUp = true;
                // if(counter ==2)//10
                // {
                //     string playerName = "RL" + PlayerPrefs.GetString("email");
                //     Info data = new Info{
                //         playerId = playerName
                //     };
                //     FunTarget_ServerRequest.instance.socket.Emit(Utility.Events.OnWinNo,new JSONObject(JsonConvert.SerializeObject(data)));
                //     closebtn.interactable = false;
                // }

            }
            

        }
        public void throwout()
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.SetInt("thrownout",1);
            
            FunTarget_ServerRequest.instance.socket.Emit(Utility.Events.onleaveRoom);
            
            //FunTarget_ServerRequest.instance.SendEvent(Utility.Events.onleaveRoom);
            //SceneManager.LoadScene("Login");
        }

        void MonitorBets()
        {
            bool canPostBetsToServer = isTimeUp || isUserPlacedBets;
            if (canPostBetsToServer)
            {
                if (canPlaceBet)
                {
                    canPlaceBet = false;
                    if (!isUserPlacedBets)
                    {
                        
                    }
                        // SendBets();
                }
                betOkBtn.interactable
                    = clearBtn.interactable
                    = doubleBtn.interactable
                    = repeatBtn.interactable
                    = cancelSpecificBtn.interactable = false;
                    // = takeBtn.interactable = false;
            }
        }

        /// <summary>
        /// this will call before 6 sec from the server
        /// </summary>
        /// <param name="res"></param>
        public int winimage;
        public int lastwin;
        public WeelData o;
        public IEnumerator showwin()
        {
            yield return new WaitForSeconds(1f);
            for (int i = 0; i < previousWinsdata.Count; i++)
            {
                previousWinsText[i].GetComponent<TextMeshProUGUI>().text = previousWinsdata[i].ToString();
            }
            StartCoroutine(funtargetwinresponse());
            winnigText.text = winnerAmount.ToString();
        }
        public IEnumerator OnRoundEnd(string res)
        {
            //FunTarget_ServerResponse.instance.roundcounttillnow++;
            // if(!spinnow)
            // {
            //     yield break;
            // }
            yield return new WaitUntil(() => currentTime == 0);
            // StartCoroutine(Timer( 60 ));
            messageText.text = "FOR AMUSEMENT ONLY NO CASH VALUE";
            //Debug.Log("on round end");
            //Debug.Log(res);
            // ResetAllBets();
            o = JsonConvert.DeserializeObject<WeelData>(res.ToString());
            // FunTarget_ServerRequest.instance.ListenEvent(Utility.Events.OnPlayerWin, (json) =>
            // {
            //     Debug.LogError("Onplayer win  " + json.ToString());
            //     OnPlayerWindata res = JsonConvert.DeserializeObject<OnPlayerWindata>(json.ToString());
            //     winnerAmount = res.winAmount;
            // });
            
            //winnerAmount = o.winPoint;
            //winround = balance + winnerAmount;
            //winimage = o.imagenumber;
            //winnigText.text = winnerAmount.ToString();
            Debug.Log("winningSpot  " + o.winningSpot);
            int no = o.winNo;
            lastwin  =o.winNo;
            // int no = 2;
            int somenumber = o.imagenumber;
            string xImg = o.winX;
            if(o.winX == "2X")
            {
                somenumber= 4;
            }
            // string xImg = "1x";

            lastwinNo = no;// = o.previousWin_single[o.previousWin_single.Count -1];
            
            if(spinnow)
            {
                StartCoroutine(Spin(no, somenumber));//xImg
                center_anime.SetBool("rotate",true);
                //StartCoroutine(funtargetwinresponse());
            }
            //spinnow = false;
            //StartCoroutine(Spin(no, xImg));

            /*for(int i = o.previousWin_single.Count - 1; i >= o.previousWin_single.Count - 10 ; i--)
            {
                if(previousWinsdata.Count >= 9)
                {
                    previousWinsdata.RemoveAt(8);
                    previousWinsdata.Add(o.previousWin_single[i]);
                    break;
                }
                else
                {
                    previousWinsdata.Add(o.previousWin_single[i]);
                }
            }*/
            while(o.previousWin_single.Count > 10)
            {
                o.previousWin_single.RemoveAt(0);
            }
            previousWinsdata.Clear();
            for (int i = 0; i < o.previousWin_single.Count-1 ; i++)
            {
                previousWinsdata.Add(o.previousWin_single[i]);
            }
            // previousWinsdata.RemoveAt( previousWinsdata.Count -1 );
            previousWinsdata.Add(o.winNo);
            // previousWinsdata.Add(no);
            //Debug.Log("COunt"+previousWinsdata.Count);

            //for(int i = 0; i < previousWinsdata.Count; i++)
            //for(int i = 0; i < o.previousWin_single.Count; i++)
            //{
            //    //Debug.Log("iiiiiiiiiii"+i);
            //    previousWinsText[i].GetComponent<TextMeshProUGUI>().text = o.previousWin_single[i].ToString();
            //}
        }
        public IEnumerator funtargetwinresponse()
        {
            
            WWWForm form = new WWWForm();
            string playername = "GK"+PlayerPrefs.GetString("email");
            int gameid  = 8;
            form.AddField("playerId", playername);
            form.AddField("game_id",gameid);
            using (UnityWebRequest www = UnityWebRequest.Post(winamounturl, form))
            {
                yield return www.SendWebRequest();
                
                if (www.result != UnityWebRequest.Result.Success)
                {
                    Debug.LogError(www.error);
                }
                else
                {
                    Debug.Log("Responce for triple win  " +www.downloadHandler.text);
                    winneramount response = JsonConvert.DeserializeObject<winneramount>(www.downloadHandler.text);
                    //winPoint = response.data.Winamount;
                    if (response.status == 200)
                    {
                        winnerAmount = response.data.Winamount;
                        winround = balance + winnerAmount;
                        winnigText.text = response.data.Winamount.ToString();
                        if(response.data.Winamount == 0)
                        {
                            FunTargetGamePlay.Instance.ResetAllBets();
                        }
                        //winround = balance + winningAmount;
                        //Debug.Log("/////////////////////////////////////////" +response.data.Winamount );
                        if(started)
                        {
                           winnigText.text = response.data.Winamount.ToString();
                           if(response.data.Winamount >0)
                           {
                                repeated = false;
                                messageText.text = "Please Collect Winning Amount";
                                betOkBtn.gameObject.SetActive(false);
                                takeBtn.gameObject.SetActive(true);
                                repeatBtn.gameObject.SetActive(false);
                                StartCoroutine(previousdata());
                                //showui();
                                //restoredata();
                                //showui();
                           }
                           else
                           {
                                messageText.text = "Please Bet to Start Game . Minimum Bet =1 ";
                           }
                            started = false;
                        }
                        else
                        {
                            if (int.Parse(FunTargetGamePlay.Instance.winnigText.text) > 0)
                            {
                                betOkBtn.gameObject.SetActive(false);
                                takeBtn.gameObject.SetActive(true);
                                repeatBtn.gameObject.SetActive(false);
                                //StartCoroutine(FunTargetGamePlay.Instance.takeBtn.GetComponent<ButtonBlinker>().TriggerBlinking());
                            }
                            else
                            {
                                FunTargetGamePlay.Instance.ResetAllBets();
                            }   
                        }
                    }
                    //winround = AUI.balance+winPoint;
                }
            }
        }
        float partialwin;
        public void testing()
        {
            
            StartCoroutine(funtargetwinresponse());
        }

        public IEnumerator emptywinresponse()
        {
            
            WWWForm form = new WWWForm();
            string playername = "GK"+PlayerPrefs.GetString("email");
            int gameid  = 8;
            form.AddField("playerId", playername);
            form.AddField("game_id",gameid);
            using (UnityWebRequest www = UnityWebRequest.Post(emptyurl, form))
            {
                yield return www.SendWebRequest();
                
                if (www.result != UnityWebRequest.Result.Success)
                {
                    Debug.LogError(www.error);
                }
               else
                {
                    winnerAmount = 0;
                    balanceText.text = balance.ToString("F2");
                    winnigText.text = winnerAmount.ToString();
                    //Debug.Log("The data should be deleted  "+ www.downloadHandler.text );
                    winclear response = JsonConvert.DeserializeObject<winclear>(www.downloadHandler.text);
                    //winPoint = response.data.Winamount;
                    if (response.status == 200)
                    {
                        //winningAmount = response.data.Winamount;
                        // Debug.Log("The data should be deleted for PlayerId: "+"GK"+PlayerPrefs.GetString("email"));
                        //StartCoroutine(funtargetwinresponse());
                    }
                }
                
            }
        }
        // public void test( float amt)
        // {
        //     balance = amt;
        //     winnerAmount = 45000;
        //     winround = balance + winnerAmount;

        //     balanceText.text = balance.ToString();
        //     winnigText.text = winnerAmount.ToString();

        //     Debug.Log("Started Animations " + balance + " winner amt " +winnerAmount  + " winRound " + winround);
        //     StartCoroutine(coinanimation());
        // }
        IEnumerator coinanimation()
        {
            float localbalance = balance;
            float localwinpoint = winnerAmount;
            // partialwin = winningAmount;
            float elapsedTime = 0f;
            float deductionPercentage =  0.1f;
            while (elapsedTime <4f)
            {
                // Debug.Log("in while loop " + winnerAmount + " Local Balance" + localbalance + " normal before blance " + balance + " Time PAssed " + elapsedTime  + " final balance " + winround);

                float deductionValue = (winnerAmount * deductionPercentage);
                    
                    // Deduct the value from the current variable
                winnerAmount -= Mathf.RoundToInt(deductionValue);
                localbalance += Mathf.RoundToInt(deductionValue);
                balanceText.text = localbalance.ToString("F2");
                winnigText.text = winnerAmount.ToString();
                    
                    // Make sure we don't go below zero
                //winPoint = Mathf.Max(winPoint, 0);
                if(winnerAmount == 5 || localbalance > winround  )
                {
                    balance = winround;//balance+winnerAmount;//winround;//+= winPoint;
                    winnerAmount = 0;
                    balanceText.text = balance.ToString("F2");
                    winnigText.text = winnerAmount.ToString();
                    //Debug.Log("while Break ");
                    break;
                }
    
                yield return null;
                elapsedTime += Time.fixedDeltaTime;
                
            }
            
            balance = winround;//balance+winnerAmount;//winround;//+= winPoint;
            winnerAmount = 0;
            balanceText.text = balance.ToString("F2");
            winnigText.text = winnerAmount.ToString();

            // Update the elapsed time
            // elapsedTime += Time.fixedDeltaTime;
        }

        public IEnumerator Spin(int winNo, int xFactorImage)
        {
            yield return new WaitUntil(() => currentTime <= 0);
            SpinWheelWithoutPlugin.instane.Spin(winNo, xFactorImage); //xFactorImage);
            isdataLoaded = true;
            canPlaceBet = true;
            SpinWheelWithoutPlugin.instane.onSpinComplete = () =>
            {
                Debug.Log("Winner = " + winNo);
                // winNumber.text = winNo.ToString();
                // StartNextOrNewRound();
                Button[] betBtns = betButtons.GetComponentsInChildren<Button>();
                foreach(Button button in betBtns)
                {
                    if(button.gameObject.name == winNo.ToString())
                    {
                        //StartCoroutine(button.GetComponent<ButtonBlinker>().TriggerBlinking());
                        //Debug.Log(button.transform.GetChild(0).transform.GetChild(0).GetComponent<Text>().text);
                        if(int.Parse(winnigText.text) > 0)
                        {
                            messageText.text = "Congratulations! You Win.";
                        }
                    }
                }
                //StartCoroutine(arrow.GetComponent<ButtonBlinker>().TriggerBlinking());
                winnerAudio.Play();
                // previousWinsdata.RemoveAt(0);
                // previousWinsdata.Add(winNo);
                //wintextupdate();

                for (int i = 0; i < previousWinsdata.Count; i++)
                {
                    //Debug.Log("iiiiiiiiiii"+i);
                    previousWinsText[i].GetComponent<TextMeshProUGUI>().text = previousWinsdata[i].ToString();
                    //previousWinsText[i].GetComponent<TextMeshProUGUI>().text = o.previousWin_single[i].ToString();
                }
            };
            wheelAudio.Play();

            //winnigText.text = winnerAmount.ToString();

            // #if UNITY_ANDROID

                // SpinWheelWithoutPlugin.instane.Spin(winNo, xFactorImage);
                // SpinWheelWithoutPlugin.instane.onSpinComplete = () =>
                // //SpinWheel.instane.SpinTheWheel(winNo, xFactorImage);
                // //SpinWheel.instane.onSpinComplete = () =>
                // { StartNextOrNewRound(); };
            // #else
            //     SpinWheelWithoutPlugin.instane.Spin(winNo, xFactorImage);
            //     SpinWheelWithoutPlugin.instane.onSpinComplete = () =>
            //     { StartNextOrNewRound(); };
            // #endif
        }
        public void testingcamera()
        {
            Debug.Log("Testing,Testing");
            GetComponent<ObjectScreenshot>().TakeScreenshot();
        }
        public IEnumerator takeblink ()
        {
            takeBtn.GetComponent<Image>().color = new Color32(255,255,255,255);
            //takeBtn.interactable = true;
            yield return new WaitForSeconds(0.2f);
            takeBtn.GetComponent<Image>().color = new Color32(200,200,200,128);
            //takeBtn.interactable = false;
            yield return new WaitForSeconds(0.2f);
            if(int.Parse(winnigText.text) >0)
            {
                StartCoroutine(takeblink());
            }

        }
        public void wintextupdate()
        {
            winnigText.text = winnerAmount.ToString();
            winnerAudio.Play();
            
        }
        private void StartNextOrNewRound(bool isAFirstRound = false)
        {
            // FunTarget_ServerRequest.instance.SendEvent(Utility.Events.OnUserInfo, null, (res) =>
            // {
            //     Debug.Log("new round started");
            //     Debug.Log(res);
            //     canPlaceBet = true;
            //     isBetConfirmed = false;
            //     isdataLoaded = false;
            //     UpdateUi();
            //     var o = JsonConvert.DeserializeObject<CurrenRoundInfo>(res);
            //     UpdateRoundData(o, isAFirstRound);
            // }, Utility.Events.OnCurrentTimer);

            canPlaceBet = true;
            // isBetConfirmed = false;
            isdataLoaded = false;
            UpdateUi();//function not executed
            isTimerStarted = false;
            betOkBtn.interactable = false;
            ResetAllBets();
        }
        private void SendBets()
        {
            //.Log("the data has been sent");
            // if (betHolder.Sum() == 0)
            // {
            //     //UpdateUi();
            //     return;
            // }
            if(!spinnow)
            {
                messageText.text = "Please collect winning amount first";
                return;
            }
            if(totalBet >0)
            {
                currentround();
                storedata();
                Debug.Log("the values have been stored in a safe place");
            }
            
            // if(totalBet > 0)
            // {
            //     countrounds =0;
            //     Debug.Log("countrounds"+countrounds);
            //     //OnBetCalculation();
            // }
            // else{
            //     countrounds++;
            //     Debug.Log("countrounds"+countrounds);
            //     if(countrounds >=3)
            //     {
            //         //close();
            //         Debug.Log("thrownout because  something went wrong in send bets");
            //         throwout();
            //     }
            // }
            Button[] betBtns = betButtons.GetComponentsInChildren<Button>();
            foreach (Button button in betBtns)
            {
                //button.GetComponent<ButtonBlinker>().StopBlinking();
                // if (button.name != "BetValue")
                // {
                //     if (button.GetComponent<ColorBlinker>().isBlinking)
                //     {
                //         button.GetComponent<ColorBlinker>().StopBlinking();
                //         Debug.Log("Resetting " + button.gameObject.name);
                //     }
                // }
            }
            
            // for(int i = 0; i < betHolder.Length; i++)
            // {
            //     if( betHolder[i] > 0 )
            //     {
            //         Bets_Container.Add(i);
            //     }
            // }
            /////commented just now
            // if (betOkBtn.gameObject.activeInHierarchy)
            // {
            //     betOkBtn.GetComponent<ButtonBlinker>().StopBlinking();
            // }
            string _playerName = "GK" + PlayerPrefs.GetString("email");

            Bet data = new Bet
            {
                
                playerId = _playerName,
                BetOnZero = betHolder[0],
                BetOnOne = betHolder[1],
                BetOnTwo = betHolder[2],
                BetOnThree = betHolder[3],
                BetOnFour = betHolder[4],
                BetOnFive = betHolder[5],
                BetOnSix = betHolder[6],
                BetOnSeven = betHolder[7],
                BetOnEight = betHolder[8],
                BetOnNine = betHolder[9],
                
            };
            //Debug.Log("zero:" + betHolder[0]+"one:"+betHolder[1]+"two:"+betHolder[2]+"three:"+betHolder[3]+"four:"+betHolder[4]+"five:"+betHolder[5]+"six:"+betHolder[6]+"seven"+betHolder[7]+"eight:"+betHolder[8]+"nine:"+betHolder[9] );
            PostBet(data);
            Debug.Log(new JSONObject(JsonConvert.SerializeObject(data)));
            //countrounds = 0;
            canPlaceBet = false;
        }
        private void PostBet(Bet data)
        {
            omegaCounter++;
            FunTarget_ServerResponse.instance.socket.Emit(Utility.Events.OnBetsPlaced,new JSONObject(JsonConvert.SerializeObject(data)));
            // FunTarget_ServerRequest.instance.SendEvent(Utility.Events.OnBetsPlaced, data, (res) =>
            // {
            //     var response = JsonConvert.DeserializeObject< DoubleChanceClasses.BetConfirmation>(res);
            //     //Debug.Log("is bet placed starus with statu - " + JsonUtility.FromJson<DoubleChanceClasses.BetConfirmation>(res).status);

            // });

        }
        private void OnWinTheBet(int winnigNO)
        {
            isLastGameWinAmountReceived = false;
            canPlaceBet = false;
            betOkBtn.gameObject.transform.GetChild(0).GetComponent<Text>().text = "Take";
            m.print("last round won and win no is" + winnigNO);
            //UpdateUi();
        }
        private void changecolor(GameObject coin)
        {
            ColorBlock cb = coin.GetComponent<Toggle>().colors;
            cb.normalColor = new Color(132,59,59,255);
            //coin.GetComponent<Toggle>().colors = new Color32(132,59,59,255);//color = new Color(132,59,59,255);
        }
        public void imageselect(int number)
        {
            for (int i = 0; i < coinimages.Length; i++)
            {
                if( number == i)
                {
                    coinimages[i].GetComponent<Image>().color = new Color32(255,255,255,255);
                }
                else{
                    
                    coinimages[i].GetComponent<Image>().color = new Color32(255,255,255,170);
                }
                
            }
        }
        
        
    }
    public class TimerClass
    {
        public int result;//timer
    }
    public class Take_Bet
    {
        public string playerId;
        public int winpoint;
    }
}

namespace FunTargetClasses
{
    public class PreviousWinData
    {
        public double RoundCount;
        public int winNo;
        public string winx;
    }
    [SerializeField]
    public class CurrenRoundInfo
    {
        public int gametimer;
        public float balance;
        public double RoundCount;
        public List<PreviousWinData> previousWinData;
        // public OnWinAmount pendingWinningData;
    }


    public class WeelData
    {
        public int RoundCount;
        public int winNo;
        public string winX;
        public int winningSpot;
        public int winPoint;
        public List<int> previousWin_single;
        public int imagenumber;
        
    }

    public class Weel
    {
        public bool status;
        public string message;
        public WeelData data;
    }

    public class Timer
    {
        public int result;//timer
    }

    public class OnWinAmount
    {
        public bool status;
        public string message;
        public OnWinAmountData data;
    }
    public class OnWinAmountData
    {
        public long RoundCount;
        public int win_no;
        // public int winPoints;
        public int win_points;
        public string player_id;
        public string balance;
    }

    public class WinAmountConfirmationData
    {
        public string playerId;
        public int balance;
    }

    public class WinAmountConfirmation
    {
        public bool status;
        public string message;
        public WinAmountConfirmationData data;
    }

    public class OnPlayerWindata
    {
        public int winAmount;
    }

    public class PreviousRoundBets
    {
        public int isCurrRoundBet;
        public int no_0;
        public int no_1;
        public int no_2;
        public int no_3;
        public int no_4;
        public int no_5;
        public int no_6;
        public int no_7;
        public int no_8;
        public int no_9;
    }

    public class Bet
    {
        public string playerId;
        public int BetOnZero;
        public int BetOnOne;
        public int BetOnTwo;
        public int BetOnThree;
        public int BetOnFour;
        public int BetOnFive;
        public int BetOnSix;
        public int BetOnSeven;
        public int BetOnEight;
        public int BetOnNine;
        
        // public string balance;
        // public int[] BetsValue;
        // public int[] BetsAmount;
    }
    public class BetConfirmation
    {
        public string status;
        public string message;
    }
    public class storingbet
    {
        public int[] funtarget_previous;
        public int funtargetwinning;

    }
    public struct savekarle
    {
        public int amount;
        public int[] bets;

    }
    public class arrayLimit
    {
        public int gametimer;
        public List<int> previousWins; 
    }
    public class details
    {
        public string playerid;
    }
    public class SendBet_Res
        {
            public int status;
            public string message;
            public SendBet_Res_Data data;
        }

    public class SendBet_Res_Data
    {
        public float balance;
    }
    public class PreveiousBet
    {
        public int status;
        public string message;
        public PreveiousBetData data;
    } 

    public class PreveiousBetData
    {
        public int zero;

        public int one;

        public int two;

        public int three;

        public int four;

        public int five;

        public int Six;

        public int Seven;

        public int Eight;

        public int Nine;
    }
}
