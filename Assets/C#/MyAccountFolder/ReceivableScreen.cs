using UnityEngine.UI;
using UnityEngine;
using Com.BigWin.Frontend.Data;
using System;
using Unity.Jobs;
using Unity.Collections;
using Receivable;
using System.Reflection;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace Accountscript
{
    public class ReceivableScreen : MonoBehaviour
    {
        public GameObject receivablePrefab;
        public GameObject content;
        public Button received;
        public Button reject;
        public Text mainBalance;
        public static ReceivableScreen instance;
        [ReadOnly] private bool isDataLoaded;
        private string playerId;
        [SerializeField] private Button backBtn;
        List<GameObject> prefabs = new List<GameObject>();
        ChildButtonHandler childcounter;
        public Toggle all;
        public bool whole;
        public List<GameObject> SelectedList;
        public List<GameObject> cloneofRecevables;

        // Start is called before the first frame update
        void Start()
        {
            instance = this;
            StartCoroutine(receiveabledata());
            //all.onValueChanged.AddListener(OnToggleValueChanged);
            // if (isDataLoaded) return;
            // string _playername = "GK" + PlayerPrefs.GetString("email");
            // playerId = _playername;
            // object user = new { playerId  };
            // Account_SocketRequest.instance.SendEvent(Constant.OnNotification,user, (res) =>
            // {
            //     BackEndData<ReceivableData> receivable = JsonConvert.DeserializeObject<BackEndData<ReceivableData>>(res);
                
            //     GetReceivableData(receivable.data,receivable.status);
            // });

            // foreach (var item in prefabs)
            // {
            //     Destroy(item);
            // }
            // prefabs.Clear();
            //UpdateBalane();
            //AddListners();
        }   
        public IEnumerator receiveabledata()
        {
            string _playername = "GK" + PlayerPrefs.GetString("email");
            playerId = _playername;
            object user = new { playerId  };
            Account_SocketRequest.instance.SendEvent(Constant.OnNotification,user, (res) =>
            {
                BackEndData<ReceivableData> receivable = JsonConvert.DeserializeObject<BackEndData<ReceivableData>>(res);
                
                GetReceivableData(receivable.data,receivable.status);
            });

            foreach (var item in prefabs)
            {
                Destroy(item);
            }
            prefabs.Clear();
            yield return new WaitForSeconds(5f);
            StartCoroutine(receiveabledata());
        }
        // private void AddListners()
        // {

        //     backBtn.onClick.AddListener(() => { isDataLoaded = false; DestroyAllChildren(content.transform); });
        // }

        public void GetReceivableData(ReceivableData data,string status)
        {
            try
            {
                if ( status != "200")
                {
                    // AndroidToastMsg.ShowAndroidDefaltMessage();
                    print("something went wrong while receiving data");
                    return;
                }
                print("nof " + data.notification_count);

                if (data.notification_count == 0) return;
                isDataLoaded = true;
                if (status == Constant.IS_INVALID_USER)
                {
                    //OnInvalidUser(data.message);
                    return;
                }
                if ( data.notification_count > 0 )
                {
                    reenable();
                //     cloneofRecevables.Clear();
                }
                // DestroyAllChildren( content.transform );
                foreach (var item in cloneofRecevables)
                {
                    bool exist = false;
                    for (int i = 0; i < data.notification_count; i++)
                    {
                        if ( item.GetComponent<ReceivablePrefab>().referfenceid == data.notification[i].id.ToString() )
                        {
                            exist = true;
                        }

                    }
                    if ( ! exist )
                    {
                        GameObject dest = item;
                        cloneofRecevables.Remove( item );                       
                        Destroy( dest);
                    }
                }

                for (int i = 0; i < data.notification_count; i++)
                {
                    if ( cloneofRecevables.Exists( x => x.GetComponent<ReceivablePrefab>().referfenceid == data.notification[i].id.ToString()  )  )
                    {
                        continue;
                    }
                    else
                    {
                        
                        GameObject clone = Instantiate(receivablePrefab, parent: content.transform);
                        cloneofRecevables.Add(clone);
                        ReceivablePrefab receivableclone = clone.GetComponent<ReceivablePrefab>();
                        string from = data.notification[i].FromAccountName;
                        string to = data.notification[i].ToAccountName;
                        string amount = data.notification[i].point.ToString();
                        string date = data.notification[i].createdat.ToString();
                        string noti_id = data.notification[i].id.ToString();
                        receivableclone.SetData(from, to,amount, date,noti_id, 200);//amopunt
                        // receivableclone.Accept.onClick.AddListener(() =>
                        // {
                        //     object accept = new  { notifyId = noti_id, playerId = playerId};
                        //     Account_SocketRequest.instance.SendEvent(Constant.OnAcceptPoints, accept, (res) =>
                        //     {
                        //         Debug.LogError("  OnAcceptPoints   " + res);
                        //         var repo = JsonConvert.DeserializeObject<Status>(res);
                        //         //AndroidToastMsg.ShowAndroidToastMessage(repo.message);
                        //         if (repo.status == 200)
                        //         {
                        //             UpdateBalane();
                        //             cloneofRecevables.RemoveAt( cloneofRecevables.FindIndex( x => x.GetComponent<ReceivablePrefab>().reference == noti_id  ) );
                        //             Destroy(clone, 0.1f);
                        //             //OnSuccessfullyAcceptedOrRejected();
                        //         }
                                
                        //         string msg = string.IsNullOrEmpty(repo.message) ? repo.error : repo.message;
                        //         if (Application.platform != RuntimePlatform.Android)
                        //         {
                        //             // dialogue.Show(msg);
                        //             Debug.Log(msg);
                        //         }
                        //         else
                        //         {
                        //             AndroidToastMsg.ShowAndroidToastMessage(msg);
                        //         }
                        //     });
                        // });
                        // receivableclone.Reject.onClick.AddListener(() =>
                        // {
                        //     object accept = new  { notifyId = noti_id, playerId = playerId,};
                        //     Account_SocketRequest.instance.SendEvent(Constant.OnRejectPoints, accept, (res) =>
                        //     {
                        //         Debug.LogError( " OnRejectPoints   " +  res);
                        //         var repo = JsonConvert.DeserializeObject<Status>(res);
                        //         if (repo.status == 200)
                        //         {
                        //             UpdateBalane();
                        //             cloneofRecevables.RemoveAt( cloneofRecevables.FindIndex( x => x.GetComponent<ReceivablePrefab>().reference == noti_id  ) );
                        //             Destroy(clone, 0.1f);
                        //             //OnSuccessfullyAcceptedOrRejected();
                        //         }
                        //         string msg = string.IsNullOrEmpty(repo.message) ? repo.error : repo.message;
                        //         if (Application.platform != RuntimePlatform.Android)
                        //         {
                        //             // dialogue.Show(msg);
                        //             Debug.Log(msg);
                        //         }
                        //         else
                        //         {
                        //             AndroidToastMsg.ShowAndroidToastMessage(msg);
                        //         }
                        //     });
                        // });
                        clone.SetActive(true);
                    }
                }
                //childcounter.readchild();
                
            }
            catch
            {

            }
        }
        private void OnToggleValueChanged(bool newValue)
        {
            whole = newValue;

        }
        public void selectall()
        {
            if ( all.isOn )
            {
                foreach (var item in cloneofRecevables)
                {
                    item.GetComponent<ReceivablePrefab>().togglebtn.isOn = true;                
                }                
            }
            else
            {
                foreach (var item in cloneofRecevables)
                {
                    item.GetComponent<ReceivablePrefab>().togglebtn.isOn = false;
                }
                
            }
            /*
            int currentIndex = 0 ;
            Button[] childButtons;
            childButtons = GetComponentsInChildren<Button>();
            if (currentIndex >= 0 && currentIndex < childButtons.Length)
            {
                // Simulate a click on the current child button
                childButtons[currentIndex].onClick.Invoke();
                // acceptance();

                // Increment the index to process the next child button
                currentIndex++;

                // If there are more child buttons, schedule pressing the next one
                if (currentIndex < childButtons.Length)
                {
                    float delay = 2.0f; // Adjust this value to control the delay between button presses (in seconds)
                    Invoke("PressNextButton", delay);
                }
            }
            else
            {
                Debug.Log("All child buttons have been pressed.");
            }*/
        }

        void UpdateBalane()
        {
            string _playername = "GK" + PlayerPrefs.GetString("email");
            object user = new { playerId = _playername };
            Account_SocketRequest.instance.SendEvent(Constant.OnUserProfile, user, (json) =>
            {
                
                BackEndData3<PlayerProfile> profile = JsonUtility.FromJson<BackEndData3<PlayerProfile>>(json);
                Debug.LogError("coins   " + json);
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

        public void LoremIpsum()
        {
            Debug.Log("Lorem Ipsum");
        }

        public void acceptance(string notiid, GameObject obj)
        {
            // if(whole)
            // {
            //     selectall();
            //     return;
            // }
            Debug.Log("Clicked on Receive");
            Debug.Log("acceptance was called"+storage.notification + "and the amount is"+storage.amount);
            received.interactable = false;
            reject.interactable =false;
            object accept = new{ notifyId = notiid /*storage.notification*/ ,playerId = "GK"+PlayerPrefs.GetString("email")};
            Account_SocketRequest.instance.SendEvent(Constant.OnAcceptPoints, accept, (res) =>
            {
                Debug.LogError("  OnAcceptPoints   " + res);
                var repo = JsonConvert.DeserializeObject<Status>(res);
                //AndroidToastMsg.ShowAndroidToastMessage(repo.message);
                if (repo.status == 200)
                {
                    Debug.Log("shpwshpwshpwshpwshpwshpwshpwshpwshowshowshow");
                    // Debug.Log("the name is"+storage.temp.name);

                    // Destroy(storage.temp);
                    cloneofRecevables.RemoveAt( cloneofRecevables.FindIndex( x => x.GetComponent<ReceivablePrefab>().referfenceid == notiid  ) );
                    Destroy( obj);
                    UpdateBalane();
                    
                    storage.amount =0;
                    storage.notification ="";
                    Invoke("reenable",2f);
                    HomeScript.Instance.refreshbutton();
                    
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

        public void newAccept()
        {
            foreach (var item in SelectedList)
            {
                acceptance( item.GetComponent<ReceivablePrefab>().referfenceid, item);
            }
            SelectedList.Clear();
        }

        public void newReject()
        {
            foreach (var item in SelectedList)
            {
                rejectbutton( item.GetComponent<ReceivablePrefab>().referfenceid, item);
            }
            SelectedList.Clear();
        }

        public void rejectbutton(string notiid, GameObject obj)
        {
            object accept = new{ notifyId = notiid /*storage.notification */ ,playerId = "GK"+PlayerPrefs.GetString("email")};
            received.interactable = false;
            reject.interactable =false;
            Account_SocketRequest.instance.SendEvent(Constant.OnRejectPoints, accept, (res) =>
            {
                Debug.LogError("  OnRejectPoints   " + res);
                var repo = JsonConvert.DeserializeObject<Status>(res);
                //AndroidToastMsg.ShowAndroidToastMessage(repo.message);
                if (repo.status == 200)
                {
                    UpdateBalane();
                    
                    // Destroy(storage.temp, 0.1f);
                    cloneofRecevables.RemoveAt( cloneofRecevables.FindIndex( x => x.GetComponent<ReceivablePrefab>().referfenceid == notiid  ) );
                    Destroy( obj);

                    Invoke("reenable",0.5f);
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

        public void reenable()
        {
            received.gameObject.SetActive(true);
            received.interactable = true;
            reject.gameObject.SetActive(true);
            reject.interactable =true;
        }

    }
    
}

[Serializable]
public class Status
{
    public int status;
    public string message;
    public string error;
}

