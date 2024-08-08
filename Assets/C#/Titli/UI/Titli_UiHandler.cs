using System;
using Com.BigWin.WebUtils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Linq;
using SocketIO;
using Newtonsoft.Json;
using LobbyScripts;
using Shared;
using Titli.Utility;
using Titli.player;
using Titli.Gameplay;
using Titli.ServerStuff;



namespace Titli.UI
{
    public class Titli_UiHandler : MonoBehaviour
    {
        public static Titli_UiHandler Instance;
        public Chip currentChip;
        public GameObject[] chipimg;
        public float balance;
        int UmbrellaBets;
        int BallBets;
        int DogBets;
        int GoatBets;
        int DiyaBets;
        int KiteBets;
        int FlowerBets;
        int RoseBets;
        int DeerBets;
        int ButterflyBets;
        int RabbitBets;

        int PigeonBets;
        int FunGameBets;
        public int[] betsholder = new int[12];
        public int[] PreviousbetHolder = new int[12];
        public Text UmbrellaBetsTxt, GoatBetsTxt, PigeonBetsTxt, BallBetsTxt, DiyaBetsTxt, RabbitBetsTxt, DogBetsTxt, RoseBetsTxt, 
        FlowerBetsTxt, KiteBetsTxt, ButterflyBetsTxt, DeerBetsTxt,NameTxt,FunGameTxt;
        [SerializeField] Text balanceTxt;
        [SerializeField] GameObject messagePopUP;
        [SerializeField] Text msgTxt;
        [SerializeField] iTween.EaseType easeType;
        public Button DoubleUp;
        public Button repeatBtn;
        public Button clearBtn;
        public Button SpinBtn;
        [SerializeField] Text totalBetsTxt;
        int totalBetsValue;
        public SceneHandler sceneHandler;
        public Transform[] spawnpos;
        float chipMovetime = .5f;
        public bool playerwon;
        public bool zoomed;

        void Awake()
        {
            Instance = this;
            Screen.orientation = ScreenOrientation.LandscapeLeft;
        }
        // Start is called before the first frame update
        void Start()
        {
            //sceneHandler = GameObject.Find("SceneHandler").GetComponent<SceneHandler>();
            NameTxt.text = "FUN"+PlayerPrefs.GetString("email");
            //ResetUi();
            AddListeners();
            totalBetsValue = 0;
            totalBetsTxt.text = totalBetsValue.ToString();
            balance = PlayerPrefs.GetFloat("points");
            //NameTxt.text = UserDetail.Name;
            UpdateUi();
            ChipImgSelect(0);
            zoomed = true;
        }
        private void Update() 
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                if(Application.platform == RuntimePlatform.Android)
                {
                    ExitLobby();
                }
            }
        }
        
        public int getBalance()
        {
            return int.Parse(balance.ToString());
        }

        private void AddListeners()
        {
            DoubleUp.onClick.AddListener(OnClickOnDoubleBetBtn());

            repeatBtn.onClick.AddListener(() =>
            {
                RepeatBets();
            });

            clearBtn.onClick.AddListener(() =>
            {
                ClearBets();
            });

            SpinBtn.onClick.AddListener(() =>
            {
                ClaimSpin();
            });
        }

        public void BetValueSelect(int val)
        {
            if(val ==1)
            {
                currentChip = Chip.Chip1;
            }
            
            else if (val == 10)
            {
                currentChip = Chip.Chip10;
            }
            else if(val ==25)
            {
                currentChip = Chip.Chip25;
            }
            else if (val == 50)
            {
                currentChip = Chip.Chip50;

            }
            else if (val == 100)
            {
                currentChip = Chip.Chip100;
            }
            else if (val == 500)
            {
                currentChip = Chip.Chip500;
            }
            else if (val == 1000)
            {
                currentChip = Chip.Chip1000;
            }
            else if (val == 200)
            {
                currentChip = Chip.Chip200;
            }      
        }    
        public void ChipImgSelect(int ind)
        {
            for (int i = 0; i < chipimg.Length; i++)
            {
                chipimg[i].SetActive(false);
            }
            chipimg[ind].SetActive(true);
        }

        public void AddBets(Spots spot)
        {
            balance -= (float)currentChip;
            totalBetsValue += (int)currentChip;

            switch (spot)
            {
                case Spots.Umbrella:UmbrellaBets += (int)currentChip; betsholder[0] = UmbrellaBets;
                    break;
                case Spots.Goat:GoatBets += (int)currentChip;  betsholder[1] = GoatBets;
                    break;
                case Spots.Pigeon:PigeonBets += (int)currentChip; betsholder[2] = PigeonBets;
                    break;
                case Spots.Ball:BallBets += (int)currentChip; betsholder[3] = BallBets;
                    break;
                case Spots.Diya:DiyaBets += (int)currentChip; betsholder[4] = DiyaBets;
                    break;
                case Spots.Rabbit:RabbitBets += (int)currentChip; betsholder[5] = RabbitBets;
                    break;
                case Spots.Dog:DogBets += (int)currentChip; betsholder[6] = DogBets;
                    break;
                case Spots.Rose:RoseBets += (int)currentChip; betsholder[7] = RoseBets;
                    break;
                case Spots.Flower:FlowerBets += (int)currentChip; betsholder[8] = FlowerBets;
                    break;
                case Spots.Kite:KiteBets += (int)currentChip; betsholder[9] = KiteBets;
                    break;
                case Spots.Butterfly:ButterflyBets += (int)currentChip; betsholder[10] = ButterflyBets;
                    break;
                case Spots.Deer:DeerBets += (int)currentChip; betsholder[11] = DeerBets;
                    break;
                case Spots.FunGame:FunGameBets += (int) currentChip; betsholder[12] += FunGameBets;
                    break;
                default:
                    break;
            }
            //Debug.Log(betsholder[].ToArray());
            UpdateUi();
        }

        public bool IsEnoughBalancePresent()
        {
            return balance - (float)currentChip > 0;
        }

        public void RepeatBets()
        {
            
            for(int i = 0; i < betsholder.Length; i++ )
            {
                betsholder[i] = PreviousbetHolder[i]; 
                
            }
            UmbrellaBets = betsholder[0];
            GoatBets = betsholder[1];
            PigeonBets = betsholder[2];
            BallBets = betsholder[3];
            DiyaBets = betsholder[4];
            RabbitBets = betsholder[5];
            DogBets = betsholder[6];
            RoseBets = betsholder[7];
            FlowerBets = betsholder[8];
            KiteBets = betsholder[9];
            ButterflyBets = betsholder[10];
            DeerBets = betsholder[11]; 
            FunGameBets = betsholder[12];
            totalBetsValue = betsholder.Sum();
            balance -= totalBetsValue;
            for(int i = 0 ;i < betsholder.Length;i++)
            {
                if(betsholder[i] > 0 && betsholder[i] < 50)
                {
                    //Titli_CardController.Instance.CreateChip(s)
                    currentChip = Chip.Chip10;
                    GameObject chipInstance = Titli_ChipSpawner.Instance.Spawn(0, currentChip, spawnpos[i]);
                    iTween.MoveTo(chipInstance, iTween.Hash("position", spawnpos[i].position, "time", chipMovetime, "easetype", easeType));
                    //Titli_CardController.Instance.StartCoroutine((MoveChip(chipInstance, spawnpos[i].position)));

                    //Titli_ChipSpawner.Instance.Spawn(PositionAsUV1,Chip.Chip10,)
                    //Debug.Log("Green at " +i +" rank as the value is " + betsholder[i] );
                }
                else if(betsholder[i] >= 50 && betsholder[i] < 100)
                {
                    currentChip = Chip.Chip50;
                    
                    GameObject chipInstance = Titli_ChipSpawner.Instance.Spawn(0, currentChip, spawnpos[i]);
                    iTween.MoveTo(chipInstance, iTween.Hash("position", spawnpos[i].position, "time", chipMovetime, "easetype", easeType));
                    

                    Debug.Log("Orange at " +i +" rank as the value is " + betsholder[i] );
                }
                else if(betsholder[i] >= 100 && betsholder[i] < 200)
                {
                    currentChip = Chip.Chip100;
                    GameObject chipInstance = Titli_ChipSpawner.Instance.Spawn(0, currentChip, spawnpos[i]);
                    iTween.MoveTo(chipInstance, iTween.Hash("position", spawnpos[i].position, "time", chipMovetime, "easetype", easeType));
                    Debug.Log("Blue at " +i +" rank as the value is " + betsholder[i] );
                }
                else if(betsholder[i] >= 200 && betsholder[i] < 500)
                {
                    currentChip = Chip.Chip200;
                    GameObject chipInstance = Titli_ChipSpawner.Instance.Spawn(0, currentChip, spawnpos[i]);
                    iTween.MoveTo(chipInstance, iTween.Hash("position", spawnpos[i].position, "time", chipMovetime, "easetype", easeType));
                    Debug.Log("Darkblue at " +i +" rank as the value is " + betsholder[i] );
                }
                else if(betsholder[i] >= 500 && betsholder[i] < 1000)
                {
                    currentChip = Chip.Chip500;
                    GameObject chipInstance = Titli_ChipSpawner.Instance.Spawn(0, currentChip, spawnpos[i]);
                    iTween.MoveTo(chipInstance, iTween.Hash("position", spawnpos[i].position, "time", chipMovetime, "easetype", easeType));
                    Debug.Log("Red at " +i +" rank as the value is " + betsholder[i] );
                }
                else if(betsholder[i] >= 1000 )
                {
                    currentChip = Chip.Chip1000;
                    GameObject chipInstance = Titli_ChipSpawner.Instance.Spawn(0, currentChip, spawnpos[i]);
                    iTween.MoveTo(chipInstance, iTween.Hash("position", spawnpos[i].position, "time", chipMovetime, "easetype", easeType));
                    Debug.Log("Purple at " +i +" rank as the value is " + betsholder[i] );
                }
                //Debug.Log("Bettttttssssssholder value" +betsholder[i]);
                //if(betsholder[i] > 0)
                //{
                    //spwnhere
                //}
            }
            //Titli_ChipSpawner.Instance.Spawn();
            UpdateUi();
        }
        

        public UnityAction OnClickOnDoubleBetBtn()
        {
            return () =>
            {
                if (betsholder.Sum() == 0)
                {
                    Debug.Log("no bet placed yet");
                    return;
                }

                bool isEnoughBalance = balance > betsholder.Sum() * 2;

                if (!isEnoughBalance)
                {
                    Debug.Log("not enough balance");
                    return;
                }
                balance += betsholder.Sum();

                for (int i = 0; i < betsholder.Length; i++)
                {
                    betsholder[i] *= 2;
                }

                UmbrellaBetsTxt.text = betsholder[0].ToString() == "0" ? string.Empty : betsholder[0].ToString();
                GoatBetsTxt.text = betsholder[1].ToString() == "0" ? string.Empty : betsholder[1].ToString();
                PigeonBetsTxt.text = betsholder[2].ToString() == "0" ? string.Empty : betsholder[2].ToString();
                BallBetsTxt.text = betsholder[3].ToString() == "0" ? string.Empty : betsholder[3].ToString();
                DiyaBetsTxt.text = betsholder[4].ToString() == "0" ? string.Empty : betsholder[4].ToString();
                RabbitBetsTxt.text = betsholder[5].ToString() == "0" ? string.Empty : betsholder[5].ToString();
                DogBetsTxt.text = betsholder[6].ToString() == "0" ? string.Empty : betsholder[6].ToString();
                RoseBetsTxt.text = betsholder[7].ToString() == "0" ? string.Empty : betsholder[7].ToString();
                FlowerBetsTxt.text = betsholder[8].ToString() == "0" ? string.Empty : betsholder[8].ToString();
                KiteBetsTxt.text = betsholder[9].ToString() == "0" ? string.Empty : betsholder[9].ToString();
                ButterflyBetsTxt.text = betsholder[10].ToString() == "0" ? string.Empty : betsholder[10].ToString();
                DeerBetsTxt.text = betsholder[11].ToString() == "0" ? string.Empty : betsholder[11].ToString();
                FunGameTxt.text = betsholder[12].ToString() == "0" ? string.Empty : betsholder[12].ToString();
                
                UmbrellaBets = betsholder[0];
                GoatBets = betsholder[1];
                PigeonBets = betsholder[2];
                BallBets = betsholder[3];
                DiyaBets = betsholder[4];
                RabbitBets = betsholder[5];
                DogBets = betsholder[6];
                RoseBets = betsholder[7];
                FlowerBets = betsholder[8];
                KiteBets = betsholder[9];
                ButterflyBets = betsholder[10];
                DeerBets = betsholder[11];

                balance -= betsholder.Sum();
                totalBetsValue = betsholder.Sum();
                UpdateUi();
            };
        }

        public void ClearBets()
        {
            foreach (var item in Titli_CardController.Instance.chipclone)
            {
                Destroy(item);
            }
            balance += totalBetsValue;
            ResetUi();
            //  

        }

        public void ClaimSpin()
        {
            Titli_Timer.Instance.OnTimeUp();
            // Titli_ServerResponse.Instance.TimerFunction();
            Titli_CardController.Instance._startCardBlink = true;
            Titli_CardController.Instance._canPlaceBet = false;
            Titli_CardController.Instance._winNo = true;
            foreach(var item in Titli_CardController.Instance._cardsImage)
            {
                item.GetComponent<Button>().interactable = false;
            }
            Titli_CardController.Instance.CardBlink_coroutine = Titli_CardController.Instance.CardsBlink();
            StartCoroutine(Titli_CardController.Instance.CardsBlink());
        }

        public void UpDateBalance(float amount)
        {
            Debug.Log("balance updated");
            // balance = amount;
            //balance = 10000f;
            UpdateUi();
        }
    

        public void ResetUi()
        {
            // Titli_MainPlayer.Instance.totalBet = 0;
            //balance += totalBetsValue;
            //Debug.Log("UIIIIIIIIII resettted");
            for(int i = 0; i < betsholder.Length; i++ )
            {
                //Debug.Log("PPPPrevious bet hoilder value"+PreviousbetHolder[i] + "  value of i"+i +"  BEEETTHOLDER "+betsholder[i]);
                PreviousbetHolder[i] = betsholder[i]; 
                betsholder[i] = 0;
                //Debug.Log("PPPPrevious bet hoilder value"+PreviousbetHolder[i]);
            }
            Titli_BetManager.Instance.ClearBet();

            //Titli_CardController.Instance.StartCoroutine(DestroyChips());
            UmbrellaBets = 0;
            GoatBets = 0;
            PigeonBets = 0;
            BallBets = 0;
            DiyaBets = 0;
            RabbitBets = 0;
            DogBets = 0;
            RoseBets = 0;
            FlowerBets = 0;
            KiteBets = 0;
            ButterflyBets = 0;
            DeerBets = 0;
            FunGameBets = 0;
            totalBetsValue = 0;
            //for(int i = 0; i < betsholder.Length; i++ )
            //{
                //PreviousbetHolder[i] = betsholder[i]; 
            //}
            //for (int i = 0; i < betsholder.Length; i++)
            //{
                //betsholder[i] = 0;
            //}
            UpdateUi();
        }

        public void UpdateUi()
        {
            balanceTxt.text = balance.ToString();
            totalBetsTxt.text = totalBetsValue.ToString();
            if (UmbrellaBets >0)
            {
                UmbrellaBetsTxt.text = UmbrellaBets.ToString();
            }
            if (GoatBets >0)
            {
                GoatBetsTxt.text = GoatBets.ToString();
            }
            if (PigeonBets>0)
            {
                PigeonBetsTxt.text = PigeonBets.ToString();
            }
            if (BallBets>0)
            {
                BallBetsTxt.text = BallBets.ToString();
            }
            if (DiyaBets>0)
            {
                DiyaBetsTxt.text = DiyaBets.ToString();
            }
            if (RabbitBets>0)
            {
                RabbitBetsTxt.text = RabbitBets.ToString();
            }
            if (DogBets>0)
            {
                DogBetsTxt.text = DogBets.ToString();
            }
            if (RoseBets>0)
            {
                RoseBetsTxt.text = RoseBets.ToString();
            }
            if (FlowerBets>0)
            {
                FlowerBetsTxt.text = FlowerBets.ToString();
            }
            if (KiteBets>0)
            {
                KiteBetsTxt.text = KiteBets.ToString();
            }
            if (ButterflyBets>0)
            {
                ButterflyBetsTxt.text = ButterflyBets.ToString();
            }
            if (DeerBets>0)
            {
                DeerBetsTxt.text = DeerBets.ToString();
            }
            if (FunGameBets>0)
            {
                FunGameTxt.text = FunGameBets.ToString();
            }
        }

        public void ShowMessage(string msg)
        {
            // messagePopUP.SetActive(true);
            // msgTxt.text = msg;
        }

        public void HideMessage()
        {
            // messagePopUP.SetActive(false);
            // msgTxt.text = string.Empty;
        }

        public bool ExitBlock = false;
        public void ExitLobby()
        {
            if (ExitBlock)
            {
                return;   
            }
            else
            {
                Titli_ServerRequest.instance.socket.Emit(Utility.Events.onleaveRoom);
                //Titli_ServerResponse.Instance.socketsoff();
                //PlayerPrefs.SetInt("Mainscene", 1); 
                PlayerPrefs.SetString("Points", balance.ToString());
            }
            // sceneHandler.unloadAddressableScene();
            //SceneManager.LoadScene("MainScene");
            //Titli_ServerRequest.instance.socket.Emit(Events.onleaveRoom);
            //SceneManager.LoadScene("MainScene");
            //sceneHandler.unloadAddressableScene();

        }

        public IEnumerator OnPlayerWin()
        {
            yield return new WaitForSeconds(3.0f);
            // Win win = Dragon.Utility.Utility.GetObjectOfType<Win>(o);
            if (betsholder[Titli_RoundWinningHandler.Instance.WinNo] != 0)
            {
                // Titli_MainPlayer.Instance.winner(Convert.ToInt32(win.winAmount));
                // balance += win.winAmount;
                balance = (betsholder[Titli_RoundWinningHandler.Instance.WinNo] * 10) + balance;
                playerwon =true;
                //ShowMessage("You Won!!");
            }
            //ShowMessage("You Won!!");
            UpdateUi();
        }
        [SerializeField] Text zoomtxt;
        
        public void zoomfunction()
        {
            if(zoomed)
            {
                zoomed = false;
                zoomtxt.text = "OFF";
            }
            else
            {
                zoomed = true;
                zoomtxt.text = "ON";
            }
        }
        public void sendbets()
        {
            Titli_CardController.Instance._canPlaceBet = false;
            for(int i = 0; i < Titli_CardController.Instance.TableObjs.Count; i++)
            {
                Titli_CardController.Instance.TableObjs[i].GetComponent<BoxCollider2D>().enabled = true;
            }
            
            Debug.Log("Sending bets");
            roundend end = new roundend()
            {
                playerId = PlayerPrefs.GetString("email"),
                umbrella_total_bets = UmbrellaBets,
                goat_total_bets = GoatBets,
                pigeon_total_bets = PigeonBets,
                ball_total_bets = BallBets,
                diya_total_bets = DiyaBets,
                rabbit_total_bets = RabbitBets,
                dog_total_bets = DogBets,
                rose_total_bets = RoseBets,
                flower_total_bets = FlowerBets,
                kite_total_bets = KiteBets,
                butterfly_total_bets = ButterflyBets,
                deer_total_bets = DeerBets,
                fungame_total_bets = FunGameBets
            };
            
            Titli_ServerRequest.instance.socket.Emit(Utility.Events.OnBetsPlaced,new JSONObject(JsonConvert.SerializeObject(end)));
            
            //Debug.Log("user :"+ end.playerId + "Umbrella" + end.umbrella_total_bets + " Ball" + end.ball_total_bets + " Sun :" + end.sun_total_bets + " Cow:" + end.cow_total_bets + " Candle:" + end.candel_total_bets + " Bucket:" + end.bucket_total_bets+ " Pidegeon:" + end.pigeon_total_bets + " Rabbit:" + end.rabbit_total_bets +" Butterfly:" + end.butterfly_total_bets+" Rose:" +end.rose_total_bets+" Spinner:" +end.lattu_total_bets+" Kite:" +end.kite_total_bets );
            //Titli_ServerRequest.instance.socket.Emit(Events.OnBetsPlaced,new JSONObject(JsonConvert.SerializeObject(end)));


        }
    }

}

[Serializable]
public class Win
{
    public float winAmount = 10.0f;
}
public class roundend
{
    public string playerId;
    public int umbrella_total_bets;
    public int goat_total_bets;
    public int pigeon_total_bets;
    public int ball_total_bets;
    public int diya_total_bets;
    public int rabbit_total_bets;
    public int dog_total_bets;
    public int rose_total_bets;
    public int flower_total_bets;
    public int kite_total_bets;
    public int butterfly_total_bets;
    public int deer_total_bets;
    public int fungame_total_bets;

    
    

}