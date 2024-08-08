using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Shared;
using UnityEngine;

namespace AndarBahar.Utility
{
    public class Fuction
    {
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
    public static class Events
    {
        internal static string OnChipMove = "OnChipMove";
        internal static string OnTest = "test";
        internal static string OnPlayerExit = "OnPlayerExit";
        internal static string OnJoinRoom = "OnJoinRoom";
        internal static string OnTimeUp = "OnTimeUp";
        internal static string OnWait = "OnWait";
        internal static string OnTimerStart = "OnTimerStart";
        internal static string OnDrawCompleted = "OnDrawCompleted";
        internal static string RegisterPlayer = "RegisterPlayer";
        internal static string OnGameStart = "OnGameStart";
        internal static string OnAddNewPlayer = "OnAddNewPlayer";
        internal static string OnCurrentTimer = "OnCurrentTimer";
        internal static string OnBetsPlaced = "OnBetsPlaced";
        internal static string OnWinNo = "OnWinNo";
        public const string OnWinAmount = "OnWinAmount";
        internal static string OnBotsData = "OnBotsData";
        internal static string OnPlayerWin = "OnPlayerWin";
        internal static string onleaveRoom = "onleaveRoom";
        internal static string OnHistoryRecord = "OnHistoryRecord";
    }
    public enum Cards
    {
        Hearts = 0,
        Speads = 1,
        Clubs = 2,
        Diamonds = 3,
    }
    enum State
    {
        Betting,
        Drawing,
        Idle,

    }
    enum AndarBahar
    {
        Andar = 0,
        Bahar = 1
    }
   public enum gameState
    {
        canBet,
        cannotBet,
        wait
    }

    public class OnChipMove
    {
        public string playerId;
        public Chip chip;
        public Spots spot;
        public Vector3 position;
    }
    public enum Spots
    {
        Andar = 22,
        Bahar = 23,
        A = 0, _2 = 1,_3 = 2,_4 = 3,_5 = 4,_6 = 5,_7 = 6,_8 = 7,_9 = 8,_10 = 9,J = 10,Q = 11,K = 12,Diamond = 13,Spade = 14,Club = 15,Heart = 16,Red = 17,Black = 18,A_6 = 19,Seven = 20,_8_k = 21
    }
    [Serializable]
    public class Bot
    {
        public Spots spot;
        public Chip chip;
        public Vector3 position;
    }
    [Serializable]
    public class WinnerCard
    {
        public int Joker_Card_No;
        public int Joker_Card_Type;
    }
    [Serializable]
    public class Win
    {
        public float winAmount;
    }
    public class Card
    {
        public Cards cardName;
        public int cardNo;
    }

    public class DisplayCard
    {
        public int card ;
        public int type;
    }

    public class HistoryCard
    {
        public int joker_card_no;
        public int winSpot;
    }

   

    public class DrawResultData
    {
        public List<int> winningSpot;
        public List<DisplayCard> displayCard;
        public List<int> previousWins;
        public List<HistoryCard> historyCards;
        public List<int> historyPercent;
        public List<int> pridictionPercent;
        public List<BotsBets> botsBetsDetails;
        public int RandomWinAmount;
    }

    public class BotsBets
    {
        public string name;
        public int Andar;
        public int Bahar;
        //public int oneToFive;
        //public int sixToTen;
        //public int elevenToFifteen;
        //public int sixteenToTwentyFive;
        //public int twentySixToThirty;
        //public int thirtyOneToThirtyFive;
        //public int thirtySixToFouty;
        //public int fortyOneAndMore;
        public double balance;
        public int win;
        public int avatarNumber;
    }
}
