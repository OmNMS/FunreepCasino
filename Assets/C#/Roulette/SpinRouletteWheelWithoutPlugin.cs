
//using System.Numerics;
//using System.Threading.Tasks.Dataflow;
using System;
using System.Collections;
using System.Collections.Generic;
 
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Roulette.Gameplay;

public class SpinRouletteWheelWithoutPlugin : MonoBehaviour
{
    [SerializeField] private int currentImageIndex;
    [SerializeField] private int lastImageIndex;

    public AnimationCurve animationCurve;
    // public List<MeshRenderer> Positions;
    // public List<BoxCollider> positions;
    [SerializeField] private GameObject _fortuneWheel;
    [SerializeField] private GameObject _content;
    [SerializeField] private GameObject[] _awardImages;
    public static SpinRouletteWheelWithoutPlugin instane;
    public Action onSpinComplete;
    public float spaceBetweenTwoImages=100;
    public bool isSpinning;
    public Transform centerObj;
    [SerializeField] GameObject reference;
    [SerializeField] Animator wheelobj;
    [SerializeField] Animator diamondobj;
    [SerializeField] Transform basepos;

    private void Awake()
    {
        instane = this;
    }

    // List<MeshRenderer> GetChildren_MeshRenderer(Transform parent)
    // {
    //     List<MeshRenderer> children = new List<MeshRenderer>();
        
    //     foreach (MeshRenderer child in parent)
    //     {
    //         children.Add(child);
    //     }
    //     return children;
    // }

    // List<BoxCollider> GetChildren_BoxCollider(Transform parent)
    // {
    //     List<BoxCollider> children = new List<BoxCollider>();
        
    //     foreach (BoxCollider child in parent)
    //     {
    //         children.Add(child);
    //     }
    //     return children;
    // }

    public float maxValue;
    public float minValue;
    private void Start()
    {
        _fortuneWheel = gameObject;
        lastImageIndex = 0;
        currentImageIndex = 0;

        // Positions = GetChildren_MeshRenderer(_fortuneWheel.transform);
        // positions = GetChildren_BoxCollider(_fortuneWheel.transform);

        // Positions[0].enabled = true;
       
    }
    int[] angles = { 0, 36, 72, 108, -216, -180, -144, -108, -72, -36 };
    int[] angles_01 = {-48, 142, -39, 180, 0, 218, 37, 255, 75, 293, 113, 265, 84, 151, -29, 189, 9, 227, 46, 65, 245, 27
        , 208, -10, 170, 103, 283, 122, 302, 94, 274, 56, 236, 18, 198, -20, 161};

    public void SpinTester()
    {
        //int whelNo = UnityEngine.Random.Range(0,36);
        Spin(17);
    }

    public void Spin(int wheelNo)
    {
        //wheelobj.SetBool("wheelzoom",true);
        diamondobj.SetBool("rotate",true);
        // if (!isSpinning)
        // {
            desireNo = wheelNo;
            isSpinning = true;
            // RearangeAwardImages(imageXfactor);

            //CalculateAngle();
            StartCoroutine(rot());
            // SoundManager.instance?.PlayClip("spinwheel");
            // initailDistance = centerObj.transform.position.x;
            // initalposX = _awardImages[currentImageIndex].transform.position.x;
        // }

    }
    float temp = 0f;
    void Update()
    {
        float now = transform.rotation.z;
        if(Mathf.Abs(seventwenty - now) == 720)
        {
            ballrotator.instant.newmoving();
        }
    }
    // Dictionary<int, float> Position = new Dictionary<int, float> {
    //     {0, 9.47f * 19 },
    //     {1, 9.47f * 0 },
    //     {2, 9.47f * 18 },
    //     {3, 9.47f * 19 },
    //     {4, 9.47f * 19 },
    //     {5, 9.47f * 19 },
    //     {6, 9.47f * 19 },
    //     {7, 9.47f * 19 },
    //     {8, 9.47f * 19 },
    //     {9, 9.47f * 19 },
    //     {10, 9.47f * 19 },
    //     {11, 9.47f * 19 },
    //     {12, 9.47f * 19 },
    //     {13, 9.47f * 19 },
    //     {14, 9.47f * 19 },
    //     {15, 9.47f * 19 },
    //     {16, 9.47f * 19 },
    //     {17, 9.47f * 19 },
    //     {18, 9.47f * 19 },
    //     {19, 9.47f * 19 },
    //     {20, 9.47f * 19 },
    //     {21, 9.47f * 19 },
    //     {22, 9.47f * 19 },
    //     {23, 9.47f * 19 },
    //     {24, 9.47f * 19 },
    //     {25, 9.47f * 19 },
    //     {26, 9.47f * 19 },
    //     {27, 9.47f * 19 },
    //     {28, 9.47f * 19 },
    //     {29, 9.47f * 19 },
    //     {30, 9.47f * 19 },
    //     {31, 9.47f * 19 },
    //     {32, 9.47f * 19 },
    //     {33, 9.47f * 19 },
    //     {34, 9.47f * 19 },
    //     {35, 9.47f * 19 },
    //     {36, 9.47f * 19 },
    //     {37, 9.47f * 19 },
    // };
    private float[] Pose = {5,22,34,15,3,24,36,13,1,37,27,10,25,29,12,8,19,31,18,6,21,33,16,4,23,35,14,2,0,28,9,26,30,11,7,20,32,17}; 
    float lastpose = 0;
    float seventwenty;
    float GetDestPosition(int no  )
    {
        float ret = 1080;
        //if ( no == -1)
        //{
        //    ret += 1440 + (9.47f * 9);//19
        //    if (lastpose > 0)
        //    {
        //        ret -= lastpose;
        //        // Debug.Log("pos:" + ret + "  Last Psoe"+ lastpose );
        //        lastpose = 9.47f * 9;
        //        Debug.Log("Last Pos:"+lastpose);
        //    }
        //    else{
        //        lastpose = 9.47f * 9;
        //    }
        //}
        //else
        //{
            for (int i = 0; i < Pose.Length; i++)
            {
                //if (i == 9) continue;
                if (no == Pose[i] )
                {
                    Debug.Log("NO:"+no);
                    Debug.Log("Pos at i:"+i);
                    //Debug.Log("posi"+Pose[i]);
                    ret += 9.47f * i;
                    //Debug.Log("REt:"+ret);
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
        //} 
        Debug.Log("REt:"+ret);
        return ret;
    }
    public void setintialangle(int value)
    {
        Vector3 current = new Vector3 (-90,0,-49);

        float intial = transform.rotation.z;
        int count =0;
        //ballrotator.instant.ball.SetActive(false);
        // ballrotator.instant.ball.GetComponent<Image>().enabled = false;
        // ballrotator.instant.rotatrnow(value);
        
        for (int i = 0; i < Pose.Length; i++)
        {
            if(value != Pose[i])
            {
                count++;
            }
            else
            {
                // ballrotator.instant.finalval = i;
                // ballrotator.instant.previousval = i;    
                transform.rotation = Quaternion.Euler(37,0,9.47f * i);
                lastpose =9.47f * i;
                Debug.Log("Value of number is : " + value );
                StartCoroutine(ballrotator.instant.values(value));
                //transform.Rotate(0,0,9.47f * i);
            }
        }
        //float new_rotation = 9.47
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
        seventwenty = transform.position.z;
        Debug.Log("Desire no "+desireNo);
        iTween.RotateAdd(_fortuneWheel, iTween.Hash( "z", GetDestPosition(desireNo ), "time",speed,  "easetype", easeType, "oncomplete", "ballspinUpdate", "oncompletetarget", _fortuneWheel ) );
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
        wheelobj.SetBool("wheelzoom",false);
        diamondobj.SetBool("rotate",false);
        //reference.GetComponent<Animator>().SetBool("wheelzoom",false);
        lastImageIndex = currentImageIndex;
        //RouletteScreen.Instance.showwinamount();
        RemoveParents();
        
        if (onSpinComplete != null)
            onSpinComplete();

        //ballrotator.instant.removeelement();
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
    public float speed;//11.5

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
    }
}
