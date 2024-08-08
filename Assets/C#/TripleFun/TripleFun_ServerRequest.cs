using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketIO;
using Shared;
using AndarBahar.Utility;
using TFU;

namespace TripleFun.ServerStuff
{
    public class TripleFun_ServerRequest : MonoBehaviour
    {

        public static TripleFun_ServerRequest instance;
        public SocketIOComponent socket;

        IEnumerator Start()
        {
            instance = this;
             socket = SocketIOComponent.instance;

            yield return new WaitForSeconds(2f);
            // JoinGame();
        }
        void JoinGame()
        {
            Debug.Log("Join");
            Player player = new Player()
            {
                balance = LocalPlayer.balance,
                playerId = UserDetail.UserId.ToString(),
                profilePic = LocalPlayer.profilePic,
                gameId = "6"
            };
            socket.Emit(Triple_Util.TF_Events.RegisterPlayer, new JSONObject(JsonUtility.ToJson(player)));
        }
    }
}
