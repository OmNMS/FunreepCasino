using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;

public static class APIHandler
{
    public static IEnumerator GetBalance(string pid, Action<PlayerBalanceData> Callback)
    {
        WWWForm data = new WWWForm();
        data.AddField("playerid", pid);

        using UnityWebRequest request = UnityWebRequest.Post("http://139.59.92.165:5000/user/bingoGetBalance", data);
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("Error" + request.error);
        }
        else
        {
            Debug.Log("Check list :" + request.downloadHandler.text);
            var response = JsonConvert.DeserializeObject<PlayerBalanceData>(request.downloadHandler.text);

            Callback(response);
        }
    }

    /// <summary>
    /// Post Request for BetsPlaced
    /// </summary>
    /// <param name="pid">player id</param>
    /// <param name="betAmount">bet amount</param>
    /// <param name="Callback">callback method</param>
    /// <returns>BetsPlacedObject object that includes card details</returns>
    public static IEnumerator BetsPlaced(string pid, int betAmount, Action<JokerBonusBetsPlacedData> Callback)
    {
        WWWForm data = new WWWForm();
        data.AddField("playerid", pid);
        data.AddField("bet_amount", betAmount);

        using UnityWebRequest request = UnityWebRequest.Post("http://139.59.92.165:5000/user/jokerBetPlaced", data);
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("Error" + request.error);
        }
        else
        {
            Debug.Log("Check list :" + request.downloadHandler.text);

            var response = JsonConvert.DeserializeObject<JokerBonusBetsPlacedData>(request.downloadHandler.text);

            Callback(response);
        }
    }

    /// <summary>
    /// Post request for TakeAmount
    /// </summary>
    /// <param name="pid">player id</param>
    /// <param name="Callback">callback method</param>
    /// <returns>message</returns>
    public static IEnumerator TakeAmount(string pid, float updatedBalance, Action<TakeAmountData> Callback)
    {
        WWWForm data = new WWWForm();
        data.AddField("playerId", pid);
        data.AddField("updateBalance", updatedBalance.ToString());

        using UnityWebRequest request = UnityWebRequest.Post("http://139.59.92.165:5000/user/jokerTakeAmount", data);
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("Error" + request.error);
        }
        else
        {
            Debug.Log("Check list :" + request.downloadHandler.text);
            var response = JsonConvert.DeserializeObject<TakeAmountData>(request.downloadHandler.text);

            Callback(response);
        }
    }

    /// <summary>
    /// Post request for DoubleUp API
    /// </summary>
    /// <param name="pid">player id</param>
    /// <param name="doubleUp">player's choice for big or small</param>
    /// <param name="Callback">call back method</param>
    /// <returns>double up card and win amount</returns>
    public static IEnumerator DoubleUp(string pid, Action<JokerBonusDoubleUpData> Callback)
    {
        WWWForm data = new WWWForm();
        data.AddField("playerId", pid);

        using UnityWebRequest request = UnityWebRequest.Post("http://139.59.92.165:5000/user/jokerDoubleUp", data);
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("Error" + request.error);
        }
        else
        {
            Debug.Log("Check list :" + request.downloadHandler.text);
            var response = JsonConvert.DeserializeObject<JokerBonusDoubleUpData>(request.downloadHandler.text);

            Callback(response);
        }
    }
}
