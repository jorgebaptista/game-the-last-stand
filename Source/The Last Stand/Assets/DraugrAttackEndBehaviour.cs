using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraugrAttackEndBehaviour : StateMachineBehaviour
{
    private DraugrScript draugrScript;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        draugrScript = draugrScript ?? animator.GetComponent<DraugrScript>();
    }
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        draugrScript.DisableIsAttacking();
    }
}
