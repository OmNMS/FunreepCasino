using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class sound : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource music;
    public AudioSource sfx;
    void Start()
    {
        check();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void check()
    {
        
        if(PlayerPrefs.GetString("musics") == "true")
        {
            music.Play();
        }
        if(PlayerPrefs.GetString("sounds") == "true")
        {
            sfx.Play();
        }
        if(PlayerPrefs.GetString("musics") == "false")
        {
            music.Stop();
        }
        if(PlayerPrefs.GetString("sounds") == "false")
        {
            sfx.Stop();
        }
    }
}
