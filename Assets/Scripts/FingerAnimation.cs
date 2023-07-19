using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FingerAnimation : MonoBehaviour
{
    public float startDelay = 1.0f;
    public float slideDuration = 1.0f;
    public float endPosition = 1.0f;

    private RectTransform rectTransform;
    private Vector2 initialPosition;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        initialPosition = rectTransform.anchoredPosition;
        rectTransform.anchoredPosition = new Vector2(-rectTransform.rect.width, initialPosition.y);
        StartCoroutine(SlideIn());
    }

    private System.Collections.IEnumerator SlideIn()
    {
        yield return new WaitForSeconds(startDelay);

        float elapsed = 0f;
        Vector2 startPosition = rectTransform.anchoredPosition;

        while (elapsed < slideDuration)
        {
            float t = Mathf.SmoothStep(0f, 1f, elapsed / slideDuration);
            float newPosition = Mathf.Lerp(startPosition.x, endPosition, t);
            rectTransform.anchoredPosition = new Vector2(newPosition, startPosition.y);

            elapsed += Time.deltaTime;
            yield return null;
        }

        rectTransform.anchoredPosition = new Vector2(endPosition, startPosition.y);
    }
}
