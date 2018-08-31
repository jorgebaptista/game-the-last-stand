using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JottunAttackBehaviour : StateMachineBehaviour
{
    [SerializeField]
    private bool disableTrigger;

    private EnemyMeleeScript enemyMelee;
    private JottunScript jottunScript;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemyMelee = enemyMelee ?? animator.GetComponent<EnemyMeleeScript>();
        jottunScript = jottunScript ?? animator.GetComponent<JottunScript>();        

        if (!disableTrigger) enemyMelee.ToggleAttackTrigger(true);
        else jottunScript.DoubleAttack();
    }
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (disableTrigger)
        {
            enemyMelee.ToggleAttackTrigger(false);
            jottunScript.DisableAttack();
        }
    }
}
