using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TwoActionHandler : MonoBehaviour
{
    public UnityEvent FristAction;
    public UnityEvent SecondAction;

    [SerializeField] private float delayTime;

    public void StartAction()
    {
        FristAction.Invoke();

        StartCoroutine(NextAction());
    }

    private IEnumerator NextAction()
    {
        yield return new WaitForSeconds(delayTime);
        SecondAction.Invoke();
    }
}
