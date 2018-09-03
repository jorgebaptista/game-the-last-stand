using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeathBehaviour : StateMachineBehaviour
{
    [Header("Settings")]
    [Space]
    [Tooltip("Percentage of animation until it starts fading.")]
    [SerializeField]
    [Range(0, 1)]
    private float fadeTimer;

    private bool isDone;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        isDone = false;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        if (!isDone && animatorStateInfo.normalizedTime >= fadeTimer)
        {
            animator.GetComponent<EnemyScript>().StartFade();
            isDone = true;
        }
    }
}