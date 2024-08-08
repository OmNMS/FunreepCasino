using UnityEngine.UI;
using UnityEngine;
using Com.BigWin.Frontend.Data;
using System;
using Unity.Jobs;
using Unity.Collections;
using Receivable;
using System.Reflection;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Collections;
using SocketIO;
using UnityEngine.Networking;

public class TransferablesScreen : MonoBehaviour
{
    public static TransferablesScreen instance;
    public GameObject TransferablePrefab;
    public GameObject content;
    public Text mainBalance;
    public Toggle all;
    public GameObject RejectBtn;
    private string playerId;
    [ReadOnly] public bool isDataLoaded;
    [SerializeField] private Button backBtn;
    List<GameObject> prefabs = new List<GameObject>();
    public List<GameObject> SelectedList;
    const string transferableurl = "http://139.59.92.165:5000/user/transferPoint";
    const string trasferreject = "http://139.59.92.165:5000/user/rejectPoint1";
    public Transform parent;
    //string staticconst transferableurl;

    void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
        string _playername = "GK" + PlayerPrefs.GetString("email");
        playerId = _playername;
        if (isDataLoaded) return;
        object user = new { playerId = _playername };
        //Account_SocketRequest.instance.socket.On(Constant.OnSenderNotification,DisplayHistory);
        //Gethistory();

        //Account_SocketRequest.instance.SendEvent(Constant.OnSenderNotification, user, (res) =>
        //    {
        //        BackEndData<ReceivableData> receivable = JsonConvert.DeserializeObject<BackEndData<ReceivableData>>(res);
        //        GetReceivableData(receivable.data, receivable.status);
        //    });
        foreach (var item in prefabs)
        {
            Destroy(item);
        }
        prefabs.Clear();
        //UpdateBalane();
        AddListners();
        //callforhistory();
    }
    /*public void Gethistory()
    {
        
        User user = new User{ playerId = "RL" + PlayerPrefs.GetString("email")};
        Account_SocketRequest.instance.socket.Emit(Constant.OnSenderNotification,new JSONObject(JsonConvert.SerializeObject(user)));
        Debug.Log("USER DETAILS"+user.playerId);

    }*/
    public List<GameObject> clonelist;
    public List<GameObject> hasrecords;
    public void callforhistory()
    {
        StartCoroutine(Transferableapi(transferableurl,"GK" + PlayerPrefs.GetString("email")));
    }
    
    IEnumerator Transferableapi(string PostApiURL, string emailId)
    {
        WWWForm form = new WWWForm();
        form.AddField("emailId", emailId);

        using (UnityWebRequest www = UnityWebRequest.Post(PostApiURL, form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Transferable Response :"+www.downloadHandler.text);
                // Debug.LogError("sxdrcfvgbhnjnbhvgfcxddcfvgbhnjvfcx"+www.downloadHandler.text);
                //Histroyclass list = JsonConvert.DeserializeObject<Histroyclass>(www.downloadHandler.text);
                DisplayHistory(www.downloadHandler.text);
            }
        }
    }
    public void DisplayHistory(string e)
    {
        //Debug.Log("///////////////////////////////////////////"+e.data);
        // if(clonelist.Count > 0)
        // {
        //     foreach(GameObject item in clonelist)
        //     {
        //         Destroy(item);
        //     }
        //     clonelist.Clear();
        //     Debug.Log("clearing list");
        // }
        /////
        /*foreach(GameObject item in parent)
        {
            //item.Destroy(GetComponentInChildren(0));
            Destroy(item);
        }*/
        Histroyclass history = JsonConvert.DeserializeObject<Histroyclass>(e);
        // Debug.Log(history.status);
        // Debug.Log("//////////////////////////"+history.data.transferRecord[0].from);

        if ( history.status != 200 )
        {
            // AndroidToastMsg.ShowAndroidDefaltMessage();
            print("something went wrong while receiving data");
            return;
        }
        else 
        {
            if ( history.data.transferRecord.Length > 0 )
            {
                RejectBtn.SetActive(true);
            }
            foreach (var item in clonelist)
            {
                bool exist = false;
                for (int i = 0; i < history.data.transferRecord.Length; i++)
                {
                    if ( item.GetComponent<ReceivablePrefab>().referfenceid == history.data.transferRecord[i].id.ToString() )
                    {
                        exist = true;
                    }

                }
                if ( ! exist )
                {
                    GameObject dest = item;
                    clonelist.Remove( item );                       
                    Destroy( dest);
                }
            }
            //foreach (var item in history.data.transferRecord)
            for (int i = 0; i < history.data.transferRecord.Length; i++)
            {
                if ( clonelist.Exists( x => x.GetComponent<ReceivablePrefab>().referfenceid == history.data.transferRecord[i].id.ToString()  )  )
                {
                    continue;
                }
                else
                {
                    
                //GameObject clone = Instantiate(receivablePrefab);
                
                //clone.transform.SetParent(parent);
                    GameObject clone = Instantiate(TransferablePrefab, content.transform);
                    clonelist.Add(clone);
                    clone.GetComponent<ReceivablePrefab>().SetData( history.data.transferRecord[i].from, history.data.transferRecord[i].to, history.data.transferRecord[i].amount.ToString(), history.data.transferRecord[i].date, history.data.transferRecord[i].id.ToString(), 400 );
                    clone.SetActive(true);   
                }
            }
        }
    }

    public void SelectAll()
    {
        if ( all.isOn )
        {
            foreach (var item in clonelist)
            {
                item.GetComponent<ReceivablePrefab>().togglebtn.isOn = true;                
            }                
        }
        else
        {
            foreach (var item in clonelist)
            {
                item.GetComponent<ReceivablePrefab>().togglebtn.isOn = false;
            }
            
        }
    }
    string id;
    public void rejecttransfer()
    {
        // id = storage.notification;
        foreach (var item in SelectedList)
        {
            Debug.Log("Sending id is : " + item.GetComponent<ReceivablePrefab>().referfenceid );
            // rejectbutton( item.GetComponent<ReceivablePrefab>().referfenceid, item);
            StartCoroutine(removefromit( item.GetComponent<ReceivablePrefab>().referfenceid, item  ));
        }
        SelectedList.Clear();

        // StartCoroutine(removefromit(0));
    }
    public IEnumerator removefromit(string id, GameObject obj)
    {
        WWWForm form = new WWWForm();
        form.AddField("id", id);

        using (UnityWebRequest www = UnityWebRequest.Post(trasferreject, form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                
                Debug.Log("delete everything responce "+www.downloadHandler.text);

                baseclassa var = JsonConvert.DeserializeObject<baseclassa>(www.downloadHandler.text);

                if ( var.status == 200 )
                {
                    UpdateBalane();
                    
                    // Destroy(storage.temp, 0.1f);
                    clonelist.RemoveAt( clonelist.FindIndex( x => x.GetComponent<ReceivablePrefab>().referfenceid == id  ) );
                    Destroy( obj);
                }


            }
        }
        
    }

    private void AddListners()
    {
        //backBtn.onClick.AddListener(() => { isDataLoaded = false; DestroyAllChildren(content.transform); });

    }

    /*public void GetReceivableData(ReceivableData data, string status)
    {
        string _playername = "RL" + PlayerPrefs.GetString("email");
        isDataLoaded = true;
        DestroyAllChildren(content.transform);
        for (int i = 0; i < data.notification_count; i++)
        {
            GameObject clone = Instantiate(TransferablePrefab, parent: content.transform);
            prefabs.Add(clone);
            ReceivablePrefab trasferableClone = clone.GetComponent<ReceivablePrefab>();
            string from = data.notification[i].FromAccountName;
            string to = data.notification[i].ToAccountName;
            string amount = data.notification[i].point.ToString();
            string date = data.notification[i].createdat.ToString();
            string noti_id = data.notification[i].id.ToString();
            trasferableClone.SetData(from, to, amount, date, noti_id);//amount

            trasferableClone.Reject.onClick.AddListener(() =>
            {
                object accept = new { notifyId = noti_id, playerId = _playername };

                Account_SocketRequest.instance.SendEvent(Constant.OnRejectPoints, accept, (res) =>
                {
                    Debug.Log(res);
                    var repo = JsonConvert.DeserializeObject<Status>(res);
                    if (repo.status == 200)
                    {
                        UpdateBalane();
                        Destroy(clone, 1f);
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
            });
            clone.SetActive(true);
        }
    }*/

    void UpdateBalane()
    {
        string _playername = "GK" + PlayerPrefs.GetString("email");
        object user = new { user_id = _playername };
        Account_SocketRequest.instance.SendEvent(Constant.OnUserProfile, user, (json) =>
        {
            
            BackEndData3<PlayerProfile> profile = JsonUtility.FromJson<BackEndData3<PlayerProfile>>(json);
            mainBalance.text = profile.data.coins.ToString();
        });
    }

    public void DestroyAllChildren(Transform parent)
    {
        foreach (Transform child in parent)
        {
            GameObject.Destroy(child.gameObject);
        }
    }

    public class User
    {
        public string playerId;
    }
    public class  Histroyclass
    {
        public int status;
        public string message;
        public classdetails data;
    }
    public class classdetails
    {
        public Transactiondetails[] transferRecord;
    }
    public class Transactiondetails
    {
        public string to;
        public string from;
        public int amount;
        public string date;
        public int id;
    }

    public class baseclassa
    {
        public int status;
        public string message;
        
    }
    
}
