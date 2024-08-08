﻿using UpDown7.Utility;
using System.Collections;
using UnityEngine;
using Shared;
using SocketIO;

namespace Updown7.ServerStuff
{
    class ServerRequest : SocketHandler
    {
        public static ServerRequest instance;
        public void Awake()
        {
            socket = SocketIOComponent.instance;
            instance = this;
        }
        void Start()
        {
            //JoinGame();
        }
        public void JoinGame()
        {
            Debug.Log($"player { UserDetail.playerId_Local} Join game");

            Player player = new Player()
            {
                balance = UserDetail.player_Local_Balance,
                playerId = UserDetail.playerId_Local,
                // profilePic = LocalPlayer.profilePic,
                gameId="1"

            };
            socket.Emit(Events.RegisterPlayer, new JSONObject(JsonUtility.ToJson(player)));
        } 
        public void OnChipMove(Vector3 position, Chip chip,Spot spot)
        {
            OnChipMove Obj = new OnChipMove()
            {
                position = position,
                playerId = UserDetail.UserId.ToString(),
                chip = chip,
                spot=spot
            };
            Debug.Log("bet " + JsonUtility.ToJson(Obj));
            socket.Emit(Events.OnChipMove, new JSONObject(JsonUtility.ToJson(Obj)));
        } 
        public void OnTest()
        {
            socket.Emit(Events.OnTest);
        }
        public void onleaveRoom()
        {
            socket.Emit(Events.onleaveRoom);
        }
    }
}
