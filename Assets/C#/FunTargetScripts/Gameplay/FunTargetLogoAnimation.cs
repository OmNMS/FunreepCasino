using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FunTargetLogoAnimation : MonoBehaviour
{
    Image logoImage;
    [SerializeField] List<Sprite> logoSprites = new List<Sprite>();
    [SerializeField] float interval = 0.5f;

    void Start()
    {
        logoImage = GetComponent<Image>();
        StartCoroutine(CycleSprites());
    }

    IEnumerator CycleSprites()
    {
        while (true)
        {
            foreach (Sprite sprite in logoSprites)
            {
                logoImage.sprite = sprite;
                yield return new WaitForSeconds(interval);
            }
        }
    }
}
