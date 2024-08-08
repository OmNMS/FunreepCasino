using System;
using AndarBahar.Utility;
using AndarBahar.UI;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AndarBahar.Gameplay
{

    public class AndarBahar_CardHandler : MonoBehaviour
    {
        public static AndarBahar_CardHandler Instance;
        public Sprite[] Hears;
        public Sprite[] Spades;
        public Sprite[] Diamonds;
        public Sprite[] Clubs;
        public Sprite CardBackSide;
        public Transform Card_flip_andar, Card_flip_bahar, Target_andar, Target_bahar, Base_position;
        public Button test;
        public Button reset;
        public List<CardFlip> cardFlips = new List<CardFlip>();
        public Image CardImg;
        public Sprite[] CardFrames;
        public Image CharacterImg;
        public Sprite[] CharacterFrames;
        public Action Card_Spin_Complete;
        public GameObject takebtn;

        void Awake()
        {
            Instance = this;
        }
        public void Start()
        {
            winnerHandlercs = GetComponent<AndarBahar_RoundWinnerHandlercs>();
            botsManager = GetComponent<AndarBahar_BotsManager>();
            int limit = andar.childCount + bahar.childCount;
            int a = 0;
            int b = 0;
            for (int i = 0; i < limit; i++)
            {
                if (i % 2 == 0)
                {
                    var c = andar.GetChild(a).GetComponent<CardFlip>();
                    cardFlips.Add(c);
                    a++;
                }
                else
                {
                    var c = bahar.GetChild(b).GetComponent<CardFlip>();
                    cardFlips.Add(c);
                    b++;
                }
            }
        }

        int maxCards = 20;
        void ResetCards()
        {
            foreach (var item in cardFlips)
            {
                item.ResetRotation();
            }
            winnerCardObject.ResetRotation();
            cards.Clear();
        }
        // public Sprite GetCard(Card card)
        // {
        //     int cardNo = card.cardNo-1;
        //     switch (card.cardName)
        //     {
        //         case Cards.Hearts:
        //             return Hears[cardNo];
        //         case Cards.Speads:
        //             return Spades[cardNo];
        //         case Cards.Clubs:
        //             return Clubs[cardNo];
        //         case Cards.Diamonds:
        //             return Diamonds[cardNo];
        //         default: return null;
        //     }
        // }
        // public void SetWinnerCard(object cardDetail)
        // {
        //     Debug.Log("winner card");
        //     Debug.Log(cardDetail.ToString());
        //     WinnerCard wc = Fuction.GetObjectOfType<WinnerCard>(cardDetail);
        //     var card = new Card()
        //     {
        //         cardName = (Cards)wc.Joker_Card_Type,
        //         cardNo = wc.Joker_Card_No,
        //     };
        //     Sprite winnerCard = GetCard(card);
        //     uiHandler.DisplayWinnerCardOnDashBoard(card);
        //     ShowWinnerCard(winnerCard);
        // }
        public CardFlip winnerCardObject;
        void ShowWinnerCard(Sprite wc)
        {
            winnerCardObject.faceSprite.GetComponent<Image>().sprite = wc;
            winnerCardObject.StartFlip();
            winnerCardObject.HideCard();
        }
        List<Card> cards = new List<Card>();
       

        public Transform andar, bahar;
        AndarBahar_RoundWinnerHandlercs winnerHandlercs;
        AndarBahar_BotsManager botsManager;
        public AndarBahar_UiHandler uiHandler;
        /// <summary>
        /// this will called from server response
        /// </summary>
        /// <param name="c"></param>
        /// <param name="index"></param>
        // void CardAnimation(List<Card> c, object o, int index = 0)
        // {
        //     if (c.Count <= index)
        //     {
        //         Spots s = c.Count % 2 == 0 ? Spots.Andar : Spots.Bahar;
        //         // winnerHandlercs.OnWin(s/*, c.Count*/);
        //         Stop_CharacterAnimation();
        //         winnerHandlercs.OnWinAnimationComplete = () => ResetCards();

        //         //show win txt on bots
        //         //and update ui dashboard
        //         botsManager.OnDrawResult(o);
        //         uiHandler.UpdateDashboard(o);
        //         uiHandler.ResetUI();
        //         return;
        //     }
        //     int i = index;
        //     Sprite card = GetCard(c[i]);
        //     var f = cardFlips[i];
        //     f.gameObject.SetActive(true);
        //     f.backSprite = CardBackSide;
        //     f.faceSprite = card;
        //     Debug.Log("facesprite  " + card.name);
        //     f.OnFlipComplete = () => { CardAnimation(c,o, i); };
        //     f.StartFlip();
        //     i++;

        // }

        // public void OnRoundDrawResult(object o)
        // {
        //     Debug.Log("OnRoundDrawResult  ");
        //     List<Card> cardsList = new List<Card>();

        //     DrawResult drawResult = JsonConvert.DeserializeObject<DrawResult>(o.ToString());

        //     foreach (var c in drawResult.displayCard)
        //     {
        //         cardsList.Add(new Card
        //         {
        //             cardName = (Cards)c.type,
        //             cardNo = c.card
        //         });
        //     }
        //     CardAnimation(cardsList,o);
        // }

        #region 
        public IEnumerator Start_CardAnimation()
        {
            CardImg.gameObject.SetActive(true);
            winnerCardObject.gameObject.SetActive(false);
            while (_runbool)
            {
                foreach (var item in CardFrames)
                {
                    CardImg.sprite = item;
                    yield return new WaitForSeconds(0.000000001f);
                }
            }
            // Stop_CardAnimation();
        }
        public void Stop_CardAnimation()
        {
            StopCoroutine(Start_CardAnimation());
            // winnerCardObject.ShowCard();
            _runbool = false;
            // CharcterCoro = Start_CharacterAnimation();
            // StartCoroutine(CharcterCoro);
            CardImg.gameObject.SetActive(false);
            winnerCardObject.gameObject.SetActive(true);
            // Card_Spin_Complete?.Invoke();
            AndarBahar_RoundWinnerHandlercs.Instance.Card_SpinComplete(); //andar and bahar card animation start...!!
        }

        public IEnumerator CharcterCoro;
        public bool _runbool;
        public void playanimation()
        {
            // _runbool = true;
            // CharcterCoro = Start_CharacterAnimation();
            // StartCoroutine(CharcterCoro);
            StartCoroutine(Start_CharacterAnimation());
            
        }

        public void StopAnimation()
        {
            Stop_CharacterAnimation();
        }

        public IEnumerator Start_CharacterAnimation()
        {
            
            CharacterImg.gameObject.SetActive(true);
            foreach (var item in CharacterFrames)
            {
                CharacterImg.sprite = item;
                yield return new WaitForSeconds(0.0000001f);
            }
        }

        public void Stop_CharacterAnimation()
        {
            // _runbool = false;
            StopCoroutine(Start_CharacterAnimation());
            CharacterImg.gameObject.SetActive(false);
            takebtn.SetActive(true);
        }

        #endregion

    }
}

public class DisplayCard
{
    public int card;
    public int type;
}

public class DrawResult
{
    // public List<int> winningSpot;
    public List<DisplayCard> displayCard;
}