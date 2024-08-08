using SocketIO;
using System.Collections;
using UnityEngine;
using Titli.Utility;
using Shared;

namespace Titli.ServerStuff
{
    public class Titli_ServerRequest : Titli_SocketHandler
    {
        public static Titli_ServerRequest instance;
        public void Awake()
        {
            socket = GameObject.Find("SocketIOComponents").GetComponent<SocketIOComponent>();
            instance = this;

        }
        public void JoinGame()
        {
            
            Debug.Log($"player { PlayerPrefs.GetString("email") } Join game");

            Player player = new Player()
            {
                balance = PlayerPrefs.GetString("Points"),
                playerId = PlayerPrefs.GetString("email"),
                gameId = "5"
            };
            socket.Emit(Utility.Events.RegisterPlayer, new JSONObject(JsonUtility.ToJson(player)));
            Debug.Log("player has registered here");
        }//profilePic = LocalPlayer.profilePic,

        public void OnChipMove(Vector3 position, Chip chip, Spot spot)
        {
            OnChipMove Obj = new OnChipMove()
            {
                position = position,
                playerId = UserDetail.UserId.ToString(),
                chip = chip,
                spot = spot
            };
            //socket.Emit(Events.OnChipMove, new JSONObject(JsonUtility.ToJson(Obj)));
        }
        public void OnTest()
        {
            //socket.Emit(Events.OnTest);
        }       
        public void OnHistoryRecordGame()
        {
            //socket.Emit(Events.OnHistoryRecord);
        }
    }
}
