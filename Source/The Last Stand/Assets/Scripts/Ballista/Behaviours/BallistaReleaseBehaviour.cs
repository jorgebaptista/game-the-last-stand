using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallistaReleaseBehaviour : StateMachineBehaviour
{
    [Range(0, 1)]
    [SerializeField]
    private float shootTimer = 0.5f;

    private bool isDone;

    private BallistaScript ballista;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        ballista = ballista ?? animator.GetComponentInParent<BallistaScript>();

        isDone = false;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (stateInfo.normalizedTime > shootTimer && !isDone)
        {
            isDone = true;
            ballista.Shoot();
        }
    }
}