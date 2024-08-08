using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace TFU
{

    public class Triple_Util
    {
        public static class TF_Events
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
            internal static string OnBotsData = "OnBotsData";
            internal static string OnPlayerWin = "OnPlayerWin";
            internal static string onleaveRoom = "onleaveRoom";
            public const string OnWinAmount = "OnWinAmount";
        }
    }


    //interface IChipMovement
    //{
    //    void OnOtherPlayerMove(object data);
    //    void CreateBotsChips(ChipDate data);
    //}
    interface ITimer
    {
        void OnTimerStart(object data);
        void OnTimeUp(object data);
        void OnWait(object data);
        void OnCurrentTime(object data);
    }

    interface IBot
    {
        void ChipCreator(int dataNo);
    }

}