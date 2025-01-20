using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FadeScreenCanvas : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    public AnimationCurve fadeCurve;
    public float duration = 1.0f;
    public bool isPlayOnStart = false;

    [Space(5)]
    [Header("Event")]
    [Space(2)]
    public UnityEvent OnFadeInScreenOut;
    public UnityEvent OnFadeOutScreenOut;

    private void Awake()
    {
        if (canvasGroup.alpha == 0) canvasGroup.alpha = 1;
    }

    private void Start()
    {
        if (isPlayOnStart) FadeOutScreen();
    }

    public void FadeOutScreen()
    {
        LeanTween.alphaCanvas(canvasGroup, 0f, duration).setEase(fadeCurve).setOnComplete(() => OnFadeOutScreenOut.Invoke());
    }

    public void FadeInScreen()
    {
        LeanTween.alphaCanvas(canvasGroup, 1f, duration).setEase(fadeCurve).setOnComplete(() => OnFadeInScreenOut.Invoke());
    }
}
