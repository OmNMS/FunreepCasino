using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonBlinker : MonoBehaviour
{
    [SerializeField] public Sprite sprite1;
    [SerializeField] public Sprite sprite2;
    [SerializeField] float blinkInterval = 0.5f;

    Image buttonImage;
    bool isBlinking;

    public bool IsBlinking
    {
        get
        {
            return isBlinking;
        }
    }


    private void Awake()
    {
        isBlinking = false;
        buttonImage = GetComponent<Image>();
    }

    public IEnumerator TriggerBlinking()
    {
        if (!isBlinking)
        {
            Debug.Log("Blinking at: " + this.gameObject.name);
            isBlinking = true;
            while (isBlinking)
            {
                if (buttonImage.sprite == sprite1)
                {
                    buttonImage.sprite = sprite2;
                }
                else
                {
                    buttonImage.sprite = sprite1;
                }

                yield return new WaitForSeconds(blinkInterval);
            }
        }
        else
        {
            yield return null;
        }
    }

    public void StopBlinking()
    {
        isBlinking = false;
        buttonImage.sprite = sprite1;
    }
}
