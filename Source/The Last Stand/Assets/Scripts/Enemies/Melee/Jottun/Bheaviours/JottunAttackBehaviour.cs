using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JottunAttackBehaviour : StateMachineBehaviour
{
    private EnemyMeleeScript enemyMelee;
    private JottunScript jottunScript;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemyMelee = enemyMelee ?? animator.GetComponent<EnemyMeleeScript>();
        jottunScript = jottunScript ?? animator.GetComponent<JottunScript>();        

        enemyMelee.ToggleAttackTrigger(true);
    }
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
            enemyMelee.ToggleAttackTrigger(false);
    }
}