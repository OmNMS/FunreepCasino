using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class uiandar : MonoBehaviour
{
    public Transform score1;
    public Transform score2;
    public Transform winner1;
    public Transform winner2;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(blink());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public IEnumerator blink()
    {
        //Debug.Log("kkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkk");
        score1.localScale = new Vector3(1.5f,1.5f,0);
        score2.localScale = new Vector3(1.5f,1.5f,0);
        winner1.localScale = new Vector3(1.5f,1.5f,0);
        winner2.localScale = new Vector3(1.5f,1.5f,0);
        yield return new WaitForSeconds(5f);
        score1.localScale = new Vector3(0.7f,0.7f,0);
        score2.localScale = new Vector3(0.7f,0.7f,0);
        winner1.localScale = new Vector3(0.7f,0.7f,0);
        winner2.localScale = new Vector3(0.7f,0.7f,0);
        StartCoroutine(blink());

    }
}
