using UnityEngine;

public class DraugrAttackBehaviour : StateMachineBehaviour
{
    [Header("Settings")]
    [SerializeField]
    [Range(0, 1)]
    private float isAttackingTimer = .6f;

    private bool isDone;

    private EnemyMeleeScript enemyMelee;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemyMelee = enemyMelee ?? animator.GetComponent<EnemyMeleeScript>();

        enemyMelee.ToggleAttackTrigger(true);

        isDone = false;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        if (!isDone && animatorStateInfo.normalizedTime > isAttackingTimer) enemyMelee.ToggleAttackTrigger(false);
    }
}