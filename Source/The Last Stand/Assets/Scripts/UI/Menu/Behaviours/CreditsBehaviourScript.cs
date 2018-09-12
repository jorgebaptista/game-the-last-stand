using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsBehaviourScript : StateMachineBehaviour
{
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        bool winGame = animator.transform.parent.GetComponent<CreditsScript>().winGame;

        if (!winGame) animator.transform.parent.parent.GetChild(0).gameObject.SetActive(true);

        animator.transform.parent.gameObject.SetActive(false);
    }
}
