using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int[] funtarget_previous = new int[10];
    public int funtargetwinning;

    public PlayerData (int[] funbet,int funwin)
    {
        for (int i = 0; i <funbet.Length; i++)
        {
            funtarget_previous[i] = funbet[i];   
            
        }
        //Debug.Log("jjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjj"+funwin);
        
        funtargetwinning = funwin;

    }
    
}
[System.Serializable]
public class TriplefunData
{
    public int winamount;
    public int[] singlebetdata = new int[10];
    //public static int[] doubleval;
    public int[] doublebetdata = new int[210];
    public List<int> triplebetdata = new List<int>();
    public List<int> triplevaldata = new List<int>();
    public string deviceid;

    public TriplefunData(int value,int[] singledata,int[] doubledata, List<int> triplekeydata,List<int> triplemoneydata,string phone)
    {
        winamount =value;
        for (int i = 0; i < singledata.Length; i++)
        {
            singlebetdata[i] = singledata[i];
        }
        
        for (int i = 0; i < doubledata.Length; i++)
        {
            doublebetdata[i] = doubledata[i];
        }
        for (int i = 0; i < triplekeydata.Count; i++)
        {
            triplebetdata.Add(triplekeydata[i]);
            triplevaldata.Add(triplemoneydata[i]);
            
        }
        deviceid = phone;
    }
}
[System.Serializable]
public class RouletteData
{
    public int winvalue;
    public  List<int> Straightstore = new List<int>();
    public  List<int> Straightvaluestore= new List<int>();
    public  List<string> Splitstore= new List<string>();
    public  List<int> Splitvaluestore= new List<int>();
    public  List<string> Streetstore= new List<string>();
    public  List<int> Streetvaluestore= new List<int>();
    public  List<string> Cornerstore= new List<string>();
    public  List<int> Cornervaluestore= new List<int>();
    public  List<string> Linestore= new List<string>();
    public  List<int> LineValuestore= new List<int>();
    public  int dozen01betstore;
    public  int dozen02betstore;
    public  int dozen03betstore;
    public  int onetoeighteenstore;
    public  int nineteentothirtysixstore;
    public  int column01store;
    public  int column02store;
    public  int column03store;
    public  int redstore;
    public  int blackstore;
    public  int evenstore;
    public  int oddstore;
    public int totalstore;
    public string iddetails;
    public int addedinfile;
    public int roundstore;

    
    public RouletteData(int winnervalues,List<int> straightbet,List<int>Straightvalue,List<string>Splitbet,List<int>Splitvalue,List<string>Streetbet,List<int>Streetvalue,List<string>Cornerbet,List<int>CornerValue,List<string>Linebet,List<int>Linevalue,int dozen01,int dozen02,int dozen03,int onetoeighteen,int nineteentothirtysix,int column01,int column02,int column03,int red,int black,int even,int odd,string deviceid,int addedfilein,int totalbets,int round)
    {
        winvalue =winnervalues;
        for (int i = 0; i < straightbet.Count; i++)
        {
            Straightstore.Add(straightbet[i]);
            Straightvaluestore.Add(Straightvalue[i]);
        }
        for (int i = 0; i < Splitbet.Count; i++)
        {
            Splitstore.Add(Splitbet[i]);
            Splitvaluestore.Add(Splitvalue[i]);
        }
        for (int i = 0; i < Streetbet.Count; i++)
        {
            Streetstore.Add(Streetbet[i]);
            Streetvaluestore.Add(Streetvalue[i]);
        }
        for (int i = 0; i < Cornerbet.Count; i++)
        {
            Cornerstore.Add(Cornerbet[i]);
            Cornervaluestore.Add(CornerValue[i]);
        }
        for (int i = 0; i < Linebet.Count; i++)
        {
            Linestore.Add(Linebet[i]);
            LineValuestore.Add(Linevalue[i]);
        }

        dozen01betstore = dozen01;
        dozen02betstore= dozen02;
        dozen03betstore= dozen03;
        onetoeighteenstore= onetoeighteen;
        nineteentothirtysixstore= nineteentothirtysix;
        column01store = column01;
        column02store = column02;
        column03store= column03;
        redstore =  red;
        blackstore = black;
        evenstore = even;
        oddstore = odd;
        iddetails = deviceid;
        addedinfile = addedfilein;
        roundstore = round;
    }   
}
[System.Serializable]
public class Andarbets
{
    public string devicedetails;
    public int winamount;
    public int A_binary;
    public int twob_binary;
    public int threeb_binary;
    public int fourb_binary;
    public int fiveb_binary;
    public int sixb_binary;
    public int sevenb_binary;
    public int eightb_binary;
    public int nineb_binary;
    public int tenb_binary;
    public int jb_binary;
    public int qb_binary;
    public int kb_binary;
    public int heartb_binary;

    public int diamondb_binary;

    public int spadeb_binary;

    public int cloverb_binary;
    public int redb_binary;

    public int blackb_binary;
    public int atosix_binary;
    public int seven_binary;
    public int eighttok_binary;
    public int andar_binary;
    public int bahar_binary;
    public Andarbets(string phone,int winvalues,int A,int two,int three,int four,int five,int six,int seven,int eight,int nine,int ten,int jack,int queen,int king,int dianmond,int spade,int clover,int heart,int red, int black,int atosix,int sevenonly,int eighttok,int andar,int bahar)
    {
        devicedetails = phone;
        winamount = winvalues;
        A_binary =A;
        twob_binary = two;
        threeb_binary = three;
        fourb_binary = four;
        fiveb_binary =five;
        sixb_binary = six;
        seven_binary = seven;
        eightb_binary = eight;
        nineb_binary = nine;
        tenb_binary = ten;
        jb_binary = jack;
        qb_binary = queen;
        kb_binary = king;
        diamondb_binary = dianmond;
        spadeb_binary = spade;
        heartb_binary = heart;
        cloverb_binary = clover;
        atosix_binary = atosix;
        seven_binary = sevenonly;
        eighttok_binary = eighttok;
        andar_binary =andar;
        bahar_binary = bahar;

    }
}
[System.Serializable]
public class sevenup
{
    public int winvalue;
    public sevenup(int winamount)
    {
        winvalue = winamount;
    }
}

