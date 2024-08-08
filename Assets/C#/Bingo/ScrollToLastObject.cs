using UnityEngine;
using UnityEngine.UI;

public class ScrollToLastObject : MonoBehaviour
{
    public ScrollRect scrollRect;
    public float scrollSpeed = 5f;
    public Transform lastobject;

    private void Start()
    {
        // Call ScrollToBottom() after your content has been populated or updated
        ScrollToBottom();
    }

    public void ScrollToBottom()
    {
        // Get the content RectTransform
        RectTransform contentRect = scrollRect.content;

        // Calculate the desired position to scroll to
        float targetY = Mathf.Clamp(/*contentRect.sizeDelta.y*/ lastobject.position.y - scrollRect.viewport.rect.height, 0f, float.MaxValue);

        // Start the scroll animation coroutine
        StartCoroutine(ScrollAnimation(contentRect, new Vector2(contentRect.anchoredPosition.x, targetY)));
    }

    private System.Collections.IEnumerator ScrollAnimation(RectTransform contentRect, Vector2 targetPosition)
    {
        Vector2 startPosition = contentRect.anchoredPosition;
        float elapsedTime = 0f;

        while (elapsedTime < scrollSpeed)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / scrollSpeed);
            contentRect.anchoredPosition = Vector2.Lerp(startPosition, targetPosition, t);
            yield return null;
        }

        // Ensure the final position is set accurately
        contentRect.anchoredPosition = targetPosition;
    }
}
