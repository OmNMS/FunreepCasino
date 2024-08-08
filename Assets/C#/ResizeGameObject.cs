//using System.Diagnostics;
using UnityEngine;

public class ResizeGameObject : MonoBehaviour
{
    public float widthPercentage ; // Percentage of screen width for the GameObject
    public float heightPercentage ; // Percentage of screen height for the GameObject
    public RectTransform sizer;

    void Start()
    {
        ResizeObject();
    }

    void ResizeObject()
    {
        // Get the screen width and height
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;

        // Calculate the new width and height based on percentages
        float newWidth = screenWidth * widthPercentage;
        float newHeight = screenHeight * heightPercentage;

        // Set the new size of the GameObject
        RectTransform rectTransform = GetComponent<RectTransform>();
        sizer.sizeDelta = new Vector2(newWidth, newHeight);
    }
    public void getscreensize()
    {
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;
        Debug.Log("The screen width is " + screenWidth + "The screen height is" + screenHeight);
    }
}
