using UnityEngine;
using System;
using System.Collections.Generic;
public class lastrecord 
{
    
    public static bool funtargetlas;
    public static bool Triplelas;
    public static bool Roulettelas;
    public static bool andarlas;
    public static bool sevenlas;
    
}
public class Funtargetlast
{
    //public static int[] funtargetbet;
    public static int winningAmount;
    public static int[] funtargetvalue = new int[10]; 
}
public class Triplelast
{
    //public static int[] singleval;
    public static int winamount;
    public static int[] singlebet = new int[10];
    //public static int[] doubleval;
    public static int[] doublebet = new int[210];
    public static List<int> triplebet = new List<int>();
    public static List<int> tripleval = new List<int>();
    public static string playerid;
}
[System.Serializable]
public class Roulettelast
{
   public static int winnervalue;
   public static int addedinfile;
   public static string deviceid;
   public static int round;
    public static List<int> Straightbetlast = new List<int>();
    public static List<int> StraightValuelast= new List<int>();
    public static List<string> Splittbetlast= new List<string>();
    public static List<int> SplitValuelast= new List<int>();
    public static List<string> Streetbetlast= new List<string>();
    public static List<int> StreetValuelast= new List<int>();
    public static List<string> CornerBetlast= new List<string>();
    public static List<int> CornerValuelast= new List<int>();
    public static List<string> Linebetlast= new List<string>();
    public static List<int> LineValuelast= new List<int>();
    public static List<int> Columnbetlast;
    public static List<int> ColumnValuelast;
    public static List<int> Specificbetlast;
    public static List<int> SpecificValuelast;
    public static int dozen01betlast;
    public static int dozen02betlast;
    public static int dozen03betlast;
    public static int onetoeighteen;
    public static int nineteentothirtysix;
    public static int column01;
    public static int column02;
    public static int column03;
    public static int red;
    public static int black;
    public static int even;
    public static int odd;
    public static int totalbets;


}
public class AndarBaharlast
{
    public static string deviceid;
    public static int winamount;
    public static int A;
    public static int twob;
    public static int threeb;
    public static int fourb;
    public static int fiveb;
    public static int sixb;
    public static int sevenb;
    public static int eightb;
    public static int nineb;
    public static int tenb;
    public static int jb;
    public static int qb;
    public static int kb;
    public static int heartb;

    public static int diamondb;

    public static int spadeb;

    public static int cloverb;
    public static int redb;

    public static int blackb;
    public static int atosix;
    public static int seven;
    public static int eighttok;
    public static int andar;
    public static int bahar;
}
public class Sevenuplast
{
    public static int winbalance;
}
