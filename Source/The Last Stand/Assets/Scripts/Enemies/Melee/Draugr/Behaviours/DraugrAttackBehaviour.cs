using UnityEngine;

public class DraugrAttackBehaviour : StateMachineBehaviour
{
    private EnemyMeleeScript enemyMelee;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemyMelee = enemyMelee ?? animator.GetComponent<EnemyMeleeScript>();

        enemyMelee.ToggleAttackTrigger(true);
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemyMelee.ToggleAttackTrigger(false);
    }
}