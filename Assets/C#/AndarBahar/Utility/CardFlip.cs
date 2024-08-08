using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using AndarBahar.Gameplay;

public class CardFlip : MonoBehaviour
{
    public static CardFlip Instance;
    Image rend;

    [SerializeField]
    public GameObject faceSprite, backSprite;
    public Action OnFlipComplete;
    private bool coroutineAllowed, facedUp;

    // Start is called before the first frame update
    void Start()
    {
        // this.gameObject.SetActive(false);
        // rend = GetComponent<Image>();
        rend.sprite = backSprite.GetComponent<Image>().sprite;
        coroutineAllowed = true;
        facedUp = false;
        // RotateCard();
    }

    // private void OnMouseDown()
    // {
    //     if (coroutineAllowed)
    //     {
    //         StartCoroutine(RotateCard());
    //     }
    // }

    public void StartFlip()
    {
        if (coroutineAllowed)
        {
            gameObject.SetActive(true);
            // StartCoroutine(RotateCard());
        }
    }

    public void HideCard()
    {
        gameObject.GetComponent<Image>().enabled = false;
    }

    public void ShowCard()
    {
        gameObject.GetComponent<Image>().enabled = true;
    }

    float wait = 0.02f;
    public IEnumerator RotateCard(GameObject card)
    {
        coroutineAllowed = false;

        if (!facedUp)
        {
            for (float i =180f; i >= 0f; i -= 10f)
            {
                card.transform.rotation = Quaternion.Euler(0f, i, 0f);
                if (i == 90f)
                {
                    card.transform.GetChild(0).gameObject.SetActive(true);
                    card.transform.GetChild(1).gameObject.SetActive(false);
                    // rend.sprite = faceSprite.GetComponent<Image>().sprite;
                }
                yield return new WaitForSeconds(wait);
            }
        }

        else if (facedUp)
        {
            for (float i = 180f; i >= 0f; i -= 10f)
            {
                card.transform.rotation = Quaternion.Euler(0f, -i, 0f);
                if (i == 90f)
                {
                    card.transform.GetChild(1).gameObject.SetActive(true);
                    card.transform.GetChild(0).gameObject.SetActive(false);
                    // rend.sprite = backSprite.GetComponent<Image>().sprite;
                }
                yield return new WaitForSeconds(wait);
            }
        }

        coroutineAllowed = true;

        facedUp = !facedUp;
        OnFlipComplete?.Invoke();
    }
    public void ResetRotation()
    {
        gameObject.SetActive(false);
        facedUp = false;
        transform.rotation = Quaternion.Euler(0f, 180f, 0f);
    }
}
