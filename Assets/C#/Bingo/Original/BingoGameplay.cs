using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BingoGameplay : MonoBehaviour
{
    public static BingoGameplay Instance;
    public Button[] box1;
    public Button[] box2;
    public Button[] box3;
    public Button[] box4;
    public Button[] box5;
    public Button[] box6;
    public Text[] five;
    [SerializeField] Sprite[] boxes;
    List<int> winner = new List<int>();
    List<int> winner2 = new List<int>();
    List<int> winner3 = new List<int>();
    List<int> winner4 = new List<int>();
    List<int> winner5 = new List<int>();
    List<int> winner6 = new List<int>();

    List<int> numbers = new List<int>();
    public List<int> finale;
    int count;
    public int[,] box1_2d = new int[5,5];
    public int[,] box2_2d = new int[5,5];
    public int[,] box3_2d = new int[5,5];
    public int[,] box4_2d = new int[5,5];
    public int[,] box5_2d = new int[5,5];
    public int[,] box6_2d = new int[5,5];
    int[] values = new int[6];
    [SerializeField] Text boxvalue1;
    [SerializeField] Text boxvalue2;
    [SerializeField] Text boxvalue3;
    [SerializeField] Text boxvalue4;
    [SerializeField] Text boxvalue5;
    [SerializeField] Text boxvalue6;
    int totalbets;
    void Start()
    {
        Instance = this;
        assignnumbers();
        assignbox1();
        //StartCoroutine(numberanimation());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator Generatenumbers()
    {
        //Debug.Log("hii");
        int number =Random.Range(0,numbers.Count);
        //Debug.Log(number);
        for (int i = 0; i < numbers.Count; i++)
        {
            if (i == number)
            {
                finale.Add(numbers[number]);
                //numbers.RemoveAt(numbers[number]);
                count++;
            }
        }
        if(count<=4)
        {
            StartCoroutine(Generatenumbers());
        }
        yield return new WaitForSeconds(1f);
    }
    void assignnumbers()
    {
        for (int i = 1; i < 26; i++)
        {
            numbers.Add(i);
        }
    }
    void Start_numberanimation()
    {
        
    }
    void callgenerator()
    {
        count =0;
        finale.Clear();
        StartCoroutine(Generatenumbers());
    }

    public void BetButton()
    {
        if (totalbets >0)
        {
            
            callgenerator();
            Shownumbers();
            resetall();
            StartCoroutine(numberanimation());
            //allchecker();
        }
        
        
        //checkbox1();
        //checkbox2();
        //Invoke("checkbox1",1f);
    }
    void resetall()
    {
        foreach (var item in box1)
        {
            item.GetComponent<Image>().sprite = boxes[0];
            item.transform.GetChild(0).GetComponent<Text>().color = new Color32(0,0,0,255);
        }
        
        foreach (var item in box2)
        {
            item.GetComponent<Image>().sprite = boxes[1];
            item.transform.GetChild(0).GetComponent<Text>().color = new Color32(0,0,0,255);
        }
        foreach (var item in box3)
        {
            item.GetComponent<Image>().sprite = boxes[2];
            item.transform.GetChild(0).GetComponent<Text>().color = new Color32(0,0,0,255);
        }
        foreach (var item in box4)
        {
            item.GetComponent<Image>().sprite = boxes[3];
            item.transform.GetChild(0).GetComponent<Text>().color = new Color32(0,0,0,255);
        }
        foreach (var item in box5)
        {
            item.GetComponent<Image>().sprite = boxes[4];
            item.transform.GetChild(0).GetComponent<Text>().color = new Color32(0,0,0,255);
        }
        foreach (var item in box6)
        {
            item.GetComponent<Image>().sprite = boxes[5];
            item.transform.GetChild(0).GetComponent<Text>().color = new Color32(0,0,0,255);
        }
        emptystrings();
    }
    
    void Shownumbers()
    {
        for (int i = 0; i < five.Length; i++)
        {
            five[i].text = finale[i].ToString();
            Debug.Log("finale:"+ finale[i]);
        }
    }
    int cunmber = 0;
    IEnumerator numberanimation()
    {
        cunmber = 0;
        while(cunmber <25)
        {
            int animate1 = Random.Range(0,25);
            five[0].text = animate1.ToString();
            five[1].text = (animate1-2).ToString();
            five[2].text = (animate1-4).ToString();
            five[3].text = (animate1-1).ToString();
            five[4].text = (animate1-2).ToString();
            cunmber++;
            yield return new WaitForSeconds(0.02f);
        }
        StartCoroutine(numberanimationn());
        five[0].text = finale[0].ToString();
        allchecker(0);
       
       
    }
    IEnumerator numberanimationn()
    {
        cunmber = 0;
        while(cunmber <25)
        {
            int animate1 = Random.Range(0,25);
            five[1].text = animate1.ToString();
            five[2].text = (animate1-4).ToString();
            five[3].text = (animate1-1).ToString();
            five[4].text = (animate1-2).ToString();
            
            cunmber++;
            yield return new WaitForSeconds(0.02f);
        }
        StartCoroutine(numberan2mation());
        five[1].text = finale[1].ToString();
       allchecker(1);
       
       
    }
    IEnumerator numberan2mation()
    {
        cunmber = 0;
        while(cunmber <25)
        {
            int animate1 = Random.Range(0,25);
            five[2].text = animate1.ToString();
            five[3].text = (animate1-1).ToString();
            five[4].text = (animate1-2).ToString();
            cunmber++;
            yield return new WaitForSeconds(0.02f);
        }
        StartCoroutine(numberan3mation());
        five[2].text = finale[2].ToString();
        allchecker(2);
       
    }
    IEnumerator numberan3mation()
    {
        cunmber = 0;
        while(cunmber <25)
        {
            int animate1 = Random.Range(0,25);
            five[3].text = animate1.ToString();
            five[4].text = (animate1-2).ToString();
            cunmber++;
            yield return new WaitForSeconds(0.02f);
        }
        StartCoroutine(numberan4mation());
        five[3].text = finale[3].ToString();
        allchecker(3);
       
    }
    IEnumerator numberan4mation()
    {
        cunmber = 0;
        while(cunmber <25)
        {
            int animate1 = Random.Range(0,25);
            five[4].text = animate1.ToString();
            cunmber++;
            yield return new WaitForSeconds(0.02f);
        }
        
        five[4].text = finale[4].ToString();
        allchecker(4);
       
    }
    void allchecker(int number)
    {
        checkbox1(finale[number]);
        checkbox2(finale[number]);
        checkbox3(finale[number]);
        checkbox4(finale[number]);
        checkbox5(finale[number]);
        checkbox6(finale[number]);
       
    }
    void checkbox1(int number)
    {
        for (int i = 0; i < box1.Length; i++)
        {
            
            if(box1[i].transform.GetChild(0).GetComponent<Text>().text == number.ToString())
            {
                
                box1[i].GetComponent<Image>().sprite = boxes[6];
                box1[i].transform.GetChild(0).GetComponent<Text>().color = new Color32(255,0,0,255);
            }
        }
        box1checker();
    }
    void checkbox2(int number)
    {
        for (int i = 0; i < box2.Length; i++)
        {
            //Debug.Log("reached",five[0]);
            if(box2[i].transform.GetChild(0).GetComponent<Text>().text == number.ToString())//finale[0].ToString())
            {
                
                box2[i].GetComponent<Image>().sprite = boxes[6];
                box2[i].transform.GetChild(0).GetComponent<Text>().color = new Color32(255,0,0,255);
            }
        }
        box2checker();
    }
    void checkbox3(int number)
    {
        for (int i = 0; i < box2.Length; i++)
        {
            //Debug.Log("reached",five[0]);
            if(box3[i].transform.GetChild(0).GetComponent<Text>().text == number.ToString())//finale[0].ToString())
            {
                
                box3[i].GetComponent<Image>().sprite = boxes[6];
                box3[i].transform.GetChild(0).GetComponent<Text>().color = new Color32(255,0,0,255);
            }
        }
        box3checker();
    }
    void checkbox4(int number)
    {
        for (int i = 0; i < box2.Length; i++)
        {
            //Debug.Log("reached",five[0]);
            if(box4[i].transform.GetChild(0).GetComponent<Text>().text == number.ToString())//finale[0].ToString())
            {
                
                box4[i].GetComponent<Image>().sprite = boxes[6];
                box4[i].transform.GetChild(0).GetComponent<Text>().color = new Color32(255,0,0,255);
            }
        }
        box4checker();
        //     box5checker();
        //     box6checker();
    }
    void checkbox5(int number)
    {
        for (int i = 0; i < box2.Length; i++)
        {
            //Debug.Log("reached",five[0]);
            if(box5[i].transform.GetChild(0).GetComponent<Text>().text == number.ToString())//finale[0].ToString())
            {
                
                box5[i].GetComponent<Image>().sprite = boxes[6];
                box5[i].transform.GetChild(0).GetComponent<Text>().color = new Color32(255,0,0,255);
            }
        }
        box5checker();
        //     box6checker();
    }
    void checkbox6(int number)
    {
        for (int i = 0; i < box2.Length; i++)
        {
            //Debug.Log("reached",five[0]);
            if(box6[i].transform.GetChild(0).GetComponent<Text>().text == number.ToString())//finale[0].ToString())
            {
                
                box6[i].GetComponent<Image>().sprite = boxes[6];
                box6[i].transform.GetChild(0).GetComponent<Text>().color = new Color32(255,0,0,255);
            }
        }
        box6checker();
    }

    int jumper = 0;
    
    string[,]box1matched =new string[5,5];
    string[,]box2matched =new string[5,5];
    string[,]box3matched =new string[5,5];
    string[,]box4matched =new string[5,5];
    string[,]box5matched =new string[5,5];
    string[,]box6matched =new string[5,5];
    void assignbox1()
    {
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                
                box1_2d[i,j] = int.Parse(box1[jumper].transform.GetChild(0).GetComponent<Text>().text);
                box1matched[i,j]="n";
                box2_2d[i,j] = int.Parse(box2[jumper].transform.GetChild(0).GetComponent<Text>().text);
                box2matched[i,j]="n";
                box3_2d[i,j] = int.Parse(box3[jumper].transform.GetChild(0).GetComponent<Text>().text);
                box3matched[i,j]="n";
                box4_2d[i,j] = int.Parse(box4[jumper].transform.GetChild(0).GetComponent<Text>().text);
                box4matched[i,j]="n";
                box5_2d[i,j] = int.Parse(box5[jumper].transform.GetChild(0).GetComponent<Text>().text);
                box5matched[i,j]="n";
                box6_2d[i,j] = int.Parse(box6[jumper].transform.GetChild(0).GetComponent<Text>().text);
                box6matched[i,j]="n";
                //Debug.Log("value of  i"+i+"value of j" + j + "value assigned"+int.Parse(box1[jumper].transform.GetChild(0).GetComponent<Text>().text));
                jumper++;//int.Parse(box1[jumper].transform.GetChild(0).GetComponent<Text>().text);
            }
        }
        Debug.Log(box1_2d.ToString());
    }
    void emptystrings()
    {
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                box1matched[i,j]="n";
                box2matched[i,j]="n";
                box3matched[i,j]="n";
                box4matched[i,j]="n";
                box5matched[i,j]="n";
                box6matched[i,j]="n";
                
            }
            winner.Clear();winner2.Clear();winner3.Clear();winner4.Clear();winner5.Clear();winner6.Clear();
        }
    }
    void box1checker()///checks winning pattern in box1
    {
        int counter =0;
        bool encounter = false;
        foreach (var item in finale)
        {
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if(box1_2d[i,j] ==item)//5
                    {
                        box1matched[i,j]="y";
                    }
                    Debug.Log("2d"+"i"+i+"j"+j+box1matched[i,j]);
                    
                }
            }
        }

        ///here we check for straights horizontally in box1
        for (int i = 0; i < 5;i++)
        {
            counter = 0;
            encounter = false;
            winner.Clear();
            for (int j = 0; j < 5; j++)
            {
                if(box1matched[i,j] =="y")
                {
                    encounter = true;
                    //counter++;
                }
                if(encounter == true && box1matched[i,j] =="y")
                {
                    counter++;
                    winner.Add(box1_2d[i,j]);
                    checkdiagonalrighttoleft(box1matched,i,j,0);
                    checkdiagonallefttoright(box1matched,i,j);
                }
                else
                {
                    counter = 0;
                }
                if(counter >=3)
                {
                    //Debug.LogError("There is a straight in box1");
                    showwinnercard(winner,0);
                }
                //Debug.Log("counter at"+i+ counter+box1matched[i,j]);
                // else
                // {
                //     Debug.Log("NO straight in row "+i);
                // }
                
            }
        }
        //
        for (int j = 0; j < 5;j++)
        {
            counter = 0;
            encounter = false;
            winner.Clear();
            for (int i = 0; i < 5; i++)
            {
                if(box1matched[i,j] =="y")
                {
                    encounter = true;
                    //counter++;
                }
                if(encounter == true && box1matched[i,j] =="y")
                {
                    counter++;
                    winner.Add(box1_2d[i,j]);
                }
                else
                {
                    counter = 0;
                }
                if(counter >=3)
                {
                    showwinnercard(winner,0);
                    //Debug.LogError("There is a straight in box1");
                }
                //Debug.Log("counter at"+i+ counter+box1matched[i,j]);
                // else
                // {
                //     Debug.Log("NO straight in row "+i);
                // }
                
            }
        }
        //checking diagonall here 
        
    
    }
    void box2checker()///checks winning pattern in box2
    {
        int counter =0;
        bool encounter = false;
        foreach (var item in finale)
        {
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if(box2_2d[i,j] ==item)
                    {
                        box2matched[i,j]="y";
                    }
                    //Debug.Log("2d"+box1matched[i,j]);
                    
                }
            }
        }

        ///here we check for straights horizontally in box1
        for (int i = 0; i < 5;i++)
        {
            counter = 0;
            encounter = false;
            winner2.Clear();
            for (int j = 0; j < 5; j++)
            {
                if(box2matched[i,j] =="y")
                {
                    encounter = true;
                    //counter++;
                }
                if(encounter == true && box2matched[i,j] =="y")
                {
                    counter++;
                    winner2.Add(box2_2d[i,j]);
                    checkdiagonalrighttoleft(box2matched,i,j,1);
                    checkdiagonallefttoright(box2matched,i,j);
                }
                else
                {
                    counter = 0;
                }
                if(counter >=3)
                {
                    //Debug.LogError("There is a straight in box1");
                    showwinnercard(winner2,1);
                }
                //Debug.Log("counter at"+i+ counter+box2matched[i,j]);
                // else
                // {
                //     Debug.Log("NO straight in row "+i);
                // }
                
            }
        }
        //
        for (int j = 0; j < 5;j++)
        {
            counter = 0;
            encounter = false;
            winner2.Clear();
            for (int i = 0; i < 5; i++)
            {
                if(box2matched[i,j] =="y")
                {
                    encounter = true;
                    //counter++;
                }
                if(encounter == true && box2matched[i,j] =="y")
                {
                    counter++;
                    winner2.Add(box2_2d[i,j]);
                }
                else
                {
                    counter = 0;
                }
                if(counter >=3)
                {
                    showwinnercard(winner2,1);
                    //Debug.LogError("There is a straight in box1");
                }
                //Debug.Log("counter at"+i+ counter+box1matched[i,j]);
                // else
                // {
                //     Debug.Log("NO straight in row "+i);
                // }
                
            }
        }
    
    }
    void box3checker()///checks winning pattern in box3
    {
        int counter =0;
        bool encounter = false;
        foreach (var item in finale)
        {
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if(box3_2d[i,j] ==item)
                    {
                        box3matched[i,j]="y";
                    }
                    //Debug.Log("2d"+box1matched[i,j]);
                    
                }
            }
        }

        ///here we check for straights horizontally in box1
        for (int i = 0; i < 5;i++)
        {
            counter = 0;
            encounter = false;
            winner3.Clear();
            for (int j = 0; j < 5; j++)
            {
                if(box3matched[i,j] =="y")
                {
                    encounter = true;
                    //counter++;
                }
                if(encounter == true && box3matched[i,j] =="y")
                {
                    counter++;
                    winner3.Add(box3_2d[i,j]);
                    checkdiagonalrighttoleft(box3matched,i,j,2);
                    checkdiagonallefttoright(box3matched,i,j);
                }
                else
                {
                    counter = 0;
                }
                if(counter >=3)
                {
                    //Debug.LogError("There is a straight in box1");
                    showwinnercard(winner3,2);
                }
                //Debug.Log("counter at"+i+ counter+box3matched[i,j]);
                // else
                // {
                //     Debug.Log("NO straight in row "+i);
                // }
                
            }
        }
        //
        for (int j = 0; j < 5;j++)
        {
            counter = 0;
            encounter = false;
            winner3.Clear();
            for (int i = 0; i < 5; i++)
            {
                if(box3matched[i,j] =="y")
                {
                    encounter = true;
                    //counter++;
                }
                if(encounter == true && box3matched[i,j] =="y")
                {
                    counter++;
                    winner3.Add(box3_2d[i,j]);
                }
                else
                {
                    counter = 0;
                }
                if(counter >=3)
                {
                    showwinnercard(winner3,2);
                    Debug.LogError("There is a straight in box1");
                }
                //Debug.Log("counter at"+i+ counter+box1matched[i,j]);
                // else
                // {
                //     Debug.Log("NO straight in row "+i);
                // }
                
            }
        }
    
    }
    void box4checker()///checks winning pattern in box4
    {
        int counter =0;
        bool encounter = false;
        foreach (var item in finale)
        {
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if(box4_2d[i,j] ==item)
                    {
                        box4matched[i,j]="y";
                    }
                    //Debug.Log("2d"+box1matched[i,j]);
                    
                }
            }
        }

        ///here we check for straights horizontally in box1
        for (int i = 0; i < 5;i++)
        {
            counter = 0;
            encounter = false;
            winner4.Clear();
            for (int j = 0; j < 5; j++)
            {
                if(box4matched[i,j] =="y")
                {
                    encounter = true;
                    //counter++;
                }
                if(encounter == true && box4matched[i,j] =="y")
                {
                    counter++;
                    winner4.Add(box4_2d[i,j]);
                    checkdiagonalrighttoleft(box4matched,i,j,3);
                    checkdiagonallefttoright(box4matched,i,j);
                }
                else
                {
                    counter = 0;
                }
                if(counter >=3)
                {
                    //Debug.LogError("There is a straight in box1");
                    showwinnercard(winner4,3);
                }
                //Debug.Log("counter at"+i+ counter+box4matched[i,j]);
                // else
                // {
                //     Debug.Log("NO straight in row "+i);
                // }
                
            }
        }
        //
        for (int j = 0; j < 5;j++)
        {
            counter = 0;
            encounter = false;
            winner4.Clear();
            for (int i = 0; i < 5; i++)
            {
                if(box4matched[i,j] =="y")
                {
                    encounter = true;
                    //counter++;
                }
                if(encounter == true && box4matched[i,j] =="y")
                {
                    counter++;
                    winner4.Add(box4_2d[i,j]);
                }
                else
                {
                    counter = 0;
                }
                if(counter >=3)
                {
                    showwinnercard(winner4,3);
                    //Debug.LogError("There is a straight in box1");
                }
                //Debug.Log("counter at"+i+ counter+box1matched[i,j]);
                // else
                // {
                //     Debug.Log("NO straight in row "+i);
                // }
                
            }
        }
    
    }
    void box5checker()///checks winning pattern in box5
    {
        int counter =0;
        bool encounter = false;
        foreach (var item in finale)
        {
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if(box5_2d[i,j] ==item)
                    {
                        box5matched[i,j]="y";
                    }
                    //Debug.Log("2d"+box1matched[i,j]);
                    
                }
            }
        }

        ///here we check for straights horizontally in box1
        for (int i = 0; i < 5;i++)
        {
            counter = 0;
            encounter = false;
            winner5.Clear();
            for (int j = 0; j < 5; j++)
            {
                if(box5matched[i,j] =="y")
                {
                    encounter = true;
                    //counter++;
                }
                if(encounter == true && box5matched[i,j] =="y")
                {
                    counter++;
                    winner5.Add(box5_2d[i,j]);
                    checkdiagonalrighttoleft(box5matched,i,j,4);
                    checkdiagonallefttoright(box5matched,i,j);
                }
                else
                {
                    counter = 0;
                    winner5.Clear();
                }
                if(counter >=3)
                {
                    //Debug.LogError("There is a straight in box1");
                    showwinnercard(winner5,4);
                }
                //Debug.Log("counter at"+i+ counter+box1matched[i,j]);
                // else
                // {
                //     Debug.Log("NO straight in row "+i);
                // }
                
            }
        }
        //
        for (int j = 0; j < 5;j++)
        {
            counter = 0;
            encounter = false;
            winner5.Clear();
            for (int i = 0; i < 5; i++)
            {
                if(box5matched[i,j] =="y")
                {
                    encounter = true;
                    //counter++;
                }
                if(encounter == true && box5matched[i,j] =="y")
                {
                    counter++;
                    winner5.Add(box5_2d[i,j]);
                }
                else
                {
                    counter = 0;
                    winner5.Clear();
                }
                if(counter >=3)
                {
                    showwinnercard(winner5,4);
                    //Debug.LogError("There is a straight in box1");
                }
                //Debug.Log("counter at"+i+ counter+box5matched[i,j]);
                // else
                // {
                //     Debug.Log("NO straight in row "+i);
                // }
                
            }
        }
    
    }
    void box6checker()///checks winning pattern in box6
    {
        int counter =0;
        bool encounter = false;
        foreach (var item in finale)
        {
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if(box6_2d[i,j] ==item)
                    {
                        box6matched[i,j]="y";
                    }
                    //Debug.Log("2d"+box1matched[i,j]);
                    
                }
            }
        }

        ///here we check for straights horizontally in box1
        for (int i = 0; i < 5;i++)
        {
            counter = 0;
            encounter = false;
            winner6.Clear();
            for (int j = 0; j < 5; j++)
            {
                if(box6matched[i,j] =="y")
                {
                    encounter = true;
                    //counter++;
                }
                if(encounter == true && box6matched[i,j] =="y")
                {
                    counter++;
                    winner6.Add(box6_2d[i,j]);
                    checkdiagonalrighttoleft(box6matched,i,j,5);
                    checkdiagonallefttoright(box6matched,i,j);
                }
                else
                {
                    counter = 0;
                    winner6.Clear();
                }
                if(counter >=3)
                {
                    //Debug.LogError("There is a straight in box1");
                    showwinnercard(winner6,5);
                }
                //Debug.Log("counter at"+i+ counter+box6matched[i,j]);
                // else
                // {
                //     Debug.Log("NO straight in row "+i);
                // }
                
            }
        }
        //
        for (int j = 0; j < 5;j++)
        {
            counter = 0;
            encounter = false;
            winner6.Clear();
            for (int i = 0; i < 5; i++)
            {
                if(box6matched[i,j] =="y")
                {
                    encounter = true;
                    //counter++;
                }
                if(encounter == true && box6matched[i,j] =="y")
                {
                    counter++;
                    winner6.Add(box6_2d[i,j]);
                }
                else
                {
                    counter = 0;
                }
                if(counter >=3)
                {
                    showwinnercard(winner6,5);
                    //Debug.LogError("There is a straight in box1");
                }
                //Debug.Log("counter at"+i+ counter+box6matched[i,j]);
                // else
                // {
                //     Debug.Log("NO straight in row "+i);
                // }
                
            }
        }
    
    }

    void checkdiagonalrighttoleft(string[,] array,int x ,int y,int number)
    {
        int diagonal = 1;
        int[,] winbox= new int[5,5];
        int winboxno = new int();
        List<int> temp = new List<int>();
        switch (number)
        {
            
            case 0: winbox = box1_2d;
                winboxno = 0;
                temp =winner;
                break;
            case 1: winbox = box2_2d;
                winboxno = 1;
                temp =winner2;
                break;
            case 2: winbox = box3_2d;
                winboxno = 2;
                temp = winner3;
                break;
            case 3: winbox = box4_2d;
                winboxno = 3;
                temp = winner4;
                break;
            case 4: winbox = box5_2d;
                winboxno = 4;
                temp =winner5;
                break;
            case 5: winbox = box6_2d;
                winboxno = 5;
                temp = winner6;
                break;
            
        }
        while(x+1 <5 && y+1<5)
        {
            if(array[x+1,y+1] !="y")
            {
                break;
            }
            else
            {
                diagonal++;
                x++;
                y++;
                temp.Add(winbox[x,y]);//box1_2d[x,y]);
            }
        }
        if(diagonal >=3)
        {
            showwinnercard(temp,winboxno);
        }
    }
    void checkdiagonallefttoright(string[,] array,int x ,int y)
    {
        int diagonal = 1;
        while(x+1 <5 && y-1>=0)
        {
            if(array[x+1,y-1] !="y")
            {
                break;
            }
            else
            {
                diagonal++;
                x++;
                y--;
                winner.Add(box1_2d[x,y]);
            }
        }
        if(diagonal >=3)
        {
            showwinnercard(winner,0);
        }
    }
    void showwinnercard(List<int> numbers,int box)
    {
        switch(box)
        {
            case 0:
                foreach (var item in numbers)
                {
                    for (int i = 0; i < box1.Length; i++)
                    {
                        //Debug.Log("reached",five[0]);
                        if(box1[i].transform.GetChild(0).GetComponent<Text>().text == item.ToString())//finale[0].ToString())
                        {
                            
                            box1[i].GetComponent<Image>().sprite = boxes[7];
                            box1[i].transform.GetChild(0).GetComponent<Text>().color = new Color32(255,0,0,255);
                        }
                    }
                }
                break;
            case 1:
                foreach (var item in numbers)
                {
                    for (int i = 0; i < box2.Length; i++)
                    {
                        //Debug.Log("reached",five[0]);
                        if(box2[i].transform.GetChild(0).GetComponent<Text>().text == item.ToString())//finale[0].ToString())
                        {
                            
                            box2[i].GetComponent<Image>().sprite = boxes[7];
                            box2[i].transform.GetChild(0).GetComponent<Text>().color = new Color32(255,0,0,255);
                        }
                    }
                }
                break;
            case 2:
                foreach (var item in numbers)
                {
                    for (int i = 0; i < box3.Length; i++)
                    {
                        //Debug.Log("reached",five[0]);
                        if(box3[i].transform.GetChild(0).GetComponent<Text>().text == item.ToString())//finale[0].ToString())
                        {
                            
                            box3[i].GetComponent<Image>().sprite = boxes[7];
                            box3[i].transform.GetChild(0).GetComponent<Text>().color = new Color32(255,0,0,255);
                        }
                    }
                }
                break;
            case 3:
                foreach (var item in numbers)
                {
                    for (int i = 0; i < box4.Length; i++)
                    {
                        //Debug.Log("reached",five[0]);
                        if(box4[i].transform.GetChild(0).GetComponent<Text>().text == item.ToString())//finale[0].ToString())
                        {
                            
                            box4[i].GetComponent<Image>().sprite = boxes[7];
                            box4[i].transform.GetChild(0).GetComponent<Text>().color = new Color32(255,0,0,255);
                        }
                    }
                }
                break;
            case 4:
                foreach (var item in numbers)
                {
                    for (int i = 0; i < box5.Length; i++)
                    {
                        //Debug.Log("reached",five[0]);
                        if(box5[i].transform.GetChild(0).GetComponent<Text>().text == item.ToString())//finale[0].ToString())
                        {
                            
                            box5[i].GetComponent<Image>().sprite = boxes[7];
                            box5[i].transform.GetChild(0).GetComponent<Text>().color = new Color32(255,0,0,255);
                        }
                    }
                }
                break;
            case 5:
                foreach (var item in numbers)
                {
                    for (int i = 0; i < box6.Length; i++)
                    {
                        //Debug.Log("reached",five[0]);
                        if(box6[i].transform.GetChild(0).GetComponent<Text>().text == item.ToString())//finale[0].ToString())
                        {
                            
                            box6[i].GetComponent<Image>().sprite = boxes[7];
                            box6[i].transform.GetChild(0).GetComponent<Text>().color = new Color32(255,0,0,255);
                        }
                    }
                }
                break;
        }
    }
    public void addbets(int boxnumber)
    {
        values[boxnumber]++;
        totalbets++;
        switch (boxnumber)
        {
            
            case 0: boxvalue1.text = values[boxnumber].ToString();
                    break;
            case 1: boxvalue2.text = values[boxnumber].ToString();
                    break;
            case 2: boxvalue3.text = values[boxnumber].ToString();
                    break;
            case 3: boxvalue4.text = values[boxnumber].ToString();
                    break;
            case 4: boxvalue5.text = values[boxnumber].ToString();
                    break;
            case 5: boxvalue6.text = values[boxnumber].ToString();
                    break;
        }
        //value.text  = values[boxnumber].ToString();
        
    }

}


