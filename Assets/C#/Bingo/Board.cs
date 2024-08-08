using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// A bingo board. Contains 25 boxes(buttons) labelled from 1 to 25.
/// </summary>
public class Board : MonoBehaviour
{
    #region Fields/Properties

    public List<Button> boxes = new List<Button>();
    public int BetValue { get; private set; }

    //ui elements
    public Sprite defaultBoxSprite;
    public Button betButton;
    public Text betValueText;

    #endregion

    #region Methods

    private void Start()
    {
        defaultBoxSprite = boxes[0].GetComponent<Image>().sprite;
        BetValue = 0;
    }

    /// <summary>
    /// Adds 1 bet amount to this board
    /// </summary>
    public void AddBet(int nmber )
    {
        BetValue++;
        betValueText.text = BetValue.ToString();

        Gameplay.instance.AddBet(nmber);
    }

    #endregion
}
