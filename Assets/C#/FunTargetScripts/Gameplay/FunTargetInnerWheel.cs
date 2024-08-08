using FunTarget.GamePlay;
using Microsoft.Win32.SafeHandles;
//using System.Reflection.Metadata;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FunTargetInnerWheel : MonoBehaviour
{
    public bool isSpinning = false;

    [SerializeField]Animator wheelAnim;
    public Animator wheelcontrol;

    public Vector3 defaultRotation = new  Vector3(0, 136.6f, 0);
    public Vector3 chestRotation = new  Vector3(0, 225, 0);
    public Vector3 hatRotation = new  Vector3(0, 47, 0);
    public Vector3 coinRotation = new  Vector3(0, 316.5f, 0);

    Quaternion[] rotations = new Quaternion[4];
    public Sprite[] sprites = new Sprite[5];


    private void Awake()
    {       
        //wheelAnim = GetComponent<Animator>();
        isSpinning = false;
        Debug.Log(gameObject.name);
    }

    private void Start()
    {
        //transform.rotation = Quaternion.Euler(defaultRotation);
    }
    int shownumber;
    /// <summary>
    /// Set inner wheel rotation according to the win type value received
    /// </summary>
    /// <param name="multiple">win type value</param>
    public void SetRotation(int multiple)
    {
        //using placeholder values for the conditions
        show.SetActive(false);
        shownumber = multiple;
        if(multiple == 1)
        {
            int random = Random.Range(0,2);
            if(random == 0)
            {
                transform.rotation = Quaternion.Euler(chestRotation);
            }
            if(random == 1)
            {
                transform.rotation = Quaternion.Euler(coinRotation);
            }
            if(random == 2)
            {
                transform.rotation = Quaternion.Euler(coinRotation);
            }
            //transform.rotation = Quaternion.Euler(hatRotation);
        }
        else if(multiple == 2)
        {
            transform.rotation = Quaternion.Euler(hatRotation);
            //transform.Rotate = hatRotation;//Quaternion.Euler(hatRotation);
            //Debug.LogError("the value of hatpoistion was placed"+hatRotation);
        }
        // else if (multiple == 3)
        // {
        //     transform.rotation = Quaternion.Euler(chestRotation);
        // }
        // else
        // {
        //     transform.rotation = Quaternion.Euler(defaultRotation);
        // }
    }
    [SerializeField] SpriteRenderer square;
    [SerializeField] GameObject show;
    public void settingroation()
    {
        show.SetActive(true);
        Debug.LogError("shownumber"+shownumber);
        square.sprite= sprites[shownumber];
        StartCoroutine(FunTargetGamePlay.Instance.showwin());
        
        // if(shownumber == 1)
        // {
        //     int random = Random.Range(0,2);
        //     square.sprite= sprites[random];
        // }
        // else if(shownumber == 2)
        // {
        //     square.sprite = sprites[3];
        // }   
    }

    /// <summary>
    /// Triggers/stops wheel spinning animation
    /// </summary>
    public void SpinWheelAnimation(bool spin)
    {
        //wheelcontrol.SetBool("isSpinning",spin);
        //wheelAnim.SetBool("isSpinning", spin);
    }
}
