using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class afteranimation : MonoBehaviour
{
    [SerializeField] GameObject Numbertemp,Animationbox;
    public void disablebox()
    {
        Numbertemp.SetActive(true);
        Animationbox.SetActive(false);
    }
}
