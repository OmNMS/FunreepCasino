using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyAccountScript : MonoBehaviour
{
    public GameObject PointTransferScreen, ReceivablesScreen, TransferablesScreen, ChangePasswordScreen, ChangePinScreen;
    public Text balance;
    public GameObject[] buttonstext;
    public Text[] headers;
    private void Update() {
        balance.text = PlayerPrefs.GetFloat("points").ToString();
    }

    public void OnClickPointTransferScreen()
    {
        PointTransferScreen.SetActive(true);
        ReceivablesScreen.SetActive(false);
        TransferablesScreen.SetActive(false);
        ChangePasswordScreen.SetActive(false);
        ChangePinScreen.SetActive(false);
        changecolor(0);
        //pointransfer.color = new Color32(255,184,13,255);
    }

    public void OnClickReceivablesScreen()
    {
        PointTransferScreen.SetActive(false);
        ReceivablesScreen.SetActive(true);
        TransferablesScreen.SetActive(false);
        ChangePasswordScreen.SetActive(false);
        ChangePinScreen.SetActive(false);
        //buttonstext[1].GetComponent<Text>().color = Color.yellow;
        changecolor(1);
    }

    public void OnClickTransferablesScreen()
    {
        PointTransferScreen.SetActive(false);
        ReceivablesScreen.SetActive(false);
        TransferablesScreen.SetActive(true);
        ChangePasswordScreen.SetActive(false);
        ChangePinScreen.SetActive(false);
        changecolor(2);
    }

    public void OnClickChangePasswordScreen()
    {
        PointTransferScreen.SetActive(false);
        ReceivablesScreen.SetActive(false);
        TransferablesScreen.SetActive(false);
        ChangePasswordScreen.SetActive(true);
        ChangePinScreen.SetActive(false);
        changecolor(3);
    }

    public void OnClickChangePinScreen()
    {
        PointTransferScreen.SetActive(false);
        ReceivablesScreen.SetActive(false);
        TransferablesScreen.SetActive(false);
        ChangePasswordScreen.SetActive(false);
        ChangePinScreen.SetActive(true);
    }
    public void changecolor(int selected)
    {
        for (int i = 0; i < headers.Length; i++)
        {
            if(i== selected)
            {
                Color changed = new Color(255,184,13,255);
                //headers[i].GetComponent<Text>().color = changed; //new Color32(255,184,13,255);
                headers[i].color = Color.yellow;//changed;//new Color32(255,184,13,255);
                //buttonstext[i].GetComponent<Text>().color = Color.yellow;//new Color(255,184,13,255);
            }   
            else
            {
                headers[i].GetComponent<Text>().color = Color.white;
            }
        }
    }

}
