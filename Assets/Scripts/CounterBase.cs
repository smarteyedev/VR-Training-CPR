using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public abstract class CounterBase : MonoBehaviour
{
    [Header("Scoring System")]
    public int maxScore = 100;
    protected int currentScore = 0;

    [Header("Countdown Timer")]
    public float countdownDuration = 60f;
    protected float currentTime;
    protected bool isCountingDown = false;
    protected bool canScore = true;

    [Header("UI Elements")]
    public Slider progressBar;
    public CanvasGroup progressBarCanvasGroup;
    public Image progressBarFill;
    public TextMeshProUGUI scoreText;

    [Header("Events")]
    public UnityEvent onMaxScoreReached;
    public UnityEvent onCountdownFinished;

    private bool isBlinking = false;

    protected virtual void Start()
    {
        ResetCountdown();
        UpdateScoreText();

        if (progressBar != null)
        {
            progressBar.maxValue = countdownDuration;
            progressBar.value = countdownDuration;
        }

        UpdateProgressBarColor();
        // StartCountdown();
    }

    protected virtual void Update()
    {
        if (isCountingDown)
        {
            currentTime -= Time.deltaTime;
            if (currentTime <= 0)
            {
                currentTime = 0;
                isCountingDown = false;
                canScore = false; // Player tidak dapat skor setelah countdown selesai
                Debug.Log("Countdown finished.");
                onCountdownFinished.Invoke();
            }

            // Update progress bar
            if (progressBar != null)
            {
                progressBar.value = currentTime;
            }

            UpdateProgressBarColor();
        }
    }

    public virtual void AddScore(int score)
    {
        if (!canScore)
        {
            Debug.Log("Cannot add score, countdown has finished.");
            return;
        }

        currentScore += score;
        Debug.Log($"Score Added: {score}, Current Score: {currentScore}");

        if (currentScore >= maxScore)
        {
            currentScore = maxScore;
            Debug.Log("Max score reached.");
            onMaxScoreReached.Invoke();

            OnCounterIsFinished();

            isCountingDown = false;
        }

        UpdateScoreText();
    }

    public virtual void StartCountdown()
    {
        isCountingDown = true;
        canScore = true; // Reset kondisi untuk bisa mendapatkan skor
    }

    public virtual void ResetCountdown()
    {
        currentTime = countdownDuration;
        isCountingDown = false;
        canScore = true; // Reset kondisi untuk bisa mendapatkan skor
        Debug.Log("Countdown reset.");

        // Reset progress bar
        if (progressBar != null)
        {
            progressBar.value = countdownDuration;
        }

        UpdateScoreText();
        UpdateProgressBarColor();

        if (isBlinking)
        {
            LeanTween.cancel(progressBarCanvasGroup.gameObject);
            progressBarCanvasGroup.alpha = 1f; // Reset to default alpha
            isBlinking = false;
        }
    }

    public float GetRemainingTime()
    {
        return currentTime;
    }

    protected void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = $"{currentScore}/{maxScore}";
        }
    }

    protected void UpdateProgressBarColor()
    {
        if (progressBarCanvasGroup != null && progressBarFill != null && countdownDuration > 0)
        {
            float percentage = currentTime / countdownDuration;

            if (percentage > 0.7f)
            {
                progressBarFill.color = Color.green;
                progressBarCanvasGroup.alpha = 1f;
                if (isBlinking)
                {
                    LeanTween.cancel(progressBarCanvasGroup.gameObject);
                    isBlinking = false;
                }
            }
            else if (percentage > 0.4f)
            {
                progressBarFill.color = Color.yellow;
                if (!isBlinking)
                {
                    isBlinking = true;
                    StartBlinking(1f); // Slower blink for yellow
                }
            }
            else
            {
                progressBarFill.color = Color.red;
                if (!isBlinking)
                {
                    isBlinking = true;
                    StartBlinking(0.5f); // Faster blink for red
                }
            }
        }
    }

    private void StartBlinking(float blinkSpeed)
    {
        LeanTween.alphaCanvas(progressBarCanvasGroup, 0.3f, blinkSpeed).setLoopPingPong();
    }

    protected virtual void OnCounterIsFinished() { }
}
