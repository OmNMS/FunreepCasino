using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SplashScreenScript : MonoBehaviour
{
    public GameObject SplashScreen;
    public Image RingFillImg;

    public void Start()
    {
        Application.targetFrameRate = -1;
        StartCoroutine(Wait());
        Debug.Log("Splash Screen");
    }
    IEnumerator Wait()
    {
        RingFillImg.fillAmount = 0;
        SplashScreen.SetActive(true);
        while (RingFillImg.fillAmount < 1)
        {
            RingFillImg.fillAmount += 0.05f;
            yield return new WaitForSeconds(0.05f);
        }
        yield return new WaitForSeconds(0.5f);
        SplashScreen.SetActive(false);
        if (PlayerPrefs.GetString("email", "") != "")
        {
            string str = PlayerPrefs.GetString("email", "");
            LoginScript.Instance.PlayerID.text = PlayerPrefs.GetString("email", "");
            LoginScript.Instance.Password.text = PlayerPrefs.GetString("password", "");
            LoginScript.Instance.LoginBtn();
            //StartCoroutine(LoginScript.Instance.checking());
            
        }
        else if (PlayerPrefs.GetString("GuestData", "") != "")
        {
            LoginScript.Instance.PlayGuestBtn();
        }
        else
        {
            LoginScript.Instance.ShowLoginUI();
        }
        //res myObject = new res();
        //myObject.en = "hello";
        ////myObject.data.profilePic = "1";
        //string json = JsonUtility.ToJson(myObject);
        ////object o = new { en = "hello" };
        //print(json);

    }
}
[Serializable]
public class res//{en:"HELLO","data":{"Abc":"bbb"}}
{
    public string en;
    public Datas data;
    [Serializable]
    public class Datas//{en:"HELLO","data":{"Abc":"bbb"}}
    {
        public string profilePic;
        public string balance;
    }
}
