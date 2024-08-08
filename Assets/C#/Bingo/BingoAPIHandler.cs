using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;

public static class BingoAPIHandler
{
    /// <summary>
    /// Post Request to get player balance
    /// </summary>
    /// <param name="pid">player id</param>
    /// <param name="Callback">callback method</param>
    /// <returns>PlayerBalanceData object</returns>
    //string getbalanceurl = " http://139.59.92.165:5000/user/bingoGetBalance";
    public static IEnumerator GetBalance(string pid, Action<PlayerBalanceData> Callback)
    {
        WWWForm data = new WWWForm();
        data.AddField("email", pid);

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
    public static IEnumerator TakeAmount(string pid, Action<TakeBalance> Callback)
    {
        WWWForm data = new WWWForm();
        data.AddField("email", pid);

        using UnityWebRequest request = UnityWebRequest.Post("http://139.59.92.165:5000/user/bingoTakeAmount", data);
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("Error" + request.error);
        }
        else
        {
            Debug.Log("Check list :" + request.downloadHandler.text);
            var response = JsonConvert.DeserializeObject<TakeBalance>(request.downloadHandler.text);

            Callback(response);
        }
    }

    public static IEnumerator BigSmall(string pid, string answer,Action<BigSmaller> Callback)
    {
        WWWForm data = new WWWForm();
        data.AddField("email", pid);
        data.AddField("choice", answer);


        using UnityWebRequest request = UnityWebRequest.Post("http://139.59.92.165:5000/user/bingoDoubleUp", data);
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("Error" + request.error);
        }
        else
        {
            Debug.Log("Check list :" + request.downloadHandler.text);
            var response = JsonConvert.DeserializeObject<BigSmaller>(request.downloadHandler.text);

            Callback(response);
        }
    }

    /// <summary>
    /// Post request to post bet value in each bingo board and also the winning board position for player pid
    /// </summary>
    /// <param name="pid">player id</param>
    /// <param name="ticketBet">int array of bet value in each bingo board</param>
    /// <param name="bingoPos">position of the winning bingo board</param>
    /// <param name="Callback">callback method</param>
    /// <returns>BingoBetsPlacedData object</returns>
    public static IEnumerator BetsPlaced(string pid, int[] ticketBet, Action<BingoBetsPlacedData> Callback)
    {
        WWWForm data = new WWWForm();
        Debug.Log("user"+pid);
        data.AddField("email", pid);
        // data.AddField("box1",ticketBet[0]);
        // data.AddField("box2",ticketBet[1]);
        // data.AddField("box3",ticketBet[2]);
        // data.AddField("box4",ticketBet[3]);
        // data.AddField("box5",ticketBet[4]);
        // data.AddField("box6",ticketBet[5]);
        //data.AddField("ticket_bet", ticketBet)

        string ticketBetString = string.Join(",", ticketBet);
        string neewstring ="["+ticketBetString+"]";
        data.AddField("target_bet", neewstring);

        



        using UnityWebRequest request = UnityWebRequest.Post("http://139.59.92.165:5000/user/bingoBetsPlaced", data);
        Debug.Log("////////////////////");
        yield return request.SendWebRequest();
        Debug.Log(request.result);
        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("Error" + request.error);
        }
        else
        {
            Debug.Log("Check list :" + request.downloadHandler.text);
            var response = JsonConvert.DeserializeObject<BingoBetsPlacedData>(request.downloadHandler.text);

            Callback(response);
        }
    }
   
    //#endregion
}
#region Bingo Data Classes

public class BingoBetsPlacedData
{
    public int status { get; set; }
    public string message { get; set; }
    public BingoData data { get; set; }
}

public class BingoData
{
    public int win_amount { get; set; }
    public int[] bingo { get; set; }
    public int point { get; set; }
}
public class PlayerBalanceData
{
    public int balance { get; set; }
}
public class TakeBalance
{
    public int point { get; set; }
}
public class BigSmaller
{
    public string message { get; set; }
    public DoubleData data{ get; set; }
}
public class DoubleData
{
    public int double_up_number{get; set; }
    public int win_amount{get; set; }
}

#endregion
