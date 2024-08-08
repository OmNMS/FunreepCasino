//using System.Threading.Tasks.Dataflow;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
// using System.Diagnostics;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ballrotator : MonoBehaviour
{
    public static ballrotator instant;
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
        // while (copyoforiginal.Count < 107)
        // {
        //     foreach (var item in ballpos)
        //     {
        //         copyoforiginal.Add(item);
        //     }
        // }

        // copyoforiginal.RemoveRange(0,previousval-1);
        // for (int i = 0; i < previousval; i++)
        // {
        //     copyoforiginal.Add(ballpos[i]);
        // }
        StartCoroutine(rotator(number));
    }
    public IEnumerator values( int number)
    {
        int[] tablenos = {5,22,34,15,3,24,36,13,1,37,27,10,25,29,12,8,19,31,18,6,21,33,16,4,23,35,14,2,0,28,9,26,30,11,7,20,32,17}; 
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
        int[] tablenos = {5,22,34,15,3,24,36,13,1,37,27,10,25,29,12,8,19,31,18,6,21,33,16,4,23,35,14,2,0,28,9,26,30,11,7,20,32,17}; 
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
        while (copyoforiginal.Count < 107)
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
        

        
        int[] tablenos = {5,22,34,15,3,24,36,13,1,37,27,10,25,29,12,8,19,31,18,6,21,33,16,4,23,35,14,2,0,28,9,26,30,11,7,20,32,17}; 
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
        
        
        // for (int i = 0; i <= finalval; i++)
        // {
        //     //Transform pos = ballpos[i];
        //     copyoforiginal.Add(ballpos[i]);
        // }
        for (int i = 0; i < 10;i++)
        {
            //Debug.Log("the position for "+i+" round was"+copyoforiginal[i].transform.position);
        }
        

        iTween.MoveTo(this.gameObject,iTween.Hash("path",copyoforiginal.ToArray(),"time",time,"easytype",easeType,"oncomplete","ballcomplete"));//ballpos
        
        //ball.SetActive(true);
        //Debug.Log("the ball must be set true");
        yield return new WaitForSeconds(0f);
        ballrotator.instant.ball.GetComponent<Image>().enabled = true;
        //ball.SetActive(true);
        /*for(int i = 0; i<(ballpos.Length)*3;i++)
        {   
            /for (int j = 0; j < ballpos.Length; j++)
            {
                iTween.MoveUpdate(this.gameObject,iTween.Hash("position",ballpos[j].position,"speed",speed));
                if(j > ballpos.Length)
                {
                    //iTween.MoveUpdate(this.gameObject,iTween.Hash("position",ballpos[j].position,"speed",speed));
                    j = 0;
                }
                //iTween.MoveUpdate(this.gameObject,iTween.Hash("position",ballpos[i].position,"speed",speed));
            }
            //transform.position = ballpos[i].position;
            //iTween.MoveTo(this.gameObject,iTween.Hash("position",ballpos[i].position,"speed",speed,"easetype",easeType));
            //iTween.MoveAdd(this.gameObject,iTween.Hash("position",ballpos[i].position,"time",time,"easetype",easeType));
            //iTween.MoveAdd(this.gameObject,iTween.Hash("position",ballpos[i].position,"speed",speed,"easetype",easeType));
            //iTween.MoveFrom(this.gameObject,iTween.Hash("position",ballpos[i].position,"speed",speed,"easetype",easeType));
            Debug.Log("position = " +ballpos[i].position);
            

            //iTween.MoveTo(this.gameObject,ballpos[i].position,0.2f);
            //ball.transform.position = ballpos[i].position;
            yield return null;
            //yield return new WaitForSeconds(0.02f);
        }*/
        
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
    // public Transform[] something()
    // {
    
    //         // Debug.Log("ffffffffffffffffffffffffffffffffffffffffffffffffff" +finalval);
    //     // if(finalval ==0 )
    //     // {
    //     //     return copyoforiginal.ToArray();
    //     // }
    //     // else{
    //     //     copyoforiginal.RemoveRange(finalval,copyoforiginal.Count -1);
    //     // }
    //     // return copyoforiginal.ToArray();

        
    //     //copyoforiginal.Clear();
    // }

   



    /*
    [SerializeField] private int currentImageIndex;
    [SerializeField] private int lastImageIndex;

    public AnimationCurve animationCurve;
    // public List<MeshRenderer> Positions;
    // public List<BoxCollider> positions;
    [SerializeField] private GameObject _fortuneWheel;
    [SerializeField] private GameObject _content;
    [SerializeField] private GameObject[] _awardImages;
    public static ballrotator instan;
    public Action onSpinComplete;
    public float spaceBetweenTwoImages=100;
    public bool isSpinning;
    public Transform centerObj;

    private void Awake()
    {
        instan = this;
    }

    

    public float maxValue;
    public float minValue;
    private void Start()
    {
        _fortuneWheel = gameObject;
        lastImageIndex = 0;
        currentImageIndex = 0;

        
       
    }
    int[] angles = { 0, 36, 72, 108, -216, -180, -144, -108, -72, -36 };
    int[] angles_01 = {-48, 142, -39, 180, 0, 218, 37, 255, 75, 293, 113, 265, 84, 151, -29, 189, 9, 227, 46, 65, 245, 27
        , 208, -10, 170, 103, 283, 122, 302, 94, 274, 56, 236, 18, 198, -20, 161};

    public void SpinTester()
    {
        int whelNo = UnityEngine.Random.Range(0,36);
        Spin(whelNo);
    }

    public void Spin(int wheelNo)
    {
        
        desireNo = wheelNo;
        isSpinning = true;
            

        CalculateAngle();
        StartCoroutine(rot());

    }
    float temp = 0f;
    void Update()
    {
    }
    Dictionary<int, float> Position = new Dictionary<int, float> {
        {0, 9.47f * 19 },
        {1, 9.47f * 0 },
        {2, 9.47f * 18 },
        {3, 9.47f * 19 },
        {4, 9.47f * 19 },
        {5, 9.47f * 19 },
        {6, 9.47f * 19 },
        {7, 9.47f * 19 },
        {8, 9.47f * 19 },
        {9, 9.47f * 19 },
        {10, 9.47f * 19 },
        {11, 9.47f * 19 },
        {12, 9.47f * 19 },
        {13, 9.47f * 19 },
        {14, 9.47f * 19 },
        {15, 9.47f * 19 },
        {16, 9.47f * 19 },
        {17, 9.47f * 19 },
        {18, 9.47f * 19 },
        {19, 9.47f * 19 },
        {20, 9.47f * 19 },
        {21, 9.47f * 19 },
        {22, 9.47f * 19 },
        {23, 9.47f * 19 },
        {24, 9.47f * 19 },
        {25, 9.47f * 19 },
        {26, 9.47f * 19 },
        {27, 9.47f * 19 },
        {28, 9.47f * 19 },
        {29, 9.47f * 19 },
        {30, 9.47f * 19 },
        {31, 9.47f * 19 },
        {32, 9.47f * 19 },
        {33, 9.47f * 19 },
        {34, 9.47f * 19 },
        {35, 9.47f * 19 },
        {36, 9.47f * 19 },
        {37, 9.47f * 19 },
    };
    public float[] Pose = {0,28,9,26,30,11,7,20,32,17,5,22,34,15,3,24,36,13,1,00,27,10,25,29,12,8,19,31,18,6,21,33,16,4,23,35,14,2}; 
    float lastpose = 0;
    float GetDestPosition(int no  )
    {
        float ret = 1440;
        if ( no == -1)
        {
            ret += 1440 + (9.47f * 19);
            if (lastpose > 0)
            {
                ret -= lastpose;
                // Debug.Log("pos:" + ret + "  Last Psoe"+ lastpose );
                lastpose = 9.47f * 19;
                Debug.Log("Last Pos:"+lastpose);
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
                    Debug.Log("NO:"+no);
                    Debug.Log("Pos at i:"+i);
                    ret += 9.47f * i;
                    Debug.Log("REt:"+ret);
                    if (lastpose > 0)
                    {
                        ret -= lastpose;
                        // Debug.Log("pos:" + ret + "  Last Psoe"+ lastpose );
                        lastpose = 9.47f * i;
                        Debug.Log("Last Pos:"+lastpose);
                    }
                    else{
                        lastpose = 9.47f * i;
                    }
                    break;
                } 
            }        
        }   
        Debug.Log("REt:"+ret);
        return ret;
    }
    public void ballspinUpdate()
    {
        Debug.Log("AFter Spin Invoked");
        // if(_fortuneWheel.transform.position.z)
        currentNo = desireNo;
        AfterSpin(currentNo);
    }
    public iTween.EaseType easeType;
    public IEnumerator rot()
    {
        iTween.RotateAdd(_fortuneWheel, iTween.Hash( "z", -GetDestPosition(desireNo ), "time", 10f,  "easetype", easeType, "oncomplete", "ballspinUpdate", "oncompletetarget", _fortuneWheel ) );
        yield return new WaitForSeconds(1f);
        // while (isSpinning)
        // {
            
            // UnityEngine.Debug.LogError("anglesUntillNow  " + anglesUntillNow + "  totalAngles  " + totalAngles);
            // if (anglesUntillNow < totalAngles)
            // {
            //     float angle = speed * t;

            //     Angle = angle;
            //     anglesUntillNow += Math.Abs(angle);

            //     float mapTime = animationCurve.Evaluate(
            //         NumberMapping(anglesUntillNow, 0, totalAngles,
            //             rateOfChangeOfSpeed, initialTime));

            //     float distanceLeft = NumberMapping(anglesUntillNow, 0, totalAngles,
            //             initalposX, initailDistance);
            //     remainDistance = distanceLeft;
            //     m = mapTime;
            //     t = mapTime;
            //     //move images
            //     // _awardImages[currentImageIndex].transform.position = new Vector2(distanceLeft,
            //     //      _awardImages[currentImageIndex].transform.position.y);
            //     //rotate wheel
            //     _fortuneWheel.transform.Rotate(0f, 0f, -angle);
            //     // iTween.RotateBy(_fortuneWheel, iTween.Hash(  "z", -angle, "easetype", iTween.EaseType.easeInOutQuint, "time", 2f ));
            //     // iTween.RotateUpdate(_fortuneWheel, iTween.Hash("y", -angle, "time", 2f));
            //     temp += Math.Abs(angle);
            //     if (temp > 360)
            //     {
            //         currentRound++;
            //         temp = 0;
            //     }
            // }
            // else
            // {
            //     currentNo = desireNo;
            //     AfterSpin(currentNo);
            // }
        // }
        
    }

    void AfterSpin(int wheelNo)
    {
        isSpinning = false;
        Debug.Log("After Spin:");
        // _fortuneWheel.transform.eulerAngles = new Vector3(-135, 0, angles_01[wheelNo]);
        // iTween.RotateBy(_fortuneWheel, iTween.Hash( "z", 360 + angles_01[wheelNo], "easetype", iTween.EaseType.easeInOutQuart, "time", 2f ));
        
        // SoundManager.instance?.PlayClip("spinwheelend");
        lastImageIndex = currentImageIndex;
        RemoveParents();
        if (onSpinComplete != null)
            onSpinComplete();
    }

    private void RemoveParents()
    {
        for (int i = 0; i < _awardImages.Length; i++)
        {
            if (i == currentImageIndex) continue;
            _awardImages[i].transform.parent = _content.transform;
        }
    }

   

    public int desireNo = 5;
    public float speed;

    public float rateOfChangeOfSpeed = 0.01f;
    public int maxRounds = 5;
    public float singleAngle = 360 / 9.4736842105f;
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
            _awardImages[i].transform.parent = _content.transform;
            _awardImages[i].transform.localPosition = new Vector3(200, _awardImages[i].transform.localPosition.y, 0);
        }
        currentNo = wheelNo;
        _awardImages[lastImageIndex].transform.localPosition = new Vector3(0, _awardImages[currentImageIndex].transform.localPosition.y, 0);

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
        _awardImages[currentImageIndex].transform.SetAsLastSibling();
        _awardImages[lastImageIndex].transform.SetAsFirstSibling();
        float finalXCoordinate = (_awardImages[currentImageIndex].GetComponent<RectTransform>().rect.width * (_awardImages.Length - 1) + spaceBetweenTwoImages * (_awardImages.Length - 1));
        _awardImages[currentImageIndex].transform.localPosition = new Vector2(-finalXCoordinate, _awardImages[currentImageIndex].transform.localPosition.y);

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
            _awardImages[i].transform.parent = _awardImages[currentImageIndex].transform;
        }
    }

    public void ForceFullyStopWheel()
    {
        if (isSpinning)
        {
            iTween.Stop(_fortuneWheel);
            iTween.Stop(_awardImages[currentImageIndex], true);
            _fortuneWheel.transform.eulerAngles = new Vector3(0, 0, 0);
            lastImageIndex = currentImageIndex;

            for (int i = 0; i < _awardImages.Length; i++)
            {
                _awardImages[i].transform.parent = _content.transform;
                _awardImages[i].transform.localPosition = new Vector3(200, _awardImages[i].transform.localPosition.y, 0);
            }
            _awardImages[currentImageIndex].transform.localPosition = new Vector3(0, _awardImages[currentImageIndex].transform.localPosition.y, 0);
            isSpinning = false;
        }
    }*/
}
