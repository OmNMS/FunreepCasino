using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorBlinker : MonoBehaviour
{
    [SerializeField] Color color1;
    [SerializeField] Color color2;
    [SerializeField] float blinkInterval = 0.5f;

    Button button;
    public bool isBlinking;

    private void Start()
    {
        isBlinking = false;
        button = GetComponent<Button>();
    }

    public IEnumerator TriggerBlinking()
    {
        if (!isBlinking)
        {
            isBlinking = true;
            while (isBlinking)
            {
                if (button.image.color == color1)
                {
                    button.image.color = color2;
                }
                else
                {
                    button.image.color = color1;
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
        button.image.color = color1;
    }
}
