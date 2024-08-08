using System.Collections;
using System.Collections.Generic;
using Commands;
using UnityEngine;
using ViewModel;

namespace Components
{
    public class GamePlayInput : MonoBehaviour
    {
        public CharacterTable chartacterTable;
        public GameRoullete gameRoullete;
        public GameCmdFactory gameCmdFactory;
        
        public void OnClick()
        {
            // gameCmdFactory.PlayTurn(chartacterTable, gameRoullete).Execute();
            Managers.GameManager.Instance.PlacedBetsTxt.text = "0";
            Managers.GameManager.Instance.BetValue = 0;
        }
    }
}
