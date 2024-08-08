//using System.Threading.Tasks.Dataflow;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
// using System.Diagnostics;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Titliball : MonoBehaviour
{
    public static Titliball instant;
    public GameObject ball;
    public List<Transform> ballpos;
    //public List<Transform> ballpos;
    public List<Transform> copyoforiginal;
    public iTween.EaseType easeType;
    public float time,speed;
    //public Transform[] temp;
    public int finalval, previousval = 0;
    public Transform[] newarray;
    public GameObject duplicate;
    [SerializeField] Transform basepos;
    Transform default_position;

    public void Start() 
    {
        instant = this;
        default_position = gameObject.GetComponent<Transform>();
    }
    

    public void rotatrnow(int number)
    {
       
        StartCoroutine(rotator(number));
    }
    public IEnumerator values( int number)
    {
        int[] tablenos = {5,6,11,2,3,7,1,4,0,12,10,9,8};//{5,22,34,15,3,24,36,13,1,37,27,10,25,29,12,8,19,31,18,6,21,33,16,4,23,35,14,2,0,28,9,26,30,11,7,20,32,17}; 
        for (int i = 0; i < tablenos.Length; i++)
        {
            if(number == tablenos[i])
            {
                finalval =i;
                previousval = i;
                gameObject.transform.position = default_position.position;
                Debug.Log("Default position : " + default_position.position );
                yield break;
            }
        }
    }
    public IEnumerator initialrun(int number)
    {
        int[] tablenos = {5,6,11,2,3,7,1,4,0,12,10,9,8};//{5,22,34,15,3,24,36,13,1,37,27,10,25,29,12,8,19,31,18,6,21,33,16,4,23,35,14,2,0,28,9,26,30,11,7,20,32,17}; 
        for (int i = 0; i < tablenos.Length; i++)
        {
            if(number == tablenos[i])
            {
                finalval =i;
                previousval = i;
                gameObject.transform.position = default_position.position;
                Debug.Log("Default position : " + default_position.position );
                yield break;
            }
            
        }
    }
    public IEnumerator rotator(int desiredno)
    {
        
        copyoforiginal.Clear();
        while (copyoforiginal.Count < 84)
        {
            foreach (var item in ballpos)
            {
                copyoforiginal.Add(item);
            }
        }
        Debug.Log("previousval :" + previousval);
        //Debug.Log("copyoforiginal :" + copyoforiginal.Count);
        
        if ( previousval > 0  ) 
        {
            copyoforiginal.RemoveRange(0,previousval );
            for (int i = 0; i < previousval + 1; i++)
            {
                copyoforiginal.Add(ballpos[i]);
            }
        }
        

        
        int[] tablenos = {5,6,11,2,3,7,1,4,0,12,10,9,8};//{5,22,34,15,3,24,36,13,1,37,27,10,25,29,12,8,19,31,18,6,21,33,16,4,23,35,14,2,0,28,9,26,30,11,7,20,32,17}; 
        for (int i = 0; i < tablenos.Length; i++)
        {
            if(desiredno == tablenos[i])
            {
                Debug.Log("yesssssssssssssssssssssssssss"+tablenos[i]+" the value of i is "+i);
                finalval = i;
                previousval = i;

                //copyoforiginal.RemoveRange(0,i);
                //finalval = initiaval + i;
                //value of i
                break;
            }
            else{
                //Debug.Log("no" + " " + tablenos[i]);
            }
        }
        
        
        
        for (int i = 0; i < 10;i++)
        {
            //Debug.Log("the position for "+i+" round was"+copyoforiginal[i].transform.position);
        }
        

        iTween.MoveTo(this.gameObject,iTween.Hash("path",copyoforiginal.ToArray(),"time",time,"easytype",easeType,"oncomplete","ballcomplete"));//ballpos
        
        
        yield return new WaitForSeconds(0f);
        ballrotator.instant.ball.GetComponent<Image>().enabled = true;
        
        
    }
    public void ballcomplete()
    {
        
    }
    public void newmoving()
    {
        duplicate.transform.position = basepos.position;
        duplicate.SetActive(true);
        while (copyoforiginal.Count >15)
        {
            copyoforiginal.RemoveAt(0);
            
        }
        iTween.MoveTo(this.gameObject,iTween.Hash("path",copyoforiginal.ToArray(),"time",2f,"easytype",easeType));
    }
    Transform lastpose;
    
}
