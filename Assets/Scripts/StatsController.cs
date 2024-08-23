using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StatsController : MonoBehaviour
{
    public TextMeshPro timerText; // The TextMeshPro object to display the timer
    public TextMeshPro resetText; // The TextMeshPro object to display the timer
    public TextMeshPro signsText; // The TextMeshPro object to display the timer
    public TextMeshPro secretsText; // The TextMeshPro object to display the timer

    private float timer = 0f;
    private bool isRunning = true;

    private int resetCount = 0; // Count of resets
    private int signsCount = 0; // Count of signs
    private int secretsCount = 0; // Count of secrets

    public int signsMax = 0; // Max number of signs
    public int secretsMax = 0; // Max number of secrets

    // Start is called before the first frame update
    void Start()
    {
        timer = 0f;
        isRunning = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isRunning)
        {
            timer += Time.deltaTime;
            int minutes = Mathf.FloorToInt(timer / 60F);
            int seconds = Mathf.FloorToInt(timer % 60F);
            timerText.text = string.Format("{0:0}:{1:00}", minutes, seconds);
        }
    }

    public void StopTimer()
    {
        isRunning = false;
    }

    // Public function to increment resetCount
    public void IncrementResetCount()
    {
        if (!isRunning)
        {
            return;
        }
        resetCount++;
        resetText.text = resetCount.ToString();
    }

    // Public function to increment signsCount
    public void IncrementSignsCount()
    {
        if (!isRunning)
        {
            return;
        }
        signsCount++;
        signsText.text = string.Format("{0} / {1}", signsCount, signsMax);
    }

    // Public function to increment secretsCount
    public void IncrementSecretsCount()
    {
        if (!isRunning)
        {
            return;
        }
        secretsCount++;
        secretsText.text = string.Format("{0} / {1}", secretsCount, secretsMax);
    }
}
