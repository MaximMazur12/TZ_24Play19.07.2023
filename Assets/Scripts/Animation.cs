using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation : MonoBehaviour
{
     public float minScale = 0.9f;
    public float maxScale = 1.1f;
    public float pulseSpeed = 1.0f;

    private Vector3 initialScale;

    private void Start()
    {
        initialScale = transform.localScale;
        StartCoroutine(PulseAnimation());
    }

    private System.Collections.IEnumerator PulseAnimation()
    {
        while (true)
        {
            yield return Pulse(minScale, maxScale, pulseSpeed);
            yield return Pulse(maxScale, minScale, pulseSpeed);
        }
    }

    private System.Collections.IEnumerator Pulse(float fromScale, float toScale, float duration)
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float t = Mathf.SmoothStep(0f, 1f, elapsed / duration);
            float scale = Mathf.Lerp(fromScale, toScale, t);
            transform.localScale = initialScale * scale;

            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.localScale = initialScale * toScale;
    }
}
