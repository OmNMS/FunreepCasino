using UnityEngine;
using UnityEngine.UI;

public class ObjectScreenshot : MonoBehaviour
{
    // Reference to the camera used for rendering the object
    public Camera renderCamera;
    public RawImage image;

    // The object to take a screenshot of
    public GameObject objectToCapture;
    public RectTransform  captureArea;

    // The width and height of the screenshot
    //public int width = 1920;
    //public int height = 1080;
    string filePath;

    // Take screenshot function
    public void TakeScreenshot()
    {
        Debug.Log("Reached here");
        filePath = Application.persistentDataPath + "/screenshot.png";
        if (System.IO.File.Exists(filePath))
        {
            // Delete the existing file
            System.IO.File.Delete(filePath);
        }
        Texture2D screenTexture = new Texture2D((int)captureArea.rect.width, (int)captureArea.rect.height, TextureFormat.RGB24, false);

        // Capture the screenshot of the specified area
        screenTexture.ReadPixels(new Rect(captureArea.position.x, captureArea.position.y, captureArea.rect.width, captureArea.rect.height), 0, 0);

        // Apply the pixel data to the texture
        screenTexture.Apply();

        // Convert the texture to a Sprite
        Sprite screenshotSprite = Sprite.Create(screenTexture, new Rect(0, 0, screenTexture.width, screenTexture.height), new Vector2(0.5f, 0.5f));

        // Optionally, display the screenshot in a RawImage component
        if (image != null)
        {
            image.texture = screenTexture;
        }

        // Save the screenshot as a PNG file (optional)
        byte[] bytes = screenTexture.EncodeToPNG();
        //string filePath = Application.persistentDataPath + "/screenshot.png";
        System.IO.File.WriteAllBytes(filePath, bytes);
        //showscreenshot();
    }
    public void showscreenshot()
    {
        Texture2D texture = LoadScreenshotTexture(filePath);
        if (texture != null)
        {
            //image.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
        }
        else
        {
            Debug.LogError("Failed to load screenshot texture.");
        }
    }
    Texture2D LoadScreenshotTexture(string filePath)
    {
        // Check if the file exists
        if (System.IO.File.Exists(filePath))
        {
            // Read the bytes from the file
            byte[] bytes = System.IO.File.ReadAllBytes(filePath);

            // Create a new Texture2D and load the bytes
            Texture2D texture = new Texture2D(2, 2); // Texture size can be adjusted as needed
            texture.LoadImage(bytes);

            return texture;
        }
        else
        {
            Debug.LogError("Screenshot file not found: " + filePath);
            return null;
        }
    }
}
