using System;
using System.Collections.Generic;
using UnityEngine;

public class CardList : MonoBehaviour
{
    public List<GameObject> deck= new List<GameObject>();

    /// <summary>
    /// Returns a card of the given rank and suit
    /// </summary>
    /// <param name="rankValue">rank of required card</param>
    /// <param name="suitValue">suit of required card</param>
    /// <returns>card of the given rank and suit</returns>
    public Card GetCard(int rankValue, int suitValue)
    {
        foreach (GameObject card in deck)
        {
            if (card.GetComponent<Card>().rank == (Rank)rankValue && card.GetComponent<Card>().suit == (Suit)suitValue)
            {
                return card.GetComponent<Card>();
            }
        }
        return null;
    }
}
