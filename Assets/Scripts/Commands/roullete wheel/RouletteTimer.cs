using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Commands;
using ViewModel;

namespace Scripts
{
    public class RouletteTimer : MonoBehaviour
    {
        public static RouletteTimer instance;
        public Text TimerTxt;
        public CharacterTable chartacterTable;
        public GameRoullete gameRoullete;
        public GameCmdFactory gameCmdFactory;

        void Awake()
        {
            instance = this;
        }

        // Start is called before the first frame update
        void Start()
        {
            StartCoroutine(Timer(20));
        }

        public IEnumerator Timer(int countDown = 60)
        {
            while(countDown > 0)
            {
                TimerTxt.text = countDown.ToString();
                yield return new WaitForSecondsRealtime(1f);
                countDown--;
                TimerTxt.text = countDown.ToString();
                if(countDown <= 0)
                {
                    gameCmdFactory.PlayTurn(chartacterTable, gameRoullete).Execute();
                    Managers.GameManager.Instance.PlacedBetsTxt.text = "0";
                    Managers.GameManager.Instance.BetValue = 0;
                }
                else if(countDown <= 10)
                {
                    AnimationScript.instance._hideRoulette = false;
                }
            }
        }
    }
}
