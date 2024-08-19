using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteBillBoardScript : MonoBehaviour
{
    private Renderer rend;
    private Color originalColor;
    public float startOpacity = 0.0f;
    public bool updateEveryFrame = false;
    public float fadeInDelay = 0.0f;
    public float fadeOutDelay = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        if (rend)
        {
            originalColor = rend.material.color;
        }
        SetOpacity(startOpacity);
        transform.eulerAngles = new Vector3(Camera.main.transform.eulerAngles.x, Camera.main.transform.eulerAngles.y, Camera.main.transform.eulerAngles.z);
        if (fadeInDelay > 0.0f && fadeOutDelay > 0.0f)
        {
            StartCoroutine(FadeInAndOutCoroutine(fadeInDelay, 1.0f, fadeOutDelay, 1.0f));
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (updateEveryFrame)
        {
            transform.eulerAngles = new Vector3(Camera.main.transform.eulerAngles.x, Camera.main.transform.eulerAngles.y, Camera.main.transform.eulerAngles.z);
        }
    }

    public void FadeIn()
    {
        StartCoroutine(FadeTo(1f, 0.5f)); // Fade in over 1 second
    }

    public void FadeOut()
    {
        StartCoroutine(FadeTo(0f, 0.5f)); // Fade out over 1 second
    }

    private IEnumerator FadeInAndOutCoroutine(float fadeInDelay, float fadeInDuration, float waitDuration, float fadeOutDuration)
    {
        yield return new WaitForSeconds(fadeInDelay); // Wait before starting fade in
        yield return StartCoroutine(FadeTo(1f, fadeInDuration)); // Fade in
        yield return new WaitForSeconds(waitDuration); // Wait
        yield return StartCoroutine(FadeTo(0f, fadeOutDuration)); // Fade out
    }

    private IEnumerator FadeTo(float targetOpacity, float duration)
    {
        if (!rend)
        {
            yield return null;
        }
        float startOpacity = rend.material.color.a;
        float time = 0;

        while (time < duration)
        {
            time += Time.deltaTime;
            float blend = Mathf.Clamp01(time / duration);
            SetOpacity(Mathf.Lerp(startOpacity, targetOpacity, blend));
            yield return null;
        }

        SetOpacity(targetOpacity);
    }

    private void SetOpacity(float opacity)
    {
        if (!rend)
        {
            return;
        }
        Color color = rend.material.color;
        color.a = opacity;
        rend.material.color = color;
    }
}
