using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Roulette.Gameplay;

public class button : MonoBehaviour, IPointerDownHandler, IPointerUpHandler 
{
 
    public bool buttonPressed;
    public static button instance;
    public Button current; 
    private void Start() {
        instance = this;
    }
    private void Update() {
        
    }
    
    public void OnPointerDown(PointerEventData eventData)
    {
        //Debug.Log("///////////////////////////////////////////");
        buttonPressed = true;
        StartCoroutine(letstryhere());
    }
    
    public void OnPointerUp(PointerEventData eventData)
    {
        //Debug.Log("jjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjj");
        //Debug.Log("\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\");
        buttonPressed = false;
        StopCoroutine(letstryhere());
    }
    IEnumerator letstryhere()
    {
        if(buttonPressed)
        {
            RouletteScreen.Instance.AddBets(current);
            //Debug.Log("here we go again");
        }
        yield return new WaitForSeconds(1f);
        StartCoroutine(letstryhere());
        
    }
}