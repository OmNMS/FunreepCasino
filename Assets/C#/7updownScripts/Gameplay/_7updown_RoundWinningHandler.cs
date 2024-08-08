using UpDown7.Utility;
using System;
using System.Collections;
using Shared;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Newtonsoft.Json;
using UnityEngine.Networking;

namespace Updown7.Gameplay
{
    class _7updown_RoundWinningHandler : MonoBehaviour
    {
        public static _7updown_RoundWinningHandler Instance;
        public Text Winner_text;
        [SerializeField] GameObject leftRing;
        [SerializeField] GameObject middleRing;
        [SerializeField] GameObject rightRing;

        public Sprite[] dice;
        public Image[] previousWins;
        public Sprite[] previosuWins_sprite;
        public GameObject leftDice;
        public GameObject rightDice;
        public Action<object> onWin;
        public _7updown_Timer timer;
        public List<int> previouswinList;
        public int winPoint;
        public int winningAmount =0;
        float winround;
        string winamounturl ="http://139.59.92.165:5000/user/Winamount";
        string emptyurl ="http://139.59.92.165:5000/user/DeletePreviousWinamount";
        bool spinnow
        {
            get{
                if(int.Parse(Winner_text.text)>0)
                {
                    return false;
                }
                else{
                    return true;
                }
            }
        }

        private void Awake()
        {
            Instance = this;
        }
        private void Start()
        {
            timer = GetComponent<_7updown_Timer>();
            timer.onTimeUp += () => isTimeUp = true;
            timer.onCountDownStart += () => isTimeUp = false;

            onWin += OnWin;
            leftDice.SetActive(false);
            rightDice.SetActive(false);
            chipController = GetComponent<_7updown_ChipController>();
            botManager = GetComponent<_7updown_BotsManager>();
            //spinnow = true;
        }
        bool isTimeUp;
        public void SetWinNumbers(object o)
        {
            InitialData winData = UpDown7.Utility.Utility.GetObjectOfType<InitialData>(o);

            while (winData.previousWins.Count > 8)
            {
                winData.previousWins.RemoveRange(0,winData.previousWins.Count - 8);
            }
            previouswinList = winData.previousWins;

            for (int i = 0; i < previousWins.Length; i++)
            {
                int totalDiceNo = winData.previousWins[i];
                if (totalDiceNo < 7)
                {
                    // previousWins[i].color = Color.red;
                    previousWins[i].GetComponent<Image>().sprite = previosuWins_sprite[0];
                }
                else if (totalDiceNo == 7)
                {
                    // previousWins[i].color = Color.blue;
                    previousWins[i].GetComponent<Image>().sprite = previosuWins_sprite[1];
                }
                else
                {
                    // previousWins[i].color = Color.green;
                    previousWins[i].GetComponent<Image>().sprite = previosuWins_sprite[2];
                }
                previousWins[i].gameObject.SetActive(true);
                previousWins[i].transform.GetChild(0).GetComponent<Text>().text = totalDiceNo.ToString();

            }
        }
        float forone;
        float final;
        public void TakeBet()
        {
            if(spinnow)
            {
                return;
            }
            //if(int.Parse(Winner_text.text) >0) return;
            winPoint = int.Parse(Winner_text.text);
            //if (winPoint == 0) return;
            Debug.Log("Win Point:"+winPoint );
            Take_Bet data = new Take_Bet()
            {
                playerId = "GK"+PlayerPrefs.GetString("email"),
                winpoint = winPoint
            };
            calculation();
            //spinnow = true;

            // Updown7.UI._7updown_UiHandler.Instance.balance += winPoint;
            // winPoint = 0;
            // Winner_text.text = winPoint.ToString();
            // Updown7.UI._7updown_UiHandler.Instance.balanceTxt.text = Updown7.UI._7updown_UiHandler.Instance.balance.ToString();

           // ServerStuff.ServerRequest.instance.socket.Emit(Events.OnWinAmount, new JSONObject ( Newtonsoft.Json.JsonConvert.SerializeObject(data) ));
            UI._7updown_UiHandler.Instance.ResetUi();
            StartCoroutine(emptysevenonwinresponse());
        }
        float partialwin;
        public void calculation()
        {
            
            // Winner_text.text = winPoint.ToString();
            // forone = winPoint/5;
            // final = 1/forone;
            StartCoroutine(coinanimation());
        }
        IEnumerator coinanimation()    
        {
            float localbalance = UI._7updown_UiHandler.Instance.balance;
            float localwinpoint = winPoint;
            float elapsedTime = 0f;
            float deductionPercentage = 0.1f;
            while (elapsedTime <4f)
            {
                float deductionValue = (winPoint * deductionPercentage);
                //_7updown_UiHandler.instance
                // Deduct the value from the current variable
                winPoint -= Mathf.RoundToInt(deductionValue);
                localbalance+= Mathf.RoundToInt(deductionValue);
                UI._7updown_UiHandler.Instance.balanceTxt.text = localbalance.ToString();
                Winner_text.text = winPoint.ToString();
                if(winPoint == 5)
                {
                    UI._7updown_UiHandler.Instance.balance += localwinpoint;//+= winPoint;
                    winPoint = 0;
                    UI._7updown_UiHandler.Instance.balanceTxt.text = UI._7updown_UiHandler.Instance.balance.ToString();
                    Winner_text.text = winPoint.ToString();
                    break;
                }
                
                // Make sure we don't go below zero
                //winPoint = Mathf.Max(winPoint, 0);
    
                yield return null;
                elapsedTime += Time.deltaTime;
                
            }
            
            

            
            elapsedTime += Time.deltaTime;
            ////////////////////////////////////////////////////////////////////////

            // int i =1;
            // if(winPoint < 50)
            // {
            //     i = 500;
            // }
            // else if((winPoint >= 50)&& (winPoint < 500))
            // {
            //     i = 5000;
            // }
            // else if((winPoint >= 500)&& (winPoint < 5000))
            // {
            //     i = 50000;
            // }
            // else if((winPoint >= 5000)&& (winPoint < 50000))
            // {
            //     i = 500000;
            // }
            // else if(winPoint>= 50000)
            // {
            //     i = 5000000;
            // }
            // partialwin = winPoint/(i*i);
            // while(winPoint>0)
            // {
                
            //     winPoint = winPoint-1;
            //     Debug.Log("dfcvgbhnm"+winPoint);
            //     Updown7.UI._7updown_UiHandler.Instance.balance++;
            //     Winner_text.text = winPoint.ToString();
            //     SoundManager.instance.PlayClip("addchip");
            //     Updown7.UI._7updown_UiHandler.Instance.balanceTxt.text = Updown7.UI._7updown_UiHandler.Instance.balance.ToString();
            //     yield return new WaitForSeconds(partialwin);

            // }
        }
        public void precheck()
        {
            if(winPoint!=0)
            {
                TakeBet();
            }
            else{
                return;
            }
        }
        public void store()
        {
            //Sevenuplast.winbalance = int.Parse(Winner_text.text);
        }

        public class Take_Bet
        {
            public string playerId;
            public int winpoint;
        }
        public GameObject stopbetting;

        public void OnWin(object o)
        {
            // if (timer.is_a_FirstRound) return;
            //timer.sevenround++;
            stopbetting.SetActive(false);
            if(!spinnow)
            {
                //chipController.TakeChipsBack(winnerSpot);
                return;
            }
            //spinnow = false;
            DiceWinNos winData = JsonConvert.DeserializeObject<DiceWinNos>(o.ToString());//UpDown7.Utility.Utility.GetObjectOfType<DiceWinNos>(o);
            int winNumber = winData.winNo.Sum();
            //_7updown_Timer.Instance.sevenround++;
            //int winNumber = winData.winNo;
            //int leftone = UnityEngine.Random.Range(1,winNumber-1);
            //int rightone = UnityEngine.Random.Range(1,winNumber-leftone);
            //int leftone = System.Random.Range(1,winNumber-1);
            //Debug.Log("winnner number:"+ winNumber);
            int leftDiceNo = winData.winNo[0];
            int rightDiceNo = winData.winNo[1];
            StartCoroutine(sevenonwinresponse());
            //winningAmount = winData.winPoint;
            //winPoint += winData.winPoint;
            //winround = UI._7updown_UiHandler.Instance.balance+winPoint;
            

            leftDice.GetComponent<Image>().sprite = dice[leftDiceNo-1];
            rightDice.GetComponent<Image>().sprite = dice[rightDiceNo-1];
            int[] temp = new int[2];
            temp[0] = leftDiceNo;
            temp[1] = rightDiceNo;
    

    
            if (winNumber < 7)//winNumber
            {
                StartCoroutine(ShowWinningRing(leftRing, Spot.left,winData.winNo.ToArray()));
                //StartCoroutine(ShowWinningRing(leftRing, Spot.left, o,winData.winNo.ToArray()));
            }
            else if (winNumber == 7)//winNumber
            {
                StartCoroutine(ShowWinningRing(middleRing, Spot.middle,winData.winNo.ToArray()));
                //StartCoroutine(ShowWinningRing(middleRing, Spot.middle, o, winData.winNo.ToArray()));
            }
            else if(winNumber > 7)//winNumber
            {
                //Debug.Log("Rrrrrrrrrrrrrrrrrrrrrrrrrreaached" + winNumber);
                StartCoroutine(ShowWinningRing(rightRing, Spot.right,winData.winNo.ToArray()));
                //StartCoroutine(ShowWinningRing(rightRing, Spot.right, o, winData.winNo.ToArray()));
            }

            while(winData.previousWins.Count  > 8)
            {
                winData.previousWins.RemoveAt(0);
            }
            // previouswinList.Add(winNumber);

            for (int i = 0; i < previouswinList.Count; i++)
            {
                previouswinList[i] = winData.previousWins[i];
            }
            
            //Winner_text.text = winPoint.ToString();

            /*for (int i = 0; i < previousWins.Length; i++)
            {
                int totalDiceNo = previouswinList[i];
                if (totalDiceNo < 7)
                {
                    // previousWins[i].color = Color.red;
                    previousWins[i].GetComponent<Image>().sprite = previosuWins_sprite[0];
                }
                else if (totalDiceNo == 7)
                {
                    // previousWins[i].color = Color.blue;
                    previousWins[i].GetComponent<Image>().sprite = previosuWins_sprite[1];
                }
                else
                {
                    // previousWins[i].color = Color.green;
                    previousWins[i].GetComponent<Image>().sprite = previosuWins_sprite[2];
                }
                previousWins[i].gameObject.SetActive(true);
                previousWins[i].transform.GetChild(0).GetComponent<Text>().text = totalDiceNo.ToString();

            }*/

        }

        public IEnumerator sevenonwinresponse()
        {
            
            WWWForm form = new WWWForm();
            string playername = "GK" +PlayerPrefs.GetString("email");
            int gameid  = 1;
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
                    
                    winneramount response = JsonConvert.DeserializeObject<winneramount>(www.downloadHandler.text);
                    //winPoint = response.data.Winamount;
                    if (response.status == 200)
                    {
                        winningAmount = response.data.Winamount;
                        Debug.Log("/////////////////////////////////////////" +response.data.Winamount );
                    }
                    if(_7updown_Timer.Instance.started)
                    {
                        Winner_text.text = winningAmount.ToString();
                        if(response.data.Winamount >0)
                        {
                            UI._7updown_UiHandler.Instance.restore();
                        }
                        _7updown_Timer.Instance.started = false;

                    }
                    //winround = AUI.balance+winPoint;
                }
            }
        }
        public IEnumerator emptysevenonwinresponse()
        {
            
            WWWForm form = new WWWForm();
            string playername = "GK"+PlayerPrefs.GetString("email");
            int gameid  = 1;
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
                    winclear response = JsonConvert.DeserializeObject<winclear>(www.downloadHandler.text);
                    //winPoint = response.data.Winamount;
                    if (response.status == 200)
                    {
                        //winningAmount = response.data.Winamount;
                        Debug.Log("The data should be deleted for PlayerId: "+"GK"+PlayerPrefs.GetString("email"));
                    }
                }
            }
        }
        //public int pass;
        /*public void passjar(int pass)
        {
            tryjar(pass);
        }
        void tryjar(int temp)
        {
            int[] less = new int[] {4,2};
            int[] seven = new int[] {4,3};
            int[] more = new int[] {6,4};
            if (temp < 7)
            {
                StartCoroutine(ShowWinningRing(leftRing, Spot.left,less));
            }
            else if (temp == 7)
            {
                StartCoroutine(ShowWinningRing(middleRing, Spot.middle, seven));
            }
            else if(temp > 7)
            {
                //Debug.Log("Rrrrrrrrrrrrrrrrrrrrrrrrrreaached" + winNumber);
                StartCoroutine(ShowWinningRing(rightRing, Spot.right, more));
            }
        }*/

        _7updown_ChipController chipController;
        _7updown_BotsManager botManager;
        public _7updown_OnlinePlayerBets onlinePlayerBets;
        public _7updown_JarAnimation jarAnimation;
        IEnumerator ShowWinningRing(GameObject ring, Spot winnerSpot,int[] winNos) //object o,int[] winNos)
        {
            //Debug.Log("winning numbers are" +winNos);
            // if (!spinnow)
            // {
            //     yield return null;
            //     //jarAnimation.PlayAnimatin(winNos);
            // }
            //spinnow = false;
            jarAnimation.PlayAnimatin(winNos);
            yield return new WaitForSeconds(2.5f);
           
            ring.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            ring.SetActive(false);
            yield return new WaitForSeconds(0.3f);
            ring.SetActive(true);
            yield return new WaitForSeconds(0.2f);
            ring.SetActive(false);
            yield return new WaitForSeconds(0.1f);
            ring.SetActive(true);

            // botManager.UpdateBotData(o);
            // onlinePlayerBets.OnWin(o);
            
            yield return new WaitForSeconds(2f);
            ring.SetActive(false);
            winPoint += winningAmount;
            Winner_text.text = winPoint.ToString();
            chipController.TakeChipsBack(winnerSpot);
            // if(int.Parse( Winner_text.text) == 0)
            // {
            //     chipController.TakeChipsBack(winnerSpot);
            // }
            
            for (int i = 0; i < previousWins.Length; i++)
            {
                int totalDiceNo = previouswinList[i];
                if (totalDiceNo < 7)
                {
                    // previousWins[i].color = Color.red;
                    previousWins[i].GetComponent<Image>().sprite = previosuWins_sprite[0];
                }
                else if (totalDiceNo == 7)
                {
                    // previousWins[i].color = Color.blue;
                    previousWins[i].GetComponent<Image>().sprite = previosuWins_sprite[1];
                }
                else
                {
                    // previousWins[i].color = Color.green;
                    previousWins[i].GetComponent<Image>().sprite = previosuWins_sprite[2];
                }
                previousWins[i].gameObject.SetActive(true);
                previousWins[i].transform.GetChild(0).GetComponent<Text>().text = totalDiceNo.ToString();

            }
            jarAnimation.ResetJar();
        }
    }
}
public class DiceWinNos
{
    //public int winNo;
    public List<int> winNo;
    //public List<int> wins;
    public List<int> previousWins;
    public int winPoint;
    public int RoundCount;
}
public class winneramount
    {
        public int status;
        public string message;
        public data data;

    }
public class winclear
{
    public int status;
    public string message;

}
    public class data
    {
        public int Winamount;
    }