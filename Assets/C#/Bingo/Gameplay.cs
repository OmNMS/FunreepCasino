using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Handles Bingo gameplay
/// </summary>
public class Gameplay : MonoBehaviour
{
    #region Fields

    public static Gameplay instance;

    //list of all 6 bingo boards available to the player
    [SerializeField] List<Board> bingoBoards = new List<Board>();

    //list of 5 numbers to be generated at the start of game
    List<int> numbers = new List<int>();
    [SerializeField] List<Text> numbersText = new List<Text>();

    //sprite reference for marked box
    [SerializeField] Sprite markedSprite;
    [SerializeField] Sprite winnersprite;
    [SerializeField] Text[] shownumber;
    [SerializeField] Animator[] animate;
    [SerializeField] GameObject Numbertemp;
    [SerializeField] GameObject Animationbox;
    [SerializeField] Sprite[] card;

    //player score/balance
    float score;
    [SerializeField] Text scoreText;
    [SerializeField] Text winText;

    //buttons
    [SerializeField]Button takeBtn;
    [SerializeField] Button bigBtn;
    [SerializeField] Button smallBtn;
    [SerializeField] Button startBtn;

    //button variants
    [SerializeField]Button TakeGreen;
    [SerializeField]Button BigGreen;
    [SerializeField]Button SmallGreen;

    [SerializeField] Image backcard;
    [SerializeField] Sprite redcard;

    string answer;
    int[] betholder = new int[6];
    int doublevalue;
    bool added;
    bool isFirstround;
    //int score;

    //double up UI reference

    //total bet amount placed across all the bingo boards
    int totalBets = 0;
    public string balanceurl =  "http://139.59.92.165:5000/user/bingoGetBalance";
    int streak = 0;
    //public GameObject startblue,startgreen;


    #endregion

    #region Methods

    private void Awake()
    {
        instance = this;
        //CallGetBalanceAPI();
    }

    void Start()
    {
        scoreText.text = PlayerPrefs.GetFloat("points").ToString("F2");
        score = float.Parse(scoreText.text);
        StartBlinking(startBtn);
        StartCoroutine(Heartblink());
        doubletext.text = PlayerPrefs.GetInt("doublebingo").ToString();
        isFirstround=true;
        //ToggleBlinking(StartBlue);
        //StartCoroutine(startblink());
        //CallGetBalanceAPI();
        Debug.Log("Score = " + score);
    }

    /// <summary>
    /// Starts blinking of the given button
    /// </summary>
    /// <param name="button">button to start blinking</param>
    void StartBlinking(Button button)
    {
        StartCoroutine(button.GetComponent<ButtonBlinker>().TriggerBlinking());
    }

    /// <summary>
    /// Stops blinking of the given button
    /// </summary>
    /// <param name="button">button to stop blinking</param>
    void StopBlinking(Button button)
    {
        button.GetComponent<ButtonBlinker>().StopBlinking();
    }

    /// <summary>
    /// Adds to total bet amount
    /// </summary>
    /// <param name="betAmount">amount to add</param>
    public void AddBet( int box)
    {
        totalBets++;

        //stopstartblink()
        betholder[box] += 1;
        score--;
        scoreText.text = score.ToString("F2");
    }

    /// <summary>
    /// Method for the Start Button. Starts the game by generating 5 random numbers 
    /// and check across the boards for a bingo only when bets are placed.
    /// </summary>
    public void StartGame()
    {
        if(totalBets > 0 && int.Parse(winText.text) ==0)
        {
            //reset boards and clear previous numbers
            ResetBoards();
            numbers.Clear();
            CallBetsPlacedAPI();
            //generate 5 numbers
            //ScrollToLastObject.instance.ScrollToBottom();
            //StartCoroutine(NumberGeneratorCoroutine());
            Numbertemp.SetActive(false);
            Animationbox.SetActive(true);
            foreach (var item in animate)
            {
                item.SetBool("started",true);
            }
           
        }
        else
        {
            Debug.Log("No bet amount raised yet!");
        }
    }

    /// <summary>
    /// Resets all 6 bingo boards
    /// </summary>
    private void ResetBoards()
    {
        backcard.sprite = redcard;
        
        foreach(Board board in bingoBoards)
        {
            foreach(Button box in board.boxes)
            {
                //resetting box sprite to the default box sprite of the particular board 
                box.GetComponent<Image>().sprite = board.defaultBoxSprite;
                box.GetComponentInChildren<Text>().color = new Color32(0, 0, 0, 255);
            }
        }
    }
    [SerializeField] GameObject[] hearts;
    IEnumerator Heartblink()
    {
        for(int i =0; i <=streak;i++)
        {
            hearts[i].GetComponent<Image>().enabled = true;
            //hearts[i].SetActive(true);
        }
        yield return new WaitForSeconds(0.4f);
        for(int i =0; i <hearts.Length;i++)
        {
            hearts[i].GetComponent<Image>().enabled = false;
            //hearts[i].SetActive(false);
        }
        yield return new WaitForSeconds(0.4f);
        StartCoroutine(Heartblink());
    }

    /// <summary>
    /// Generates 5 random number within the range of 1 to 25 and added to the numbers list.
    /// </summary>
    /// <returns>a delay of 1s between each generation</returns>
    [SerializeField] Transform Numberssix;
    [SerializeField] Transform Animatonsix;
    private IEnumerator NumberGeneratorCoroutine(int[] bingonumber)
    {
        //int[] testCase = {5, 6, 12, 17, 2};
        int number = Random.Range(1,26);

        for(int i = 0; i < 5; i++)
        {
            while (numbers.Contains(number))
            {
                number = Random.Range(1, 26);
            }

            numbers.Add(number);
            numbersText[i].text = bingonumber[i].ToString();//number.ToString();
            shownumber[i].text = bingonumber[i].ToString();//number.ToString();

            MarkBoards(bingonumber[i]);
            //MarkBoards(testCase[i]);

            //test case
            //numbers.Add(testCase[i]);
            //numbersText[i].text = testCase[i].ToString();
            //MarkBoards(testCase[i]);

            yield return new WaitForSeconds(0.2f);
            //Debug.Log(numbers[i] + " added");
        }
        if(bingonumber[5] == 1)
        {
            Numberssix.GetChild(0).gameObject.SetActive(true);
            Animatonsix.GetChild(0).gameObject.SetActive(true);
            Numberssix.GetChild(1).gameObject.SetActive(false);
            Animatonsix.GetChild(1).gameObject.SetActive(false);

        }
        else if(bingonumber[5] == 2)
        {
            Numberssix.GetChild(0).gameObject.SetActive(false);
            Animatonsix.GetChild(0).gameObject.SetActive(false);
            Numberssix.GetChild(1).gameObject.SetActive(true);
            Animatonsix.GetChild(1).gameObject.SetActive(true);

        }  

        //check across all boards for bingo
        CheckForBingo();
        //Invoke("AfterAnimation",5f);
        //AfterAnimation();
        //showgreen();
        
    }

    //this function is called after the number animation is completed
    public void AfterAnimation()
    {
        // Numbertemp.SetActive(true);
        // Animationbox.SetActive(false);
        foreach (var item in animate)
        {
            item.SetBool("started",false);
        }
    }



    /// <summary>
    /// Marks a box in each board containing the given number
    /// </summary>
    /// <param name="number">the number to check in each board</param>
    private void MarkBoards(int number)
    {
        foreach(Board board in bingoBoards)
        {
            foreach(Button box in board.boxes)
            {
                if(box.GetComponentInChildren<Text>().text == number.ToString())
                {
                    box.GetComponent<Image>().sprite = winnersprite;//markedSprite;  
                    box.GetComponentInChildren<Text>().color = new Color32(255, 0, 0, 255);
                }
            }
        }
    }

    /// <summary>
    /// Checks each board for a possible bingo line
    /// </summary>
    private void CheckForBingo()
    {
        int bingoPos = 0;

        for(int i = 0; i < bingoBoards.Count; i++)
        {
            var matrix = ListToMatrixConverter.ConvertButtonListToMatrix(bingoBoards[i].boxes);
            //ListToMatrixConverter.DisplayMatrix(matrix);

            int row = 0;
            int col = 0;
            
            //check horizontal 
            for (row = 0; row < 5; row++)
            {
                List<int> winn = new List<int>();
                int count = 0;
                

                for (col = 0; col < 5; col++)
                {
                    if (matrix[row, col].GetComponent<Image>().sprite == winnersprite)//winnersprite is my winning number
                    {
                        count++;
                        winn.Add(int.Parse(matrix[row, col].transform.GetChild(0).GetComponent<Text>().text));//winning number is added to the list
                    }
                    else
                    {
                        count = 0;
                        winn.Clear();
                    }
                    
                    if(count >2)
                    {
                        showwinnercard(winn,i);//this calls the animation for winner where winn is the list containning the number and i is the box number in which the winner has occured
            
                    }
                }
            }

            //check vertical
            for (col = 0; col < 5; col++)
            {
                List<int> winn = new List<int>();
                int count = 0;

                for (row = 0; row < 5; row++)
                {
                    if (matrix[row, col].GetComponent<Image>().sprite == winnersprite)
                    {
                        count++;
                        diagonallylefttoright(row,col,i);
                        diagonallyrighttoleft(row,col,i);
                        winn.Add(int.Parse(matrix[row, col].transform.GetChild(0).GetComponent<Text>().text));//winning number is added to the list
                    }
                    else
                    {
                        count = 0;
                        winn.Clear();
                    }
                    if(count >2)
                    {
                        showwinnercard(winn,i);//this calls the animation for winner where winn is the list containning the number and i is the box number in which the winner has occured
                       
                        //bingoPos = (i + 1);
                        //break;
                    }
                }
                

                
            }

            //check top-left to bottom-right diagonal
           
        }

        //CallBetsPlacedAPI(bingoPos);
    }
    //this functions checks the diagonally available element if matched then proceed further or else throw out
    void diagonallylefttoright(int x,int y,int boxnumber)
    {
        int count = 1;
        List<int> diag = new List<int>();
        var matrix = ListToMatrixConverter.ConvertButtonListToMatrix(bingoBoards[boxnumber].boxes);
        diag.Add(int.Parse(matrix[x, y].transform.GetChild(0).GetComponent<Text>().text));
        while (x+1 <5 && y+1<5)
        {
            int xplus = x+1;
            int yplus = y+1;
            if(matrix[x+1, y+1].GetComponent<Image>().sprite != winnersprite)
            {
                diag.Clear();
                break;
            }
            else
            {
                count++;
                Debug.Log("winner "+int.Parse(matrix[x, y].transform.GetChild(0).GetComponent<Text>().text)+"next number is "+int.Parse(matrix[x+1, y+1].transform.GetChild(0).GetComponent<Text>().text)+" and the box number is"+boxnumber +" the countis"+count +"x:"+x+" y:"+y +"(x+1):"+xplus+"[y+1]:"+yplus);
                diag.Add(int.Parse(matrix[x+1, y+1].transform.GetChild(0).GetComponent<Text>().text));
                x++;
                y++;
               
                
                
            }
            if(count >2)
            {
                showwinnercard(diag,boxnumber);
            }
        }
    }
    void diagonallyrighttoleft(int x,int y,int boxnumber)
    {
        int count = 1;
        List<int> diag = new List<int>();
        var matrix = ListToMatrixConverter.ConvertButtonListToMatrix(bingoBoards[boxnumber].boxes);
        diag.Add(int.Parse(matrix[x, y].transform.GetChild(0).GetComponent<Text>().text));
        while (x+1 <5 && y-1>=0)
        {
            int xplus = x+1;
            int yplus = y+1;
            if(matrix[x+1, y-1].GetComponent<Image>().sprite != winnersprite)
            {
                diag.Clear();
                break;
            }
            else
            {
                count++;
                //Debug.Log("winner "+int.Parse(matrix[x, y].transform.GetChild(0).GetComponent<Text>().text)+"next number is "+int.Parse(matrix[x+1, y+1].transform.GetChild(0).GetComponent<Text>().text)+" and the box number is"+boxnumber +" the countis"+count +"x:"+x+" y:"+y +"(x+1):"+xplus+"[y+1]:"+yplus);
                diag.Add(int.Parse(matrix[x+1, y-1].transform.GetChild(0).GetComponent<Text>().text));
                x++;
                y--;
            
                
                
            }
            if(count >2)
            {
                showwinnercard(diag,boxnumber);
            }
        }
        
    }
    public void close()
    {
        SceneManager.LoadScene("MainScene");
    }
    void showwinnercard(List<int> win,int boxnumber)//this functon will be called to show the winnig card
    {
        
        foreach (var item in win)
        {
            foreach (Button box in bingoBoards[boxnumber].boxes)
            {
                if(box.GetComponentInChildren<Text>().text == item.ToString())
                {
                    box.GetComponent<Image>().sprite = markedSprite;  
                    box.GetComponentInChildren<Text>().color = new Color32(255, 0, 0, 255);
                }
            }
        }
    }
    public void cardchanger(int number)
    {
        //int something = Random.Range(0,52);
        backcard.sprite = card[number];
    }
    public void doubler(string value)
    {

        ValueDoubler(value);

        StopBlinking(bigBtn);
        StopBlinking(smallBtn);
        StartBlinking(takeBtn);
        //bigBtn.gameObject.SetActive(true);
        //smallBtn.gameObject.SetActive(true);
        //TakeGreen.gameObject.SetActive(true);
        //BigGreen.gameObject.SetActive(false);
        //SmallGreen.gameObject.SetActive(false);
    }
    void showgreen()
    {
        takeBtn.gameObject.SetActive(false);
        bigBtn.gameObject.SetActive(false);
        smallBtn.gameObject.SetActive(false);
        TakeGreen.gameObject.SetActive(true);
        BigGreen.gameObject.SetActive(true);
        SmallGreen.gameObject.SetActive(true);
    }
    public void TakeBtn()
    {
        winText.text ="0";
        TakeApi();
        backcard.sprite = redcard;
        StopBlinking(takeBtn);
        StopBlinking(bigBtn);
        StopBlinking(smallBtn);
        //TakeBlue.gameObject.SetActive(true);
        //BigBlue.gameObject.SetActive(true);
        //SmallBLue.gameObject.SetActive(true);
        //TakeGreen.gameObject.SetActive(false);
        //BigGreen.gameObject.SetActive(false);
        //SmallGreen.gameObject.SetActive(false);
    }
    [SerializeField] Text doubletext;

    #region API Calling Methods
    void CallGetBalanceAPI()
    {
        StartCoroutine(BingoAPIHandler.GetBalance("GK"+PlayerPrefs.GetString("emall"), (response) =>
        {
            score = response.balance;
            scoreText.text = response.balance.ToString("F2");
        }));
    }
    void TakeApi()
    {
        StartCoroutine(BingoAPIHandler.TakeAmount("GK"+PlayerPrefs.GetString("email"), (response) =>
        {
            score = response.point;
            scoreText.text = response.point.ToString("F2");
        }));
    }
    void ValueDoubler( string solution)
    {
        StartCoroutine(BingoAPIHandler.BigSmall("GK"+PlayerPrefs.GetString("email"),solution, (response) =>
        {
            cardchanger(response.data.double_up_number);
            winText.text = response.data.win_amount.ToString();
            if(response.data.win_amount >0)
            {
                //TakeBlue.gameObject.SetActive(false);
                //TakeGreen.gameObject.SetActive(true);
                StartBlinking(takeBtn);
                doublevalue++;
                doubletext.text = doublevalue.ToString();
                PlayerPrefs.SetInt("doublebingo",doublevalue);
            }
            else
            {
                //TakeGreen.gameObject.SetActive(false);
                //TakeBlue.gameObject.SetActive(true);
                //StartCoroutine(startblink());
                StopBlinking(takeBtn);
                StartBlinking(startBtn);
            }
            // score = response.balance;
            // scoreText.text = score.ToString();
        }));
    }

    /// <summary>
    /// Calls BetsPlaced API
    /// </summary>
    void CallBetsPlacedAPI()
    {
        for(int i = 0; i< betholder.Length;i++)
        {
            Debug.Log("the value of betholder at i" + betholder[i]);
        }
        if(!isFirstround)
        {
            score -=totalBets;
            scoreText.text = score.ToString("F2");
        }
        isFirstround = false;
        StartCoroutine(BingoAPIHandler.BetsPlaced("GK"+PlayerPrefs.GetString("email"), betholder, (response) =>
        {
            winText.text = response.data.win_amount.ToString();
            StartCoroutine(NumberGeneratorCoroutine(response.data.bingo));
            //process winamount here
            Debug.Log(response.data.win_amount);
            if(response.data.win_amount > 0)
            {
                //showgreen();
                StartBlinking(takeBtn);
                StartBlinking(bigBtn);
                StartBlinking(smallBtn);
                StopBlinking(startBtn);
                streak++;
            }
            else
            {
                streak =0;
                //StartCoroutine(startblink());
                StartBlinking(startBtn);
            }
            
        }));
    }

    #endregion

    #endregion
}
