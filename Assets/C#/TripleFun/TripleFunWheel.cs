 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using static Com.BigWin.Frontend.DoubleChanceScreen;
using UnityEngine.UI;
using System;
using TripleChance.GamePlay;


public class TripleFunWheel : MonoBehaviour
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


        #region Common Arguments
        public int wheelTime = 8;                 //7
        public int noOfRounds = 7;
        public bool isStarted;
        [SerializeField] GameObject outterWheel, innerWheel,midWheel,thewheel;
        #endregion
        public Action OnSpinComplete;

        [SerializeField] TripleFunScreen TFS;
        int[] angles = { 0, 36, 72, 108, -216, -180, -144, -108, -72, -36 };

        // public Button spinBtn;
        public int testNum1;
        public int testNum2;
        public int testNum3;

        int outerIndex = 0;
        int innerIndex = 0;
        int midIndex = 0;

        [SerializeField] List<Text> Single_Last5;
        [SerializeField] List<Text> Double_Last5;
        [SerializeField] List<Text> Triple_Last5;
        public List<int> PreviousWin_Single, PreviousWin_Double, PreviousWin_Triple;

        
    


        private void Awake()
        {
            instance = this;
        }

        private void Start()
            {
            //spinBtn.onClick.AddListener(() =>
            //{
            //    Spin(testNum1, testNum2, testNum3);
            //});
        }
        public iTween.EaseType easetype;
        public void testing()
        {
            Spin(4,8,7);
        }
        public void setinitialrotation(int single,int doubleno,int triple)
        {
            int[] outerWheelNumbers = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            int[] innerWheelNumbers = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            int[] midWheelnumbers = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            

            for (int i = 0; i < outerWheelNumbers.Length; i++)
            {
                if (outerWheelNumbers[i] == triple)
                {
                    outerIndex =  i;
                    currentOuterWheelNumber = outerWheelNumbers[i];
                    outterWheel.transform.Rotate(0,0,i*36);
                //   Debug.Log(" outer index is " + outerIndex);
                break;
                }
            }
            for (int i = 0; i < innerWheelNumbers.Length; i++)
            {
                if (innerWheelNumbers[i] == single)
                {
                    Debug.Log(single);
                    innerIndex = i;
                    currentInnerWheelNumber = innerWheelNumbers[i];
                    innerWheel.transform.Rotate(0,0,i*36);
                    //   Debug.Log("inner index is " + innerIndex);
                    break;
                }
            }


            for (int i = 0; i < midWheelnumbers.Length; i++)
            {
                if (midWheelnumbers[i] == doubleno)
                {
                    midIndex =  i;
                    currentmidWheelNumber = midWheelnumbers[i];
                    midWheel.transform.Rotate(0,0,i*36);
                    //  Debug.Log("index is " + midIndex);
                    break;
                }
            }
            //transfrom.Rotate(innerWheel)
            
            //InnerWheel_02(innerIndex);
        }

        int hundredvalue;
        int tensvalue;
       
        public void Spin(int innerNum, int midNum, int outerNum )
        {
            int temp;
            Debug.Log("Spintill " +innerNum+" "+midNum+" "+outerNum );
            
            if(outerNum > 100)
            {
                hundredvalue = (outerNum/100)%10;
            }
            if(midNum>10)
            {
                tensvalue = (midNum/10)%10;
            }
            int[] outerWheelNumbers = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            int[] innerWheelNumbers = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            int[] midWheelnumbers = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            

            for (int i = 0; i < outerWheelNumbers.Length; i++)
            {
                if (outerWheelNumbers[i] == hundredvalue)
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
                    //Debug.Log(innerNum);
                    innerIndex = i;
                    //   Debug.Log("inner index is " + innerIndex);
                    break;
                }
            }


            for (int i = 0; i < midWheelnumbers.Length; i++)
            {
                if (midWheelnumbers[i] == tensvalue)
                {
                    midIndex =  i;
                    //  Debug.Log("index is " + midIndex);
                    break;
                }
            }
            // Debug.LogError("Outer = " + outerIndex);
            // Debug.LogError("mid = " + midIndex);
            // Debug.LogError("Inner = " + innerIndex);
            // OuterWheel(outerIndex);
            // InnerWheel(innerIndex);
            // MidWheel(midIndex);

            //OuterWheel_02(outerIndex);
            InnerWheel_02(innerIndex);
            MidWheel_02(midIndex);
            StartCoroutine(outerwheel_03(outerIndex));
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
                innerWheelAngle = Mathf.Abs(currentmidWheelNumber - nextmidWheelNumber) / 10f;
            }
            else
            {
                innerWheelAngle = Mathf.Abs(10- (nextmidWheelNumber - currentmidWheelNumber)) / 10f;

            }
            innerWheelAngle += noOfRounds;
            iTween.RotateBy(innerWheel, iTween.Hash("z", -innerWheelAngle, "time", wheelTime,
                    "easetype", easetype, "oncompletetarget", this.gameObject));

        }

        void InnerWheel_02(int number)
        {
            nextInnerWheelNumber = number;
            if (currentInnerWheelNumber == nextInnerWheelNumber)
            {
                innerWheelAngle = 0;
            }
            else if (currentInnerWheelNumber > nextInnerWheelNumber)
            {
                innerWheelAngle = Mathf.Abs(currentInnerWheelNumber - nextInnerWheelNumber) / 10f;
            }
            else
            {
                innerWheelAngle = Mathf.Abs(10 - (nextInnerWheelNumber - currentInnerWheelNumber)) / 10f;

            }
            innerWheelAngle += noOfRounds;
            
            iTween.RotateBy(innerWheel, iTween.Hash("z", -innerWheelAngle, "time", wheelTime, 
                    "easetype", easetype, "oncompletetarget", this.gameObject));

        }

        void MidWheel(int number)
        {
            nextOuterWheelNumber = number;
            // Debug.LogError("nextOuterWheelNumber  " + nextOuterWheelNumber + "    currentOuterWheelNumber   " + currentOuterWheelNumber);
            if (currentOuterWheelNumber == nextOuterWheelNumber)
            {
                midWheelAngle = 0;
            }
            else if (currentOuterWheelNumber > nextOuterWheelNumber)
            {
                midWheelAngle = Mathf.Abs(currentOuterWheelNumber - nextOuterWheelNumber) / 10f;
            }
            else
            {
                midWheelAngle = Mathf.Abs((nextOuterWheelNumber - currentOuterWheelNumber)) / 10f;

            }
            midWheelAngle += noOfRounds;
            // iTween.RotateBy(midWheel, iTween.Hash("z", midWheelAngle, "time", wheelTime +1,
            //         "easetype", easetype, "oncompletetarget", this.gameObject));
            // iTween.RotateBy(midWheel, iTween.Hash("z", -midWheelAngle, "time", wheelTime + 1, "easetype", easetype, 
            //     "oncomplete", "OnAnimationComplete", "oncompletetarget", this.gameObject));

            iTween.RotateBy(midWheel, iTween.Hash("z", midWheelAngle, "time", wheelTime,
                 "easetype", easetype, "oncompletetarget", this.gameObject));

        }

        void MidWheel_02(int number)
        {
            nextmidWheelNumber = number;
            // Debug.LogError("nextmidWheelNumber  " + nextmidWheelNumber + "    currentmidWheelNumber   " + currentmidWheelNumber);
            if (currentmidWheelNumber == nextmidWheelNumber)
            {
                midWheelAngle = 0;
            }
            else if (currentmidWheelNumber > nextmidWheelNumber)
            {
                midWheelAngle = Mathf.Abs( 10 - (currentmidWheelNumber - nextmidWheelNumber) ) / 10f;
            }
            else
            {
                midWheelAngle = Mathf.Abs(nextmidWheelNumber - currentmidWheelNumber) / 10f;

            }
            midWheelAngle += noOfRounds;
            iTween.RotateBy(midWheel, iTween.Hash("z", midWheelAngle, "time", wheelTime,
                    "easetype", easetype, "oncompletetarget", this.gameObject));

        }

        void OuterWheel(int number)
        {
            nextInnerWheelNumber = number;
            if (currentInnerWheelNumber == nextInnerWheelNumber)
            {
                outerwheelangle = 0;
            }
            else if (currentInnerWheelNumber > nextInnerWheelNumber)
            {
                outerwheelangle = Mathf.Abs(currentInnerWheelNumber - nextInnerWheelNumber) / 10f;
            }
            else
            {
                outerwheelangle = Mathf.Abs(10 - (nextInnerWheelNumber - currentInnerWheelNumber)) / 10f;

            }
            outerwheelangle += noOfRounds;
            iTween.RotateBy(outterWheel, iTween.Hash("z", -outerwheelangle, "time", wheelTime + 1, "easetype", easetype,
                "oncomplete", "OnAnimationComplete", "oncompletetarget", this.gameObject));
        }

        void OuterWheel_02(int number)
        {
            nextOuterWheelNumber = number;
            if (currentOuterWheelNumber == nextOuterWheelNumber)
            {
                outerwheelangle = 0;
            }
            else if (currentOuterWheelNumber > nextOuterWheelNumber)
            {
                outerwheelangle = Mathf.Abs(currentOuterWheelNumber - nextOuterWheelNumber) / 10f;
            }
            else
            {
                outerwheelangle = Mathf.Abs(10 - (nextOuterWheelNumber - currentOuterWheelNumber)) / 10f;

            }
            outerwheelangle += noOfRounds;
            iTween.RotateBy(outterWheel, iTween.Hash("z", -outerwheelangle, "time", wheelTime, "easetype", easetype,
                "oncomplete", "OnAnimationComplete", "oncompletetarget", this.gameObject));
            //Debug.Log("audio sssssssssssssssttttttttttttttttttttooooooooooooooooppppppppppppp");
        }
        IEnumerator outerwheel_03(int number)
        {
            Debug.Log("outer number: "+number);
            nextOuterWheelNumber = number;
            if (currentOuterWheelNumber == nextOuterWheelNumber)
            {
                outerwheelangle = 0;
            }
            else if (currentOuterWheelNumber > nextOuterWheelNumber)
            {
                outerwheelangle = Mathf.Abs(currentOuterWheelNumber - nextOuterWheelNumber) / 10f;
            }
            else
            {
                outerwheelangle = Mathf.Abs(10 - (nextOuterWheelNumber - currentOuterWheelNumber)) / 10f;

            }
            outerwheelangle += noOfRounds;
            TripleFunScreen.Instance.spin_AudioSource.GetComponent<AudioSource>().Play();
            iTween.RotateBy(outterWheel, iTween.Hash("z", -outerwheelangle, "time", wheelTime, "easetype", easetype,
                "oncomplete", "OnAnimationComplete", "oncompletetarget", this.gameObject));
            
            yield return new WaitForSeconds(wheelTime-0.1f);
            TripleFunScreen.Instance.spin_AudioSource.GetComponent<AudioSource>().Stop();
            //Debug.Log("audio sssssssssssssssttttttttttttttttttttooooooooooooooooppppppppppppp");
        }

        int doubleWin;
        int tripleWin;
        void OnAnimationComplete()
        {
            //Debug.Log("audio sssssssssssssssttttttttttttttttttttooooooooooooooooppppppppppppp");
            TripleFunScreen.Instance.spin_AudioSource.GetComponent<AudioSource>().Stop();
            
            TripleFunScreen.Instance.UiAnimation_Obj.GetComponent<UiAnimation>().Stop_WheelRotation();
            TripleFunScreen.Instance.UiAnimation_Obj.GetComponent<UiAnimation>()._playWheel_rotation = false;
            TripleFunScreen.Instance.UiAnimation_Obj.GetComponent<UiAnimation>().WheelImg.gameObject.SetActive(false);
            StartCoroutine(ZoomOut_Anim());
            //TripleFunScreen.Instance.spin_AudioSource.GetComponent<AudioSource>().Stop();
            // TripleFunScreen.Instance.UiAnimation_Obj.GetComponent<UiAnimation>().Stop_WheelRotation();
            OnSpinComplete?.Invoke();
            innerWheelAngle = noOfRounds - innerWheelAngle;
            currentmidWheelNumber = nextmidWheelNumber;

            outerwheelangle = noOfRounds - outerwheelangle;
            currentInnerWheelNumber = nextInnerWheelNumber;

            midWheelAngle = noOfRounds - midWheelAngle;
            currentOuterWheelNumber = nextOuterWheelNumber;

            //  TFS.singleGridBtns[]
            // Timer_TripleFun.time = 30;
            doubleWin = int.Parse(midIndex.ToString() + innerIndex.ToString());
            tripleWin = int.Parse(outerIndex.ToString() + midIndex.ToString() + innerIndex.ToString());
            TripleFunScreen.Instance.WinNo_txt.gameObject.SetActive(true);
            TripleFunScreen.Instance.WinNo_txt.text = tripleWin.ToString();
            // TripleFunScreen.Instance.FillerList[0].SetActive(true);
            // TripleFunScreen.Instance.FillerList[1].SetActive(true);
            // TripleFunScreen.Instance.FillerList[0].GetComponent<Animation>().Play("TripleChance_FillerAnim");
            // TripleFunScreen.Instance.FillerList[1].GetComponent<Animation>().Play("TripleChance_FillerAnim");
            TripleFunScreen.Instance.OuterWheel_txt.text = outerIndex.ToString();
            TripleFunScreen.Instance.MediumWheel_txt.text = midIndex.ToString();
            TripleFunScreen.Instance.InnerWheel_txt.text = innerIndex.ToString();
            arrowcount = 0;
            StartCoroutine(arrowblink());
            //TripleFunScreen.Instance.UiAnimation_Obj.GetComponent<UiAnimation>().LeftStokesImg.gameObject.SetActive(true);
            //TripleFunScreen.Instance.UiAnimation_Obj.GetComponent<UiAnimation>().RightStokesImg.gameObject.SetActive(true);
            TripleFunScreen.Instance.UiAnimation_Obj.GetComponent<UiAnimation>().OuterWheelCut_Img.gameObject.SetActive(true);
            TripleFunScreen.Instance.UiAnimation_Obj.GetComponent<UiAnimation>().MediumWheelCut_Img.gameObject.SetActive(true);
            TripleFunScreen.Instance.UiAnimation_Obj.GetComponent<UiAnimation>().InnerWheelCut_Img.gameObject.SetActive(true);
            //StartCoroutine(TripleFunScreen.Instance.UiAnimation_Obj.GetComponent<UiAnimation>().Start_LeftStrokeAnimation());
            //StartCoroutine(TripleFunScreen.Instance.UiAnimation_Obj.GetComponent<UiAnimation>().Start_RightStrokeAnimation());
            //StartCoroutine(TripleFunScreen.Instance.UiAnimation_Obj.GetComponent<UiAnimation>().Start_OuterWheelAnimation());
            //StartCoroutine(TripleFunScreen.Instance.UiAnimation_Obj.GetComponent<UiAnimation>().Start_MediumWheelAnimation());
            //StartCoroutine(TripleFunScreen.Instance.UiAnimation_Obj.GetComponent<UiAnimation>().Start_InnerWheelAnimation());
            StartCoroutine(StopBlinkAnimation());
            // TFS.added =true;
            //StartCoroutine(TFS.WinAnimation(doubleWin,innerIndex, tripleWin));
            //TFS.changetriple(tripleWin);
            SetLastFive();
        }
        int arrowcount = 0;
        IEnumerator arrowblink()
        {
            TripleFunScreen.Instance.UiAnimation_Obj.GetComponent<UiAnimation>().LeftStokesImg.gameObject.SetActive(true);
            TripleFunScreen.Instance.UiAnimation_Obj.GetComponent<UiAnimation>().RightStokesImg.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            TripleFunScreen.Instance.UiAnimation_Obj.GetComponent<UiAnimation>().LeftStokesImg.gameObject.SetActive(false);
            TripleFunScreen.Instance.UiAnimation_Obj.GetComponent<UiAnimation>().RightStokesImg.gameObject.SetActive(false);
            arrowcount++;
            yield return new WaitForSeconds(0.5f);
            if(arrowcount <8)
            {
                StartCoroutine(arrowblink());
            }
        }

        IEnumerator ZoomOut_Anim()
        {
            yield return new WaitForSeconds(2.0f);
            TripleFunScreen.Instance.Wheel.GetComponent<Animation>().Play("TripleFun_ZoomOut");
            TripleFunScreen.Instance.Wheel_shadowPanel.SetActive(false);
            // TripleFunScreen.Instance.UiAnimation_Obj.GetComponent<UiAnimation>().WheelImg.sprite = TripleFunScreen.Instance.UiAnimation_Obj.GetComponent<UiAnimation>().Original_WheelImg;
        }

        IEnumerator StopBlinkAnimation()
        {
            yield return new WaitForSeconds(10.0f);
            TripleFunScreen.Instance.UiAnimation_Obj.GetComponent<UiAnimation>()._wheelAnim = false;
            TripleFunScreen.Instance.UiAnimation_Obj.GetComponent<UiAnimation>().Stop_LeftStrokeAnimation();
            TripleFunScreen.Instance.UiAnimation_Obj.GetComponent<UiAnimation>().Stop_RightStrokeAnimation();
            TripleFunScreen.Instance.UiAnimation_Obj.GetComponent<UiAnimation>().Stop_OuterWheelAnimation();
            TripleFunScreen.Instance.UiAnimation_Obj.GetComponent<UiAnimation>().Stop_MediumWheelAnimation();
            TripleFunScreen.Instance.UiAnimation_Obj.GetComponent<UiAnimation>().Stop_InnerWheelAnimation();
        }

        

        int count = 0;
        public void SetLastFive()
        {
            // if (count > 4)
            // {
            //     count = 4;
            // }
            // for(int i = 0; i < PreviousWin_Single.Count; i++)
            // {
            //     Single_Last5[i].GetComponent<Text>().text =  PreviousWin_Single[i].ToString();
            // }

            // for(int i = 0; i < PreviousWin_Double.Count; i++)
            // {
            //     Double_Last5[i].GetComponent<Text>().text =  PreviousWin_Double[i].ToString();
            // }

            for(int i = 0; i < PreviousWin_Triple.Count; i++)
            {
                //Debug.LogError("reached here");
                Triple_Last5[i].GetComponent<Text>().text =  PreviousWin_Triple[i].ToString();
            }

            // Single_Last5[count].GetComponent<Text>().text = innerIndex.ToString();
            // Double_Last5[count].GetComponent<Text>().text = doubleWin.ToString();
            // Triple_Last5[count].GetComponent<Text>().text = tripleWin.ToString();
            //count++;
        }
}
