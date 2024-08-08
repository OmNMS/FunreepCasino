using UnityEngine;

public class KeepScreenActive : MonoBehaviour
{
    void Start()
    {
        // Prevent the screen from dimming or going to sleep
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }
}