using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Networking;
using TMPro;

public class GameHandler : MonoBehaviour
{
    //ui objects
    public TMP_Text Score_Text, Bet_Text, Pop_Up_Text, Deal_Text, Help_Text;
    [SerializeField] HorizontalLayoutGroup centerCards, doubleUpPanel, bottomPanel;
    Button[] buttons;
    [SerializeField] GameObject goodLuckText;
    [SerializeField] List<GameObject> winningTypes =  new List<GameObject>();
    [SerializeField] List<TextMeshProUGUI> winningAmounts = new List<TextMeshProUGUI>();

    //cards
    [SerializeField] List<Card> deck = new List<Card>();
    CardList cardList;
    [SerializeField] Card defaultCard;
    List<Card> dealedCards = new List<Card>();
    List<Card> heldCards = new List<Card>();
    List<Card> hand = new List<Card>();
    Card mysteryCard = null;

    List<int> defaultWinningAmounts = new List<int>() {1000, 500, 150, 100, 10, 7, 5, 3, 2, 1};
    GameObject typeWon;
    int betValue = 0;
    float score = 200;
    int winningAmount = 0;
    bool isHand = false;
    bool blinking = false;
    [SerializeField] int dealLimit = 2;
    int dealCount = 0;

    //audio
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip startAudio;
    [SerializeField] AudioClip betAudio;
    [SerializeField] AudioClip cardDrawAudio;
    [SerializeField] AudioClip dupAudio;
    [SerializeField] AudioClip dupRevealAudio;


    // Start is called before the first frame update
    void Start()
    {
        cardList = GetComponent<CardList>();

        score = PlayerPrefs.GetFloat("points");

        for (int i = 0; i < 5; i++)
        {
            GameObject card = Instantiate(defaultCard.gameObject, centerCards.transform);
            card.transform.SetParent(centerCards.transform);
            dealedCards.Add(card.GetComponent<Card>());
        }

        Bet_Text.text = betValue.ToString();
        Score_Text.text = score.ToString("F2");
        Pop_Up_Text.text = "";

        buttons = bottomPanel.GetComponentsInChildren<Button>();
        foreach(Button button in buttons)
        {
            if(button.gameObject.name != "Bet")
            {
                //button.interactable = false;
                button.GetComponent<ButtonBlinker>().StopBlinking();
            }
            else
            {
                //button.interactable = true;
                StartCoroutine(button.GetComponent<ButtonBlinker>().TriggerBlinking());
            }
        }

        audioSource.PlayOneShot(startAudio);
        Help_Text.gameObject.SetActive(true);
        StartCoroutine(GoodLuckAnimation());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator GoodLuckAnimation()
    {
        while (betValue < 1)
        {
            foreach(Card card in dealedCards)
            {
                card.gameObject.SetActive(false);
            }
            goodLuckText.SetActive(true);

            yield return new WaitForSeconds(1f);

            goodLuckText.SetActive(false);
            foreach (Card card in dealedCards)
            {
                card.gameObject.SetActive(true);
            }

            yield return new WaitForSeconds(1f);
        }
    }

    void Shuffle()
    {
        System.Random rand = new System.Random();

        for (int i = deck.Count - 1; i > 0; i--)
        {
            int randomIndex = rand.Next(i + 1);
            Card tempCard = deck[i];
            deck[i] = deck[randomIndex];
            deck[randomIndex] = tempCard;
        }
    }

    public void ResetGame()
    {
        winningAmount = 0;
        SetBetValue(0);
        AddScore(0);
        isHand = false;
        dealCount = 0;
        Deal_Text.text = null;
        Pop_Up_Text.text = null;
        Help_Text.gameObject.SetActive(true);

        foreach (Button button in buttons)
        {
            if (button.gameObject.name == "Bet")
            {
                //button.interactable = true;
                StartCoroutine(button.GetComponent<ButtonBlinker>().TriggerBlinking());
            }
            else
            {
                //button.interactable = false;
                button.GetComponent<ButtonBlinker>().StopBlinking();
            }
        }

        foreach (Card card in hand)
        {
            if(card != null)
            Destroy(card.gameObject);
        }
        heldCards.Clear();

        for (int i = 0; i < 5; i++)
        {
            GameObject card = Instantiate(defaultCard.gameObject, centerCards.transform);
            card.transform.SetParent(centerCards.transform);
            dealedCards.Add(card.GetComponent<Card>());
        }

        blinking = false;

        for(int i = 0; i < 10; i++)
        {
            winningAmounts[i].text = defaultWinningAmounts[i].ToString();
        }
        //StartCoroutine(GoodLuckAnimation());
    }

    public void Take()
    {
        if (isHand)
        {
            centerCards.gameObject.SetActive(true);
            doubleUpPanel.gameObject.SetActive(false);

            AddScore(winningAmount);

            #region Code using API request (commented currently, implement when APIs are available)
            StartCoroutine(APIHandler.TakeAmount("GK"+PlayerPrefs.GetString("email")/*UserDetail.playerId_Local*/, score, (response) =>
            {
                if (response.status == 200)
                {
                    Debug.Log(response.message);
                    //SetScore(betValue);
                    ResetGame();
                }
            }));
            #endregion

            isHand = false;
            //SetScore(betValue);
            //ResetGame();
        }

    }

    public void Big()
    {
        if (mysteryCard != null)
        {
            if (mysteryCard.rank >= Rank.Eight)
            {
                //SetBetValue(betValue * 2);
                winningAmount *= 2;
                typeWon.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = winningAmount.ToString();
                Pop_Up_Text.text = "Correct! ";
                Debug.Log("Correct!");
            }
            else
            {
                SetBetValue(0);
                winningAmount = 0;
                typeWon.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = winningAmount.ToString();
                Pop_Up_Text.text = "Incorrect! ";
                Debug.Log("Incorrect");
            }

            foreach (Button button in buttons)
            {
                if (button.gameObject.name == "Take")
                {
                    //button.interactable = true;
                    StartCoroutine(button.GetComponent<ButtonBlinker>().TriggerBlinking());
                }
                else
                {
                    //button.interactable = false;
                    button.GetComponent<ButtonBlinker>().StopBlinking();
                }
            }

            doubleUpPanel.GetComponentInChildren<Image>().sprite = mysteryCard.GetComponent<Image>().sprite;

            //Pop_Up_Text.text += "Mystery card was " + mysteryCard.rank + " of " + mysteryCard.suit + System.Environment.NewLine + 
            //    "Press 'Take' to continue";
            Pop_Up_Text.text = "Press 'Take' to continue";
            Debug.Log("Mystery card was " + mysteryCard.rank + " of " + mysteryCard.suit);
            mysteryCard = null;

            //centerCards.gameObject.SetActive(true);
            //doubleUpPanel.gameObject.SetActive(false);

            audioSource.PlayOneShot(dupRevealAudio);
        }
    }

    public void Small()
    {
        if (mysteryCard != null)
        {
            if(mysteryCard.rank <= Rank.Six)
            {
                //SetBetValue(betValue * 2);
                winningAmount *= 2;
                typeWon.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = winningAmount.ToString();
                Pop_Up_Text.text = "Correct! ";
                Debug.Log("Correct!");
            }
            else
            {
                SetBetValue(0);
                winningAmount = 0;
                typeWon.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = winningAmount.ToString();
                Pop_Up_Text.text = "Incorrect! ";
                Debug.Log("Incorrect");
            }

            foreach (Button button in buttons)
            {
                if (button.gameObject.name == "Take")
                {
                    //button.interactable = true;
                    StartCoroutine(button.GetComponent<ButtonBlinker>().TriggerBlinking());
                }
                else
                {
                    //button.interactable = false;
                    button.GetComponent<ButtonBlinker>().StopBlinking();
                }
            }

            doubleUpPanel.GetComponentInChildren<Image>().sprite = mysteryCard.GetComponent<Image>().sprite;

            //Pop_Up_Text.text += "Mystery card was " + mysteryCard.rank + " of " + mysteryCard.suit + System.Environment.NewLine +
            //    "Press 'Take' to continue";
            Pop_Up_Text.text = "Press 'Take' to continue";
            Debug.Log("Mystery card was " + mysteryCard.rank + " of " + mysteryCard.suit);
            mysteryCard = null;

            //centerCards.gameObject.SetActive(true);
            //doubleUpPanel.gameObject.SetActive(false);

            audioSource.PlayOneShot(dupRevealAudio);
        }
    }

    /// <summary>
    /// Loads double up panel, generates a random card and a choice
    /// </summary>
    public void Double_Up()
    {
        if(isHand)
        {
            //loading double up panel
            centerCards.gameObject.SetActive(false);
            doubleUpPanel.gameObject.SetActive(true);
            Pop_Up_Text.text = null;

            doubleUpPanel.GetComponentInChildren<Image>().sprite = defaultCard.gameObject.GetComponent<Image>().sprite;

            audioSource.PlayOneShot(dupAudio);

            #region Code using API request (commented currently, implement when APIs are available)
            //request double up card
            StartCoroutine(APIHandler.DoubleUp("GK"+PlayerPrefs.GetString("email"), (response) =>
            {
                if (response.status == "success")
                {
                    //receiving the double up card generated
                    mysteryCard = cardList.GetCard(response.data.doubleUp_card[0], response.data.doubleUp_card[1]);

                    foreach (Button button in buttons)
                    {
                        if (button.gameObject.name == "Big" || button.gameObject.name == "Small")
                        {
                            //button.interactable = true;
                            StartCoroutine(button.GetComponent<ButtonBlinker>().TriggerBlinking());
                        }
                        else
                        {
                            //button.interactable = false;
                            button.GetComponent<ButtonBlinker>().StopBlinking();
                        }
                    }

                    Debug.Log("Is this mystery card higher than an 8 or smaller than a 6?");
                }
            }));
            #endregion

            #region Code using Local Assets (replace later)
            //mysteryCard = deck[0];

            //foreach (Button button in buttons)
            //{
            //    if (button.gameObject.name == "Big" || button.gameObject.name == "Small")
            //    {
            //        button.interactable = true;
            //button.GetComponent<ButtonBlinker>().isBlinking = true;
            //button.GetComponent<ButtonBlinker>().TriggerBlinking();
            //    }
            //    else
            //    {
            //        button.interactable = false;
            //button.GetComponent<ButtonBlinker>().StopBlinking();

            //    }
            //}

            //Debug.Log("Is this mystery card higher than an 8 or smaller than a 6?");
            #endregion
        }
    }

    /// <summary>
    /// Deals new playing cards from deck
    /// </summary>
    public void Deal()
    {
        if (dealCount < dealLimit && !isHand && betValue > 0)
        {
            foreach (Button button in buttons)
            {
                if (button.gameObject.name == "Cancel")
                {
                    //button.interactable = true;
                    StartCoroutine(button.GetComponent<ButtonBlinker>().TriggerBlinking());
                }
                if (button.gameObject.name == "Bet")
                {
                    //button.interactable = false;
                    button.GetComponent<ButtonBlinker>().StopBlinking();
                }
            }

            //clearing previously dealed cards that are not held
            foreach (Card card in dealedCards)
            {
                if (card != null)
                {
                    if (!heldCards.Contains(card))
                    {
                        Destroy(card.gameObject);
                    }
                }
            }
            dealedCards.Clear();

            //shuffling deck before dealing
            //Shuffle();

            #region Code using Local Assets
            //getting 5 random cards
            //for (int i = 0; i < 5 - heldCards.Count; i++)
            //{
            //    GameObject dealedCard = Instantiate(deck[i].gameObject, centerCards.transform);
            //    dealedCard.transform.SetParent(centerCards.transform);
            //    dealedCards.Add(dealedCard.GetComponent<Card>());

            //    dealedCard.GetComponentInChildren<TextMeshProUGUI>().text = null;
            //}
            #endregion

            #region  Code using API request (commented currently, implement when APIs are available)
            StartCoroutine(APIHandler.BetsPlaced(UserDetail.playerId_Local, betValue, (response) =>
            {
                if (response.status == 200)
                {
                    Debug.Log("From GameHandler response: " + response.data);
                    for (int i = 0; i < response.data.cards.Count - heldCards.Count; i++)
                    {
                        int[] card = response.data.cards[i];

                        GameObject dealedCard = Instantiate(cardList.GetCard(card[0], card[1]).gameObject, centerCards.transform);

                        dealedCard.transform.SetParent(centerCards.transform);
                        dealedCards.Add(dealedCard.GetComponent<Card>());

                        audioSource.PlayOneShot(cardDrawAudio);
                    }

                    //Debug.Log("-------DEALED CARDS-------");
                    //for (int i = 0; i < dealedCards.Count; i++)
                    //{
                    //    Debug.Log(dealedCards[i].rank + "of" + dealedCards[i].suit);
                    //}

                    dealCount++;
                    Help_Text.gameObject.SetActive(false);
                    CheckCard();
                }
            }));
            #endregion

            //dealCount++;
            //CheckCard();
        }
    }

    /// <summary>
    /// Check for cards that potentially makes a hand
    /// </summary>
    public void CheckCard()
    {
        //for Jacks or Better
        foreach(Card card in dealedCards)
        {
            if(card.rank >= Rank.Jack)
            {
                HoldCard(card);
            }
        }

        //for same ranks
        for (int i = 0; i < dealedCards.Count; i++)
        {
            for(int j = i + 1; j < dealedCards.Count; j++)
            {
                if(dealedCards[i].rank == dealedCards[j].rank)
                {
                    HoldCard(dealedCards[i]);
                    HoldCard(dealedCards[j]);
                }
            }    
        }

        CheckForHand();
    }

    /// <summary>
    /// Check if we have a poker hand
    /// </summary>
    public void CheckForHand()
    {
        hand.Clear();
        hand.AddRange(dealedCards);

        foreach(Card card in heldCards)
        {
            if (!dealedCards.Contains(card))
            {
                hand.Add(card);
            }
        }
        Debug.Log("Total cards = " + hand.Count);

        //sorting by rank
        hand.Sort(new CardRankComparer());

        isHand = IsFlush(hand) || IsPair(hand) || IsStraight(hand);

        if (isHand)
        {
            foreach (Button button in buttons)
            {
                if (button.gameObject.name == "Take" || button.gameObject.name == "D-Up")
                {
                    //button.interactable = true;
                    StartCoroutine(button.GetComponent<ButtonBlinker>().TriggerBlinking()); ;
                }
                else
                {
                    //button.interactable = false;
                    button.GetComponent<ButtonBlinker>().StopBlinking();
                }

                Deal_Text.text = null;
                Pop_Up_Text.text = "Press 'Take' or 'D-Up' button";
            }
        }
        else if(!isHand && dealCount == dealLimit)
        {
            Debug.Log("Better Luck Next Time!");
            Pop_Up_Text.text = "Better Luck Next Time!";

            StartCoroutine(APIHandler.TakeAmount("GK" + PlayerPrefs.GetString("email")/*UserDetail.playerId_Local*/, score, (response) =>
            {
                if (response.status == 200)
                {
                    Debug.Log(response.message);
                    StartCoroutine(WaitBeforeReset());
                }
            }));
        }
    }

    private IEnumerator WaitBeforeReset()
    {
        yield return new WaitForSeconds(3);
        ResetGame();
    }


    #region Checking for Poker Hands 
    private bool IsStraight(List<Card> hand)
    {
        //check if cards are in sequence
        int count = 0;
        for (int i = 0; i < hand.Count-1; i++)
        {
            if (hand[i].rank - 1 == hand[i + 1].rank)
            {
                count++;
            }
        }
        if(count == 4)
        {
            Pop_Up_Text.text = "Straight!";
            Debug.Log("Straight!");
            //SetBetValue(betValue + 5);
            foreach(GameObject winType in winningTypes)
            {
                if (winType.GetComponent<TextMeshProUGUI>().text == "Straight")
                {
                    blinking = true;

                    StartCoroutine(Blink(winType));
                    winningAmount = int.Parse(winType.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text);
                }
            }

            return true;
        }
        else
        {
            return false;
        }
    }

    private bool IsPair(List<Card> hand)
    {
        Dictionary<Rank, int> repeats = new Dictionary<Rank, int>();

        for (int i = 0; i < hand.Count-1; i++)
        {
            if (hand[i].rank == hand[i + 1].rank)
            {
                if (!repeats.ContainsKey(hand[i].rank))
                {
                    int count = hand.FindAll(x => x.rank == hand[i].rank).Count();
                    if(count > 1)
                    {
                        repeats.Add(hand[i].rank, count);
                    }
                }
            }
        }

        Debug.Log(repeats.Count);
        foreach (KeyValuePair<Rank, int> pair in repeats)
        {
            Debug.Log(pair.Key + " repeats " + pair.Value + " times");
        }

        if (repeats.ContainsValue(3))
        {
            Pop_Up_Text.text = "Three of a kind!";
            Debug.Log("Three of a Kind!");
            //SetBetValue(betValue + 3);
            foreach (GameObject winType in winningTypes)
            {
                if (winType.GetComponent<TextMeshProUGUI>().text == "3 of a Kind")
                {
                    blinking = true;

                    StartCoroutine(Blink(winType));
                    winningAmount = int.Parse(winType.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text);
                }
            }

            return true;
        }

        if (repeats.Count >= 2)
        {
            if (repeats.ContainsValue(3) && repeats.ContainsValue(2))
            {
                Pop_Up_Text.text = "Full House!";
                Debug.Log("Full House!");
                //SetBetValue(betValue + 10);
                foreach (GameObject winType in winningTypes)
                {
                    if (winType.GetComponent<TextMeshProUGUI>().text == "Full House")
                    {
                        blinking = true;

                        StartCoroutine(Blink(winType));
                        winningAmount = int.Parse(winType.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text);
                    }
                }

                return true;
            }
            else
            {
                Pop_Up_Text.text = "Two Pair!";
                Debug.Log("Two Pair!");
                //SetBetValue(betValue + 2);
                foreach (GameObject winType in winningTypes)
                {
                    if (winType.GetComponent<TextMeshProUGUI>().text == "2 Pairs")
                    {
                        blinking = true;

                        StartCoroutine(Blink(winType));
                        winningAmount = int.Parse(winType.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text);
                    }
                }

                return true;
            }
        }
        else
        {
            if (repeats.ContainsValue(4))
            {
                Pop_Up_Text.text = "Four of a Kind!";
                Debug.Log("Four of a Kind!");
                //SetBetValue(betValue + 100);
                foreach (GameObject winType in winningTypes)
                {
                    if (winType.GetComponent<TextMeshProUGUI>().text == "4 of a Kind")
                    {
                        blinking = true;

                        StartCoroutine(Blink(winType));
                        winningAmount = int.Parse(winType.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text);
                    }
                }

                return true;
            }
            else
            {
                foreach (KeyValuePair<Rank, int> pair in repeats)
                {
                    if (pair.Key >= Rank.Jack)
                    {
                        Pop_Up_Text.text = "Is Jacks or Better!";
                        Debug.Log("Is Jacks or Better!");
                        //SetBetValue(betValue + 1);
                        foreach (GameObject winType in winningTypes)
                        {
                            if (winType.GetComponent<TextMeshProUGUI>().text == "Jackes Better")
                            {
                                blinking = true;

                                StartCoroutine(Blink(winType));
                                winningAmount = int.Parse(winType.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text);
                            }
                        }

                        return true;
                    }
                }
                return false;
            }
        }
    }
    #endregion

    /// <summary>
    /// Checks if given list of cards form a flush
    /// </summary>
    /// <param name="hand">card list</param>
    public bool IsFlush(List<Card> hand)
    {
        //creating a list of only ranks from the hand
        List<Rank> handRanks = new List<Rank>();
        foreach (Card card in hand)
        {
            handRanks.Add(card.rank);
        }
        handRanks.Reverse();

        //check for any flush
        if (hand.TrueForAll(x => x.suit == hand[0].suit))
        {
            //check if cards are in sequence
            int count = 0;
            for (int i = 0; i < handRanks.Count; i++)
            {
                if (handRanks[i] - 1 == handRanks[i + 1])
                {
                    count++;
                }
            }

            if (count == 4)
            {
                //check for royal flush
                List<Rank> royalFush = new List<Rank> { Rank.Ace, Rank.King, Rank.Queen, Rank.Jack, Rank.Ten };
                if (handRanks.SequenceEqual(royalFush))
                {
                    Pop_Up_Text.text = "Royal Flush!";
                    Debug.Log("Royal Flush!");
                    //SetBetValue(betValue + 500);
                    foreach (GameObject winType in winningTypes)
                    {
                        if (winType.GetComponent<TextMeshProUGUI>().text == "Royal Flush")
                        {
                            blinking = true;

                            StartCoroutine(Blink(winType));
                            winningAmount = int.Parse(winType.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text);
                        }
                    }

                    return true;
                }
                else
                {
                    Pop_Up_Text.text = "Straight Flush!";
                    Debug.Log("Straight Flush!");
                    //SetBetValue(betValue + 150);
                    foreach (GameObject winType in winningTypes)
                    {
                        if (winType.GetComponent<TextMeshProUGUI>().text == "Straight Flush")
                        {
                            blinking = true;
                            StartCoroutine(Blink(winType));
                            winningAmount = int.Parse(winType.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text);
                        }
                    }

                    return true;
                }
            }
            else
            {
                Pop_Up_Text.text = "Flush!";
                Debug.Log("Flush!");
                //SetBetValue(betValue + 7);
                foreach (GameObject winType in winningTypes)
                {
                    if (winType.GetComponent<TextMeshProUGUI>().text == "Flush")
                    {
                        blinking = true;
                        StartCoroutine(Blink(winType));
                        winningAmount = int.Parse(winType.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text);
                    }
                }

                return true;
            }
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// Adds given card to heldCards list
    /// </summary>
    /// <param name="card">card to hold</param>
    public void HoldCard(Card card)
    {
        if (!heldCards.Contains(card))
        {
            heldCards.Add(card);

            //card.gameObject.GetComponent<Image>().color = new Color(1f, 0.5f, 0.5f, 1f);
            card.GetComponentInChildren<TextMeshProUGUI>().color = Color.yellow;
            card.GetComponentInChildren<TextMeshProUGUI>().text = "HOLD";

            card.GetComponent<Button>().onClick.AddListener(() => CancelCard(card));

            Pop_Up_Text.text = "Push Cancel to Cancel Holds";
        }
    }

    /// <summary>
    /// Clears heldCards list and added back to deck
    /// </summary>
    public void CancelAll()
    {
        if (dealCount < dealLimit && !isHand)
        {
            foreach (Card card in heldCards)
            {
                card.gameObject.GetComponent<Image>().color = Color.white;
                card.GetComponentInChildren<TextMeshProUGUI>().text = null;

                dealedCards.Add(card);
            }
            heldCards.Clear();
        }
    }

    /// <summary>
    /// Cancels the hold of a specific card
    /// </summary>
    public void CancelCard(Card card)
    {
        dealedCards.Add(card);
        heldCards.Remove(card);
        card.gameObject.GetComponent<Image>().color = Color.white;
        card.GetComponentInChildren<TextMeshProUGUI>().text = null;
    }

    //public void ButtonClick()
    //{
    //    string buttonName = EventSystem.current.currentSelectedGameObject.name;
    //    Debug.Log("Clicked " + buttonName);
    //}



    /// <summary>
    /// Place bet value
    /// </summary>
    public void Bet()
    {
        if (score > 0 && dealCount == 0)
        {
            audioSource.PlayOneShot(betAudio);
            betValue++;
            Bet_Text.text = betValue.ToString();
            Deal_Text.text = "Push Deal";

            AddScore(-1);

            foreach(Button button in buttons)
            {
                if(button.gameObject.name == "Deal")
                {
                    //button.interactable = true;
                    StartCoroutine(button.GetComponent<ButtonBlinker>().TriggerBlinking());
                }
            }

            for(int i = 0; i < 10; i++)
            {
                int winValue = defaultWinningAmounts[i];
                winValue *= betValue;
                winningAmounts[i].text = winValue.ToString();
            }
        }
    }

    public void SetBetValue(int value)
    {
        betValue = value;
        Bet_Text.text = betValue.ToString();
    }

    public void AddScore(int value)
    {
        score += value;
        Score_Text.text = score.ToString("F2");
        PlayerPrefs.SetFloat("points", score);
    }


    IEnumerator Blink(GameObject obj)
    {
        typeWon = obj;
        while (blinking)
        {
            obj.SetActive(false);
            yield return new WaitForSeconds(0.5f);
            obj.SetActive(true);
            yield return new WaitForSeconds(0.5f);
        }
    }

    public void CloseScene()
    {
        SceneManager.LoadScene("MainScene");
    }

}

/// <summary>
/// Class to compare two playing cards by rank
/// </summary>
public class CardRankComparer : IComparer<Card>
{
    public int Compare(Card x, Card y)
    {
        return x.rank.CompareTo(y.rank);
    }
}
