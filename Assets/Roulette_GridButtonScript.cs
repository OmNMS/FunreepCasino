using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Roulette_GridButtonScript : MonoBehaviour
{
    [SerializeField] CoinSelection CS;
    public Button First_12;
    public Button Second_12;
    public Button Third_12;

    [SerializeField] GameObject F12Selected;
    [SerializeField] GameObject S12Selected;
    [SerializeField] GameObject T12Selected;

    public void BetButton()
    {
        CS.TotalBetValue += CS.currentBetValue;
        CS.TotalValueText.text = CS.TotalBetValue.ToString();
    }

    private void Start()
    {
        OnAdListener();
    }

   private void  OnAdListener()
    {
        First_12.onClick.AddListener(() =>
        {
            Debug.Log("First12");
            F12Selected.SetActive(true);
        });

        Second_12.onClick.AddListener(() =>
        {
            Debug.Log("Second12");
            S12Selected.SetActive(true);
        });

        Third_12.onClick.AddListener(() =>
        {
            Debug.Log("Second12");
            T12Selected.SetActive(true);
        });
    }
}
