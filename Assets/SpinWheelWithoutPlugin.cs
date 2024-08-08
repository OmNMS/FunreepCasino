//using System.Threading.Tasks.Dataflow;
using System;
using System.Collections;
using System.Collections.Generic;
//using System.Diagnostics;
using UnityEngine;
using UnityEditor;
using TMPro;
using UnityEngine.UI;
using FunTarget.GamePlay;
using TripleChance.GamePlay;

public class SpinWheelWithoutPlugin : MonoBehaviour
{
    [SerializeField] private int currentImageIndex;
    [SerializeField] private int lastImageIndex;

    public AnimationCurve animationCurve;

    [SerializeField] FunTargetInnerWheel innerWheel;
    [SerializeField] private GameObject _fortuneWheel;
    [SerializeField] private GameObject _content;
    [SerializeField] private Sprite[] _awardImages;
    public static SpinWheelWithoutPlugin instane;
    public Action onSpinComplete;
    public float spaceBetweenTwoImages=100;//
    public bool isSpinning;
    public Transform centerObj;
    public Transform fortunewheeltransform;
    public float outerspeed;
    public AudioSource wheelsound;
    public Animator center;
    public GameObject middleobj;
    public GameObject[] middleimgs;
    public GameObject[] centerimg;
    [SerializeField] Sprite[] images;
    public GameObject animatioon;
    public Transform central;
    public Transform centerpos;
    public string currentvariable;
    public Transform lastslide;
    public Transform secondslide;
    int xfactor;

    private void Awake()
    {
        instane = this;
    }

    public float maxValue;
    public float minValue;
    bool answer;
    private void Start()
    {
        Debug.Log("SpinWheel cs is attached to : "+this.gameObject.name);
        _fortuneWheel = gameObject;
        lastImageIndex = 0;
        currentImageIndex = 0;
       
    }
    int[] angles = { 0, 36, 72, 108, -216, -180, -144, -108, -72, -36 };
    //int[] angles_01 = {-48, 142, -39, 180, 0, 218, 37, 255, 75, 293, 113, 265, 84, 151, -29, 189, 9, 227, 46, 65, 245, 27
    //    , 208, -10, 170, 103, 283, 122, 302, 94, 274, 56, 236, 18, 198, -20, 161};

    public Animator something;
    [SerializeField] int newnumber;
    public void testing()
    {
        //Spin(number,"2x");
        Spin(3,2);
        something.SetBool("isSpinning",true);
        //answer = false;
        //center.SetBool("second",true);
        //center.SetBool("inner",answer);
        //Spin(UnityEngine.Random.Range(0,9),str);
    }
    public void Spin(int wheelNo, int imageXfactor)
    {
        //UnityEngine.Debug.Log("Spin   " + wheelNo + "  image  " + imageXfactor);
        // if (!isSpinning)
        // {
            showuser.SetActive(false);
            desireNo = wheelNo;
            isSpinning = true;
            xfactor = imageXfactor;
            //RearangeAwardImages(imageXfactor);
            CalculateAngle();
            //currentvariable = imageXfactor;
            answer = true;
            StartCoroutine(rot(imageXfactor));
            something.SetBool("isSpinning",true);
            //SoundManager.instance?.PlayClip("spinwheel");
            initailDistance = centerObj.transform.position.x;
            //initalposX = _awardImages[currentImageIndex].transform.position.x;
            
        // }

    }
    public void setfirst(int value)
    {
        for (int i = 0; i < Posee.Length; i++)
        {
            if(Posee[i] == value)
            {
                lastpos = 36*i;
            }
        }
        // float rotator = (value) *36;
        // lastpose = (10-value)* 36;//rotator;
        transform.rotation = Quaternion.Euler(43,0,-lastpos);
        //transform.Rotate(0,0,-lastpos);
    }
    float temp = 0f;
    //public float[] Posee = {0,1,2,3,4,5,6,7,8,9}; 
    private float[] Posee = {0,9,8,7,6,5,4,3,2,1}; 
    float lastpos = 0;
    float GetDestPosition(int no  )
    {
        float ret = 2160;
        // if ( no == -1)
        // {
        //     ret += 1440 + (36f);// * 19);
        //     if (lastpos > 0)
        //     {
        //         ret -= lastpos;
        //         // Debug.Log("pos:" + ret + "  Last Psoe"+ lastpose );
        //         lastpos = 36f;// * 19;
        //         Debug.Log("Last Pos:"+lastpos);
        //     }
        //     else{
        //         lastpos = 36f; //* 19;
        //     }
        // }
        // else
        // {
        for (int i = 0; i < Posee.Length; i++)
        {
            //if (i == 19) continue;
            if (no == Posee[i] )
            {
                //Debug.Log("NO:"+no);
                //Debug.Log("Pos at i:"+i);
                //Debug.Log("vvbdbafaf"+ Posee[i]);
                ret += 36 * i;//
                //ret += 8f * i;
                //Debug.Log("REt:"+ret);
                if (lastpos > 0)
                {
                    ret -= lastpos;
                    // Debug.Log("pos:" + ret + "  Last Psoe"+ lastpose );
                    lastpos = 36* i;   
                    //lastpose = 8f * i;
                    //Debug.Log("Last Pos:"+lastpos);
                }
                else
                {
                    lastpos = 36* i;
                    //Debug.Log("Last Pos:"+lastpos);
                    //lastpose = 8f * i;
                }
                break;
            } 
        }        /// -360 - 360 + 36
                /// -360 -1
        //} 
        //Debug.Log("REt:"+ret);
        return ret;
    }   
    
    IEnumerator rot(int currentmultiple)
    {
        //Debug.Log("answeeeeeeeeeeeeer"+answer);
        //center.SetBool("inner",false);
        //Invoke("image")
        //animatioon.SetActive(true);
        //center.SetBool("inner",answer);
        innerWheel.SetRotation(currentmultiple);
        disable();
        iTween.RotateAdd(_fortuneWheel, iTween.Hash( "z", -GetDestPosition(desireNo ), "speed",outerspeed,  "easetype", easeType,"oncomplete","ballspinUpdate","oncompletetarget", _fortuneWheel ) );
        //center.SetBool("inner",true);  
        wheelsound.Play();
        //imagechanger(currentmultiple);
        yield return new WaitForSeconds(5f);
        //PlayerPrefs.SetInt("funwin",int.Parse(Win))
        //center.SetBool("second",false);
        //innerWheel.SpinWheelAnimation(false);
        innerWheel.SetRotation(currentmultiple);
        //FunTargetInnerWheel.SetRotation(currentmultiple);
        //imagechanger(currentmultiple);
        //changeimage(currentmultiple);
        //function(currentmultiple);
        //movenow();
        //AfterSpin(desireNo);
    }
    void imagechanger(int value)
    {
        lastslide.GetChild(0).GetComponent<Image>().sprite = images[value];
        lastslide.GetChild(7).GetComponent<Image>().sprite = images[value];
    }
    void changeimage(string multiple)
    {
        if(multiple == "2x")
        {
            lastslide.GetChild(0).GetComponent<Image>().sprite = images[5];
            secondslide.GetChild(0).GetComponent<Image>().sprite = images[5];
            //centerimg[5].SetActive(true);
        }
        else if(multiple == "1x")
        {
            //Debug.Log("reached here");
            int xyz = UnityEngine.Random.Range(0,4);
            //Debug.Log("the value of xyz" +xyz + " value of i" + i);
            //middleimgs[xyz].SetActive(true);
            //centerimg[xyz].SetActive(true);
            //Debug.Log("xyz"+xyz);
            lastslide.GetChild(0).GetComponent<Image>().sprite = images[xyz];
            secondslide.GetChild(0).GetComponent<Image>().sprite = images[xyz];
            // if(i == xyz)
            // {
            //     middleimgs[i].SetActive(true);
            // }
            
        }
        //StopCoroutine(inneranimation());
        //StartCoroutine(inneranimation());
    }
    IEnumerator inneranimation()
    {

        lastslide.GetChild(0).transform.localScale = new Vector3(1.2f,1.2f,1.2f);
        secondslide.GetChild(0).transform.localScale = new Vector3(1.2f,1.2f,1.2f);
        yield return new WaitForSeconds(0.5f);
        lastslide.GetChild(0).transform.localScale = new Vector3(1f,1f,1f);
        secondslide.GetChild(0).transform.localScale = new Vector3(1f,1f,1f);
        yield return new WaitForSeconds(0.5f);
        lastslide.GetChild(0).transform.localScale = new Vector3(1.2f,1.2f,1.2f);
        secondslide.GetChild(0).transform.localScale = new Vector3(1.2f,1.2f,1.2f);
        yield return new WaitForSeconds(0.5f);
        lastslide.GetChild(0).transform.localScale = new Vector3(1f,1f,1f);
        secondslide.GetChild(0).transform.localScale = new Vector3(1f,1f,1f);
        yield return new WaitForSeconds(0.5f);
        lastslide.GetChild(0).transform.localScale = new Vector3(1.2f,1.2f,1.2f);
        secondslide.GetChild(0).transform.localScale = new Vector3(1.2f,1.2f,1.2f);
        yield return new WaitForSeconds(0.5f);
        lastslide.GetChild(0).transform.localScale = new Vector3(1f,1f,1f);
        secondslide.GetChild(0).transform.localScale = new Vector3(1f,1f,1f);
        yield return new WaitForSeconds(0.5f);
        lastslide.GetChild(0).transform.localScale = new Vector3(1.2f,1.2f,1.2f);
        secondslide.GetChild(0).transform.localScale = new Vector3(1.2f,1.2f,1.2f);
        yield return new WaitForSeconds(0.5f);
        lastslide.GetChild(0).transform.localScale = new Vector3(1f,1f,1f);
        secondslide.GetChild(0).transform.localScale = new Vector3(1f,1f,1f);
        
        
    }
    public void function(string multiple)
    {
        Debug.Log("vgbfvbncfvbnjnm"+currentvariable);
        multiple = currentvariable;
        //animatioon.SetActive(false);
        Debug.Log("//////////////////////////////////////"+middleimgs.Length);
        // for (int i = 0; i < middleimgs.Length; i++)
        // {
        if(multiple == "2x")
        {
            //Debug.Log("the value of i" + i + " multiple value" + multiple);
            //middleimgs[5].SetActive(true);
            centerimg[5].SetActive(true);
        }
        else if(multiple == "1x")
        {
            Debug.Log("reached here");
            int xyz = UnityEngine.Random.Range(0,4);
            //Debug.Log("the value of xyz" +xyz + " value of i" + i);
            //middleimgs[xyz].SetActive(true);
            centerimg[xyz].SetActive(true);
            // if(i == xyz)
            // {
            //     middleimgs[i].SetActive(true);
            // }
            
        }
        //     else{
        //         middleimgs[i].SetActive(false);
        //     }
        // }
    }
    void movecentral()
    {
        central.position = Vector2.MoveTowards(central.position,centerpos.position,10f);
    }
    void disable()
    {
        for (int i = 0; i < middleimgs.Length; i++)
        {
            middleimgs[i].SetActive(false);
        }
    }
    void Update()
    {
        // if (isSpinning)
        // {
        //     // UnityEngine.Debug.LogError("anglesUntillNow  " + anglesUntillNow + "  totalAngles  " + totalAngles);
        //     if (anglesUntillNow < totalAngles)
        //     {
        //         float angle = speed * t;//t = 2 in editor


        //         Angle = angle;
        //         anglesUntillNow += Math.Abs(angle);

        //         float mapTime = animationCurve.Evaluate(
        //             NumberMapping(anglesUntillNow, 0, totalAngles,
        //                 rateOfChangeOfSpeed, initialTime));

        //         float distanceLeft = NumberMapping(anglesUntillNow, 0, totalAngles,
        //                 initalposX, initailDistance);
        //         remainDistance = distanceLeft;
        //         m = mapTime;
        //         t = mapTime;
        //         //move images

        //         _awardImages[currentImageIndex].transform.position = new Vector2(distanceLeft,_awardImages[currentImageIndex].transform.position.y);
        //         //rotate wheel
        //         //_fortuneWheel.transform.Rotate(0f, 0f, -angle);
        //         //iTween.RotateAdd(_fortuneWheel,iTween.Hash("destination", -angle,"time",speed, "easeType", easeType));
        //         //iTween.RotateUpdate(fortunewheeltransform,iTween.Hash("destination", -angle,"time",speed, "easeType", easeType));
        //         temp += Math.Abs(angle);
        //         if (temp > 360)
        //         {
        //             currentRound++;
        //             temp = 0;
        //         }
        //     }
        //     else
        //     {
        //         currentNo = desireNo;
        //         //AfterSpin(currentNo);
        //     }
        // }
    }
    public void ballspinUpdate()
    {
        
        currentNo = desireNo;
        AfterSpin(currentNo);
        center.SetBool("inner",false);  
        wheelsound.Stop();
        FunTargetGamePlay.Instance.stopcenteranim();
        //if(FunTargetGamePlay.Instance.winnigText)
        //FunTargetGamePlay.Instance.ResetAllBets();
        FunTargetGamePlay.Instance.takeBtn.interactable = true;
        


        // if (int.Parse(FunTargetGamePlay.Instance.winnigText.text) > 0)
        // {
        //     StartCoroutine(FunTargetGamePlay.Instance.takeBtn.GetComponent<ButtonBlinker>().TriggerBlinking());
        // }
        // else
        // {
        //     FunTargetGamePlay.Instance.ResetAllBets();
        // }
    }

    public iTween.EaseType easeType;
    // public IEnumerator rotate()
    // {
    //     while (anglesUntillNow < totalAngles)
    //     {
    //         float angle = speed * t;


    //         Angle = angle;
    //         anglesUntillNow += Math.Abs(angle);

    //         // float mapTime = animationCurve.Evaluate(
    //         //     NumberMapping(anglesUntillNow, 0, totalAngles,
    //         //         rateOfChangeOfSpeed, initialTime));

    //         float distanceLeft = NumberMapping(anglesUntillNow, 0, totalAngles,
    //                 initalposX, initailDistance);
    //         remainDistance = distanceLeft;
    //         // m = mapTime;
    //         // t = mapTime;
    //         //move images
    //         _awardImages[currentImageIndex].transform.position = new Vector2(distanceLeft,
    //              _awardImages[currentImageIndex].transform.position.y);
    //         //rotate wheel
    //         //_fortuneWheel.transform.Rotate(0f, 0f, -angle);
    //         iTween.RotateAdd(_fortuneWheel, iTween.Hash("destination", angle,"speed",speed, "easeType", easeType));
    //         temp += Math.Abs(angle);
    //         if (temp > 360)
    //         {
    //             currentRound++;
    //             temp = 0;
    //         }
    //         yield return new WaitForEndOfFrame();
    //     }
        
    //     currentNo = desireNo;
    //     AfterSpin(currentNo);
    //     // else
    //     // {
    //     //     currentNo = desireNo;
    //     //     AfterSpin(currentNo);
    //     // }
        
    // }
    public GameObject showuser;
    void AfterSpin(int wheelNo)
    {
        //_fortuneWheel.transform.eulerAngles = new Vector3(0, 0, angles[wheelNo]);
        isSpinning = false;
        //FunTargetGamePlay.Instance.ResetAllBets();
        //SoundManager.instance?.PlayClip("spinwheelend");
        something.SetBool("isSpinning",false);
        showuser.SetActive(true);
        showuser.GetComponent<SpriteRenderer>().sprite = images[xfactor];
        lastImageIndex = currentImageIndex;
        RemoveParents();
        StartCoroutine(inneranimation());
        
        //FunTargetGamePlay.Instance.wintextupdate();
        if (onSpinComplete != null)
            onSpinComplete();
    }

    public void initialimage(int xfactorss)
    {
        showuser.SetActive(true);
        showuser.GetComponent<SpriteRenderer>().sprite = images[xfactorss];
    }

    private void RemoveParents()
    {
        for (int i = 0; i < _awardImages.Length; i++)
        {
            if (i == currentImageIndex) continue;
            //_awardImages[i].transform.parent = _content.transform;
        }
    }

   

    public int desireNo = 5;
    public float speed;

    public float rateOfChangeOfSpeed = 0.01f;
    public int maxRounds = 10;
    public float singleAngle = 360 / 10;
    public float totalAngles;
    public float anglesUntillNow = 0f;
    public float initialTime = 1;
    public float t = 2f;
    public float m = 2f;
    public int currentNo = 0;
    public int currentRound = 0;
    public float offset = .10f;
    public float Angle;
    public float initailDistance;
    public float initalposX;
    public float remainDistance;
    public float moveTime;
    public bool useDeltaTime;
    private void CalculateAngle()
    {
        anglesUntillNow = 0f;
        currentRound = 1;
        t = initialTime;
        if (currentNo == desireNo)
        {
            totalAngles = maxRounds * 360;
        }
        else if (currentNo < desireNo)
        {
            totalAngles = (10 - Math.Abs(currentNo - desireNo)) * singleAngle + maxRounds * 360;
        }
        else
        {
            totalAngles = Math.Abs(currentNo - desireNo) * singleAngle + maxRounds * 360;

        }
    }


   
    float NumberMapping(float num, float in_min, float in_max, float out_min, float out_max)
    {
        return (num - in_min) * (out_max - out_min) / (in_max - in_min) + out_min;
    }
    public void SetWheelInitialAngle(int wheelNo, string xfactor)
    {
        print("set initialangle ");
        _fortuneWheel.transform.eulerAngles = new Vector3(0, 0, angles[wheelNo]);
        lastImageIndex = 0;
        if (xfactor == "4x")
        {
            lastImageIndex = _awardImages.Length - 1;
        }
        else if (xfactor == "2x")
        {
            lastImageIndex = _awardImages.Length - 2;
        }
        for (int i = 0; i < _awardImages.Length; i++)
        {
            //_awardImages[i].transform.parent = _content.transform;
            //_awardImages[i].transform.localPosition = new Vector3(200, _awardImages[i].transform.localPosition.y, 0);
        }
        currentNo = wheelNo;
       //_awardImages[lastImageIndex].transform.localPosition = new Vector3(0, _awardImages[currentImageIndex].transform.localPosition.y, 0);

    }
    private void RearangeAwardImages(string img_xFactor)
    {
        print("rearange xfactor is " + img_xFactor);
        if (img_xFactor == "1x")
        {
            while (lastImageIndex == currentImageIndex)
            {
                currentImageIndex = UnityEngine.Random.Range(0, _awardImages.Length - 4);
            }
        }
        else if (img_xFactor == "2x")
        {
            currentImageIndex = lastImageIndex == _awardImages.Length - 4 ? _awardImages.Length - 2 : _awardImages.Length - 4;
        }
        else if (img_xFactor == "4x")
        {
            currentImageIndex = lastImageIndex == _awardImages.Length - 3 ? _awardImages.Length - 1 : _awardImages.Length - 3;
        }
        //_awardImages[currentImageIndex].transform.SetAsLastSibling();
        //_awardImages[lastImageIndex].transform.SetAsFirstSibling();
        //float finalXCoordinate = (_awardImages[currentImageIndex].GetComponent<RectTransform>().rect.width * (_awardImages.Length - 1) + spaceBetweenTwoImages * (_awardImages.Length - 1));
        //_awardImages[currentImageIndex].transform.localPosition = new Vector2(-finalXCoordinate, _awardImages[currentImageIndex].transform.localPosition.y);

        int index = 0;
        foreach (Transform image in _content.transform)
        {

            float xCoordinate = 0;
            if (index == 0)
            {
                xCoordinate = 0f;
            }
            else xCoordinate = (100 * index + spaceBetweenTwoImages * index);
            image.transform.localPosition = new Vector2(-xCoordinate, image.transform.localPosition.y);
            index++;
        }
        for (int i = 0; i < _awardImages.Length; i++)
        {
            if (i == currentImageIndex) continue;
            //_awardImages[i].transform.parent = _awardImages[currentImageIndex].transform;
        }
    }

    public void ForceFullyStopWheel()
    {
        if (isSpinning)
        {
            iTween.Stop(_fortuneWheel);
            //iTween.Stop(_awardImages[currentImageIndex], true);
            _fortuneWheel.transform.eulerAngles = new Vector3(0, 0, 0);
            lastImageIndex = currentImageIndex;

            for (int i = 0; i < _awardImages.Length; i++)
            {
                //_awardImages[i].transform.parent = _content.transform;
                //_awardImages[i].transform.localPosition = new Vector3(200, _awardImages[i].transform.localPosition.y, 0);
            }
            //_awardImages[currentImageIndex].transform.localPosition = new Vector3(0, _awardImages[currentImageIndex].transform.localPosition.y, 0);
            isSpinning = false;
        }
    }
    float[] Pose = {0,1,2,3,4,5,6,7,8,9};
    float lastpose = 0;
    public float destinationposition(int no)
    {
        float ret = 1080;
        if ( no == -1)
        {
            ret += 1440 + (9.47f * 19);
            if (lastpose > 0)
            {
                ret -= lastpose;
                // Debug.Log("pos:" + ret + "  Last Psoe"+ lastpose );
                lastpose = 9.47f * 19;
                
                //Debug.Log("Last Pos:"+lastpose);
            }
            else{
                lastpose = 9.47f * 19;
            }
        }
        else
        {
            for (int i = 0; i < Pose.Length; i++)
            {
                if (i == 19) continue;
                if (no == Pose[i] )
                {
                    //Debug.Log("NO:"+no);
                    //Debug.Log("Pos at i:"+i);
                    ret += 9.47f * i;
                    //Debug.Log("REt:"+ret);
                    if (lastpose > 0)
                    {
                        ret -= lastpose;
                        // Debug.Log("pos:" + ret + "  Last Psoe"+ lastpose );
                        lastpose = 9.47f * i;
                        //Debug.Log("Last Pos:"+lastpose);
                    }
                    else{
                        lastpose = 9.47f * i;
                    }
                    break;
                } 
            }        
        } 
        //Debug.Log("REt:"+ret);
        return ret;
    }

}

