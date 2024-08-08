using UnityEngine;
using SocketIO;

namespace RouletteScripts.ServerStuff
{
    public class Roulette_SocketHandler : MonoBehaviour
    {
        protected bool isConnected;
        public SocketIOComponent socket;
    }
}
