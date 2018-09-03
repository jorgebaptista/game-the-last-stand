using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallistaChargeEndBehaviour : StateMachineBehaviour
{
    private BallistaScript ballista;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        ballista = ballista ?? animator.GetComponentInParent<BallistaScript>();
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        ballista.SetShootStats(Mathf.Clamp01(stateInfo.normalizedTime));
    }
}
