using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Titli.UI;
using Titli.Utility;
using KhushbuPlugin;
using Shared;

namespace Titli.Gameplay
{
    public class Titli_RoundWinningHandler : MonoBehaviour
    {
        public static Titli_RoundWinningHandler Instance;
        [SerializeField] List<GameObject> WinningRing;
        public Sprite[] Imgs;
        public Image[] previousWins;
        public List<int> PreviousWinValue;
        //public Action<object> onWin;
        bool isTimeUp;
        int cardNo;
        int newbalance;
        [HideInInspector]public int WinNo;
        [SerializeField] Animator anime;

        private void Awake()
        {
            Instance = this;
        }
        private void Start()
        {
            Titli_Timer.Instance.onTimeUp += () => isTimeUp = true;
            Titli_Timer.Instance.onCountDownStart += () => isTimeUp = false;

            //onWin += OnWin;
            //leftDice.SetActive(false);
            //rightDice.SetActive(false);       
        }

        public void SetWinNumbers(object o)
        {
            InitialData winData = Utility.Utility.GetObjectOfType<InitialData>(o);

            while(winData.previousWins.Count > 6)
            {
                winData.previousWins.RemoveAt(0);
                //PreviousWinValue.Add(cardNo);
            }
            PreviousWinValue.Clear();
            for (int i = 0; i < winData.previousWins.Count; i++)
            {
                PreviousWinValue.Add(winData.previousWins[i]);
            }
            //winData.previousWins.Reverse();
            PreviousWinValue.Reverse();
            for (int i = 0; i < winData.previousWins.Count; i++)
            {
                //PreviousWinValue.Add(winData.previousWins[i]);
                previousWins[i].enabled = true;
                previousWins[i].transform.GetChild(0).gameObject.SetActive(true);
                previousWins[i].transform.GetChild(1).gameObject.SetActive(true);
                previousWins[i].sprite = Imgs[PreviousWinValue[i]];
                // int num = winData.previousWins[i];
                // if (num == 0)
                // {
                //     previousWins[i].sprite = Imgs[0];//dragon
                // }
                // else if (num == 1)
                // {
                //     previousWins[i].sprite = Imgs[1];//tie
                // }
                // else
                // {
                //     previousWins[i].sprite = Imgs[2];//tiger
                // }
                //previousWins[i].gameObject.transform.GetChild(0).GetComponent<Text>().text = totalDiceNo.ToString();

            }
            PreviousWinValue.Reverse();
            
        }
        public void OnWin(object o)
        {
            Titli_UiHandler.Instance.HideMessage();
            //Titli_CardController.Instance.StopCardsBlink();
            //StartCoroutine(Titli_CardController.Instance.StopCardsBlink());



            DiceWinNos winData = Utility.Utility.GetObjectOfType<DiceWinNos>(o);
            if (Titli_UiHandler.Instance.zoomed)
            {
                anime.Play("titliwheelzoomin");
            }
            // cardNo = UnityEngine.Random.Range(0, 12);
            cardNo = winData.winNo;
            WinNo = winData.winNo;
            Titliwheel.instane.Spin(winData.winNo);
            Titliball.instant.rotatrnow(winData.winNo);
            //Titli_UiHandler.Instance.balance = winData.Balance;
            
            
            Titli_UiHandler.Instance.playerwon = false;
            while(winData.previousWin_single.Count > 6)
            {
                winData.previousWin_single.RemoveAt(0);
            }
            for (int i = 0; i < winData.previousWin_single.Count; i++)
            {
                PreviousWinValue[i] = winData.previousWin_single[i];
            }
            
            
            // while(winData.previousWin_single.Count > 5)
            // {
            //     //PreviousWinValue.RemoveAt(PreviousWinValue.Count -1);
            //     winData.previousWin_single.RemoveAt(0);
            //     //PreviousWinValue.RemoveAt(0);
            //     //PreviousWinValue.Add(cardNo);
            // }
            // PreviousWinValue.Clear();
            // for (int i = 0; i < winData.previousWin_single.Count; i++)
            // {
            //     PreviousWinValue.Add(winData.previousWin_single[i]);
            // }
            //PreviousWinValue.Add(WinNo);
            //else
            //{
               // PreviousWinValue.Add(cardNo);
                // PreviousWinValue.Reverse();
            //}
            //PreviousWinValue.Reverse();

            // while(winData.previousWins.Count > 5)
            // {
            //     winData.previousWins.RemoveAt(0);
            //     //PreviousWinValue.Add(cardNo);
            // }
            // Debug.Log("winarray"+ winData.previousWins.Count);
            // for (int i = 0; i < winData.previousWins.Count; i++)
            // {
            //     PreviousWinValue.Add(winData.previousWins[i]);
            // }
            //PreviousWinValue.Add(cardNo);
            //else
            //{
               // PreviousWinValue.Add(cardNo);
                // PreviousWinValue.Reverse();
            //}
            //PreviousWinValue.Reverse();

        }
        public void SpinComplete()
        {
            if (Titli_UiHandler.Instance.zoomed)
            {
                anime.Play("titliwheelzoomout");
            }
            if(Titli_UiHandler.Instance.betsholder[WinNo] > 0)
            {
                StartCoroutine(Titli_UiHandler.Instance.OnPlayerWin());
            }
           
            if(WinNo == 0)
            {
                StartCoroutine( ShowWinningRing(WinningRing[WinNo] , Spots.Umbrella) );
            }
            else if(WinNo == 1)
            {
                StartCoroutine( ShowWinningRing(WinningRing[WinNo] , Spots.Goat) );
            }
            else if(WinNo == 2)
            {
                StartCoroutine( ShowWinningRing(WinningRing[WinNo] , Spots.Pigeon) );
            }
            else if(WinNo == 3)
            {
                StartCoroutine( ShowWinningRing(WinningRing[WinNo] , Spots.Ball) );
            }
            else if(WinNo == 4)
            {
                StartCoroutine( ShowWinningRing(WinningRing[WinNo] , Spots.Diya) );
            }
            else if(WinNo == 5)
            {
                StartCoroutine( ShowWinningRing(WinningRing[WinNo] , Spots.Rabbit) );
            }
            else if(WinNo == 6)
            {
                StartCoroutine( ShowWinningRing(WinningRing[WinNo] , Spots.Dog) );
            }
            else if(WinNo == 7)
            {
                StartCoroutine( ShowWinningRing(WinningRing[WinNo] , Spots.Rose) );
            }
            else if(WinNo == 8)
            {
                StartCoroutine( ShowWinningRing(WinningRing[WinNo] , Spots.Flower) );
            }
            else if(WinNo == 9)
            {
                StartCoroutine( ShowWinningRing(WinningRing[WinNo] , Spots.Kite) );
            }
            else if(WinNo == 10)
            {
                StartCoroutine( ShowWinningRing(WinningRing[WinNo] , Spots.Butterfly) );
            }
            else if(WinNo == 11)
            {
                StartCoroutine( ShowWinningRing(WinningRing[WinNo] , Spots.Deer) );
            }
            else if (WinNo == 12)
            {
                StartCoroutine(ShowWinningRing(WinningRing[WinNo], Spots.FunGame));
            }
            //reset the bets here
            Debug.Log("OnWin called");
            
        }
        // public void storage(object o)
        // {
        //     historyrecord history = Utility.Utility.GetObjectOfType<historyrecord>(o);
            
        //     while(history.previouswin.Count > 6)
        //     {
        //         //PreviousWinValue.RemoveAt(PreviousWinValue.Count -1);
        //         history.previouswin.RemoveAt(0);
        //         //PreviousWinValue.RemoveAt(0);
        //         //PreviousWinValue.Add(cardNo);
        //     }
        //     PreviousWinValue.Clear();
        //     for (int i = 0; i < history.previouswin.Count; i++)
        //     {
        //         PreviousWinValue.Add(history.previouswin[i]);
        //     }
            
        // }

        void mySpinComplete(){
            // StartCoroutine( ShowWinningRing(WinningRing[cardNo] ) );
        }

        /* Need to call below function of OnWin once api is integrated */
        // public void OnWin(object o)
        // {
        //     if (WOF_Timer.Instance.is_a_FirstRound) return;
        //     DiceWinNos winData = Utility.Utility.GetObjectOfType<DiceWinNos>(o);
        //     int[] turnArray = { 0, 1, 2, 3, 0, 1, 2, 3, 0, 1, 2, 3 };
        //     System.Random rands = new System.Random();
        //     List<int> rand = turnArray.OrderBy(c => rands.Next()).Select(c => c).ToList();
        //     int No1 = winData.winNo - 1;
        //     int No2 = winData.winNo[1] - 1;
        //     StartCoroutine(cardOpen(No1, No2, rand[0], rand[1]));

        //     for (int i = 0; i < winData.previousWins.Count; i++)
        //     {
        //         int num = winData.previousWins[i];
        //         if (num == 0)
        //         {
        //             previousWins[i].sprite = Imgs[0];//dragon
        //         }
        //         else if (num == 1)
        //         {
        //             previousWins[i].sprite = Imgs[1];//tie
        //         }
        //         else
        //         {
        //             previousWins[i].sprite = Imgs[2];//tiger
        //         }                
        //     }
        //     if (winData.winningSpot == 0)//aniamtion add khusi
        //     {
        //         StartCoroutine(ShowWinningRing(leftRing, Spot.left, o));//dragon
        //     }
        //     else if (winData.winningSpot == 1)
        //     {
        //         StartCoroutine(ShowWinningRing(middleRing, Spot.middle, o));
        //     }
        //     else
        //     {
        //         StartCoroutine(ShowWinningRing(rightRing, Spot.right, o));
        //     }
        // }

        IEnumerator ShowWinningRing( GameObject ring , Spots winnerSpot )
        {
            yield return StartCoroutine(Titli_CardController.Instance.StopCardsBlink(WinNo));
            Debug.Log("ShowWinningRing");
            yield return new WaitForSeconds(0.25f);
            if(Titli_UiHandler.Instance.playerwon)
            {
                Titli_UiHandler.Instance.ShowMessage("You Won");
            }
            var tempColor_1 = ring.transform.parent.gameObject.GetComponent<Image>().color;
            Titli_UiHandler.Instance.ResetUi();
            
            foreach(var item in Titli_CardController.Instance._cardsImage)
            {
                item.GetComponent<Button>().interactable = true;
            }
            Titli_CardController.Instance.TakeChipsBack(winnerSpot); 
            ring.SetActive(true);
            tempColor_1.a = 0.5f;
            ring.transform.parent.gameObject.GetComponent<Image>().color = tempColor_1;
            yield return new WaitForSeconds(0.8f);
            ring.SetActive(false);
            tempColor_1.a = 1.0f;
            ring.transform.parent.gameObject.GetComponent<Image>().color = tempColor_1;
            yield return new WaitForSeconds(0.8f);
            ring.SetActive(true);
            tempColor_1.a = 0.5f;
            ring.transform.parent.gameObject.GetComponent<Image>().color = tempColor_1;
            yield return new WaitForSeconds(0.8f);
            ring.SetActive(false);
            tempColor_1.a = 1.0f;
            ring.transform.parent.gameObject.GetComponent<Image>().color = tempColor_1;
            yield return new WaitForSeconds(0.8f);
            ring.SetActive(true);
            tempColor_1.a = 0.5f;
            ring.transform.parent.gameObject.GetComponent<Image>().color = tempColor_1;
            yield return new WaitForSeconds(2f);
            ring.SetActive(false);
            tempColor_1.a = 1.0f;
            ring.transform.parent.gameObject.GetComponent<Image>().color = tempColor_1;
            Titli_CardController.Instance._winNo = false;

            // while(PreviousWinValue.Count > 5)
            // {
                //PreviousWinValue.RemoveAt(PreviousWinValue.Count -1);
                // PreviousWinValue.RemoveAt(0);
                //PreviousWinValue.Add(cardNo);
            // }
            // PreviousWinValue.Add(WinNo);
            // //else
            // //{
            //    // PreviousWinValue.Add(cardNo);
            //     // PreviousWinValue.Reverse();
            // //}
            PreviousWinValue.Reverse();
            
            //debugImageArray();
            
            for(int i = 0;i < PreviousWinValue.Count; i++)
            {
                previousWins[i].enabled = true;
                previousWins[i].transform.GetChild(0).gameObject.SetActive(true);
                previousWins[i].transform.GetChild(1).gameObject.SetActive(true);
                previousWins[i].sprite = Imgs[PreviousWinValue[i]];
            }
            
            PreviousWinValue.Reverse();
        }


        /*void debugImageArray()
        {
            string s = "";
            for(int i = 0;i < PreviousWinValue.Count; i++)
            {
                s = s + " " + PreviousWinValue.ElementAt(i);
            }
            Debug.Log("value of image list:--"+s);
        }*/

        /* Need to call below function of OnWin once api is integrated */

        // IEnumerator ShowWinningRing(GameObject ring, Spot winnerSpot, object o)
        // {
        //     yield return new WaitForSeconds(1f);
        //     WOF_BotsManager.Instance.UpdateBotData(o);
        //     WOF_OnlinePlayerBets.Intsance.OnWin(o);
        //     WOF_ChipController.Instance.TakeChipsBack(winnerSpot);
        //     yield return new WaitForSeconds(2f);
        //     ring.SetActive(false);
        //     if (winnerSpot == Spot.left)
        //     {
        //         StartCoroutine(DragonAnimation());
        //     }
        //     else if (winnerSpot == Spot.middle)
        //     {
        //         StartCoroutine(TieAnimation());
        //     }
        //     else if (winnerSpot == Spot.right)
        //     {
        //         StartCoroutine(TigerAnimation());
        //     }

        //     //BetManager.Instance.WinnerBets(winnerSpot);

        //     yield return new WaitForSeconds(2f);
        //     UtilitySound.Instance.Cardflipsound();
        //     Card1.sprite = TigerBackCard;           
        //     Card2.sprite = ElephantBackCard;

        // }

    }

    public class DiceWinNos
    {
        public int winNo;
        //public List<int> winNo;
        public List<int> wins;
        public List<int> previousWin_single;
        public int winningSpot;
        public int Balance;
        //public int winamount;
    }
    public class historyrecord
    {
        public List<int> previouswin;
    }
}
