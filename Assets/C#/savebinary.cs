using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
using System.Data.Common;

public static class savebinary
{
    public static List<int> roulettenull = new List<int>();
    public static List<string> roulettestrnull = new List<string>();
    
    public static void savefunctionfuntarget()
    {
        //int[] something = {2,3,4,5,6,7,8,6,5};
        PlayerData funrep = new PlayerData(Funtargetlast.funtargetvalue,Funtargetlast.winningAmount);
        //TriplefunData tripledata = new TriplefunData(Triplelast.singlebet,Triplelast.doublebet,Triplelast.triplebet,Triplelast.tripleval);
        //RouletteData roulettedeta = new RouletteData(Roulettelast.Straightbetlast,Roulettelast.StraightValuelast,Roulettelast.Splittbetlast,Roulettelast.SplitValuelast,Roulettelast.Streetbetlast,Roulettelast.StreetValuelast,Roulettelast.CornerBetlast,
        //Roulettelast.CornerValuelast,Roulettelast.Linebetlast,Roulettelast.LineValuelast,Roulettelast.column01,Roulettelast.column02,Roulettelast.column03,Roulettelast.dozen01betlast,Roulettelast.dozen02betlast,Roulettelast.dozen03betlast,Roulettelast.onetoeighteen,Roulettelast.nineteentothirtysix,Roulettelast.red,Roulettelast.black,Roulettelast.even,Roulettelast.odd);
        BinaryFormatter bf = new BinaryFormatter();
        string id = PlayerPrefs.GetString("email");
        Debug.Log("the user has logged in with this id" + id);
        string path = Application.persistentDataPath + "/funtarget.nms";
        FileStream stream = new FileStream(path,FileMode.Create);
        //Debug.Log("saving values"+ funrep.funtarget_previous+funrep.funtargetwinning);
        //PlayerData data = new PlayerData(PlayerPrefs.GetString("email"),PlayerPrefs.GetString("password"));
        bf.Serialize(stream,funrep);

        stream.Close();
    }
    public static PlayerData LoadPlayerfuntarget()
    {
        string path = Application.persistentDataPath + "/funtarget.nms";
        if(File.Exists(path))
        {
            //Debug.LogError("file exist");
            try
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream stream = new FileStream(path,FileMode.Open);
                //Debug.LogError("Fileopened");
               
                PlayerData data =   bf.Deserialize(stream) as PlayerData;
                stream.Close(); 
                readfrombinary.funtarget(data.funtarget_previous,data.funtargetwinning);
                // or (int i = 0; i < data.Straightvaluestore.Count; i++)
                // {
                //     Debug.LogError("loaded value :" + data.Straightvaluestore[i]);
                // }
                // for (int i = 0; i < data.Linestore.Count; i++)
                // {
                //     Debug.LogError("loaded value :" + data.Linestore[i]+ "Linevalues " + data.LineValuestore[i]);
                // }
                // Debug.Log(data.evenstore);f
    
                return data;
            }
            catch (System.Exception e)
            {
                Debug.Log("///////////////////////////////////" + e);
                return null;
                
            }

        }
        else
        {
            Debug.Log("SaveFileNOtFOund in " + path);
            return null;
        }
    }
        public static void savefunctiontriple()
    {
        int[] something = {2,3,4,5,6,7,8,6,5};
        //PlayerData funrep = new PlayerData(Funtargetlast.funtargetvalue,Funtargetlast.winningAmount);
        TriplefunData tripledata = new TriplefunData(Triplelast.winamount,Triplelast.singlebet,Triplelast.doublebet,Triplelast.triplebet,Triplelast.tripleval,Triplelast.playerid);
        //RouletteData roulettedeta = new RouletteData(Roulettelast.Straightbetlast,Roulettelast.StraightValuelast,Roulettelast.Splittbetlast,Roulettelast.SplitValuelast,Roulettelast.Streetbetlast,Roulettelast.StreetValuelast,Roulettelast.CornerBetlast,
        //Roulettelast.CornerValuelast,Roulettelast.Linebetlast,Roulettelast.LineValuelast,Roulettelast.column01,Roulettelast.column02,Roulettelast.column03,Roulettelast.dozen01betlast,Roulettelast.dozen02betlast,Roulettelast.dozen03betlast,Roulettelast.onetoeighteen,Roulettelast.nineteentothirtysix,Roulettelast.red,Roulettelast.black,Roulettelast.even,Roulettelast.odd);
        BinaryFormatter bf = new BinaryFormatter();
        string id = PlayerPrefs.GetString("email");
        string path = Application.persistentDataPath +"/"+ id+"Triplefun.nms";
        FileStream stream = new FileStream(path,FileMode.Create);
        //Debug.Log("saving values"+ funrep.funtarget_previous+funrep.funtargetwinning);
        //PlayerData data = new PlayerData(PlayerPrefs.GetString("email"),PlayerPrefs.GetString("password"));
        bf.Serialize(stream,tripledata);

        stream.Close();
    }
    
    public static TriplefunData LoadPlayertriple()
    {
        string id = PlayerPrefs.GetString("email");
        string path = Application.persistentDataPath +"/"+ id+"Triplefun.nms";
        if(File.Exists(path))
        {
            //Debug.LogError("file exist");
            try
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream stream = new FileStream(path,FileMode.Open);
                //Debug.LogError("Fileopened");
               
                TriplefunData data =   bf.Deserialize(stream) as TriplefunData;
                stream.Close();
                //Debug.LogError("this is deviceid " + data.deviceid);
                if(data.deviceid == PlayerPrefs.GetString("email"))
                {
                    
                    readfrombinary.triplefun(data.winamount,data.singlebetdata,data.doublebetdata,data.triplebetdata,data.triplevaldata,data.deviceid);
                }
                else
                {
                    readfrombinary.triplefun(0,new int[10],new int[100],new List<int>(),new List<int>(),data.deviceid);
                } 
                
                // or (int i = 0; i < data.Straightvaluestore.Count; i++)
                // {
                //     Debug.LogError("loaded value :" + data.Straightvaluestore[i]);
                // }
                // for (int i = 0; i < data.Linestore.Count; i++)
                // {
                //     Debug.LogError("loaded value :" + data.Linestore[i]+ "Linevalues " + data.LineValuestore[i]);
                // }
                // Debug.Log(data.evenstore);f
    
                return data;
            }
            catch (System.Exception e)
            {
                Debug.Log("///////////////////////////////////" + e);
                return null;
                
            }

        }
        else
        {
            Debug.Log("SaveFileNOtFOund in " + path);
            return null;
        }
    }

    public static void savefunctionroulette()
    {
        int[] something = {2,3,4,5,6,7,8,6,5};
        //PlayerData funrep = new PlayerData(Funtargetlast.funtargetvalue,Funtargetlast.winningAmount);
        //TriplefunData tripledata = new TriplefunData(Triplelast.singlebet,Triplelast.doublebet,Triplelast.triplebet,Triplelast.tripleval);
        RouletteData roulettedeta = new RouletteData(Roulettelast.winnervalue,Roulettelast.Straightbetlast,Roulettelast.StraightValuelast,Roulettelast.Splittbetlast,Roulettelast.SplitValuelast,Roulettelast.Streetbetlast,Roulettelast.StreetValuelast,Roulettelast.CornerBetlast,
        Roulettelast.CornerValuelast,Roulettelast.Linebetlast,Roulettelast.LineValuelast,Roulettelast.dozen01betlast,Roulettelast.dozen02betlast,Roulettelast.dozen03betlast,Roulettelast.onetoeighteen,Roulettelast.nineteentothirtysix,Roulettelast.column01,Roulettelast.column02,Roulettelast.column03,Roulettelast.red,Roulettelast.black,Roulettelast.even,Roulettelast.odd,Roulettelast.deviceid,Roulettelast.addedinfile,Roulettelast.totalbets,Roulettelast.round);
        
        BinaryFormatter bf = new BinaryFormatter();
        string path = Application.persistentDataPath + "/roulette.nms";
        FileStream stream = new FileStream(path,FileMode.Create);
        //Debug.Log("saving values"+ funrep.funtarget_previous+funrep.funtargetwinning);
        //PlayerData data = new PlayerData(PlayerPrefs.GetString("email"),PlayerPrefs.GetString("password"));
        bf.Serialize(stream,roulettedeta);

        stream.Close();
    }
    
    public static void LoadPlayerroulette()
    {
        string path = Application.persistentDataPath + "/roulette.nms";
        if(File.Exists(path))
        {
            //Debug.LogError("file exist");
            try
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream stream = new FileStream(path,FileMode.Open);
                //Debug.LogError("Fileopened");
               
                RouletteData data =   bf.Deserialize(stream) as RouletteData;
                stream.Close(); 
                //Debug.LogError(data.iddetails);
                //Debug.LogError("this is in player prefs "+PlayerPrefs.GetString("email"));
                
                // if(data.iddetails == PlayerPrefs.GetString("email"))
                // {
                //     //Debug.LogError("Same account on the device");
                //     readfrombinary.roulettesave(data.winvalue,data.Straightstore,data.Straightvaluestore,data.Splitstore,data.Splitvaluestore,data.Streetstore,data.Streetvaluestore,data.Cornerstore,data.Cornervaluestore,data.Linestore,data.LineValuestore,data.dozen01betstore,data.dozen02betstore,data.dozen03betstore,data.onetoeighteenstore,data.nineteentothirtysixstore,data.column01store,data.column02store,data.column03store,data.redstore,data.blackstore,data.evenstore,data.oddstore);
                // }
                // else
                // {
                //     //Debug.LogError("Different account on the device");
                //     // File.Delete(path);
                //     // return;
                //     readfrombinary.roulettesave(0,roulettenull,roulettenull,new List<string>(),roulettenull,new List<string>(),roulettenull,new List<string>(),roulettenull,new List<string>(),roulettenull,0,0,0,0,0,0,0,0,0,0,0,0);
                //     //readfrombinary.roulettesave(0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0);
                //     //return data;
                    
                // }
                readfrombinary.roulettesave(data.winvalue,data.Straightstore,data.Straightvaluestore,data.Splitstore,data.Splitvaluestore,data.Streetstore,data.Streetvaluestore,data.Cornerstore,data.Cornervaluestore,data.Linestore,data.LineValuestore,data.dozen01betstore,data.dozen02betstore,data.dozen03betstore,data.onetoeighteenstore,data.nineteentothirtysixstore,data.column01store,data.column02store,data.column03store,data.redstore,data.blackstore,data.evenstore,data.oddstore,data.roundstore);

                
                // or (int i = 0; i < data.Straightvaluestore.Count; i++)
                // {
                //     Debug.LogError("loaded value :" + data.Straightvaluestore[i]);
                // }
                // for (int i = 0; i < data.Linestore.Count; i++)
                // {
                //     Debug.LogError("loaded value :" + data.Linestore[i]+ "Linevalues " + data.LineValuestore[i]);
                // }
                // Debug.Log(data.evenstore);f
    
                // return data;
            }
            catch (System.Exception e)
            {
                Debug.Log("///////////////////////////////////" + e);
                // return null;
                
            }

        }
        else
        {
            Debug.Log("SaveFileNOtFOund in " + path);
            // return null;
        }
    }

    public static void savefunctionandar()
    {
        Andarbets andarbets = new Andarbets(Andarwithin.deviceid,AndarBaharlast.winamount,AndarBaharlast.A,AndarBaharlast.twob,AndarBaharlast.threeb,AndarBaharlast.fourb,AndarBaharlast.fiveb,AndarBaharlast.sixb,AndarBaharlast.sevenb,AndarBaharlast.eightb,AndarBaharlast.nineb,AndarBaharlast.tenb,AndarBaharlast.jb,AndarBaharlast.qb,AndarBaharlast.kb,AndarBaharlast.diamondb,AndarBaharlast.spadeb,AndarBaharlast.cloverb,AndarBaharlast.heartb,AndarBaharlast.redb,AndarBaharlast.blackb,AndarBaharlast.atosix,AndarBaharlast.seven,AndarBaharlast.eighttok,AndarBaharlast.andar,AndarBaharlast.bahar);
        BinaryFormatter bf = new BinaryFormatter();
        string path = Application.persistentDataPath + "/andar.nms";
        FileStream stream = new FileStream(path,FileMode.Create);
        //Debug.Log("saving values"+ funrep.funtarget_previous+funrep.funtargetwinning);
        //PlayerData data = new PlayerData(PlayerPrefs.GetString("email"),PlayerPrefs.GetString("password"));
        bf.Serialize(stream,andarbets);

        stream.Close();
    }

    public static Andarbets LoadPlayerAndar()
    {
        string path = Application.persistentDataPath + "/andar.nms";
        if(File.Exists(path))
        {
            //Debug.LogError("file exist");
            try
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream stream = new FileStream(path,FileMode.Open);
                //Debug.LogError("Fileopened");
               
                Andarbets data =   bf.Deserialize(stream) as Andarbets;
                stream.Close(); 
                if(data.devicedetails == PlayerPrefs.GetString("email"))
                {
                    readfrombinary.andarsave(data.devicedetails,data.winamount,data.A_binary,data.twob_binary,data.threeb_binary,data.fourb_binary,data.fiveb_binary,data.sixb_binary,data.sevenb_binary,data.eightb_binary,data.nineb_binary,data.tenb_binary,data.jb_binary,data.qb_binary,data.kb_binary,data.diamondb_binary,data.spadeb_binary,data.cloverb_binary,data.heartb_binary,data.redb_binary,data.blackb_binary,data.atosix_binary,data.seven_binary,data.eighttok_binary,data.andar_binary,data.bahar_binary);
                }
                else
                {
                    readfrombinary.andarsave(data.devicedetails,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0);
                }
                
                
    
                return data;
            }
            catch (System.Exception e)
            {
                Debug.Log("///////////////////////////////////" + e);
                return null;
                
            }

        }
        else
        {
            Debug.Log("SaveFileNOtFOund in " + path);
            return null;
        }
    }
    public static void savefunctionseven()
    {
        sevenup sevenupdown = new sevenup(Sevenuplast.winbalance);
        BinaryFormatter bf = new BinaryFormatter();
        string path = Application.persistentDataPath + "/seven.nms";
        FileStream stream = new FileStream(path,FileMode.Create);
        //Debug.Log("saving values"+ funrep.funtarget_previous+funrep.funtargetwinning);
        //PlayerData data = new PlayerData(PlayerPrefs.GetString("email"),PlayerPrefs.GetString("password"));
        bf.Serialize(stream,sevenupdown);

        stream.Close();
    }

    public static sevenup LoadPlayerSeven()
    {
        string path = Application.persistentDataPath + "/seven.nms";
        if(File.Exists(path))
        {
            //Debug.LogError("file exist");
            try
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream stream = new FileStream(path,FileMode.Open);
                //Debug.LogError("Fileopened");
               
                sevenup data =   bf.Deserialize(stream) as sevenup;
                stream.Close(); 

                readfrombinary.sevenuplast(data.winvalue);
                
    
                return data;
            }
            catch (System.Exception e)
            {
                Debug.Log("///////////////////////////////////" + e);
                return null;
                
            }

        }
        else
        {
            Debug.Log("SaveFileNOtFOund in " + path);
            return null;
        }
    }


}   
