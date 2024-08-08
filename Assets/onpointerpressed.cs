using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class onpointerpressed : MonoBehaviour, IPointerDownHandler , IPointerUpHandler
{
    public GameObject displayImage;

    public void OnPointerDown(PointerEventData eventData)
    {
        // throw new System.NotImplementedException();
        displayImage.SetActive(true);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        // throw new System.NotImplementedException();
        displayImage.SetActive(false);
    }
}
