using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Titli.Utility;
using Shared;

namespace Titli.Gameplay
{
    public class Titli_BetManager : MonoBehaviour
    {
        public static Titli_BetManager Instance;
            Dictionary<Spots, int> betHolder = new Dictionary<Spots, int>();
            private void Awake()
            {
                Instance = this;
            }
            private void Start()
            {
                betHolder.Add(Spots.Umbrella, 0);
                betHolder.Add(Spots.Goat, 0);
                betHolder.Add(Spots.Pigeon, 0);
                betHolder.Add(Spots.Ball, 0);
                betHolder.Add(Spots.Diya, 0);
                betHolder.Add(Spots.Rabbit, 0);
                betHolder.Add(Spots.Dog, 0);
                betHolder.Add(Spots.Rose, 0);
                betHolder.Add(Spots.Flower, 0);
                betHolder.Add(Spots.Kite, 0);
                betHolder.Add(Spots.Butterfly, 0);
                betHolder.Add(Spots.Deer, 0);
                betHolder.Add(Spots.FunGame,0);
                Titli_Timer.Instance.onTimeUp += PostBets;
                Titli_Timer.Instance.onCountDownStart += ClearBet;
            }
            void PostBets()
            {
            }
            public void ClearBet()
            {
                betHolder[Spots.Umbrella] = 0;
                betHolder[Spots.Goat] = 0;
                betHolder[Spots.Pigeon] = 0;
                betHolder[Spots.Ball] = 0;
                betHolder[Spots.Diya] = 0;
                betHolder[Spots.Rabbit] = 0;
                betHolder[Spots.Dog] = 0;
                betHolder[Spots.Rose] = 0;
                betHolder[Spots.Flower] = 0;
                betHolder[Spots.Kite] = 0;
                betHolder[Spots.Butterfly] = 0;
                betHolder[Spots.Deer] = 0;
                betHolder[Spots.FunGame] = 0;
            }
            public void AddBets(Spots betType, Chip chipType)
            {
                betHolder[betType] = GetBetAmount(chipType);
            }
            private int GetBetAmount(Chip chipType)
            {
                int amount = 0;
                switch (chipType)
                {
                    // case Chip.Chip2:
                    //     amount = 2;
                    //     break;
                    case Chip.Chip10:
                        amount = 10;
                        break;
                    case Chip.Chip50:
                        amount = 50;
                        break;
                    case Chip.Chip100:
                        amount = 100;
                        break;
                    case Chip.Chip500:
                        amount = 500;
                        break;
                    case Chip.Chip1000:
                        amount = 1000;
                        break;
                    case Chip.Chip5000:
                        amount = 5000;
                        break;
                    default:
                        break;
                }
                return amount;
            }
    }
}
