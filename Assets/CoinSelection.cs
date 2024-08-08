using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CoinSelection : MonoBehaviour
{
    public int currentBetValue = 0;
    public int TotalBetValue = 0;
    [SerializeField] Button chipNo1Btn;
    [SerializeField] Button chipNo5Btn;
    [SerializeField] Button chipNo10Btn;
    [SerializeField] Button chipNo50Btn;
    [SerializeField] Button chipNo100Btn;
    [SerializeField] Button chipNo500Btn;
    [SerializeField] Button chipNo1000Btn;
    [SerializeField] Button chipNo5000Btn;

    public Text TotalValueText;
    // Start is called before the first frame update
    void Start()
    {
        OnAdListeners();
    }


    private void OnAdListeners()
    {
        chipNo1Btn.onClick.AddListener(() =>
        {
            Debug.Log("1");
            currentBetValue = 1;
        });

        chipNo5Btn.onClick.AddListener(() =>
        {
            Debug.Log("5");
            currentBetValue = 5;
        });
        chipNo10Btn.onClick.AddListener(() =>
        {
            Debug.Log("10");
            currentBetValue = 10;
        });
        chipNo50Btn.onClick.AddListener(() =>
        {
            Debug.Log("50");
            currentBetValue = 50;
        });
        chipNo100Btn.onClick.AddListener(() =>
        {
            Debug.Log("100");
            currentBetValue = 100;
        });
        chipNo500Btn.onClick.AddListener(() =>
        {
            Debug.Log("500");
            currentBetValue = 500;
        });
        chipNo1000Btn.onClick.AddListener(() =>
        {
            Debug.Log("1000");
            currentBetValue = 1000;
        });

        chipNo5000Btn.onClick.AddListener(() =>
        {
            Debug.Log("5000");
            currentBetValue = 5000;
            
        });
       
    }

    // Update is called once per frame
    void Update()
    {
    }
}
