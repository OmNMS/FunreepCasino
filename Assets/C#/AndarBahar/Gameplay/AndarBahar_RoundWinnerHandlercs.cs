using System.Collections.Generic;
using AndarBahar.Utility;
using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using AndarBahar.UI;
using Newtonsoft.Json;
using UnityEngine.Networking;

namespace AndarBahar.Gameplay
{
    class AndarBahar_RoundWinnerHandlercs : MonoBehaviour
    {
        [SerializeField] AndarBahar_UiHandler AUI;
        //AndarBahar_Timer timerr;
        public static AndarBahar_RoundWinnerHandlercs Instance;    
        // public Transform AndarImg;
        public Sprite[] Cards_deck_WinList;
        public Image Winner_Image_game_object;
        // public Image Andhar_winSprite, Bahar_winSprite;
        // public Transform BaharImg;
        // public Transform Andar;
        // public Transform Bahar;
        int winAmt = 0;
        int winNo, Andhar_Bahar_winNo; 
        // public Button test;
        public GameObject[] WinnerRing;
        public Transform Target_andar, Target_bahar, Base_position;
        public GameObject Card_andar, Card_bahar,coinsaudio;
        public Image[] PreviousWins_History;
        [SerializeField]List<int> previouswin;
        public int winPoint;
        public float winround;
        int something;
        string winamounturl ="http://139.59.92.165:5000/user/Winamount";
        string emptyurl ="http://139.59.92.165:5000/user/DeletePreviousWinamount";
        //bool spinnow;
        public bool spinnow
        {
            get{
                if(int.Parse(AUI.WinAmountTxt.text)>0)
                {
                    return false;
                }
                else{
                    return true;
                }
            }
        }
        public List<GameObject> CardList_ToDelete;
        public System.Random r = new System.Random();
        // int[][] Card = new {0,13,26,39}, Card_2 = {1,12,27,40}, Card_3 = {2,13,28,41}, Card_4 = {3,14,29,42}, Card_5 = {4,15,30,43}, Card_6 = {5,16,31,44}, 
        //Card_7 = {6,17,32,45}, Card_8 = {7,18,33,46}, Card_9 = {8,19,34,47}, Card_10 = {9,20,35,48}, Card_J = {10,21,36,49}, Card_Q = {11,22,37,50}, 
        //Card_K = {12,23,38,51};
        int[][] card_nested_list = new int[13][] {
            new int[4] {0,13,26,39},
            new int[4] {1,14,27,40 },
            new int[4] {2,15,28,41 },
            new int[4] {3,16,29,42 },
            new int[4] {4,17,30,43},
            new int[4] {5,18,31,44},
            new int[4] {6,19,32,45 },
            new int[4] {7,20,33,46},
            new int[4] {8,21,34,47},
            new int[4] {9,22,35,48 },
            new int[4] {10,23,36,49},
            new int[4] {11,24,37,50 },
            new int[4] {12,25,38,51 }
         };
        public void Start()
        {
        
            // Andhar_winSprite.gameObject.SetActive(true);
            // Bahar_winSprite.gameObject.SetActive(true);
            

        }
        void Awake()
        {
            Instance = this;
        }
        public bool taken;
        public void TakeBet()
        {
            if(spinnow)
            {
                return;
            }
            //if (winPoint == 0) return;
            AUI.canPlaceBet = true;
            taken = true;
            Debug.Log("Win Point:"+winPoint );
            Take_Bet data = new Take_Bet()
            {
                playerId = "GK"+PlayerPrefs.GetString("email"),
                winpoint = winPoint
            };

            //AUI.balance += winPoint;
            //winPoint = 0;
            //AUI.WinText.text = "0";
            //AUI.balanceTxt.text = AUI.balance.ToString();
            StartCoroutine(coinanimation());
            StartCoroutine(emptyonwinresponse());
            //PlayerPrefs.SetFloat("points", AUI.balance);
            //winPoint = 0;
            //AndarBahar.ServerStuff.AndarBahar_ServerRequest.instance.socket.Emit(AndarBahar.Utility.Events.OnWinAmount, new JSONObject (JsonConvert.SerializeObject(data) ));
            AUI.ResetUI();
            AUI.TakeBet_Btn.interactable = false;
        }

        float partialwin;
        
        
        public void testing()
        {
            int i =1;
            //winPoint = 5;
            
            // if(winningAmount > 50000)
            // {
            //     partialwin = 0.1f/winningAmount;
            // }
            
            
            Debug.Log(partialwin + "dfgd" + i);
            
            //partialwin = winningAmount/500f;
            StartCoroutine(coinanimation());
        }
        IEnumerator coinanimation()
        {
            float localbalance = AUI.balance;
            float localwinpoint = winPoint;
            float elapsedTime = 0f;
            float deductionPercentage = 0.1f;
            while (elapsedTime <4f)
            {
                float deductionValue = (winPoint * deductionPercentage);
                    
                    // Deduct the value from the current variable
                winPoint -= Mathf.RoundToInt(deductionValue);
                localbalance += Mathf.RoundToInt(deductionValue);
                AUI.balanceTxt.text = localbalance.ToString(); //+ Mathf.RoundToInt(deductionValue)).ToString(); //.ToString();
                AUI.WinAmountTxt.text = winPoint.ToString();
                
                if(winPoint == 5)
                {
                    AUI.balance = winround;//localwinpoint;//winround;//+= winPoint;
                    winPoint = 0;
                    AUI.balanceTxt.text = AUI.balance.ToString();
                    AUI.WinAmountTxt.text = winPoint.ToString();
                    break;
                }
                // Make sure we don't go below zero
                //winPoint = Mathf.Max(winPoint, 0);
    
                yield return null;
                elapsedTime += Time.deltaTime;
                
            }
            Debug.Log("/////////////////////xdcfvgbhnjmk,lkmjnhbgvfddxcfvgbnm///////////////////////"+temp_balance);
            Debug.Log("BALANCE: " +AUI.balance +" finalamount:" +winround);
            AUI.isBetConfirmed = false;
            // AUI.balance +=localwinpoint;//winround;//+= winPoint;
            // winPoint = 0;
            // AUI.balanceTxt.text = AUI.balance.ToString();
            // AUI.WinAmountTxt.text = winPoint.ToString();
            

            // Update the elapsed time
            elapsedTime += Time.deltaTime;
            //     int currentValue = winPoint;
            //     float elapsedTime = 0f;
        
       
        }
        public void sec_testing()
        {
            StartCoroutine(AddToBalance(5000.0f,5.0f));
        }
        public IEnumerator AddToBalance(float amount, float duration)
        {
            float startBalance = AUI.balance;
            float currentTime = 0.0f;
            while (currentTime < duration)
            {
                float t = currentTime / duration;
                AUI.balance = Mathf.Lerp(startBalance, startBalance + amount, t);
                currentTime += Time.deltaTime;
                yield return null;
            }
            AUI.balance = startBalance + amount;
            AUI.balanceTxt.text = AUI.balance.ToString();
        }

        public class Take_Bet
        {
            public string playerId;
            public int winpoint;
        }

        public void Start_Previouswin(object o)//(List<int> previouslist )
        {
            
            // for (int i = 0; i < PreviousWins_History.Length; i++)
            // {    
            //     Debug.Log(previouslist[i]); 
            // }
            //previouswin = previouslist;
            JsonSerializerSettings settingsJson = new JsonSerializerSettings()
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore
            };
            var settings = new JsonSerializerSettings
            {
                Converters = new[] { new NullCharacterConverter() }
            };

            pahila data = new pahila();
            try
            {
                data = JsonConvert.DeserializeObject<pahila>(o.ToString(),settings);
            }
            catch (System.Exception)
            {
                
            }
            int samay = data.gametimer;
            Debug.Log("jjjjjjjjjjjjjjjjjjjjjjjjjjjjjj"+data.gametimer);
            
            // AndarBahar_Timer.instance.firsttimer(samay);
            
            //timerr.firsttimer(samay);
            //StartCoroutine(timerr.firsttimer(samay));
            Debug.Log("ooooooooooooooooooooooo" + data.previous_Wins.Count);
            //Debug.Log("the length of previouswin" + previouswin.Count+"previous values from currentimer");
            while (data.previous_Wins.Count > 5)
            {
                data.previous_Wins.RemoveAt(0);
            }
            
            // for (int i = 0; i < PreviousWins_History.Length; i++)
            // {    
            //     Debug.Log(previouslist[i]); 
            // }

            for (int i = 0; i < PreviousWins_History.Length; i++)
            {
                PreviousWins_History[i].sprite = Cards_deck_WinList[data.previous_Wins[i]];
                PreviousWins_History[i].gameObject.SetActive(true);
                previouswin.Add(data.previous_Wins[i]);
            }
            //StartCoroutine(timer.Countdown(data.gametimer));//Countdown(cr.gametimer));
        }


        public int[] Getwin_Array()
        {
            int[] checklist = new int[4];
            for(int i=0; i<card_nested_list.Length; i++)
            {
                for(int j=0; j < (card_nested_list[i]).Length; j++)
                {
                    if (winNo == card_nested_list[i][j])
                    {
                        // return card_nested_list[i][];
                        checklist = new int [4] {card_nested_list[i][0], card_nested_list[i][1],card_nested_list[i][2],card_nested_list[i][3]} ;
                    }
                    // Console.WriteLine(card_nested_list[i][j]);
                }
            }
            return checklist;
            
        }

        public void Card_SpinComplete()
        {
            //int i = 0;
            StartCoroutine(Move_Card_animation(andarcards,baharcards));
            //StartCoroutine(AndharWin(i));
        }
        public float temp_balance;
        public int[] andarcards;
        public int[] baharcards;
        int winner;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="spot">willl only consider andar Or Bahar</param>
        /// <param name="subSpot"></param>
        public void OnWin(object o)                   //(Spots spot/*, int subSpot*/)
        {
            Debug.Log("ssssssssssssssssssssssttttttttttttttttttttttttoooooooooooooopppppppppppppppp");
            //AndarBahar_Timer.instance.andarround++;
            if(!spinnow)
            {
                //Debug.Log("ssssssssssssssssssssssttttttttttttttttttttttttoooooooooooooopppppppppppppppp");
                //AndarBahar_CardHandler.Instance.Stop_CardAnimation();
                return;
            }
            Winning winData = AndarBahar.Utility.Fuction.GetObjectOfType<Winning>(o);
            
            //winPoint = winData.winPoint;
            if(winData.win_andhar_bahar == 0)
            {
                winNo = winData.Andar_Card_List[winData.Andar_Card_List.Length-1];
            }
            else{
                winNo = winData.Bahar_Card_List[winData.Bahar_Card_List.Length-1];
            }

            //winNo = winData.winNo;
            temp_balance = winData.Balance;
            winner = winData.win_andhar_bahar;
            StartCoroutine(onwinresponse());
            //winround = AUI.balance + winPoint;
            List<int> andarlist = winData.Andar_Card_List.ToList();
            while(andarlist.Count>5)
            {
                andarlist.RemoveAt(0);
            }
            List<int> baharlist = winData.Bahar_Card_List.ToList();
            while(baharlist.Count>5)
            {
                baharlist.RemoveAt(0);
            }
            
            andarcards = andarlist.ToArray();//winData.Andar_Card_List;
            baharcards = baharlist.ToArray();//winData.Bahar_Card_List;
            andarlist.Clear();
            baharlist.Clear();
            something = (int)winround;
            Debug.Log("Balance :"+AUI.balance + " Winno: "+winNo+" Total:"+winround);
            AndarBahar_CardHandler.Instance.Stop_CardAnimation();
            Winner_Image_game_object.gameObject.SetActive(true);
            Winner_Image_game_object.sprite = Cards_deck_WinList[winNo];

            while (winData.previous_Wins.Count > 5 )
            {
                winData.previous_Wins.RemoveAt(0);    
            }
            // previouswin.Add(winNo);

            // StartCoroutine(AndarBahar_CardHandler.Instance.Start_CardAnimation());
            //AndarBahar_CardHandler.Instance.Stop_CardAnimation();

            

            for (int i = 0; i < PreviousWins_History.Length; i++)
            {
                PreviousWins_History[i].sprite = Cards_deck_WinList[winData.previous_Wins[i]];
                PreviousWins_History[i].gameObject.SetActive(true);
            }

            //now start andhar and bahar card showing !!!1
            Andhar_Bahar_winNo = winData.win_andhar_bahar;
            

            AndarBahar_CardHandler.Instance.Card_Spin_Complete += Card_SpinComplete;
            //call the api to fetch data and add the winround value here

            
            // Start_Andhar_bahar_Win(Andhar_Bahar_winNo, winNo);   
            ////////

            ////////////
            // Debug.Log("reachhhhhhhedddd hereeeeeeeee");
            // if (winNo == 0 || winNo == 13 || winNo == 26 || winNo == 39 )//A
            // {
            //    StartCoroutine(ShowRings( WinnerRing[0] )); //ace of spade ,
            // }
            // else if (winNo == 1 || winNo == 14 || winNo == 27 || winNo == 40 )//2  
            // {
            //     StartCoroutine(ShowRings( WinnerRing[1] )); 
            // }
            // else if (winNo == 2 || winNo == 15 || winNo == 28 || winNo == 41 )//3
            // {
            //     StartCoroutine(ShowRings( WinnerRing[2] )); 
            // }
            // else if (winNo == 3 || winNo == 16 || winNo == 29 || winNo == 42 )//4
            // {
            //     StartCoroutine(ShowRings( WinnerRing[3] )); 
            // }
            // else if (winNo == 4 || winNo == 17 || winNo == 30 || winNo == 43 )//5
            // {
            //     StartCoroutine(ShowRings( WinnerRing[4] )); 
            // }
            // else if (winNo == 5 || winNo == 18 || winNo == 31 || winNo == 44 )//6
            // {
            //     StartCoroutine(ShowRings( WinnerRing[5] )); 
            // }
            // else if (winNo == 6 || winNo == 19 || winNo == 32 || winNo == 45 )//7
            // {
            //     StartCoroutine(ShowRings( WinnerRing[6] )); 
            // }
            // else if (winNo == 7 || winNo == 20 || winNo == 33 || winNo == 46 )//8
            // {
            //     StartCoroutine(ShowRings( WinnerRing[7] )); 
            // }
            // else if (winNo == 8 || winNo == 21 || winNo == 34 || winNo == 47 )//9
            // {
            //     StartCoroutine(ShowRings( WinnerRing[8] )); 
            // }
            // else if (winNo == 9 || winNo == 22 || winNo == 35 || winNo == 48 )//10
            // {
            //     StartCoroutine(ShowRings( WinnerRing[9] )); 
            // }
            // else if (winNo == 10 || winNo == 23 || winNo == 36 || winNo == 49 )//J
            // {
            //     StartCoroutine(ShowRings( WinnerRing[10] )); 
            // }
            // else if (winNo == 11 || winNo == 24 || winNo == 37 || winNo == 50 )//Q
            // {
            //     StartCoroutine(ShowRings( WinnerRing[11] )); 
            // }
            // else if (winNo == 12 || winNo == 25 || winNo == 38 || winNo == 51 )//K
            // {
            //     StartCoroutine(ShowRings( WinnerRing[12] )); 
            // }


            //need to work herer no!!!
            // StartCoroutine(ShowRings(winnerImg, winnerSpot /*subWinnerSpot)*/));
        }
        public bool started;
        public IEnumerator onwinresponse()
        {
            Debug.Log("The Api for winresponse was called");
            WWWForm form = new WWWForm();
            string playername = "GK"+PlayerPrefs.GetString("email");
            int gameid  = 3;
            form.AddField("playerId", playername);
            form.AddField("game_id",gameid);
            using (UnityWebRequest www = UnityWebRequest.Post(winamounturl, form))
            {
                yield return www.SendWebRequest();

                if (www.result != UnityWebRequest.Result.Success)
                {
                    Debug.Log(www.error);
                }
                else
                {
                    winneramount response = JsonConvert.DeserializeObject<winneramount>(www.downloadHandler.text);
                    if ( response.status == 200 )
                    {
                        winPoint = (int)response.data.Winamount;
                        winround = AUI.balance+winPoint;
                        if(started)
                        {
                            //AUI.WinAmountTxt.text = winPoint.ToString();
                            AUI.WinAmountTxt.text = response.data.Winamount.ToString();
                            if(response.data.Winamount >0)
                            {
                                //AUI.restore();
                                AUI.restorewithn();
                            }
                            started = false;
                        }
                    }
                }
            }
        }

        public IEnumerator emptyonwinresponse()
        {
            WWWForm form = new WWWForm();
            string playername = "GK"+PlayerPrefs.GetString("email");
            int gameid  = 3;
            form.AddField("playerId", playername);
            form.AddField("game_id",gameid);
            using (UnityWebRequest www = UnityWebRequest.Post(emptyurl, form))
            {
                yield return www.SendWebRequest();

                if (www.result != UnityWebRequest.Result.Success)
                {
                    Debug.Log(www.error);
                }
                else
                {
                    Debug.Log("the data has been deletd from the winpoint table");
                }
            }
        }

        public void blinking()
        {
            if (winNo == 0 || winNo == 13 || winNo == 26 || winNo == 39 )//A
            {
               StartCoroutine(ShowRings( WinnerRing[0] )); //ace of spade ,
            }
            else if (winNo == 1 || winNo == 14 || winNo == 27 || winNo == 40 )//2  
            {
                StartCoroutine(ShowRings( WinnerRing[1] )); 
            }
            else if (winNo == 2 || winNo == 15 || winNo == 28 || winNo == 41 )//3
            {
                StartCoroutine(ShowRings( WinnerRing[2] )); 
            }
            else if (winNo == 3 || winNo == 16 || winNo == 29 || winNo == 42 )//4
            {
                StartCoroutine(ShowRings( WinnerRing[3] )); 
            }
            else if (winNo == 4 || winNo == 17 || winNo == 30 || winNo == 43 )//5
            {
                StartCoroutine(ShowRings( WinnerRing[4] )); 
            }
            else if (winNo == 5 || winNo == 18 || winNo == 31 || winNo == 44 )//6
            {
                StartCoroutine(ShowRings( WinnerRing[5] )); 
            }
            else if (winNo == 6 || winNo == 19 || winNo == 32 || winNo == 45 )//7
            {
                StartCoroutine(ShowRings( WinnerRing[6] )); 
            }
            else if (winNo == 7 || winNo == 20 || winNo == 33 || winNo == 46 )//8
            {
                StartCoroutine(ShowRings( WinnerRing[7] )); 
            }
            else if (winNo == 8 || winNo == 21 || winNo == 34 || winNo == 47 )//9
            {
                StartCoroutine(ShowRings( WinnerRing[8] )); 
            }
            else if (winNo == 9 || winNo == 22 || winNo == 35 || winNo == 48 )//10
            {
                StartCoroutine(ShowRings( WinnerRing[9] )); 
            }
            else if (winNo == 10 || winNo == 23 || winNo == 36 || winNo == 49 )//J
            {
                StartCoroutine(ShowRings( WinnerRing[10] )); 
            }
            else if (winNo == 11 || winNo == 24 || winNo == 37 || winNo == 50 )//Q
            {
                StartCoroutine(ShowRings( WinnerRing[11] )); 
            }
            else if (winNo == 12 || winNo == 25 || winNo == 38 || winNo == 51 )//K
            {
                StartCoroutine(ShowRings( WinnerRing[12] )); 
            }
        }

        public iTween.EaseType easeType;

        public void Card_flip_courout(int i)
        {
            StartCoroutine( AndharWin(i) ); 
        }
        int andarc = 0;
        int[] nothingbahar = {9,21,49,33,17,18};
        public void testting()
        {
            
            
            //StartCoroutine(Move_Card_animation());
            // StartCoroutine(throwandarcards(nothing));
        }
    //     public IEnumerator throwandarcards(int[] cards_andar)
    //     {
            
    //         StartCoroutine(AndarBahar_CardHandler.Instance.Start_CharacterAnimation());
    //         for (int i = andarc; i < cards_andar.Length; i++)
    //         {
    //             GameObject Card = Instantiate(Card_andar, Target_andar );
    //             CardList_ToDelete.Add(Card);
                
    //             Card.gameObject.SetActive(true);
    //             Card.transform.position = Base_position.transform.position;
    //             Card.GetComponent<Image>().sprite = Cards_deck_WinList[cards_andar[i]];

    //             iTween.MoveTo(Card, Target_andar.transform.position, 0.3f);          
    //             // iTween.RotateTo(Card, iTween.Hash("y", 0.5f, "time", 0.2f, "easetype", easeType,"onupdatetarget",this.gameObject, "onupdate", "RotationStart", "onupdateparams", Card));
    //             iTween.RotateTo(Card, new Vector3(0,0,0), 0.15f );
                
    //             iTween.ScaleTo(Card, new Vector3(1.1f,1.1f,1.1f), 0.1f );
    //             yield return new WaitForSeconds(0.1f);
    //             iTween.ScaleTo(Card, new Vector3(1f,1f,1f), 0.1f );
    //             andarc++;
    //             StartCoroutine(throwbaharcards(nothingbahar));
    //         }
    //     }
    //     public IEnumerator throwbaharcards(int[] cards_bahar)
    //     {
    //         for (int i = 0; i < cards_bahar.Length; i++)
    //         {
    //             GameObject Card = Instantiate(Card_bahar, Target_bahar );
    //             CardList_ToDelete.Add(Card);
                
    //             Card.gameObject.SetActive(true);
    //             Card.transform.position = Base_position.transform.position;
    //             Card.GetComponent<Image>().sprite = Cards_deck_WinList[cards_bahar[i]];
    
    //             iTween.MoveTo(Card, Target_bahar.transform.position, 0.3f);
    //             // iTween.RotateTo(Card, iTween.Hash("rotation",new Vector3(0,0,0), "time", 0.2f, "easetype", easeType));
    //             iTween.RotateTo(Card, new Vector3(0,0,0), 0.15f );
                
    //             iTween.ScaleTo(Card, new Vector3(1.1f,1.1f,1.1f), 0.1f );
    //             yield return new WaitForSeconds(0.1f);
    //             iTween.ScaleTo(Card, new Vector3(1f,1f,1f), 0.1f );
    //         }
    // }

    public IEnumerator Move_Card_animation(int[] andarholder,int[] baharholder) // thsi is for testing of card move and flip, attach to button before or while runtime...
    {
        //Andhar_Bahar_winNo = 1;
        //int[] nothing_andar = andarholder;
        //int[] nothing_bahar = baharholder;
        int[] temp = new int[5];
        switch (winner)
        {
            
            case 0 :
                temp = andarholder;
                for (int i = 0; i < andarholder.Length; i++)
                {
                    yield return StartCoroutine(FlipCard(andarholder[i] , Target_andar));
                    if (Andhar_Bahar_winNo == 0 && i == andarholder.Length -1)
                    {
                        break;
                    }
                    yield return StartCoroutine(FlipCard(baharholder[i] , Target_bahar));
                }
                break;
            case 1:
                temp = baharholder;
                for (int i = 0; i < baharholder.Length; i++)
                {
                    yield return StartCoroutine(FlipCard(baharholder[i] , Target_bahar));
                    if (Andhar_Bahar_winNo == 1 && i == baharholder.Length -1)
                    {
                        break;
                    }
                    yield return StartCoroutine(FlipCard(andarholder[i] , Target_andar));
                }
                break;
        }
        // for (int i = 0; i < temp.Length; i++)
        // {
        //     yield return StartCoroutine(FlipCard(andarholder[i] , Target_andar));
        //     if (Andhar_Bahar_winNo == 0 && i == andarholder.Length -1)
        //     {
        //         break;
        //     }
        //     yield return StartCoroutine(FlipCard(baharholder[i] , Target_bahar));
        // }
        blinking();
        yield return new WaitUntil( ( ) =>AUI.canPlaceBet );
        Destroy_Cards();
    }

    IEnumerator FlipCard(int cardno, Transform parent)
    {
        yield return StartCoroutine(AndarBahar_CardHandler.Instance.Start_CharacterAnimation());

        GameObject Card = Instantiate(Card_andar, parent );
        CardList_ToDelete.Add(Card);
        
        Card.gameObject.SetActive(true);
        Card.transform.position = Base_position.transform.position;
        Card.GetComponent<Image>().sprite = Cards_deck_WinList[cardno];

        iTween.MoveTo(Card, parent.position, 0.3f);          
        // iTween.RotateTo(Card, iTween.Hash("y", 0.5f, "time", 0.2f, "easetype", easeType,"onupdatetarget",this.gameObject, "onupdate", "RotationStart", "onupdateparams", Card));
        iTween.RotateTo(Card, new Vector3(0,0,0), 0.15f );
        
        iTween.ScaleTo(Card, new Vector3(1.1f,1.1f,1.1f), 0.1f );
        yield return new WaitForSeconds(0.1f);
        iTween.ScaleTo(Card, new Vector3(1f,1f,1f), 0.1f );
    }

        // IEnumerator Move_And_Rotate_Card(GameObject card, Transform Target)
        // {
        //     GameObject Card = Instantiate(card, AndarBahar_CardHandler.Instance.andar );
        //     CardList_ToDelete.Add(Card);
        //     yield return StartCoroutine(AndarBahar_CardHandler.Instance.Start_CharacterAnimation());
        //     Card.gameObject.SetActive(true);
        //     Card.transform.position = Base_position.transform.position;
        //     iTween.MoveTo(Card, Target.transform.position, 2f);
        //     iTween.RotateTo(Card, new Vector3(0,180,0), 2f );
        //     iTween.ScaleTo(Card, new Vector3(1,1,1), 0.1f );
        //     yield return new WaitForSeconds(1f);
        //     yield return Card;
        //     Card.gameObject.SetActive(false);
        //     new WaitForSeconds(3f);
        //     Destroy(Card);
        // }
        
        // public IEnumerator Card_flip_animation(int i)
        // {
        //     yield return StartCoroutine( Start_CharacterAnimation() );
        //     if (i == 1)
        //     {
        //         GameObject obj = Instantiate(Card_flip_andar.gameObject, Target_andar );
        //         Vector3 Base = Base_position.gameObject.transform.position, Target = Target_andar.gameObject.transform.position;
                
        //         // Card_flip_andar.gameObject.transform.position = Base;
        //         obj.gameObject.SetActive(true);

        //         float distance = Vector3.Distance(obj.transform.position, Target);
        //         Debug.Log("distance:"+distance+  " Base Position" +Base+ "  Andar target position:" +Target );
        //         int speed = (int)distance/2;
                
        //         while (distance > .1f)
        //         {
        //             obj.transform.position = Vector3.MoveTowards(obj.transform.position, Target, 1 );
        //             obj.transform.Rotate (new Vector3 (0,90,0));
        //             distance = Vector3.Distance(obj.transform.position, Target );
        //             yield return new WaitForEndOfFrame();
        //         }
        //         // obj.gameObject.SetActive(false);
                
        //         // iTween.MoveTo(Card_flip_andar, iTween.Hash("position", andar, "time", 10f, "easetype", easeType));
        //         // Card_flip_andar.gameObject.transform.Rotate (new Vector3 (0,90,0));
        //         // Card_flip_andar.gameObject.SetActive(false);
        //     }
        //     else if (i ==2)
        //     {
        //         Card_flip_bahar.gameObject.transform.position = Base_position.gameObject.transform.position;
                
        //         Card_flip_bahar.gameObject.SetActive(true);

        //         float distance = Vector3.Distance(Card_flip_bahar.gameObject.transform.position, Target_bahar.gameObject.transform.position);
        //         Debug.Log("distance:"+distance+ " Base Position" +Base_position.gameObject.transform.position +"  Bahar target position:" +Target_bahar.gameObject.transform.position+ "   Bahar Start Pos:" +Card_flip_bahar.gameObject.transform.position  );
        //         int speed = (int)distance/2;

        //         while (distance > .1f)
        //         {
        //             Card_flip_bahar.transform.position = Vector3.MoveTowards(Card_flip_bahar.transform.position, Target_bahar.transform.position, speed );
        //             Card_flip_bahar.gameObject.transform.Rotate (new Vector3 (0,90,0));
        //             distance = Vector3.Distance(Card_flip_bahar.transform.position, Target_bahar.transform.position);
        //             yield return new WaitForEndOfFrame();
        //         }
        //         Card_flip_bahar.gameObject.SetActive(false);
                
        //         // iTween.MoveTo(Card_flip_bahar, iTween.Hash("position", bahar, "time", 10f, "easetype", easeType));
        //         // Card_flip_bahar.gameObject.transform.Rotate (new Vector3 (0,90,0));
        //         // Card_flip_bahar.gameObject.SetActive(false);
        //     }
        // }

        IEnumerator AndharWin(int i)
        {
            
            // yield return StartCoroutine(Move_And_Rotate_Card(Card_andar, Target_andar));
            int cardno = r.Next(Cards_deck_WinList.Length);
            if ( Array.Exists<int>(Getwin_Array(),ele => ele == cardno) && Andhar_Bahar_winNo == 0 )
            {
                StartCoroutine(Final_Andhar_win(cardno)); 
            }
            else
            {
                do
                {
                    cardno = r.Next(Cards_deck_WinList.Length);
                }while ( Array.Exists<int>(Getwin_Array(),ele => ele == cardno) );  //(cardno == Array.Exists(Getwin_Array(),element => element == cardno));

                yield return StartCoroutine(AndarBahar_CardHandler.Instance.Start_CharacterAnimation());

                GameObject Card = Instantiate(Card_andar, Target_andar );
                CardList_ToDelete.Add(Card);
                
                Card.gameObject.SetActive(true);
                Card.transform.position = Base_position.transform.position;
                Card.GetComponent<Image>().sprite = Cards_deck_WinList[cardno];

                iTween.MoveTo(Card, Target_andar.transform.position, 0.3f);          
                // iTween.RotateTo(Card, iTween.Hash("y", 0.5f, "time", 0.2f, "easetype", easeType,"onupdatetarget",this.gameObject, "onupdate", "RotationStart", "onupdateparams", Card));
                iTween.RotateTo(Card, new Vector3(0,0,0), 0.15f );
                
                iTween.ScaleTo(Card, new Vector3(1.1f,1.1f,1.1f), 0.1f );
                yield return new WaitForSeconds(0.1f);
                iTween.ScaleTo(Card, new Vector3(1f,1f,1f), 0.1f );
                // Card.gameObject.SetActive(false);

                // Andhar_winSprite.GetComponent<Image>().sprite = Cards_deck_WinList[cardno];
                if (i < 3 )
                {    
                    
                    StartCoroutine(BaharWin(i));
                }
                else
                {
                    if (Andhar_Bahar_winNo == 0)
                    {
                        StartCoroutine(BaharWin(i));
                        // AndarBahar_CardHandler.Instance.playanimation();
                        // Andhar_winSprite.GetComponent<Image>().sprite = Cards_deck_WinList[winNo];
                    }
                    else if (Andhar_Bahar_winNo == 1)
                    {
                        StartCoroutine(Final_Bahar_win(winNo));
                        // AndarBahar_CardHandler.Instance.playanimation();
                        // Bahar_winSprite.GetComponent<Image>().sprite = Cards_deck_WinList[winNo];
                    }
                }
            }
        }

        IEnumerator BaharWin(int i)
        {
            
            // yield return StartCoroutine(Move_And_Rotate_Card(Card_bahar, Target_bahar));
            
            int cardno = r.Next(Cards_deck_WinList.Length);
            if ( Array.Exists<int>(Getwin_Array(),ele => ele == cardno) && Andhar_Bahar_winNo == 1 )
            {
               StartCoroutine(Final_Bahar_win(cardno));
            }
            else
            {
                do
                {
                    cardno = r.Next(Cards_deck_WinList.Length);
                }while (Array.Exists<int>(Getwin_Array(),ele => ele == cardno));

                yield return StartCoroutine(AndarBahar_CardHandler.Instance.Start_CharacterAnimation());

                GameObject Card = Instantiate(Card_bahar, Target_bahar );
                CardList_ToDelete.Add(Card);
                
                Card.gameObject.SetActive(true);
                Card.transform.position = Base_position.transform.position;
                Card.GetComponent<Image>().sprite = Cards_deck_WinList[cardno];

                iTween.MoveTo(Card, Target_bahar.transform.position, 0.3f);
                // iTween.RotateTo(Card, iTween.Hash("rotation",new Vector3(0,0,0), "time", 0.2f, "easetype", easeType));
                iTween.RotateTo(Card, new Vector3(0,0,0), 0.15f );
                
                iTween.ScaleTo(Card, new Vector3(1.1f,1.1f,1.1f), 0.1f );
                yield return new WaitForSeconds(0.1f);
                iTween.ScaleTo(Card, new Vector3(1f,1f,1f), 0.1f );
                // Card.gameObject.SetActive(false);


                // Bahar_winSprite.GetComponent<Image>().sprite = Cards_deck_WinList[cardno];
                if (i <3 )
                { 
                    i++;   
                    StartCoroutine(AndharWin(i));
                }
                else
                {
                    if (Andhar_Bahar_winNo == 0)
                    {
                        StartCoroutine(Final_Andhar_win(winNo)); 
                    }
                    else if (Andhar_Bahar_winNo == 1)
                    {
                        StartCoroutine(AndharWin(i));
                    }
                }
            }
        }

        IEnumerator Final_Bahar_win(int cardno)
        {
            yield return StartCoroutine(AndarBahar_CardHandler.Instance.Start_CharacterAnimation());
            // yield return StartCoroutine(Move_And_Rotate_Card(Card_andar, Target_andar));

            GameObject Card = Instantiate(Card_bahar, Target_bahar );
            CardList_ToDelete.Add(Card);
            
            Card.gameObject.SetActive(true);
            Card.transform.position = Base_position.transform.position;
            Card.GetComponent<Image>().sprite = Cards_deck_WinList[winNo];

            iTween.MoveTo(Card, Target_bahar.transform.position, 0.3f);                
            // iTween.RotateTo(Card, iTween.Hash("rotation",new Vector3(0,0,0), "time", 0.2f, "easetype", easeType));
            iTween.RotateTo(Card, new Vector3(0,0,0), 0.15f );
            
            iTween.ScaleTo(Card, new Vector3(1.1f,1.1f,1.1f), 0.1f );
            yield return new WaitForSeconds(0.1f);
            iTween.ScaleTo(Card, new Vector3(1f,1f,1f), 0.1f );
            // Card.gameObject.SetActive(false);

            // Bahar_winSprite.GetComponent<Image>().sprite = Cards_deck_WinList[winNo];
            // AndarBahar.Gameplay.AndarBahar_CardHandler.Instance.CharacterImg.gameObject.SetActive(false);    
            AUI.WinAmountTxt.text = winPoint.ToString();
            yield return new WaitForSecondsRealtime(5);
            //AUI.WinAmountTxt.text = winPoint.ToString();
            Destroy_Cards();        
        }

        IEnumerator Final_Andhar_win(int cardno)
        {
            yield return StartCoroutine(AndarBahar_CardHandler.Instance.Start_CharacterAnimation());
            
            // yield return StartCoroutine(Move_And_Rotate_Card(Card_bahar, Target_bahar));

            GameObject Card = Instantiate(Card_andar, Target_andar );
            CardList_ToDelete.Add(Card);
            
            Card.gameObject.SetActive(true);
            Card.transform.position = Base_position.transform.position;
            Card.GetComponent<Image>().sprite = Cards_deck_WinList[cardno];

            iTween.MoveTo(Card, Target_andar.transform.position, 0.3f); 
            // iTween.RotateTo(Card, iTween.Hash("rotation",new Vector3(0,180,0), "time", 0.5f, "easetype", easeType));          
            iTween.RotateTo(Card, new Vector3(0,0,0), 0.15f );
            
            iTween.ScaleTo(Card, new Vector3(1.1f,1.1f,1.1f), 0.1f );
            yield return new WaitForSeconds(0.1f);
            iTween.ScaleTo(Card, new Vector3(1f,1f,1f), 0.1f );
            // Card.gameObject.SetActive(false);


            // Andhar_winSprite.GetComponent<Image>().sprite = Cards_deck_WinList[winNo];
            // AndarBahar.Gameplay.AndarBahar_CardHandler.Instance.CharacterImg.gameObject.SetActive(false);
            AUI.WinAmountTxt.text = winPoint.ToString();
            yield return new WaitForSecondsRealtime(5);
            //AUI.WinAmountTxt.text = winPoint.ToString();
            Destroy_Cards();
        }


        float delay = .25f;
        IEnumerator ShowRings(GameObject ring1)
        {

            //AUI.WinAmountTxt.text = winPoint.ToString();                

            int rounds = 6;
            Debug.Log("Winning Ring");
            for (int i = 0; i < rounds; i++)
            {
                
                ring1.gameObject.SetActive(true);
                yield return new WaitForSeconds(delay);
                ring1.gameObject.SetActive(false);               
                yield return new WaitForSeconds(delay);
            }
            yield return new WaitForSeconds(delay);
            ring1.gameObject.SetActive(true);
            yield return new WaitForSeconds(delay);
            ring1.gameObject.SetActive(false);
            GetComponent<AndarBahar_ChipController>().DestroyChips();
            AUI.WinAmountTxt.text = winPoint.ToString();
            if(winPoint > 0)
            {
                StartCoroutine(AUI.takeblink());
            }
            // if(int.Parse(AUI.WinText.text)>0)
            // {
            //     StartCoroutine(AUI.takeblink());
            // }
            
            AndarBahar_CardHandler.Instance.takebtn.SetActive(true);
            if (int.Parse(AUI.WinAmountTxt.text) == 0)
            {
                AUI.ResetUI();
            }

            // if (CardList_ToDelete.Count != 0)
            // {

            //     Destriy();            
            // }
        }

        public void Destroy_Cards() 
        {
            Debug.Log("Destroy Cards:");
            AndarBahar_CardHandler.Instance.Stop_CharacterAnimation();
            // foreach (var card in CardList_ToDelete)
            // {
            //     Destroy(card);                
            // }
            // CardList_ToDelete.Clear();
        }
        public void nextrounddelete()
        {
            foreach (var card in CardList_ToDelete)
            {
                Destroy(card);                
            }
            CardList_ToDelete.Clear();
        }
    }

    public class Winning
    {
        public int winNo, win_andhar_bahar,winPoint;
        public List<int> previous_Wins;
        public int[] Andar_Card_List,Bahar_Card_List;
        public float Balance;
    }
    public class pahila
    {
        public int gametimer;
        public List<int> previous_Wins;
    }
    public class winneramount
    {
        public int status;
        public string message;
        public data data;

    }
    public class data
    {
        public float Winamount;
    }

}
