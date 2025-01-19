using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShoulderCounter : CounterBase
{
    [SerializeField] private Animator anim;
    protected override void OnCounterIsFinished()
    {
        anim.SetBool("isConvulsions", false);
    }
}
