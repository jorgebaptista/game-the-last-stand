using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsBehaviourScript : StateMachineBehaviour
{
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.transform.parent.gameObject.SetActive(false);
    }
}
