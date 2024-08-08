using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class funtarget_Sounds : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip buttonPress;
    public AudioClip wheel;
    public AudioClip betAudio;
    public AudioClip takeAudio;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per fra

    public void buttonPressfn()
    {
        audioSource.PlayOneShot(buttonPress);
    }

    public void TakeButtonPress()
    {
        audioSource.PlayOneShot(takeAudio);
    }

    public void BetButtonPress()
    {
        audioSource.PlayOneShot(betAudio);
    }

    public void wheelsound()
    {
        //AudioClip.Play(wheel);
    }
}
