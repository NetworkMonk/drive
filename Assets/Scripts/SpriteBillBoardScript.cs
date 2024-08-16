using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteBillBoardScript : MonoBehaviour
{
    private Renderer rend;
    private Color originalColor;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        originalColor = rend.material.color;
        SetOpacity(0f);
        transform.eulerAngles = new Vector3(Camera.main.transform.eulerAngles.x, Camera.main.transform.eulerAngles.y, Camera.main.transform.eulerAngles.z);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void FadeIn()
    {
        StartCoroutine(FadeTo(1f, 0.5f)); // Fade in over 1 second
    }

    public void FadeOut()
    {
        StartCoroutine(FadeTo(0f, 0.5f)); // Fade out over 1 second
    }

    private IEnumerator FadeTo(float targetOpacity, float duration)
    {
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
        Color color = rend.material.color;
        color.a = opacity;
        rend.material.color = color;
    }
}
