using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class zoom : MonoBehaviour
{
    private bool zoomOn = true;
    [SerializeField] GameObject CamAnimation;
    // Start is called before the first frame update

    public void ClickZoom()
    {
        if (zoomOn)
        {
            zoomOn = false;
        }
        else
        {
            zoomOn = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (zoomOn)
        {
            gameObject.transform.GetChild(0).GetComponent<Text>().text = "Zoom : ON";
            CamAnimation.GetComponent<Animator>().enabled = true;
        }
        else
        {
            gameObject.transform.GetChild(0).GetComponent<Text>().text = "Zoom : OFF";
            CamAnimation.GetComponent<Animator>().enabled = false;
        }
    }


}
