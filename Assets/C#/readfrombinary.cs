using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class readfrombinary : MonoBehaviour
{
    
    public static void funtarget(int[] array, int number)
    {
        for (int i = 0; i < array.Length; i++)
        {
            Funtargetlast.funtargetvalue[i] = array[i];
            
        }
        //Debug.Log("Aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa"+number);
        Funtargetlast.winningAmount = number;
        lastrecord.funtargetlas = true;
    }
    public static void triplefun(int value,int[] single,int[] doubleb, List<int> triplekey, List<int> triplevalue,string phone)
    {
        Triplelast.winamount = value;
        for (int i = 0; i < single.Length; i++)
        {
            Triplelast.singlebet[i] = single[i];
        }
        for (int i = 0; i < doubleb.Length; i++)
        {
            Triplelast.doublebet[i] = doubleb[i];
        }
        for (int i = 0; i < triplekey.Count; i++)
        {
            Triplelast.triplebet.Add(triplekey[i]);
            Triplelast.tripleval.Add(triplevalue[i]);
        }
        Triplelast.playerid = phone;
        lastrecord.Triplelas = true;
    }
    public static void roulettesave(int value,List<int> stbet,List<int>Stvalue,List<string>Spbet,List<int>Spvalue,List<string>Strbet,List<int>Strvalue,List<string>Cbet,List<int>CValue,List<string>Lbet,List<int>Lvalue,int dzn01,int dzn02,int dzn03,int oneeighteen,int nineteenthirtysix,int clmn01,int clmn02,int clmn03,int rd,int blck,int evn,int od, int round)
    {
        //Debug.LogError("the data has been breached");
        Roulettelast.winnervalue = value;
        for (int i = 0; i < stbet.Count; i++)
        {
            Roulettelast.Straightbetlast.Add(stbet[i]);
            Roulettelast.StraightValuelast.Add(Stvalue[i]);
                    
        }
        for (int i = 0; i < Spbet.Count; i++)
        {
            Roulettelast.Splittbetlast.Add(Spbet[i]);
            Roulettelast.SplitValuelast.Add(Spvalue[i]);
                    
        }
        for (int i = 0; i < Strbet.Count; i++)
        {
            Roulettelast.Streetbetlast.Add(Strbet[i]);
            Roulettelast.StreetValuelast.Add(Strvalue[i]);
                    
        }
        for (int i = 0; i < Cbet.Count; i++)
        {
            Roulettelast.CornerBetlast.Add(Cbet[i]);
            Roulettelast.CornerValuelast.Add(CValue[i]);
                    
        }
        for (int i = 0; i < Lbet.Count; i++)
        {
            Roulettelast.Linebetlast.Add(Lbet[i]);
            Roulettelast.LineValuelast.Add(Lvalue[i]);
        }
        Roulettelast.column01 = clmn01;
        Roulettelast.column02 = clmn02;
        Roulettelast.column03 = clmn03;
        Roulettelast.dozen01betlast = dzn01;
        Roulettelast.dozen02betlast = dzn02;
        Roulettelast.dozen03betlast = dzn03;
        Roulettelast.onetoeighteen = oneeighteen;
        Roulettelast.nineteentothirtysix = nineteenthirtysix;
        Roulettelast.red = rd;
        Roulettelast.black = blck;
        Roulettelast.even = evn;
        Roulettelast.odd = od;
        Roulettelast.round =round;
        lastrecord.Roulettelas = true;
    }
    public static void andarsave(string phone,int value,int A,int two,int three,int four,int five,int six,int seven,int eight,int nine,int ten,int jack,int queen,int king,int dianmond,int spade,int clover,int heart,int red, int black,int atosix,int sevenonly,int eighttok,int andar,int bahar)
    {
        AndarBaharlast.deviceid = phone;
        AndarBaharlast.winamount = value;
        AndarBaharlast.A =A;
        AndarBaharlast.twob =two;
        AndarBaharlast.threeb = three;
        AndarBaharlast.fourb =four;
        AndarBaharlast.fiveb = five;
        AndarBaharlast.sixb = six;
        AndarBaharlast.sevenb = seven;
        AndarBaharlast.eightb = eight;
        AndarBaharlast.nineb = nine;
        AndarBaharlast.tenb = ten;
        AndarBaharlast.jb = jack;
        AndarBaharlast.qb = queen;
        AndarBaharlast.kb = king;
        AndarBaharlast.diamondb =dianmond;
        AndarBaharlast.heartb = heart;
        AndarBaharlast.spadeb = spade;
        AndarBaharlast.cloverb = clover;
        AndarBaharlast.redb = red;
        AndarBaharlast.blackb =black;
        AndarBaharlast.atosix = atosix;
        AndarBaharlast.seven = seven;
        AndarBaharlast.eighttok = eighttok;
        AndarBaharlast.andar = andar;
        AndarBaharlast.bahar = bahar;
        
    }
    public static void sevenuplast(int value)
    {
        Sevenuplast.winbalance = value;
    }
}
