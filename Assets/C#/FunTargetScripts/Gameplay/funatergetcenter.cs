using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class funatergetcenter : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject central;
    public iTween.EaseType easeType;
    public float tezi;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void testing()
    {
        StartCoroutine(rot());
    }
    IEnumerator rot()
    {
        iTween.RotateAdd(central, iTween.Hash( "y",-2160, "speed",tezi,  "easetype", easeType,"oncomplete","ballspinUpdate","oncompletetarget", central ) );
        yield return new WaitForSeconds(1f);
        //AfterSpin(desireNo);
    }
}
