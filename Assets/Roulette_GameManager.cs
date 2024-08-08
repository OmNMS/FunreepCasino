using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Roulette_GameManager : MonoBehaviour
{
    int balance = 10000;
  [SerializeField]  Text BalanceTxt;
    // Start is called before the first frame update
    void Start()
    {
        BalanceTxt.text = balance.ToString();
    }     

    // Update is called once per frame
    void Update()
    {
        
    }
}
