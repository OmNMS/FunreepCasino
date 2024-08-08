using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
public class sceneloading : MonoBehaviour
{
    private AsyncOperationHandle SceneHandle;
    [SerializeField]
    private AssetReference AddressableScene;
    [SerializeField]
    private Slider LoadingSlider;
    [SerializeField]
    //private Text LoadingText;
    void Start()
    {
        SceneHandle = Addressables.DownloadDependenciesAsync(AddressableScene);
        SceneHandle.Completed += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneHandle.Completed -= OnSceneLoaded; 
    }
    private void OnSceneLoaded(AsyncOperationHandle obj)
    {
        if(obj.Status == AsyncOperationStatus.Succeeded)
        {  
            Debug.Log("Success");
            Addressables.LoadSceneAsync(AddressableScene, UnityEngine.SceneManagement.LoadSceneMode.Single, true);
        }
    }

    void Update()
    {
        LoadingSlider.value = SceneHandle.GetDownloadStatus().Percent;
        //LoadingText.text = "Loading Scene "+ SceneHandle.GetDownloadStatus().Percent * 100f + "%".ToString();
        if (SceneHandle.GetDownloadStatus().Percent == 1)
        {
            //LoadingText.text = "Please Wait ... ";
        }
    }
}
