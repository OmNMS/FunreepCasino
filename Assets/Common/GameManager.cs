using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net.NetworkInformation;
using Components;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using Controllers;
using System.Linq;
using System.Threading.Tasks;
using ViewModel;
using UnityEngine.UI;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        // What the roullete game is currently
        // What the preferences of the user and the pass rounds
        // Load and unload roulletes
        // Keep track of the game state
        // Generate other persitente systems.
        
        public static GameManager Instance; // A static reference to the GameManager instance
        public CharacterTable characterTable;
        public CharacterMoney charMoney;
        private protected string URL_PATH;
        public GameObject[] SystemPrefabs;
        
        private protected List<GameObject> _instanceSystemPrefabs;
        private protected List<AsyncOperation> _loadOperations;
        private GameState _currentGameState = GameState.PREGAME;
        private string _currentLevelName = string.Empty;

        public Text BalanceTxt;
       [HideInInspector] public int balance;

        public List<Text> LastFive = new List<Text>();
        public Text Winner;

        // [SerializeField] public ChipSelected CSel;

        
       [HideInInspector] public int BetValue = 0;
        public Text PlacedBetsTxt;


        public String UrlDataPath
        {
            get{ return URL_PATH;}
        }

        void Awake()
        {
            if(Instance == null) // If there is no instance already
            {
                DontDestroyOnLoad(gameObject); // Keep the GameObject, this component is attached to, across different scenes
                Instance = this;
            } else if(Instance != this) // If there is already an instance and it's not `this` instance
            {
                Destroy(gameObject); // Destroy the GameObject, this component is attached to
            }

           
           // balance = int.Parse(BalanceTxt.ToString());

        }

        public void TakeMoney()
        {
           BalanceTxt.text = balance.ToString();
        }


        async void Start() 
        {
            // Persistance instance
            URL_PATH = Application.persistentDataPath + "/Saves/";
            charMoney.characterMoney.Value = PlayerPrefs.GetInt("Balance",10000);
            balance = charMoney.characterMoney.Value;
            BalanceTxt.text = charMoney.characterMoney.ToString();
            PlayerPrefs.SetInt("Balance", charMoney.characterMoney.Value);
           

            // Start game persistence
            var tasks = new Task[2];

            await StartRouletteInstance();
            await StartRouletteGame();
        

        }

        void AddSocketListners()
        {
           Action onBadResponse = () => {  };

            
            // RouletteScripts.ServerStuff.Roulette_ServerRequest.intance.ListenEvent<weelNumbers>( Utility.Events.OnWinNo, (json) =>
            // {
            //     StartCoroutine(OnRoundEnd(json.ToString()));
            // }, onBadResponse);

            // TripleChance_ServerRequest.intance.ListenEvent(Utility.Events.OnWinNo, (json) => 
            // {
            //     // Debug.LogError("onwin  " + json.ToString());
            //     StartCoroutine(OnRoundEnd(json));
            // });

            // TripleChance_ServerRequest.intance.ListenEvent(Utility.Events.OnTimerStart, (json) =>
            // {
            //     Enable_OverrideSorting();
            //     // Debug.LogError("timer  " + json.ToString());
            //     OnTimerStart(json);
            // });

            // TripleChance_ServerRequest.intance.ListenEvent<DoubleChanceClasses.OnWinAmount>(Utility.Events.OnWinNo, (json) =>
            // {
            //     OnWinAmount(json.ToString());
            // }, onBadResponse);


            // TripleChance_ServerRequest.intance.ListenEvent(Utility.Events.OnTimeUp, (json) =>
            // {
            //     BettingButtonInteractablity(false);
            //     isTimeUp = true;
            // });

        }

        public async Task StartRouletteInstance()
        {   
            // Create game instance undestroyable.
            _instanceSystemPrefabs = new List<GameObject>();
            _loadOperations = new List<AsyncOperation>();

            GameObject prefabsInstance;
            
            //for(int i = 0; i < SystemPrefabs.Length;i++)
            //{
            //    prefabsInstance = Instantiate(SystemPrefabs[i]);
            //    _instanceSystemPrefabs.Add(prefabsInstance);
            //}

            await Task.Yield();
            
        }

        private async Task StartRouletteGame()
        {
            // Initialize save directory
            CheckDirectory();

            await CreateNewPlayer();

            // Initialize game
            StartRound();
        }
        void CheckDirectory()
        {
            Debug.Log($"Directory: {URL_PATH}");

            // Check if the save directory exists
            if(!Directory.Exists(URL_PATH))
            {
                Directory.CreateDirectory(URL_PATH);
            }
        }
        private async Task CreateNewPlayer() 
        {
            string playerPath = URL_PATH+"player";
            await Player.CreatePlayer(characterTable, "MatiV154", playerPath);
        }
        
        private void StartRound()
        {
           // Initialize round components
            ToggleGame();
        }

        // States controller
        private async void UpdateState(GameState state)
        {
            GameState previousGameState = _currentGameState;
            _currentGameState = state;

            switch (_currentGameState)
            {
                case GameState.PAUSED:
                    Time.timeScale = 0.0f;
                    break;
                case GameState.RUNNING:
                    await OnGameOpened();
                    characterTable.OnLoadGame
                        .OnNext(true);
                    Time.timeScale = 1.0f;
                    break;
                case GameState.REWARD:
                    OnGameClosed();
                    Time.timeScale = 1.0f;
                    break;
            }
            Debug.Log($"[GameManager] Current state game is now {_currentGameState.ToString()}");
        }

        // Player event
        public void TogglePauseGame()
        {
            UpdateState(GameState.PAUSED);
        }
        public void ToggleRewardSystem()
        {
            UpdateState(GameState.REWARD);
        }
        public void ToggleGame()
        {
            UpdateState(GameState.RUNNING);
        }

        // Unity event
        public void OnGameClosed() 
        {
            Debug.Log("Game have been closed! Files was saved!");  

            characterTable.OnSaveGame
                .OnNext(true);

            characterTable.OnResetGame
                .OnNext(true);

            characterTable.currentNumbers.Clear();
            characterTable.currentTableInGame.Clear();
        }
        public async Task OnGameOpened() 
        { 
            await Task.Delay(TimeSpan.FromSeconds(1));

            // Update round parameters
            characterTable.currentTableActive.Value = false; 
            characterTable.currentTableCount = 0;
            characterTable.currentTable.Clear();
            characterTable.currentNumbers.Clear();
            characterTable.currentTableInGame.Clear();
            characterTable.lastNumber = 0;
            characterTable.lastTable.Clear();
            
            await Task.Delay(TimeSpan.FromSeconds(2));

            characterTable.currentTableActive.Value = true; 
            characterTable.currentChipSelected = characterTable.chipData.Where(chip => chip.chipkey == KeyFicha.Chip1).First();

            await Task.Yield();
        }
        protected void OnApplicationPause()
        {
            if(Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.Android)
                OnGameClosed();
        }
        protected void OnApplicationQuit()
        {
            if(Application.platform == RuntimePlatform.WindowsEditor)
                OnGameClosed();
        }
        protected void OnDestroy() 
        {
            if(Instance == this)
            {
                Instance = null;
            }

            if(_instanceSystemPrefabs == null)
                return;
            
            for(int i = 0; i < _instanceSystemPrefabs.Count; i++)
            {
                Destroy(_instanceSystemPrefabs[i]);
            }
            _instanceSystemPrefabs.Clear();
        }

        // Loaders
        public void LoadScene(string levelName)
        {
            SceneManager.LoadScene(levelName);
        }

        int click = 0;
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                click++;
                ShowAndroidToastMessage("Press again to exit");
                Debug.Log("Prss again to exit");
                StartCoroutine(ClickAgain());
                if(click > 1)
                {
                    Application.Quit();
                    // SceneManager.LoadScene(0);
                }
            }

            if(int.Parse(PlacedBetsTxt.text) <= 0)
            {
                PlacedBetsTxt.text = "0";
            }
        }


        private void SetFive()
        {

        }



        public void BetsPlaced(int x)
        {
            BetValue = BetValue + x;
            PlacedBetsTxt.text = BetValue.ToString();
        }

        IEnumerator ClickAgain()
        {
            yield return new WaitForSeconds(0.5f);
            click = 0;
        }


        public static void ShowAndroidToastMessage(string message = "something went wrong")
        {
            if (Application.platform != RuntimePlatform.Android) return;

#if UNITY_ANDROID
            AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject unityActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");

            if (unityActivity != null)
            {
                AndroidJavaClass toastClass = new AndroidJavaClass("android.widget.Toast");
                unityActivity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
                {
                    AndroidJavaObject toastObject = toastClass.CallStatic<AndroidJavaObject>("makeText", unityActivity, message, 0);
                    toastObject.Call("show");
                }));
            }
#endif
        }

    }

    // Pregame, Runing, Paused, Reward
    public enum GameState
    {
        PREGAME,
        RUNNING,
        PAUSED,
        REWARD
    }




}
