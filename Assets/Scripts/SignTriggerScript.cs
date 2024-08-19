using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignTriggerScript : MonoBehaviour
{

    public GameObject dialogBackground;
    public GameObject dialogText;

    private bool isPlayerInside = false;
    private bool isDialogVisible = false;
    private bool isDialogFading = false;

    // Start is called before the first frame update
    void Start()
    {
        CanvasGroup backgroundCanvasGroup = dialogBackground.GetComponent<CanvasGroup>();
        CanvasGroup textCanvasGroup = dialogText.GetComponent<CanvasGroup>();
        backgroundCanvasGroup.alpha = 0f;
        textCanvasGroup.alpha = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayerInside && Input.GetKeyDown(KeyCode.Space) && !isDialogFading)
        {
            if (isDialogVisible)
            {
                StartCoroutine(FadeOutDialog());
            }
            else
            {
                StartCoroutine(FadeInDialog());
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (IsChildOfTaggedParent(other.transform, "Player"))
        {
            GetComponentInChildren<SpriteBillBoardScript>().FadeIn();
            isPlayerInside = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (IsChildOfTaggedParent(other.transform, "Player"))
        {
            GetComponentInChildren<SpriteBillBoardScript>().FadeOut();
            isPlayerInside = false;
            if (isDialogVisible)
            {
                StartCoroutine(FadeOutDialog());
            }
        }
    }

    private bool IsChildOfTaggedParent(Transform child, string tag)
    {
        Transform current = child;
        while (current != null)
        {
            if (current.CompareTag(tag))
            {
                return true;
            }
            current = current.parent;
        }
        return false;
    }

    private IEnumerator FadeInDialog()
    {
        isDialogFading = true;
        CanvasGroup backgroundCanvasGroup = dialogBackground.GetComponent<CanvasGroup>();
        CanvasGroup textCanvasGroup = dialogText.GetComponent<CanvasGroup>();

        float duration = 0.5f;
        float time = 0;

        while (time < duration)
        {
            time += Time.deltaTime;
            float alpha = Mathf.Clamp01(time / duration);
            backgroundCanvasGroup.alpha = alpha;
            textCanvasGroup.alpha = alpha;
            yield return null;
        }

        backgroundCanvasGroup.alpha = 1f;
        textCanvasGroup.alpha = 1f;
        isDialogVisible = true;
        isDialogFading = false;
    }

    private IEnumerator FadeOutDialog()
    {
        isDialogFading = true;
        CanvasGroup backgroundCanvasGroup = dialogBackground.GetComponent<CanvasGroup>();
        CanvasGroup textCanvasGroup = dialogText.GetComponent<CanvasGroup>();

        float duration = 0.5f;
        float time = 0;

        while (time < duration)
        {
            time += Time.deltaTime;
            float alpha = Mathf.Clamp01(1 - (time / duration));
            backgroundCanvasGroup.alpha = alpha;
            textCanvasGroup.alpha = alpha;
            yield return null;
        }

        backgroundCanvasGroup.alpha = 0f;
        textCanvasGroup.alpha = 0f;
        isDialogVisible = false;
        isDialogFading = false;
    }
}
