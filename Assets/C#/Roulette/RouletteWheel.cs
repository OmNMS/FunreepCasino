 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using static Com.BigWin.Frontend.DoubleChanceScreen;
using UnityEngine.UI;
using System;

public class RouletteWheel : MonoBehaviour
{
    public static TripleFunWheel instance;
    #region OuterWheel

    [SerializeField]float outerwheelangle;
    [SerializeField] int currentOuterWheelNumber;
    [SerializeField] int nextOuterWheelNumber;
    #endregion

    #region InnerWheel
    [SerializeField] float innerWheelAngle;
    [SerializeField] int currentInnerWheelNumber;
    [SerializeField] int nextInnerWheelNumber;
    #endregion


    #region MidWheel
    [SerializeField] float midWheelAngle;
    [SerializeField] int currentmidWheelNumber;
    [SerializeField] int nextmidWheelNumber;
    #endregion

    public static RouletteWheel instan;
    #region Common Arguments
    public int wheelTime = 7;                 //7
    public int noOfRounds = 3;
    public bool isStarted;
    [SerializeField] GameObject outterWheel, innerWheel,midWheel;
    #endregion
    public Action OnSpinComplete;

    //int[] angles = { 0, 36, 72, 108, -216, -180, -144, -108, -72, -36 };

    // public Button spinBtn;
    public int testNum1;
    public int testNum2;
    public int testNum3;

    int outerIndex = 0;
    int innerIndex = 0;
    int midIndex = 0;
    public iTween.EaseType easetype;
    public GameObject _defaultBall;
    int _generateNumber;

    // Start is called before the first frame update
    void Start()
    {
        instan = this;
    }

    void Update()
    {
        
    }

    public void Btn()
    {
        int randomnum = UnityEngine.Random.Range(1, 36);
        //Spin(randomnum, 5, 3);
    }
    int[] angles = { 0, 36, 72, 108, -216, -180, -144, -108, -72, -36 };
    int[] angles_01 = {-48, 142, -39, 180, 0, 218, 37, 255, 75, 293, 113, 265, 84, 151, -29, 189, 9, 227, 46, 65, 245, 27
        , 208, -10, 170, 103, 283, 122, 302, 94, 274, 56, 236, 18, 198, -20, 161};

    //ublic void Spin(int wheelno)
    /*int innerNum, int midNum, int outerNum
    {
        desireNo = wheelNo;
        isSpinning = true;

        CalculateAngle();
        StartCoroutine(rot());
    }
    {
        Debug.Log("Spin");
        int[] outerWheelNumbers = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        int[] innerWheelNumbers = new int[] {00, 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18,
        19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36 };
        int[] midWheelnumbers = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        

        for (int i = 0; i < outerWheelNumbers.Length; i++)
        {
            if (outerWheelNumbers[i] == outerNum)
            {
                outerIndex =  i;
            //   Debug.Log(" outer index is " + outerIndex);
            break;
            }
        }
        for (int i = 0; i < innerWheelNumbers.Length; i++)
        {
            if (innerWheelNumbers[i] == innerNum)
            {
                Debug.Log(innerNum);
                innerIndex = i;
                //   Debug.Log("inner index is " + innerIndex);
                break;
            }
        }


        for (int i = 0; i < midWheelnumbers.Length; i++)
        {
            if (midWheelnumbers[i] == midNum)
            {
                midIndex =  i;
                //  Debug.Log("index is " + midIndex);
                break;
            }
        }

        _generateNumber = innerIndex;
        InnerWheel(innerIndex);
    }

    void InnerWheel(int number)
    {
        nextmidWheelNumber = number;
        if (currentmidWheelNumber == nextmidWheelNumber)
        {
            innerWheelAngle = 0;
        }
        else if (currentmidWheelNumber > nextmidWheelNumber)
        {
            innerWheelAngle = Mathf.Abs(currentmidWheelNumber - nextmidWheelNumber) / 38f;
        }
        else
        {
            innerWheelAngle = Mathf.Abs(38- (nextmidWheelNumber - currentmidWheelNumber)) / 38f;

        }
        innerWheelAngle += noOfRounds;
        iTween.RotateBy(innerWheel, iTween.Hash("z", innerWheelAngle, "time", wheelTime, "easetype", easetype, 
                "oncomplete",  "OnAnimationComplete", "oncompletetarget", this.gameObject));
    }

    void OnAnimationComplete()
    {
        // innerWheel.transform.GetChild(_generateNumber).gameObject.SetActive(true);
        // _defaultBall.SetActive(true);
        // StartCoroutine(Reset());
    }

    IEnumerator Reset()
    {
        yield return new WaitForSeconds(1.0f);
        innerWheel.transform.GetChild(_generateNumber).gameObject.SetActive(false);
        _defaultBall.SetActive(true);
    }*/
}
