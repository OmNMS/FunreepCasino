using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Roulette.Gameplay;

public class testingforroulette : MonoBehaviour
{
    //public RouletteScreen script;
    //public GameObject script;
    // Start is called before the first frame update
    public void wheelrotation()
    {
        RouletteScreen.Instance.afterzoom();
        //script.GetComponent<RouletteScreen>().
    }
}
