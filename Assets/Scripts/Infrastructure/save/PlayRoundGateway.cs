using System;
using UniRx;
using UnityEngine;
using UnityEditor;
using ViewModel;
using System.Collections;
using Managers;
using UnityEngine.Networking;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using Random=UnityEngine.Random;
using UnityEngine.UI;

namespace Infrastructure
{
    public class PlayRoundGateway : IRound
    {
        public int randomNumber { get; set; }
       static int i = 0;
        public IObservable<Unit> PlayTurn()
        {
            randomNumber = Random.Range(0, 37);
            LastFive(randomNumber);
            return Observable.Return(Unit.Default)
                    .Do(_ =>  Debug.Log($"Generating number {randomNumber} for the roullete game round!"));
           
        }

        
        private void LastFive(int r)
        {
            GameManager.Instance.Winner.text = r.ToString();
            if (i < 5)
            {
                Debug.Log("i = " + i);
                GameManager.Instance.LastFive[i].text = r.ToString();
                if (r % 2 != 0)
                {
                    GameManager.Instance.LastFive[i].GetComponent<Text>().color = Color.red;
                }
                if (r == 0)
                {
                    GameManager.Instance.LastFive[i].GetComponent<Text>().color = Color.green;
                }
                i++;

            }
            else
            {
                i = 0;
            }
        }

        }



    }


