using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAnimate : MonoBehaviour
{
    public Animator mainCameraAnimator;
    // Start is called before the first frame update
    IEnumerator Start()
    {
        yield return new WaitForSeconds(1.0f);
        AnimateMainCamera(true);
        yield return new WaitForSeconds(4.0f);
        AnimateMainCamera(false);
    }

    public void AnimateMainCamera(bool isRound)
    {
        mainCameraAnimator.SetBool("Play", isRound);
    }
}
