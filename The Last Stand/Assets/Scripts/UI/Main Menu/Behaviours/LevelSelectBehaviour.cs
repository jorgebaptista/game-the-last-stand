using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectBehaviour : StateMachineBehaviour
{
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponent<LevelSelectScript>().OpenMainMenu();
    }
}
