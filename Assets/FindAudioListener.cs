using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindAudioListener : MonoBehaviour
{
    AudioListener[] ears;

    // Start is called before the first frame update
    void Start()
    {
        ears = FindObjectsOfType<AudioListener>();

        foreach(AudioListener ear in ears)
        {
            Debug.LogError("This ear is attached to : "+ear.gameObject);
        }
    }

}
