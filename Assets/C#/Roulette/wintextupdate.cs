using System.Collections;
using System.Collections.Generic;
using Roulette.Gameplay;
using UnityEngine;

public class wintextupdate : MonoBehaviour
{
    // Start is called before the first frame update
    public void displaynow()
    {
        Debug.LogWarning("called form the animator");
        RouletteScreen.Instance.showwinamount();
    }
}
