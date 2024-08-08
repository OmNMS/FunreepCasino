using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceProviders;
//using UnityEngine.ResourceManagement.AsyncOperations;

public class SceneHandler : MonoBehaviour
{
    public bool clearPreviousScene = false;
    SceneInstance previousLoadedScene;
    //private AsyncOperationHandle Handler;
    private void Awake() 
    {
        //DontDestroyOnLoad(this.gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void loadAddressableScene(string addressableKey)
    {
        // if (clearPreviousScene)
        // {
        //     Addressables.UnloadSceneAsync(previousLoadedScene).Completed += (asyncHandle) => 
        //     {
        //         clearPreviousScene = false;
        //         previousLoadedScene = new SceneInstance();
        //         Debug.Log("Scene Unloaded  ");
        //     };
        // }

        Addressables.LoadSceneAsync(addressableKey, LoadSceneMode.Single).Completed += (asyncHandle) =>
        {
            Debug.Log("Scene Loading in progress " + asyncHandle.Status);
            previousLoadedScene = asyncHandle.Result;
            Debug.Log("Scene Loaded ");

        };
    }

    public void unloadAddressableScene()
    {
        Addressables.UnloadSceneAsync(previousLoadedScene, true).Completed += (asyncHandle) => 
        {
            Debug.Log("Scene Unloading in progress " + asyncHandle.Status);
            previousLoadedScene = new SceneInstance();
            Debug.Log("Scene Unloaded ");
            SceneManager.LoadScene("MainScene");
        };
    }
    /*private void Update() 
    {
        slider = 
    }*/
    

}
