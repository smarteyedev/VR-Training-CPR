using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.Events;

public class TwoHandPokePushable : XRSimpleInteractable
{
    [Header("Events")]
    public UnityEvent onTwoHandPushed; // Event yang dipanggil satu kali saat tombol ditekan dengan kedua tangan
    public UnityEvent onInvalidHandTouching;
    private bool leftHandTouching = false;
    private bool rightHandTouching = false;
    private Vector3 initialPosition;

    private bool eventTriggered = false;

    protected override void OnHoverEntered(HoverEnterEventArgs args)
    {
        base.OnHoverEntered(args);

        GameObject interactorGameObject = args.interactorObject.transform.gameObject;

        if (interactorGameObject.CompareTag("Left Hand"))
        {
            leftHandTouching = true;
            // Debug.Log("Left hand is touching the button.");
        }
        else if (interactorGameObject.CompareTag("Right Hand"))
        {
            rightHandTouching = true;
            // Debug.Log("Right hand is touching the button.");
        }
    }


    protected override void OnHoverExited(HoverExitEventArgs args)
    {
        base.OnHoverExited(args);

        if (eventTriggered)
        {
            eventTriggered = false;
        }

        GameObject interactorGameObject = args.interactorObject.transform.gameObject;

        if (interactorGameObject.CompareTag("Left Hand"))
        {
            leftHandTouching = false;
            // Debug.Log("Left hand is no longer touching the button.");
        }
        else if (interactorGameObject.CompareTag("Right Hand"))
        {
            rightHandTouching = false;
            // Debug.Log("Right hand is no longer touching the button.");
        }
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);

        // Hanya izinkan mendorong jika kedua tangan menyentuh
        if (leftHandTouching && rightHandTouching)
        {
            Debug.Log("Both hands are touching. Pushing the button!");

            // Panggil event satu kali
            if (!eventTriggered)
            {
                onTwoHandPushed.Invoke();
                eventTriggered = true;
            }
        }
        else
        {
            onInvalidHandTouching.Invoke();
            Debug.Log("Both hands are not touching. Push denied.");
        }
    }
}
