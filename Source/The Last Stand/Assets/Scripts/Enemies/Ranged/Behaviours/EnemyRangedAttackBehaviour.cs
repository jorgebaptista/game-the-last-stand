using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRangedAttackBehaviour : StateMachineBehaviour
{
    private DarkElfScript darkElfScript;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        darkElfScript = darkElfScript ?? animator.GetComponent<DarkElfScript>();
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        darkElfScript.DisableAttack();
    }
}
