using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserDetail : MonoBehaviour
{
    public static int UserId;
    // {
    //     get 
    //     {
    //         return PlayerPrefs.GetString("email");
    //     }
    // }
    public static string ID;
    public static string Name;
    public static int ProfileId;
    public static string refer_id;
    public static int Balance;
    public static string MobileNo;
    public static int Safechip;
    public static string playerId_Local
    {
        get
        {
            return "GK" + PlayerPrefs.GetString("email");
        }
    }
    public static string player_Local_Balance
    {
        get
        {
            return "GK" + PlayerPrefs.GetString("points");
        }
    }

    //public static void UpdateSelfPicture(Action callback)
    //{
    //    Hashtable table = new Hashtable(); //update photon profile picture
    //    table.Add("image", Profile.image);
    //    PhotonNetwork.player.SetCustomProperties(table);

    //    if (WebRequestHandler == null)
    //        WebRequestHandler = GameObject.Find("WebRequestHandler").GetComponent<WebRequestHandler>();
    //    WebRequestHandler.DownloadImage(Profile.image, (sprite) =>
    //    {
    //        ProfilePicture = sprite;
    //        callback?.Invoke();
    //    });
    //}
}
