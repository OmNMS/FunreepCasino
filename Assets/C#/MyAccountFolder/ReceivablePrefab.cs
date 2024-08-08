using UnityEngine.UI;
using UnityEngine;
using Newtonsoft.Json;
using Com.BigWin.Frontend.Data;
using Accountscript;

public class ReceivablePrefab : MonoBehaviour
{
    // public static ReceivablePrefab instance;
    [SerializeField] private Text From;
    [SerializeField] private Text To;
    [SerializeField] private Text Date;
    [SerializeField] private Text Amount;
    int isReceivable;
    int childNumber;
    int finalamount;
    string identify;
    public string referfenceid
    {
        get 
        {
            return identify; 
        }
    } 
    public GameObject temp;
    public static int amount;
    public string reference;

    public Button Reject;
    public Button Accept;
    public Toggle togglebtn;
    public string ids;
    public void Start()
    {
        // instance= this;
    }
    public void SetData(string from,string to, string amount,string date,string notification, int settheval)
    {
        From.text = from;
        To.text = to;
        Amount.text = amount;
        Date.text = date;
        identify = notification;
        isReceivable = settheval;
    }

   
    public void showdata()
    {
        amount = int.Parse(Amount.text);
        reference = identify;
        //temp = this.gameObject;
        storage.amount = amount;
        storage.notification = reference;
        storage.temp = this.gameObject;
        ids = storage.notification;
        
        //staticselector.instance.amount = amount;
        
        
    }
    public void acceptbutton()
    {
        object accept = new{ notifyId = reference,playerId = "GK"+PlayerPrefs.GetString("email")};
        Account_SocketRequest.instance.SendEvent(Constant.OnAcceptPoints, accept, (res) =>
        {
            Debug.LogError("  OnAcceptPoints   " + res);
            var repo = JsonConvert.DeserializeObject<Status>(res);
            //AndroidToastMsg.ShowAndroidToastMessage(repo.message);
            if (repo.status == 200)
            {
                UpdateBalane();
                
                Destroy(temp, 0.1f);
                //OnSuccessfullyAcceptedOrRejected();
            }
            
            string msg = string.IsNullOrEmpty(repo.message) ? repo.error : repo.message;
            if (Application.platform != RuntimePlatform.Android)
            {
                // dialogue.Show(msg);
                Debug.Log(msg);
            }
            else
            {
                AndroidToastMsg.ShowAndroidToastMessage(msg);
            }
        });
    }
    public void rejectbutton(string notification)
    {
        object accept = new{ notifyId = notification,playerId = "GK"+PlayerPrefs.GetString("email")};
        Account_SocketRequest.instance.SendEvent(Constant.OnRejectPoints, accept, (res) =>
        {
            Debug.LogError("  OnRejectPoints   " + res);
            var repo = JsonConvert.DeserializeObject<Status>(res);
            //AndroidToastMsg.ShowAndroidToastMessage(repo.message);
            if (repo.status == 200)
            {
                UpdateBalane();
                //Destroy(clone, 0.1f);
                //OnSuccessfullyAcceptedOrRejected();
            }
            
            string msg = string.IsNullOrEmpty(repo.message) ? repo.error : repo.message;
            if (Application.platform != RuntimePlatform.Android)
            {
                // dialogue.Show(msg);
                Debug.Log(msg);
            }
            else
            {
                AndroidToastMsg.ShowAndroidToastMessage(msg);
            }
        });
    }
    void UpdateBalane()
    {
        string _playername = "GK" + PlayerPrefs.GetString("email");
        object user = new { playerId = _playername };
        Account_SocketRequest.instance.SendEvent(Constant.OnUserProfile, user, (json) =>
        {
            
            BackEndData3<PlayerProfile> profile = JsonUtility.FromJson<BackEndData3<PlayerProfile>>(json);
            Debug.LogError("coins   " + json);
            Debug.Log("the value of mainBalance"+profile.data.coins.ToString());
            //mainBalance.text = profile.data.coins.ToString();
        });
    }

    public void SetSelection()
    {
        // Debug.Log(" Stats of bol is " + gameObject.GetComponentInChildren<Toggle>().isOn);
        if ( isReceivable == 200 )
        {
            
            if ( togglebtn.isOn )
            {
                if ( ! ReceivableScreen.instance.SelectedList.Exists( x => x.GetComponent<ReceivablePrefab>().referfenceid == referfenceid ))
                    ReceivableScreen.instance.SelectedList.Add(this.gameObject);
            }
            else
            {        
                if (ReceivableScreen.instance.SelectedList.Exists( x => x.GetComponent<ReceivablePrefab>().referfenceid == referfenceid ))
                {
                    ReceivableScreen.instance.SelectedList.RemoveAt( ReceivableScreen.instance.SelectedList.FindIndex( x => x.GetComponent<ReceivablePrefab>().referfenceid == referfenceid  ) );
                }
            }
        }
        if ( isReceivable == 400)
        {
            if ( togglebtn.isOn )
            {
                if ( ! TransferablesScreen.instance.SelectedList.Exists( x => x.GetComponent<ReceivablePrefab>().referfenceid == referfenceid ))
                    TransferablesScreen.instance.SelectedList.Add(this.gameObject);
            }
            else
            {        
                if (TransferablesScreen.instance.SelectedList.Exists( x => x.GetComponent<ReceivablePrefab>().referfenceid == referfenceid ))
                {
                    TransferablesScreen.instance.SelectedList.RemoveAt( TransferablesScreen.instance.SelectedList.FindIndex( x => x.GetComponent<ReceivablePrefab>().referfenceid == referfenceid  ) );
                }
            }            
        }
    } 
}

public static class storage 
{
    
    public static int amount;
    public static string notification;
    public static GameObject temp;
   
    
}

