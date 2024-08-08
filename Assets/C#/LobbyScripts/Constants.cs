using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;

namespace LobbyScripts
{
  public class Constants
  {
    public static  string BaseURL = "https://jeetogame.in/jeeto_game/WebServices/";
    public static  string LoginURL = BaseURL+"loginPassword";
    public static  string GuestURL = BaseURL + "Guestlogin";
    public static  string UpdateProfileURL = BaseURL + "updateProfile";
    public static  string ProfileURL = BaseURL + "profile";
    public static  string RankURL = BaseURL + "myRank";
    public static  string SafeLockerURL = BaseURL + "safeLocker";
    public static string AddDiamondURL = BaseURL + "AddDiamond";
    public static string DiamondOfferURL = BaseURL + "DiamondOfferDatails";

    // Data Keys
    public const string EMAIL_DATA_KEY = "EMAIL";
    public const string PASSWORD_DATA_KEY = "PASSWORD";
    public static readonly string IS_INVALID_USER = "401";

    public const string PASSWORD_NOT_MATCHED = "Password not matched!\nPlease enter same Password and retry.";
    public const string PASSWORD_UPDATE_COMPLETE = "Password Changed Successfully.";


    public const string RegisterPlayer = "RegisterPlayer";
    public const string OnLogin = "OnLogin";
    public const string OnPlaceBet = "OnPlaceBet";
    public const string OnWinNo = "OnWinNo";
    public const string OnWinAmount = "OnWinAmount";
    public const string OnTakeAmount = "OnTakeAmount";
    public const string OnChipMove = "OnChipMove";
    public const string OnPlayerExit = "OnPlayerExit";
    public const string OnJoinRoom = "OnJoinRoom";
    public const string OnTimeUp = "OnTimeUp";
    public const string OnTimerStart = "OnTimerStart";
    public const string OnDrawCompleted = "OnDrawCompleted";
    public const string OnWait = "OnWait";
    public const string OnGameStart = "OnGameStart";
    public const string OnAddNewPlayer = "OnAddNewPlayer";
    public const string OnCurrentTimer = "OnCurrentTimer";
    public const string OnBetsPlaced = "OnBetsPlaced";
    public const string OnBotsData = "OnBotsData";
    public const string OnPlayerWin = "OnPlayerWin";
    public const string onEnterLobby = "onEnterLobby";
    public const string onleaveRoom = "onleaveRoom";
    public const string OnForceLogin = "OnForceLogin";
    public const string OnTimer = "OnTimer";
    public const string OnDissconnect = "disconnect";
    public const string OnUserInfo = "OnUserInfo";
    public const string OnChangePassword = "OnChangePassword";
    public const string OnNotification = "OnNotification";
    public const string OnsenderNotification = "OnsenderNotification";
    public const string OnSendPoints = "OnSendPoints";
    public const string OnAcceptPoints = "OnAcceptPoints";
    public const string OnRejectPoints = "OnRejectPoints";
    public const string OnReturnPoints = "OnReturnPoints";
    public const string OnUserProfile = "OnUserProfile";
    public const string OnLogout = "OnLogout";
    public const string OnPreBet = "OnPreBet";
    public static T GetObjectOfType<T>(object json) where T : class
    {
      T t = null;
      try
      {
          t = JsonConvert.DeserializeObject<T>(json.ToString());
          return t;
      }
      catch (Exception e)
      {
          Debug.Log(e.Message);
      }
      return t;
    }
  }

  public enum Games
  {
    funWheel = 3,
    roulette = 2
  }
  public class BackEndData<T> where T : class
  {
    public string status;
    public string message;
    public T data;
  }
  public class BackEndData2<T> where T : class
  {
    public bool status;
    public string message;
    public T data;
  }  
  public class BackEndData3<T> where T : class
  {
    public int status;
    public string message;
    public T data;
  }

}
