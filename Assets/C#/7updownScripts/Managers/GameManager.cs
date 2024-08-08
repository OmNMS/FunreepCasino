using UpDown7.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    void Start()
    {
        LocalPlayer.LoadGame();
    }
    public void OnApplicationPause(bool pause)
    {
        if (Application.isEditor) return;
        if (!pause)
        {
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }
    }

    int click = 0;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            click++;
            ShowAndroidToastMessage("Press again to exit");
            Debug.Log("Prss again to exit");
            StartCoroutine(ClickAgain());
            if (click > 1)
            {
                Application.Quit();
                // SceneManager.LoadScene(0);
            }
        }
    }



    IEnumerator ClickAgain()
    {
        yield return new WaitForSeconds(0.5f);
        click = 0;
    }


    public static void ShowAndroidToastMessage(string message = "something went wrong")
    {
        if (Application.platform != RuntimePlatform.Android) return;

#if UNITY_ANDROID
        AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject unityActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");

        if (unityActivity != null)
        {
            AndroidJavaClass toastClass = new AndroidJavaClass("android.widget.Toast");
            unityActivity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
            {
                AndroidJavaObject toastObject = toastClass.CallStatic<AndroidJavaObject>("makeText", unityActivity, message, 0);
                toastObject.Call("show");
            }));
        }
#endif
    }


}
